using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;
using DIPStudioCore;
using DIPStudioCore.Data;

namespace DymAnalizCalculation
{
    public partial class SetSynxTimeUserControl : Form
    {
        int Margins = 12;
        int startTime = 0;
        public SetSynxTimeUserControl()
        {
            InitializeComponent();
            DIPApplicationBase app = DIPApplicationBase.GetInstance();
            cmbInputImg.Properties.Application = app;
            cmbDataInput.Properties.Application = app;
        }

        private void cmbInputImg_SelectedValueChanged(object sender, EventArgs e)
        {
            DIPApplicationBase app = DIPApplicationBase.GetInstance();
            Series series = app.GetSeriesByName(cmbInputImg.Text);
            Draw(series);
            seFrameChoose.Properties.MaxValue = series.Count;
        }
        private void Draw(Series series)
        {
            if (series == null)
                throw new PluginException(string.Format("Series {0} does not exist", series));
            if (series.Count == 0)
                throw new PluginException(string.Format("Series {0} does not contain frames", series));
            Frame frame = series.GetElementByIndex((int)seFrameChoose.Value);
            pictureEdit1.Image = frame.Image;
        }

        private void SetSynxTimeUserControl_SizeChanged(object sender, EventArgs e)
        {
            this.pictureEdit1.Width = this.Width / 2 - Margins * 2;
            this.listBoxControl1.Left = this.Width / 2 - Margins;
            this.listBoxControl1.Width = this.Width / 2 - Margins;
        }

        public int GetStartTime()
        {
            return startTime;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void cmbDataInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            fieldComboBox1.Properties.TableName = cmbDataInput.Text;
        }

        private void fieldComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DIPApplicationBase app = DIPApplicationBase.GetInstance();
            Table table = app.GetTableByName(cmbDataInput.Text);
            foreach (var item in table) {
                listBoxControl1.Items.Add(item.Objects[0].Properties[fieldComboBox1.SelectedItem.ToString()]);
            }
           
        }
    }
}
