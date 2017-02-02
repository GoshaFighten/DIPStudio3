using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DIPStudioCore;

namespace ModelImages {
    [Serializable]
    public class ModelTarget {
        private bool _UseRData;

        private bool _UseVData;

        private string _VField;

        private string _VDataName;

        private string _RField;

        private double fAM = 100;

        public double AM {
            get { return fAM; }
            set { fAM = value; }
        }

        private double fDeltaD = 0.6;

        public double DeltaD {
            get { return fDeltaD; }
            set { fDeltaD = value; }
        }

        private double fXC = 200;

        public double XC {
            get { return fXC; }
            set { fXC = value; }
        }

        private double fXCNRnd = 50;

        public double XCNRnd {
            get { return fXCNRnd; }
            set { fXCNRnd = value; }
        }

        private double fWC = 5;

        public double WC {
            get { return fWC; }
            set { fWC = value; }
        }

        private double fRM = 300;

        public double RM {
            get { return fRM; }
            set { fRM = value; }
        }

        private double fYC = 200;

        public double YC {
            get { return fYC; }
            set { fYC = value; }
        }

        private double fYCNRnd = 50;

        public double YCNRnd {
            get { return fYCNRnd; }
            set { fYCNRnd = value; }
        }

        private double fXMRnd = 10;

        public double XMRnd {
            get { return fXMRnd; }
            set { fXMRnd = value; }
        }

        private double fYMRnd = 10;

        public double YMRnd {
            get { return fYMRnd; }
            set { fYMRnd = value; }
        }

        private double fP = 0.08;

        public double P {
            get { return fP; }
            set { fP = value; }
        }

        private double fARnd = 2;

        public double ARnd {
            get { return fARnd; }
            set { fARnd = value; }
        }

        private double fAlpha = 0.2;

        public double Alpha {
            get { return fAlpha; }
            set { fAlpha = value; }
        }

        public void Draw(Graphics gr, double t, ModelMath math, ModelFrame frame) {
            RectangleF rect = CalcRect(t, math, frame);
            DrawObject(gr, rect, math, t);
        }

        private RectangleF CalcRect(double t, ModelMath math, ModelFrame frame) {
            float width = (float)math.ap(t);
            float height = (float)math.ap(t);
            float x0 = (float)math.x(t) + frame.Width / 2;
            float y0 = (float)math.y(t) + frame.Height / 2;
            //Log("Объект", x0, y0, width, height, t);
            RectangleF rect = new RectangleF(x0 - width / 2, y0 - height / 2, width, height);
            return rect;
        }

        private void DrawObject(Graphics gr, RectangleF rect, ModelMath math, double t) {
            int buffer = 5;
            using(Bitmap image = new Bitmap((int)rect.Width + 2 * buffer, (int)rect.Height + 2 * buffer)) {
                for(int i = 0; i < image.Width; i++) {
                    for(int j = 0; j < image.Height; j++) {
                        Color color = GetColor(i, j, rect.Size, buffer, math, t);
                        image.SetPixel(i, j, color);
                    }
                }
                gr.DrawImage(image, rect.Location);
            }
        }

        private Color GetColor(int x, int y, SizeF size, int buffer, ModelMath math, double t) {
            double x0 = size.Width / 2.0 + buffer;
            double a = size.Width / 2.0;
            double y0 = size.Height / 2.0 + buffer;
            double b = size.Height / 2.0;
            return (x - x0) * (x - x0) / a / a + (y - y0) * (y - y0) / b / b <= 1 ? CreateColor(math, t, (x - x0), (y - y0), a, b) : Color.Transparent;
        }

        private Color CreateColor(ModelMath math, double t, double x, double y, double a, double b) {
            int component = (int)(math.I(t, x, y, a, b) * ImageMath.ImageDepth);
            return Color.FromArgb(component, component, component);
        }

        private string fRDataName;

        //private DataHelper logger;

        public string RDataName {
            get { return fRDataName; }
            set { fRDataName = value; }
        }

        public string RField {
            get { return _RField; }
            set { _RField = value; }
        }

        public bool UseRData {
            get { return _UseRData; }
            set { _UseRData = value; }
        }

        public string VDataName {
            get { return _VDataName; }
            set { _VDataName = value; }
        }

        public string VField {
            get { return _VField; }
            set { _VField = value; }
        }

        public bool UseVData {
            get { return _UseVData; }
            set { _UseVData = value; }
        }

        //public void SetLogger(DataHelper logger) {
        //    this.logger = logger;
        //}

        //private void Log(string name, float x0, float y0, double width, double height, double time) {
        //    if(logger == null) {
        //        return;
        //    }
        //    logger.Log(name, x0, y0, width, height, time);
        //}
    }
}
