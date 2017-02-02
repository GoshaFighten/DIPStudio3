using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DIPStudioUICore;
using DIPStudioCore;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DIPStudioUICore.Extensions;

namespace DIPStudio3.Controls {
    public partial class ProjectControl : BaseUserControl {
        Operation exchangeBuffer;
        const string strColCaption = "Имя операции";
        const string strColStatus = "Статус выполнения";
        const string strColTimeElapsed = "Время выполнения";
        const string strColCurrentTime = "Текущий шаг";
        DIPApplication fApplication;

        public ProjectControl(DIPApplication application) {
            InitializeComponent();
            this.fApplication = application;
            colCaption.Caption = strColCaption;
            colStatus.Caption = strColStatus;
            colTimeElapsed.Caption = strColTimeElapsed;
            colCurrentTime.Caption = strColCurrentTime;
            colCurrentTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colCurrentTime.DisplayFormat.FormatString = "0 мс";
            colTimeElapsed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTimeElapsed.DisplayFormat.FormatString = "0 мс";
            repositoryItemImageComboBox1.Items.AddEnum<OperationStatus>(ConvertStatusValue);
            imageCollection1.AddImage(DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/next_16x16.png"));
            imageCollection1.AddImage(DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/apply_16x16.png"));
            imageCollection1.AddImage(DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/cancel_16x16.png"));
            foreach(DevExpress.XtraEditors.Controls.ImageComboBoxItem item in repositoryItemImageComboBox1.Items) {
                OperationStatus status = (OperationStatus)item.Value;
                item.ImageIndex = (int)status;
            }
            InitBar();
            gridView1.MakeGridReadOnly();
            gridControl1.ContextMenuStrip = contextMenuStrip1;
        }

        private void InitBar() {
            repositoryItemTextEdit1.SetIntMask();
            beiStart.Caption = "Начало, мс";
            beiD.Caption = "Дискретизация, мс";
            beiEnd.Caption = "Конец, мс";
        }

        private string ConvertStatusValue(OperationStatus value) {
            switch(value) {
                case OperationStatus.NotExecuted:
                    return "Не выполнено";
                case OperationStatus.Executed:
                    return "Выполнено";
                case OperationStatus.Error:
                    return "Ошибка";
            }
            throw new DIPStudiogException("Unexpected status type");
        }

        public void AssignProject(BindingList<Operation> project) {
            operationBindingSource.DataSource = project;
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) {
            if(!gridView1.IsValidRowHandle(e.RowHandle)) {
                return;
            }
            OperationStatus status = (OperationStatus)gridView1.GetRowCellValue(e.RowHandle, colStatus);
            switch(status) {
                case OperationStatus.NotExecuted:
                    break;
                case OperationStatus.Executed:
                    e.Appearance.BackColor = Color.Green;
                    break;
                case OperationStatus.Error:
                    e.Appearance.BackColor = Color.Red;
                    break;
            }
        }

        public bool ValidateTime() {
            if(Start < 0) {
                ShowError("Время начала не задано или меньше нуля!", beiStart);
                return false;
            }
            if(Start >= End) {
                ShowError("Время начала больше времени конца!", beiEnd);
                return false;
            }
            if(Discretization <= 0) {
                ShowError("Дискретизация должна быть больше нуля!", beiD);
                return false;
            }
            return true;
        }

        private void ShowError(string error, BarEditItem item) {
            DIPMessageBox.Show(this, error);
            ((ControlContainer)this.Parent).Panel.Show();
            BarEditItemLink link = (BarEditItemLink)item.Links[0];
            link.ShowEditor();
            link.ActiveEditor.ErrorText = error;
        }

        private int fStart = int.MinValue;
        public int Start {
            get { return fStart; }
            set {
                if(fStart == value)
                    return;
                ProjectManager.IsModified = true;
                fStart = value;
                beiStart.EditValue = fStart;
            }
        }
        private int fDiscretization;
        public int Discretization {
            get { return fDiscretization; }
            set {
                if(fDiscretization == value)
                    return;
                ProjectManager.IsModified = true;
                fDiscretization = value;
                beiD.EditValue = fDiscretization;
            }
        }
        private int fEnd;
        public int End {
            get { return fEnd; }
            set {
                if(fEnd == value)
                    return;
                ProjectManager.IsModified = true;
                fEnd = value;
                beiEnd.EditValue = fEnd;
            }
        }

        public DIPStudio3.ProjectManager ProjectManager { get; set; }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e) {
            if(ProjectManager.CurrentStep == null)
                return;
            if(gridView1.GetDataSourceRowIndex(e.RowHandle) == ProjectManager.CurrentStep.OperationIndex) {
                e.Painter.DrawObject(e.Info);
                Image image = DevExpress.Images.ImageResourceCache.Default.GetImage("images/navigation/forward_16x16.png");
                Rectangle rect = new Rectangle();
                rect.Width = image.Width;
                rect.Height = image.Height;
                rect.X = e.Bounds.X + (e.Bounds.Width - image.Width) / 2;
                rect.Y = e.Bounds.Y + (e.Bounds.Height - image.Height) / 2;
                e.Graphics.DrawImageUnscaled(image, rect);
                e.Handled = true;
            }
        }

        public void SetState(bool horiz) {
            colCurrentTime.Visible = horiz;
        }

        private void bei_EditValueChanged(object sender, EventArgs e) {
            if(sender == beiStart) {
                if(beiStart.EditValue == null)
                    Start = int.MinValue;
                Start = Convert.ToInt32(beiStart.EditValue);
            }
            else if(sender == beiEnd) {
                End = Convert.ToInt32(beiEnd.EditValue);
            }
            else {
                Discretization = Convert.ToInt32(beiD.EditValue);
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = gridView1.CalcHitInfo(gridControl1.PointToClient(Cursor.Position));
            if(!hi.InRow)
                return;
            Operation row = (Operation)gridView1.GetRow(hi.RowHandle);
            ProjectManager.SetupPlugin(row.Plugin, row.CurrentTime == null);
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fApplication.RemoveOperation(GetActiveOperation());
            ProjectManager.IsModified = true;
            this.Invalidate();
        }

        private void runProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ProjectManager.Run();
        }

        private void runByStepsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ProjectManager.RunByStep();
        }

        private void copyOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exchangeBuffer = GetActiveOperation().Clone();
        }

        private void pasteOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (exchangeBuffer != null) {
                fApplication.AddPluginToProject(fApplication.GetPluginByKey(exchangeBuffer.Plugin.Key));
                ProjectManager.IsModified = true;
            }
        }

        private void cutOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exchangeBuffer = GetActiveOperation();
            fApplication.RemoveOperation(exchangeBuffer.Clone());
        }
        Operation GetActiveOperation()
        {
            return (Operation)gridView1.GetFocusedRow();
        }
    }
}
