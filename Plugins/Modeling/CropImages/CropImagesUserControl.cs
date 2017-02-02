using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;
using DIPStudioCore.Data;
using DIPStudioCore;

namespace CropImages
{
    public partial class CropImagesUserControl : BasePluginUserControl
    {
        public CropImagesUserControl(CropImagesSettings settings)
            : base(settings)
        {
            InitializeComponent();
            cmbInput.Properties.Application = DIPApplication.GetInstance();
            BindToControl(settings, s => s.InputSeries, cmbInput, c => c.EditValue);
            BindToControl(settings, s => s.X, seX, c => c.EditValue);
            BindToControl(settings, s => s.Y, seY, c => c.EditValue);
            BindToControl(settings, s => s.Width, seXSize, c => c.EditValue);
            BindToControl(settings, s => s.Height, seYSize, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, textEdit1, c => c.EditValue, settings.Plugin.ShortName);
        }

        private void cmbInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            CropImagesSettings settings = (CropImagesSettings)Settings;
            string inputSeries = settings.InputSeries;
            Series series = fApplication.GetSeriesByName( inputSeries);
            if (series != null && series.Count() != 0){
            
                Frame sourceFrame = series.GetElementByIndex(0);
                Bitmap tempImg = new Bitmap(sourceFrame.Image.Width, sourceFrame.Image.Height);
                Graphics gr = Graphics.FromImage(tempImg);
                gr.DrawImage(sourceFrame.Image, Point.Empty);
                gr.DrawRectangle(Pens.Red, settings.X, settings.Y, settings.Width, settings.Height);
                gr.Dispose();
                peResult.Image = tempImg;
            }
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
            if (peResult.Image != null)
            {
                SizeF originPictureSize = peResult.Image.PhysicalDimension;
                Size controlSize = peResult.Size;
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
