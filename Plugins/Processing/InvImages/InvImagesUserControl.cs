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

namespace InvImages
{
    public partial class InvImagesUserControl : BasePluginUserControl
    {
        public InvImagesUserControl(InvImagesSettings settings)
            : base(settings)
        {
            InitializeComponent();
            frameComboBox1.Properties.Application = DIPApplication.GetInstance();
            BindToControl(settings, s => s.InputSeries, frameComboBox1, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, textEdit1, c => c.EditValue, settings.Plugin.ShortName);
        }

        private void frameComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            InvImagesSettings settings = (InvImagesSettings)Settings;
            DIPApplication application = DIPApplication.GetInstance();
            Series inputSeries = application.GetSeriesByName( settings.InputSeries);
            if (inputSeries != null && inputSeries.Count != 0)
            {
                seriesView1.SetImage(ImageMath.GetInvertedImage(inputSeries.GetElementByIndex(seriesView1.Number).Image));
            }
        }
    }
}
