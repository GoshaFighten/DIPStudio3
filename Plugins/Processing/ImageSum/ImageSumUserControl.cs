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

namespace ImageSum {
    public partial class ImageSumUserControl : BasePluginUserControl {
        public ImageSumUserControl(ImageSumSettings settings)
            : base(settings) {
            InitializeComponent();
            cmbInput1.Properties.Application = fApplication;
            cmbInput2.Properties.Application = fApplication;
            BindToControl(settings, s => s.ResultName, teResult, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, s => s.InputSeries1, cmbInput1, c => c.EditValue);
            BindToControl(settings, s => s.InputSeries2, cmbInput2, c => c.EditValue);
        }
        private void Draw()
        {
            DIPApplication application = DIPApplication.GetInstance();
            ImageSumSettings settings = (ImageSumSettings)Settings;
            Series inputSeries1 = application.GetSeriesByName((string)cmbInput1.EditValue);
            if (inputSeries1 == null) {
                throw new PluginException(string.Format("Series {0} does not exist", (string)cmbInput1.EditValue));
            }
            Series inputSeries2 = application.GetSeriesByName((string)cmbInput2.EditValue);
            if (inputSeries2 == null) {
                throw new PluginException(string.Format("Series {0} does not exist", (string)cmbInput2.EditValue));
            }
            seriesView1.SetImage(ImageMath.GetSumFrame(inputSeries1[seriesView1.Number], inputSeries2[seriesView1.Number]).Image);
        }

        private void seriesView1_UpdateControl(object sender, EventArgs e)
        {
            Draw();
        }
    }
}
