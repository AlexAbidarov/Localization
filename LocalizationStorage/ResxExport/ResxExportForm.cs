using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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
        private TextEdit tePath;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;

        List<SimpleTranslation> source = null;
        static readonly string satteliteExt = "de";
        private SimpleButton sbExport;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        static readonly string root = @"D:\Work\v24.2\Localization";

        public ResxExportForm(Form owner, List<SimpleTranslation> data) {
            InitializeComponent();
            Location = UIHelper.GetCenterPoint(owner, Size);
            source = data;
            gridControl1.DataSource = source;
            InitGrid();
            tePath.Text = root;
            if(Directory.Exists(tePath.Text))
                UpdateDataPath(tePath.Text);
            else sbExport.Enabled = false;
        }
        void UpdateDataPath(string root) {
            source.ForEach(row => row.DePath = LocalizationHelper.GetSatellitePath(root, row.Path, satteliteExt));
        }
        void InitGrid() {
            gridView1.Columns.ForEach(col => col.OptionsColumn.AllowFocus = false);
            if(gridView1.Columns.Count > 0) {
                GridColumn column = gridView1.Columns[0];
                column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            }
        }
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResxExportForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbExport = new DevExpress.XtraEditors.SimpleButton();
            this.tePath = new DevExpress.XtraEditors.TextEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbExport);
            this.layoutControl1.Controls.Add(this.tePath);
            this.layoutControl1.Controls.Add(this.gridControl1);
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
            this.sbExport.Size = new System.Drawing.Size(1374, 22);
            this.sbExport.StyleController = this.layoutControl1;
            this.sbExport.TabIndex = 6;
            this.sbExport.Text = "Export Data to ResXs";
            this.sbExport.Click += new System.EventHandler(this.sbExport_Click);
            // 
            // tePath
            // 
            this.tePath.Location = new System.Drawing.Point(108, 12);
            this.tePath.Name = "tePath";
            this.tePath.Size = new System.Drawing.Size(1278, 20);
            this.tePath.StyleController = this.layoutControl1;
            this.tePath.TabIndex = 5;
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
            this.layoutControlItem3});
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
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tePath;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1378, 24);
            this.layoutControlItem2.Text = "Localization Path:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbExport;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1378, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.tePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }
        protected override bool ProcessDialogKey(Keys keyData) {
            if(keyData == Keys.Escape) Close();
            return false;
        }
        private void sbExport_Click(object sender, System.EventArgs e) {
            int count = 0;
            Cursor = Cursors.WaitCursor;
            try {
                source.ForEach(r => {
                    if(string.IsNullOrEmpty(r.DePath)) {
                        ResxExportHelper.CreateSatellite(root, r.Path, r.DePath);
                    }
                    if(!string.IsNullOrEmpty(r.DePath)) {
                        FileInfo fi = new FileInfo(r.DePath);
                        if(ResxExportHelper.SetXValue(fi, r.Key, r.Translation
                            , fi.FullName.IndexOf("DevExpress.XtraEditors") >= 0 //TODO check all values
                            ))
                            count++;
                    }

                });
            } finally {
                Cursor = Cursors.Default;
            }
            XtraMessageBox.Show($"Export {count} values.", "ResX Export");
        }
    }
}
