using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using System.ComponentModel;

namespace MethodTraces {
    public class MethodTracesSettings : ValidatedPluginSettings {
        public MethodTracesSettings(MethodTraces plugin)
            : base(plugin) {

        }
        protected override bool IsResultSeries {
            get { return false; }
        }

        private string fInputTable = String.Empty;
        [DisplayName("Исходные данные")]
        public string InputTable {
            get { return fInputTable; }
            set { fInputTable = value; }
        }
    }
}
