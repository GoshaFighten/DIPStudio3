using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;

namespace RGB2Gray {
    public class RGB2Gray : Plugin<RGB2GraySettings, RGB2GrayUserControl>
    {
        public RGB2Gray()
            : base(ProjectKind.Processing, "Преобразование в серое", "[ПС]")
        {
        }
        
        #region Члены IPlugin

        /// <summary>
        /// Метод, который выполняет математику плагина
        /// </summary>
        /// <param name="t">Текущее время выполнения</param>
        /// <param name="index">Номер текущего цикла</param>

        protected override void Run(int t, int index, int tFinish)
        {
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            Series inputSeries = application.GetSeriesByName( this.PluginSettings.InputSeries);
            if (inputSeries == null) {
                throw new PluginException(string.Format("Series {0} does not exist", this.PluginSettings.InputSeries));
            }
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Frame resultFrame = ImageMath.GetProcess(inputSeries[t], Logic, Image.GetPixelFormatSize(inputSeries[t].Image.PixelFormat) / 8, t);

            resultFrame.Name = inputSeries[t].Name;
            series.Add(resultFrame);
            application.AddSeries(series);
        }
        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 600;
            frm.Height = 500;
        }

        public byte[] Logic(int t, byte[] inputPixel)
        {
            int pixelLength = inputPixel.Count();
            byte[] result = new byte[inputPixel.Count()];
            if (pixelLength > 1) {
                double r = PluginSettings.Red,
                       g = PluginSettings.Green,
                       b = PluginSettings.Blue;

                double sum = r + g + b;
                for (int i = 0; i < inputPixel.Count(); i++) {
                    result[i] = (byte)(r * inputPixel[0] / sum + g * inputPixel[1] / sum + b * inputPixel[2] / sum);
                }
            }
            else if (pixelLength == 1)
                result = inputPixel;
            return result;
        }

        #endregion

        protected override string GetHelpString()
        {
            return "sum = r + g + b  result[i] = (byte)(r * inputPixel[0] / sum + g * inputPixel[1] / sum + b * inputPixel[2] / sum);";
        }
    }
}
