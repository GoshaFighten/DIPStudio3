using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using System.Drawing;

namespace GraphViewer
{
    [Serializable]
    public class Curva : LineItem
    {
        public Curva(string label, IPointList points, Color color)
            :base(label, points, color, SymbolType.None)
        {}
        public Curva(string label, Color color)
            : base(label, new PointPairList(), color, SymbolType.None)
        { }
        private int fObjectNum;
        public bool KeepLine { get; set; }
//public bool Y2 { get; set; }
        public string SourceTable { get; set; }
        public string SourceField { get; set; }

        public int ObjectNum
        {
            get { return fObjectNum; }
            set {
                if (value > 0)
                    fObjectNum = value;
                else
                    fObjectNum = 0;
            }
        }

        //public double Max()
        //{
        //    double result = double.MinValue;
        //    for (int i = 0; i < this.Points.Count; i++)
        //        if (this.Points[i].Y > result)
        //            result = this.Points[i].Y;
        //    return result;
        //}

        //public double Min()
        //{
        //    double result = double.MaxValue;
        //    for (int i = 0; i < this.Points.Count; i++)
        //        if (this.Points[i].Y < result)
        //            result = this.Points[i].Y;
        //    return result;
        //}

        //public double Average()
        //{
        //    double result = 0;
        //    for (int i = 0; i < this.Points.Count; i++)
        //        result += this.Points[i].Y;
        //    return result / this.Points.Count;
        //}
    }
}
