using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;
using System.ComponentModel;

namespace BinImages {
    public class BinImagesSettings : ValidatedPluginSettings {
        private string fInputSeries = string.Empty;
        private bool fMode = false;
        private string fDataField = string.Empty;


        public BinImagesSettings(IPlugin plugin)
            : base(plugin) {
        }

        [DisplayName("�������� ��������")]
        public string InputSeries {
            get { return fInputSeries; }
            set { fInputSeries = value; }
        }
        private ValueObject fThreshold = new ValueObject();
        [DisplayName("����� �����������")]
        public ValueObject Threshold
        {
            get { return fThreshold; }
            set { fThreshold = value; }
        }
        [DisplayName("�������� ���")]
        public bool Mode {
            get { return fMode; }
            set { fMode = value; }
        }

        protected override bool IsResultSeries {
            get { return true; }
        }
    }
}
