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

namespace DymAnaliz3
{
    public partial class DymAnaliz3UserControl : BasePluginUserControl
    {

        //private Rectangle PictureSize;
        public DymAnaliz3UserControl(DymAnaliz3Settings settings)
            : base(settings) {
            InitializeComponent();
            frameComboBox1.Properties.Application = DIPApplication.GetInstance();
            BindToControl(settings, s => s.InputSeries, frameComboBox1, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, textEdit1, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, s => s.ResultData, textEdit2, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, s => s.NcsY, seY, c => c.EditValue);
            BindToControl(settings, s => s.NcsX, seX, c => c.EditValue);
            BindToControl(settings, s => s.StrobY, seYSize, c => c.EditValue);
            BindToControl(settings, s => s.StrobX, seXSize, c => c.EditValue);
            BindToControl(settings, s => s.NcsYzas, seYzas, c => c.EditValue);
            BindToControl(settings, s => s.NcsXzas, seXzas, c => c.EditValue);
            BindToControl(settings, s => s.StrobYzas, seYzasSize, c => c.EditValue);
            BindToControl(settings, s => s.StrobXzas, seXzasSize, c => c.EditValue);
            BindToControl(settings, s => s.newSeries, ceNewSeries, c => c.EditValue);
            BindToControl(settings, s => s.IsZasUse, ceZasUse, c => c.EditValue);
        }
        private void StrobChanged(object sender, EventArgs e)
        {
            Draw();
        }
        private void pictureEdit1_SizeChanged (object sender, EventArgs e)
        {
            RectangleF rect = GetPictureEdit1ImageRect();
        }

        private void frameComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            DymAnaliz3Settings settings = (DymAnaliz3Settings)Settings;
            string inputSeries = settings.InputSeries;
            Series series = fApplication.GetSeriesByName( inputSeries);
            if (series != null && series.Count != 0) {
                Frame frame = series.GetElementByIndex(0);
                DymAnaliz3 plugin= (DymAnaliz3)settings.Plugin;
                pictureEdit1.Image = plugin.DrawStrob(frame, settings);
            }
        }
        //senichkin
        #region strobEvent  

        private bool changeStrob = false;
        private bool secondStrob = false;

        double scale = 1;

        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            RectangleF pictureRect = GetPictureEdit1ImageRect();
            if (changeStrob && pictureRect.Contains(e.Location))
            {
                if (secondStrob) {
                    seYzasSize.Value = (int)((e.Location.Y - (decimal)pictureRect.Y) * (decimal)scale - seYzas.Value);
                    seXzasSize.Value = (int)((e.Location.X - (decimal)pictureRect.X) * (decimal)scale - seXzas.Value);
                    changeStrob = true;
                }
                else {
                    seYSize.Value = (int)((e.Location.Y - (decimal)pictureRect.Y) * (decimal)scale - seY.Value);
                    seXSize.Value = (int)((e.Location.X - (decimal)pictureRect.X) * (decimal)scale - seX.Value);
                    changeStrob = true;
                }
                Draw();
            }
        }
        private void OnPictureMouseDown(object sender, MouseEventArgs e)
        {
            RectangleF pictureRect = GetPictureEdit1ImageRect();
            if (pictureRect.Contains(e.Location))
            {
                if (secondStrob) {
                    seYzas.Value = (int)(e.Location.Y - (decimal)pictureRect.Y) * (decimal)scale;
                    seXzas.Value = (int)(e.Location.X - (decimal)pictureRect.X) * (decimal)scale;
                    changeStrob = true;
                }
                else {
                    seY.Value = (int)(e.Location.Y - (decimal)pictureRect.Y) * (decimal)scale;
                    seX.Value = (int)(e.Location.X - (decimal)pictureRect.X) * (decimal)scale;
                    changeStrob = true;
                }
            }
        }

        private void OnPictureMouseUp(object sender, MouseEventArgs e)
        {
            changeStrob = false;
            if (ceZasUse.Checked)
                secondStrob = !secondStrob;
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

        private void ceZasUse_CheckedChanged(object sender, EventArgs e)
        {
            seYzas.Enabled = ceZasUse.Checked;
            seXzas.Enabled = ceZasUse.Checked;
            seYzasSize.Enabled = ceZasUse.Checked;
            seXzasSize.Enabled = ceZasUse.Checked;
        }
    }
}
