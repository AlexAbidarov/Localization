using DevExpress.XtraEditors;
using LocalizationStorage.ResxExport;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LocalizationStorage {
    public class ResxExportForm : XtraForm {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;

        readonly List<SimpleTranslation> source = null;
        static readonly string satteliteExt = "de";
        private SimpleButton sbExport;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private ButtonEdit tePath;
        static readonly string root = @"D:\Work\v25.2\Localization";

        public ResxExportForm(Form owner, List<SimpleTranslation> data) {
            InitializeComponent();
            Location = UIHelper.GetCenterPoint(owner, Size);
            source = data;
            gridControl1.DataSource = source;
            UIHelper.SetGridReadOnly(gridView1, false);
            tePath.Text = root;
            if(Directory.Exists(tePath.Text)) {
                UpdateDataPath(tePath.Text);
                CreateEmptySatellites();
            }
            else sbExport.Enabled = false;

        }
        void UpdateDataPath(string root) {
            source.ForEach(row => row.DePath = LocalizationHelper.GetSatellitePath(root, row.Path, satteliteExt));
            gridView1.LayoutChanged();
        }
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResxExportForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbExport = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.tePath = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbExport);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.tePath);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1398, 468);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbExport
            // 
            this.sbExport.Location = new System.Drawing.Point(12, 36);
            this.sbExport.Name = "sbExport";
            this.sbExport.Size = new System.Drawing.Size(441, 22);
            this.sbExport.StyleController = this.layoutControl1;
            this.sbExport.TabIndex = 6;
            this.sbExport.Text = "Export Data to ResXs";
            this.sbExport.Click += new System.EventHandler(this.sbExport_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 62);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1374, 394);
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
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1398, 468);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1378, 398);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tePath;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(445, 24);
            this.layoutControlItem2.Text = "Localization Path:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbExport;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(445, 26);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.Location = new System.Drawing.Point(445, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(933, 50);
            // 
            // tePath
            // 
            this.tePath.Location = new System.Drawing.Point(108, 12);
            this.tePath.Name = "tePath";
            this.tePath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tePath.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.tePath.Size = new System.Drawing.Size(345, 20);
            this.tePath.StyleController = this.layoutControl1;
            this.tePath.TabIndex = 5;
            this.tePath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.tePath_ButtonClick);
            this.tePath.EditValueChanged += new System.EventHandler(this.tePath_EditValueChanged);
            // 
            // ResxExportForm
            // 
            this.ClientSize = new System.Drawing.Size(1398, 468);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ResxExportForm.IconOptions.SvgImage")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "ResxExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ResX Export";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePath.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        protected override bool ProcessDialogKey(Keys keyData) {
            if(keyData == Keys.Escape) Close();
            return false;
        }
        void CreateEmptySatellites() {
            source.ForEach(row => {
                if(string.IsNullOrEmpty(row.DePath))
                    row.DePath = ResxExportHelper.CreateSatellite(tePath.Text, row.Path, satteliteExt);
            });
        }
        int DoStepToStepExport() {
            int count = 0;
            source.ForEach(row => {
                if(ResxExportHelper.IsTestValue(row.DePath))
                    count += ResxExportHelper.ChangeXValue(row.DePath, row.Key, row.Translation);
            });
            return count;
        }
        int DoOptimizedExport() {
            int count = 0;
            Dictionary<string, string> valuePairs = [];
            string path = null;
            source.ForEach(row => {
                if(ResxExportHelper.IsTestValue(row.DePath)) {
                    if(path != row.DePath) {
                        count += ResxExportHelper.ChangeXValues(path, valuePairs);
                        valuePairs.Clear();
                        path = row.DePath;
                    }
                    if(!valuePairs.ContainsKey(row.Key)) //TODO some resources have duplicate keys(!)
                        valuePairs.Add(row.Key, row.Translation);
                }
            });
            count += ResxExportHelper.ChangeXValues(path, valuePairs);
            return count;
        }
        private void sbExport_Click(object sender, System.EventArgs e) {
            int count;
            Cursor = Cursors.WaitCursor;
            try {
                ElapsedTime.Start();
                count = DoOptimizedExport();
                ElapsedTime.Stop();
            } finally {
                Cursor = Cursors.Default;
            }
            XtraMessageBox.Show($"Export {count} values. Time: {ElapsedTime.GetNuGetTime()}", "ResX Export");
        }
        private void tePath_EditValueChanged(object sender, System.EventArgs e) {
            UpdateDataPath(tePath.Text);
            CreateEmptySatellites();
        }
        private void tePath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            string folderName = ResxExportHelper.GetFolder(tePath.Text, this);
            if(!string.IsNullOrEmpty(folderName))
                tePath.Text = folderName;
        }
    }
}
