using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CorrelationAnalysis
{
    internal class CaptureClass
     {
        public int ystrobSP, xstrobSP, strobySP, strobxSP;
        private int znachmetoda;
        private byte YY;
        private double mo, dis;
        internal byte[,] A;
        public static bool Perezapis;
        /// <summary> 
        /// Класс для захват цели.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="metodBin"></param>
        /// <param name="Uroven"></param>
        /// <param name="constt"></param>
        /// <param name="zah"></param>
        
        public CaptureClass(Bitmap image, bool metodBin, double Uroven, int constt, Areas zah) {
            MassivData(image);
            MatOshidandDiaspersia(zah);
            if (metodBin) { znachmetoda = (int)(mo + 3 * Math.Sqrt(dis) + constt); }
            else { znachmetoda = (int)(Uroven * YY); }
           // MedianFiltre();
            Zahvat(zah);
            RatioSizes();
        }
        private void MassivData(Bitmap im)  {
            A = new byte[im.Height, im.Width];
              for (int i = 0; i < im.Height; i++)
                for (int j = 0; j < im.Width; j++) {
                    Color pixel = im.GetPixel(j, i);
                    A[i, j] = pixel.R;
                }
        }
        private void MatOshidandDiaspersia(Areas zah) {
            mo = dis = 0; YY = 0;
            for (int i = zah.yop; i < zah.yop + zah.opy; i++)
                for (int j = zah.xop; j < zah.xop + zah.opx; j++) {
                    mo = mo + A[i, j];
                    if (A[i, j] > YY) YY = A[i, j];
                }

            mo = mo / (zah.opy * zah.opx);

            for (int i = zah.yop; i < zah.yop + zah.opy; i++)
                for (int j = zah.xop; j < zah.xop + zah.opx; j++)
                    dis = dis + Math.Pow(A[i, j] - mo, 2);

            dis = dis / (zah.opy * zah.opx);
        }
        private void MedianFiltre() {
            int rank = 5;
            int h = A.GetLength(0);
            int w = A.GetLength(1);
            int zeros = rank / 2;
            int[,] longImg = new int[h + 2 * zeros, w + 2 * zeros];

            for (int i = 0; i < zeros; i++)
                for (int j = 0; j < (w + 2 * zeros); j++) {
                    longImg[i, j] = 0;
                }

            for (int i = (h + zeros); i < (h + 2 * zeros); i++)
                for (int j = 0; j < (w + 2 * zeros); j++) {
                    longImg[i, j] = 0;
                }

            for (int i = 0; i < (h + 2 * zeros); i++)
                for (int j = 0; j < zeros; j++) {
                    longImg[i, j] = 0;
                }

            for (int i = 0; i < (h + 2 * zeros); i++)
                for (int j = (w + zeros); j < (w + 2 * zeros); j++) {
                    longImg[i, j] = 0;
                }

            for (int i = zeros; i < (h + zeros); i++)
                for (int j = zeros; j < (w + zeros); j++) {
                    longImg[i, j] = A[i - zeros, j - zeros];
                }

            for (int i = zeros; i < (h + zeros); i++) {
                for (int j = zeros; j < (w + zeros); j++) {
                    int[] mask = new int[rank * rank];
                    int k = 0;
                    for (int x = -zeros; x <= zeros; x++) {
                        for (int y = -zeros; y <= zeros; y++) {
                            mask[k++] = longImg[i + x, j + y];
                        }
                    }
                    Array.Sort(mask);
                    A[i - zeros, j - zeros] = (byte)mask[mask.Length / 2];
                }
            }
         return;
        }
        private void Zahvat(Areas zah) {
            char p = 'a';
            for (int i = zah.yop; i < zah.yop + zah.opy; i++)
                for (int j = zah.xop; j < zah.xop + zah.opx; j++) {
                    if (A[i, j] - znachmetoda > 0) {
                        switch (p) {
                            case 'a':
                                xstrobSP = j;
                                ystrobSP = i;
                                strobxSP = j;
                                strobySP = i;
                                p = 'b';
                            break;
                            case 'b':
                                if (j > strobxSP) strobxSP = j;
                                if (i > strobySP) strobySP = i;
                                if (j < xstrobSP) xstrobSP = j;
                            break;
                        }
                    }
                }

            strobxSP = strobxSP - xstrobSP;
            strobySP = strobySP - ystrobSP;
        }

        private void RatioSizes(){ 
            if (DataPurpose.strobX == 0) { Perezapis = true; return; }
            if (strobySP == 0) { strobySP += 1; }
            if (strobxSP == 0) { strobxSP += 1; }
            double S1 = strobySP * strobxSP;
            double S2 = DataPurpose.strobX * DataPurpose.strobY;
            if (S1 > S2)
                if (Math.Sqrt(S1 / S2) > 1.7) //расширение цели
                   Perezapis = false;
                else 
                   Perezapis = true;
            else
                if (Math.Sqrt(S2 / S1) > 3.3) //сужение цели
                   Perezapis = false;
                else
                   Perezapis = true;
            return;
       }
        public static byte[,] MassivIM(Bitmap im)  {
            byte[,] AA = new byte[im.Height, im.Width];
            for (int i = 0; i < im.Height; i++)
                for (int j = 0; j < im.Width; j++){
                    Color pixel = im.GetPixel(j, i);
                    AA[i, j] = pixel.R;
                }
            return AA;
        }
    }
}
