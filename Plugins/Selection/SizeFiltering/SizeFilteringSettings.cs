using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using System.ComponentModel;
using DIPStudioCore;

namespace SizeFiltering {
    public class SizeFilteringSettings : ValidatedPluginSettings {
        public SizeFilteringSettings(SizeFiltering plugin)
            : base(plugin) {

        }
        private string fInputTable = string.Empty;
        [DisplayName("Исходные данные")]
        public string InputTable {
            get { return fInputTable; }
            set { fInputTable = value; }
        }
        private ValueObject fMinSize = new ValueObject();
        [DisplayName("Минимальный размер")]
        public ValueObject MinSize {
            get {
                return fMinSize;
            }
            set {
                fMinSize = value;
            }
        }
        private ValueObject fMaxSize = new ValueObject();
        [DisplayName("Максимальный размер")]
        public ValueObject MaxSize {
            get {
                return fMaxSize;
            }
            set {
                fMaxSize = value;
            }
        }

        protected override bool IsResultSeries {
            get { return false; }
        }
    }
}
