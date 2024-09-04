using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Colors;
using DevExpress.Utils.Text;
using DevExpress.XtraBars;
using DevExpress.XtraBars.ToolbarForm;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LocalizationStorage {
    public partial class ClientForm : ToolbarForm {
        DataTable firstTranslateTable = null;
        public ClientForm() {
            InitializeComponent();
            this.IconOptions.ImageUri = "language;Size32x32;Svg";
            repositoryItemImageComboBox1.AddEnum(typeof(TranslationStatus), true);
            UIHelper.SortBySummary(gridView1, colEnglish, ColumnSortOrder.Descending);
            AddFirstTranslation();
            UIHelper.SetColumnAppearance(gridView1.Columns);
            SetRowMenu();
            SetRowVisualInfo();
            bHeader.Caption = "Localization Storage\r\n(German)";
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
        }
        void SetRowVisualInfo() {
            gridView1.RowCellStyle += (s, e) => {
                if(e.Column == colStatus && e.CellValue != null)
                    UpdateAppearance(e.Appearance, (int)e.CellValue);
            };
            gridView1.CustomDrawGroupRow += (s, e) => {
                if(bbGroupCustomization.Down) {
                    if(e.RowHandle != gridView1.FocusedRowHandle) {
                        TranslationStatus status = Source.GetStatusByGroupRowValue(e.RowHandle, gridView1);
                        UpdateAppearance(e.Appearance, (int)status);
                    } else
                        SetColor(e.Appearance, DXSkinColorHelper.GetDXSkinColor(DXSkinColors.FillColors.Warning, 255, LookAndFeel));
                }
            };
        }
        void UpdateAppearance(AppearanceObject app, int status) {
            switch(status) {
                case 1://TranslationStatus.Translated
                    SetColor(app, DXSkinColors.FillColors.Success);
                    break;
                case 2://TranslationStatus.NoTranslationNeeded:
                    SetColor(app, DXSkinColors.FillColors.Primary);
                    break;
                case 3:// TranslationStatus.NotSure:
                    SetColor(app, Color.Yellow);
                    break;
                case 4://TranslationStatus.Problems:
                    SetColor(app, DXSkinColors.FillColors.Danger);
                    break;
            }
        }
        void SetColor(AppearanceObject app, Color color) { 
            app.BackColor = color;
            app.ForeColor = ContrastColor.GetContrastForeColor(color); 
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
                //gridView1.FindPanelItems.AddButton(string.Empty, "Test", (s, args) => {});
                gridView1.ShowFindPanel();
            }
        }
        ExpertDataTableDe Source => Settings.MainDataSet.Tables[Settings.deTableName] as ExpertDataTableDe;
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if(ExpertDataHelper.UpdateGermanData())
                gridControl1.DataSource = Source;
            else {
                MessageBox.Show($"DataFile {Settings.GermanDataSetPath} is not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
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
            gridView1.BeginUpdate();
            try {
                int changedRows = update();
                if(changedRows > 0)
                    bbSave.Enabled = true;
            } finally {
                gridView1.EndUpdate();
                Cursor = Cursors.Default;
            }
        }
        void SaveData() {
            IOHelper.CreateBakFile(Settings.GermanDataSetPath);
            Settings.MainDataSet.WriteXml(Settings.GermanDataSetPath, System.Data.XmlWriteMode.WriteSchema);
            bbSave.Enabled = false;
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
            string key = Source.GetEnglishKeyByLink(e.Link, gridView1);
            if(key != null)
                RowUpdate(() => Source.AddClear(key, 
                    Source.GetKeyByLink(e.Link, gridView1),
                    Source.GetPathByLink(e.Link, gridView1)));
        }
        private void bbGroupCustomization_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            gridView1.LayoutChanged();
        }
        private void bbAddTranslation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            AddTranslation(e.Link);
        }
        void AddTranslation(BarItemLink link) {
            using(TranslationForm form = new TranslationForm(this, Source.GetTranslationObjectByRow(link, gridView1))) {
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
                        form.English, form.Comment, form.Key, form.Path));
            }
        }
    }
}
