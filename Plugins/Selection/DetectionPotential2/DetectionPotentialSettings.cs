using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using System.ComponentModel;

namespace DetectionPotential2
{
    public class DetectionPotentialSettings : ValidatedPluginSettings
    {
        public DetectionPotentialSettings(IPlugin plugin)
            : base(plugin)
        {
            Tresholder = 150;
            K = 3;
        }

        protected override bool IsResultSeries
        {
            get { return false; }
        }

        [DisplayName("Исходный видеоряд")]
        public string InputSeries { get; set; }

        [DisplayName("Исходные объекты")]
        public string InputTable { get; set; }

        [DisplayName("Порог")]
        public int Tresholder { get; set; }

        [DisplayName("Коэффициент площади объекта")]
        public int K { get; set; }
    }
}
