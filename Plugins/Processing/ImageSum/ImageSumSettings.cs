using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using System.ComponentModel;

namespace ImageSum {
    public class ImageSumSettings : ValidatedPluginSettings {
        public ImageSumSettings(IPlugin plugin)
            : base(plugin) {
        }

        protected override bool IsResultSeries {
            get { return true; }
        }

        private string fInputSeries2;

        private string fInputSeries1;

        [DisplayName("Исходный видеоряд 1")]
        public string InputSeries1 {
            get { return fInputSeries1; }
            set { fInputSeries1 = value; }
        }

        [DisplayName("Исходный видеоряд 2")]
        public string InputSeries2 {
            get { return fInputSeries2; }
            set { fInputSeries2 = value; }
        }

        protected override void GetPropertyError(string propertyName, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo info) {
            base.GetPropertyError(propertyName, info);
            string inputSeries = String.Empty;
            switch(propertyName) {
                case "InputSeries1":
                    inputSeries = InputSeries1;
                    break;
                case "InputSeries2":
                    inputSeries = InputSeries2;
                    break;
                default:
                    return;
            }
            if (DIPApplicationBase.GetInstance().GetSeriesByName(Plugin.ShortName + inputSeries) == null) {
                info.ErrorText = string.Format("Input series \"{0}\" does not exist", inputSeries);
            }
        }
    }
}
