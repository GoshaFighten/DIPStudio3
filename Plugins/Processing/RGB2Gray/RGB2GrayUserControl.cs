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

namespace RGB2Gray
{
    public partial class RGB2GrayUserControl : BasePluginUserControl
    {
        public RGB2GrayUserControl(RGB2GraySettings settings)
            :base(settings)
        {
            InitializeComponent();
            cmbInput.Properties.Application = this.fApplication;
            BindToControl(settings, s => s.InputSeries, cmbInput, c => c.EditValue);
            BindToControl(settings, s => s.Red, spinRed, c => c.Text);
            BindToControl(settings, s => s.Green, spinGreen, c => c.Text);
            BindToControl(settings, s => s.Blue, spinBlue, c => c.Text);
            BindToControl(settings, s => s.ResultName, txtResult, c => c.EditValue, settings.Plugin.ShortName);
        }


        private void cmbInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            DIPApplication app = DIPApplication.GetInstance();
            Series inputSeries = app.GetSeriesByName( cmbInput.Text);
            if (inputSeries != null)
                Draw(inputSeries);
        }

        private void Draw(Series inputSeries)
        {
            if (inputSeries == null)
                throw new PluginException(string.Format("Series {0} does not exist", inputSeries));
            if (inputSeries.Count == 0)
                throw new PluginException(string.Format("Series {0} does not contain frames", inputSeries));

            RGB2Gray classEx = (RGB2Gray) this.fSettings.Plugin;
            Frame sourceFrame = inputSeries.GetElementByIndex(seriesView1.Number);
            seriesView1.SetImage(ImageMath.GetProcess(sourceFrame, classEx.Logic, Image.GetPixelFormatSize(sourceFrame.Image.PixelFormat) / 8, seriesView1.Number).Image);
        }
    }
}