using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DIPStudioCore;

namespace SelectObjs {
    public class MathSelectObjs {
        public static List<Area> GetObjs(Bitmap image, int threshold)
        {
            int[,] img = ImageMath.ImgToInt(image);
            double mba = ImageMath.Mean(img);
            double qba = ImageMath.Std(img);
            int[,] area = GetArea(img, threshold);
            List<Area> areas = new List<Area>();
            Dictionary<int, int> number = new Dictionary<int, int>();
            int p = 0;
            for (int i = 0; i < area.GetLength(0); i++) {
                for (int j = 0; j < area.GetLength(1); j++) {
                    if (area[i, j] != 0) {
                        if (!number.ContainsKey(area[i, j])) {
                            areas.Add(new Area());
                            number.Add(area[i, j], p++);
                        }
                        int value;
                        if (number.TryGetValue(area[i, j], out value)) {
                            areas[value].coords.Add(new Coord(j, i, img[i, j]));
                        }
                    }
                }
            }
            foreach (Area a in areas) {
                a.S = a.coords.Count;
                int[] x = new int[a.coords.Count];
                int[] y = new int[a.coords.Count];
                int maxBrightness = 0;
                int[] bs = new int[a.coords.Count];
                for (int i = 0; i < a.coords.Count; i++) {
                    x[i] = a.coords[i].X;
                    y[i] = a.coords[i].Y;
                    bs[i] = a.coords[i].Brightness;
                    if (a.coords[i].Brightness > maxBrightness) {
                        maxBrightness = a.coords[i].Brightness;
                    }
                }
                a.MBA = mba;
                a.QBA = qba;
                a.MBO = ImageMath.Mean(bs);
                a.QBO = ImageMath.Std(bs);
                a.MaxBrightness = maxBrightness;
                int[] xMinMax = MinMax(x);
                int[] yMinMax = MinMax(y);
                a.Width = xMinMax[1] - xMinMax[0] + 1;
                a.Height = yMinMax[1] - yMinMax[0] + 1;
                double sx = 0.0;
                foreach (int xi in x) {
                    sx += xi;
                }
                a.CenterX = sx / x.Length;
                double sy = 0.0;
                foreach (int yi in y) {
                    sy += yi;
                }
                a.CenterY = sy / y.Length;
            }
            return areas;
        }

        private static int[] MinMax(int[] array) {
            int min = array[0];
            int max = array[0];
            for (int i = 0; i < array.Length; i++) {
                if (array[i] > max) {
                    max = array[i];
                }
                if (array[i] < min) {
                    min = array[i];
                }
            }
            return new int[] { min, max };
        }

        private static int[,] GetArea(int[,] img, int threshold)
        {
            int h = img.GetLength(0);
            int w = img.GetLength(1);
            int[,] objs = new int[h, w];
            for (int i = 0; i < h; i++) {
                for (int j = 0; j < w; j++) {
                    objs[i, j] = 0;
                }
            }
            int numberArea = 0;
            if (img[0, 0] > threshold) {
                numberArea++;
                objs[0, 0] = numberArea;
            }
            for (int j = 1; j < w; j++) {
                if (img[0, j] > threshold) {
                    if (objs[0, j - 1] == 0) {
                        numberArea++;
                        objs[0, j] = numberArea;
                    }
                    else {
                        objs[0, j] = objs[0, j - 1];
                    }
                }
            }
            for (int i = 1; i < h; i++) {
                if (img[i, 0] > threshold) {
                    if (objs[i - 1, 0] == 0) {
                        numberArea++;
                        objs[i, 0] = numberArea;
                    }
                    else {
                        objs[i, 0] = objs[i - 1, 0];
                    }
                }
                for (int j = 1; j < w; j++) {
                    if (img[i, j] > threshold) {
                        if ((objs[i - 1, j] == 0) && (objs[i, j - 1] == 0)) {
                            numberArea++;
                            objs[i, j] = numberArea;
                        }
                        if ((objs[i - 1, j] == 0) && (objs[i, j - 1] != 0)) {
                            objs[i, j] = objs[i, j - 1];
                        }
                        if ((objs[i - 1, j] != 0) && (objs[i, j - 1] == 0)) {
                            objs[i, j] = objs[i - 1, j];
                        }
                        if ((objs[i - 1, j] != 0) && (objs[i, j - 1] != 0)) {
                            if (objs[i - 1, j] == objs[i, j - 1]) {
                                objs[i, j] = objs[i - 1, j];
                            }
                            else {
                                RenameArea(objs, objs[i - 1, j], objs[i, j - 1]);
                                objs[i, j] = objs[i, j - 1];
                            }
                        }
                    }
                }
            }
            return objs;
        }

        private static int[,] RenameArea(int[,] area, int inArea, int outArea) {
            for (int i = 0; i < area.GetLength(0); i++) {
                for (int j = 0; j < area.GetLength(1); j++) {
                    if (area[i, j] == inArea) {
                        area[i, j] = outArea;
                    }
                }
            }
            return area;
        }

        
    }
}
