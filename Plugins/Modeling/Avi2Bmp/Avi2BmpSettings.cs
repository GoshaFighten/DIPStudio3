using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;
using System.ComponentModel;

namespace Avi2Bmp {
    public class Avi2BmpSettings : ValidatedPluginSettings {

        public Avi2BmpSettings(IPlugin plugin)
            : base(plugin) {
        }
        private int fStartTime;

        [DisplayName("Время начала")]
        public int StartTime
        {
            get { return fStartTime; }
            set { fStartTime = value; }
        }
        private int fFinishTime ;

        [DisplayName("Время конца")]
        public int FinishTime
        {
            get { return fFinishTime; }
            set { fFinishTime = value; }
        }

        private string fInputSeries = string.Empty;

        [DisplayName("Исходный видеоряд")]
        public string InputSeries {
            get { return fInputSeries; }
            set { fInputSeries = value; }
        }
        private string fSavePath = string.Empty;

        [DisplayName("Папка сохранения")]
        public string SavePath
        {
            get { return fSavePath; }
            set { fSavePath = value; }
        }
        protected override bool IsResultSeries {
            get { return true; }
        }
    }
}
