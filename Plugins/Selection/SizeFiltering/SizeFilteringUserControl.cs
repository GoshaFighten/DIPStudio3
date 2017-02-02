using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;

namespace SizeFiltering {
    public partial class SizeFilteringUserControl : BasePluginUserControl {
        public SizeFilteringUserControl(SizeFilteringSettings settings)
            : base(settings) {
            InitializeComponent();
            tableComboBox1.Properties.Application = this.fApplication;
            BindToControl(settings, s => s.InputTable, tableComboBox1, c => c.EditValue);
            BindValueObject(settings, s => s.MinSize, emptySpaceItem1);
            BindValueObject(settings, s => s.MaxSize, emptySpaceItem2);
            BindToControl(settings, s => s.ResultName, textEdit1, c => c.EditValue, settings.Plugin.ShortName);
        }
    }
}
