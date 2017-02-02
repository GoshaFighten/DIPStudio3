using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.IO;
using DIPStudioUICore;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace LoadImages {
    public class LoadImages : Plugin<LoadImageSettings, LoadImagesUserControl> {
        public LoadImages()
            : base(ProjectKind.Modeling, "Загрузить изображения", "[ЗИ]")
        {

        }
        protected override void CustomizeForm(BasePluginForm frm) {
            base.CustomizeForm(frm);
            frm.Width = 400;
            frm.Height = 250;
        }
        private void LogicNew(int t) {
            if (!Directory.Exists(PluginSettings.FolderName)) {
                throw new PluginException(string.Format("Folder \"{0}\" does not exist", PluginSettings.FolderName));
            }
            DIPApplication application = DIPApplication.GetInstance();
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            application.AddSeries(series);
            if (t < PluginSettings.StartTime) {
                return;
            }
            string[] files = Directory.GetFiles(PluginSettings.FolderName);
            using (NaturalComparer comparer = new NaturalComparer()) {
                files = files.OrderBy((s) => s, comparer).ToArray();
            }
            int fileCount = files.Count();
            int maxEndTime = PluginSettings.StartTime + (fileCount - 1) * PluginSettings.D;
            int loadedImagesCount = series.Count;
            if (t > maxEndTime) {
                if (loadedImagesCount == 0)
                    return;
                Frame lastLoadedFrame = series.GetElementByIndex(loadedImagesCount - 1);
                int lastLoadedTime = lastLoadedFrame.Time;
                for (int timeToLoad = lastLoadedTime + PluginSettings.D; timeToLoad <= maxEndTime; timeToLoad += PluginSettings.D) {
                    LoadImage(series, files, (timeToLoad - PluginSettings.StartTime) / PluginSettings.D);
                }
            }

            if (loadedImagesCount == 0) {
                int deep = 5;
                for (int timeToLoad = t - (deep * PluginSettings.D); timeToLoad <= t; timeToLoad += PluginSettings.D) {
                    int index = (timeToLoad - PluginSettings.StartTime) / PluginSettings.D;
                    if (index < 0)
                        continue;
                    LoadImage(series, files, index);
                }
            }
            else {
                Frame lastLoadedFrame = series.GetElementByIndex(loadedImagesCount - 1);
                int lastLoadedTime = lastLoadedFrame.Time;
                for (int timeToLoad = lastLoadedTime + PluginSettings.D; timeToLoad <= t; timeToLoad += PluginSettings.D) {
                    LoadImage(series, files, (timeToLoad - PluginSettings.StartTime) / PluginSettings.D);
                }
            }
        }
        private void Logic(int t) {
            if (!Directory.Exists(PluginSettings.FolderName)) {
                throw new PluginException(string.Format("Folder \"{0}\" does not exist", PluginSettings.FolderName));
            }
            DIPApplication application = DIPApplication.GetInstance();
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            application.AddSeries(series);
            //if (t < PluginSettings.StartTime) {
            //    return;
            //}
            string[] files = Directory.GetFiles(PluginSettings.FolderName);
            using (NaturalComparer comparer = new NaturalComparer()) {
                files = files.OrderBy((s) => s, comparer).ToArray();
            }
            //int fileCount = files.Count();
            //int endTime = PluginSettings.StartTime + (fileCount - 1) * PluginSettings.D;

            int deep = 5;
            int end = (t - PluginSettings.StartTime) / PluginSettings.D + 1;
            int startTest = end - deep;
            int start;
            if (series.Count == 0) {
                start = startTest > PluginSettings.StartTime ? startTest : PluginSettings.StartTime;
            }
            else {
                int lastTime = series.Last().Time;
                if ((end - lastTime) / PluginSettings.D < deep) {
                    start = lastTime / PluginSettings.D + 1;
                }
                else {
                    start = startTest > PluginSettings.StartTime ? startTest : PluginSettings.StartTime;
                }
            }

            int maxEnd = files.Count();
            if (end > maxEnd)
                return;
            for (int i = start; i < end; i++) {
                LoadImage(series, files, i);
            }

        }

        private void LoadImage(Series series, string[] files, int i) {
            Bitmap image = (Bitmap)Image.FromFile(files[i]);
            if (PluginSettings.Convert && image.PixelFormat != PixelFormat.Format8bppIndexed) {
                ImageConvertor.Convertor convertor = new ImageConvertor.Convertor();
                Bitmap newImage = convertor.ConvertTo8bppFormat(image);
                image.Dispose();
                image = newImage;
            }
            series.Add(new Frame(i * PluginSettings.D + PluginSettings.StartTime, image) {
                Name = files[i]
            });
        }

        protected override void Run(int t, int index, int tFinish) {
            LogicNew(t);
        }

        protected override string GetHelpString()
        {
            return "Выберите в первом окне папку, в которой хранятся ваши изображения, задайте начальное время нового видеоряда, его дискретизацию и имя";
        }
    }
}
