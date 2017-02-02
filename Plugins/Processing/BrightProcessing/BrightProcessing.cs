using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;

namespace BrightProcessing {
    public class BrightProcessing : Plugin <BrightProcessingSettings, BrightProcessingUserControl>
    {
        public BrightProcessing()
            : base(ProjectKind.Processing, "Преобразование яркости", "[ПРЯ]")
        {
        }
        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 600;
            frm.Height = 500;
        }
        protected override void Run(int t, int index, int tFinish)
        {
            DIPApplication application = DIPApplication.GetInstance();
            Series inputSeries = application.GetSeriesByName( this.PluginSettings.InputSeries);
            if (inputSeries == null) {
                throw new PluginException(string.Format("Series {0} does not exist", this.PluginSettings.InputSeries));
            }
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Frame resultFrame = ImageMath.GetProcess(inputSeries[t], Logic, 1, t);

            resultFrame.Name = inputSeries[t].Name;
            series.Add(resultFrame);
            application.AddSeries(series);
        }

        public byte[] Logic(int time, byte[] inputPixel)
        {
            int x1 = PluginSettings.X1,
                x2 = PluginSettings.X2,
                y1 = PluginSettings.Y1,
                y2 = PluginSettings.Y2;
            byte[] result = new byte[1];
            if (inputPixel[0] <  x1) {
                result[0] = (byte)(y1 / x1 * result[0]);
            }
            if ((result[0] >= x1) && (result[0] < x2)) {
                result[0] = (byte)((y1 - y2) / (x1 - x2) * (result[0] - x2) + y2);
            }
            if (result[0] >= x2) {
                result[0] = (byte)((y2 - ImageMath.ImageDepth - 1) / (x2 - ImageMath.ImageDepth - 1) * (result[0] - ImageMath.ImageDepth - 1) + ImageMath.ImageDepth - 1);
            }
            return result;
        }

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }
}
