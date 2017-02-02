using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars.Docking;

namespace DIPStudio3 {
    public interface IManager {
        bool HasPanel();

        DockPanel AddPanel(DockManager dockManager);

        void Load();

        Guid PanelID { get; }

        event EventHandler Changed;
    }
}
