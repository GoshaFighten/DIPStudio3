using System;
using System.Collections.Generic;
using System.Linq;
using DIPStudioUICore;
using DIPStudioCore;
using System.Windows.Forms;
using System.Drawing;
using ImageViewer;
using GraphViewer;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System.IO;
using DIPStudio3.Controls;

namespace DIPStudio3 {
    public partial class MainForm : BaseForm {
        private string[] fInputArgs;

        private static string DIPStudioFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DIPStudio3";

        private string layoutPath = DIPStudioFolder + "\\DockingLayout.xml";

        DIPApplication application;

        List<IManager> managers;

        ImageManager imageManager;

        DataManager dataManager;

        PluginManager pluginManager;

        ProjectManager projectManager;

        RightsManager rightsManager;

        const string Caption = "DIPStudio3";

        public MainForm() {
            InitializeComponent();
            this.Icon = DIPStudio3.Properties.Resources.logo;
            this.Size = new Size(1300, 800);
        }

        public string[] InputArgs {
            get { return fInputArgs; }
            set { fInputArgs = value; }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            CreateApplication();
            CreateManagers();
            CreateControls();
            InitBars();
            AddDockPanels();
            LoadManagers();
            OpenProject();
            this.Text = GetCaption();
        }

        private string GetCaption() {
            return String.Format("{0} - {1}", projectManager.Name, Caption);
        }

        private void OpenProject() {
            if(InputArgs.Length == 0)
                return;
            projectManager.Open(InputArgs[0]);
        }

        private void LoadManagers() {
            foreach(IManager manager in managers) {
                manager.Load();
            }
        }

        private void CreateManagers() {
            managers = new List<IManager>();
            imageManager = new ImageManager(application);
            dataManager = new DataManager(application);
            dataManager.Changed += new EventHandler(dataManager_Changed);
            pluginManager = new PluginManager(application);
            projectManager = new ProjectManager(application);
            rightsManager = new RightsManager(application);
            managers.Add(imageManager);
            managers.Add(dataManager);
            managers.Add(pluginManager);
            managers.Add(projectManager);
            managers.Add(rightsManager);
        }

        void dataManager_Changed(object sender, EventArgs e)
        {
            UpdateDataManagerCommands();
        }

        private void UpdateDataManagerCommands()
        {
            UpdateExportCommand();
        }
        private void UpdateExportCommand()
        {
            bbiExport.Enabled = IsActivePanelData() && dataManager.CanExport();
            bbiSimpleExport.Enabled = IsActivePanelData();
        }

        private bool IsActivePanelData() {
            if (dockManager1.ActivePanel == null)
                return false;
            return dockManager1.ActivePanel.ID == dataManager.PanelID;
        }
        private void CreateApplication() {
            application = DIPApplication.CreateDIPApplication();
            application.MainForm = this;
            application.ProjectModified += new EventHandler<ProjectModifiedEventArgs>(application_ProjectModified);
            application.RightsSet += new EventHandler(application_RightsSet);
            application.IsRunChanged += new EventHandler(application_IsRunChanged);
        }

        void application_IsRunChanged(object sender, EventArgs e) {
            bciHoriz.Enabled = !application.IsRun;
        }

        void application_RightsSet(object sender, EventArgs e) {
            bsiRights.Caption = application.Rights.ToString();
        }

        void application_ProjectModified(object sender, ProjectModifiedEventArgs e) {
            this.Text = e.Modified ? GetCaption() + '*' : GetCaption();
        }

        private void CreateControls() {
            ImgViewer viewer = CreateImg();
            this.Controls.Add(viewer);
            imageManager.Viewer = viewer;
            viewer.Visible = true;
            projectManager.Viewer = CreateGraph();
            this.Controls.Add(projectManager.Viewer);

        }
        private ImgViewer CreateImg()
        {
            ImgViewer viewer = new ImgViewer();
            viewer.Name = "viewer";
            viewer.Dock = DockStyle.Fill;
            viewer.Scrollbars = true;
            viewer.OverImage += (s, e) => {
                ReportBright(e.Color);
                ReportImageCoord(e.Point.X, e.Point.Y);
            };
            viewer.FrameChanged += (s, e) => {
                if (e.Frame == null) {
                    SetEmptyImageName();
                    return;
                }
                bsiImage.Caption = e.Frame.Name;
                ReportTime(e.Frame.Time);
                imageManager.SetFocusFrame(e.Frame);
            };
            return viewer;
        }

        private GraphControl CreateGraph()
        {
            GraphControl graph = new GraphControl();
            graph.Name = "graphViewer";
            graph.Dock = DockStyle.Fill;
            graph.Visible = false;
            return graph;
        }

        private void InitBars() {
            InitStatusBar();
            InitMainMenu();
            InitToolbar();
        }

        const string horiz = "Покадрово";
        const string vert = "Пакетно";
        private void InitMainMenu() {
            bar2.Text = "Главное меню";
            bsiPanels.Caption = "Панели";
            bbiDefaultLayout.Caption = "Восстановить расположение панелей";
            bsiProject.Caption = "Проект";
            bsiExport.Caption = "Экспорт";
            bbiExport.Caption = "Экспорт";
            bbiSimpleExport.Caption = "Отладочный экспорт";
            SetBSIHorizText();
        }

        private void InitToolbar() {
            bar1.Text = "Панель инструментов";
            bbiRun.Caption = "Запустить";
            bbiRun.Hint = "Запустить весь проект";
            bbiStep.Caption = "Запустить по шагам";
            bbiStep.Hint = "Запустить проект по шагам";
            bbiReset.Caption = "Сбросить";
            bbiReset.Hint = "Сбросить проект";
            btnOpenProject.Caption = "Открыть";
            btnOpenProject.Hint = "Открыть проект";
            btnSaveProject.Caption = "Сохранить";
            btnSaveProject.Hint = "Сохранить проект";
            btnRights.Caption = "Права доступа";
            btnRights.Hint = "Права доступа (User/Admin)";
        }

        private void SetEmptyImageName() {
            bsiImage.Caption = "Нет изображения";
        }

        private void InitStatusBar() {
            bar3.Text = "Статус бар";
            SetBSIHorizText();
            SetEmptyImageName();
            SetEmptyBright();
            SetEmptyImageCoord();
            SetEmptyTime();
        }

        private void SetBSIHorizText() {
            bciHoriz.Caption = horiz;
            bciHoriz.Hint = horiz;
            bsiHoriz.Caption = horiz;
        }

        private void SetBSIVertText() {
            bciHoriz.Caption = vert;
            bciHoriz.Hint = vert;
            bsiHoriz.Caption = vert;
        }
        public void ChangeBSI(bool horiz)
        {
            projectManager.SetDirection(horiz);
            if (horiz)
                SetBSIHorizText();
            else
                SetBSIVertText();
        }

        private void SetEmptyBright() {
            bsiBright.Caption = "Яркость: -";
        }

        private void ReportBright(int b) {
            bsiBright.Caption = "Яркость: " + b;
        }

        private void SetEmptyTime()
        {
            bsiTime.Caption = "Время кадра: -";
        }

        private void ReportTime(int b)
        {
            bsiTime.Caption = "Время кадра: " + b;
        }

        private void SetEmptyImageCoord() {
            bsiX.Caption = "X: -";
            bsiY.Caption = "Y: -";
        }

        private void ReportImageCoord(int x, int y) {
            bsiX.Caption = "X: " + x;
            bsiY.Caption = "Y: " + y;
        }

        private void AddDockPanels() {
            foreach(IManager manager in managers) {
                if(manager.HasPanel())
                    manager.AddPanel(dockManager1);
            }
            RestoreDockingLayout();
        }

        private void bbiRun_ItemClick(object sender, ItemClickEventArgs e) {
            RunProject(false);
        }

        private void bbiStep_ItemClick(object sender, ItemClickEventArgs e) {
            RunProject(true);
        }

        private void RunProject(bool step) {
            if(!projectManager.CanRun())
                return;
            application.IsRun = true;
            if(step)
                projectManager.RunByStep();
            else
                projectManager.Run();
        }

        private void bbiReset_ItemClick(object sender, ItemClickEventArgs e) {
            ResetProject(false);
        }

        private void ResetProject(bool openProject) {
            projectManager.ResetProject(openProject);
        }

        private void bciHoriz_CheckedChanged(object sender, ItemClickEventArgs e) {
            bool horiz = ((BarCheckItem)e.Item).Checked; 
            ChangeBSI(horiz);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (projectManager != null)
                e.Cancel = !projectManager.SaveAndExit();
            if(!e.Cancel)
                SaveDockingLayout();
        }

        private void SaveDockingLayout() {
            if(!Directory.Exists(DIPStudioFolder))
                Directory.CreateDirectory(DIPStudioFolder);
            dockManager1.SaveLayoutToXml(layoutPath);
        }

        private void RestoreDockingLayout() {
            if(File.Exists(layoutPath))
                dockManager1.RestoreLayoutFromXml(layoutPath);
            else
                RestoreDefaultPanelLayout();
        }

        private void RestoreDefaultPanelLayout() {
            using (Stream s = GenerateStreamFromString(DIPStudio3.Properties.Resources.DefaultLayout)) {
                dockManager1.RestoreLayoutFromStream(s);
            }            
        }

        private void btnSaveProject_ItemClick(object sender, ItemClickEventArgs e) {
            projectManager.Save();
        }

        private void btnOpenProject_ItemClick(object sender, ItemClickEventArgs e) {
            projectManager.Open();
            ChangeBSI(projectManager.Horiz);
            bciHoriz.Checked = projectManager.Horiz;
        }

        private void bbiDefaultLayout_ItemClick(object sender, ItemClickEventArgs e) {
            RestoreDefaultPanelLayout();
        }

        private void btnRights_ItemClick(object sender, ItemClickEventArgs e) {
            rightsManager.ChangeRights();
        }

        private void bbiExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsActivePanelData())
                return;
            dataManager.ExportCurrentData();
        }

        private void dockManager1_ActivePanelChanged(object sender, ActivePanelChangedEventArgs e)
        {
            UpdateManagersCommands();
        }
        private void UpdateManagersCommands()
        {
            UpdateDataManagerCommands();
        }

        private void bbiSimpleExport_ItemClick(object sender, ItemClickEventArgs e) {
            if (!IsActivePanelData())
                return;
            dataManager.SimpleExportCurrentData();
        }

        private void btnSwitch_ItemClick(object sender, ItemClickEventArgs e)
        {
            imageManager.Viewer.Visible = !imageManager.Viewer.Visible;
            projectManager.Viewer.Visible = !projectManager.Viewer.Visible;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveOpenProjectHelper.SaveImg(imageManager.ImgControl.FocusTab());
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (bciHoriz.Caption == vert) {
                MessageBox.Show("Только для покадрового режима");
                return;//do nothing for vertical mode
            }
            SetNumber setNumber = new SetNumber("Kоличество шагов для выполнения");
            setNumber.ShowDialog();
            if (setNumber.DialogResult == System.Windows.Forms.DialogResult.OK)
                for (int i = 0; i < setNumber.DialogValue; i++) {
                    RunProject(true);
                }
        }

        public Stream GenerateStreamFromString(string s) {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
