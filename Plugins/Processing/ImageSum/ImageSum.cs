using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using DIPStudioUICore;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageSum {
    public class ImageSum : Plugin<ImageSumSettings, ImageSumUserControl> {
        public ImageSum()
            : base(ProjectKind.Processing, "Сложение изображений", "[СИ]")
        {

        }

        private void Logic(int t) {
            DIPApplication application = DIPApplication.GetInstance();
            Series inputSeries1 = application.GetSeriesByName( this.PluginSettings.InputSeries1);
            if (inputSeries1 == null) {
                throw new PluginException(string.Format("Series {0} does not exist", this.PluginSettings.InputSeries1));
            }
            Series inputSeries2 = application.GetSeriesByName( this.PluginSettings.InputSeries2);
            if (inputSeries2 == null) {
                throw new PluginException(string.Format("Series {0} does not exist", this.PluginSettings.InputSeries2));
            }
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Frame sourceFrame1 = inputSeries1[t];
            Frame sourceFrame2 = inputSeries2[t];
            Frame resultFrame;
            if (sourceFrame1 == null && sourceFrame2 == null)
                return;
            else if (sourceFrame1 != null && sourceFrame2 != null)
                resultFrame = ImageMath.GetSumFrame(sourceFrame1, sourceFrame2);
            else {
                resultFrame = sourceFrame1;
                if (resultFrame == null)
                    resultFrame = sourceFrame2;
            }
            series.Add(resultFrame);
            application.AddSeries(series);
        }

        protected override void Run(int t, int index, int tFinish)
        {
            Logic(t);
        }

        protected override string GetHelpString()
        {
            return "Выберите требуемые видеряды в первом и втором окне. сложение производится попиксельно с левого верхнего угла";
        }
    }
}
