using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DIPStudioUICore {
    public static class DIPMessageBox {
        const string ErrorCaption = "Ошибка";
        const string HelpCaption = "Помощь";
        const string StudioCaption = "DIPStudio3";
        public static void Show(IWin32Window parent, string text) {
            DIPMessageBox.Show(parent, text, ErrorCaption, MessageBoxIcon.Error);
        }

        public static void ShowHelp(IWin32Window parent, string text)
        {
            DIPMessageBox.Show(parent, text, HelpCaption, MessageBoxIcon.Information);
        }

        public static void Show(IWin32Window parent, string text, string caption, MessageBoxIcon icon)
        {

            XtraMessageBox.Show(parent, text, caption, MessageBoxButtons.OK, icon);
        }

        public static DialogResult ShowThreeButtonDialog(IWin32Window parent, string text) {
            return XtraMessageBox.Show(parent, text, StudioCaption, MessageBoxButtons.YesNoCancel);
        }

        public static DialogResult ShowTwoButtonDialog(IWin32Window parent, string text) {
            return XtraMessageBox.Show(parent, text, StudioCaption, MessageBoxButtons.YesNo);
        }
    }
}
