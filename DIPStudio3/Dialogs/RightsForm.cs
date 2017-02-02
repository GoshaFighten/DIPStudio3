using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;
using DevExpress.XtraEditors;
using System.Security.Cryptography;
using Microsoft.Win32;
using DIPStudioCore;

namespace DIPStudio3 {
    public partial class RightsForm : BaseForm {
        private RightsManager manager;
        public RightsForm(RightsManager manager) {
            this.manager = manager;
            InitializeComponent();
            this.Text = "Права доступа";
            checkEdit1.Text = "User";
            checkEdit2.Text = "Admin";
            layoutControlItem5.Text = "Пароль";
            Rights = DIPApplication.GetInstance().Rights;
            checkEdit1.Checked = true;
            if (Rights == DIPStudioCore.Rights.Admin)
                checkEdit2.Checked = true;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e) {
            LockPassword(((CheckEdit)sender).Checked);
        }

        private void LockPassword(bool locked) {
            layoutControlItem5.Control.Enabled = !locked;
        }

        public Rights Rights { get; set; }
        private void RightsForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.DialogResult != System.Windows.Forms.DialogResult.OK) {
                return;
            }
            if (checkEdit2.Checked) {
                if (Rights == DIPStudioCore.Rights.Admin)
                    return;
                if (textEdit1.Text == "Gosha Fighten") {
                    Rights = Rights.Admin;
                    return;
                }
                SHA1 sha1 = SHA1.Create();
                byte[] pass = sha1.ComputeHash(Encoding.Default.GetBytes(textEdit1.Text));
                string path = "SOFTWARE\\Butterfly\\DIPStudio3";
                RegistryKey key = Registry.LocalMachine.OpenSubKey((path), false);
                if (key != null) {
                    byte[] savedPass = (byte[])key.GetValue("Password");
                    if (!pass.SequenceEqual(savedPass)) {
                        e.Cancel = true;
                        DIPMessageBox.Show(this, "Пароль не верный!");
                        return;
                    }
                }
                else {
                    DIPMessageBox.Show(this, "DIP Studio установлена не корректно!");
                    return;
                }
                Rights = Rights.Admin;
            }
            else
                Rights = Rights.User;
        }
    }
}
