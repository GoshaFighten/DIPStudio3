﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.ComponentModel;
using DIPStudioUICore;

namespace InvImages {
    public class InvImagesSettings : ValidatedPluginSettings {
        public InvImagesSettings(IPlugin plugin)
            : base(plugin) {
        }

        string fInputSeries = string.Empty;

        [DisplayName("Исходный видеоряд")]
        public string InputSeries {
            get { return fInputSeries; }
            set { fInputSeries = value; }
        }

        // Определяет, если плагин создает только видеокадры (true - если да, false - если только данные)
        // Если возвращается несколько видеорядов или таблиц данных, необходимо переписать RegisterOutputSeries и/или RegisterOutputTables методы
        // чтобы зарегестрировать имена выходных видеокадров или таблиц данных
        protected override bool IsResultSeries {
            get { return true; }
        }
    }
}
