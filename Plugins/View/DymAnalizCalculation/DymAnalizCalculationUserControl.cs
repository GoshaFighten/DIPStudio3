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

namespace DymAnalizCalculation
{
    public partial class DymAnalizCalculationUserControl : BasePluginUserControl
    {

        //private Rectangle PictureSize;
        public DymAnalizCalculationUserControl(DymAnalizCalculationSettings settings)
            : base(settings) {
            InitializeComponent();

            //Constant
            BindToControl(settings, s => s.g, spinEditg, c => c.EditValue);
            BindToControl(settings, s => s.U1, spinEditU1, c => c.EditValue);
            BindToControl(settings, s => s.M1, spinEditM1, c => c.EditValue);
            BindToControl(settings, s => s.Rov, spinEditRov, c => c.EditValue);
            BindToControl(settings, s => s.C, spinEditC, c => c.EditValue);
            //Data tables
            BindValueObject(settings, s => s.Koef, emptySpaceItem3);
            BindValueObject(settings, s => s.P, emptySpaceItem4);
            BindToControl(settings, s => s.ResultData, ResultData, c => c.EditValue, settings.Plugin.ShortName);
            //Variables
            BindToControl(settings, s => s.K, spinK, c => c.EditValue);
            BindToControl(settings, s => s.R, spinR, c => c.EditValue);
            BindToControl(settings, s => s.Tk, spinTk, c => c.EditValue);
            BindToControl(settings, s => s.X1, spinX1, c => c.EditValue);
            BindToControl(settings, s => s.Gsr, spinGsr, c => c.EditValue);
            BindToControl(settings, s => s.Psr, spinPsr, c => c.EditValue);
            BindToControl(settings, s => s.dkr, spindkr, c => c.EditValue);
            BindToControl(settings, s => s.da1, spinda1, c => c.EditValue);
            BindToControl(settings, s => s.tPros, spintPros, c => c.EditValue);
            BindToControl(settings, s => s.tStart, spintStart, c => c.EditValue);
        }
        
        //private void btSetStart_Click(object sender, EventArgs e)
        //{
        //    SetSynxTimeUserControl setSynx = new SetSynxTimeUserControl();
        //    setSynx.ShowDialog();
        //}
    }
}
