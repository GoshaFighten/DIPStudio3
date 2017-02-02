using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DIPStudioCore;
using ModelImages.Generator;
using DIPStudioUICore;
using System.ComponentModel;

namespace ModelImages {
    [Serializable]
    public class ModelDisturbance : ValidatedPluginSettings
    {

        private static NormalDistribution rnd;

        public ModelDisturbance(IPlugin plugin)
            :base(plugin)
        {
            rnd = new NormalDistribution();
        }
        #region fields and properties
        private double fPy;

        private double fWy;

        private double fAy;

        private double fVy;

        private double fPx;

        private double fWx;

        private double fAx;

        private double fVx;

        //private DataHelper logger;


        private double fX0 = 5;
        [DisplayName("Х0, пикс")]
        public double X0 {
            get { return fX0; }
            set { fX0 = value; }
        }
        [DisplayName("Скорость по оси Х, пикс/с")]
        public double Vx {
            get { return fVx; }
            set { fVx = value; }
        }
        [DisplayName("Амплитуда по оси Х, пикс")]
        public double Ax {
            get { return fAx; }
            set { fAx = value; }
        }
        [DisplayName("Угловая скорость по оси Х, рад/с")]
        public double Wx {
            get { return fWx; }
            set { fWx = value; }
        }
        [DisplayName("Начальная фаза по оси Х, рад")]
        public double Px {
            get { return fPx; }
            set { fPx = value; }
        }

        private double fY0 = 5;
        [DisplayName("Y0, пикс")]
        public double Y0 {
            get { return fY0; }
            set { fY0 = value; }
        }
        [DisplayName("Скорость по оси Y, пикс/с")]
        public double Vy {
            get { return fVy; }
            set { fVy = value; }
        }
        [DisplayName("Амплитуда по оси Y, пикс")]
        public double Ay {
            get { return fAy; }
            set { fAy = value; }
        }
        [DisplayName("Угловая скорость по оси Y, рад/с")]
        public double Wy {
            get { return fWy; }
            set { fWy = value; }
        }
        [DisplayName("Начальная фаза по оси Y, рад")]
        public double Py {
            get { return fPy; }
            set { fPy = value; }
        }
        
        private double fS = 10;
        [DisplayName("Размер, пикс")]
        public double S {
            get { return fS; }
            set { fS = value; }
        }

        private double fIMAX = 1.0;
        [DisplayName("Максимальная яркость, доля")]
        public double IMAX {
            get { return fIMAX; }
            set { fIMAX = value; }
        }

        private double fA = 3;
        [DisplayName("Коэффициент, определяющий характер распределения яркости, 1/пикс")]
        public double A {
            get { return fA; }
            set { fA = value; }
        }

        private double fRandPosition = 0;
        [DisplayName("Случ. составляющая координаты, пикс")]
        public double RandPosition {
            get { return fRandPosition; }
            set { fRandPosition = value; }
        }

        private double fRandSize = 0;
        [DisplayName("Случ. составляющая площади, пикс")]
        public double RandSize {
            get { return fRandSize; }
            set { fRandSize = value; }
        }
        #endregion
        public void Draw(Graphics gr, double t, ModelFrame frame, ModelReceiver receiver) {
            double x = X0 + Vx * t + Ax * Math.Sin(Wx * t + Px) + RandPosition * rnd.NextDouble();
            double y = Y0 + Vy * t + Ay + Math.Sin(Wy * t + Py) + RandPosition * rnd.NextDouble();
            double s = S + RandSize * rnd.NextDouble();
            if(s <= 0) {
                s = S;
            }
            //Log("Помеха", x, y, s, s, t);
            RectangleF rect = new RectangleF((float)(x - s / 2), (float)(y - s / 2), (float)s, (float)s);
            DrawObject(gr, rect);
        }

        private void DrawObject(Graphics gr, RectangleF rect) {
            int buffer = 5;
            using(Bitmap image = new Bitmap((int)rect.Width + 2 * buffer, (int)rect.Height + 2 * buffer)) {
                for(int i = 0; i < image.Width; i++) {
                    for(int j = 0; j < image.Height; j++) {
                        Color color = GetColor(i, j, rect.Size, buffer);
                        image.SetPixel(i, j, color);
                    }
                }
                gr.DrawImage(image, rect.Location);
            }
        }

        private Color GetColor(double x, double y, SizeF size, int buffer) {
            double x0 = size.Width / 2.0 + buffer;
            double a = size.Width / 2.0;
            double y0 = size.Height / 2.0 + buffer;
            double b = size.Height / 2.0;
            return (x - x0) * (x - x0) / a / a + (y - y0) * (y - y0) / b / b <= 1 ? CreateColor((x - x0), (y - y0), a, b) : Color.Transparent;
        }

        private Color CreateColor(double x, double y, double a, double b) {
            int component = I(x, y, a, b);
            return Color.FromArgb(component, component, component);
        }

        private int I(double x, double y, double a, double b) {
            return (int)Math.Floor((ImageMath.ImageDepth - 1) * IMAX * Math.Exp(-Math.Sqrt(x * x + y * y) / A / (a * a + b * b)));
        }

        //public void SetLogger(DataHelper logger) {
        //    this.logger = logger;
        //}

        //private void Log(string name, double x0, double y0, double width, double height, double time) {
        //    if(logger == null) {
        //        return;
        //    }
        //    logger.Log(name, x0, y0, width, height, time);
        //}

        protected override bool IsResultSeries
        {
            get { return false; }
        }
    }
}
