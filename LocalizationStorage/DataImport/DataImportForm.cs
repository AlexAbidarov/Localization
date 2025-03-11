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
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
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
        private DevExpress.XtraLayout.LayoutControlGroup Root;

        public DataImportForm(Form owner) {
            InitializeComponent();
            Location = UIHelper.GetCenterPoint(owner, Size);
        }
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataImportForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbShowExtraData = new DevExpress.XtraEditors.CheckEdit();
            this.sbMerge = new DevExpress.XtraEditors.SimpleButton();
            this.beFile = new DevExpress.XtraEditors.ButtonEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbRemove = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbShowExtraData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
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
            this.gridControl1.Location = new System.Drawing.Point(413, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1008, 444);
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
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.splitterItem1,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem5});
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
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(401, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1012, 448);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbMerge;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 422);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(391, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
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
            this.emptySpaceItem1.Size = new System.Drawing.Size(391, 348);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cbShowExtraData;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(391, 24);
            this.layoutControlItem4.TextVisible = false;
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
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbRemove;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 396);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(391, 26);
            this.layoutControlItem5.TextVisible = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
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
                if(isTableExist)
                    gridControl1.DataSource = GetData(cbShowExtraData.Checked);
                else gridControl1.DataSource = null;
                gridView1.PopulateColumns();
                UIHelper.SetGridReadOnly(gridView1, false);
                bool isNotNullData = gridControl1.DataSource != null && gridView1.RowCount > 0;
                sbMerge.Enabled = isNotNullData && !cbShowExtraData.Checked;
                sbRemove.Enabled = isNotNullData && cbShowExtraData.Checked;
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
    }
}
