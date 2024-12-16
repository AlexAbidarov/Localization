using DevExpress.XtraEditors;
using System.Linq;

namespace LocalizationStorage {
    public class GroupRowTranslation : TranslatinUserControl {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private MemoEdit teTranslation;
        private MemoEdit teEnglish;
        private SimpleButton sbCopy;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;

        public GroupRowTranslation(TranslationDe info) : base() {
            InitializeComponent();
            UpdateInfo(info);
        }
        string germanWord = string.Empty;
        protected override void UpdateInfo(TranslationDe info) {
            teTranslation.Text = info.Translation;
            teEnglish.Text = info.English;
            gridControl1.DataSource = info.UserTranslation;
            sbCopy.Enabled = info.UserTranslation.Count > 0;
            sbCopy.ToolTip = "Copy the most frequently used word from current translation";
            if(info.UserTranslation.Count > 0)
                germanWord = info.UserTranslation.OrderByDescending(x => x.Count).First().Name;
        }
        public override string Translation => teTranslation.Text;
        public override string English => teEnglish.Text;
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupRowTranslation));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbCopy = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.teTranslation = new DevExpress.XtraEditors.MemoEdit();
            this.teEnglish = new DevExpress.XtraEditors.MemoEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTranslation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEnglish.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this.sbCopy);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.teTranslation);
            this.layoutControl1.Controls.Add(this.teEnglish);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(636, 433);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbCopy
            // 
            this.sbCopy.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sbCopy.ImageOptions.SvgImage")));
            this.sbCopy.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.sbCopy.Location = new System.Drawing.Point(610, 4);
            this.sbCopy.Name = "sbCopy";
            this.sbCopy.Size = new System.Drawing.Size(22, 22);
            this.sbCopy.StyleController = this.layoutControl1;
            this.sbCopy.TabIndex = 13;
            this.sbCopy.Click += new System.EventHandler(this.sbCopy_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(16, 171);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(604, 246);
            this.gridControl1.TabIndex = 12;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.gridColumn2});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn2, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 454;
            // 
            // gridColumn2
            // 
            this.gridColumn2.FieldName = "Count";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 125;
            // 
            // teTranslation
            // 
            this.teTranslation.Location = new System.Drawing.Point(57, 4);
            this.teTranslation.Name = "teTranslation";
            this.teTranslation.Properties.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.teTranslation.Properties.Appearance.FontSizeDelta = 4;
            this.teTranslation.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.teTranslation.Properties.Appearance.Options.UseBackColor = true;
            this.teTranslation.Properties.Appearance.Options.UseFont = true;
            this.teTranslation.Properties.Appearance.Options.UseForeColor = true;
            this.teTranslation.Size = new System.Drawing.Size(549, 58);
            this.teTranslation.StyleController = this.layoutControl1;
            this.teTranslation.TabIndex = 11;
            // 
            // teEnglish
            // 
            this.teEnglish.Location = new System.Drawing.Point(57, 66);
            this.teEnglish.Name = "teEnglish";
            this.teEnglish.Properties.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.teEnglish.Properties.Appearance.FontSizeDelta = 4;
            this.teEnglish.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.teEnglish.Properties.Appearance.Options.UseBackColor = true;
            this.teEnglish.Properties.Appearance.Options.UseFont = true;
            this.teEnglish.Properties.Appearance.Options.UseForeColor = true;
            this.teEnglish.Properties.ReadOnly = true;
            this.teEnglish.Size = new System.Drawing.Size(575, 58);
            this.teEnglish.StyleController = this.layoutControl1;
            this.teEnglish.TabIndex = 6;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlGroup1,
            this.layoutControlItem8,
            this.emptySpaceItem3,
            this.emptySpaceItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(636, 433);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teEnglish;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 62);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(632, 62);
            this.layoutControlItem3.Text = "English:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(41, 13);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 134);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(632, 295);
            this.layoutControlGroup1.Text = "Current Translation";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(608, 250);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.teTranslation;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(606, 62);
            this.layoutControlItem8.Text = "German:";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(41, 13);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 124);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(632, 10);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(606, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(26, 36);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbCopy;
            this.layoutControlItem2.Location = new System.Drawing.Point(606, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(26, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // GroupRowTranslation
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "GroupRowTranslation";
            this.Size = new System.Drawing.Size(636, 433);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTranslation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEnglish.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }
        private void sbCopy_Click(object sender, System.EventArgs e) {
            teTranslation.Text = germanWord;
        }
    }
}
