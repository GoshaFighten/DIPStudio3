namespace ImageViewer
{
    partial class ImgViewer
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
            DisposeControl();

            if(disposing) {
                if(components != null) {
                    components.Dispose();
                }
                if(preview != null) {
                    preview.Dispose();
                    preview = null;
                }
                if(grabCursor != null) {
                    grabCursor.Dispose();
                    grabCursor = null;
                }
                if(dragCursor != null) {
                    dragCursor.Dispose();
                    dragCursor = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImgViewer));
            this.panelMenu = new DevExpress.XtraEditors.PanelControl();
            this.btnInv = new DevExpress.XtraEditors.SimpleButton();
            this.btnHist = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnMode = new DevExpress.XtraEditors.SimpleButton();
            this.cbZoom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnFitToScreen = new DevExpress.XtraEditors.SimpleButton();
            this.btnZoomIn = new DevExpress.XtraEditors.SimpleButton();
            this.btnZoomOut = new DevExpress.XtraEditors.SimpleButton();
            this.btnRotate270 = new DevExpress.XtraEditors.SimpleButton();
            this.btnRotate90 = new DevExpress.XtraEditors.SimpleButton();
            this.pbPanel = new DevExpress.XtraEditors.PictureEdit();
            this.pbFull = new ImageViewer.PanelDoubleBuffered();
            this.sbVert = new DevExpress.XtraEditors.VScrollBar();
            this.sbHoriz = new DevExpress.XtraEditors.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.panelMenu)).BeginInit();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbZoom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPanel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFull)).BeginInit();
            this.pbFull.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMenu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelMenu.Controls.Add(this.btnInv);
            this.panelMenu.Controls.Add(this.btnHist);
            this.panelMenu.Controls.Add(this.btnNext);
            this.panelMenu.Controls.Add(this.btnPrev);
            this.panelMenu.Controls.Add(this.btnMode);
            this.panelMenu.Controls.Add(this.cbZoom);
            this.panelMenu.Controls.Add(this.btnFitToScreen);
            this.panelMenu.Controls.Add(this.btnZoomIn);
            this.panelMenu.Controls.Add(this.btnZoomOut);
            this.panelMenu.Controls.Add(this.btnRotate270);
            this.panelMenu.Controls.Add(this.btnRotate90);
            this.panelMenu.Location = new System.Drawing.Point(471, 124);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(150, 78);
            this.panelMenu.TabIndex = 11;
            // 
            // btnInv
            // 
            this.btnInv.Image = ((System.Drawing.Image)(resources.GetObject("btnInv.Image")));
            this.btnInv.Location = new System.Drawing.Point(114, 29);
            this.btnInv.Name = "btnInv";
            this.btnInv.Size = new System.Drawing.Size(25, 25);
            this.btnInv.TabIndex = 20;
            this.btnInv.Click += new System.EventHandler(this.btnInv_Click);
            // 
            // btnHist
            // 
            this.btnHist.Image = ((System.Drawing.Image)(resources.GetObject("btnHist.Image")));
            this.btnHist.Location = new System.Drawing.Point(86, 29);
            this.btnHist.Name = "btnHist";
            this.btnHist.Size = new System.Drawing.Size(25, 25);
            this.btnHist.TabIndex = 19;
            this.btnHist.Click += new System.EventHandler(this.btnHist_Click);
            // 
            // btnNext
            // 
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(58, 29);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(25, 25);
            this.btnNext.TabIndex = 18;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.Image")));
            this.btnPrev.Location = new System.Drawing.Point(30, 29);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(25, 25);
            this.btnPrev.TabIndex = 17;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnMode
            // 
            this.btnMode.Image = global::ImageViewer.Properties.Resources.btnSelect;
            this.btnMode.Location = new System.Drawing.Point(2, 29);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(25, 25);
            this.btnMode.TabIndex = 16;
            this.btnMode.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // cbZoom
            // 
            this.cbZoom.Location = new System.Drawing.Point(2, 57);
            this.cbZoom.Name = "cbZoom";
            this.cbZoom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbZoom.Size = new System.Drawing.Size(62, 20);
            this.cbZoom.TabIndex = 14;
            this.cbZoom.SelectedIndexChanged += new System.EventHandler(this.cbZoom_SelectedIndexChanged);
            this.cbZoom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbZoom_KeyPress);
            // 
            // btnFitToScreen
            // 
            this.btnFitToScreen.Image = global::ImageViewer.Properties.Resources.btnFitToScreen;
            this.btnFitToScreen.Location = new System.Drawing.Point(58, 1);
            this.btnFitToScreen.Name = "btnFitToScreen";
            this.btnFitToScreen.Size = new System.Drawing.Size(25, 25);
            this.btnFitToScreen.TabIndex = 13;
            this.btnFitToScreen.Click += new System.EventHandler(this.btnFitToScreen_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Image = global::ImageViewer.Properties.Resources.btnZoomIn;
            this.btnZoomIn.Location = new System.Drawing.Point(2, 1);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(25, 25);
            this.btnZoomIn.TabIndex = 12;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Image = global::ImageViewer.Properties.Resources.btnZoomOut;
            this.btnZoomOut.Location = new System.Drawing.Point(30, 1);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(25, 25);
            this.btnZoomOut.TabIndex = 11;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnRotate270
            // 
            this.btnRotate270.Image = global::ImageViewer.Properties.Resources.btnRotate270;
            this.btnRotate270.Location = new System.Drawing.Point(86, 1);
            this.btnRotate270.Name = "btnRotate270";
            this.btnRotate270.Size = new System.Drawing.Size(25, 25);
            this.btnRotate270.TabIndex = 10;
            this.btnRotate270.Click += new System.EventHandler(this.btnRotate270_Click);
            // 
            // btnRotate90
            // 
            this.btnRotate90.Image = global::ImageViewer.Properties.Resources.btnRotate90;
            this.btnRotate90.Location = new System.Drawing.Point(114, 1);
            this.btnRotate90.Name = "btnRotate90";
            this.btnRotate90.Size = new System.Drawing.Size(25, 25);
            this.btnRotate90.TabIndex = 9;
            this.btnRotate90.Click += new System.EventHandler(this.btnRotate90_Click);
            // 
            // pbPanel
            // 
            this.pbPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPanel.Location = new System.Drawing.Point(471, 3);
            this.pbPanel.Name = "pbPanel";
            this.pbPanel.Properties.AllowFocused = false;
            this.pbPanel.Properties.ShowMenu = false;
            this.pbPanel.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pbPanel.Size = new System.Drawing.Size(148, 117);
            this.pbPanel.TabIndex = 10;
            this.pbPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbPanel_MouseDown);
            this.pbPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbPanel_MouseMove);
            this.pbPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbPanel_MouseUp);
            // 
            // pbFull
            // 
            this.pbFull.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFull.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pbFull.Controls.Add(this.sbVert);
            this.pbFull.Controls.Add(this.sbHoriz);
            this.pbFull.Location = new System.Drawing.Point(2, 3);
            this.pbFull.Name = "pbFull";
            this.pbFull.Size = new System.Drawing.Size(464, 304);
            this.pbFull.TabIndex = 13;
            this.pbFull.Click += new System.EventHandler(this.pbFull_Click);
            this.pbFull.DragDrop += new System.Windows.Forms.DragEventHandler(this.pbFull_DragDrop);
            this.pbFull.DragEnter += new System.Windows.Forms.DragEventHandler(this.pbFull_DragEnter);
            this.pbFull.Paint += new System.Windows.Forms.PaintEventHandler(this.pbFull_Paint);
            this.pbFull.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbFull_MouseDoubleClick);
            this.pbFull.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbFull_MouseDown);
            this.pbFull.MouseEnter += new System.EventHandler(this.pbFull_MouseEnter);
            this.pbFull.MouseLeave += new System.EventHandler(this.pbFull_MouseLeave);
            this.pbFull.MouseHover += new System.EventHandler(this.pbFull_MouseHover);
            this.pbFull.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbFull_MouseMove);
            this.pbFull.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbFull_MouseUp);
            // 
            // sbVert
            // 
            this.sbVert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sbVert.Location = new System.Drawing.Point(445, 0);
            this.sbVert.Name = "sbVert";
            this.sbVert.Size = new System.Drawing.Size(17, 286);
            this.sbVert.TabIndex = 0;
            this.sbVert.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbVert_Scroll);
            // 
            // sbHoriz
            // 
            this.sbHoriz.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sbHoriz.Location = new System.Drawing.Point(0, 285);
            this.sbHoriz.Name = "sbHoriz";
            this.sbHoriz.Size = new System.Drawing.Size(445, 17);
            this.sbHoriz.TabIndex = 1;
            this.sbHoriz.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbHoriz_Scroll);
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbFull);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.pbPanel);
            this.MinimumSize = new System.Drawing.Size(454, 157);
            this.Name = "Viewer";
            this.Size = new System.Drawing.Size(623, 310);
            this.Load += new System.EventHandler(this.ImageViewer_Load);
            this.Click += new System.EventHandler(this.ImageViewer_Click);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ImageViewer_MouseWheel);
            this.Resize += new System.EventHandler(this.ImageViewer_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.panelMenu)).EndInit();
            this.panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbZoom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPanel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFull)).EndInit();
            this.pbFull.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelMenu;
        private DevExpress.XtraEditors.PictureEdit pbPanel;
        private PanelDoubleBuffered pbFull;
        private DevExpress.XtraEditors.SimpleButton btnRotate270;
        private DevExpress.XtraEditors.SimpleButton btnRotate90;
        private DevExpress.XtraEditors.SimpleButton btnZoomIn;
        private DevExpress.XtraEditors.SimpleButton btnZoomOut;
        private DevExpress.XtraEditors.SimpleButton btnFitToScreen;
        private DevExpress.XtraEditors.ComboBoxEdit cbZoom;
        private DevExpress.XtraEditors.SimpleButton btnMode;
        private DevExpress.XtraEditors.HScrollBar sbHoriz;
        private DevExpress.XtraEditors.VScrollBar sbVert;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnHist;
        private DevExpress.XtraEditors.SimpleButton btnInv;
    }
}
