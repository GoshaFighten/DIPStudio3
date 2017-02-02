using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DIPStudio3
{
    public partial class SetNumber : Form
    {
        public int DialogValue;
        public SetNumber(string text)
        {
            InitializeComponent();
            this.layoutControlItem1.Text = text;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.DialogValue = (int) this.spinEdit1.Value;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void SetNumber_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
