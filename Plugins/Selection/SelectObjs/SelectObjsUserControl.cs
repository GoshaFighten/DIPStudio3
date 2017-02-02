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

namespace SelectObjs {
    public partial class SelectObjsUserControl : BasePluginUserControl {
        public SelectObjsUserControl(SelectObjsSettings settings)
            : base(settings) {
            InitializeComponent();
            frameComboBox1.Properties.Application = fApplication;
            BindToControl(settings, s => s.ResultName, textEdit1, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, s => s.InputSeries, frameComboBox1, c => c.EditValue);
            BindToControl(settings, s => s.AddSeries, ceAddSeries, c => c.EditValue);
            BindValueObject(settings, s => s.Threshold, emptySpaceItem1, cmbInput_SelectedIndexChanged);               
        }

        private void cmbInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            DIPApplication app = DIPApplication.GetInstance();
            Series inputSeries = app.GetSeriesByName( frameComboBox1.Text);
            if (inputSeries != null && inputSeries.Count != 0)
                Draw(inputSeries);
        }

        private void Draw(Series inputSeries)
        {
            SelectObjs example = (SelectObjs)Settings.Plugin;
            peResult.Image = ImageMath.GetProcess(inputSeries.GetElementByIndex(0), example.BinLogic, 1, 0).Image;
        }
    }
}
