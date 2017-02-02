using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.ComponentModel;
using DIPStudioUICore;

namespace BrightProcessing 
{
    public class BrightProcessingSettings : ValidatedPluginSettings 
    {
        public BrightProcessingSettings(IPlugin plugin)
            : base(plugin) {
        }

        string fInputSeries = string.Empty;

        [DisplayName("Исходный видеоряд")]
        public string InputSeries {
            get { return fInputSeries; }
            set { fInputSeries = value; }
        }
        [DisplayName("X1")]
        public int X1 { get; set;}
        [DisplayName("Y1")]
        public int Y1 { get; set; }
        [DisplayName("X2")]
        public int X2 { get; set; }
        [DisplayName("Y2")]
        public int Y2 { get; set; }

        // Определяет, если плагин создает только видеокадры (true - если да, false - если только данные)
        // Если возвращается несколько видеорядов или таблиц данных, необходимо переписать RegisterOutputSeries и/или RegisterOutputTables методы
        // чтобы зарегестрировать имена выходных видеокадров или таблиц данных
        protected override bool IsResultSeries {
            get { return true; }
        }
    }
}
