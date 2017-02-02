using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GraphViewer
{
    public class GraphData
    {
        private string fTitle = "Заголовок";
        private string fOX = "Ось Х";
        private string fOY = "Ось У";

        public string Title 
        { 
            get {return fTitle;}
            set { fTitle = value; }
        }
        public string OX
        {
            get { return fOX; }
            set { fOX = value; }
        }
        public string OY
        {
            get { return fOY; }
            set { fOY = value; }
        }

        private IList<Curva> fCurvas = new BindingList<Curva>();
        public IList<Curva> Curvas
        {
            get
            {
                return fCurvas;
            }
        }
    }
}
