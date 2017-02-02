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
    public partial class DisturbanceUserControl : BasePluginUserControl
    {
        ModelDisturbance Source;
        public DisturbanceUserControl(ModelDisturbance source)
        {
            Source = source;
            InitializeComponent();
            BindToControl(source, s => s.X0, seX0, c => c.EditValue);
            BindToControl(source, s => s.Vx, seVx, c => c.EditValue);
            BindToControl(source, s => s.Ax, seAx, c => c.EditValue);
            BindToControl(source, s => s.Wx, seWx, c => c.EditValue);
            BindToControl(source, s => s.Px, sePx, c => c.EditValue);
            BindToControl(source, s => s.Y0, seY0, c => c.EditValue);
            BindToControl(source, s => s.Vy, seVy, c => c.EditValue);
            BindToControl(source, s => s.Ay, seAy, c => c.EditValue);
            BindToControl(source, s => s.Wy, seWy, c => c.EditValue);
            BindToControl(source, s => s.Py, sePy, c => c.EditValue);
            BindToControl(source, s => s.S, seS, c => c.EditValue);
            BindToControl(source, s => s.IMAX, seIMAX, c => c.EditValue);
            BindToControl(source, s => s.A, seTA, c => c.EditValue);
            BindToControl(source, s => s.RandPosition, seRandPosition, c => c.EditValue);
            BindToControl(source, s => s.RandSize, seRandSize, c => c.EditValue);
        }
    }
}
