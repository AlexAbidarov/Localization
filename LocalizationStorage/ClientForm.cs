using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars.ToolbarForm;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace LocalizationStorage {
    public partial class ClientForm : ToolbarForm {
        DataTable firstTranslateTable = null;
        public ClientForm() {
            InitializeComponent();
            repositoryItemImageComboBox1.AddEnum(typeof(TranslationStatus), true);
            UIHelper.SortBySummary(gridView1, colEnglish, ColumnSortOrder.Descending);
            AddFirstTranslation();
            UIHelper.SetColumnAppearance(gridView1.Columns);
            SetRowMenu();
            SetRowVisualInfo();
        }
        void SetRowMenu() {
            gridView1.PopupMenuShowing += (s, e) => {
                if(e.HitInfo.InGroupRow) {
                    pmWordMenu.Tag = e.HitInfo;
                    e.ShowCustomMenu(pmWordMenu);
                }
            };
        }
        void SetRowVisualInfo() {
            gridView1.RowCellStyle += (s, e) => {
                if(e.Column == colStatus) {
                    switch(e.CellValue) {
                        case 1://TranslationStatus.Translated
                            SetColor(e.Appearance, DXSkinColors.FillColors.Success, Color.White);
                            break;
                        case 2://TranslationStatus.NoTranslationNeeded:
                            SetColor(e.Appearance, DXSkinColors.FillColors.Primary, Color.White);
                            break;
                        case 3:// TranslationStatus.NotSure:
                            e.Appearance.BackColor = Color.Yellow;
                            break;
                        case 4://TranslationStatus.Problems:
                            SetColor(e.Appearance, DXSkinColors.FillColors.Danger, Color.White);
                            break;
                    }
                }
            };
        }
        void SetColor(AppearanceObject app, Color color1, Color color2) { 
            app.BackColor = color1;
            app.ForeColor = color2;
        }
        void AddFirstTranslation() {
            string firstTranslatePath = $"{Settings.DataPath}\\240414.csv";
            if(File.Exists(firstTranslatePath)) {
                firstTranslateTable = CSVHelper.ConvertCSVtoDataTable(new FileInfo(firstTranslatePath));
                var button = gridView1.FindPanelItems.AddButton(string.Empty, null, 
                    (s, args) => {
                        using(EmptyGrid form = new EmptyGrid(this, firstTranslateTable))
                            form.ShowDialog();
                        //DoSync();
                    });
                button.ImageOptions.ImageUri.Uri = "business_idea;Size16x16;Svg";
                button.ToolTip = "Show 240414_DX Data";
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
        void DoSync() {
            List<string> notFound = new List<string>();
            string key, word;
            int count;
            Cursor = Cursors.WaitCursor;
            gridView1.BeginUpdate();
            try {
                foreach(DataRow row in firstTranslateTable.Rows) {
                    key = $"{row["English"]}";
                    word = $"{row["German"]}";
                    count = Source.AddTranslation(key, word);
                    if(count == 0)
                        notFound.Add(key);
                }
                bbSave.Enabled = true;
            } finally {
                gridView1.EndUpdate();
                Cursor = Cursors.Default;
            }
        }
        void RowUpdate(Func<int> update) {
            Cursor = Cursors.WaitCursor;
            gridView1.BeginUpdate();
            try {
                if(update() > 0)
                    bbSave.Enabled = true;
            } finally {
                gridView1.EndUpdate();
                Cursor = Cursors.Default;
            }
        }
        private void bbSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //TODO make backup file
            Settings.MainDataSet.WriteXml($@"Data\{Settings.deDataSetName}", System.Data.XmlWriteMode.WriteSchema);
            bbSave.Enabled = false;
        }

        private void bbNoTranslate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            string key = ExpertDataTableDe.GetEnglishKeyByLink(e.Link, gridView1);
            if(key != null)
                RowUpdate(() => Source.AddNoNeedTranslate(key));
        }

        private void bbTranslate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //TODO RowUpdate(() => Source.AddTranslation("Name", "der Name)" ));
        }

        private void bbNotSure_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            string key = ExpertDataTableDe.GetEnglishKeyByLink(e.Link, gridView1);
            if(key != null)
                RowUpdate(() => Source.AddNotSure(key));
        }

        private void bbClearTranslate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            string key = ExpertDataTableDe.GetEnglishKeyByLink(e.Link, gridView1);
            if(key != null)
                RowUpdate(() => Source.AddClear(key));
        }
    }
}
