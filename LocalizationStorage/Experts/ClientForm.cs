using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.ToolbarForm;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraSplashScreen;
using LocalizationStorage.AI;
using LocalizationStorage.XLS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LocalizationStorage {
    public partial class ClientForm : ToolbarForm {
        DataTable firstTranslateTable = null;
        public ClientForm() {
            InitializeComponent();
            IconOptions.ImageUri = "language;Size32x32;Svg";
            repositoryItemImageComboBox1.AddEnum(typeof(TranslationStatus), true);
            UIHelper.SortBySummary(gridView1, colEnglish, ColumnSortOrder.Descending);
            AddDataMerging();
            AddResxExport();
            AddDataImport();
            //AddFirstTranslation(); //do it only for the firts version
            AddNotTranslationNeededImport();
            AddProblemResourcesImport();
            CheckPrompts();
            //AddOperationButtons(); //single operation, code left as an example
            UIHelper.SetColumnAppearance(gridView1.Columns);
            SetRowMenu();
            SetRowVisualInfo();
            SetViewHandler();
            bHeader.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            bHeader.Caption = $"Localization Storage (German)<br>User: <r>{Settings.User}</r>";
            bHelp.Caption = "<b>Ctrl + Click</b> <r>- Show online help</r>";
            bbGroupCustomization.Down = true;
            CreateFilterPanel();
            UIHelper.SetColumnAppearance(gridView1.Columns);
            SplashScreenManager.CloseForm();
        }
        void SetRowMenu() {
            bbSave.ImageOptions.ImageUri = "save;Size36x36;Svg";
            bbNoTranslate.ImageOptions.ImageUri = "bo_state;Size16x16;Svg";
            bbNotSure.ImageOptions.ImageUri = "scatterchartlabeloptions;Size16x16;Svg";
            bbAddTranslation.ImageOptions.ImageUri = "bo_transition;Size16x16;Svg";
            bbClearTranslate.ImageOptions.ImageUri = "richeditclearformatting;Size16x16;Svg";
            bbTranslate.ImageOptions.ImageUri = "spreadsheet/group;Size16x16;Svg";
            bbGroupCustomization.ImageOptions.ImageUri = "treemap;Size16x16;Svg";
            bbComment.ImageOptions.ImageUri = "actions_comment;Size16x16;Svg";
            bbVerify.ImageOptions.ImageUri = "check;Size16x16;Svg";
            pmGroupRowMenu.BeforePopup += (s, e) => RemoveNoTranslationNeeded(s as PopupMenu);
            pmRowMenu.BeforePopup += (s, e) => RemoveNoTranslationNeeded(s as PopupMenu);
            gridView1.PopupMenuShowing += (s, e) => {
                if(e.HitInfo.InGroupRow) {
                    pmGroupRowMenu.Tag = e.HitInfo;
                    e.ShowCustomMenu(pmGroupRowMenu);
                    return;
                }
                if(e.HitInfo.InRow && e.HitInfo.RowHandle >= 0) {
                    pmRowMenu.Tag = e.HitInfo;
                    e.ShowCustomMenu(pmRowMenu);
                }
            };
            bbEditNew.ItemClick += (s, e) => {
                string key = Source.GetEnglishKeyByLink(e.Link, gridView1);
                using(var form = new EditData(this, key)) {
                    form.ShowDialog();
                    bbSave.Enabled = true;
                }
            };
        }
        void RemoveNoTranslationNeeded(PopupMenu menu) {
            if(menu != null && !Settings.IsAdmin)
                UIHelper.RemoveLinksFromMenu(menu, "No Translation Needed");
        }
        void SetRowVisualInfo() {
            gridView1.RowCellStyle += (s, e) => {
                if(e.Column == colStatus && e.CellValue != null)
                    UIHelper.UpdateAppearance(e.Appearance, (int)e.CellValue);
            };
            gridView1.CustomDrawGroupRow += (s, e) => {
                if(bbGroupCustomization.Down) {
                    if(e.RowHandle != gridView1.FocusedRowHandle) {
                        TranslationStatus status = Source.GetStatusByGroupRowValue(e.RowHandle, gridView1);
                        UIHelper.UpdateAppearance(e.Appearance, (int)status);
                    }
                    else { }
                    //SetColor(e.Appearance, DXSkinColorHelper.GetDXSkinColor(DXSkinColors.FillColors.Warning, 255, LookAndFeel));
                }
            };
        }
        void SetViewHandler() {
            gridView1.KeyDown += (s, e) => {
                if((e.KeyData == Keys.Space || e.KeyData == Keys.Enter) &&
                    gridView1.IsValidRowHandle(gridView1.FocusedRowHandle) && !gridView1.IsFilterRow(gridView1.FocusedRowHandle))
                    AddTranslation(gridView1.FocusedRowHandle);
                if(e.KeyData == (Keys.M | Keys.Alt))
                    GridHelper.ShowRowPopuMenu(gridView1, pmGroupRowMenu, pmRowMenu);
            };
            gridView1.DoubleClick += (s, e) => {
                DXMouseEventArgs args = e as DXMouseEventArgs;
                var hInfo = gridView1.CalcHitInfo(args.X, args.Y);
                if(hInfo.InDataRow || hInfo.InGroupRow) {
                    AddTranslation(hInfo.RowHandle);
                    args.Handled = true;
                }
            };
        }
        void CreateFilterPanel() {
            FilterPanel panel = new(gridView1, bFilter);
            panel.AddFilterItem("<b>Not</b> Translated", "[Status] = 0");
            panel.AddFilterItem("Translated", "[Status] = 1");
            panel.AddFilterItem("Accepted (<b>Needs Verification</b>)", "[Status] = 6");
            panel.AddFilterItem("Accepted (<b>Auto</b>)", "[Status] = 5");
            panel.AddFilterItem("Translations with <b>problems</b>", "([Status] = 3 Or [Status] = 4)");
            panel.AddFilterItem("<i>Different</i> translations",
                "[NewGerman] <> [German] And Not IsNullOrEmpty([NewGerman])", "Records where new and old translations do not match");
            panel.AddFilterItem($"Created by {Settings.User}",
                $"[User] = '{Settings.User}' Or StartsWith([User], '{Settings.User}[') And EndsWith([User], ']')",
                $"All records modified by {Settings.User}", "bo_lead;Size16x16;Svg", true);
            panel.AddFilterItem($"Changed Records", $"[SessionChanged] = True", "Records modified during this session", "bo_audit_changehistory;Size16x16;Svg", true);
            panel.AddFilterEx(new List<Tuple<string, string>>() {
                new Tuple<string, string>("IsNullOrEmpty([User])", "User Is Empty"),
                new Tuple<string, string>("Not IsNullOrEmpty([User])", "User Is Not Empty"),
                new Tuple<string, string>(string.Empty, "No Additional Filter")
            }, Settings.IsAdmin ? 2 : 0);
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            if(IsUnsavedData &&
                XtraMessageBox.Show("There is unsaved data. Save?", "Data Set", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                SaveData();
        }
        readonly string traslationName = "240414";
        void AddFirstTranslation() {
            string firstTranslatePath = $"{Settings.DataPath}\\{traslationName}.csv";
            if(File.Exists(firstTranslatePath)) {
                firstTranslateTable = CSVHelper.ConvertCSVtoDataTable(new FileInfo(firstTranslatePath));
                var button = gridView1.FindPanelItems.AddButton(string.Empty, null,
                    (s, args) => {
                        using(EmptyGrid form = new EmptyGrid(this, firstTranslateTable))
                            form.ShowDialog();
                        //RowUpdate(() => DoSync());
                    });
                button.ImageOptions.ImageUri.Uri = "business_idea;Size16x16;Svg";
                button.ToolTip = $"Show {traslationName}_DX Data";
                gridView1.ShowFindPanel();
            }
        }
        void AddNotTranslationNeededImport(string name = "Internal data export") {
            if(!Settings.IsAdmin) return;
            var button = gridView1.FindPanelItems.AddButton(string.Empty, null,
                    (s, args) => {
                        Cursor = Cursors.WaitCursor;
                        try {
                            var serializer = new XmlSerializer(typeof(HashSet<string>));
                            using(var fs = new FileStream($"{Settings.DataPath}\\NoTranslationNeeded.xml", FileMode.Create)) {
                                serializer.Serialize(fs, Source.GetNoTranslationNeededKeys());
                            }
                        } finally {
                            Cursor = Cursors.Default;
                        }
                    });
            button.ImageOptions.ImageUri.Uri = "export;Size16x16;Svg";
            button.ToolTip = name;
            gridView1.ShowFindPanel();
        }
        void AddProblemResourcesImport(string name = "Create xls file for the problem resources") {
            if(!Settings.IsAdmin) return;
            var button = gridView1.FindPanelItems.AddButton(string.Empty, null,
                    (s, args) => {
                        Cursor = Cursors.WaitCursor;
                        try {
                            XLSHelper.DoXLSImport(Source.TranslationRows, @$"{Settings.DataPath}\TranslationProblems.de.xlsx");
                        } finally {
                            Cursor = Cursors.Default;
                        }
                    });
            button.ImageOptions.ImageUri.Uri = "business_idea;Size16x16;Svg";
            button.ToolTip = name;
            gridView1.ShowFindPanel();
        }
        void CheckPrompts(string name = "Make prompts resources to untranslatable") {
            if(!Settings.IsAdmin) return;
            var button = gridView1.FindPanelItems.AddButton(string.Empty, null,
                    (s, args) => {
                        Cursor = Cursors.WaitCursor;
                        try {
                            Source.MakePromptsUntranslatable();
                        } finally {
                            Cursor = Cursors.Default;
                        }
                    });
            button.ImageOptions.ImageUri.Uri = "actions_forbid;Size16x16;Svg";
            button.ToolTip = name;
            gridView1.ShowFindPanel();
        }
        void AddResxExport(string name = "Resx Export") {
            if(!Settings.IsAdmin) return;
            var button = gridView1.FindPanelItems.AddButton(string.Empty, name,
                    (s, args) => {
                        using(var form = new ResxExportForm(this, Source.GetTranslationData(
                            (Control.ModifierKeys & System.Windows.Forms.Keys.Control) == System.Windows.Forms.Keys.Control)))
                            form.ShowDialog();
                    });
            button.ImageOptions.ImageUri.Uri = "exportas;Size16x16;Svg";
            button.AllowHtmlTextInToolTip = DefaultBoolean.True;
            button.ToolTip = $"{name}<br><b>Ctrl + Click</b> <r>- Simplified selection</r>";
            gridView1.ShowFindPanel();
        }
        void AddDataMerging(string name = "Data Merging") {
            if(!Settings.IsAdmin) return;
            var button = gridView1.FindPanelItems.AddButton(string.Empty, name,
                    (s, args) => {
                        gridView1.BeginDataUpdate();
                        try {
                            using(var form = new DataMergingForm(this)) {
                                form.ShowDialog();
                                if(form.RowChanged > 0)
                                    bbSave.Enabled = true;
                            }
                        } finally {
                            gridView1.EndDataUpdate();
                        }
                    });
            button.ImageOptions.ImageUri.Uri = "mergeacross;Size16x16;Svg";
            button.ToolTip = name;
            gridView1.ShowFindPanel();
        }
        void AddAdminPrivileges() {
            if(!Settings.IsAdmin) return;
            GridColumn column = gridView1.Columns.Add();
            column.OptionsColumn.AllowFocus = false;
            column.OptionsColumn.AllowEdit = false;
            column.FieldName = "InternalInfo";
            column.Visible = true;
            var link = pmRowMenu.ItemLinks[0];
            link.BeginGroup = true;
            pmRowMenu.InsertItem(link, bbEditNew);
        }
        void AddDataImport(string name = "New Data Import") {
            if(!Settings.IsAdmin) return;
            var button = gridView1.FindPanelItems.AddButton(string.Empty, name,
                    (s, args) => {
                        Cursor = Cursors.WaitCursor;
                        gridView1.BeginDataUpdate();
                        try {
                            using(var form = new DataImportForm(this)) {
                                form.ShowDialog();
                                if(form.RowChanged > 0)
                                    bbSave.Enabled = true;
                            }
                        } finally {
                            gridView1.EndDataUpdate();
                            Cursor = Cursors.Default;
                        }
                    });
            button.ToolTip = name;
            gridView1.ShowFindPanel();
        }
        void AddOperationButtons() {
            gridView1.FindPanelItems.AddButton(string.Empty, "Automatic translation", (s, args) => {
                RowUpdate(() => DoAutomaticTranslate());
            });
            gridView1.ShowFindPanel();
        }
        ExpertDataTableDe Source => Settings.MainTable;
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            AddAdminPrivileges();
            if(ExpertDataHelper.UpdateGermanData())
                gridControl1.DataSource = Source;
            else {
                MessageBox.Show($"DataFile {Settings.GermanDataSetPath} is not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
        int DoAutomaticTranslate() {
            gridView1.CollapseAllGroups();
            int count = 0;
            int groupRowCount = gridView1.RowCount;
            for(int i = -1; i >= -groupRowCount; i--) {
                if(gridView1.IsGroupRow(i)) {
                    string key = Source.GetEnglishKey(i, gridView1);
                    count += Source.TryToAddAutomaticTranslate(key, i, gridView1);
                }
            }
            return count;
        }
        int DoSync() {
            List<string> notFoundKeys = new List<string>();
            string key, word;
            int count, countRows = 0;
            foreach(DataRow row in firstTranslateTable.Rows) {
                key = $"{row["English"]}";
                word = $"{row["German"]}";
                count = Source.AddTranslation(key, word);
                if(count == 0)
                    notFoundKeys.Add(key);
                countRows += count;
            }
            IOHelper.SaveLog(notFoundKeys, $"{traslationName}_Errors.txt");
            return countRows;
        }
        bool IsUnsavedData => bbSave.Enabled;
        void RowUpdate(Func<int> update) {
            Cursor = Cursors.WaitCursor;
            gridView1.BeginDataUpdate();
            try {
                int changedRows = update();
                if(changedRows > 0)
                    bbSave.Enabled = true;
            } finally {
                gridView1.EndDataUpdate();
                Cursor = Cursors.Default;
            }
        }
        void SaveData() {
            Source.BeginDataSave(gridView1);
            try {
                IOHelper.CreateBakFile(Settings.GermanDataSetPath);
                Settings.MainDataSet.WriteXml(Settings.GermanDataSetPath, System.Data.XmlWriteMode.WriteSchema);
                bbSave.Enabled = false;
            } finally {
                Source.EndDataSave(gridView1);
            }
        }
        private void bbSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveData();
        }
        private void bbNoTranslate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            string key = Source.GetEnglishKeyByLink(e.Link, gridView1);
            if(key != null)
                RowUpdate(() => Source.AddNoNeedTranslate(key,
                    Source.GetKeyByLink(e.Link, gridView1),
                    Source.GetPathByLink(e.Link, gridView1)));
        }
        private void bbTranslate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            AddTranslation(e.Link);
        }
        private void bbNotSure_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            string key = Source.GetEnglishKeyByLink(e.Link, gridView1);
            if(key != null)
                RowUpdate(() => Source.AddNotSure(key));
        }
        private void bbClearTranslate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(XtraMessageBox.Show("Are you sure that you want to remove the German translation?",
                "Remove Translation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                string key = Source.GetEnglishKeyByLink(e.Link, gridView1);
                if(key != null)
                    RowUpdate(() => Source.AddClear(key,
                        Source.GetKeyByLink(e.Link, gridView1),
                        Source.GetPathByLink(e.Link, gridView1)));
            }
        }
        private void bbGroupCustomization_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            gridView1.LayoutChanged();
        }
        private void bbAddTranslation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            AddTranslation(e.Link);
        }
        void AddTranslation(BarItemLink link) {
            AddTranslation(Source.GetTranslationObjectByRow(link, gridView1));
        }
        void AddTranslation(int rowHandle) {
            AddTranslation(Source.GetTranslationObjectByRow(rowHandle, gridView1));
        }
        void AddTranslation(TranslationDe userTranslation) {
            using(TranslationForm form = new TranslationForm(this, userTranslation)) {
                if(form.ShowDialog() == DialogResult.OK &&
                    !string.IsNullOrEmpty(form.Translation.Trim())) {
                    RowUpdate(() => Source.AddTranslation(form.English,
                        form.Translation, TranslationStatus.Translated, form.Key, form.Path));
                }
            }
        }
        private void bbComment_ItemClick(object sender, ItemClickEventArgs e) {
            using(CommentForm form = new CommentForm(this, Source.GetTranslationObjectByRow(e.Link, gridView1))) {
                if(form.ShowDialog() == DialogResult.OK)
                    RowUpdate(() => Source.AddComment(
                        form.English, form.Comment, form.Key, form.Path, form.Auto));
            }
        }
        private void bbVerify_ItemClick(object sender, ItemClickEventArgs e) {
            string key = Source.GetEnglishKeyByLink(e.Link, gridView1);
            if(key != null)
                RowUpdate(() => Source.Verify(key,
                    Source.GetKeyByLink(e.Link, gridView1),
                    Source.GetPathByLink(e.Link, gridView1)));
        }
        private void gridView1_SubstituteFilter(object sender, SubstituteFilterEventArgs e) {
            e.Filter |= CriteriaOperator.Parse("[SessionChanged] = true");
        }
        private void bHelp_ItemClick(object sender, ItemClickEventArgs e) {
            Settings.ShowHelp();
        }
        private void bAI_ItemClick(object sender, ItemClickEventArgs e) {
            using(var form = new AISettingsForm())
                form.ShowDialog(this);
        }
    }
}
