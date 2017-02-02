using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using DevExpress.XtraEditors.DXErrorProvider;

namespace DIPStudioUICore {
    public abstract class ValidatedPluginSettings : PluginSettings, IDXDataErrorInfo {
        public ValidatedPluginSettings(IPlugin plugin)
            : base(plugin) {
        }

        void IDXDataErrorInfo.GetError(ErrorInfo info) {
            //throw new NotImplementedException();
        }

        protected virtual void GetPropertyError(string propertyName, ErrorInfo info) {
            if(propertyName != "ResultName") {
                return;
            }
            if(string.IsNullOrWhiteSpace(ResultName)) {
                info.ErrorText = "Result name is empty";
            }
            if(IsResultSeries && !DIPApplicationBase.GetInstance().CheckSeriesName(this)) {
                info.ErrorText = string.Format("Series with the {0} name already exists!", ResultName);
            }
            if(!IsResultSeries && !DIPApplicationBase.GetInstance().CheckTableName(this)) {
                info.ErrorText = string.Format("Table with the {0} name already exists!", ResultName);
            }
        }

        void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info) {
            GetPropertyError(propertyName, info);
        }
    }
}
