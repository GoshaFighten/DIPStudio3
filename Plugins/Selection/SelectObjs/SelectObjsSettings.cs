using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using System.ComponentModel;

namespace SelectObjs {
    public class SelectObjsSettings : ValidatedPluginSettings {
        private string fInputSeries = string.Empty;
        private ValueObject fThreshold = new ValueObject();

        public SelectObjsSettings(IPlugin plugin)
            : base(plugin) {
                fThreshold.Constant = 150;
        }

        [DisplayName("Входной видеоряд")]
        public string InputSeries {
            get { return fInputSeries; }
            set { fInputSeries = value; }
        }

        [DisplayName("Порог выделения")]
        public ValueObject Threshold
        {
            get { return fThreshold; }
            set { fThreshold = value; }
        }

        private bool fAddSeries;
        [DisplayName("Добавить видеоряд")]
        public bool AddSeries
        {
            get { return fAddSeries; }
            set { fAddSeries = value; }
        }

        protected override void GetPropertyError(string propertyName, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo info) {
            base.GetPropertyError(propertyName, info);
            if(propertyName == "InputSeries") {
                if(DIPApplication.GetInstance().GetSeriesByName(InputSeries) == null) {
                    info.ErrorText = "Input series does not exist!";
                }
            }
        }

        protected override bool IsResultSeries {
            get { return false; }
        }
    }
}
