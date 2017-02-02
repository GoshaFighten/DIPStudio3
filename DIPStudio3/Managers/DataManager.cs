using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphViewer;
using DevExpress.XtraBars.Docking;
using DIPStudioCore;
using DIPStudio3.Controls;

namespace DIPStudio3 {
    public class DataManager : IManager {
        DIPApplication application;

        public DataManager(DIPApplication application) {
            this.application = application;
        }


        public bool HasPanel() {
            return true;
        }

        public void Load() {
            control.Tables = application.Tables;
        }

        DataControl control;
 
        public DockPanel AddPanel(DockManager dockManager) {
            DockPanel panel = dockManager.AddPanel(DockingStyle.Left);
            panel.ID = PanelID;
            panel.Text = "Данные";
            panel.Image = DevExpress.Images.ImageResourceCache.Default.GetImage("images/grid/grid_16x16.png");
            control = CreateControl(panel);
            return panel;
        }

        private DataControl CreateControl(DockPanel panel) {
            DataControl control = new DataControl();
            control.Dock = System.Windows.Forms.DockStyle.Fill;
            control.CanExportChanged += new EventHandler(control_CanExportChanged);
            panel.ControlContainer.Controls.Add(control);
            return control;
        }

        void control_CanExportChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        public event EventHandler Changed;
        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        public Guid PanelID
        {
            get { return Guid.Parse("33baa1d2-9c21-4136-8d2a-982395515f7b"); }
        }
        public void ExportCurrentData()
        {
            control.ExportCurrentTab();
        }
        public bool CanExport()
        {
            return control.CanExport();
        }

        public void SimpleExportCurrentData() {
            control.SimpleExportCurrentTab();
        }
    }
}
