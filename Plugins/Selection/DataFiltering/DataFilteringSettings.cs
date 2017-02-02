using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using System.ComponentModel;
using DIPStudioCore;
using System.Collections;

namespace DataFiltering
{
    public class DataFilteringSettings : ValidatedPluginSettings {
        public DataFilteringSettings(DataFiltering plugin)
            : base(plugin) {

        }

        private string fInputTable = string.Empty;
        [DisplayName("Исходные данные")]
        public string InputTable {
            get { return fInputTable; }
            set { fInputTable = value; }
        }

        BindingList<FilteredField> fFilteredFields = new BindingList<FilteredField>();

        public BindingList<FilteredField> FilteredFields
        {
            get { return fFilteredFields; }
        }
        
        protected override bool IsResultSeries {
            get { return false; }
        }
    }
}
