using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using System.ComponentModel;
using System.IO;

namespace LoadData {
    public class LoadDataSettings : ValidatedPluginSettings {
        public LoadDataSettings(IPlugin plugin)
            : base(plugin) {
        }

        // Fields...
        private string fFolderName = string.Empty;

        [DisplayName("Имя файла")]
        public string FileName {
            get { return fFolderName; }
            set { fFolderName = value; }
        }

        protected override bool IsResultSeries {
            get { return false; }
        }

        protected override void GetPropertyError(string propertyName, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo info) {
            base.GetPropertyError(propertyName, info);
            if(propertyName == "FileName") {
                if(!File.Exists(FileName)) {
                    info.ErrorText = string.Format("{0} file does not exist!", FileName);
                }
            }
        }
    }
}
