using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;

namespace CropImages
{
    public class CropImages : Plugin<CropImagesSettings, CropImagesUserControl>
    {
        public CropImages()
            : base(ProjectKind.Modeling, "Обрезка изображений", "[ОИ]")
        {
        }
        /// <summary>
        /// Метод, который выполняет математику плагина
        /// </summary>
        /// <param name="t">Текущее время выполнения</param>
        /// <param name="index">Номер текущего цикла</param>
        protected override void Run(int t, int index, int tFinish)
        {
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            Series inputSeries = application.GetSeriesByName( PluginSettings.InputSeries);
            if (inputSeries == null)
            {
                throw new PluginException(string.Format("Series {0} does not exist", PluginSettings.InputSeries));
            }
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Rectangle newRect = new Rectangle(PluginSettings.X, PluginSettings.Y, PluginSettings.Width, PluginSettings.Height);
            Frame resultFrame = ImageMath.GetProcess(inputSeries[t], (x, y) => { return y; }, newRect, newRect.Width, t); 
            series.Add(resultFrame);
            application.AddSeries(series);
        }
        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 550;
            frm.Height = 450;
        }

        protected override string GetHelpString()
        {
            return "Перед настройкой данного плагина рекомендуется выполнить загрузку хотя бы одного изображения из требуемого видеоряда." + Environment.NewLine + "Выберите видеоряд в первом поле, выделите мышкой требуемую область нового изображения в окне предпросмотра, подкорректируйте результаты при необходимости и выберите имя для сохраняемого видеоряда.";
        }
    }
}
