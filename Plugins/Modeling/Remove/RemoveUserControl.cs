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

namespace Remove
{
    public partial class RemoveUserControl : BasePluginUserControl
    {
        public RemoveUserControl(RemoveSettings settings)
            :base(settings)
        {
            InitializeComponent();
            frameComboBox1.Properties.Application = DIPApplicationBase.GetInstance();
            tableComboBox1.Properties.Application = DIPApplicationBase.GetInstance();

            BindToControl(settings, s => s.InputSeries, frameComboBox1, c => c.EditValue);
            BindToControl(settings, s => s.InputTable, tableComboBox1, c => c.EditValue);
        }
    }
}
