using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;
using DIPStudioCore.Data;
using AviFile;

namespace Avi2Bmp {
    public class Avi2Bmp : Plugin<Avi2BmpSettings, Avi2BmpUserControl> {
        public Avi2Bmp()
            : base(ProjectKind.Modeling, "Avi2Bmp", "[В2И]")
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
            if (index != 0) {
                return;
            }
            Avi2BmpSettings settings = (Avi2BmpSettings) PluginSettings;
            if (settings.InputSeries == String.Empty)
                return;
            bool saveToDisk = settings.InputSeries != String.Empty;

            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            AviManager aviManager = new AviManager(settings.InputSeries, true);

            VideoStream stream = aviManager.GetVideoStream();
            stream.GetFrameOpen();
            int discretization = (int)(1000 / stream.FrameRate);
            Series result = new Series() { Name = settings.ResultName };
            for (int n = settings.StartTime / discretization; n < settings.FinishTime / discretization; n++) {
                result.Add(new Frame(discretization * (n - settings.StartTime / discretization), stream.GetBitmap(n)));
                if (saveToDisk)
                    stream.ExportBitmap(n, settings.SavePath + "\\" + (discretization * n).ToString() + ".bmp");
            }
            application.AddSeries(result);
            stream.GetFrameClose();
            aviManager.Close();
        }

        protected override string GetHelpString()
        {
            return "Плагин переводит видеофайлы .avi в изображения .bmp " + Environment.NewLine + "Выберите видеофайл в первом поле, при необходимости сразу сохранить получившиеся картинки в папку, выберите папку во втором поле, иначе - оставьте его пустым. если файл удачно расшифруется, вы увидите его частоту и количество кадров, иначе - отошлите файл разработчикам для анализа.";;
        }
    }
}
