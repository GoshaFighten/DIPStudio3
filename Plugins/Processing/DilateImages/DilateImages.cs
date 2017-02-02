using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;
using DIPStudioCore.Data;

namespace DilateImages
{
    public class DilateImages : Plugin <DilateImagesSettings, DilateImagesUserControl>
    {
        public DilateImages()
            : base(ProjectKind.Processing, "Дилатация изображений", "[ДИ]")
        {
            
        }
        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 500;
            frm.Height = 400;
        }
        #region Члены IPlugin

        /// <summary>
        /// Метод, который выполняет математику плагина
        /// </summary>
        /// <param name="t">Текущее время выполнения</param>
        /// <param name="index">Номер текущего цикла</param>
        protected override void Run(int t, int index, int tFinish) {
            DIPApplication application = DIPApplication.GetInstance();
            Series inputSeries = application.GetSeriesByName( PluginSettings.InputSeries);
            if (inputSeries == null) {
                throw new PluginException(string.Format("Series {0} does not exist", PluginSettings.InputSeries));
            }
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Frame sourceFrame = inputSeries[t];
            int order = (int) PluginSettings.Order.GetValue(t) / 2;
            if (order == 0)
                return;
            Frame resultFrame = ImageMath.GetExpandProcess(sourceFrame, GetMax, order, Byte.MinValue);
            
            series.Add(resultFrame);
            application.AddSeries(series);
        }
        public byte GetMax(byte[] mask)
        {
            return mask.Max();
        }

        #endregion

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }
}
