using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using LocalizationStorage.Reports;
using System;
using System.Windows.Forms;

namespace LocalizationStorage {
    public partial class MainForm : XtraForm {
        public MainForm() {
            InitializeComponent();
            Text += $"({Settings.RootPath})";
            checkEdit1.Checked = Settings.LoadTranslations;
            SetRowMenu();
        }
        private void simpleButton1_Click(object sender, EventArgs e) {
            Settings.ClearData();
            ElapsedTime.Start();
            Cursor.Current = Cursors.WaitCursor;
            gridControl2.DataSource = Settings.Paths;
            gridControl1.DataSource = Settings.Keys;
            Cursor.Current = Cursors.Default;
            ElapsedTime.Stop();
            simpleButton1.Text = $"Load Data({ElapsedTime.GetNuGetTime()})";
            UIHelper.SetColumnAppearance(gridView1.Columns);
        }

        private void simpleButton2_Click(object sender, EventArgs e) {
            gridView1.Columns.Clear();
            gridView2.Columns.Clear();
        }

        void SetKeyFilter(string value) {
            gridView1.ActiveFilter.Add(colKey, new ColumnFilterInfo($"[Key] = '{value}'"));
            this.tabbedControlGroup1.SelectedTabPage = this.layoutControlGroup2;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) {
            UIHelper.ShowTranslatioDetailForm(gridView1.CalcHitInfo(gridControl1.PointToClient(Cursor.Position)));
        }
        private void checkEdit1_CheckedChanged(object sender, EventArgs e) {
            Settings.LoadTranslations = checkEdit1.Checked;
        }
        void SetRowMenu() {
            gridView4.PopupMenuShowing += KeyMenuShow;
            gridView5.PopupMenuShowing += KeyMenuShow;
            gridView1.PopupMenuShowing += (s, e) => {
                if(e.HitInfo.InDataRow) {
                    pmTranslationRow.Tag = e.HitInfo;
                    e.ShowCustomMenu(pmTranslationRow);
                }
            };
        }
        void KeyMenuShow(object sender, PopupMenuShowingEventArgs e) {
            if(e.HitInfo.InDataRow) {
                pmKeyRow.Tag = e.HitInfo;
                e.ShowCustomMenu(pmKeyRow);
            }
        }
        private void bbiSetKeyFilter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var key = UIHelper.GetKeyByLink(e.Link);
            if(key == null) return;
            SetKeyFilter(key.Name);
        }

        private void bbiCopyKey_ItemClick(object sender, ItemClickEventArgs e) {
            var key = UIHelper.GetKeyByLink(e.Link);
            if(key != null)
                Clipboard.SetText(key.Name);
            else {
                var key2 = UIHelper.GetTranslateByLink(e.Link);
                if(key2 != null)
                    Clipboard.SetText(key2.Key);
            }
        }

        private void bbiCopyValue_ItemClick(object sender, ItemClickEventArgs e) {
            var key = UIHelper.GetKeyByLink(e.Link);
            if(key != null)
                Clipboard.SetText(key.Value);
        }

        private void bbiCopyPath_ItemClick(object sender, ItemClickEventArgs e) {
            var key = UIHelper.GetTranslateByLink(e.Link);
            if(key != null)
                Clipboard.SetText(key.Path);
        }

        private void bbiShowForm_ItemClick(object sender, ItemClickEventArgs e) {
            UIHelper.ShowTranslatioDetailForm(UIHelper.GetGridHitInfoByLink(e.Link));
        }

        private void simpleButton3_Click(object sender, EventArgs e) {
            using(ReplaysForm form = new ReplaysForm(this)) {
                form.ShowDialog();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e) {
            if(UIHelper.ConvertData() > 0) {
                Settings.MainDataSet.WriteXml(Settings.deDataSetName, System.Data.XmlWriteMode.WriteSchema);
                XtraMessageBox.Show($"Table {Settings.deDataSetName} created");
            }
        }
    }
}
