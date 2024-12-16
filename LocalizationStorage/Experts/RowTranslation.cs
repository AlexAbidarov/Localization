using DevExpress.AIIntegration;
using DevExpress.XtraEditors;
using LocalizationStorage.AI;

namespace LocalizationStorage {
    public class RowTranslation : TranslatinUserControl {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private TextEdit teKey;
        private TextEdit tePath;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private TextEdit teRussian;
        private TextEdit teGerman;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private MemoEdit teTranslation;
        private MemoEdit teEnglish;
        private SimpleButton sbCopy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private SimpleButton sbAI;
        private DevExpress.XtraLayout.LayoutControlItem lcAI;
        private DevExpress.XtraLayout.LayoutControlGroup Root;

        public RowTranslation(TranslationDe info) : base() {
            InitializeComponent();
            UpdateInfo(info);
        }
        string requestCondition = null;
        protected override void UpdateInfo(TranslationDe info) {
            teTranslation.Text = info.Translation;
            teEnglish.Text = info.English;
            teGerman.Text = info.German;
            tePath.Text = info.Path;
            teKey.Text = info.Key;
            teRussian.Text = info.Russian;
            sbCopy.Enabled = LocalizationHelper.ValueExist(info.German);
            sbCopy.ToolTip = "Copy value from current translation";
            lcAI.Enabled = AISettings.Default.KeysExist && AISettings.Default.RequestExists;
            requestCondition = GermanTranslateRequest.GetRequestCondition(info.English, info.PureRussian, info.PureGerman);
            sbAI.ToolTip = $"Get a translation generated via AI with a condition:\r\n{requestCondition}";
        }
        public override string Translation => teTranslation.Text;
        public override string English => teEnglish.Text;
        public override string Key => teKey.Text;
        public override string Path => tePath.Text;
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RowTranslation));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbAI = new DevExpress.XtraEditors.SimpleButton();
            this.sbCopy = new DevExpress.XtraEditors.SimpleButton();
            this.teRussian = new DevExpress.XtraEditors.TextEdit();
            this.teGerman = new DevExpress.XtraEditors.TextEdit();
            this.teKey = new DevExpress.XtraEditors.TextEdit();
            this.tePath = new DevExpress.XtraEditors.TextEdit();
            this.teTranslation = new DevExpress.XtraEditors.MemoEdit();
            this.teEnglish = new DevExpress.XtraEditors.MemoEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcAI = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teRussian.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teGerman.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTranslation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEnglish.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAI)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this.sbAI);
            this.layoutControl1.Controls.Add(this.sbCopy);
            this.layoutControl1.Controls.Add(this.teRussian);
            this.layoutControl1.Controls.Add(this.teGerman);
            this.layoutControl1.Controls.Add(this.teKey);
            this.layoutControl1.Controls.Add(this.tePath);
            this.layoutControl1.Controls.Add(this.teTranslation);
            this.layoutControl1.Controls.Add(this.teEnglish);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(945, 377);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbAI
            // 
            this.sbAI.Location = new System.Drawing.Point(919, 30);
            this.sbAI.Name = "sbAI";
            this.sbAI.Size = new System.Drawing.Size(22, 22);
            this.sbAI.StyleController = this.layoutControl1;
            this.sbAI.TabIndex = 13;
            this.sbAI.Text = "AI";
            this.sbAI.Click += new System.EventHandler(this.sbAI_Click);
            // 
            // sbCopy
            // 
            this.sbCopy.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sbCopy.ImageOptions.SvgImage")));
            this.sbCopy.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.sbCopy.Location = new System.Drawing.Point(919, 4);
            this.sbCopy.Name = "sbCopy";
            this.sbCopy.Size = new System.Drawing.Size(22, 22);
            this.sbCopy.StyleController = this.layoutControl1;
            this.sbCopy.TabIndex = 12;
            this.sbCopy.Click += new System.EventHandler(this.sbCopy_Click);
            // 
            // teRussian
            // 
            this.teRussian.Location = new System.Drawing.Point(69, 281);
            this.teRussian.Name = "teRussian";
            this.teRussian.Properties.ReadOnly = true;
            this.teRussian.Size = new System.Drawing.Size(860, 20);
            this.teRussian.StyleController = this.layoutControl1;
            this.teRussian.TabIndex = 8;
            // 
            // teGerman
            // 
            this.teGerman.Location = new System.Drawing.Point(69, 257);
            this.teGerman.Name = "teGerman";
            this.teGerman.Properties.ReadOnly = true;
            this.teGerman.Size = new System.Drawing.Size(860, 20);
            this.teGerman.StyleController = this.layoutControl1;
            this.teGerman.TabIndex = 7;
            // 
            // teKey
            // 
            this.teKey.Location = new System.Drawing.Point(69, 329);
            this.teKey.Name = "teKey";
            this.teKey.Properties.ReadOnly = true;
            this.teKey.Size = new System.Drawing.Size(860, 20);
            this.teKey.StyleController = this.layoutControl1;
            this.teKey.TabIndex = 5;
            // 
            // tePath
            // 
            this.tePath.Location = new System.Drawing.Point(69, 305);
            this.tePath.Name = "tePath";
            this.tePath.Properties.ReadOnly = true;
            this.tePath.Size = new System.Drawing.Size(860, 20);
            this.tePath.StyleController = this.layoutControl1;
            this.tePath.TabIndex = 4;
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
            this.teTranslation.Size = new System.Drawing.Size(858, 101);
            this.teTranslation.StyleController = this.layoutControl1;
            this.teTranslation.TabIndex = 11;
            // 
            // teEnglish
            // 
            this.teEnglish.Location = new System.Drawing.Point(57, 109);
            this.teEnglish.Name = "teEnglish";
            this.teEnglish.Properties.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.teEnglish.Properties.Appearance.FontSizeDelta = 4;
            this.teEnglish.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.teEnglish.Properties.Appearance.Options.UseBackColor = true;
            this.teEnglish.Properties.Appearance.Options.UseFont = true;
            this.teEnglish.Properties.Appearance.Options.UseForeColor = true;
            this.teEnglish.Properties.ReadOnly = true;
            this.teEnglish.Size = new System.Drawing.Size(884, 101);
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
            this.layoutControlItem6,
            this.emptySpaceItem2,
            this.lcAI});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(945, 377);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teEnglish;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 105);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(941, 105);
            this.layoutControlItem3.Text = "English:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(41, 13);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 220);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(941, 141);
            this.layoutControlGroup1.Text = "Current Translation";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teGerman;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(917, 24);
            this.layoutControlItem4.Text = "German:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(41, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teRussian;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(917, 24);
            this.layoutControlItem5.Text = "Russian:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(41, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tePath;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(917, 24);
            this.layoutControlItem1.Text = "Path:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(41, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teKey;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(917, 24);
            this.layoutControlItem2.Text = "Key:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(41, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.teTranslation;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(915, 105);
            this.layoutControlItem8.Text = "German:";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(41, 13);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 210);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(941, 10);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 361);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 12);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 12);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(941, 12);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sbCopy;
            this.layoutControlItem6.Location = new System.Drawing.Point(915, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(26, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(915, 52);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(26, 53);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcAI
            // 
            this.lcAI.Control = this.sbAI;
            this.lcAI.Location = new System.Drawing.Point(915, 26);
            this.lcAI.Name = "lcAI";
            this.lcAI.Size = new System.Drawing.Size(26, 26);
            this.lcAI.TextSize = new System.Drawing.Size(0, 0);
            this.lcAI.TextVisible = false;
            // 
            // RowTranslation
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "RowTranslation";
            this.Size = new System.Drawing.Size(945, 377);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teRussian.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teGerman.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTranslation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEnglish.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAI)).EndInit();
            this.ResumeLayout(false);

        }
        void sbCopy_Click(object sender, System.EventArgs e) {
            teTranslation.Text = teGerman.Text;
            teTranslation.Properties.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
        }
        async void sbAI_Click(object sender, System.EventArgs e) {
            if(string.IsNullOrEmpty(requestCondition)) return;
            string answer =
                await AIExtensionsContainerDesktop.Default.GermanTranslateAsync(new GermanTranslateRequest(requestCondition));
            if(LocalizationHelper.ValueExist(answer)) {
                teTranslation.Text = answer;//AIHelper.GetTranslation(answer, teTranslation.Text);
                teTranslation.Properties.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
                sbAI.Enabled = false;
            }
        }
    }
}
