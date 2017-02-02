using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DIPStudioCore;
using System.Windows.Forms;
using DIPStudioCore.Data;

namespace DIPStudioCore {
    public class DIPApplication {

        private DIPApplication() {
            project = new BindingList<Operation>();
            series = new BindingList<Series>();
            tables = new BindingList<Table>();
        }
        public bool CalculationStop;

        private static DIPApplication instance;
        public static DIPApplication  GetInstance() {
            if (instance == null) {
                instance = new DIPApplication();
            }
            return instance;
        }

        public void AddPluginToProject(IPlugin plugin) {
               AddPluginToProject(plugin, project.Count);
        }

        public void AddPluginToProject(IPlugin plugin, int index)
        {
            Operation operation = new Operation();
            operation.Plugin = plugin;
            operation.Status = OperationStatus.NotExecuted;
            operation.TimeElapsed = null;
            BindingList<Operation> resultList = new BindingList<Operation>();
            for (int i = 0; i < index; i++) {
                resultList.Add(project[i]);
            }
            resultList.Add(operation);
            for (int i = index; i < project.Count; i++) {
                resultList.Add(project[i]);
            }
            project.Clear();
            foreach (var item in resultList) {
                project.Add(item);
            }
            plugin.PluginSettings.RegisterOutputs(seriesMap, tableMap);
        }

        public void ClearProject() {
            project.Clear();
        }

        private BindingList<Operation> project;
        public BindingList<Operation> Project {
            get { return project; }
        }

        private BindingList<Series> series;
        public BindingList<Series> Series {
            get { return series; }
        }

        private BindingList<Table> tables;
        public BindingList<Table> Tables {
            get { return tables; }
        }

        public void AddSeries(Series series) {
            Series existingSeries = GetSeriesByName(series.Name);
            if (existingSeries == null) {
                this.Series.Add(series);
            }
        }

        public void AddTable(Table table) {
            Table existingTable = GetTableByName(table.Name);
            if (existingTable == null) {
                this.Tables.Add(table);
            }
        }
        //senichkin
        public void RemoveTable(Table table)
        {
            this.Tables.Remove(table);
            table.Dispose();
        }
        public void RemoveSeries(Series series)
        {
            this.Series.Remove(series);
            series.Dispose();

        }
        public void RemoveOperation(Operation operation)
        {
            this.project.Remove(operation);
        }

        private Dictionary<IPluginSettings, string> seriesMap = new Dictionary<IPluginSettings, string>();
        private Dictionary<IPluginSettings, string> tableMap = new Dictionary<IPluginSettings, string>();
        public Series GetSeriesByName(string seriesName) {
            foreach (Series series in Series) {
                if (series.Name == seriesName) {
                    return series;
                }
            }
            return null;
        }

        public Table GetTableByName(string tableName) {
            foreach (Table table in Tables) {
                if (table.Name == tableName) {
                    return table;
                }
            }
            return null;
        }

        public Series GetSeriesByNameOrCreateNew(string seriesName) {
            foreach (Series series in Series) {
                if (series.Name == seriesName) {
                    return series;
                }
            }
            return new Series() { Name = seriesName
            };
        }

        public Table GetTableByNameOrCreateNew(string tableName, string sourceSeriesName) {
            foreach (Table table in Tables) {
                if (table.Name == tableName) {
                    return table;
                }
            }
            return new Table(this.GetSeriesByName(sourceSeriesName)) { Name = tableName
            };
        }

        public bool CheckSeriesName(IPluginSettings settings) {
            return CheckName(settings, seriesMap);
        }

        public bool CheckTableName(IPluginSettings settings) {
            return CheckName(settings, tableMap);
        }

        private bool CheckName(IPluginSettings settings, Dictionary<IPluginSettings, string> map) {
            IPluginSettings existingSettings = (from kvp in map
                                                where kvp.Value == settings.ResultName
                                                select kvp.Key).FirstOrDefault();
            if (existingSettings == null) {
                return true;
            }
            return object.ReferenceEquals(settings, existingSettings);
        }

        public List<Series> GetSeriesList() {
            List<Series> list = new List<Series>();
            list.AddRange(Series);
            return list;
        }

        public List<Table> GetTableList() {
            List<Table> list = new List<Table>();
            list.AddRange(Tables);
            return list;
        }

        public Form MainForm { get; set; }
        public IPlugin GetPluginByKey(string key) {
            PluginEventArgs args = new PluginEventArgs() { Key = key
            };
            OnRequestPlugin(args);
            return args.Plugin;
        }

        protected virtual void OnRequestPlugin(PluginEventArgs e) {
            EventHandler<PluginEventArgs> handler = RequestPlugin;
            if (handler != null) {
                handler(this, e);
            }
        }

        public void SetProjectModified(bool modified) {
            OnProjectModified(modified);
        }

        protected virtual void OnProjectModified(bool modified) {
            EventHandler<ProjectModifiedEventArgs> handler = ProjectModified;
            if (handler != null) {
                handler(this, new ProjectModifiedEventArgs(modified));
            }
        }

        public event EventHandler<PluginEventArgs> RequestPlugin;
        public event EventHandler<ProjectModifiedEventArgs> ProjectModified;
        public bool SetupPlugin(IPlugin plugin, bool modify)
        {
            return plugin.Setup(modify);
        }

        public Rights Rights { get; private set; }

        public void SetRights(Rights rights) {
            Rights = rights;
            OnRightsSet();
        }

        public event EventHandler RightsSet;
        protected virtual void OnRightsSet() {
            EventHandler handler = RightsSet;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        bool fIsRun;
        public bool IsRun {
            get {
                return fIsRun;
            }
            set {
                if (fIsRun == value)
                    return;
                fIsRun = value;
                OnIsRunChanged();
            }
        }

        public event EventHandler IsRunChanged;

        private void OnIsRunChanged() {
            EventHandler handler = IsRunChanged;
            if (handler == null)
                return;
            handler(this, EventArgs.Empty);
        }

        public void Reset(bool clearMaps)
        {
            IsRun = false;
            if (!clearMaps)
                return;
            seriesMap.Clear();
            tableMap.Clear();
        }
    }

    public enum Rights {
        User,
        Admin
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UserAccessAttribute : Attribute {
    }

    public class PluginEventArgs : EventArgs {
        // Fields...
        private IPlugin fPlugin;
        private string fKey;
        public string Key {
            get { return fKey; }
            set { fKey = value; }
        }

        public IPlugin Plugin {
            get { return fPlugin; }
            set { fPlugin = value; }
        }
    }

    public class ProjectModifiedEventArgs : EventArgs {
        // Fields...
        private bool fModified;
        public bool Modified {
            get { return fModified; }
        }

        public ProjectModifiedEventArgs(bool modified) {
            fModified = modified;
        }
    }
}
