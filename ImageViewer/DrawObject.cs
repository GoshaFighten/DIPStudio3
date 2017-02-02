using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using DIPStudioCore;

namespace ImageViewer {
    public class DrawObject {
        private ImgViewer viewer;

        private Rectangle boundingRect;

        private Point dragPoint;

        private bool dragging;

        private Bitmap bmp;

        private Bitmap bmpPreview;

        private double zoom = 1.0;

        private int panelWidth {
            get { return viewer.PanelWidth; }
        }

        private int panelHeight {
            get { return viewer.PanelHeight; }
        }

        private int rotation = 0;

        public Rectangle BoundingBox {
            get { return boundingRect; }
        }

        //public void Dispose() {
        //    if(this.Image != null) {
        //        this.Image.Dispose();
        //        this.Image = null;
        //    }
        //}

        public bool IsDragging {
            get { return dragging; }
        }

        public Size OriginalSize {
            get {
                if(this.Image != null) {
                    return this.Image.Size;
                }
                else {
                    return Size.Empty;
                }
            }
        }

        public Size CurrentSize {
            get {
                if(boundingRect != null) {
                    return new Size(boundingRect.Width, boundingRect.Height);
                }
                else {
                    return Size.Empty;
                }
            }
        }

        public double Zoom {
            get { return zoom; }
        }

        public int Rotation {
            get { return rotation; }
            set {
                // Making sure that the rotation is only 0, 90, 180 or 270 degrees!
                if(value == 90 || value == 180 || value == 270 || value == 0) {
                    rotation = value;
                }
            }
        }

        public int ImageWidth {
            get { return this.Image.Width; }
        }

        public int ImageHeight {
            get { return this.Image.Height; }
        }

        public Bitmap Image {
            get { return bmp; }
            set {
                try {
                    if (value != null) {
                        // No memory leaks here!
                        bool fit = false; //senichkin can't avoid flag
                        if (this.bmp == null || this.bmp.Size != value.Size)
                            fit = true;
                        if (this.bmp != null) {
                            this.bmp.Dispose();
                            this.bmp = null;
                        }
                        this.bmp = value;
                        // Initial rotation adjustments
                        if (rotation != 0) {
                            if (rotation == 180) {
                                this.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                boundingRect = new Rectangle(boundingRect.X, boundingRect.Y, (int)(this.ImageWidth * zoom), (int)(this.ImageHeight * zoom));
                            }
                            else {
                                if (rotation == 90) {
                                    this.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                }
                                else {
                                    if (rotation == 270) {
                                        this.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    }
                                }// Flip the X and Y values
                                boundingRect = new Rectangle(boundingRect.Y, boundingRect.X, (int)(this.ImageHeight * zoom), (int)(this.ImageWidth * zoom));
                            }
                        }
                        else {
                            this.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                            boundingRect = new Rectangle(boundingRect.X, boundingRect.Y, (int)(this.ImageWidth * zoom), (int)(this.ImageHeight * zoom));
                        }
                        bmpPreview = CreatePreviewImage();
                        if (fit)
                            FitToScreen();   //senichkin. to keep zoom & location after frame changed
                    }
                    else {
                        bmpPreview = null;
                    }
                }
                catch(Exception ex) {
                    System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
                }
            }
        }

        public Image PreviewImage {
            get { return bmpPreview; }
        }

        public string ImagePath {
            set {
                try {
                    Image = new Bitmap(value);
                }
                catch {
                    Image = null;
                    System.Windows.Forms.MessageBox.Show("ImageViewer error: Incorrect image format!");
                }
            }
        }

        //public DrawObject(Viewer viewer, Bitmap bmp) {
        //    try {
        //        this.viewer = viewer;
        //        // Initial dragging to false and an Image.
        //        dragging = false;
        //        this.Image = bmp;
        //        this.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
        //        boundingRect = new Rectangle(0, 0, (int)(this.ImageWidth * zoom), (int)(this.ImageHeight * zoom));
        //    }
        //    catch(Exception ex) {
        //        System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
        //    }
        //}

        public DrawObject(ImgViewer viewer) {
            try {
                this.viewer = viewer;
                // Initial dragging to false and No image.
                dragging = false;
                this.bmp = null;
            }
            catch(Exception ex) {
                throw new PluginException("ImageViewer error: " + ex.ToString());
            }
        }
        public void Dispose()
        {
            this.boundingRect = new Rectangle();
            if (this.bmp != null)
                this.bmp.Dispose();
            this.bmp = null;
            if (this.bmpPreview != null)
                this.bmpPreview.Dispose();
            this.bmpPreview = null;
        }

        public void Rotate90() {
            Rotate(90);
        }

        public void Rotate180() {
            Rotate(180);
        }

        public void Rotate270() {
            Rotate(270);
        }

        private void Rotate(int angle) {
            try {
                if(this.Image != null) {
                    int tempWidth = boundingRect.Width;
                    int tempHeight = boundingRect.Height;
                    boundingRect.Width = tempHeight;
                    boundingRect.Height = tempWidth;
                    rotation = (rotation + angle) % 360;
                    switch(angle) {
                        case 90:
                            this.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        case 180:
                            this.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        case 270:
                            this.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                        default:
                            throw new Exception("Incorrect angle");
                    }
                    AvoidOutOfScreen();
                    // No memory leaks here!
                    if(this.bmpPreview != null) {
                        this.bmpPreview.Dispose();
                        this.bmpPreview = null;
                    }
                    this.bmpPreview = CreatePreviewImage();
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        private Bitmap CreatePreviewImage() {
            // 148 && 117 as initial and default size for the preview panel.
            Rectangle previewRect = new Rectangle(0, 0, 148, 117);
            double x_ratio = (double)previewRect.Width / (double)this.BoundingBox.Width;
            double y_ratio = (double)previewRect.Height / (double)this.BoundingBox.Height;
            if((this.BoundingBox.Width <= previewRect.Width) && (this.BoundingBox.Height <= previewRect.Height)) {
                previewRect.Width = this.BoundingBox.Width;
                previewRect.Height = this.BoundingBox.Height;
            }
            else {
                if((x_ratio * this.BoundingBox.Height) < previewRect.Height) {
                    previewRect.Height = Convert.ToInt32(Math.Ceiling(x_ratio * this.BoundingBox.Height));
                    previewRect.Width = previewRect.Width;
                }
                else {
                    previewRect.Width = Convert.ToInt32(Math.Ceiling(y_ratio * this.BoundingBox.Width));
                    previewRect.Height = previewRect.Height;
                }
            }
            Bitmap bmp = new Bitmap(previewRect.Width, previewRect.Height);
            using(Graphics g = Graphics.FromImage(bmp)) {
                if(this.Image != null) {
                    g.DrawImage(this.Image, previewRect);
                }
            }
            return bmp;
        }

        public void ZoomToSelection(Rectangle selection, Point ptPbFull) {
            int x = (selection.X - ptPbFull.X);
            int y = (selection.Y - ptPbFull.Y);
            int width = selection.Width;
            int height = selection.Height;
            // So, where did my selection start on the entire picture?
            int selectedX = (int)((double)(((double)boundingRect.X - ((double)boundingRect.X * 2)) + (double)x) / zoom);
            int selectedY = (int)((double)(((double)boundingRect.Y - ((double)boundingRect.Y * 2)) + (double)y) / zoom);
            int selectedWidth = width;
            int selectedHeight = height;
            // The selection width on the scale of the Original size!
            if(zoom < 1.0 || zoom > 1.0) {
                selectedWidth = Convert.ToInt32((double)width / zoom);
                selectedHeight = Convert.ToInt32((double)height / zoom);
            }
            // What is the highest possible zoomrate?
            double zoomX = ((double)panelWidth / (double)selectedWidth);
            double zoomY = ((double)panelHeight / (double)selectedHeight);
            double newZoom = Math.Min(zoomX, zoomY);
            // Avoid Int32 crashes!
            if(newZoom * 100 < Int32.MaxValue && newZoom * 100 > Int32.MinValue) {
                SetZoom(newZoom);
                selectedWidth = (int)((double)selectedWidth * newZoom);
                selectedHeight = (int)((double)selectedHeight * newZoom);
                // Center the selected area
                int offsetX = 0;
                int offsetY = 0;
                if(selectedWidth < panelWidth) {
                    offsetX = (panelWidth - selectedWidth) / 2;
                }
                if(selectedHeight < panelHeight) {
                    offsetY = (panelHeight - selectedHeight) / 2;
                }
                boundingRect.X = (int)((int)((double)selectedX * newZoom) - ((int)((double)selectedX * newZoom) * 2)) + offsetX;
                boundingRect.Y = (int)((int)((double)selectedY * newZoom) - ((int)((double)selectedY * newZoom) * 2)) + offsetY;
                AvoidOutOfScreen();
            }
        }

        public void JumpToOrigin(int x, int y, int width, int height, int pWidth, int pHeight) {
            try {
                double zoom = (double)boundingRect.Width / (double)width;
                int originX = (int)(x * zoom);
                int originY = (int)(y * zoom);
                originX = originX - (originX * 2);
                originY = originY - (originY * 2);
                boundingRect.X = originX + (pWidth / 2);
                boundingRect.Y = originY + (pHeight / 2);
                AvoidOutOfScreen();
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public void JumpToOrigin(int x, int y, int width, int height) {
            try {
                boundingRect.X = (x - (width / 2)) - ((x - (width / 2)) * 2);
                boundingRect.Y = (y - (height / 2)) - ((y - (height / 2)) * 2);
                AvoidOutOfScreen();
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public Point PointToOrigin(int x, int y, int width, int height) {
            try {
                double zoomX = (double)width / (double)boundingRect.Width;
                double zoomY = (double)height / (double)boundingRect.Height;
                if(width > panelWidth) {
                    int oldX = (boundingRect.X - (boundingRect.X * 2)) + (panelWidth / 2);
                    int oldY = (boundingRect.Y - (boundingRect.Y * 2)) + (panelHeight / 2);
                    int newX = (int)(oldX * zoomX);
                    int newY = (int)(oldY * zoomY);
                    int originX = newX - (panelWidth / 2) - ((newX - (panelWidth / 2)) * 2);
                    int originY = newY - (panelHeight / 2) - ((newY - (panelHeight / 2)) * 2);
                    return new Point(originX, originY);
                }
                else {
                    if(height > panelHeight) {
                        int oldY = (boundingRect.Y - (boundingRect.Y * 2)) + (panelHeight / 2);
                        int newY = (int)(oldY * zoomY);
                        int originY = newY - (panelHeight / 2) - ((newY - (panelHeight / 2)) * 2);
                        return new Point(0, originY);
                    }
                    else {
                        return new Point(0, 0);
                    }
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
                return new Point(0, 0);
            }
        }

        public void ZoomIn() {
            try {
                if(this.Image != null) {
                    // Make sure zoom steps are with 25%
                    double index = 0.25 - (zoom % 0.25);
                    if(index != 0) {
                        zoom += index;
                    }
                    else {
                        zoom += 0.25;
                    }
                    Point p = PointToOrigin(boundingRect.X, boundingRect.Y, (int)(this.ImageWidth * zoom), (int)(this.ImageHeight * zoom));
                    boundingRect = new Rectangle(p.X, p.Y, (int)(this.ImageWidth * zoom), (int)(this.ImageHeight * zoom));
                    AvoidOutOfScreen();
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public void ZoomOut() {
            try {
                if(this.Image != null) {
                    // Make sure zoom steps are with 25% and higher than 0%
                    if(zoom - 0.25 > 0) {
                        if(((zoom - 0.25) % 0.25) != 0) {
                            zoom -= zoom % 0.25;
                        }
                        else {
                            zoom -= 0.25;
                        }
                    }
                    Point p = PointToOrigin(boundingRect.X, boundingRect.Y, (int)(this.ImageWidth * zoom), (int)(this.ImageHeight * zoom));
                    boundingRect = new Rectangle(p.X, p.Y, (int)(this.ImageWidth * zoom), (int)(this.ImageHeight * zoom));
                    AvoidOutOfScreen();
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public void SetPosition(int x, int y) {
            boundingRect.X = -x;
            boundingRect.Y = -y;
        }

        public void SetPositionX(int x) {
            boundingRect.X = -x;
        }

        public void SetPositionY(int y) {
            boundingRect.Y = -y;
        }

        public void SetZoom(double z) {
            try {
                if(this.Image != null) {
                    zoom = z;
                    Point p = PointToOrigin(boundingRect.X, boundingRect.Y, (int)(this.ImageWidth * zoom), (int)(this.ImageHeight * zoom));
                    boundingRect = new Rectangle(p.X, p.Y, (int)(this.ImageWidth * zoom), (int)(this.ImageHeight * zoom));
                    AvoidOutOfScreen();
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public void Scroll(object sender, System.Windows.Forms.MouseEventArgs e) {
            try {
                if(this.Image != null) {
                    if(e.Delta < 0) {
                        ZoomOut();
                    }
                    else {
                        ZoomIn();
                    }
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public void FitToScreen() {
            try {
                if(this.Image != null) {
                    double x_ratio = (double)panelWidth / (double)this.ImageWidth;
                    double y_ratio = (double)panelHeight / (double)this.ImageHeight;
                    if((this.ImageWidth <= panelWidth) && (this.ImageHeight <= panelHeight)) {
                        boundingRect.Width = this.ImageWidth;
                        boundingRect.Height = this.ImageHeight;
                    }
                    else {
                        if((x_ratio * this.ImageHeight) < panelHeight) {
                            boundingRect.Height = Convert.ToInt32(Math.Ceiling(x_ratio * this.ImageHeight));
                            boundingRect.Width = panelWidth;
                        }
                        else {
                            boundingRect.Width = Convert.ToInt32(Math.Ceiling(y_ratio * this.ImageWidth));
                            boundingRect.Height = panelHeight;
                        }
                    }
                    boundingRect.X = 0;
                    boundingRect.Y = 0;
                    zoom = ((double)boundingRect.Width / (double)this.ImageWidth);
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public void AvoidOutOfScreen() {
            try {
                // Am I lined out to the left?
                if(boundingRect.X >= 0) {
                    boundingRect.X = 0;
                }
                else {
                    if((boundingRect.X <= (boundingRect.Width - panelWidth) - ((boundingRect.Width - panelWidth) * 2))) {
                        if((boundingRect.Width - panelWidth) - ((boundingRect.Width - panelWidth) * 2) <= 0) {
                            // I am too far to the left!
                            boundingRect.X = (boundingRect.Width - panelWidth) - ((boundingRect.Width - panelWidth) * 2);
                        }
                        else {
                            // I am too far to the right!
                            boundingRect.X = 0;
                        }
                    }
                }// Am I lined out to the top?
                if(boundingRect.Y >= 0) {
                    boundingRect.Y = 0;
                }
                else {
                    if((boundingRect.Y <= (boundingRect.Height - panelHeight) - ((boundingRect.Height - panelHeight) * 2))) {
                        if((boundingRect.Height - panelHeight) - ((boundingRect.Height - panelHeight) * 2) <= 0) {
                            // I am too far to the top!
                            boundingRect.Y = (boundingRect.Height - panelHeight) - ((boundingRect.Height - panelHeight) * 2);
                        }
                        else {
                            // I am too far to the bottom!
                            boundingRect.Y = 0;
                        }
                    }
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public void Drag(Point pt) {
            try {
                if(this.Image != null) {
                    if(dragging == true) {
                        // Am I dragging it outside of the panel?
                        if((pt.X - dragPoint.X > (boundingRect.Width - panelWidth) - ((boundingRect.Width - panelWidth) * 2)) && (pt.X - dragPoint.X < 0)) {
                            // No, everything is just fine
                            boundingRect.X = pt.X - dragPoint.X;
                        }
                        else {
                            if((pt.X - dragPoint.X > 0)) {
                                // Now don't drag it out of the panel please
                                boundingRect.X = 0;
                            }
                            else {
                                if((pt.X - dragPoint.X < (boundingRect.Width - panelWidth) - ((boundingRect.Width - panelWidth) * 2))) {
                                    // I am dragging it out of my panel. How many pixels do I have left?
                                    if((boundingRect.Width - panelWidth) - ((boundingRect.Width - panelWidth) * 2) <= 0) {
                                        // Make it fit perfectly
                                        boundingRect.X = (boundingRect.Width - panelWidth) - ((boundingRect.Width - panelWidth) * 2);
                                    }
                                }
                            }// Am I dragging it outside of the panel?
                        }
                        if(pt.Y - dragPoint.Y > (boundingRect.Height - panelHeight) - ((boundingRect.Height - panelHeight) * 2) && (pt.Y - dragPoint.Y < 0)) {
                            // No, everything is just fine
                            boundingRect.Y = pt.Y - dragPoint.Y;
                        }
                        else {
                            if((pt.Y - dragPoint.Y > 0)) {
                                // Now don't drag it out of the panel please
                                boundingRect.Y = 0;
                            }
                            else {
                                if(pt.Y - dragPoint.Y < (boundingRect.Height - panelHeight) - ((boundingRect.Height - panelHeight) * 2)) {
                                    // I am dragging it out of my panel. How many pixels do I have left?
                                    if((boundingRect.Height - panelHeight) - ((boundingRect.Height - panelHeight) * 2) <= 0) {
                                        // Make it fit perfectly
                                        boundingRect.Y = (boundingRect.Height - panelHeight) - ((boundingRect.Height - panelHeight) * 2);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public void BeginDrag(Point pt) {
            try {
                if(this.Image != null) {
                    // Initial drag position
                    dragPoint.X = pt.X - boundingRect.X;
                    dragPoint.Y = pt.Y - boundingRect.Y;
                    dragging = true;
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public void EndDrag() {
            try {
                if(this.Image != null) {
                    dragging = false;
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public void Draw(Graphics g) {
            try {
                if(this.bmp != null) {
                    g.DrawImage(this.bmp, boundingRect);
                }
            }
            catch(Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public bool InImage(Point location) {
            return boundingRect.Contains(location);
        }

        public Point GetOriginPoint(Point location) {
            double x = (location.X - boundingRect.X) / zoom;
            double y = (location.Y - boundingRect.Y) / zoom;
            return new Point((int)x, (int)y);
        }
    }
}
