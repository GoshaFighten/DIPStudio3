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

namespace BinImages {
    public partial class BinImagesUserControl : BasePluginUserControl {
        public BinImagesUserControl(BinImagesSettings settings)
            : base(settings) {
            InitializeComponent();
            cmbInput.Properties.Application = fApplication;
            BindValueObject(settings, s => s.Threshold, emptySpaceItem1, cmbInput_SelectedIndexChanged);
            BindToControl(settings, s => s.ResultName, teSeriesName, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, s => s.InputSeries, cmbInput, c => c.EditValue);
            BindToControl(settings, s => s.Mode, checkEdit1, ch => ch.EditValue);
        }

        private void cmbInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            DIPApplicationBase app = DIPApplicationBase.GetInstance();
            Series inputSeries = app.GetSeriesByName(cmbInput.Text);
            if (inputSeries != null && inputSeries.Count != 0)
                Draw(inputSeries);
        }

        private void Draw(Series inputSeries)
        {
            BinImagesSettings settings = (BinImagesSettings)Settings;
            BinImages example = (BinImages)settings.Plugin;
            Frame previewFrame = inputSeries.GetElementByIndex(seriesView.Number);
            seriesView.SetImage(ImageMath.GetProcess(previewFrame, example.BinLogic, 1, previewFrame.Time).Image);
        }
    }
}
 