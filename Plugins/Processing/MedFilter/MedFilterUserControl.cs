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

namespace MedFilter2
{
    public partial class MedFilterUserControl : BasePluginUserControl
    {
        public MedFilterUserControl(MedFilterSettings settings)
            : base(settings)
        {
            InitializeComponent();
            frameComboBox1.Properties.Application = DIPApplicationBase.GetInstance();
            BindValueObject(settings, s => s.Order, emptySpaceItem1, frameComboBox1_SelectedValueChanged);
            BindToControl(settings, s => s.InputSeries, frameComboBox1, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, textEdit1, c => c.EditValue, settings.Plugin.ShortName);
        }

        private void frameComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            MedFilterSettings settings = (MedFilterSettings)Settings;
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            Series inputSeries = application.GetSeriesByName( settings.InputSeries);
            if (inputSeries != null && inputSeries.Count != 0)
            {
                MedFilter example = (MedFilter)settings.Plugin;
                if (settings.Order.GetValue(seriesView1.Number) == 0)
                    return;
                seriesView1.SetImage(ImageMath.GetExpandProcess(inputSeries.GetElementByIndex(seriesView1.Number), example.GetAverage, (int)settings.Order.GetValue(seriesView1.Number) / 2, Byte.MaxValue / 2).Image);
            }
        }
    }
}
