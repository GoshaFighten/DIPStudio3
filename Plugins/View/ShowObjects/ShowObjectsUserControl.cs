using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;

namespace ShowObjects {
    public partial class ShowObjectsUserControl : BasePluginUserControl {
        public ShowObjectsUserControl(ShowObjectsSettings settings)
            : base(settings) {
            InitializeComponent();
            cmbImages.Properties.Application = this.fApplication;
            cmbTable.Properties.Application = this.fApplication;
            BindToControl(settings, s => s.InputImages, cmbImages, c => c.EditValue);
            BindToControl(settings, s => s.InputTable, cmbTable, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, txtResult, c => c.EditValue, settings.Plugin.ShortName);
        }
    }
}
