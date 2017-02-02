using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using System.ComponentModel;

namespace CorrelationAnalysis {
    public class CorrelationAnalysisSettings : ValidatedPluginSettings {
        public CorrelationAnalysisSettings(IPlugin plugin)
            : base(plugin) {
            StrobX = 38;
            StrobY = 24;
            NcsX = 350;
            NcsY = 266;
            MethodBin = false;
            MethodPer = false;
            N = 1;
            Constt = 0;
            PrKadr = 25;
            PgrPO = 40.0;
            Uroven = 0.8;
            OBper = 0.7;
        }
        protected override bool IsResultSeries {
            get { return true; }
        }

        [DisplayName("Исходный видеоряд")]
        public string InputSeries { get; set; }

        [DisplayName("X")]
        public int StrobX { get; set; }
        [DisplayName("Y")]
        public int StrobY { get; set; }

        [DisplayName("X")]
        public int NcsX { get; set; }
        [DisplayName("Y")]
        public int NcsY { get; set; }

        public bool MethodPer { get; set; }
        public bool MethodBin { get; set; }

        [DisplayName("Размеры ОП")]
        public int N { get; set; }

        [DisplayName("MO-3SKO-Const")]
        public int Constt { get; set; }

        [DisplayName("Перерасчет ЭО по кадрам")]
        public int PrKadr { get; set; }

        [DisplayName("Критериии выбора ПО")]
        public double PgrPO { get; set; }

        [DisplayName("Отсечение по уровню")]
        public double Uroven { get; set; }

        [DisplayName("Перерасчет ЭО по нарастанию ОО")]
        public double OBper { get; set; }

        [SavePropertyAttribute(false)]
        public double ACF { get; set; }

        [SavePropertyAttribute(false)]
        public byte[,] A { get; set; }
    }
}
