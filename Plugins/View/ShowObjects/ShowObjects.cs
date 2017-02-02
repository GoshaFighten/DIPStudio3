using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using DIPStudioCore.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace ShowObjects {
    public class ShowObjects : Plugin<ShowObjectsSettings, ShowObjectsUserControl> {
        public ShowObjects() : base(DIPStudioCore.ProjectKind.View, "Отображение объектов", "[ОО]") {
            points.DefaultIfEmpty(PointWithColor.Empty);
        }
        protected override void Run(int t, int index, int tFinish)
        {
            DIPApplicationBase app = DIPApplicationBase.GetInstance();
            Series inputSeries = app.GetSeriesByName( this.PluginSettings.InputImages);
            if (inputSeries == null) {
                throw new PluginException(string.Format("Series {0} does not exist", this.PluginSettings.InputImages));
            }
            Table inputTable = app.GetTableByName(PluginSettings.InputTable);
            if (inputTable == null) {
                throw new PluginException(string.Format("Table {0} does not exist", PluginSettings.InputTable));
            }
            Series resultSeries = app.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Frame sourceFrame = inputSeries[t];
            DataFrame sourceObjects = inputTable[t];


            Bitmap resultImage = new Bitmap(sourceFrame.Image);
            Graphics g = Graphics.FromImage(resultImage);
            foreach (IObjectWithProperties obj in sourceObjects.Objects) {
                string objName = (string)obj.Properties["Name"];
                int x = Convert.ToInt32(obj.Properties["X"]);
                int y = Convert.ToInt32(obj.Properties["Y"]);

                PointWithColor c = points.FirstOrDefault(p => p.NameTrace == objName);

                Color color;
                if (c.Equals(PointWithColor.Empty)) {
                    PointWithColor newPoint = new PointWithColor() { NameTrace = objName, Color = colors[TraceCount++] };
                    points.Add(newPoint);
                    color = newPoint.Color;
                }
                else {
                    color = c.Color;
                }

                g.DrawLine(new Pen(color), new Point(x, y - lenght), new Point(x, y + lenght));
                g.DrawLine(new Pen(color), new Point(x - lenght, y), new Point(x + lenght, y));
            }
            Frame resultFrame = new Frame(t, resultImage);
            resultFrame.Name = sourceFrame.Name;
            resultSeries.Add(resultFrame);
            app.AddSeries(resultSeries);
        }
        const int lenght = 5;

        private struct PointWithColor {
            static PointWithColor fEmpty = new PointWithColor() { Color = Color.Empty, NameTrace = null };
            public static PointWithColor Empty {
                get {
                    return fEmpty;
                }
            }
            public Color Color { get; set; }
            public string NameTrace { get; set; }
        }

        private static List<PointWithColor> points = new List<PointWithColor>();

        private static int TraceCount = 0;
        private Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Aqua, Color.Green, Color.Yellow, Color.Orange, Color.Brown, Color.Violet, Color.LightBlue };

        protected override string GetHelpString()
        {
            return "Выберите видеоряд в первом окне и данные во втором. данные должны содержать столбцы Х и У. Плагин добавит на каждое изображение видеоряда крест по указнным в данных координатам (в пикселях)";
        }
    }
}
