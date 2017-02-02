using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections;

namespace ImageConvertor {
    public class Convertor {
        public Bitmap ConvertTo8bppFormat(Bitmap bmpSource) {
            int imageWidth = bmpSource.Width;
            int imageHeight = bmpSource.Height;
            Bitmap bmpDest = null;
            BitmapData bmpDataDest = null;
            BitmapData bmpDataSource = null;
            try {
                // Create new image with 8BPP format
                bmpDest = new Bitmap(imageWidth, imageHeight, PixelFormat.Format8bppIndexed);
                CreateGrayPalette(bmpDest);
                // Lock bitmap in memory
                bmpDataDest = bmpDest.LockBits(new Rectangle(0, 0, imageWidth, imageHeight), ImageLockMode.ReadWrite, bmpDest.PixelFormat);
                bmpDataSource = bmpSource.LockBits(new Rectangle(0, 0, imageWidth, imageHeight), ImageLockMode.ReadOnly, bmpSource.PixelFormat);
                int pixelSize = GetPixelInfoSize(bmpDataSource.PixelFormat);
                byte[] buffer = new byte[imageWidth * imageHeight * pixelSize];
                byte[] destBuffer = new byte[imageWidth * imageHeight];
                // Read all data to buffer
                ReadBmpData(bmpDataSource, buffer, imageHeight);
                // Get color indexes
                MatchColors(buffer, destBuffer, pixelSize);
                // Copy all colors to destination bitmaps
                WriteBmpData(bmpDataDest, destBuffer, imageWidth, imageHeight);
                return bmpDest;
            }
            finally {
                if (bmpDest != null) {
                    bmpDest.UnlockBits(bmpDataDest);
                }
                if (bmpSource != null) {
                    bmpSource.UnlockBits(bmpDataSource);
                }
            }
        }

        private static void CreateGrayPalette(Bitmap bmpDest) {
            ColorPalette palette = bmpDest.Palette;
            for (int i = 0; i < palette.Entries.Length; i++) {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            bmpDest.Palette = palette;
        }

        private void MatchColors(byte[] buffer, byte[] destBuffer, int pixelSize) {
            int length = destBuffer.Length;
            byte[] temp = new byte[pixelSize];
            for (int i = 0; i < length; i++) {
                Array.Copy(buffer, i * pixelSize, temp, 0, pixelSize);
                destBuffer[i] = temp[2];
            }
        }

        private int GetPixelInfoSize(PixelFormat format) {
            switch (format) {
                case PixelFormat.Format24bppRgb: {
                        return 3;
                    }
                case PixelFormat.Format32bppArgb: {
                        return 4;
                    }
                case PixelFormat.Format32bppRgb: {
                        return 4;
                    }
                default: {
                        throw new ApplicationException("Only 24bit colors supported now");
                    }
            }
        }

        private void ReadBmpData(BitmapData bmpDataSource, byte[] buffer, int height) {
            int addrStart = bmpDataSource.Scan0.ToInt32();
            for (int i = 0; i < height; i++) {
                IntPtr realByteAddr = new IntPtr(addrStart + Convert.ToInt32(i * bmpDataSource.Stride));
                Marshal.Copy(realByteAddr, buffer, (int)(i * bmpDataSource.Stride), bmpDataSource.Stride);
            }
        }

        private void WriteBmpData(BitmapData bmpDataDest, byte[] destBuffer, int imageWidth, int imageHeight) {
            int addrStart = bmpDataDest.Scan0.ToInt32();
            for (int i = 0; i < imageHeight; i++) {
                IntPtr realByteAddr = new IntPtr(addrStart + Convert.ToInt32(i * bmpDataDest.Stride));
                Marshal.Copy(destBuffer, i * imageWidth, realByteAddr, imageWidth);
            }
        }
    }
}
