using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;

namespace InvImages {
    public class InvImages : Plugin<InvImagesSettings, InvImagesUserControl>
    {
        public InvImages()
            : base(ProjectKind.Processing, "Инверсия изображений", "[ИИ]")
        {
        }

        #region Члены IPlugin

        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 500;
            frm.Height = 400;
        }
        /// <summary>
        /// Метод, который выполняет математику плагина
        /// </summary>
        /// <param name="t">Текущее время выполнения</param>
        /// <param name="index">Номер текущего цикла</param>
        protected override void Run(int t, int index, int tFinish) {
            DIPApplication application = DIPApplication.GetInstance();
            Series inputSeries = application.GetSeriesByName(PluginSettings.InputSeries);
            if (inputSeries == null) {
                throw new PluginException(string.Format("Series {0} does not exist", PluginSettings.InputSeries));
            }
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Frame sourceFrame = inputSeries[t];
            Frame resultFrame = new Frame(t, ImageMath.GetInvertedImage(inputSeries[t].Image));
            series.Add(resultFrame);
            application.AddSeries(series);
        }
        #endregion

        protected override string GetHelpString()
        {
            return "Плагин сделает негатив изображений выбранного в первом поле видеоряда и сохранит под указанным именем";
        }
    }
}
