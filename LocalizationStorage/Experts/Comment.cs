using DevExpress.XtraEditors;

namespace LocalizationStorage {
    public class CommentControl : TranslatinUserControl {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private TextEdit teKey;
        private TextEdit tePath;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private TextEdit teRussian;
        private TextEdit teGerman;
        private TextEdit teEnglish;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private MemoEdit meComment;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup Root;

        public CommentControl(TranslationDe info) : base() {
            InitializeComponent();
            UpdateInfo(info);
        }
        protected override void UpdateInfo(TranslationDe info) {
            meComment.Text = info.Comment;
            teEnglish.Text = info.English;
            teGerman.Text = info.German;
            tePath.Text = info.Path;
            teKey.Text = info.Key;
            teRussian.Text = info.Russian;
        }
        public override string Comment => meComment.Text;
        public override string English => teEnglish.Text;
        public override string Key => teKey.Text;
        public override string Path => tePath.Text;
        private void InitializeComponent() {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.teRussian = new DevExpress.XtraEditors.TextEdit();
            this.teGerman = new DevExpress.XtraEditors.TextEdit();
            this.teEnglish = new DevExpress.XtraEditors.TextEdit();
            this.teKey = new DevExpress.XtraEditors.TextEdit();
            this.tePath = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.meComment = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teRussian.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teGerman.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEnglish.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.meComment);
            this.layoutControl1.Controls.Add(this.teRussian);
            this.layoutControl1.Controls.Add(this.teGerman);
            this.layoutControl1.Controls.Add(this.teEnglish);
            this.layoutControl1.Controls.Add(this.teKey);
            this.layoutControl1.Controls.Add(this.tePath);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(863, 391);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // teRussian
            // 
            this.teRussian.Location = new System.Drawing.Point(77, 232);
            this.teRussian.Name = "teRussian";
            this.teRussian.Properties.ReadOnly = true;
            this.teRussian.Size = new System.Drawing.Size(770, 20);
            this.teRussian.StyleController = this.layoutControl1;
            this.teRussian.TabIndex = 8;
            // 
            // teGerman
            // 
            this.teGerman.Location = new System.Drawing.Point(77, 208);
            this.teGerman.Name = "teGerman";
            this.teGerman.Properties.ReadOnly = true;
            this.teGerman.Size = new System.Drawing.Size(770, 20);
            this.teGerman.StyleController = this.layoutControl1;
            this.teGerman.TabIndex = 7;
            // 
            // teEnglish
            // 
            this.teEnglish.Location = new System.Drawing.Point(65, 135);
            this.teEnglish.Name = "teEnglish";
            this.teEnglish.Properties.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.teEnglish.Properties.Appearance.FontSizeDelta = 4;
            this.teEnglish.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.teEnglish.Properties.Appearance.Options.UseBackColor = true;
            this.teEnglish.Properties.Appearance.Options.UseFont = true;
            this.teEnglish.Properties.Appearance.Options.UseForeColor = true;
            this.teEnglish.Properties.ReadOnly = true;
            this.teEnglish.Size = new System.Drawing.Size(794, 26);
            this.teEnglish.StyleController = this.layoutControl1;
            this.teEnglish.TabIndex = 6;
            // 
            // teKey
            // 
            this.teKey.Location = new System.Drawing.Point(77, 280);
            this.teKey.Name = "teKey";
            this.teKey.Properties.ReadOnly = true;
            this.teKey.Size = new System.Drawing.Size(770, 20);
            this.teKey.StyleController = this.layoutControl1;
            this.teKey.TabIndex = 5;
            // 
            // tePath
            // 
            this.tePath.Location = new System.Drawing.Point(77, 256);
            this.tePath.Name = "tePath";
            this.tePath.Properties.ReadOnly = true;
            this.tePath.Size = new System.Drawing.Size(770, 20);
            this.tePath.StyleController = this.layoutControl1;
            this.tePath.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlGroup1,
            this.emptySpaceItem3,
            this.emptySpaceItem1,
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(863, 391);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teEnglish;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 131);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(859, 30);
            this.layoutControlItem3.Text = "English:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(49, 13);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 171);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(859, 141);
            this.layoutControlGroup1.Text = "Current Translation";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teGerman;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(835, 24);
            this.layoutControlItem4.Text = "German:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(49, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teRussian;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(835, 24);
            this.layoutControlItem5.Text = "Russian:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(49, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tePath;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(835, 24);
            this.layoutControlItem1.Text = "Path:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(49, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teKey;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(835, 24);
            this.layoutControlItem2.Text = "Key:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(49, 13);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 161);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(859, 10);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 312);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 4);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(859, 75);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // meComment
            // 
            this.meComment.Location = new System.Drawing.Point(4, 29);
            this.meComment.Name = "meComment";
            this.meComment.Size = new System.Drawing.Size(855, 102);
            this.meComment.StyleController = this.layoutControl1;
            this.meComment.TabIndex = 12;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.meComment;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(859, 131);
            this.layoutControlItem6.Text = "Comment:";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(49, 13);
            // 
            // Comment
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "Comment";
            this.Size = new System.Drawing.Size(863, 391);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teRussian.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teGerman.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEnglish.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
