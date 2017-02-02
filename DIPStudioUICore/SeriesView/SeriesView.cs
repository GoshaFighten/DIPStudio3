using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DIPStudioUICore
{
    public partial class SeriesView : UserControl
    {
        public int Number { get; set; }
        public SeriesView()
        {
            InitializeComponent();
            this.spinEdit1.DataBindings.Add(new Binding("Value", this, "Number"));
            this.btnUpdate.Click += new EventHandler(spinEdit1_ValueChanged);
            this.spinEdit1.ValueChanged += new EventHandler(spinEdit1_ValueChanged);
        }

        void spinEdit1_ValueChanged(object sender, EventArgs e)
        {
            UpdateControl(sender,e);
        }
        public void SetImage(Bitmap image)
        {
            this.pictureEdit1.Image = image;
        }
        public event EventHandler UpdateControl;
    }
}
