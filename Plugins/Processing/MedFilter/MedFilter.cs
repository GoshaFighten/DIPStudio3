using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;
using DIPStudioCore.Data;

namespace MedFilter2 {
    public class MedFilter : Plugin<MedFilterSettings, MedFilterUserControl>
    {
        public MedFilter()
            : base(ProjectKind.Processing, "Медианная фильтрация", "[МФ]")
        {
        }

        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 500;
            frm.Height = 450;
        }
        #region Члены IPlugin

        /// <summary>
        /// Метод, который выполняет математику плагина
        /// </summary>
        /// <param name="t">Текущее время выполнения</param>
        /// <param name="index">Номер текущего цикла</param>
        protected override void Run(int t, int index, int tFinish) {
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            Series inputSeries = application.GetSeriesByName(PluginSettings.InputSeries);
            if (inputSeries == null) {
                throw new PluginException(string.Format("Series {0} does not exist", PluginSettings.InputSeries));
            }
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Frame sourceFrame = inputSeries[t];
            Frame resultFrame = ImageMath.GetExpandProcess(sourceFrame, GetAverage, (int)PluginSettings.Order.GetValue(t) / 2, Byte.MinValue);

            series.Add(resultFrame);
            application.AddSeries(series);
        }
        public byte GetAverage(byte[] mask)
        {
            Array.Sort(mask);
            return mask[mask.Count() / 2];
        }

        #endregion

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }
}
