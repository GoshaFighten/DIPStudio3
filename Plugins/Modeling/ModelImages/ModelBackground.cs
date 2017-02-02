using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using ModelImages.Generator;
using DIPStudioCore;

namespace ModelImages {
    [Serializable]
    public class ModelBackground {
        private static NormalDistribution rnd;

        static ModelBackground() {
            rnd = new NormalDistribution();
        }

        public ModelBackground() {
        }
        [NonSerialized]
        private DIPApplicationBase fApplication = DIPApplicationBase.GetInstance();

        public Series GetVisuals() {
            return fApplication.GetSeriesByName(BackgroundImages);
        }
        
        private Bitmap GetImage(int index) {
            return GetVisuals()[index].Image;
        }
        public void Draw(Graphics gr, int index, ModelFrame frame) {
            if (UseImages) {
                gr.DrawImage(GetImage(index), new Rectangle(Point.Empty, new Size(frame.Width, frame.Height)));
                return;
            }
            using (Bitmap img = new Bitmap(frame.Width, frame.Height)) {
                for (int i = 0; i < frame.Height; i++) {
                    for (int j = 0; j < frame.Width; j++) {
                        img.SetPixel(j, i, GetColor(j, i, frame));
                    }
                }
                gr.DrawImage(img, Point.Empty);
            }
        }

        private int fARnd = 2;

        public int ARnd {
            get {
                return fARnd;
            }
            set {
                fARnd = value;
            }
        }

        private int fA = 22;

        public int A {
            get {
                return fA;
            }
            set {
                fA = value;
            }
        }

        private int fW = 200;

        public int W {
            get {
                return fW;
            }
            set {
                fW = value;
            }
        }

        private int fH = 200;

        public int H {
            get {
                return fH;
            }
            set {
                fH = value;
            }
        }

        private bool fUseImages;
        public bool UseImages {
            get {
                return fUseImages;
            }
            set {
                fUseImages = value;
            }
        }

        private string fBackgroundImages = "";
        public string BackgroundImages {
            get {
                return fBackgroundImages;
            }
            set {
                fBackgroundImages = value;
            }
        }
        
        private Color GetColor(int x, int y, ModelFrame frame) {
            int component = (int)((ARnd * rnd.NextDouble() + A) / (Math.Sqrt(((2.0 * x - frame.Width) / W) * ((2.0 * x - frame.Width) / W) + ((2.0 * y - frame.Height) / H) * ((2.0 * y - frame.Height) / H) + 1.0)));
            if (component < 0)
                component = 0;
            return Color.FromArgb(component, component, component);
        }
    }
}
