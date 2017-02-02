namespace DIPStudio3 {
    partial class MainForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::DIPStudio3.SplashScreen1), true, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnSaveProject = new DevExpress.XtraBars.BarButtonItem();
            this.btnOpenProject = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRun = new DevExpress.XtraBars.BarButtonItem();
            this.bbiStep = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.bbiReset = new DevExpress.XtraBars.BarButtonItem();
            this.btnRights = new DevExpress.XtraBars.BarButtonItem();
            this.btnSwitch = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bsiProject = new DevExpress.XtraBars.BarSubItem();
            this.bciHoriz = new DevExpress.XtraBars.BarCheckItem();
            this.bsiPanels = new DevExpress.XtraBars.BarSubItem();
            this.bbiDefaultLayout = new DevExpress.XtraBars.BarButtonItem();
            this.biPanels = new DevExpress.XtraBars.BarToolbarsListItem();
            this.bsiExport = new DevExpress.XtraBars.BarSubItem();
            this.bbiExport = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSimpleExport = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.bsiImage = new DevExpress.XtraBars.BarStaticItem();
            this.bsiBright = new DevExpress.XtraBars.BarStaticItem();
            this.bsiX = new DevExpress.XtraBars.BarStaticItem();
            this.bsiY = new DevExpress.XtraBars.BarStaticItem();
            this.bsiTime = new DevExpress.XtraBars.BarStaticItem();
            this.bsiHoriz = new DevExpress.XtraBars.BarStaticItem();
            this.bsiRights = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bsiImage,
            this.bsiBright,
            this.bsiX,
            this.bsiY,
            this.biPanels,
            this.bbiRun,
            this.bbiStep,
            this.bsiProject,
            this.bciHoriz,
            this.bbiReset,
            this.bsiHoriz,
            this.btnSaveProject,
            this.btnOpenProject,
            this.bsiPanels,
            this.bbiDefaultLayout,
            this.btnRights,
            this.bsiRights,
            this.bbiExport,
            this.bsiExport,
            this.bbiSimpleExport,
            this.btnSwitch,
            this.barButtonItem1,
            this.barButtonItem2,
            this.bsiTime});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 30;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOpenProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRun, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiStep),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiReset, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRights),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSwitch, DevExpress.XtraBars.BarItemPaintStyle.Standard)});
            this.bar1.Text = "Tools";
            // 
            // btnSaveProject
            // 
            this.btnSaveProject.Caption = "Save Project";
            this.btnSaveProject.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSaveProject.Glyph")));
            this.btnSaveProject.Id = 12;
            this.btnSaveProject.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnSaveProject.LargeGlyph")));
            this.btnSaveProject.Name = "btnSaveProject";
            this.btnSaveProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveProject_ItemClick);
            // 
            // btnOpenProject
            // 
            this.btnOpenProject.Caption = "Open Project";
            this.btnOpenProject.Glyph = ((System.Drawing.Image)(resources.GetObject("btnOpenProject.Glyph")));
            this.btnOpenProject.Id = 14;
            this.btnOpenProject.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnOpenProject.LargeGlyph")));
            this.btnOpenProject.Name = "btnOpenProject";
            this.btnOpenProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOpenProject_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "SaveSeries";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Id = 25;
            this.barButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.LargeGlyph")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // bbiRun
            // 
            this.bbiRun.Caption = "Run";
            this.bbiRun.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiRun.Glyph")));
            this.bbiRun.Id = 6;
            this.bbiRun.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiRun.LargeGlyph")));
            this.bbiRun.Name = "bbiRun";
            this.bbiRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRun_ItemClick);
            // 
            // bbiStep
            // 
            this.bbiStep.Caption = "Run By Steps";
            this.bbiStep.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiStep.Glyph")));
            this.bbiStep.Id = 7;
            this.bbiStep.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiStep.LargeGlyph")));
            this.bbiStep.Name = "bbiStep";
            this.bbiStep.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiStep_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "doNsteps";
            this.barButtonItem2.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.Glyph")));
            this.barButtonItem2.Id = 26;
            this.barButtonItem2.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.LargeGlyph")));
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // bbiReset
            // 
            this.bbiReset.Caption = "Reset";
            this.bbiReset.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiReset.Glyph")));
            this.bbiReset.Id = 10;
            this.bbiReset.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiReset.LargeGlyph")));
            this.bbiReset.Name = "bbiReset";
            this.bbiReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiReset_ItemClick);
            // 
            // btnRights
            // 
            this.btnRights.Caption = "Rights";
            this.btnRights.Glyph = ((System.Drawing.Image)(resources.GetObject("btnRights.Glyph")));
            this.btnRights.Id = 17;
            this.btnRights.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnRights.LargeGlyph")));
            this.btnRights.Name = "btnRights";
            this.btnRights.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRights_ItemClick);
            // 
            // btnSwitch
            // 
            this.btnSwitch.Caption = "switch";
            this.btnSwitch.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSwitch.Glyph")));
            this.btnSwitch.Id = 23;
            this.btnSwitch.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnSwitch.LargeGlyph")));
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSwitch_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiPanels),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiExport)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bsiProject
            // 
            this.bsiProject.Caption = "Project";
            this.bsiProject.Id = 8;
            this.bsiProject.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRun),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiStep),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveProject, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOpenProject),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiReset, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.bciHoriz, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRights, true)});
            this.bsiProject.Name = "bsiProject";
            // 
            // bciHoriz
            // 
            this.bciHoriz.Caption = "Horiz";
            this.bciHoriz.Checked = true;
            this.bciHoriz.CloseSubMenuOnClick = false;
            this.bciHoriz.Id = 9;
            this.bciHoriz.Name = "bciHoriz";
            this.bciHoriz.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bciHoriz_CheckedChanged);
            // 
            // bsiPanels
            // 
            this.bsiPanels.Caption = "Panels";
            this.bsiPanels.Id = 15;
            this.bsiPanels.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDefaultLayout),
            new DevExpress.XtraBars.LinkPersistInfo(this.biPanels, true)});
            this.bsiPanels.Name = "bsiPanels";
            // 
            // bbiDefaultLayout
            // 
            this.bbiDefaultLayout.Caption = "Restore Default Layout";
            this.bbiDefaultLayout.Id = 16;
            this.bbiDefaultLayout.Name = "bbiDefaultLayout";
            this.bbiDefaultLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDefaultLayout_ItemClick);
            // 
            // biPanels
            // 
            this.biPanels.Caption = "Panels";
            this.biPanels.Id = 4;
            this.biPanels.Name = "biPanels";
            this.biPanels.ShowCustomizationItem = false;
            this.biPanels.ShowDockPanels = true;
            this.biPanels.ShowToolbars = false;
            // 
            // bsiExport
            // 
            this.bsiExport.Caption = "Export";
            this.bsiExport.Id = 20;
            this.bsiExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSimpleExport)});
            this.bsiExport.Name = "bsiExport";
            // 
            // bbiExport
            // 
            this.bbiExport.Caption = "Export";
            this.bbiExport.Id = 19;
            this.bbiExport.Name = "bbiExport";
            this.bbiExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExport_ItemClick);
            // 
            // bbiSimpleExport
            // 
            this.bbiSimpleExport.Caption = "Simple Export";
            this.bbiSimpleExport.Id = 21;
            this.bbiSimpleExport.Name = "bbiSimpleExport";
            this.bbiSimpleExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSimpleExport_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiImage),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiBright),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiX),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiY),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiTime),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiHoriz),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiRights)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // bsiImage
            // 
            this.bsiImage.Caption = "Image";
            this.bsiImage.Id = 0;
            this.bsiImage.Name = "bsiImage";
            this.bsiImage.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bsiBright
            // 
            this.bsiBright.Caption = "Brigth";
            this.bsiBright.Id = 1;
            this.bsiBright.Name = "bsiBright";
            this.bsiBright.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bsiX
            // 
            this.bsiX.Caption = "X";
            this.bsiX.Id = 2;
            this.bsiX.Name = "bsiX";
            this.bsiX.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bsiY
            // 
            this.bsiY.Caption = "Y";
            this.bsiY.Id = 3;
            this.bsiY.Name = "bsiY";
            this.bsiY.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bsiTime
            // 
            this.bsiTime.Caption = "Time";
            this.bsiTime.Id = 28;
            this.bsiTime.Name = "bsiTime";
            this.bsiTime.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bsiHoriz
            // 
            this.bsiHoriz.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bsiHoriz.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.bsiHoriz.Caption = "Horiz";
            this.bsiHoriz.Id = 11;
            this.bsiHoriz.Name = "bsiHoriz";
            this.bsiHoriz.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // bsiRights
            // 
            this.bsiRights.Caption = "Rights";
            this.bsiRights.Id = 18;
            this.bsiRights.Name = "bsiRights";
            this.bsiRights.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(710, 53);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 445);
            this.barDockControlBottom.Size = new System.Drawing.Size(710, 25);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 53);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 392);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(710, 53);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 392);
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCaptionImage = true;
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            this.dockManager1.ActivePanelChanged += new DevExpress.XtraBars.Docking.ActivePanelChangedEventHandler(this.dockManager1_ActivePanelChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 470);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "MainForm";
            this.Text = "DIPStudio3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem bsiImage;
        private DevExpress.XtraBars.BarStaticItem bsiBright;
        private DevExpress.XtraBars.BarStaticItem bsiX;
        private DevExpress.XtraBars.BarStaticItem bsiY;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.BarToolbarsListItem biPanels;
        private DevExpress.XtraBars.BarButtonItem bbiRun;
        private DevExpress.XtraBars.BarButtonItem bbiStep;
        private DevExpress.XtraBars.BarSubItem bsiProject;
        private DevExpress.XtraBars.BarCheckItem bciHoriz;
        private DevExpress.XtraBars.BarButtonItem bbiReset;
        private DevExpress.XtraBars.BarStaticItem bsiHoriz;
        private DevExpress.XtraBars.BarButtonItem btnSaveProject;
        private DevExpress.XtraBars.BarButtonItem btnOpenProject;
        private DevExpress.XtraBars.BarSubItem bsiPanels;
        private DevExpress.XtraBars.BarButtonItem bbiDefaultLayout;
        private DevExpress.XtraBars.BarButtonItem btnRights;
        private DevExpress.XtraBars.BarStaticItem bsiRights;
        private DevExpress.XtraBars.BarButtonItem bbiExport;
        private DevExpress.XtraBars.BarSubItem bsiExport;
        private DevExpress.XtraBars.BarButtonItem bbiSimpleExport;
        private DevExpress.XtraBars.BarButtonItem btnSwitch;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarStaticItem bsiTime;

    }
}

