using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LocalizationStorage {
    public class EditData : XtraForm {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colPath;
        private DevExpress.XtraGrid.Columns.GridColumn colKey;
        private DevExpress.XtraGrid.Columns.GridColumn colEnglish;
        private DevExpress.XtraGrid.Columns.GridColumn colGermanNew;
        private DevExpress.XtraGrid.Columns.GridColumn colInternalInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colUser;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private SimpleButton sbAddTranslation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;

        public EditData(Form owner, string englishKey) {
            InitializeComponent();
            Location = UIHelper.GetCenterPoint(owner, Size);
            gridControl1.DataSource = Settings.MainTable;
            repositoryItemImageComboBox1.AddEnum(typeof(TranslationStatus), true);
            try {
                englishKey = englishKey.Replace("'", "''");
                gridView1.ActiveFilterCriteria = CriteriaOperator.Parse($"[English] = '{englishKey}'");
            } catch { }
            CheckAutoUpdate();
            gridView1.RowCellStyle += (s, e) => {
                if(e.Column == colStatus && e.CellValue != null)
                    UIHelper.UpdateAppearance(e.Appearance, (int)e.CellValue);
            };
            gridView1.ValidateRow += (s, e) => {
                var row = e.Row as DataRowView;
                if(row != null) {
                    row[Settings.sessionChanged] = true;
                    if(1.Equals(row["Status"]) && string.IsNullOrEmpty($"{row["User"]}"))
                        row["User"] = user;
                }
            };
        }
        readonly string user = "New[20241213]"; //TODO
        readonly HashSet<string> translationSet = [];
        readonly HashSet<int> statusSet = [];
        private void CheckAutoUpdate() {
            for(int i = 0; i < gridView1.RowCount; i++) {
                var row = gridView1.GetRow(i) as DataRowView;
                int status = row["Status"] as int? ?? 0;
                string english = row["English"] as string ?? "";
                string translation = row["NewGerman"] as string ?? "";
                if(status != 0) {
                    statusSet.Add(status);
                    if(!string.IsNullOrEmpty(translation)) translationSet.Add(translation);
                }
            }
            var allow = translationSet.Count == 1 && statusSet.Count == 1;
            sbAddTranslation.Enabled = allow;
        }
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditData));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEnglish = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGermanNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInternalInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbAddTranslation = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbAddTranslation);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1005, 207, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1313, 625);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(18, 56);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.gridControl1.Size = new System.Drawing.Size(1277, 551);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Calibri", 10.25F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPath,
            this.colKey,
            this.colEnglish,
            this.colGermanNew,
            this.colInternalInfo,
            this.colStatus,
            this.colUser});
            this.gridView1.DetailHeight = 404;
            this.gridView1.FixedLineWidth = 1;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "Key", null, "")});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsMenu.ShowFooterItem = true;
            this.gridView1.OptionsView.AllowHtmlDrawGroups = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colPath
            // 
            this.colPath.FieldName = "Path";
            this.colPath.MinWidth = 17;
            this.colPath.Name = "colPath";
            this.colPath.OptionsColumn.AllowEdit = false;
            this.colPath.OptionsColumn.AllowFocus = false;
            this.colPath.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPath.Width = 173;
            // 
            // colKey
            // 
            this.colKey.FieldName = "Key";
            this.colKey.MinWidth = 17;
            this.colKey.Name = "colKey";
            this.colKey.OptionsColumn.AllowEdit = false;
            this.colKey.OptionsColumn.AllowFocus = false;
            this.colKey.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colKey.Width = 71;
            // 
            // colEnglish
            // 
            this.colEnglish.AppearanceCell.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            this.colEnglish.AppearanceCell.Options.UseForeColor = true;
            this.colEnglish.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.colEnglish.AppearanceHeader.Options.UseBackColor = true;
            this.colEnglish.FieldName = "English";
            this.colEnglish.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colEnglish.MinWidth = 17;
            this.colEnglish.Name = "colEnglish";
            this.colEnglish.OptionsColumn.AllowEdit = false;
            this.colEnglish.OptionsColumn.AllowFocus = false;
            this.colEnglish.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.colEnglish.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colEnglish.OptionsColumn.AllowMove = false;
            this.colEnglish.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "English", "{0}")});
            this.colEnglish.Visible = true;
            this.colEnglish.VisibleIndex = 0;
            this.colEnglish.Width = 173;
            // 
            // colGermanNew
            // 
            this.colGermanNew.AppearanceCell.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.colGermanNew.AppearanceCell.Options.UseForeColor = true;
            this.colGermanNew.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.colGermanNew.AppearanceHeader.Options.UseBackColor = true;
            this.colGermanNew.Caption = "German (new)";
            this.colGermanNew.FieldName = "NewGerman";
            this.colGermanNew.MinWidth = 17;
            this.colGermanNew.Name = "colGermanNew";
            this.colGermanNew.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colGermanNew.Visible = true;
            this.colGermanNew.VisibleIndex = 2;
            this.colGermanNew.Width = 231;
            // 
            // colInternalInfo
            // 
            this.colInternalInfo.FieldName = "InternalInfo";
            this.colInternalInfo.MinWidth = 17;
            this.colInternalInfo.Name = "colInternalInfo";
            this.colInternalInfo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInternalInfo.Visible = true;
            this.colInternalInfo.VisibleIndex = 4;
            this.colInternalInfo.Width = 165;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            this.colStatus.AppearanceHeader.Options.UseBackColor = true;
            this.colStatus.Caption = "Status";
            this.colStatus.ColumnEdit = this.repositoryItemImageComboBox1;
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colStatus.OptionsColumn.AllowMove = false;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 1;
            this.colStatus.Width = 146;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // colUser
            // 
            this.colUser.FieldName = "User";
            this.colUser.Name = "colUser";
            this.colUser.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colUser.Visible = true;
            this.colUser.VisibleIndex = 3;
            this.colUser.Width = 161;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1313, 625);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 38);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1283, 557);
            this.layoutControlItem1.TextVisible = false;
            // 
            // sbAddTranslation
            // 
            this.sbAddTranslation.Location = new System.Drawing.Point(18, 18);
            this.sbAddTranslation.Name = "sbAddTranslation";
            this.sbAddTranslation.Size = new System.Drawing.Size(352, 32);
            this.sbAddTranslation.StyleController = this.layoutControl1;
            this.sbAddTranslation.TabIndex = 5;
            this.sbAddTranslation.Text = "Add Translation";
            this.sbAddTranslation.Click += new System.EventHandler(this.sbAddTranslation_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbAddTranslation;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(358, 38);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.Location = new System.Drawing.Point(358, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(925, 38);
            // 
            // EditData
            // 
            this.ClientSize = new System.Drawing.Size(1313, 625);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("EditData.IconOptions.SvgImage")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "EditData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Edit Data (admin)";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }
        protected override bool ProcessDialogKey(Keys keyData) {
            if(keyData == Keys.Escape) Close();
            return false;
        }
        private void sbAddTranslation_Click(object sender, System.EventArgs e) {
            for(int i = 0; i < gridView1.RowCount; i++) {
                var row = gridView1.GetRow(i) as DataRowView;
                int status = row["Status"] as int? ?? 0;
                if(status == 0) {
                    row["Status"] = statusSet.First();
                    if(string.IsNullOrEmpty($"{row["User"]}") && 1.Equals(row["Status"]))
                        row["User"] = user;
                    row["NewGerman"] = translationSet.First();
                    row["SessionChanged"] = true;
                }
            }
            gridView1.RefreshData();
        }
    }
}
