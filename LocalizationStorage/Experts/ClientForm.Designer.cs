namespace LocalizationStorage
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEnglish = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGermanNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRussian = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGerman = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colNotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPicture = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.pmGroupRowMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.bbTranslate = new DevExpress.XtraBars.BarButtonItem();
            this.bbNoTranslate = new DevExpress.XtraBars.BarButtonItem();
            this.bbNotSure = new DevExpress.XtraBars.BarButtonItem();
            this.bbClearTranslate = new DevExpress.XtraBars.BarButtonItem();
            this.toolbarFormManager1 = new DevExpress.XtraBars.ToolbarForm.ToolbarFormManager(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.bbSave = new DevExpress.XtraBars.BarButtonItem();
            this.bHeader = new DevExpress.XtraBars.BarHeaderItem();
            this.skinDropDownButtonItem1 = new DevExpress.XtraBars.SkinDropDownButtonItem();
            this.bbGroupCustomization = new DevExpress.XtraBars.BarButtonItem();
            this.bbAddTranslation = new DevExpress.XtraBars.BarButtonItem();
            this.bbComment = new DevExpress.XtraBars.BarButtonItem();
            this.bsUser = new DevExpress.XtraBars.BarStaticItem();
            this.toolbarFormControl1 = new DevExpress.XtraBars.ToolbarForm.ToolbarFormControl();
            this.pmRowMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmGroupRowMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmRowMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.gridControl1.Size = new System.Drawing.Size(1074, 570);
            this.gridControl1.TabIndex = 0;
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
            this.colRussian,
            this.colGerman,
            this.colStatus,
            this.colNotes,
            this.colComment,
            this.colPicture});
            this.gridView1.DetailHeight = 404;
            this.gridView1.FixedLineWidth = 1;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.GroupFormat = "[#image]{1} {2}";
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "Key", null, "")});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsEditForm.PopupEditFormWidth = 933;
            this.gridView1.OptionsLayout.Columns.StoreAllOptions = true;
            this.gridView1.OptionsLayout.StoreAllOptions = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupedColumns = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colEnglish, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.SubstituteFilter += new System.EventHandler<DevExpress.Data.SubstituteFilterEventArgs>(this.gridView1_SubstituteFilter);
            // 
            // colPath
            // 
            this.colPath.FieldName = "Path";
            this.colPath.MinWidth = 17;
            this.colPath.Name = "colPath";
            this.colPath.OptionsColumn.AllowEdit = false;
            this.colPath.OptionsColumn.AllowFocus = false;
            this.colPath.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPath.Visible = true;
            this.colPath.VisibleIndex = 5;
            this.colPath.Width = 215;
            // 
            // colKey
            // 
            this.colKey.FieldName = "Key";
            this.colKey.MinWidth = 17;
            this.colKey.Name = "colKey";
            this.colKey.OptionsColumn.AllowEdit = false;
            this.colKey.OptionsColumn.AllowFocus = false;
            this.colKey.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
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
            this.colEnglish.Visible = true;
            this.colEnglish.VisibleIndex = 1;
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
            this.colGermanNew.OptionsColumn.AllowEdit = false;
            this.colGermanNew.OptionsColumn.AllowFocus = false;
            this.colGermanNew.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colGermanNew.Visible = true;
            this.colGermanNew.VisibleIndex = 2;
            this.colGermanNew.Width = 172;
            // 
            // colRussian
            // 
            this.colRussian.Caption = "Russian (ru)";
            this.colRussian.FieldName = "Russian";
            this.colRussian.MinWidth = 17;
            this.colRussian.Name = "colRussian";
            this.colRussian.OptionsColumn.AllowEdit = false;
            this.colRussian.OptionsColumn.AllowFocus = false;
            this.colRussian.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colGerman
            // 
            this.colGerman.Caption = "German (de)";
            this.colGerman.FieldName = "German";
            this.colGerman.MinWidth = 17;
            this.colGerman.Name = "colGerman";
            this.colGerman.OptionsColumn.AllowEdit = false;
            this.colGerman.OptionsColumn.AllowFocus = false;
            this.colGerman.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colGerman.Visible = true;
            this.colGerman.VisibleIndex = 3;
            this.colGerman.Width = 162;
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
            this.colStatus.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.AllowFocus = false;
            this.colStatus.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colStatus.OptionsColumn.AllowMove = false;
            this.colStatus.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Status", "{0}")});
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 0;
            this.colStatus.Width = 124;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // colNotes
            // 
            this.colNotes.Caption = "Notes";
            this.colNotes.FieldName = "Notes";
            this.colNotes.Name = "colNotes";
            this.colNotes.OptionsColumn.AllowEdit = false;
            this.colNotes.OptionsColumn.AllowFocus = false;
            this.colNotes.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colComment
            // 
            this.colComment.Caption = "Comment";
            this.colComment.FieldName = "Comment";
            this.colComment.Name = "colComment";
            this.colComment.OptionsColumn.AllowEdit = false;
            this.colComment.OptionsColumn.AllowFocus = false;
            this.colComment.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colComment.Visible = true;
            this.colComment.VisibleIndex = 4;
            this.colComment.Width = 202;
            // 
            // colPicture
            // 
            this.colPicture.Caption = "Picture";
            this.colPicture.FieldName = "Picture";
            this.colPicture.Name = "colPicture";
            this.colPicture.OptionsColumn.AllowEdit = false;
            this.colPicture.OptionsColumn.AllowFocus = false;
            this.colPicture.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1214, 348, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1098, 594);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1098, 594);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1078, 574);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // pmGroupRowMenu
            // 
            this.pmGroupRowMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbTranslate),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbNoTranslate),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbNotSure),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbClearTranslate, true)});
            this.pmGroupRowMenu.Manager = this.toolbarFormManager1;
            this.pmGroupRowMenu.Name = "pmGroupRowMenu";
            // 
            // bbTranslate
            // 
            this.bbTranslate.Caption = "Add Translation (Group)";
            this.bbTranslate.Id = 2;
            this.bbTranslate.Name = "bbTranslate";
            this.bbTranslate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbTranslate_ItemClick);
            // 
            // bbNoTranslate
            // 
            this.bbNoTranslate.Caption = "Set \"No Translation Needed\" Status";
            this.bbNoTranslate.Id = 3;
            this.bbNoTranslate.Name = "bbNoTranslate";
            this.bbNoTranslate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbNoTranslate_ItemClick);
            // 
            // bbNotSure
            // 
            this.bbNotSure.Caption = "Set \"Not Sure\" Status";
            this.bbNotSure.Id = 4;
            this.bbNotSure.Name = "bbNotSure";
            this.bbNotSure.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbNotSure_ItemClick);
            // 
            // bbClearTranslate
            // 
            this.bbClearTranslate.Caption = "Remove Translation";
            this.bbClearTranslate.Id = 5;
            this.bbClearTranslate.Name = "bbClearTranslate";
            this.bbClearTranslate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbClearTranslate_ItemClick);
            // 
            // toolbarFormManager1
            // 
            this.toolbarFormManager1.DockControls.Add(this.barDockControl1);
            this.toolbarFormManager1.DockControls.Add(this.barDockControl2);
            this.toolbarFormManager1.DockControls.Add(this.barDockControl3);
            this.toolbarFormManager1.DockControls.Add(this.barDockControl4);
            this.toolbarFormManager1.Form = this;
            this.toolbarFormManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbSave,
            this.bHeader,
            this.bbTranslate,
            this.bbNoTranslate,
            this.bbNotSure,
            this.bbClearTranslate,
            this.skinDropDownButtonItem1,
            this.bbGroupCustomization,
            this.bbAddTranslation,
            this.bbComment,
            this.bsUser});
            this.toolbarFormManager1.MaxItemId = 12;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 31);
            this.barDockControl1.Manager = this.toolbarFormManager1;
            this.barDockControl1.Size = new System.Drawing.Size(1098, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 625);
            this.barDockControl2.Manager = this.toolbarFormManager1;
            this.barDockControl2.Size = new System.Drawing.Size(1098, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 31);
            this.barDockControl3.Manager = this.toolbarFormManager1;
            this.barDockControl3.Size = new System.Drawing.Size(0, 594);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(1098, 31);
            this.barDockControl4.Manager = this.toolbarFormManager1;
            this.barDockControl4.Size = new System.Drawing.Size(0, 594);
            // 
            // bbSave
            // 
            this.bbSave.Caption = "Save Data";
            this.bbSave.Enabled = false;
            this.bbSave.Id = 0;
            this.bbSave.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.bbSave.Name = "bbSave";
            this.bbSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSave_ItemClick);
            // 
            // bHeader
            // 
            this.bHeader.Caption = "Localization Storage (German)";
            this.bHeader.Id = 1;
            this.bHeader.Name = "bHeader";
            // 
            // skinDropDownButtonItem1
            // 
            this.skinDropDownButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.skinDropDownButtonItem1.Id = 7;
            this.skinDropDownButtonItem1.Name = "skinDropDownButtonItem1";
            // 
            // bbGroupCustomization
            // 
            this.bbGroupCustomization.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bbGroupCustomization.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.bbGroupCustomization.Caption = "Group Custom Style";
            this.bbGroupCustomization.Id = 8;
            this.bbGroupCustomization.Name = "bbGroupCustomization";
            this.bbGroupCustomization.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbGroupCustomization_ItemClick);
            // 
            // bbAddTranslation
            // 
            this.bbAddTranslation.Caption = "Add Translation (Row)";
            this.bbAddTranslation.Id = 9;
            this.bbAddTranslation.Name = "bbAddTranslation";
            this.bbAddTranslation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbAddTranslation_ItemClick);
            // 
            // bbComment
            // 
            this.bbComment.Caption = "Add Comment";
            this.bbComment.Id = 10;
            this.bbComment.Name = "bbComment";
            this.bbComment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbComment_ItemClick);
            // 
            // bsUser
            // 
            this.bsUser.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bsUser.Id = 11;
            this.bsUser.Name = "bsUser";
            // 
            // toolbarFormControl1
            // 
            this.toolbarFormControl1.Location = new System.Drawing.Point(0, 0);
            this.toolbarFormControl1.Manager = this.toolbarFormManager1;
            this.toolbarFormControl1.Name = "toolbarFormControl1";
            this.toolbarFormControl1.Size = new System.Drawing.Size(1098, 31);
            this.toolbarFormControl1.TabIndex = 11;
            this.toolbarFormControl1.TabStop = false;
            this.toolbarFormControl1.TitleItemLinks.Add(this.bbSave);
            this.toolbarFormControl1.TitleItemLinks.Add(this.bHeader);
            this.toolbarFormControl1.TitleItemLinks.Add(this.skinDropDownButtonItem1);
            this.toolbarFormControl1.TitleItemLinks.Add(this.bbGroupCustomization);
            this.toolbarFormControl1.TitleItemLinks.Add(this.bsUser);
            this.toolbarFormControl1.ToolbarForm = this;
            // 
            // pmRowMenu
            // 
            this.pmRowMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbAddTranslation),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbNoTranslate),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbComment),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbClearTranslate, true)});
            this.pmRowMenu.Manager = this.toolbarFormManager1;
            this.pmRowMenu.Name = "pmRowMenu";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 625);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Controls.Add(this.toolbarFormControl1);
            this.Name = "ClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ToolbarFormControl = this.toolbarFormControl1;
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmGroupRowMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmRowMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colPath;
        private DevExpress.XtraGrid.Columns.GridColumn colKey;
        private DevExpress.XtraGrid.Columns.GridColumn colEnglish;
        private DevExpress.XtraGrid.Columns.GridColumn colGermanNew;
        private DevExpress.XtraGrid.Columns.GridColumn colRussian;
        private DevExpress.XtraGrid.Columns.GridColumn colGerman;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraBars.PopupMenu pmGroupRowMenu;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colNotes;
        private DevExpress.XtraGrid.Columns.GridColumn colComment;
        private DevExpress.XtraGrid.Columns.GridColumn colPicture;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormControl toolbarFormControl1;
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormManager toolbarFormManager1;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarButtonItem bbSave;
        private DevExpress.XtraBars.BarHeaderItem bHeader;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.BarButtonItem bbTranslate;
        private DevExpress.XtraBars.BarButtonItem bbNoTranslate;
        private DevExpress.XtraBars.BarButtonItem bbNotSure;
        private DevExpress.XtraBars.BarButtonItem bbClearTranslate;
        private DevExpress.XtraBars.SkinDropDownButtonItem skinDropDownButtonItem1;
        private DevExpress.XtraBars.BarButtonItem bbGroupCustomization;
        private DevExpress.XtraBars.BarButtonItem bbAddTranslation;
        private DevExpress.XtraBars.PopupMenu pmRowMenu;
        private DevExpress.XtraBars.BarButtonItem bbComment;
        private DevExpress.XtraBars.BarStaticItem bsUser;
    }
}

