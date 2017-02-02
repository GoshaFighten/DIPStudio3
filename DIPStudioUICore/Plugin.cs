using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;

namespace DIPStudioUICore {
    public abstract class Plugin<TSettings, TUserControl> : IPlugin
        where TSettings : PluginSettings
        where TUserControl : BasePluginUserControl {
        private PluginSettings settings;
        private ProjectKind kind;
        private Type userControlType;
        private string caption;
        private string shortName;
        public Plugin(ProjectKind kind, string caption, string shortName) {
            Type[] types = this.GetType().BaseType.GetGenericArguments();
            Type settingsType = types[0];
            this.settings = (PluginSettings)Activator.CreateInstance(settingsType, this);
            this.kind = kind;
            this.userControlType = types[1];
            this.caption = caption;
            this.shortName = shortName;
        }
        protected abstract string GetHelpString();
        protected abstract void Run(int t, int index, int tFinish);

        protected TSettings PluginSettings {
            get {
                return (TSettings)((IPlugin)this).PluginSettings;
            }
        }

        #region Члены IPlugin

        string IPlugin.Key {
            get { return this.GetType().Name; }
        }

        ProjectKind IPlugin.Kind {
            get { return kind; }
        }

        string IPlugin.Caption {
            get { return caption; }
        }

        IPluginSettings IPlugin.PluginSettings {
            get { return settings; }
        }

        bool IPlugin.Setup(bool modify) {
            using (BasePluginForm frm = GetForm(modify)) {
                return frm.ShowDialog() == System.Windows.Forms.DialogResult.OK;
            }
        }

        void IPlugin.Run(int t, int index, int tFinish) {
            this.Run(t, index, tFinish);
        }
        string IPlugin.GetHelp(){
            return caption + Environment.NewLine + this.GetHelpString();
        }
        public string ShortName
        {
            get { return shortName; }
        }

        #endregion

        protected virtual BasePluginForm GetForm(bool modify) {
            BasePluginUserControl uc = (BasePluginUserControl)Activator.CreateInstance(userControlType, this.settings);
            BasePluginForm frm = new BasePluginForm(uc, modify);
            CustomizeForm(frm);
            return frm;
        }

        protected virtual void CustomizeForm(BasePluginForm frm) {
        }
        }
}
