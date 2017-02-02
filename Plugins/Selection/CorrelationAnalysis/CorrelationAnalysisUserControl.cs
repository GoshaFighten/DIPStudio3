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

namespace CorrelationAnalysis {
    public partial class CorrelationAnalysisUserControl : BasePluginUserControl {
        public CorrelationAnalysisUserControl(CorrelationAnalysisSettings settings)
            : base(settings) {
            InitializeComponent();
            frameComboBox1.Properties.Application = DIPApplication.GetInstance();
            BindToControl(settings, s => s.InputSeries, frameComboBox1, c => c.EditValue);
            BindToControl(settings, s => s.NcsY, seX, c => c.EditValue);
            BindToControl(settings, s => s.NcsX, seY, c => c.EditValue);
            BindToControl(settings, s => s.StrobY, seXSize, c => c.EditValue);
            BindToControl(settings, s => s.StrobX, seYSize, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, textEdit5, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, s => s.Constt, spinEdit1, c => c.EditValue);
            BindToControl(settings, s => s.Uroven, spinEdit2, c => c.EditValue);
            BindToControl(settings, s => s.OBper, spinEdit3, c => c.EditValue);
            BindToControl(settings, s => s.PrKadr, spinEdit4, c => c.EditValue);
            BindToControl(settings, s => s.N, spinEdit5, c => c.EditValue);
            BindToControl(settings, s => s.PgrPO, spinEdit6, c => c.EditValue);
            BindToControl(settings, s => s.MethodBin, checkEdit2, c => c.Checked);
            BindToControl(settings, s => s.MethodPer, checkEdit4, c => c.Checked);
        }

        private void frameComboBox1_SelectedIndexChanged(object sender, EventArgs e) {            
            Draw();
        }

        private void Draw() {
            CorrelationAnalysisSettings settings = (CorrelationAnalysisSettings)Settings;
            string inputSeries = settings.InputSeries;
            Series series = fApplication.GetSeriesByName( inputSeries);
            if (series == null)
                throw new PluginException(string.Format("Series {0} does not exist", series));
            if (series.Count == 0)
                throw new PluginException(string.Format("Series {0} does not contain frames", series));
            Frame frame = series.GetElementByIndex(0);
            Image source = frame.Image;
            Bitmap bmp = new Bitmap(source.Width, source.Height);
            
            Graphics gr = Graphics.FromImage(bmp);
            gr.DrawImage(source, Point.Empty);
            gr.DrawRectangle(Pens.Red, settings.NcsX, settings.NcsY, settings.StrobX, settings.StrobY);
            gr.Dispose();

            pictureEdit1.Image = bmp;
        }

        private void StrobChanged(object sender, EventArgs e) {
            CorrelationAnalysisSettings settings = (CorrelationAnalysisSettings)Settings;
            Series series = fApplication.GetSeriesByName( settings.InputSeries);
            if (series != null && series.Count != 0)
                Draw();
        }


        //senichkin
        #region strobEvent

        private bool changeStrob = false;
        double scale = 1;

        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            RectangleF pictureRect = GetPictureEdit1ImageRect();
            if (changeStrob && pictureRect.Contains(e.Location))
            {
                seYSize.Value = (int)((e.Location.Y - (decimal)pictureRect.Y) * (decimal)scale - seY.Value);
                seXSize.Value = (int)((e.Location.X - (decimal)pictureRect.X) * (decimal)scale - seX.Value);
                Draw();
            }
        }
        private void OnPictureMouseDown(object sender, MouseEventArgs e)
        {
            RectangleF pictureRect = GetPictureEdit1ImageRect();
            if (pictureRect.Contains(e.Location))
            {
                seY.Value = (int)(e.Location.Y - (decimal)pictureRect.Y) * (decimal)scale;
                seX.Value = (int)(e.Location.X - (decimal)pictureRect.X) * (decimal)scale;
                changeStrob = true;
            }
        }

        private void OnPictureMouseUp(object sender, MouseEventArgs e)
        {
            changeStrob = false;
        }

        //bullshit method... find actual picture size in pictureEdit, and delete this module...
        private RectangleF GetPictureEdit1ImageRect()
        {
            RectangleF pictureRect = new Rectangle();
            if (pictureEdit1.Image != null)
            {
                SizeF originPictureSize = pictureEdit1.Image.PhysicalDimension;
                Size controlSize = pictureEdit1.Size;
                float pictureRatio = originPictureSize.Width / originPictureSize.Height;
                if (controlSize.Width > originPictureSize.Width && controlSize.Height > originPictureSize.Height)
                {
                    pictureRect = new RectangleF((controlSize.Width - originPictureSize.Width) / 2, (controlSize.Height - originPictureSize.Height) / 2, originPictureSize.Width, originPictureSize.Height);
                    scale = 1;
                }
                else
                {
                    if (controlSize.Width / pictureRatio > controlSize.Height)
                    {
                        pictureRect = new RectangleF((controlSize.Width - controlSize.Height * pictureRatio) / 2, 0, controlSize.Height * pictureRatio, controlSize.Height);
                        scale = originPictureSize.Width / pictureRect.Width;
                    }
                    else
                    {
                        pictureRect = new RectangleF(0, (controlSize.Height - controlSize.Width / pictureRatio) / 2, controlSize.Width, controlSize.Width / pictureRatio);
                        scale = originPictureSize.Height / pictureRect.Height;
                    }
                }

            }
            return pictureRect;
        }
        #endregion
    }
}
