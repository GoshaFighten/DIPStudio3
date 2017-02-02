using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;

namespace DIPStudio3 {
    public class RightsManager : IManager {
        private RightsForm fFrm;
        private DIPApplication application;
        public RightsManager(DIPApplication application) {
            this.application = application;
        }

        public bool HasPanel() {
            return false;
        }

        public DevExpress.XtraBars.Docking.DockPanel AddPanel(DevExpress.XtraBars.Docking.DockManager dockManager) {
            throw new NotImplementedException();
        }

        public void Load() {
            application.SetRights(Rights.Admin);//senichkin
            fFrm = new RightsForm(this);
        }

        public void ChangeRights() {
            if (fFrm.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                application.SetRights(fFrm.Rights);
            }
        }

        public Guid PanelID
        {
            get { throw new NotImplementedException(); }
        }
        public event EventHandler Changed;
    }
}
