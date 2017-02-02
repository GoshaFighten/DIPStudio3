using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;

namespace MethodTraces {
    public partial class MethodTracesUserControl : BasePluginUserControl {
        public MethodTracesUserControl(MethodTracesSettings settings)
            : base(settings) {
            InitializeComponent();
            cmpInput.Properties.Application = this.fApplication;
            BindToControl(settings, s => s.InputTable, cmpInput, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, teResultName, c => c.EditValue, settings.Plugin.ShortName);
        }
    }
}
