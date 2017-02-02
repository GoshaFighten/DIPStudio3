using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;
using DIPStudioCore;

namespace DIPStudio3 {
    public partial class WaitForm1 : WaitForm {
        public WaitForm1() {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
            progressPanel1.Caption = "Выполнение..";
            progressPanel1.Description = "Загрузка...";
        }

        #region Overrides

        public override void SetCaption(string caption) {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description) {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }

        public override void ProcessCommand(Enum cmd, object arg) {
            base.ProcessCommand(cmd, arg);
            if(cmd is WaitFormCommand) {
                WaitFormCommand command = (WaitFormCommand)cmd;
                switch(command) {
                    case WaitFormCommand.ReportProgress: {
                            progressBarControl1.Position = (int)arg;
                            break;
                        }
                    default:
                        return;
                }
            }
        }

        #endregion

        public enum WaitFormCommand {
            ReportProgress
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DIPStudioCore.DIPApplication.GetInstance().CalculationStop = true;
        }
    }
}