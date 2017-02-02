using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Skins.XtraForm;
using DevExpress.Utils;
using DevExpress.Skins;

namespace DIPStudioUICore {
    public partial class BaseForm : DevExpress.XtraEditors.XtraForm {
        public BaseForm() {
            InitializeComponent();
        }

        protected override FormPainter CreateFormBorderPainter() {
            return new MyFormPainter(this, LookAndFeel);
        }
    }

    public class MyFormPainter : FormPainter {
        public MyFormPainter(Control owner, DevExpress.Skins.ISkinProvider provider)
            : base(owner, provider) {
        }

        protected override void DrawText(DevExpress.Utils.Drawing.GraphicsCache cache) {
            string text = Text;
            if(text == null || text.Length == 0 || TextBounds.IsEmpty) {
                return;
            }
            AppearanceObject appearance = new AppearanceObject(GetDefaultAppearance());
            appearance.TextOptions.Trimming = Trimming.EllipsisCharacter;
            appearance.TextOptions.HAlignment = HorzAlignment.Center;
            if(AllowHtmlDraw) {
                DrawHtmlText(cache, appearance);
                return;
            }
            Rectangle r = RectangleHelper.GetCenterBounds(Buttons.CalcBounds(null, ButtonPainter, GetCaptionClient(CaptionBounds)), new Size(TextBounds.Width, CalcTextHeight(cache.Graphics, appearance)));
            if(this.IconBounds.Right + IconToCaptionIndent > r.X) {
                r.Width -= (IconBounds.Right + IconToCaptionIndent - r.X);
                r.X = (IconBounds.Right + IconToCaptionIndent);
            }
            DrawTextShadow(cache, appearance, r);
            cache.DrawString(text, appearance.Font, appearance.GetForeBrush(cache), r, appearance.GetStringFormat());
        }
    }
}
