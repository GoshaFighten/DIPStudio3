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

namespace ModelImages
{
    public partial class ModelImagesUserControl : BasePluginUserControl
    {
        public ModelImagesUserControl(ModelImagesSettings settings)
            : base(settings)
        {
            InitializeComponent();
            frameComboBox1.Properties.Application = DIPApplication.GetInstance();
            BindToControl(settings, s => s.Width, seWidth, c => c.EditValue);
            BindToControl(settings, s => s.Height, seHeight, c => c.EditValue);
            BindToControl(settings, s => s.BARnd, seBARnd, c => c.EditValue);
            BindToControl(settings, s => s.A, seA, c => c.EditValue);
            BindToControl(settings, s => s.W, seW, c => c.EditValue);
            BindToControl(settings, s => s.H, seH, c => c.EditValue);
            BindToControl(settings, s => s.UseImages, checkEdit1, c => c.EditValue);
            BindToControl(settings, s => s.BackgroundImages, frameComboBox1, c => c.EditValue);
            BindToControl(settings, s => s.AM, seAM, c => c.EditValue);
            BindToControl(settings, s => s.DeltaD, seDeltaD, c => c.EditValue);
            BindToControl(settings, s => s.XC, seXC, c => c.EditValue);
            BindToControl(settings, s => s.XCNRnd, seXCNRnd, c => c.EditValue);
            BindToControl(settings, s => s.WC, seWC, c => c.EditValue);
            BindToControl(settings, s => s.RM, seRM, c => c.EditValue);
            BindToControl(settings, s => s.YC, seYC, c => c.EditValue);
            BindToControl(settings, s => s.YCNRnd, seYCNRnd, c => c.EditValue);
            BindToControl(settings, s => s.XMRnd, seXMRnd, c => c.EditValue);
            BindToControl(settings, s => s.YMRnd, seYMRnd, c => c.EditValue);
            BindToControl(settings, s => s.P, seP, c => c.EditValue);
            BindToControl(settings, s => s.ARnd, seARnd, c => c.EditValue);
            BindToControl(settings, s => s.Alpha, seAlpha, c => c.EditValue);
            BindToControl(settings, s => s.X0, seX0, c => c.EditValue);
            BindToControl(settings, s => s.Vx, seVx, c => c.EditValue);
            BindToControl(settings, s => s.Ax, seAx, c => c.EditValue);
            BindToControl(settings, s => s.Wx, seWx, c => c.EditValue);
            BindToControl(settings, s => s.Px, sePx, c => c.EditValue);
            BindToControl(settings, s => s.Y0, seY0, c => c.EditValue);
            BindToControl(settings, s => s.Vy, seVy, c => c.EditValue);
            BindToControl(settings, s => s.Ay, seAy, c => c.EditValue);
            BindToControl(settings, s => s.Wy, seWy, c => c.EditValue);
            BindToControl(settings, s => s.Py, sePy, c => c.EditValue);
            BindToControl(settings, s => s.S, seS, c => c.EditValue);
            BindToControl(settings, s => s.IMAX, seIMAX, c => c.EditValue);
            BindToControl(settings, s => s.TA, seTA, c => c.EditValue);
            BindToControl(settings, s => s.RandPosition, seRandPosition, c => c.EditValue);
            BindToControl(settings, s => s.RandSize, seRandSize, c => c.EditValue);
            BindToControl(settings, s => s.F0, seF0, c => c.EditValue);
            BindToControl(settings, s => s.F1, seF1, c => c.EditValue);
            BindToControl(settings, s => s.T0, seT0, c => c.EditValue);
            BindToControl(settings, s => s.Cell, seCell, c => c.EditValue);
            BindToControl(settings, s => s.X0Constant, seX0Constant, c => c.EditValue);
            BindToControl(settings, s => s.Y0Constant, seY0Constant, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, teResult, c => c.EditValue, settings.Plugin.ShortName);
            pictureEdit1.Image = Properties.Resources.image001;
        }
    }
}
