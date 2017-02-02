using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioCore.Data;
namespace BackgroundAnalyze {
    public class BackgroundAnalyze : Plugin<BackgroundAnalyzeSettings, BackgroundAnalyzeUserControl> {
        public BackgroundAnalyze()
            : base(ProjectKind.Selection, "Анализ фона", "[АФ]")
        {

        }
        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 450;
            frm.Height = 320;
        }
        protected override void Run(int t, int index, int tFinish)
        {
            DIPApplication application = DIPApplication.GetInstance();
            Series sourceSeries = application.GetSeriesByName(this.PluginSettings.InputSeries);
            if (sourceSeries == null) {
                throw new PluginException(string.Format("Series {0} does not exist", this.PluginSettings.InputSeries));
            }

            Frame sourceFrame = sourceSeries[t];

            Table table = application.GetTableByNameOrCreateNew(ShortName + PluginSettings.ResultName, sourceSeries.Name);
            DataFrame frame = new DataFrame(t);
            frame.Objects.Pattern = BackgroundData.GetPattern();
            frame.Name = sourceFrame.Name;
            table.Add(frame);

            Bitmap sourceImage = sourceFrame.Image;
            int sourceWidth = this.PluginSettings.StrobWidth;
            int sourceHeight = this.PluginSettings.StrobHeight;
            int windowWidth = this.PluginSettings.WindowWidth;
            int windowHeight = this.PluginSettings.WindowHeight;
            int stepX = this.PluginSettings.StepX;
            int stepY = this.PluginSettings.StepY;

            Rectangle strobRectangle = new Rectangle(this.PluginSettings.StrobX, this.PluginSettings.StrobY, this.PluginSettings.StrobWidth, this.PluginSettings.StrobHeight);

            BitmapData bmpData = sourceImage.LockBits(new Rectangle(Point.Empty, sourceImage.Size), ImageLockMode.ReadWrite, sourceImage.PixelFormat);
            try 
            {
                for (int i = strobRectangle.Left; i < strobRectangle.Right; i += stepX) 
                    for (int j = strobRectangle.Top; j < strobRectangle.Bottom; j += stepY) 
                    {
                        Rectangle windowRectangle = new Rectangle(i, j, i + windowWidth < strobRectangle.Right ? windowWidth : strobRectangle.Right - i, j + windowHeight < strobRectangle.Bottom ? windowHeight : strobRectangle.Bottom - j);
                        Save(windowRectangle, bmpData);
                        if (this.PluginSettings.Scan) 
                            frame.Objects.Add(ProcessWindowScan(windowRectangle, bmpData));
                        else {
                            if (this.PluginSettings.Three) 
                                frame.Objects.Add(ProcessWindowThree(windowRectangle, bmpData));
                            else 
                                frame.Objects.Add(ProcessWindow(windowRectangle, bmpData));
                        }
                    }
                DrawRectangle(strobRectangle, bmpData, 127);
            }
            finally 
            {
                sourceImage.UnlockBits(bmpData);
            }
            application.AddTable(table);
        }

        private void Save(Rectangle windowRectangle, BitmapData bmpData) {
            string folder = @"D:\User\Егоров\Test\";
            string imgPath = folder + string.Format("{0} - {1}.bmp", windowRectangle.Left, windowRectangle.Top);
            string tablePath = folder + string.Format("{0} - {1}.txt", windowRectangle.Left, windowRectangle.Top);
            Bitmap bmp = new Bitmap(windowRectangle.Width, windowRectangle.Height, PixelFormat.Format8bppIndexed);
            ColorPalette palette = bmp.Palette;
            for (int i = 0; i < palette.Entries.Length; i++) 
                palette.Entries[i] = Color.FromArgb(i, i, i);
            bmp.Palette = palette;
            var lockBits = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            var stream = System.IO.File.Open(tablePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
            
            for (int i = 0; i < windowRectangle.Height; i++) 
            {
                byte[] row = GetRowData(windowRectangle, bmpData, i);
                stream.Write(row, 0, row.Length);
                int offset = lockBits.Stride * i;
                for (int j = 0; j < row.Length; j++) 
                {
                    System.Runtime.InteropServices.Marshal.WriteByte(lockBits.Scan0, offset, row[j]);
                    offset += sizeof(byte);
                }
                

            }
            bmp.UnlockBits(lockBits);
            bmp.Save(imgPath);
            stream.Dispose();
        }

        private BackgroundData ProcessWindowThree(Rectangle windowRectangle, BitmapData bmpData) {
            int firstRow = windowRectangle.Height / 3;
            int secondRow = firstRow * 2;
            int n = secondRow - firstRow;
            List<byte> m1 = new List<byte>();
            for (int i = 0; i < n; i++) 
            {
                byte[] rowData = GetRowData(windowRectangle, bmpData, i);
                m1.AddRange(rowData);
            }
            List<byte> m2 = new List<byte>();
            for (int i = 0; i < n; i++) 
            {
                byte[] rowData = GetRowData(windowRectangle, bmpData, i + firstRow);
                m2.AddRange(rowData);
            }
            List<byte> m3 = new List<byte>();
            for (int i = 0; i < n; i++) 
            {
                byte[] rowData = GetRowData(windowRectangle, bmpData, i + secondRow);
                m3.AddRange(rowData);
            }
            double k1 = CCalc(m1.ToArray(), m2.ToArray());
            double k2 = CCalc(m1.ToArray(), m3.ToArray());
            double k3 = CCalc(m2.ToArray(), m3.ToArray());
            double k = (k1 + k2 + k3) / 3;
            BackgroundData data = new BackgroundData() { Name = string.Format("{0} - {1}", windowRectangle.Left, windowRectangle.Top), K = k, Rect = windowRectangle, R = n, S = k > PluginSettings.S, X = windowRectangle.Left + windowRectangle.Width / 2, Y = windowRectangle.Top + windowRectangle.Height / 2 };
            byte color = data.S ? (byte)255 : (byte)0;
            DrawRectangle(windowRectangle, bmpData, color);
            return data;
        }

        private BackgroundData ProcessWindow(Rectangle windowRectangle, BitmapData bmpData) 
        {
            int firstRow = windowRectangle.Height / 3;
            int secondRow = windowRectangle.Height * 2 / 3;
            byte[] firstRowData = GetRowData(windowRectangle, bmpData, firstRow);
            byte[] secondRowData = GetRowData(windowRectangle, bmpData, secondRow);
            double k = CCalc(firstRowData, secondRowData);
            BackgroundData data = new BackgroundData() { Name = string.Format("{0} - {1}", windowRectangle.Left, windowRectangle.Top), K = k, Rect = windowRectangle, R = secondRow - firstRow, S = k > PluginSettings.S, X = windowRectangle.Left + windowRectangle.Width / 2, Y = windowRectangle.Top + windowRectangle.Height / 2 };
            data.AddProperty<List<byte>>("FirstRow");
            data.SetPropertyValue<List<byte>>("FirstRow", firstRowData.ToList());
            data.AddProperty<List<byte>>("SecondRow");
            data.SetPropertyValue<List<byte>>("SecondRow", secondRowData.ToList());
            DrawBaseLine(firstRow, windowRectangle, bmpData);
            DrawBaseLine(secondRow, windowRectangle, bmpData);
            byte color = data.S ? (byte)255 : (byte)0;
            DrawRectangle(windowRectangle, bmpData, color);
            return data;
        }

        private BackgroundData ProcessWindowScan(Rectangle windowRectangle, BitmapData bmpData) 
        {
            int baseLine = windowRectangle.Height / 2;
            double k = 0.0;
            List<Tuple<double, bool, List<byte>, List<byte>>> cache = new List<Tuple<double, bool, List<byte>, List<byte>>>();
            for (int i = 1; i <= baseLine; i++) 
            {
                int firstRow = baseLine - i;
                int secondRow = baseLine + i + (windowRectangle.Height % 2 == 0 ? -1 : 0);
                byte[] firstRowData = GetRowData(windowRectangle, bmpData, firstRow);
                byte[] secondRowData = GetRowData(windowRectangle, bmpData, secondRow);
                double result = CCalc(firstRowData, secondRowData);
                cache.Add(new Tuple<double, bool, List<byte>, List<byte>>(result, result > PluginSettings.S, firstRowData.ToList(), secondRowData.ToList()));
                if (result > PluginSettings.S) 
                {
                    DrawBaseLine(firstRow, windowRectangle, bmpData);
                    DrawBaseLine(secondRow, windowRectangle, bmpData);
                }
                k += result;
            }
            k = k / baseLine;
            BackgroundData data = new BackgroundData() { Name = string.Format("{0} - {1}", windowRectangle.Left, windowRectangle.Top), K = k, Rect = windowRectangle, R = -1, S = k > PluginSettings.S, X = windowRectangle.Left + windowRectangle.Width / 2, Y = windowRectangle.Top + windowRectangle.Height / 2 };
            data.AddProperty<List<Tuple<double, bool, List<byte>, List<byte>>>>("Data");
            data.SetPropertyValue<List<Tuple<double, bool, List<byte>, List<byte>>>>("Data", cache);
            byte color = data.S ? (byte)255 : (byte)0;
            DrawRectangle(windowRectangle, bmpData, color);
            return data;
        }

        private byte[] GetRowData(Rectangle windowRectangle, BitmapData bmpData, int row) 
        {
            byte[] rowData = new byte[windowRectangle.Width];
            int offset = bmpData.Stride * (row + windowRectangle.Top) + windowRectangle.Left;
            for (int x = 0; x < windowRectangle.Width; x++) {
                rowData[x] = System.Runtime.InteropServices.Marshal.ReadByte(bmpData.Scan0, offset);
                offset += sizeof(byte);
            }
            return rowData;
        }

        private void DrawBaseLine(int row, Rectangle windowRectangle, BitmapData bmpData) 
        {
            int off = bmpData.Stride * (windowRectangle.Top + row) + windowRectangle.Left;
            DrawHorizontalLine(off, windowRectangle.Width, bmpData, 255);
        }

        private void DrawRectangle(Rectangle rect, BitmapData bmpData, byte color) 
        {
            DrawTopLine(rect, bmpData, color);
            DrawBottomLine(rect, bmpData, color);
            DrawLeftLine(rect, bmpData, color);
            DrawRightLine(rect, bmpData, color);
        }

        private void DrawRightLine(Rectangle windowRectangle, BitmapData bmpData, byte color) 
        {
            int off = bmpData.Stride * windowRectangle.Top + windowRectangle.Right - 1;
            DrawVerticalLine(off, windowRectangle.Height, bmpData, color);
        }

        private void DrawLeftLine(Rectangle windowRectangle, BitmapData bmpData, byte color) 
        {
            int off = bmpData.Stride * windowRectangle.Top + windowRectangle.Left;
            DrawVerticalLine(off, windowRectangle.Height, bmpData, color);
        }

        private void DrawTopLine(Rectangle windowRectangle, BitmapData bmpData, byte color) 
        {
            int off = bmpData.Stride * windowRectangle.Top + windowRectangle.Left;
            DrawHorizontalLine(off, windowRectangle.Width, bmpData, color);
        }

        private void DrawBottomLine(Rectangle windowRectangle, BitmapData bmpData, byte color) 
        {
            int off = bmpData.Stride * (windowRectangle.Bottom - 1) + windowRectangle.Left;
            DrawHorizontalLine(off, windowRectangle.Width, bmpData, color);
        }

        private void DrawHorizontalLine(int start, int width, BitmapData bmpData, byte color) 
        {
            int offset = start;
            for (int x = 0; x < width; x++) {
                System.Runtime.InteropServices.Marshal.WriteByte(bmpData.Scan0, offset, color);
                offset += sizeof(byte);
            }
        }

        private void DrawVerticalLine(int start, int height, BitmapData bmpData, byte color) 
        {
            int offset = start;
            for (int x = 0; x < height; x++) {
                System.Runtime.InteropServices.Marshal.WriteByte(bmpData.Scan0, offset, color);
                offset += bmpData.Stride;
            }
        }

        private double CCalc(byte[] first, byte[] second) {
            if (first.Length != second.Length)
                throw new PluginException("Массивы имеют разный размер");
            double sum1 = first.Sum(b => (double)b);
            double sum2 = second.Sum(b => (double)b);
            double m1 = sum1 / first.Length;
            double m2 = sum2 / second.Length;
            double d1 = first.Sum(b => (b - m1) * (b - m1)) / (first.Length - 1);
            double d2 = second.Sum(b => (b - m2) * (b - m2)) / (second.Length - 1);
            double g1 = Math.Sqrt(d1);
            double g2 = Math.Sqrt(d2);
            double result = 0;
            for (int i = 0; i < first.Length; i++) {
                result += (first[i] - m1) * (second[i] - m2);
            }
            result = result / g1 / g2 / first.Length;
            return result;
        }

        public class BackgroundData : ObjectWithProperties 
        {
            const string STR_Name = "Name";
            const string STR_K = "K";
            const string STR_Rect = "Rect";
            const string STR_R = "R";
            const string STR_S = "S";
            const string STR_X = "X";
            const string STR_Y = "Y";

            static IObjectWithProperties pattern;

            public static IObjectWithProperties GetPattern() {
                return pattern;
            }

            static BackgroundData() {
                pattern = new ObjectWithProperties();
                pattern.Properties.Add(STR_Name, typeof(string));
                pattern.Properties.Add(STR_K, typeof(double));
                pattern.Properties.Add(STR_Rect, typeof(Rectangle));
                pattern.Properties.Add(STR_R, typeof(int));
                pattern.Properties.Add(STR_S, typeof(bool));
                pattern.Properties.Add(STR_X, typeof(double));
                pattern.Properties.Add(STR_Y, typeof(double));
            }

            public BackgroundData() {
                properties.Add(STR_Name, string.Empty);
                properties.Add(STR_K, double.MinValue);
                properties.Add(STR_Rect, Rectangle.Empty);
                properties.Add(STR_R, int.MinValue);
                properties.Add(STR_S, false);
                properties.Add(STR_X, double.MinValue);
                properties.Add(STR_Y, double.MinValue);
            }

            public string Name {
                get { return (string)properties[STR_Name]; }
                set { properties[STR_Name] = value; }
            }

            public double K {
                get { return (double)properties[STR_K]; }
                set { properties[STR_K] = value; }
            }

            public Rectangle Rect {
                get { return (Rectangle)properties[STR_Rect]; }
                set { properties[STR_Rect] = value; }
            }

            public int R {
                get { return (int)properties[STR_R]; }
                set { properties[STR_R] = value; }
            }

            public bool S {
                get { return (bool)properties[STR_S]; }
                set { properties[STR_S] = value; }
            }

            public double X {
                get { return (double)properties[STR_X]; }
                set { properties[STR_X] = value; }
            }

            public double Y {
                get { return (double)properties[STR_Y]; }
                set { properties[STR_Y] = value; }
            }

            public void AddProperty<T>(string name) {
                if (!pattern.Properties.ContainsKey(name))
                    pattern.Properties.Add(name, typeof(T));
                properties.Add(name, default(T));
            }

            public T GetPropertyValue<T>(string name) {
                return (T)properties[name];
            }

            public void SetPropertyValue<T>(string name, T value) {
                properties[name] = value;
            }
        }

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }
}
