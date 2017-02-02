using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CorrelationAnalysis{
    public class Areas
    {
        internal readonly int opx, opy, xop, yop;

          public Areas(int ncsx, int ncsy, int strobx, int stroby, Image image, int sdvigX, int sdvigY, bool IR, int n) {
              if (IR) { n = n * 2; }
              xop = (int)(ncsx - n * strobx) + sdvigX;
              if (xop < 0) xop = 0;
              yop = (int)(ncsy - n * stroby) + sdvigY;
              if (yop < 0) yop = 0;
              opx = (int)((2 * n + 1) * strobx);
              if (opx + xop >= image.Width) opx = image.Width - xop - 1;
              opy = (int)((2 * n + 1) * stroby);
              if (opy + yop >= image.Height) opy = image.Height - yop - 1;
          }

          public Areas(int ncsx, int ncsy, int strobx, int stroby, Image image)
          {
              xop = (int)(ncsx - 1 * strobx);
              if (xop < 0) xop = 0;
              yop = (int)(ncsy - 1 * stroby);
              if (yop < 0) yop = 0;
              opx = (int)((2 * 1 + 1) * strobx);
              if (opx + xop >= image.Width) opx = image.Width - xop - 1;
              opy = (int)((2 * 1 + 1) * stroby);
              if (opy + yop >= image.Height) opy = image.Height - yop - 1;
          }
    }
}
