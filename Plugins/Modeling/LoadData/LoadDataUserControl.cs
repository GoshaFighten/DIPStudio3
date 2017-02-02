using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;

namespace LoadData {
    public partial class LoadDataUserControl : BasePluginUserControl {
        public LoadDataUserControl(LoadDataSettings settings)
            : base(settings) {
            InitializeComponent();
            BindToControl(settings, s => s.ResultName, teResultName, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, s => s.FileName, beFileName, c => c.EditValue);
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            openFileDialog1.FileName = (string)beFileName.EditValue;
            if(openFileDialog1.ShowDialog() == DialogResult.OK) {
                beFileName.EditValue = openFileDialog1.FileName;
            }
        }
    }
}
