using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;
using DIPStudioUICore.Extensions;

namespace LoadImages {
    public partial class LoadImagesUserControl : BasePluginUserControl {
        public LoadImagesUserControl(LoadImageSettings settings)
            : base(settings) {
            InitializeComponent();
            teStartTime.Properties.SetIntMask();
            BindToControl(settings, s => s.ResultName, teResultName, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, s => s.FolderName, beFolderName, c => c.EditValue);
            BindToControl(settings, s => s.StartTime, teStartTime, c => c.EditValue);
            BindToControl(settings, s => s.D, teD, c => c.EditValue);
            BindToControl(settings, s => s.Convert, ceConvert, c => c.EditValue);
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            folderBrowserDialog1.SelectedPath = (string)beFolderName.EditValue;
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                beFolderName.EditValue = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
