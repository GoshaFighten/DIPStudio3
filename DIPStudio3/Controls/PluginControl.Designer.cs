namespace DIPStudio3.Controls {
    partial class PluginControl {
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
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgModeling = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbgProcessing = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbgSelection = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbgView = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbgModeling;
            this.navBarControl1.Appearance.NavigationPaneHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.navBarControl1.Appearance.NavigationPaneHeader.Options.UseFont = true;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgModeling,
            this.nbgProcessing,
            this.nbgSelection,
            this.nbgView});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 492;
            this.navBarControl1.OptionsNavPane.ShowGroupImageInHeader = true;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(492, 482);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarControl1_LinkClicked);
            // 
            // nbgModeling
            // 
            this.nbgModeling.Caption = "Modeling";
            this.nbgModeling.Expanded = true;
            this.nbgModeling.Name = "nbgModeling";
            // 
            // nbgProcessing
            // 
            this.nbgProcessing.Caption = "Processing";
            this.nbgProcessing.Name = "nbgProcessing";
            // 
            // nbgSelection
            // 
            this.nbgSelection.Caption = "Selection";
            this.nbgSelection.Name = "nbgSelection";
            // 
            // nbgView
            // 
            this.nbgView.Caption = "View";
            this.nbgView.Name = "nbgView";
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navBarControl1);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(492, 482);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbgModeling;
        private DevExpress.XtraNavBar.NavBarGroup nbgProcessing;
        private DevExpress.XtraNavBar.NavBarGroup nbgSelection;
        private DevExpress.XtraNavBar.NavBarGroup nbgView;
    }
}
