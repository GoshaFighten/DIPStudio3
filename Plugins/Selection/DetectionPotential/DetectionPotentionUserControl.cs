using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;

namespace DetectionPotential {
    public partial class DetectionPotentionUserControl : BasePluginUserControl {
        public DetectionPotentionUserControl(DetectionPotentialSettings settings)
            : base(settings) {
            InitializeComponent();
            tableComboBox1.Properties.Application = this.fApplication;
            BindToControl(settings, s => s.InputTable, tableComboBox1, c => c.EditValue);
            BindValueObject(settings, s => s.F, emptySpaceItem1);
            BindToControl(settings, s => s.MBA, txtMBA, c => c.EditValue);
            BindToControl(settings, s => s.MBO, txtMBO, c => c.EditValue);
            BindToControl(settings, s => s.QBA, txtQBA, c => c.EditValue);
            BindToControl(settings, s => s.FrameWidth, txtFrameWidth, c => c.EditValue);
            BindToControl(settings, s => s.FrameHeight, txtFrameHeight, c => c.EditValue);
            BindToControl(settings, s => s.ObjWidth, txtObjWidth, c => c.EditValue);
            BindToControl(settings, s => s.ObjHeight, txtObjHeight, c => c.EditValue);
            BindToControl(settings, s => s.UseStrob, ceStrob, c => c.EditValue);
            BindToControl(settings, s => s.KStrob, seKStrob, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, txtResultName, c => c.EditValue, settings.Plugin.ShortName);
        }
    }
}
