using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using DIPStudioCore.Data;
using DIPStudioUICore;

namespace SelectObjs {
    public class SelectObjs : Plugin<SelectObjsSettings, SelectObjsUserControl> {
        public SelectObjs()
            : base(ProjectKind.Selection, "Выделение объектов", "[ВО]")
        {

        }
        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 500;
            frm.Height = 700;
        }

        private void AddSeries(int t, Series inputSeries)
        {
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName);
            Frame resultFrame = ImageMath.GetProcess(inputSeries[t], BinLogic, 1, t);
            series.Add(resultFrame);
            application.AddSeries(series);
        }
        public byte[] BinLogic(int time, byte[] inputPixel)
        {
            byte r = inputPixel[0];
            if (r > PluginSettings.Threshold.GetValue(time)) {
                r = byte.MaxValue;
            }
            inputPixel[0] = r;
            return inputPixel;
        }

        private void Logic(int t) {
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            Series inputSeries = application.GetSeriesByName( this.PluginSettings.InputSeries);
            if (inputSeries == null) {
                throw new PluginException(string.Format("Series {0} does not exist", this.PluginSettings.InputSeries));
            }
            if (this.PluginSettings.AddSeries) {
                AddSeries(t, inputSeries);
            }
            Frame sourceFrame = inputSeries[t];
            Table table = application.GetTableByNameOrCreateNew(ShortName + PluginSettings.ResultName, inputSeries.Name);
            DataFrame frame = new DataFrame(t);
            frame.Name = sourceFrame.Name;
            frame.Objects.Pattern = CoordObject.GetPattern();
            List<Area> areas = MathSelectObjs.GetObjs(sourceFrame.Image, (int) this.PluginSettings.Threshold.GetValue(t));
            if (areas.Count != 0) {
                for (int j = 0; j < areas.Count; j++) {
                    CoordObject coordObject = new CoordObject();
                    frame.Objects.Add(coordObject);
                    coordObject.Name = (j + 1).ToString();
                    coordObject.X = areas[j].CenterX;
                    coordObject.Y = areas[j].CenterY;
                    coordObject.S = areas[j].S;
                    coordObject.Width = areas[j].Width;
                    coordObject.Height = areas[j].Height;
                    coordObject.MaxBrightness = areas[j].MaxBrightness;
                    coordObject.MBA = areas[j].MBA;
                    coordObject.QBA = areas[j].QBA;
                    coordObject.MBO = areas[j].MBO;
                    coordObject.QBO = areas[j].QBO;
                    coordObject.FrameWidth = sourceFrame.GetFrameWidth();
                    coordObject.FrameHeight = sourceFrame.GetFrameHeight();
                }
            }
            table.Add(frame);
            application.AddTable(table);
        }

        protected override void Run(int t, int index, int tFinish)
        {
            Logic(t);

        }

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }

    public class Coord {
        public int X;

        public int Y;

        public int Brightness;

        public Coord(int x, int y, int brightness) {
            X = x;
            Y = y;
            Brightness = brightness;
        }
    }

    public class Area {
        public List<Coord> coords = new List<Coord>();

        public int S;

        public double CenterX;

        public double CenterY;

        public int Width;

        public int Height;

        public int MaxBrightness;

        public double MBA;

        public double QBA;

        public double MBO;

        public double QBO;
    }

    public class CoordObject : ViewObject {
        internal const string STR_S = "S";

        internal const string STR_MaxBrightness = "MaxBrightness";

        internal const string STR_MBA = "MBA";

        internal const string STR_QBA = "QBA";

        internal const string STR_MBO = "MBO";

        internal const string STR_QBO = "QBO";

        internal const string STR_FrameWidth = "FrameWidth";

        internal const string STR_FrameHeight = "FrameHeight";

        public CoordObject() {
            properties.Add(STR_S, null);
            properties.Add(STR_MaxBrightness, null);
            properties.Add(STR_MBA, null);
            properties.Add(STR_QBA, null);
            properties.Add(STR_MBO, null);
            properties.Add(STR_QBO, null);
            properties.Add(STR_FrameWidth, null);
            properties.Add(STR_FrameHeight, null);
        }

        public CoordObject(string name, int x, int y, int width, int height, int s, int maxBrightness, double mba, double qba, double mbo, double qbo, int frameWidth, int frameHeight)
            : base(name, x, y, width, height) {
            S = s;
            MaxBrightness = maxBrightness;
            MBA = mba;
            QBA = qba;
            MBO = mbo;
            QBO = qbo;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
        }

        public int S {
            get { return (int)properties[STR_S]; }
            set { properties[STR_S] = value; }
        }

        public int MaxBrightness {
            get { return (int)properties[STR_MaxBrightness]; }
            set { properties[STR_MaxBrightness] = value; }
        }

        public double MBA {
            get { return (double)properties[STR_MBA]; }
            set { properties[STR_MBA] = value; }
        }

        public double QBA {
            get { return (double)properties[STR_QBA]; }
            set { properties[STR_QBA] = value; }
        }

        public double MBO {
            get { return (double)properties[STR_MBO]; }
            set { properties[STR_MBO] = value; }
        }

        public double QBO {
            get { return (double)properties[STR_QBO]; }
            set { properties[STR_QBO] = value; }
        }

        public int FrameWidth {
            get { return (int)properties[STR_FrameWidth]; }
            set { properties[STR_FrameWidth] = value; }
        }

        public int FrameHeight {
            get { return (int)properties[STR_FrameHeight]; }
            set { properties[STR_FrameHeight] = value; }
        }

        static IObjectWithProperties pattern;

        public static new IObjectWithProperties GetPattern() {
            return pattern;
        }

        static CoordObject() {
            pattern = new ObjectWithProperties();
            IObjectWithProperties source = ViewObject.GetPattern();
            pattern.Assign(source);
            pattern.Properties.Add(STR_S, typeof(int));
            pattern.Properties.Add(STR_MaxBrightness, typeof(int));
            pattern.Properties.Add(STR_MBA, typeof(double));
            pattern.Properties.Add(STR_QBA, typeof(double));
            pattern.Properties.Add(STR_MBO, typeof(double));
            pattern.Properties.Add(STR_QBO, typeof(double));
            pattern.Properties.Add(STR_FrameWidth, typeof(int));
            pattern.Properties.Add(STR_FrameHeight, typeof(int));
        }
    }
}
