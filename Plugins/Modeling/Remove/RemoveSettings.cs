using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using System.ComponentModel;

namespace Remove
{
    public class RemoveSettings : ValidatedPluginSettings
    {
        public RemoveSettings(IPlugin plugin)
            :base(plugin)
    {
    }
        private string fInputSeries = string.Empty;
        [DisplayName("Удалить видеоряд")]
        public string InputSeries
        {
            get { return fInputSeries; }
            set { fInputSeries = value; }
        }

        private string fInputTable = string.Empty;
        [DisplayName("Удалить данные")]
        public string InputTable
        {
            get { return fInputTable; }
            set { fInputTable = value; }
        }
        protected override bool IsResultSeries
        {
            get { return false; }
        }
    }
}
