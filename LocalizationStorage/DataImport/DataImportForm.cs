using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using LocalizationStorage.DataImport;
using LocalizationStorage.DataMerging;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocalizationStorage {
    public class DataImportForm : XtraForm {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private ButtonEdit beFile;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private SimpleButton sbMerge;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private CheckEdit cbShowExtraData;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private SimpleButton sbRemove;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private SimpleButton sbTry;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup Root;

        public DataImportForm(Form owner) {
            InitializeComponent();
            Location = UIHelper.GetCenterPoint(owner, Size);
        }
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataImportForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbRemove = new DevExpress.XtraEditors.SimpleButton();
            this.cbShowExtraData = new DevExpress.XtraEditors.CheckEdit();
            this.sbMerge = new DevExpress.XtraEditors.SimpleButton();
            this.beFile = new DevExpress.XtraEditors.ButtonEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbTry = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbShowExtraData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl2);
            this.layoutControl1.Controls.Add(this.sbTry);
            this.layoutControl1.Controls.Add(this.sbRemove);
            this.layoutControl1.Controls.Add(this.cbShowExtraData);
            this.layoutControl1.Controls.Add(this.sbMerge);
            this.layoutControl1.Controls.Add(this.beFile);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1433, 468);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbRemove
            // 
            this.sbRemove.Location = new System.Drawing.Point(12, 408);
            this.sbRemove.Name = "sbRemove";
            this.sbRemove.Size = new System.Drawing.Size(387, 22);
            this.sbRemove.StyleController = this.layoutControl1;
            this.sbRemove.TabIndex = 8;
            this.sbRemove.Text = "Remove Extra Data";
            this.sbRemove.Click += new System.EventHandler(this.sbRemove_Click);
            // 
            // cbShowExtraData
            // 
            this.cbShowExtraData.Location = new System.Drawing.Point(12, 36);
            this.cbShowExtraData.Name = "cbShowExtraData";
            this.cbShowExtraData.Properties.Caption = "Show Extra Data";
            this.cbShowExtraData.Size = new System.Drawing.Size(387, 20);
            this.cbShowExtraData.StyleController = this.layoutControl1;
            this.cbShowExtraData.TabIndex = 7;
            this.cbShowExtraData.CheckedChanged += new System.EventHandler(this.cbShowExtraData_CheckedChanged);
            // 
            // sbMerge
            // 
            this.sbMerge.Location = new System.Drawing.Point(12, 434);
            this.sbMerge.Name = "sbMerge";
            this.sbMerge.Size = new System.Drawing.Size(387, 22);
            this.sbMerge.StyleController = this.layoutControl1;
            this.sbMerge.TabIndex = 6;
            this.sbMerge.Text = "Import New Data";
            this.sbMerge.Click += new System.EventHandler(this.sbMerge_Click);
            // 
            // beFile
            // 
            this.beFile.EditValue = "F:\\Localization\\Import\\NewData\\ExpertDataDe.xml";
            this.beFile.Location = new System.Drawing.Point(70, 12);
            this.beFile.Name = "beFile";
            this.beFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beFile.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.beFile.Size = new System.Drawing.Size(329, 20);
            this.beFile.StyleController = this.layoutControl1;
            this.beFile.TabIndex = 5;
            this.beFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beFile_ButtonClick);
            this.beFile.EditValueChanged += new System.EventHandler(this.beFile_EditValueChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(425, 47);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(984, 397);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.splitterItem1,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.tabbedControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1433, 468);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.beFile;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(391, 24);
            this.layoutControlItem2.Text = "Data File:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(46, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbMerge;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 422);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(391, 26);
            this.layoutControlItem3.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.Location = new System.Drawing.Point(391, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(10, 448);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 48);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(391, 322);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cbShowExtraData;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(391, 24);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbRemove;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 396);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(391, 26);
            this.layoutControlItem5.TextVisible = false;
            // 
            // sbTry
            // 
            this.sbTry.Location = new System.Drawing.Point(12, 382);
            this.sbTry.Name = "sbTry";
            this.sbTry.Size = new System.Drawing.Size(387, 22);
            this.sbTry.StyleController = this.layoutControl1;
            this.sbTry.TabIndex = 9;
            this.sbTry.Text = "Try to update \"English\"";
            this.sbTry.Click += new System.EventHandler(this.sbTry_Click);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sbTry;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 370);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(391, 26);
            this.layoutControlItem6.TextVisible = false;
            // 
            // tabbedControlGroup1
            // 
            this.tabbedControlGroup1.Location = new System.Drawing.Point(401, 0);
            this.tabbedControlGroup1.Name = "tabbedControlGroup1";
            this.tabbedControlGroup1.SelectedTabPage = this.layoutControlGroup1;
            this.tabbedControlGroup1.Size = new System.Drawing.Size(1012, 448);
            this.tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup2});
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(988, 401);
            this.layoutControlGroup1.Text = "Extra/New Data";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(988, 401);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(988, 401);
            this.layoutControlGroup2.Text = "Diff English";
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(425, 47);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(984, 397);
            this.gridControl2.TabIndex = 10;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowFooter = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.gridControl2;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(988, 401);
            this.layoutControlItem7.TextVisible = false;
            // 
            // DataImportForm
            // 
            this.ClientSize = new System.Drawing.Size(1433, 468);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("DataImportForm.IconOptions.SvgImage")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "DataImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Data Import";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbShowExtraData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }
        protected override bool ProcessDialogKey(Keys keyData) {
            if(keyData == Keys.Escape) Close();
            return false;
        }
        public int RowChanged { get; private set; } = 0;
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            gridControl1.ForceInitialize();
            UpdateData();
        }
        private void cbShowExtraData_CheckedChanged(object sender, EventArgs e) {
            UpdateData();
        }
        void UpdateData() {
            Cursor = Cursors.WaitCursor;
            try {
                bool isTableExist = DataImportHelper.GetDataSet(beFile.Text).Tables.Count > 0;
                if(isTableExist) {
                    gridControl1.DataSource = GetData(cbShowExtraData.Checked);
                    gridControl2.DataSource = DataImportHelper.DataTable.GetDiffData(Settings.MainTable);
                }
                else {
                    gridControl1.DataSource = null;
                    gridControl2.DataSource = null;
                }
                gridView1.PopulateColumns();
                gridView2.PopulateColumns();
                UIHelper.SetGridReadOnly(gridView1, false);
                UIHelper.SetGridReadOnly(gridView2, false);
                bool isNotNullData = gridControl1.DataSource != null && gridView1.RowCount > 0;
                sbMerge.Enabled = isNotNullData && !cbShowExtraData.Checked;
                sbRemove.Enabled = isNotNullData && cbShowExtraData.Checked;
                sbTry.Enabled = gridView2.RowCount > 0;
            } finally {
                Cursor = Cursors.Default;
            }
        }
        static object GetData(bool extra) {
            if(extra)
                return DataImportHelper.DataTable.GetExtraData(Settings.MainTable);
            return DataImportHelper.DataTable.GetNewData(Settings.MainTable);
        }
        void beFile_EditValueChanged(object sender, EventArgs e) {
            UpdateData();
        }
        private void beFile_ButtonClick(object sender, ButtonPressedEventArgs e) {
            string fileName = DataMergingHelper.GetFile(beFile.Text, this);
            if(!string.IsNullOrEmpty(fileName))
                beFile.Text = fileName;
        }
        void DisableButtons() {
            sbMerge.Enabled = false;
            sbRemove.Enabled = false;
            sbTry.Enabled = false;
        }
        void sbMerge_Click(object sender, System.EventArgs e) {
            int rowChangedCount = 0;
            var fromData = gridControl1.DataSource as List<NewTranslation>;
            Cursor = Cursors.WaitCursor;
            try {
                rowChangedCount = Settings.MainTable.ImportData(fromData, $"New[{Settings.FormattedToday}].txt");
            } finally {
                Cursor = Cursors.Default;
            }
            RowChanged += rowChangedCount;
            XtraMessageBox.Show($"Added {rowChangedCount} from {fromData.Count} records.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DisableButtons();
        }
        private void sbRemove_Click(object sender, EventArgs e) {
            int rowChangedCount = 0;
            var fromData = gridControl1.DataSource as List<SimpleTranslationDe>;
            Cursor = Cursors.WaitCursor;
            try {
                rowChangedCount = Settings.MainTable.RemoveExtraData(fromData, $"Extra[{Settings.FormattedToday}].txt");
            } finally {
                Cursor = Cursors.Default;
            }
            RowChanged += rowChangedCount;
            XtraMessageBox.Show($"Removed {rowChangedCount} from {fromData.Count} records.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DisableButtons();
        }

        private void sbTry_Click(object sender, EventArgs e) {
            var data = gridControl2.DataSource as List<DiffTranslation>;
            var rowChangedCount = Settings.MainTable.SetDiffData(data);
            RowChanged += rowChangedCount;
            XtraMessageBox.Show($"Changed {rowChangedCount} records.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
