using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using DIPStudioCore.Base;

namespace DIPStudioCore {
    public class Frame : FrameElement {
        public Frame(int time, Bitmap image)
            : base(time) {
            fImage = image;
        }

        public Frame(int time, int width, int height, System.Drawing.Imaging.PixelFormat format)
            : base(time) {
            fImage = new Bitmap(width, height, format);
        }

        private Image fThumbnail;

        private Bitmap fImage;

        public Bitmap Image {
            get { return fImage; }
            set {
                if(fImage == value) {
                    return;
                }
                fImage = value;
                OnPropertyChanged("Image");
            }
        }

        public int GetFrameWidth() {
            return Image.Width;
        }

        public int GetFrameHeight() {
            return Image.Height;
        }

        public Image Thumbnail {
            get {
                if(fThumbnail == null) {
                    fThumbnail = Image.GetThumbnailImage(ImageMath.ThumbnailSize, ImageMath.ThumbnailSize, () => {
                        return false;
                    }, System.IntPtr.Zero);
                }
                return fThumbnail;
            }
        }

        public new Frame GetNextElement(int offset) {
            return (Frame)base.GetNextElement(offset);
        }

        public new Frame GetPrevElement(int offset) {
            return (Frame)base.GetPrevElement(offset);
        }

        public new Frame GetNextElement() {
            return this.GetNextElement(1);
        }

        public new Frame GetPrevElement() {
            return this.GetPrevElement(1);
        }

        protected override void DisposeCore()
        {
            base.DisposeCore();
            this.fImage.Dispose();
            this.fImage = null;
            if (this.fThumbnail != null)
            {
                this.fThumbnail.Dispose();
                this.fThumbnail = null;
            }
        }
    }
}
