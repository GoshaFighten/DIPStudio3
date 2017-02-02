using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using System.ComponentModel;

namespace ShowObjects {
    public class ShowObjectsSettings : ValidatedPluginSettings {
        public ShowObjectsSettings(ShowObjects plugin)
            : base(plugin) {

        }
        protected override bool IsResultSeries {
            get { return true; }
        }
        private string fInputImages = String.Empty;
        [DisplayName("Исходные изображения")]
        public string InputImages {
            get { return fInputImages; }
            set { fInputImages = value; }
        }
        private string fInputTable = String.Empty;
        [DisplayName("Исходные данные")]
        public string InputTable {
            get { return fInputTable; }
            set { fInputTable = value; }
        }
    }
}
