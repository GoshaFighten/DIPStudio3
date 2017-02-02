using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioCore;
using DevExpress.XtraEditors;

namespace DIPStudioUICore {
    public partial class BasePluginForm : BaseForm {
        protected readonly DIPApplication fApplication;
        public readonly bool fModify;
        BasePluginUserControl fCtrl;
        public BasePluginForm(BasePluginUserControl ctrl, bool modify)
            : this() {
                fCtrl = ctrl;
            ctrl.Dock = DockStyle.Fill;
            ctrl.Parent = parentControl;
            fModify = modify;
            dxErrorProvider1.DataSource = ctrl.Settings;
            if (ctrl.Settings.Plugin != null)
                this.Text = String.Format("Настройка плагина \"{0}\" {1}", ctrl.Settings.Plugin.Caption, ctrl.Settings.Plugin.ShortName);
            btnCancel.Text = DevExpress.XtraEditors.Controls.Localizer.Active.GetLocalizedString(DevExpress.XtraEditors.Controls.StringId.XtraMessageBoxCancelButtonText);
            btnOK.Text = DevExpress.XtraEditors.Controls.Localizer.Active.GetLocalizedString(DevExpress.XtraEditors.Controls.StringId.XtraMessageBoxOkButtonText);
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            SetMinSize();
            LockControls(this.Controls);
            if (dxErrorProvider1.DataSource != null) {
                dxErrorProvider1.UpdateBinding();
            }
            fCtrl.BestFitLayout();
        }

        private void SetMinSize() {
            int width = btnOK.Width + btnCancel.Width + btnCancel.Left - btnOK.Right + 2 * (this.Width - btnCancel.Right);
            this.MinimumSize = new Size(width, this.MinimumSize.Height);
        }

        protected BasePluginForm() {
            fApplication = DIPApplication.GetInstance();
            InitializeComponent();
        }
        private bool GetEnabled() {
            if (fModify)
                return fApplication.Rights == Rights.Admin;
            return fModify;
        }

        private void LockControls(Control.ControlCollection controls) {
            foreach (Control control in controls) {
                if (control == btnCancel) {
                    continue;
                }
                if (control is TextEdit) {
                    ((TextEdit)control).Properties.ReadOnly = !GetEnabled();
                    if (control is ButtonEdit) {
                        foreach (DevExpress.XtraEditors.Controls.EditorButton button in ((ButtonEdit)control).Properties.Buttons) {
                            button.Enabled = GetEnabled();
                        }
                    }
                }
                else {
                    LockControls(control.Controls);
                    if (!(control is ContainerControl || control is ScrollableControl))
                        control.Enabled = GetEnabled();
                }
            }
        }

        private void BasePluginForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.DialogResult != System.Windows.Forms.DialogResult.OK) {
                return;
            }
            e.Cancel = dxErrorProvider1.HasErrors;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            DIPMessageBox.ShowHelp(this, fCtrl.Settings.Plugin.GetHelp());
        }
    }
}
