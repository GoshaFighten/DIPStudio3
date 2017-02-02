using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace DIPStudioCore {
    public interface IPlugin {
        string Key { get; }
        ProjectKind Kind { get; }
        string Caption { get; }
        string ShortName { get; }

        IPluginSettings PluginSettings { get; }
        bool Setup(bool modify);
        string GetHelp();
        void Run(int t, int index, int tFinish);
    }

    public enum ProjectKind {
        Modeling,
        Processing,
        Selection,
        View
    }

    public interface IPluginSettings {
        string ResultName { get; set; }
        Dictionary<string, object> Save();
        void Open(Dictionary<string, object> parameters);
        void RegisterOutputs(Dictionary<IPluginSettings, string> seriesMap, Dictionary<IPluginSettings, string> tableMap);
    }

    public abstract class PluginSettings : IPluginSettings {
        public PluginSettings(IPlugin plugin) {
            ResultName = string.Empty;
            fPlugin = plugin;
            
        }

        private IPlugin fPlugin;
        [SavePropertyAttribute(false)]
        public IPlugin Plugin {
            get { return fPlugin; }
        }

        public void RegisterOutputs(Dictionary<IPluginSettings, string> seriesMap, Dictionary<IPluginSettings, string> tableMap) {
            RegisterOutputSeries(seriesMap);
            RegisterOutputTables(tableMap);
        }

        protected virtual void RegisterOutputSeries(Dictionary<IPluginSettings, string> seriesMap) {
            if (IsResultSeries) {
                RegisterOutput(seriesMap, ResultName);
            }
        }

        protected virtual void RegisterOutputTables(Dictionary<IPluginSettings, string> tableMap) {
            if (!IsResultSeries) {
                RegisterOutput(tableMap, ResultName);
            }
        }

        protected void RegisterOutput(Dictionary<IPluginSettings, string> map, string name) {
            if (map.ContainsKey(this)) {
                map[this] = name;
            }
            else {
                map.Add(this, name);
            }
        }

        protected abstract bool IsResultSeries { get; }

        [DisplayName("Имя результата ")]
        public string ResultName { get; set; }

        string IPluginSettings.ResultName {
            get { return ResultName; }
            set { ResultName = value; }
        }

        private Dictionary<string, object> SaveCore(object objToSave) {
            Dictionary<string, object> list = new Dictionary<string, object>();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(objToSave);
            foreach (PropertyDescriptor property in properties) {
                if (property.Attributes.Contains(new SavePropertyAttribute(false))) {
                    continue;
                }
                object value = property.GetValue(objToSave);
                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string)) {
                    list.Add(property.Name, value);
                }
                else {
                    if (value is IList) {
                        IList lst = (IList)value;
                        Dictionary<string, object> items = new Dictionary<string, object>();
                        int i = 0;
                        foreach (System.Object item in lst) {
                            items.Add("Item" + i++, SaveCore(item));
                        }
                        list.Add(property.Name, items);
                    }
                    else {
                        list.Add(property.Name, SaveCore(value));
                    }
                }
            }
            return list;
        }

        Dictionary<string, object> IPluginSettings.Save() {
            return SaveCore(this);
        }

        private void OpenCore(Dictionary<string, object> parameters, object objToOpen) {
            Type property = objToOpen.GetType();
            if (!property.IsPrimitive && property != typeof(string) && typeof(IList).IsAssignableFrom(property)) {
                IList lst = (IList)objToOpen;
                if (!lst.GetType().IsGenericType)
                    throw new DIPStudiogException("Fucking list!");
                Type genericType = lst.GetType().GetGenericArguments()[0];
                foreach (KeyValuePair<string, object> parameter in parameters) {
                    System.Reflection.ConstructorInfo constructor = genericType.GetConstructor(new Type[0]);
                    object obj = constructor.Invoke(new object[0]);
                    OpenCore((Dictionary<string, object>)parameter.Value, obj);
                    lst.Add(obj);
                }
                return;
            }
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(objToOpen);
            foreach (KeyValuePair<string, object> parameter in parameters) {
                if (parameter.Value is Dictionary<string, object>) {
                    OpenCore((Dictionary<string, object>)parameter.Value, properties[parameter.Key].GetValue(objToOpen));
                }
                else {
                    properties[parameter.Key].SetValue(objToOpen, parameter.Value);
                }
            }
        }

        void IPluginSettings.Open(Dictionary<string, object> parameters) {
            OpenCore(parameters, this);
        }

        void IPluginSettings.RegisterOutputs(Dictionary<IPluginSettings, string> seriesMap, Dictionary<IPluginSettings, string> tableMap) {
            RegisterOutputs(seriesMap, tableMap);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SavePropertyAttribute : Attribute {
        public SavePropertyAttribute() {
            Visible = true;
        }

        public SavePropertyAttribute(bool visible) {
            fVisible = visible;
        }

        // Fields...
        private bool fVisible;
        public bool Visible {
            get { return fVisible; }
            set { fVisible = value; }
        }
    }
}
