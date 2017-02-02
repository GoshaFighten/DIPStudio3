using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;
using DIPStudioCore.Data;

namespace ErodeImages2
{
    public class ErodeImages : Plugin<ErodeImagesSettings, ErodeImagesUserControl>
    {
        public ErodeImages()
            : base(ProjectKind.Processing, "Эрозия изображения", "[ЭИ]")
        {
        }

        #region Члены IPlugin

        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 500;
            frm.Height = 450;
        }

        /// <summary>
        /// Метод, который выполняет математику плагина
        /// </summary>
        /// <param name="t">Текущее время выполнения</param>
        /// <param name="index">Номер текущего цикла</param>
        protected override void Run(int t, int index, int tFinish) {
            DIPApplication application = DIPApplication.GetInstance();
            Series inputSeries = application.GetSeriesByName( PluginSettings.InputSeries);
            if (inputSeries == null)
                throw new PluginException(string.Format("Series {0} does not exist", PluginSettings.InputSeries));
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Frame sourceFrame = inputSeries[t];
            Frame resultFrame = ImageMath.GetExpandProcess(sourceFrame, GetMin, (int) PluginSettings.Order.GetValue(t) / 2, Byte.MaxValue);
            series.Add(resultFrame);
            application.AddSeries(series);
        }

        public byte GetMin(byte[] mask)
        {
            return mask.Min();
        }

        #endregion

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }
}
