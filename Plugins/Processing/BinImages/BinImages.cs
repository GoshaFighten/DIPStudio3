using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;
using DIPStudioCore.Data;

namespace BinImages {
    public class BinImages : Plugin<BinImagesSettings, BinImagesUserControl> {
        public BinImages()
            : base(ProjectKind.Processing, "Бинаризация изображений", "[БИ]")
        {
        }
        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 550;
            frm.Height = 700;
        }
        protected override void Run(int t, int index, int tFinish)
        {
            Logic(t);
        }
        private void Logic(int t) {
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            Series inputSeries = application.GetSeriesByName(this.PluginSettings.InputSeries);
            if (inputSeries == null) {
                throw new PluginException(string.Format("Series {0} does not exist", this.PluginSettings.InputSeries));
            }
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Frame resultFrame = ImageMath.GetProcess(inputSeries[t], BinLogic, 1, t);
            series.Add(resultFrame);
            application.AddSeries(series);
        }
        public byte[] BinLogic(int time, byte[] inputPixel)
        {
            byte r = inputPixel[0];
            if (r > PluginSettings.Threshold.GetValue(time)) {
                r = byte.MaxValue; 
            }
            else if (this.PluginSettings.Mode) {
                r = byte.MinValue;
            }
            inputPixel[0] = r;
            return inputPixel;
        }

        protected override string GetHelpString()
        {
            return "Переводит яркость всех изображений указанного в первом окне видеоряда бинарный формат. все точки с яркостью выше указанного порога становятся белыми, остальные - черными (с включенным режимом отсечения фона) или остаются без изменений. сохраняет результат в новый видеоряд";
        }
    }
}
