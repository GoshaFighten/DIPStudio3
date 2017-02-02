namespace DIPStudio3.Controls {
    partial class ProjectControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectControl));
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.operationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCaption = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrentTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTimeElapsed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.beiStart = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.beiD = new DevExpress.XtraBars.BarEditItem();
            this.beiEnd = new DevExpress.XtraBars.BarEditItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyOperationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteOperationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutOperationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runByStepsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // colStatus
            // 
            this.colStatus.ColumnEdit = this.repositoryItemImageComboBox1;
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 1;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.imageCollection1;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.operationBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 22);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.gridControl1.Size = new System.Drawing.Size(508, 374);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // operationBindingSource
            // 
            this.operationBindingSource.DataSource = typeof(DIPStudioCore.Operation);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCaption,
            this.colStatus,
            this.colCurrentTime,
            this.colTimeElapsed});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 20;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // colCaption
            // 
            this.colCaption.FieldName = "Caption";
            this.colCaption.Name = "colCaption";
            this.colCaption.OptionsColumn.ReadOnly = true;
            this.colCaption.Visible = true;
            this.colCaption.VisibleIndex = 0;
            // 
            // colCurrentTime
            // 
            this.colCurrentTime.FieldName = "CurrentTime";
            this.colCurrentTime.Name = "colCurrentTime";
            this.colCurrentTime.Visible = true;
            this.colCurrentTime.VisibleIndex = 2;
            // 
            // colTimeElapsed
            // 
            this.colTimeElapsed.FieldName = "TimeElapsed";
            this.colTimeElapsed.Name = "colTimeElapsed";
            this.colTimeElapsed.Visible = true;
            this.colTimeElapsed.VisibleIndex = 3;
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.beiStart,
            this.beiD,
            this.beiEnd});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 4;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCalcEdit1,
            this.repositoryItemTextEdit1});
            this.barManager1.TransparentEditors = true;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.beiStart),
            new DevExpress.XtraBars.LinkPersistInfo(this.beiD),
            new DevExpress.XtraBars.LinkPersistInfo(this.beiEnd)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // beiStart
            // 
            this.beiStart.AutoFillWidth = true;
            this.beiStart.Caption = "Start";
            this.beiStart.Edit = this.repositoryItemTextEdit1;
            this.beiStart.Id = 0;
            this.beiStart.Name = "beiStart";
            this.beiStart.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.Caption;
            this.beiStart.EditValueChanged += new System.EventHandler(this.bei_EditValueChanged);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // beiD
            // 
            this.beiD.AutoFillWidth = true;
            this.beiD.Caption = "D";
            this.beiD.Edit = this.repositoryItemTextEdit1;
            this.beiD.Id = 1;
            this.beiD.Name = "beiD";
            this.beiD.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.Caption;
            this.beiD.EditValueChanged += new System.EventHandler(this.bei_EditValueChanged);
            // 
            // beiEnd
            // 
            this.beiEnd.AutoFillWidth = true;
            this.beiEnd.Caption = "End";
            this.beiEnd.Edit = this.repositoryItemTextEdit1;
            this.beiEnd.Id = 2;
            this.beiEnd.Name = "beiEnd";
            this.beiEnd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.Caption;
            this.beiEnd.EditValueChanged += new System.EventHandler(this.bei_EditValueChanged);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(508, 22);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 396);
            this.barDockControlBottom.Size = new System.Drawing.Size(508, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 22);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 374);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(508, 22);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 374);
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteItemToolStripMenuItem,
            this.copyOperationToolStripMenuItem,
            this.pasteOperationToolStripMenuItem,
            this.cutOperationToolStripMenuItem,
            this.runProjectToolStripMenuItem,
            this.runByStepsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 136);
            // 
            // deleteItemToolStripMenuItem
            // 
            this.deleteItemToolStripMenuItem.Name = "deleteItemToolStripMenuItem";
            this.deleteItemToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.deleteItemToolStripMenuItem.Text = "delete operation";
            this.deleteItemToolStripMenuItem.Click += new System.EventHandler(this.deleteItemToolStripMenuItem_Click);
            // 
            // copyOperationToolStripMenuItem
            // 
            this.copyOperationToolStripMenuItem.Name = "copyOperationToolStripMenuItem";
            this.copyOperationToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.copyOperationToolStripMenuItem.Text = "copy operation";
            this.copyOperationToolStripMenuItem.Click += new System.EventHandler(this.copyOperationToolStripMenuItem_Click);
            // 
            // pasteOperationToolStripMenuItem
            // 
            this.pasteOperationToolStripMenuItem.Name = "pasteOperationToolStripMenuItem";
            this.pasteOperationToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.pasteOperationToolStripMenuItem.Text = "paste operation";
            this.pasteOperationToolStripMenuItem.Click += new System.EventHandler(this.pasteOperationToolStripMenuItem_Click);
            // 
            // cutOperationToolStripMenuItem
            // 
            this.cutOperationToolStripMenuItem.Name = "cutOperationToolStripMenuItem";
            this.cutOperationToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.cutOperationToolStripMenuItem.Text = "cut operation";
            this.cutOperationToolStripMenuItem.Click += new System.EventHandler(this.cutOperationToolStripMenuItem_Click);
            // 
            // runProjectToolStripMenuItem
            // 
            this.runProjectToolStripMenuItem.Name = "runProjectToolStripMenuItem";
            this.runProjectToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.runProjectToolStripMenuItem.Text = "run project";
            this.runProjectToolStripMenuItem.Click += new System.EventHandler(this.runProjectToolStripMenuItem_Click);
            // 
            // runByStepsToolStripMenuItem
            // 
            this.runByStepsToolStripMenuItem.Name = "runByStepsToolStripMenuItem";
            this.runByStepsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.runByStepsToolStripMenuItem.Text = "run by steps";
            this.runByStepsToolStripMenuItem.Click += new System.EventHandler(this.runByStepsToolStripMenuItem_Click);
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(508, 396);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource operationBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colCaption;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeElapsed;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem beiStart;
        private DevExpress.XtraBars.BarEditItem beiD;
        private DevExpress.XtraBars.BarEditItem beiEnd;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyOperationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteOperationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutOperationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runByStepsToolStripMenuItem;
    }
}
