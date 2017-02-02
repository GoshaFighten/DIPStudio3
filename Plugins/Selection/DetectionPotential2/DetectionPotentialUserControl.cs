using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;

namespace DetectionPotential2
{
    public partial class DetectionPotentialUserControl : BasePluginUserControl
    {
        public DetectionPotentialUserControl(DetectionPotentialSettings settings)
            :base(settings)
        {
            InitializeComponent();
            inputSeries.Properties.Application = this.fApplication;
            inputObjs.Properties.Application = this.fApplication;
            BindToControl(settings, s => s.InputSeries, inputSeries, c => c.EditValue);
            BindToControl(settings, s => s.InputTable, inputObjs, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, teResult, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, s => s.Tresholder, seT, c => c.EditValue);
            BindToControl(settings, s => s.K, seK, c => c.EditValue);
        }
    }
}
