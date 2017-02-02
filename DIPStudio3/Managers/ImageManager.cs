using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars.Docking;
using DIPStudioCore;
using DIPStudio3.Controls;
using System.Drawing;

namespace DIPStudio3 {
    public class ImageManager : IManager {
        public ImageControl ImgControl;

        DIPApplication application;

        private ImageViewer.ImgViewer fViewer;

        public ImageViewer.ImgViewer Viewer
        {
            get { return fViewer; }
            set { fViewer = value; }
        }

        public ImageManager(DIPApplication application) {
            this.application = application;
        }

        public DockPanel AddPanel(DockManager dockManager) {
            DockPanel panel = dockManager.AddPanel(DockingStyle.Left);
            panel.ID = PanelID;
            panel.Text = "Изображения";
            panel.Image = DevExpress.Images.ImageResourceCache.Default.GetImage("images/content/image_16x16.png");
            ImgControl = CreateControl(panel);
            return panel;
        }

        private ImageControl CreateControl(DockPanel panel)
        {
            ImageControl control = new ImageControl();
            control.Dock = System.Windows.Forms.DockStyle.Fill;
            control.FrameChosen += new EventHandler<FrameChosenEventArgs>(control_FrameChosen);
            panel.ControlContainer.Controls.Add(control);
            return control;
        }

        void control_FrameChosen(object sender, FrameChosenEventArgs e) {
            Viewer.Frame = e.Frame;
        }


        public void Load() {
            ImgControl.Series = application.Series;
        }

        public bool HasPanel() {
            return true;
        }

        public void SetFocusFrame(Frame frame) {
            ImgControl.FocusFrame(frame);
        }

        public Guid PanelID
        {
            get { return Guid.Parse("76519fd0-a269-46dc-a13c-c2d048586402"); }
        }
        public event EventHandler Changed;
    }
}
