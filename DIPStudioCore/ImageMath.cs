using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace DIPStudioCore {
    public static class ImageMath {
        public const int ImageDepth = 256;

        public const int ThumbnailSize = 96;

        public static int[] GetHistogram(Bitmap image) {
            int[] hist = new int[ImageDepth];
            hist.Initialize();
            for(int i = 0; i < image.Width; i++) {
                for(int j = 0; j < image.Height; j++) {
                    hist[image.GetPixel(i, j).R]++;
                }
            }
            return hist;
        }

        public static Bitmap GetInvertedImage(Bitmap image) {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(image.Width, image.Height);
            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);
            // create the negative color matrix
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] { new float[] { -1, 0, 0, 0, 0
                }, new float[] { 0, -1, 0, 0, 0
                }, new float[] { 0, 0, -1, 0, 0
                }, new float[] { 0, 0, 0, 1, 0
                }, new float[] { 1, 1, 1, 0, 1
                }
            });
            // create some image attributes
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        /// <summary>
        /// Перевод изображения в матрицу цвета
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static int[,] ImgToInt(Bitmap image) {
            return ImgToInt(image, new Rectangle(new Point(0,0), image.Size));
        }

        public static int[,] ImgToInt(Bitmap image, Rectangle strob)
        {
            int w = strob.Width;
            int h = strob.Height;
            int[,] img = new int[h, w];
            BitmapData bmpData = image.LockBits(strob, ImageLockMode.ReadOnly, image.PixelFormat);
            try {
                int offset = 0;
                for (int i = 0; i < h; i++) {
                    offset = sizeof(byte) * bmpData.Stride * i;
                    for (int j = 0; j < w; j++) {
                        img[i, j] = System.Runtime.InteropServices.Marshal.ReadByte(bmpData.Scan0, offset);
                        offset += sizeof(byte);
                    }
                }
            }
            finally {
                image.UnlockBits(bmpData);
            }
            return img;
        }

        public static ColorPalette GetGrayscalePalette(ColorPalette palette)
        {
            for (int i = 0; i < 256; i++)
                palette.Entries[i] = Color.FromArgb(i, i, i);

            return palette;
        }
        /// <summary>
        /// обработка изображений согласно передаваемому делегату
        /// </summary>
        /// <param name="sourceFrame">обрабатываемое изображение</param>
        /// <param name="func">делегат обработки</param>
        /// <param name="lineLength">длинна обрабатываемой строки изображения</param>
        /// <param name="time">время кадра</param>
        /// <returns>Кадр после обработки</returns>
        public static Frame GetProcess(Frame sourceFrame, Func<int, byte[], byte[]> func, int lineLength, int time)
        {
            int h = sourceFrame.Image.Height;
            int w = sourceFrame.Image.Width;
            return GetProcess(sourceFrame, func, new Rectangle(0, 0, w, h), lineLength, time);
        }

        /// <summary>
        /// обработка изображений согласно передаваемому делегату       
        /// </summary>
        /// <param name="sourceFrame">обрабатываемое изображение</param>
        /// <param name="func">делегат обработки</param>
        /// <param name="newFrameRect">обрезка изображения по данному прямоугольнику</param>
        /// <param name="lineLength">длинна обрабатываемой строки изображения</param>
        /// <param name="time">время кадра</param>
        /// <returns>Кадр после обработки</returns>
  
        public static Frame GetProcess(Frame sourceFrame, Func<int, byte[], byte[]> func, Rectangle newFrameRect, int lineLength, int time)
        {
            int bytesPerPixel = Image.GetPixelFormatSize(sourceFrame.Image.PixelFormat) / 8;
            int oldHeight = sourceFrame.Image.Height;
            int oldWidth = sourceFrame.Image.Width;
            Rectangle oldRect = new Rectangle(0, 0, oldWidth, oldHeight);

            Frame resultFrame = new Frame(sourceFrame.Time, newFrameRect.Width, newFrameRect.Height, sourceFrame.Image.PixelFormat);
            resultFrame.Name = sourceFrame.Name;
            BitmapData sourceBmpData = new BitmapData();
            BitmapData resultBmpData = new BitmapData();
            if (bytesPerPixel == 1)
                resultFrame.Image.Palette = GetGrayscalePalette(resultFrame.Image.Palette);
            try {
                sourceBmpData = sourceFrame.Image.LockBits(new Rectangle(0, 0, oldWidth, oldHeight), ImageLockMode.ReadOnly, sourceFrame.Image.PixelFormat);
                resultBmpData = resultFrame.Image.LockBits(new Rectangle(0, 0, newFrameRect.Width, newFrameRect.Height), ImageLockMode.WriteOnly, resultFrame.Image.PixelFormat);
                if (resultBmpData.Stride != sourceBmpData.Stride)
                    lineLength = resultBmpData.Stride;
                int size = resultBmpData.Stride * newFrameRect.Height / lineLength;

                byte[] pixel = new byte[lineLength];
                for (int i = 0; i < size; i++) {
                    if (resultBmpData.Stride != sourceBmpData.Stride) {
                        pixel = new byte[lineLength];
                        System.Runtime.InteropServices.Marshal.Copy(sourceBmpData.Scan0 + (i + newFrameRect.Y) * sourceBmpData.Stride + newFrameRect.X * bytesPerPixel, pixel, 0, resultBmpData.Stride);
                    }
                    else
                        System.Runtime.InteropServices.Marshal.Copy(sourceBmpData.Scan0 + i * lineLength, pixel, 0, lineLength);
                    pixel = func(time, pixel);
                    System.Runtime.InteropServices.Marshal.Copy(pixel, 0, resultBmpData.Scan0 + i * lineLength, lineLength);
                }
            }
            finally {
                sourceFrame.Image.UnlockBits(sourceBmpData);
                resultFrame.Image.UnlockBits(resultBmpData);
            }
    
            return resultFrame;
        }
        /// <summary>
        /// Вызывается, если для работы с изображением в каждой точке требуется учесть окружающие точки
        /// </summary>
        /// <param name="sourceFrame">изображение - источник</param>
        /// <param name="func">делегат обработки</param>
        /// <param name="zeros">радиус окружающих точек (в пикселях)</param>
        /// <param name="fillExpand">при обращении к точке за пределами источника вернется это значение</param>
        /// <returns>Кадр после обработки</returns>
        public static Frame GetExpandProcess(Frame sourceFrame, Func<byte[], byte> func, int zeros, byte fillExpand)
        {
            int h = sourceFrame.Image.Height;
            int w = sourceFrame.Image.Width;

            Frame resultFrame = new Frame(sourceFrame.Time, w, h, sourceFrame.Image.PixelFormat);
            resultFrame.Image.Palette = GetGrayscalePalette(resultFrame.Image.Palette);
            resultFrame.Name = sourceFrame.Name;
            BitmapData sourceBmpData = new BitmapData();
            BitmapData resultBmpData = new BitmapData();
            try {
                sourceBmpData = sourceFrame.Image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, sourceFrame.Image.PixelFormat);
                resultBmpData = resultFrame.Image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, resultFrame.Image.PixelFormat);

                byte[,] imgExpanded = new byte[h + 2 * zeros, w + 2 * zeros];
                for (int i = 0; i < (h + 2 * zeros); i++)
                    for (int j = 0; j < (w + 2 * zeros); j++)
                        if (i >= zeros && i < (h + zeros) && j >= zeros && j < (w + zeros))
                            imgExpanded[i, j] = System.Runtime.InteropServices.Marshal.ReadByte(sourceBmpData.Scan0 + (j - zeros) + (i - zeros) * w);
                        else
                            imgExpanded[i, j] = fillExpand;

                for (int i = zeros; i < (h + zeros); i++)
                    for (int j = zeros; j < (w + zeros); j++) {
                        byte[] mask = new byte[zeros * zeros * 4];
                        int p = 0;
                        for (int x = -zeros; x < zeros; x++)
                            for (int y = -zeros; y < zeros; y++)
                                    mask[p++] = imgExpanded[i + x, j + y];
                        System.Runtime.InteropServices.Marshal.WriteByte(resultBmpData.Scan0 + (j - zeros) + (i - zeros) * w, func(mask));
                    }
            }
            finally {
                sourceFrame.Image.UnlockBits(sourceBmpData);
                resultFrame.Image.UnlockBits(resultBmpData);
            }
            return resultFrame; ;
        }
        /// <summary>
        /// Попиксельное сложение двух изображений
        /// </summary>
        /// <param name="sourceFrame1"></param>
        /// <param name="sourceFrame2"></param>
        /// <returns></returns>
        public static Frame GetSumFrame(Frame sourceFrame1, Frame sourceFrame2)
        {
            int w = Math.Min(sourceFrame1.Image.Width, sourceFrame2.Image.Width);
            int h = Math.Min(sourceFrame1.Image.Height, sourceFrame2.Image.Height);
            Frame resultFrame = new Frame(sourceFrame1.Time, w, h, PixelFormat.Format8bppIndexed);
            ColorPalette palette = GetGrayscalePalette(resultFrame.Image.Palette);
            BitmapData sourceBmpData1 = new BitmapData(); ;
            BitmapData sourceBmpData2 = new BitmapData(); ;
            BitmapData resultBmpData = new BitmapData(); ;
            try {
                sourceBmpData1 = sourceFrame1.Image.LockBits(new Rectangle(0, 0, sourceFrame1.Image.Width, sourceFrame1.Image.Height), ImageLockMode.ReadOnly, sourceFrame1.Image.PixelFormat);
                sourceBmpData2 = sourceFrame2.Image.LockBits(new Rectangle(0, 0, sourceFrame2.Image.Width, sourceFrame2.Image.Height), ImageLockMode.ReadOnly, sourceFrame2.Image.PixelFormat);
                resultBmpData = resultFrame.Image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, resultFrame.Image.PixelFormat);
                byte argb1;
                byte argb2;
                int offset;
                int offset1;
                int offset2;
                int byteSize = sizeof(byte);
                for (int i = 0; i < w; i++) {
                    offset = resultBmpData.Stride * i;
                    offset1 = sourceBmpData1.Stride * i;
                    offset2 = sourceBmpData2.Stride * i;
                    for (int j = 0; j < h; j++) {
                        argb1 = System.Runtime.InteropServices.Marshal.ReadByte(sourceBmpData1.Scan0, offset1);
                        argb2 = System.Runtime.InteropServices.Marshal.ReadByte(sourceBmpData2.Scan0, offset2);
                        byte r = (byte)((argb1 + argb2) / 2);
                        System.Runtime.InteropServices.Marshal.WriteByte(resultBmpData.Scan0, offset, r);
                        offset += byteSize;
                        offset1 += byteSize;
                        offset2 += byteSize;
                    }
                }
            }
            finally {
                sourceFrame1.Image.UnlockBits(sourceBmpData1);
                sourceFrame2.Image.UnlockBits(sourceBmpData2);
                resultFrame.Image.UnlockBits(resultBmpData);
            }
            return resultFrame;
        }

        public static double Mean(int[,] array)
        {
            double s = 0;
            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++) {
                    s += array[i, j];
                }
            }
            return s / array.GetLength(0) / array.GetLength(1);
        }

        public static double Mean(int[,] array, Rectangle strob, bool inside)
        {
            double s = 0;
            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++) {
                    if (inside && strob.Contains(i, j)) {
                        s += array[i, j];
                    }
                    else if (!inside && !strob.Contains(i, j)) {
                        s += array[i, j];
                    }
                }
            }
            int totalS = array.GetLength(0) * array.GetLength(1);
            int strobS = strob.Width * strob.Height;
            int square = inside ? strobS : totalS - strobS;
            return s / square;
        }

        public static double Mean(int[] array)
        {
            double s = 0;
            for (int i = 0; i < array.Length; i++) {
                s += array[i];
            }
            return s / array.Length;
        }

        public static double Std(int[,] array)
        {
            var m = Mean(array);
            double s = 0;
            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++) {
                    s += (array[i, j] - m) * (array[i, j] - m);
                }
            }
            return Math.Sqrt(s / (array.GetLength(0) * array.GetLength(1) - 1));
        }

        public static double Std(int[,] array, Rectangle strob, bool inside)
        {
            var m = Mean(array, strob, inside);
            double s = 0;
            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++) {
                    if (inside) {
                        if (strob.Contains(i, j)) {
                            s += (array[i, j] - m) * (array[i, j] - m);
                        }
                    }
                    else {
                        if (!strob.Contains(i, j)) {
                            s += (array[i, j] - m) * (array[i, j] - m);
                        }
                    }                    
                }
            }
            int totalS = array.GetLength(0) * array.GetLength(1);
            int strobS = strob.Width * strob.Height;
            int square = inside ? strobS : totalS - strobS;
            return Math.Sqrt(s / (square - 1));
        }

        public static double Std(int[] array)
        {
            var m = Mean(array);
            double s = 0;
            for (int i = 0; i < array.Length; i++) {
                s += (array[i] - m) * (array[i] - m);
            }
            return Math.Sqrt(s / (array.Length - 1));
        }
    }
}
