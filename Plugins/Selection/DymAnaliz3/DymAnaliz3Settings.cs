using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;
using System.ComponentModel;

namespace DymAnaliz3
{
    public class DymAnaliz3Settings : ValidatedPluginSettings
    {

        public DymAnaliz3Settings(IPlugin plugin)
            : base(plugin)
        {

        }

        private string fInputSeries = string.Empty;
        [DisplayName("Исходный видеоряд")]
        public string InputSeries
        {
            get { return fInputSeries; }
            set { fInputSeries = value; }
        }
        [DisplayName("добавить видеоряд")]
        public bool newSeries { get; set; }
        [DisplayName("Имя выходных данных")]
        public string ResultData { get; set; }
        [DisplayName("Размер по Х")]
        public int StrobX { get; set; }
        [DisplayName("Размер по Y")]
        public int StrobY { get; set; }

        [DisplayName("Координата Х")]
        public int NcsX { get; set; }
        [DisplayName("Координата Y")]
        public int NcsY { get; set; }

        [DisplayName("Использовать засветочный строб для корректировки изменения освещенности")]
        public bool IsZasUse { get; set; }
        [DisplayName("Размер по X")]
        public int StrobXzas { get; set; }
        [DisplayName("Размер по Y")]
        public int StrobYzas { get; set; }

        [DisplayName("Координата X")]
        public int NcsXzas { get; set; }
        [DisplayName("Координата Y")]
        public int NcsYzas { get; set; }

        protected override bool IsResultSeries
        {
            get { return newSeries; }
        }
    }
}
