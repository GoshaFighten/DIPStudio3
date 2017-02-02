using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;

namespace ModelImages
{
    public class ModelImages : Plugin<ModelImagesSettings, ModelImagesUserControl> 
    {
        public ModelImages()
            : base(ProjectKind.Modeling, "Моделирование видеоряда", "[МВ]")
        {
        }
        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 580;
            frm.Height = 700;
        }
        #region Члены IPlugin


        /// <summary>
        /// Метод, который выполняет математику плагина
        /// </summary>
        /// <param name="t">Текущее время выполнения</param>
        /// <param name="index">Номер текущего цикла</param>
        protected override void Run(int t, int index, int tFinish) {
            DIPApplication application = DIPApplication.GetInstance();
            int shift = 200;

            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Frame resultFrame = new Frame(t, PluginSettings.ModelFrame.Width, PluginSettings.ModelFrame.Height, PixelFormat.Format32bppArgb);

            using (Graphics gr = Graphics.FromImage(resultFrame.Image)) {
                PluginSettings.ModelBackground.Draw(gr, index, PluginSettings.ModelFrame);
                PluginSettings.ModelTarget.Draw(gr, (t + shift) / 1000.0, PluginSettings.ModelMath, PluginSettings.ModelFrame);
                PluginSettings.ModelDisturbance.Draw(gr, t / 1000.0, PluginSettings.ModelFrame, PluginSettings.ModelReceiver);
            }

            ImageConvertor.Convertor convertor = new ImageConvertor.Convertor();
            Bitmap newImage = convertor.ConvertTo8bppFormat(resultFrame.Image);
            resultFrame.Image.Dispose();
            resultFrame.Image = newImage;

            resultFrame.Name = index.ToString();
            series.Add(resultFrame);
            application.AddSeries(series);
        }

        public ColorPalette GetGrayscalePalette(ColorPalette palette)
        {
            for (int i = 0; i < 256; i++)
                palette.Entries[i] = Color.FromArgb(i, i, i);

            return palette;
        }

        #endregion

        protected override string GetHelpString()
        {
            return "Создание видеоряда с заданными параметрами. выберите размер изображений, параметры фона, объекта и помех.";
        }
    }
}
