using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars.Docking;
using DIPStudio3.Controls;
using DIPStudioCore;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.Serialization;
using DIPStudioUICore;
using DIPStudioCore.Data;

namespace DIPStudio3 {
    public class ProjectManager : IManager {
        private bool fIsModified = false;

        ProjectControl control;

        public void Load() {
            control.AssignProject(Operations);
            SetDirection(true);
        }
        private GraphControl fViewer;

        public GraphControl Viewer
        {
            get { return fViewer; }
            set { fViewer = value; }
        }
        DIPApplication application;

        public ProjectManager(DIPApplication application) {
            this.application = application;
            NewProject();
            Operations.ListChanged += new ListChangedEventHandler(Project_ListChanged);
        }

        private void NewProject() {
            Name = "Новый проект";
        }

        void Project_ListChanged(object sender, ListChangedEventArgs e) {
            if (e.ListChangedType != ListChangedType.ItemAdded)
                return;
            IsModified = true;
            if(currentStep != null && currentStep is HorizStep && currentStep.TimeIndex > 0) {
                DIPMessageBox.Show(application.MainForm, "Действие добавлено не на первом шаге. Необходимо сбросить проект");
                ResetProject(false);
            }
        }

        public bool HasPanel() {
            return true;
        }

        public DockPanel AddPanel(DockManager dockManager) {
            DockPanel panel = dockManager.AddPanel(DockingStyle.Left);
            panel.ID = PanelID;
            panel.Text = "Проект";
            panel.Image = DevExpress.Images.ImageResourceCache.Default.GetImage("images/tasks/task_16x16.png");
            control = CreateControl(panel);
            return panel;
        }

        private ProjectControl CreateControl(DockPanel panel) {
            ProjectControl control = new ProjectControl(application);
            control.ProjectManager = this;
            control.Dock = DockStyle.Fill;
            panel.ControlContainer.Controls.Add(control);
            return control;
        }

        private void ShowWaitForm() {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(application.MainForm, typeof(WaitForm1), true, true);
            application.CalculationStop = false;
        }

        private static void CloseWaitForm() {
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
        }

        private static void ReportProgress(int progress) {
            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SendCommand(DIPStudio3.WaitForm1.WaitFormCommand.ReportProgress, progress);
        }

        private int GetFrameCount() {
            return (EndTime - StartTime) / Discretization + 1;
        }

        bool horiz;

        public void SetDirection(bool horiz) {
            this.horiz = horiz;
            control.SetState(horiz);
        }

        public void Run() {
            Step previousStep = currentStep;
            if(currentStep == null) {
                ResetTime();
                if(Horiz)
                    currentStep = new HorizStep(Operations, GetFrameCount(), EndTime) { OperationIndex = 0, TimeIndex = 0
                    };
                else
                    currentStep = new VertStep(Operations, GetFrameCount(), EndTime) { OperationIndex = 0, TimeIndex = 0
                    };
            }
            else {
                currentStep = currentStep.GetNextStep();
            }
            LockControl(application.MainForm, true);
            ShowWaitForm();
            if(previousStep != null)
                ReportProgress(previousStep.Progress);
            while(currentStep != null && !application.CalculationStop) {
                try {
                    currentStep.DoStep(StartTime, Discretization);
                    ReportProgress(currentStep.Progress);
                    currentStep = currentStep.GetNextStep();
                }
                catch(DIPStudiogException ex) {
                    CloseWaitForm();
                    LockControl(application.MainForm, false);
                    currentStep.SetError(ex, application.MainForm);
                    currentStep = null;
                    LogManager.ReportError(ex.Message);
                }
            }
            CloseWaitForm();
            LockControl(application.MainForm, false);
            //currentStep = null;
        }

        Step currentStep;

        public Step CurrentStep {
            get { return currentStep; }
        }

        public bool CanRun() {
            return control.ValidateTime() && CheckProjectLength();
        }

        private bool CheckProjectLength() {
            bool result = Operations.Count > 0;
            if(!result)
                DIPMessageBox.Show(application.MainForm, "Проект пуст");
            return result;
        }

        public void RunByStep() {
            Step previousStep = currentStep;
            if(currentStep == null) {
                ResetTime();
                if(Horiz)
                    currentStep = new HorizStep(Operations, GetFrameCount(), EndTime) { OperationIndex = 0, TimeIndex = 0
                    };
                else
                    currentStep = new VertStep(Operations, GetFrameCount(), EndTime) { OperationIndex = 0, TimeIndex = 0
                    };
            }
            else {
                currentStep = currentStep.GetNextStep();
            }
            LockControl(application.MainForm, true);
            ShowWaitForm();
            if(previousStep != null)
                ReportProgress(previousStep.Progress);
            try {
                if(currentStep != null) {
                    currentStep.DoStep(StartTime, Discretization);
                    ReportProgress(currentStep.Progress);
                }
                //if(currentStep.IsEnd)
                //    currentStep = null;
                CloseWaitForm();
                LockControl(application.MainForm, false);
            }
            catch(DIPStudiogException ex) {
                CloseWaitForm();
                LockControl(application.MainForm, false);
                currentStep.SetError(ex, application.MainForm);
                currentStep = null;
            }
        }

        public abstract class Step {
            private BindingList<Operation> fProject;

            private int fFrameCount;

            private int fTimeIndex;

            private int fEndTime;

            private int fOperationIndex;

            public int OperationIndex {
                get { return fOperationIndex; }
                set { fOperationIndex = value; }
            }

            public int TimeIndex {
                get { return fTimeIndex; }
                set { fTimeIndex = value; }
            }
            public int EndTime
            {
                get { return fEndTime; }
                set { fEndTime = value; }
            }

            public int FrameCount {
                get { return fFrameCount; }
            }

            public BindingList<Operation> Project {
                get { return fProject; }
            }

            //public bool IsEnd {
            //    get { return (OperationIndex == (Project.Count - 1)) && (TimeIndex == (FrameCount - 1)); }
            //}

            public abstract Step GetNextStep();

            public abstract int Progress { get; }

            public Step(BindingList<Operation> project, int frameCount, int finishTime) {
                fProject = project;
                fFrameCount = frameCount;
                fEndTime = finishTime;
            }

            public virtual void DoStep(int start, int discretization) {
                if(Project.Count <= OperationIndex)
                    throw new StepErrorException("Проект пуст");
            }

            public void SetError(Exception error, IWin32Window owner) {
                DIPMessageBox.Show(owner, error.Message);
                if(!(error is StepErrorException))
                    Project[OperationIndex].Status = OperationStatus.Error;
            }

            private class StepErrorException : DIPStudiogException {
                public StepErrorException() {
                }

                public StepErrorException(string message)
                    : base(message) {
                }

                public StepErrorException(string message, Exception innerException)
                    : base(message, innerException) {
                }

                protected StepErrorException(SerializationInfo info, StreamingContext context)
                    : base(info, context) {
                }
            }
        }

        public class HorizStep : Step {
            public HorizStep(BindingList<Operation> project, int frameCount, int endTime)
                : base(project, frameCount, endTime) {
            }

            public override Step GetNextStep() {
                if(OperationIndex == Project.Count - 1) {
                    if(TimeIndex == FrameCount - 1) {
                        return null;
                    }
                    else {
                        return new HorizStep(Project, FrameCount, EndTime) { OperationIndex = 0, TimeIndex = this.TimeIndex + 1
                        };
                    }
                }
                else {
                    return new HorizStep(Project, FrameCount, EndTime) { TimeIndex = this.TimeIndex, OperationIndex = this.OperationIndex + 1
                    };
                }
            }

            public override int Progress {
                get { return (TimeIndex * Project.Count + OperationIndex + 1) * 100 / Project.Count / FrameCount; }
            }

            public override void DoStep(int start, int discretization) {
                base.DoStep(start, discretization);
                Operation operation = Project[OperationIndex];
                if(operation.Status != OperationStatus.Executed)
                    operation.Execute(TimeIndex * discretization + start, TimeIndex, EndTime);
                if (TimeIndex == FrameCount - 1 && operation.Status != OperationStatus.Error)
                    operation.Status = OperationStatus.Executed;
                Application.DoEvents();
            }
        }

        public class VertStep : Step {
            public VertStep(BindingList<Operation> project, int frameCount, int endTime)
                : base(project, frameCount, endTime)
            {
            }

            public override Step GetNextStep() {
                if(OperationIndex == Project.Count - 1)
                    return null;
                return new VertStep(Project, FrameCount, EndTime) { OperationIndex = this.OperationIndex + 1
                };
            }

            public override int Progress {
                get { return (OperationIndex + 1) * 100 / Project.Count; }
            }

            public override void DoStep(int start, int discretization) {
                base.DoStep(start, discretization);
                Operation operation = Project[OperationIndex];
                if(operation.Status != OperationStatus.Executed) {
                    for(TimeIndex = 0; TimeIndex < FrameCount; TimeIndex++) {
                        operation.Execute(TimeIndex * discretization + start, TimeIndex, EndTime);
                        Application.DoEvents();
                    }
                    TimeIndex--;
                }
                if (operation.Status != OperationStatus.Error)
                    operation.Status = OperationStatus.Executed;
            }
        }

        private void ResetTime() {
            foreach(Operation operation in Operations) {
                if(operation.Status != OperationStatus.Executed) {
                    operation.TimeElapsed = null;
                    operation.CurrentTime = null;
                }
            }
        }

        private void LockControl(Control control, bool locked) {
            foreach(Control ctrl in control.Controls) {
                ctrl.Enabled = !locked;
                LockControl(ctrl, locked);
            }
        }

        public void ResetProject(bool openProject) {
            foreach(Operation operation in Operations) 
                operation.Status = OperationStatus.NotExecuted;
            //incapsulation mistake..
            List<Series> ser = application.GetSeriesList();
            foreach (Series series in ser)
                application.RemoveSeries(series);
            List<Table> tab = application.GetTableList();
            foreach (Table table in tab)
                application.RemoveTable(table);
            ResetTime();
            application.Reset(openProject);
            currentStep = null;
        }

        public bool Save() {
            if(control.ValidateTime())
                return SaveOpenProjectHelper.Save(this);
            return false;
        }

        public bool SaveAndExit() {
            if(!IsModified)
                return true;
            switch(ShowShouldSaveForm()) {
                case DialogResult.Cancel:
                    return false;
                case DialogResult.No:
                    break;
                default:
                    if(!Save())
                        return false;
                    break;
            }
            return true;
        }

        public void Open() {
            if(SaveAndExit())
                SaveOpenProjectHelper.Open(this);
        }

        public DialogResult ShowShouldSaveForm() {
            return DIPMessageBox.ShowThreeButtonDialog(application.MainForm, "Сохранить текущий проект?");
        }

        public void Open(string file) {
            SaveOpenProjectHelper.Open(this, file);
        }

        public int EndTime {
            get { return control.End; }
            set { control.End = value; }
        }

        public int StartTime {
            get { return control.Start; }
            set { control.Start = value; }
        }

        public int Discretization {
            get { return control.Discretization; }
            set { control.Discretization = value; }
        }

        public bool Horiz {
            get { return horiz; }
            set { horiz = value; }
        }

        public BindingList<Operation> Operations {
            get { return application.Project; }
        }

        public void AddOperation(string key, Dictionary<string, object> parameters) {
            IPlugin plugin = application.GetPluginByKey(key);
            plugin.PluginSettings.Open(parameters);
            application.AddPluginToProject(plugin);
        }

        public void ClearOperations() {
            application.ClearProject();
        }

        public bool IsModified {
            get { return fIsModified; }
            set {
                fIsModified = value;
                application.SetProjectModified(IsModified);
            }
        }

        public string Name { get; set; }

        public void SetupPlugin(IPlugin plugin, bool modify) {
            if(application.SetupPlugin(plugin, modify))
                this.IsModified = true;
        }


        public Guid PanelID
        {
            get { return Guid.Parse("38fd6aa2-8fbd-4b11-a3d5-f6a13f277940"); }
        }


        public event EventHandler Changed;
    }
}
