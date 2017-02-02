using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using DIPStudioCore;
using System.ComponentModel;
using DIPStudioUICore;

namespace RGB2Gray {
    public class RGB2GraySettings : ValidatedPluginSettings {
        public RGB2GraySettings(IPlugin plugin)
            : base(plugin) {
        }

        string fInputSeries = string.Empty;
        [DisplayName("Исходный видеоряд")]
        public string InputSeries {
            get { return fInputSeries; }
            set { fInputSeries = value; }
        }
        [DisplayName("Красный компонент(R)")]
        public double Red
        { get; set; }
        [DisplayName("Зеленый компонент(G)")]
        public double Green 
        { get; set; }
        [DisplayName("Синий компонент(B)")]
        public double Blue
        { get; set; }
        // Определяет, если плагин создает только видеокадры (true - если да, false - если только данные)
        // Если возвращается несколько видеорядов или таблиц данных, необходимо переписать RegisterOutputSeries и/или RegisterOutputTables методы
        // чтобы зарегестрировать имена выходных видеокадров или таблиц данных
        protected override bool IsResultSeries {
            get { return true; }
        }
    }
}
