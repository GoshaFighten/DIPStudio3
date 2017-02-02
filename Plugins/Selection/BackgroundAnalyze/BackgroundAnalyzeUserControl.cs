using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;

namespace BackgroundAnalyze {
    public partial class BackgroundAnalyzeUserControl : BasePluginUserControl {
        public BackgroundAnalyzeUserControl(BackgroundAnalyzeSettings settings)
            : base(settings) {
            InitializeComponent();
            cmbInput.Properties.Application = fApplication;
            BindToControl(settings, s => s.InputSeries, cmbInput, c => c.EditValue);
            BindToControl(settings, x => x.WindowWidth, seWindowWidth, c => c.EditValue);
            BindToControl(settings, x => x.WindowHeight, seWindowHeight, c => c.EditValue);
            BindToControl(settings, x => x.StepX, seStepX, c => c.EditValue);
            BindToControl(settings, x => x.StepY, seStepY, c => c.EditValue);
            BindToControl(settings, x => x.StrobX, seStrobX, c => c.EditValue);
            BindToControl(settings, x => x.StrobY, seStrobY, c => c.EditValue);
            BindToControl(settings, x => x.StrobWidth, seStrobWidth, c => c.EditValue);
            BindToControl(settings, x => x.StrobHeight, seStrobHeight, c => c.EditValue);
            BindToControl(settings, x => x.ResultName, teResult, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, x => x.S, teS, c => c.EditValue);
            BindToControl(settings, x => x.Scan, checkEdit1, ce => ce.Checked);
            BindToControl(settings, x => x.Three, checkEdit2, ce => ce.Checked);
        }
    }
}
