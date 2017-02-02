using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace DIPStudio3 {
    public partial class SplashScreen1 : SplashScreen {
        public SplashScreen1() {
            InitializeComponent();
            labelControl2.Text = "Запуск...";
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg) {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand {
        }
    }
}