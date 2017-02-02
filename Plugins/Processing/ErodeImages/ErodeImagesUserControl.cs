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

namespace ErodeImages2
{
    public partial class ErodeImagesUserControl : BasePluginUserControl
    {
        public ErodeImagesUserControl(ErodeImagesSettings settings)
            :base(settings)
        {
            InitializeComponent();
            frameComboBox1.Properties.Application = DIPApplication.GetInstance();
            BindValueObject(settings, s => s.Order, emptySpaceItem1, frameComboBox1_SelectedValueChanged);
            BindToControl(settings, s => s.InputSeries, frameComboBox1, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, textEdit1, c => c.EditValue, settings.Plugin.ShortName);
        }

        private void frameComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            ErodeImagesSettings settings = (ErodeImagesSettings)Settings;
            DIPApplication application = DIPApplication.GetInstance();
            Series inputSeries = application.GetSeriesByName( settings.InputSeries);
            if (inputSeries != null && inputSeries.Count != 0)
            {
                ErodeImages example = (ErodeImages)settings.Plugin;
                if (settings.Order.GetValue(seriesView1.Number) == 0)
                    return;
                seriesView1.SetImage(ImageMath.GetExpandProcess(inputSeries.GetElementByIndex(seriesView1.Number), example.GetMin, (int)settings.Order.GetValue(seriesView1.Number) / 2, Byte.MaxValue).Image);
            }
        }
    }
}
