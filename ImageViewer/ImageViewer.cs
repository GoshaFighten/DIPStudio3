using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DIPStudioCore;
using DIPStudioUICore;

namespace ImageViewer
{
    public enum ZoomDirection
    {
        ZoomIn,
        ZoomOut
    }

    public partial class ImgViewer : XtraUserControl
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern short GetKeyState(int key);

        private DrawEngine drawEngine;

        private DrawObject drawing;

        private bool IsKeyPressed(int key)
        {
            bool keyPressed = false;
            short result = GetKeyState(key);
            switch (result)
            {
                case 0:
                    // Not pressed and not toggled
                    keyPressed = false;
                    break;
                case 1:
                    // Not presses but toggled
                    keyPressed = false;
                    break;
                default:
                    // Pressed
                    keyPressed = true;
                    break;
            }
            return keyPressed;
        }

        public ImgViewer()
        {
            // DrawEngine & DrawObject initiralization
            drawEngine = new DrawEngine();
            drawing = new DrawObject(this);
            // Stream to initialize the cursors.
            Stream imgStream = null;
            try
            {
                Assembly a = Assembly.GetExecutingAssembly();
                imgStream = a.GetManifestResourceStream("ImageViewer.Resources.Grab.cur");
                if (imgStream != null)
                {
                    grabCursor = new Cursor(imgStream);
                    imgStream = null;
                }
                imgStream = a.GetManifestResourceStream("ImageViewer.Resources.Drag.cur");
                if (imgStream != null)
                {
                    //dragCursor = new Cursor(imgStream);
                    imgStream = null;
                }
            }
            catch
            {
                // Cursors could not be found
            }
            InitializeComponent();
            InitControl();
        }

        private void DisposeControl()
        {
            // No memory leaks here
            if (drawing != null)
            {
                drawing.Dispose();
            }
            if (drawEngine != null)
            {
                drawEngine.Dispose();
            }
            if (preview != null)
            {
                preview.Dispose();
            }
            pbPanel.Refresh();
            pbFull.Refresh();
        }

        private void OnOverImage(Point point, int color)
        {
            if (OverImage != null)
            {
                OverImage(this, new ImageOverEventArgs(color, point));
            }
        }

        public event EventHandler<ImageOverEventArgs> OverImage;

        #region draw & invalidate

        public int PanelWidth
        {
            get
            {
                if (pbFull != null) {
                    return pbFull.Width - 18;
                }
                else {
                    return 0;
                }
            }
        }

        public int PanelHeight
        {
            get
            {
                if (pbFull != null) {
                    return pbFull.Height - 18;
                }
                else {
                    return 0;
                }
            }
        }

        public void InvalidatePanel()
        {
            this.pbFull.Invalidate();
        }

        public Color MenuColor
        {
            get { return panelMenu.BackColor; }
            set { panelMenu.BackColor = value; }
        }

        public Color MenuPanelColor
        {
            get { return panelMenu.BackColor; }
            set { panelMenu.BackColor = value; }
        }

        public Color BackgroundColor
        {
            get { return pbFull.BackColor; }
            set { pbFull.BackColor = value; }
        }

        private Cursor grabCursor = null;

        private Cursor dragCursor = null;

        private Point ptSelectionStart = new Point();

        private Point ptSelectionEnd = new Point();

        public void InitControl()
        {
            // Make sure panel is DoubleBuffering
            drawEngine.CreateDoubleBuffer(pbFull.CreateGraphics(), PanelWidth, PanelHeight);
            if (!scrollbars) {
                sbHoriz.Visible = false;
                sbVert.Visible = false;
            }
            pbPanel.Properties.NullText = "Нет изображения";
            btnZoomIn.ToolTip = "Увеличить";
            btnZoomOut.ToolTip = "Уменьшить";
            btnFitToScreen.ToolTip = "По размеру окна";
            btnRotate270.ToolTip = "Повернуть против часовой";
            btnRotate90.ToolTip = "Повернуть по часовой";
            btnMode.ToolTip = "Режим";
            btnPrev.ToolTip = "Предыдущее изображение";
            btnNext.ToolTip = "Следующее изображение";
            btnHist.ToolTip = "Гистограмма";
            btnInv.ToolTip = "Инверсия";
        }

        private void ImageViewer_Load(object sender, EventArgs e)
        {
            // Loop for ComboBox Items! Increments by 25%
            for (double z = 0.25; z <= 4.0; z = z + 0.25) {
                cbZoom.Properties.Items.Add(z * 100 + "%");
            }
            cbZoom.SelectedIndex = 3;
        }

        private void ImageViewer_Resize(object sender, EventArgs e)
        {
            InitControl();
            drawing.AvoidOutOfScreen();
            UpdatePanels(true);
        }

        private void pbFull_Paint(object sender, PaintEventArgs e)
        {
            // Can I double buffer?
            if (drawEngine.CanDoubleBuffer()) {
                // Yes I can!
                drawEngine.g.FillRectangle(new SolidBrush(pbFull.BackColor), e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height);
                // Drawing to backBuffer
                drawing.Draw(drawEngine.g);
                // Drawing to Panel
                drawEngine.Render(e.Graphics);
            }
        }

        private void UpdatePanels(bool updatePreview)
        {
            if (drawing.CurrentSize.Width > 0 && drawing.OriginalSize.Width > 0) {
                // scrollbars
                DisplayScrollbars();
                SetScrollbarValues();
                // Make sure panel is up to date
                pbFull.Refresh();
                // Calculate zoom
                double zoom = Math.Round(((double)drawing.CurrentSize.Width / (double)drawing.OriginalSize.Width), 2);
                // Display zoom in percentages
                cbZoom.Text = (int)(zoom * 100) + "%";
                if (updatePreview && drawing.PreviewImage != null && pbPanel.Visible == true) {
                    // No memory leaks here
                    if (preview != null) {
                        preview.Dispose();
                        preview = null;
                    }
                    // New preview
                    preview = new Bitmap(drawing.PreviewImage.Size.Width, drawing.PreviewImage.Size.Height);
                    // Make sure panel is the same size as the bitmap
                    if (pbPanel.Size != drawing.PreviewImage.Size) {
                        pbPanel.Size = drawing.PreviewImage.Size;
                    }
                    // New Graphics from the new bitmap we created (Empty)
                    using (Graphics g = Graphics.FromImage(preview)) {
                        // Draw the image on the bitmap
                        g.DrawImage(drawing.PreviewImage, 0, 0, drawing.PreviewImage.Size.Width, drawing.PreviewImage.Size.Height);
                        double ratioX = (double)drawing.PreviewImage.Size.Width / (double)drawing.CurrentSize.Width;
                        double ratioY = (double)drawing.PreviewImage.Size.Height / (double)drawing.CurrentSize.Height;
                        double boxWidth = PanelWidth * ratioX;
                        double boxHeight = PanelHeight * ratioY;
                        double positionX = ((drawing.BoundingBox.X - (drawing.BoundingBox.X * 2)) * ratioX);
                        double positionY = ((drawing.BoundingBox.Y - (drawing.BoundingBox.Y * 2)) * ratioY);
                        // Making the red pen
                        Pen pen = new Pen(Color.Red, 1);
                        if (boxHeight >= drawing.PreviewImage.Size.Height) {
                            boxHeight = drawing.PreviewImage.Size.Height - 1;
                        }
                        else {
                            if ((boxHeight + positionY) > drawing.PreviewImage.Size.Height) {
                                boxHeight = drawing.PreviewImage.Size.Height - (positionY);
                            }
                        }
                        if (boxWidth >= drawing.PreviewImage.Size.Width) {
                            boxWidth = drawing.PreviewImage.Size.Width - 1;
                        }
                        else {
                            if ((boxWidth + positionX) > drawing.PreviewImage.Size.Width) {
                                boxWidth = drawing.PreviewImage.Size.Width - (positionX);
                            }
                        }// Draw the rectangle on the bitmap
                        g.DrawRectangle(pen, new Rectangle((int)positionX, (int)positionY, (int)boxWidth, (int)boxHeight));
                    }
                    // Display the bitmap
                    pbPanel.Image = preview;
                }
            }
        }

        #endregion

        #region Frame and Images

        public string ImagePath
        {
            set
            {
                drawing.ImagePath = value;
                UpdatePanels(true);
                // scrollbars
                DisplayScrollbars();
                SetScrollbarValues();
            }
        }

        public Bitmap Image
        {
            get { return drawing.Image; }
            set
            {
                drawing.Image = value;
                UpdatePanels(true);
                // scrollbars
                DisplayScrollbars();
                SetScrollbarValues();
            }
        }

        private Bitmap preview;

        Frame fFrame;

        public Frame Frame
        {
            get { return fFrame; }
            set
            {
                if (fFrame == value) {
                    return;
                }
                fFrame = value;
                if (fFrame == null) {
                    Image = null;
                    DisposeControl();
                }
                else
                    Image = (Bitmap)fFrame.Image.Clone();
                OnFrameChanged(fFrame);
            }
        }

        public event EventHandler<FrameChangedEventArgs> FrameChanged;

        protected virtual void OnFrameChanged(Frame frame)
        {
            EventHandler<FrameChangedEventArgs> handler = FrameChanged;
            if (handler != null) {
                handler(this, new FrameChangedEventArgs(frame));
            }
        }

        #endregion

        #region Zoom & rotation

        public double Zoom
        {
            get { return Math.Round(drawing.Zoom * 100, 0); }
            set
            {
                if (value > 0) {
                    // Make it a double!
                    double zoomDouble = (double)value / (double)100;
                    drawing.SetZoom(zoomDouble);
                    UpdatePanels(true);
                    btnZoomIn.Focus();
                }
            }
        }

        public delegate void ImageViewerZoomEventHandler(object sender, ImageViewerZoomEventArgs e);

        public event ImageViewerZoomEventHandler AfterZoom;

        protected virtual void OnZoom(ImageViewerZoomEventArgs e)
        {
            if (AfterZoom != null) {
                AfterZoom(this, e);
            }
        }

        public Size OriginalSize
        {
            get { return drawing.OriginalSize; }
        }

        public Size CurrentSize
        {
            get { return drawing.CurrentSize; }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            drawing.ZoomOut();
            // AfterZoom Event
            if (drawing.Image != null)
            {
                OnZoom(new ImageViewerZoomEventArgs(drawing.Zoom, ZoomDirection.ZoomOut));
            }
            UpdatePanels(true);
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            drawing.ZoomIn();
            // AfterZoom Event
            if (drawing.Image != null)
            {
                OnZoom(new ImageViewerZoomEventArgs(drawing.Zoom, ZoomDirection.ZoomIn));
            }
            UpdatePanels(true);
        }

        private void cbZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int zoomInt = 0;
            // Make sure the percent sign is out of the cbZoom.Text
            int.TryParse(cbZoom.Text.Replace("%", string.Empty), out zoomInt);
            // If zoom is higher than zero
            if (zoomInt <= 0) {
                return;
            }
            // Make it a double!
            double zoom = (double)zoomInt / (double)100;
            double originalZoom = drawing.Zoom;
            if (drawing.Zoom != zoom) {
                drawing.SetZoom(zoom);
                if (drawing.Image != null) {
                    if (zoom > originalZoom) {
                        OnZoom(new ImageViewerZoomEventArgs(drawing.Zoom, ZoomDirection.ZoomIn));
                    }
                    else {
                        OnZoom(new ImageViewerZoomEventArgs(drawing.Zoom, ZoomDirection.ZoomOut));
                    }
                }
                UpdatePanels(true);
            }
        }

        private void cbZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            try {
                // If it's not a digit, delete or backspace then make sure the input is being handled with. (Suppressed)
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Back) {
                    // If enter is pressed apply the entered zoom
                    if (e.KeyChar == (char)Keys.Return) {
                        int zoom = 0;
                        // Make sure the percent sign is out of the cbZoom.Text
                        int.TryParse(cbZoom.Text.Replace("%", string.Empty), out zoom);
                        // If zoom is higher than zero
                        if (zoom > 0) {
                            // Make it a double!
                            double zoomDouble = (double)zoom / (double)100;
                            drawing.SetZoom(zoomDouble);
                            UpdatePanels(true);
                            btnZoomIn.Focus();
                        }
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        public int Rotation
        {
            get { return drawing.Rotation; }
            set
            {
                // Making sure the rotation is 0, 90, 180 or 270 degrees!
                if (value == 90 || value == 180 || value == 270 || value == 0) {
                    drawing.Rotation = value;
                }
            }
        }

        private void btnRotate270_Click(object sender, EventArgs e)
        {
            if (drawing != null) {
                drawing.Rotate270();
                // AfterRotation Event
                OnRotation(new ImageViewerRotationEventArgs(drawing.Rotation));
                UpdatePanels(true);
            }
        }

        private void btnRotate90_Click(object sender, EventArgs e)
        {
            if (drawing != null) {
                drawing.Rotate90();
                // AfterRotation Event
                OnRotation(new ImageViewerRotationEventArgs(drawing.Rotation));
                UpdatePanels(true);
            }
        }

        public void Rotate90()
        {
            if (drawing != null) {
                drawing.Rotate90();
                // AfterRotation Event
                OnRotation(new ImageViewerRotationEventArgs(drawing.Rotation));
                UpdatePanels(true);
            }
        }

        public void Rotate180()
        {
            if (drawing != null) {
                drawing.Rotate180();
                // AfterRotation Event
                OnRotation(new ImageViewerRotationEventArgs(drawing.Rotation));
                UpdatePanels(true);
            }
        }

        public void Rotate270()
        {
            if (drawing != null) {
                drawing.Rotate270();
                // AfterRotation Event
                OnRotation(new ImageViewerRotationEventArgs(drawing.Rotation));
                UpdatePanels(true);
            }
        }

        protected virtual void OnRotation(ImageViewerRotationEventArgs e)
        {
            if (AfterRotation != null) {
                AfterRotation(this, e);
            }
        }

        public delegate void ImageViewerRotationEventHandler(object sender, ImageViewerRotationEventArgs e);

        public event ImageViewerRotationEventHandler AfterRotation;

        #endregion

        #region mouseEvents & Drag and drop

        private bool panelDragging = false;

        private void pbPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (panelDragging == false) {
                drawing.JumpToOrigin(e.X, e.Y, pbPanel.Width, pbPanel.Height, PanelWidth, PanelHeight);
                UpdatePanels(true);
                panelDragging = true;
            }
        }

        private void pbPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (panelDragging) {
                drawing.JumpToOrigin(e.X, e.Y, pbPanel.Width, pbPanel.Height, PanelWidth, PanelHeight);
                UpdatePanels(true);
            }
        }

        private void pbPanel_MouseUp(object sender, MouseEventArgs e)
        {
            panelDragging = false;
        }

        private void pbFull_Click(object sender, EventArgs e)
        {
            FocusOnMe();
        }

        private void ImageViewer_Click(object sender, EventArgs e)
        {
            FocusOnMe();
        }

        private void ImageViewer_MouseWheel(object sender, MouseEventArgs e)
        {
            drawing.Scroll(sender, e);
            if (drawing.Image != null) {
                if (e.Delta < 0) {
                    OnZoom(new ImageViewerZoomEventArgs(drawing.Zoom, ZoomDirection.ZoomOut));
                }
                else {
                    OnZoom(new ImageViewerZoomEventArgs(drawing.Zoom, ZoomDirection.ZoomIn));
                }
            }
            UpdatePanels(true);
        }

        private void FocusOnMe()
        {
            // Do not lose focus! ("Fix" for the Scrolling issue)
            this.Focus();
        }

        private bool shiftSelecting = false;

        private void pbFull_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                // Left Shift or Right Shift pressed? Or is select mode one?
                if (this.IsKeyPressed(0xA0) || this.IsKeyPressed(0xA1) || selectMode == true) {
                    // Fancy cursor
                    pbFull.Cursor = Cursors.Cross;
                    shiftSelecting = true;
                    // Initial seleciton
                    ptSelectionStart.X = e.X;
                    ptSelectionStart.Y = e.Y;
                    // No selection end
                    ptSelectionEnd.X = -1;
                    ptSelectionEnd.Y = -1;
                }
                else {
                    // Start dragging
                    drawing.BeginDrag(new Point(e.X, e.Y));
                    // Fancy cursor
                    if (grabCursor != null) {
                        pbFull.Cursor = grabCursor;
                    }
                }
            }
        }

        private void pbFull_MouseEnter(object sender, EventArgs e)
        {
            if (this.IsKeyPressed(0xA0) || this.IsKeyPressed(0xA1) || selectMode == true) {
                pbFull.Cursor = Cursors.Cross;
            }
            else {
                if (dragCursor != null) {
                    pbFull.Cursor = dragCursor;
                }
            }
        }

        private void pbFull_MouseLeave(object sender, EventArgs e)
        {
            pbFull.Cursor = Cursors.Default;
        }

        private void pbFull_MouseHover(object sender, EventArgs e)
        {
            // Left shift or Right shift!
            if (this.IsKeyPressed(0xA0) || this.IsKeyPressed(0xA1)) {
                // Fancy cursor
                pbFull.Cursor = Cursors.Cross;
            }
            else {
                // Fancy cursor if not dragging
                if (!drawing.IsDragging) {
                    pbFull.Cursor = dragCursor;
                }
            }
        }

        private void pbFull_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            drawing.JumpToOrigin(e.X + (drawing.BoundingBox.X - (drawing.BoundingBox.X * 2)), e.Y + (drawing.BoundingBox.Y - (drawing.BoundingBox.Y * 2)), pbFull.Width, pbFull.Height);
            UpdatePanels(true);
        }

        private void pbFull_MouseUp(object sender, MouseEventArgs e)
        {
            // Am i dragging or selecting?
            if (shiftSelecting == true) {
                // Calculate my selection rectangle
                Rectangle rect = CalculateReversibleRectangle(ptSelectionStart, ptSelectionEnd);
                // Clear the selection rectangle
                ptSelectionEnd.X = -1;
                ptSelectionEnd.Y = -1;
                ptSelectionStart.X = -1;
                ptSelectionStart.Y = -1;
                // Stop selecting
                shiftSelecting = false;
                // Position of the panel to the screen
                Point ptPbFull = PointToScreen(pbFull.Location);
                // Zoom to my selection
                drawing.ZoomToSelection(rect, ptPbFull);
                // Refresh my screen & update my preview panel
                pbFull.Refresh();
                UpdatePanels(true);
            }
            else {
                // Stop dragging and update my panels
                drawing.EndDrag();
                UpdatePanels(true);
                // Fancy cursor
                if (dragCursor != null) {
                    pbFull.Cursor = dragCursor;
                }
            }
        }

        private void pbFull_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing.InImage(e.Location)) {
                Point point = drawing.GetOriginPoint(e.Location);
                OnOverImage(point, Image.GetPixel(point.X, point.Y).R);
            }
            // Am I dragging or selecting?
            if (shiftSelecting == true) {
                // Keep selecting
                ptSelectionEnd.X = e.X;
                ptSelectionEnd.Y = e.Y;
                Rectangle pbFullRect = new Rectangle(0, 0, PanelWidth, PanelHeight);
                // Am I still selecting within my panel?
                if (pbFullRect.Contains(new Point(e.X, e.Y))) {
                    // If so, draw my Rubber Band Rectangle!
                    Rectangle rect = CalculateReversibleRectangle(ptSelectionStart, ptSelectionEnd);
                    DrawReversibleRectangle(rect);
                }
            }
            else {
                // Keep dragging
                drawing.Drag(new Point(e.X, e.Y));
                if (drawing.IsDragging) {
                    UpdatePanels(false);
                }
                else {
                    // I'm not dragging OR selecting
                    // Make sure if left or right shift is pressed to change cursor
                    if (this.IsKeyPressed(0xA0) || this.IsKeyPressed(0xA1) || selectMode == true) {
                        // Fancy Cursor
                        if (pbFull.Cursor != Cursors.Cross) {
                            pbFull.Cursor = Cursors.Cross;
                        }
                    }
                    else {
                        // Fancy Cursor
                        if (pbFull.Cursor != dragCursor) {
                            pbFull.Cursor = dragCursor;
                        }
                    }
                }
            }
        }

        public override bool AllowDrop
        {
            get { return base.AllowDrop; }
            set
            {
                this.pbFull.AllowDrop = value;
                base.AllowDrop = value;
            }
        }

        private void pbFull_DragDrop(object sender, DragEventArgs e)
        {
            try {
                // Get The file(s) you dragged into an array. (We'll just pick the first image anyway)
                string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                for (int f = 0; f < FileList.Length; f++) {
                    // Make sure the file exists!
                    if (System.IO.File.Exists(FileList[f])) {
                        string ext = (System.IO.Path.GetExtension(FileList[f])).ToLower();
                        // Checking the extensions to be Image formats
                        if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".wmf" || ext == ".emf" || ext == ".bmp" || ext == ".png" || ext == ".tif" || ext == ".tiff") {
                            try {
                                // Try to load it into a bitmap
                                //newBmp = Bitmap.FromFile(FileList[f]);
                                this.ImagePath = FileList[f];
                                // If succeeded stop the loop
                                if (this.Image != null) {
                                    break;
                                }
                            }
                            catch {
                                // Not an image?
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        private void pbFull_DragEnter(object sender, DragEventArgs e)
        {
            try {
                if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                    // Drop the file
                    e.Effect = DragDropEffects.Copy;
                }
                else {
                    // I'm not going to accept this unknown format!
                    e.Effect = DragDropEffects.None;
                }
            }
            catch (Exception ex) {
                System.Windows.Forms.MessageBox.Show("ImageViewer error: " + ex.ToString());
            }
        }

        #endregion

        #region scroll

        private bool isScrolling = false;

        private bool scrollbars = false;

        public bool Scrollbars
        {
            get { return scrollbars; }
            set
            {
                scrollbars = value;
                DisplayScrollbars();
                SetScrollbarValues();
            }
        }

        private void DisplayScrollbars()
        {
            if (scrollbars) {
                if (this.Image != null) {
                    int perPercent = this.CurrentSize.Width / 100;
                    if (this.CurrentSize.Width - perPercent > PanelWidth) {
                        this.sbHoriz.Visible = true;
                    }
                    else {
                        this.sbHoriz.Visible = false;
                    }
                    if (this.CurrentSize.Height - perPercent > PanelHeight) {
                        this.sbVert.Visible = true;
                    }
                    else {
                        this.sbVert.Visible = false;
                    }
                    if (this.sbVert.Visible == true && this.sbHoriz.Visible == true) {
                        this.sbVert.Height = PanelHeight;
                        this.sbHoriz.Width = PanelWidth;
                    }
                    else {
                        if (this.sbVert.Visible) {
                            this.sbVert.Height = PanelHeight;
                        }
                        else {
                            this.sbHoriz.Width = PanelWidth;
                        }
                    }
                }
                else {
                    this.sbHoriz.Visible = false;
                    this.sbVert.Visible = false;
                }
            }
            else {
                this.sbHoriz.Visible = false;
                this.sbVert.Visible = false;
            }
        }

        private void SetScrollbarValues()
        {
            if (scrollbars) {
                if (sbHoriz.Visible) {
                    isScrolling = true;
                    double perPercent = (double)this.CurrentSize.Width / 101.0;
                    double totalPercent = (double)PanelWidth / perPercent;
                    sbHoriz.Minimum = 0;
                    sbHoriz.Maximum = 100;
                    sbHoriz.LargeChange = Convert.ToInt32(Math.Round(totalPercent, 0));
                    double value = (double)((-this.drawing.BoundingBox.X) / perPercent);
                    if (value > sbHoriz.Maximum) {
                        sbHoriz.Value = (sbHoriz.Maximum - sbHoriz.LargeChange) + ((sbHoriz.LargeChange > 0) ? 1 : 0);
                    }
                    else {
                        if (value < 0) {
                            sbHoriz.Value = 0;
                        }
                        else {
                            sbHoriz.Value = Convert.ToInt32(Math.Round(value, 0));
                        }
                    }
                    isScrolling = false;
                }
                if (sbVert.Visible) {
                    isScrolling = true;
                    double perPercent = (double)this.CurrentSize.Height / 101.0;
                    double totalPercent = (double)PanelHeight / perPercent;
                    sbVert.Minimum = 0;
                    sbVert.Maximum = 100;
                    sbVert.LargeChange = Convert.ToInt32(Math.Round(totalPercent, 0));
                    double value = (double)((-this.drawing.BoundingBox.Y) / perPercent);
                    if (value > sbVert.Maximum) {
                        sbVert.Value = (sbVert.Maximum - sbVert.LargeChange) + ((sbVert.LargeChange > 0) ? 1 : 0);
                    }
                    else {
                        if (value < 0) {
                            sbVert.Value = 0;
                        }
                        else {
                            sbVert.Value = Convert.ToInt32(Math.Round(value, 0));
                        }
                    }
                    isScrolling = false;
                }
            }
            else {
                sbHoriz.Visible = false;
                sbVert.Visible = false;
            }
        }

        private void sbVert_Scroll(object sender, ScrollEventArgs e)
        {
            if (!isScrolling) {
                double perPercent = (double)this.CurrentSize.Height / 101.0;
                double value = e.NewValue * perPercent;
                this.drawing.SetPositionY(Convert.ToInt32(Math.Round(value, 0)));
                this.drawing.AvoidOutOfScreen();
                pbFull.Invalidate();
                UpdatePanels(true);
            }
        }

        private void sbHoriz_Scroll(object sender, ScrollEventArgs e)
        {
            if (!isScrolling) {
                double perPercent = (double)this.CurrentSize.Width / 101.0;
                double value = e.NewValue * perPercent;
                this.drawing.SetPositionX(Convert.ToInt32(Math.Round(value, 0)));
                this.drawing.AvoidOutOfScreen();
                pbFull.Invalidate();
                UpdatePanels(true);
            }
        }

        #endregion

        #region buttons

        private bool selectMode = false;

        private void btnMode_Click(object sender, EventArgs e)
        {
            if (selectMode == false) {
                selectMode = true;
                this.btnMode.Image = ImageViewer.Properties.Resources.btnDrag;
            }
            else {
                selectMode = false;
                this.btnMode.Image = ImageViewer.Properties.Resources.btnSelect;
            }
        }

        private void btnHist_Click(object sender, EventArgs e)
        {
            if (Image == null) {
                return;
            }
            int[] hist = ImageMath.GetHistogram(Image);
            ZedGraph.ZedGraphControl zedGraphControl = new ZedGraph.ZedGraphControl {
                Dock = DockStyle.Fill
            };
            ZedGraph.GraphPane pane = zedGraphControl.GraphPane;
            ZedGraph.PointPairList list = new ZedGraph.PointPairList();
            for (int i = 0; i < hist.Length; i++) {
                list.Add(i, hist[i]);
            }
            ZedGraph.LineItem curve = pane.AddCurve("Гистограмма", list, Color.Black, ZedGraph.SymbolType.None);
            curve.Label.IsVisible = false;
            int buf = 5;
            pane.XAxis.Scale.Min = 0 - buf;
            pane.XAxis.Scale.Max = ImageMath.ImageDepth - 1 + buf;
            pane.XAxis.Title.Text = "Яркость";
            pane.YAxis.Title.Text = "Количество пикселей";
            pane.Title.IsVisible = false;
            zedGraphControl.AxisChange();
            Bitmap bmp = (Bitmap)btnHist.Image;
            using (BaseForm form = new BaseForm {
                Text = "Гистограмма",
                Size = new Size(900, 500),
                Icon = System.Drawing.Icon.FromHandle(bmp.GetHicon())
            }) {
                form.Controls.Add(zedGraphControl);
                form.ShowDialog();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Frame != null) {
                Frame next = Frame.GetNextElement();
                if (next != null) {
                    Frame = next;
                }
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (Frame != null) {
                Frame prev = Frame.GetPrevElement();
                if (prev != null)
                    Frame = prev;
            }
        }

        private void btnInv_Click(object sender, EventArgs e)
        {
            if (Image == null) {
                return;
            }
            Image = ImageMath.GetInvertedImage(Image);
        }

        private void btnFitToScreen_Click(object sender, EventArgs e)
        {
            drawing.FitToScreen();
            UpdatePanels(true);
        }

        #endregion
     
        private Rectangle CalculateReversibleRectangle(Point ptSelectStart, Point ptSelectEnd)
        {
            Rectangle rect = new Rectangle();
            ptSelectStart = pbFull.PointToScreen(ptSelectStart);
            ptSelectEnd = pbFull.PointToScreen(ptSelectEnd);
            if (ptSelectStart.X < ptSelectEnd.X)
            {
                rect.X = ptSelectStart.X;
                rect.Width = ptSelectEnd.X - ptSelectStart.X;
            }
            else
            {
                rect.X = ptSelectEnd.X;
                rect.Width = ptSelectStart.X - ptSelectEnd.X;
            }
            if (ptSelectStart.Y < ptSelectEnd.Y)
            {
                rect.Y = ptSelectStart.Y;
                rect.Height = ptSelectEnd.Y - ptSelectStart.Y;
            }
            else
            {
                rect.Y = ptSelectEnd.Y;
                rect.Height = ptSelectStart.Y - ptSelectEnd.Y;
            }
            return rect;
        }

        private void DrawReversibleRectangle(Rectangle rect)
        {
            pbFull.Refresh();
            ControlPaint.DrawReversibleFrame(rect, Color.LightGray, FrameStyle.Dashed);
        }


    }

    public class ImageOverEventArgs : EventArgs
    {
        // Fields...
        private int fColor;

        private Point fPoint;

        public Point Point
        {
            get { return fPoint; }
        }

        public int Color
        {
            get { return fColor; }
        }

        /// <summary>
        /// Initializes a new instance of the ImageOverEventArgs class.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="point"></param>
        public ImageOverEventArgs(int color, Point point)
        {
            fColor = color;
            fPoint = point;
        }
    }

    public class FrameChangedEventArgs : EventArgs
    {
        // Fields...
        private Frame fFrame;

        public Frame Frame
        {
            get { return fFrame; }
        }

        /// <summary>
        /// Initializes a new instance of the FrameChangedEventArgs class.
        /// </summary>
        /// <param name="frame"></param>
        public FrameChangedEventArgs(Frame frame)
        {
            fFrame = frame;
        }
    }

    public class ImageViewerRotationEventArgs : EventArgs
    {
        private int rotation;

        public int Rotation
        {
            get { return rotation; }
        }

        public ImageViewerRotationEventArgs(int rotation)
        {
            this.rotation = rotation;
        }
    }

    public class ImageViewerZoomEventArgs : EventArgs
    {
        private int zoom;

        public int Zoom
        {
            get { return zoom; }
        }

        private ZoomDirection inOut;

        public ZoomDirection InOut
        {
            get { return inOut; }
        }

        public ImageViewerZoomEventArgs(double zoom, ZoomDirection inOut)
        {
            this.zoom = Convert.ToInt32(Math.Round((zoom * 100), 0));
            this.inOut = inOut;
        }
    }
}
