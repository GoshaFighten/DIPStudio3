using System;
using System.Collections.Generic;
using System.Text;

namespace ModelImages {
    [Serializable]
    public class ModelFrame {
        //private double fStartTime = 0;
        [NonSerialized]
        private ModelBackground fBackground;
        public ModelBackground Background {
            get {
                return fBackground;
            }
            set {
                fBackground = value;
            }
        }
        
        private int fWidth = 512;

        public int Width {
            get {
                if(Background != null && Background.UseImages)
                    return Background.GetVisuals()[0].Image.Width;
                return fWidth;
            }
            set { fWidth = value; }
        }

        private int fHeight = 512;

        public int Height {
            get {
                if(Background != null && Background.UseImages)
                    return Background.GetVisuals()[0].Image.Height;
                return fHeight;
            }
            set { fHeight = value; }
        }
    }
}
