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

namespace BrightProcessing
{
    public partial class BrightProcessingUserControl : BasePluginUserControl
    {
        public BrightProcessingUserControl(BrightProcessingSettings settings)
            : base(settings) 
        {
            InitializeComponent();
            frameComboBox1.Properties.Application = DIPApplicationBase.GetInstance();
            BindToControl(settings, s => s.InputSeries, frameComboBox1, c => c.EditValue);
            BindToControl(settings, s => s.X1, seX1, c => c.EditValue);
            BindToControl(settings, s => s.Y1, seY1, c => c.EditValue);
            BindToControl(settings, s => s.X2, seX2, c => c.EditValue);
            BindToControl(settings, s => s.Y2, seY2, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, textEdit1, c => c.EditValue, settings.Plugin.ShortName);
            Draw();
            decimal maxVal = ImageMath.ImageDepth;
            this.seX1.Properties.MaxValue = maxVal;
            this.seY1.Properties.MaxValue = maxVal;
            this.seX2.Properties.MaxValue = maxVal;
            this.seY2.Properties.MaxValue = maxVal;
        }
        
        private void Draw()
        {
            BrightProcessingSettings settings = (BrightProcessingSettings)Settings;
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            Series inputSeries = application.GetSeriesByName( settings.InputSeries);

            Bitmap bmp = new Bitmap(peGraf.Width, peGraf.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            Graphics gr = Graphics.FromImage(bmp);
            using (Pen pencil = new Pen(Color.Red, 2))
            {
                double kx = (double)ImageMath.ImageDepth / (double)peGraf.Width;
                double ky = (double)ImageMath.ImageDepth / (double)peGraf.Height;
                double x1 = 0;
                double y1 = (double)peGraf.Height;
                double x2 = settings.X1 / kx;
                double y2 = (ImageMath.ImageDepth - settings.Y1) / ky;
                double x3 = settings.X2 / kx;
                double y3 = (ImageMath.ImageDepth - settings.Y2) / ky;
                double x4 = (double)peGraf.Width;
                double y4 = 0;
                gr.DrawLine(pencil, (int)x1, (int)y1, (int)x2, (int)y2);
                gr.DrawLine(pencil, (int)x2, (int)y2, (int)x3, (int)y3);
                gr.DrawLine(pencil, (int)x3, (int)y3, (int)x4, (int)y4);
            }
            if (inputSeries != null && inputSeries.Count != 0)
            {
                BrightProcessing example = (BrightProcessing) settings.Plugin;
                seriesView1.SetImage(ImageMath.GetProcess(inputSeries.GetElementByIndex(0), example.Logic, 1, seriesView1.Number).Image);
            }
            peGraf.Image = bmp;
            gr.Dispose();
        }
        private void se_EditValueChanged(object sender, EventArgs e)
        {
            this.seX1.Properties.MaxValue = this.seX2.Value;
            this.seX2.Properties.MinValue = this.seX1.Value; 
            Draw();
        }
    }
}
