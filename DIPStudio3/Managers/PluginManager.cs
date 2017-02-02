using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars.Docking;
using DIPStudio3.Controls;
using DIPStudioCore;
using System.IO;
using System.Reflection;

namespace DIPStudio3 {
    public class PluginManager : IManager {
        private DIPApplication application;

        PluginControl control;

        public void Load() {
            control.Plugins = plugins;
        }

        public PluginManager(DIPApplication application) {
            this.application = application;
            application.RequestPlugin += application_RequestPlugin;
            plugins = new List<IPlugin>();
            LoadPlugins();
        }

        void application_RequestPlugin(object sender, PluginEventArgs e) {
            e.Plugin = GetPluginByKey(e.Key);
        }

        public bool HasPanel() {
            return true;
        }

        public DockPanel AddPanel(DockManager dockManager) {
            DockPanel panel = dockManager.AddPanel(DockingStyle.Left);
            panel.ID = PanelID;
            panel.Text = "Плагины";
            panel.Image = DevExpress.Images.ImageResourceCache.Default.GetImage("images/programming/technology_16x16.png");
            control = CreateControl(panel);
            return panel;
        }

        private PluginControl CreateControl(DockPanel panel) {
            PluginControl control = new PluginControl();
            control.Dock = System.Windows.Forms.DockStyle.Fill;
            control.PluginChosen += control_PluginChosen;
            panel.ControlContainer.Controls.Add(control);
            return control;
        }

        void control_PluginChosen(object sender, PluginEventArgs e) {
            IPlugin plugin = GetPluginByKey(e.Key);
            if(application.SetupPlugin(plugin, true)) {
                application.AddPluginToProject(plugin);
            }
        }

        public IPlugin GetPluginByKey(string key) {
            IPlugin plugin = plugins.FirstOrDefault(p => p.Key == key);
            if(plugin == null)
                return null;
            return (IPlugin)Activator.CreateInstance(plugin.GetType());
        }

        List<IPlugin> plugins;

        private void LoadPlugins() {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "Plugins";
            if(!Directory.Exists(dir)) {
                DIPStudioUICore.DIPMessageBox.Show(application.MainForm, "Папка Plugins не существует");
                return;
            }
            String[] files = Directory.GetFiles(dir, "*.dll");
            if(files.Length == 0) {
                DIPStudioUICore.DIPMessageBox.Show(application.MainForm, "Папка Plugins пуста");
                return;
            }
            foreach(String file in files) {
                Assembly assembly = Assembly.LoadFile(file);
                Type[] types = new Type[0];
                try {
                    types = assembly.GetExportedTypes();
                }
                catch (FileNotFoundException e) {
                    LogManager.ReportError("Ошибка плагина " + file);
                    LogManager.ProcessError(e);
                    continue;//senichkin
                }
                catch (FileLoadException e) {
                    LogManager.ReportError("Версия плагина " + file + " не совпадает с версией программы");
                    LogManager.ProcessError(e);
                    continue;//senichkin
                }

                foreach(Type type in types) {
                    if(type.IsClass && typeof(IPlugin).IsAssignableFrom(type)) {
                        ConstructorInfo ctor = type.GetConstructor(new Type[0]);
                        if (ctor == null)
                            continue;
                        IPlugin plugin = (IPlugin)ctor.Invoke(null);
                        if(ContainsKey(plugin.Key)) {
                            LogManager.ReportError(string.Format("Плагин с ключом {0} уже загружен", plugin.Key));
                            continue;
                        }
                        plugins.Add(plugin);
                    }
                }
            }
        }

        private bool ContainsKey(string key) {
            return (from p in plugins
                    where p.Key == key
                    select p).ToList().Count > 0;
        }


        public Guid PanelID
        {
            get { return Guid.Parse("4a6ba890-36f1-4de9-b848-21c59826295a"); }
        }


        public event EventHandler Changed;
    }
}
