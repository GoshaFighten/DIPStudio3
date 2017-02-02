using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using DIPStudioUICore;

namespace SelectionObjects {
    public class SelectionObjs : Plugin<SelectionObjsSettings, SelectionObjsUserControl> {
        public SelectionObjs()
            : base(ProjectKind.Selection, "Селекция объектов", "[СО]")
        {

        }

        protected override void Run(int t, int index, int tFinish)
        {
            new Logic(PluginSettings, t, index).Run();
        }

        protected override void CustomizeForm(BasePluginForm frm) {
            base.CustomizeForm(frm);
            frm.Width = 550;
            frm.Height = 500;
        }

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }
}
