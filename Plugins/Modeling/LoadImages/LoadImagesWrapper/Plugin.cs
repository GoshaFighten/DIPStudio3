using DIPStudioCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPlugin
{
    public class Plugin
    {
        public static OutputObject Run(InputObject args) {
            args.FolderName = !string.IsNullOrEmpty(args.FolderName) ? args.FolderName : @"D:\test";
            args.ResultName = !string.IsNullOrEmpty(args.ResultName) ? args.ResultName : "imgs";
            var app = SimInTechApplication.SimInTechApplication.RequestApplication();
            IPlugin plugin = (IPlugin)(new LoadImages.LoadImages());
            var settings = (LoadImages.LoadImageSettings)plugin.PluginSettings;
            settings.FolderName = args.FolderName;
            settings.StartTime = args.StartTime;
            settings.D = args.D;
            settings.Convert = args.Convert;
            settings.ResultName = args.ResultName;
            plugin.Run(args.Time, -1, int.MaxValue);
            var result = new OutputObject();
            Bitmap img = app.GetImage(plugin.ShortName + args.ResultName, args.Time);
            result.Size = img.Size;
            result.Data = new int[result.Size.Width * result.Size.Height];
            for (int i = 0; i < result.Size.Height; i++) {
                for (int j = 0; j < result.Size.Width; j++) {
                    result.Data[i * result.Size.Width + j] = img.GetPixel(j, i).R;
                }
            }
            return result;
        }
    }
}
