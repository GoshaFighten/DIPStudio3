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
using AviFile;

namespace Avi2Bmp{
    public partial class Avi2BmpUserControl : BasePluginUserControl {
        public Avi2BmpUserControl(Avi2BmpSettings settings)
            : base(settings) {
            InitializeComponent();
            BindToControl(settings, s => s.ResultName, teSeriesName, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, s => s.InputSeries, beSourcePath, c => c.EditValue);
            BindToControl(settings, s => s.SavePath, beSavePath, c => c.EditValue);
            BindToControl(settings, s => s.StartTime, seStart, c => c.EditValue);
            BindToControl(settings, s => s.FinishTime, seFinish, c => c.EditValue);
        }
        private const string aviExtension = "avi";
        private const string aviFilter = "video (*.avi)|*.avi";

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (OpenFileDialog dialog = GetOpenFileDialog()) {
                if (dialog.ShowDialog() != DialogResult.OK) {
                    return;
                }
                beSourcePath.Text = dialog.FileName;
            }
        }

        private void beSavePath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog()) {
                if (dialog.ShowDialog() != DialogResult.OK) {
                    return;
                }
                beSavePath.Text = dialog.SelectedPath;
            }
        }
        private static OpenFileDialog GetOpenFileDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = aviExtension;
            dialog.Filter = aviFilter;
            dialog.Title = "Выбор видео ...";
            dialog.Multiselect = false;
            return dialog;
        }

        private void beSourcePath_EditValueChanged(object sender, EventArgs e)
        {
            if (beSourcePath.Text != String.Empty) {
                try {
                    AviManager aviManager = new AviManager(beSourcePath.Text, true);
                    VideoStream stream = aviManager.GetVideoStream();
                    stream.GetFrameOpen();
                    lcFrameRate.Text = "Частота выбранного файла " + stream.FrameRate.ToString() + " кадров в секунду";
                    decimal discretisation = (decimal)(1000 / stream.FrameRate);
                    seFinish.Properties.Increment = seStart.Properties.Increment = discretisation;
                    seFinish.Properties.MaxValue = seFinish.Value = (stream.CountFrames - 1) * discretisation;
                    stream.GetFrameClose();
                    aviManager.Close();
                }
                catch {
                    throw new PluginException("stream open mistake");
                }
            }
        }

        private void seFinish_EditValueChanged(object sender, EventArgs e)
        {
            seStart.Value = seStart.Value - seStart.Value % 40;
            seFinish.Value = seFinish.Value - seFinish.Value % 40;
            seFinish.Properties.MinValue = seStart.Value;
            seStart.Properties.MaxValue = seFinish.Value;
            lcFrameCount.Text = "Будет обработано " + ((seFinish.Value - seStart.Value) / seFinish.Properties.Increment) + " кадров";
        }
    }
}
  