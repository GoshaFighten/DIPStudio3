using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using System.ComponentModel;
namespace BackgroundAnalyze {
    public class BackgroundAnalyzeSettings : ValidatedPluginSettings {
        public BackgroundAnalyzeSettings(IPlugin plugin)
            : base(plugin) {

        }

        private string fInputSeries = string.Empty;
        [DisplayName("Исходный видеоряд")]
        public string InputSeries {
            get { return fInputSeries; }
            set { fInputSeries = value; }
        }

        protected override bool IsResultSeries {
            get { return false; }
        }

        private int fWindowWidth = 40;
        [DisplayName("Ширина окна")]
        public int WindowWidth {
            get { return fWindowWidth; }
            set { fWindowWidth = value; }
        }

        private int fWindowHeight = 40;
        [DisplayName("Высота окна")]
        public int WindowHeight {
            get { return fWindowHeight; }
            set { fWindowHeight = value; }
        }

        private int fStepX = 50;
        [DisplayName("Смещение окна по X")]
        public int StepX {
            get { return fStepX; }
            set { fStepX = value; }
        }

        private int fStepY = 50;
        [DisplayName("Смещение окна по Y")]
        public int StepY {
            get { return fStepY; }
            set { fStepY = value; }
        }

        private double fS = 0.5;
        [DisplayName("Порог")]
        public double S {
            get { return fS; }
            set { fS = value; }
        }

        [DisplayName("X строба")]
        public int StrobX { get; set; }
        [DisplayName("Y строба")]
        public int StrobY { get; set; }
        [DisplayName("Ширина строба")]
        public int StrobWidth { get; set; }
        [DisplayName("Высота строба")]
        public int StrobHeight { get; set; }
        [DisplayName("Сканирование")]
        public bool Scan { get; set; }
        [DisplayName("Три окна")]
        public bool Three { get; set; }
    }
}
