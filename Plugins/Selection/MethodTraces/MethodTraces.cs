using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;

namespace MethodTraces {
    public class MethodTraces : Plugin<MethodTracesSettings, MethodTracesUserControl> {
        public MethodTraces()
            : base(DIPStudioCore.ProjectKind.Selection, "Метод трасс", "[МТ]")
        {

        }
        protected override void Run(int t, int index, int tFinish)
        {
            GetLogic().Run(t, index, PluginSettings);
        }
        MethodTracesLogic logic;
        MethodTracesLogic GetLogic() {
            if (logic == null)
                logic = new MethodTracesLogic();
            return logic;
        }

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }
}
