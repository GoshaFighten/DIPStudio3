using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.IO;
using DIPStudioUICore;
using System.ComponentModel;

namespace LoadImages {
    public class LoadImageSettings : ValidatedPluginSettings {
        public LoadImageSettings(IPlugin plugin)
            : base(plugin) {
        }

        private bool fConvert = true;

        private int fD;

        private int fStartTime;

        private string fFolderName = string.Empty;

        [DisplayName("Папка с изображениями")]
        public string FolderName {
            get { return fFolderName; }
            set { fFolderName = value; }
        }

        [DisplayName("Время начала, мс")]
        public int StartTime {
            get { return fStartTime; }
            set { fStartTime = value; }
        }

        [DisplayName("Дискретизация, мс")]
        public int D {
            get { return fD; }
            set { fD = value; }
        }

        [DisplayName("Преобразовать в 8-бит формат")]
        public bool Convert {
            get { return fConvert; }
            set { fConvert = value; }
        }

        protected override void GetPropertyError(string propertyName, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo info) {
            base.GetPropertyError(propertyName, info);
            if(propertyName == "FolderName") {
                if(!Directory.Exists(FolderName)) {
                    info.ErrorText = string.Format("{0} folder does not exist!", FolderName);
                }
                return;
            }
            if(propertyName == "D") {
                if(D <= 0) {
                    info.ErrorText = "Значение дискретизации должно быть больше нуля!";
                }
                return;
            }
        }

        protected override bool IsResultSeries {
            get { return true; }
        }
    }
}
