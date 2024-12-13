using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using LocalizationStorage.DataMerging;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocalizationStorage {
    public class DataMergingForm : XtraForm {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private ButtonEdit beFile;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private SimpleButton sbMerge;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DateEdit deDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private RadioGroup rgUsers;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;

        public DataMergingForm(Form owner) {
            InitializeComponent();
            Location = UIHelper.GetCenterPoint(owner, Size);
            deDate.DateOnly = DateOnly.FromDateTime(DateTime.Today);
        }
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataMergingForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.rgUsers = new DevExpress.XtraEditors.RadioGroup();
            this.deDate = new DevExpress.XtraEditors.DateEdit();
            this.sbMerge = new DevExpress.XtraEditors.SimpleButton();
            this.beFile = new DevExpress.XtraEditors.ButtonEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgUsers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.rgUsers);
            this.layoutControl1.Controls.Add(this.deDate);
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
            // rgUsers
            // 
            this.rgUsers.Location = new System.Drawing.Point(12, 60);
            this.rgUsers.Name = "rgUsers";
            this.rgUsers.Properties.Columns = 1;
            this.rgUsers.Properties.ItemVertAlignment = DevExpress.XtraEditors.RadioItemVertAlignment.Top;
            this.rgUsers.Size = new System.Drawing.Size(387, 370);
            this.rgUsers.StyleController = this.layoutControl1;
            this.rgUsers.TabIndex = 8;
            this.rgUsers.SelectedIndexChanged += new System.EventHandler(this.rgUsers_SelectedIndexChanged);
            this.rgUsers.DoubleClick += new System.EventHandler(this.rgUsers_DoubleClick);
            // 
            // deDate
            // 
            this.deDate.EditValue = null;
            this.deDate.Location = new System.Drawing.Point(70, 36);
            this.deDate.Name = "deDate";
            this.deDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.deDate.Size = new System.Drawing.Size(329, 20);
            this.deDate.StyleController = this.layoutControl1;
            this.deDate.TabIndex = 7;
            this.deDate.EditValueChanged += new System.EventHandler(this.deDate_EditValueChanged);
            // 
            // sbMerge
            // 
            this.sbMerge.Location = new System.Drawing.Point(12, 434);
            this.sbMerge.Name = "sbMerge";
            this.sbMerge.Size = new System.Drawing.Size(387, 22);
            this.sbMerge.StyleController = this.layoutControl1;
            this.sbMerge.TabIndex = 6;
            this.sbMerge.Text = "Merge Data";
            this.sbMerge.Click += new System.EventHandler(this.sbMerge_Click);
            // 
            // beFile
            // 
            this.beFile.EditValue = "F:\\Localization\\Import\\241008-Oli\\ExpertDataDe.xml";
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
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.splitterItem1});
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
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.deDate;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(391, 24);
            this.layoutControlItem4.Text = "Date:";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.rgUsers;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(391, 374);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
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
            // DataMergingForm
            // 
            this.ClientSize = new System.Drawing.Size(1433, 468);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("DataMergingForm.IconOptions.SvgImage")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "DataMergingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Data Merging";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgUsers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);

        }
        protected override bool ProcessDialogKey(Keys keyData) {
            if(keyData == Keys.Escape) Close();
            return false;
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            gridControl1.ForceInitialize();
            UpdateData();
        }
        string UserDate => Settings.GetDateString(deDate.DateOnly);
        void deDate_EditValueChanged(object sender, EventArgs e) {
            Text = $"Data Merging [{UserDate}]";
        }
        void UpdateData() {
            bool isTableExist = DataMergingHelper.GetDataSet(beFile.Text).Tables.Count > 0;
            sbMerge.Enabled = false;
            rgUsers.Properties.Items.Clear();
            rgUsers.SelectedIndex = -1;
            if(gridControl1.IsHandleCreated && isTableExist) {
                DataMergingHelper.DataTable.GetUserList().ForEach(x =>
                    rgUsers.Properties.Items.Add(new RadioGroupItem(x, x)));
            }
        }
        void beFile_EditValueChanged(object sender, EventArgs e) {
            UpdateData();
        }
        void UpdateDataSource() {
            if(rgUsers.SelectedIndex == -1)
                gridControl1.DataSource = null;
            else
                gridControl1.DataSource = DataMergingHelper.DataTable.GetUserData(rgUsers.Text, UserDate);
            gridView1.PopulateColumns();
            UIHelper.SetGridReadOnly(gridView1, false);
            sbMerge.Enabled = gridControl1.DataSource != null;
        }
        void rgUsers_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateDataSource();
        }
        private void rgUsers_DoubleClick(object sender, EventArgs e) {
            UpdateDataSource();
        }
        private void beFile_ButtonClick(object sender, ButtonPressedEventArgs e) {
            string fileName = DataMergingHelper.GetFile(beFile.Text, this);
            if(!string.IsNullOrEmpty(fileName))
                beFile.Text = fileName;
        }
        public int RowChanged { get; private set; } = 0;
        void sbMerge_Click(object sender, System.EventArgs e) {
            int rowChangedCount = 0;
            var fromData = gridControl1.DataSource as List<SimpleTranslationDe>;
            Cursor = Cursors.WaitCursor;
            try {
                rowChangedCount = Settings.MainTable.MergeData(fromData, $"{rgUsers.Text}[{UserDate}].txt");
            } finally {
                Cursor = Cursors.Default;
            }
            RowChanged += rowChangedCount;
            XtraMessageBox.Show($"Changed {rowChangedCount} from {fromData.Count} records.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
