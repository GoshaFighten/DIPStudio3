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
using DevExpress.XtraBars.Docking2010.Views;

namespace ModelImages
{
    public partial class AllDisturb : UserControl
    {
        private IPlugin Plugin;

        public BindingList<ModelDisturbance> Disturbs;

        public AllDisturb(ModelImagesSettings settings)
        {
            Plugin = settings.Plugin;
            Disturbs = settings.Disturbs;
            InitializeComponent();
            AddDisturb();
            tabbedView1.CustomHeaderButtonClick += new CustomHeaderButtonEventHandler(tabbedView1_CustomHeaderButtonClick);
        }
        private void AddDisturb()
        {
            ModelDisturbance newModelDisturbance = new ModelDisturbance(Plugin);
            Disturbs.Add(newModelDisturbance);
            AddPanel(newModelDisturbance);
        }

        private void AddPanel(ModelDisturbance newModelDisturbance)
        {
            DisturbanceUserControl control = new DisturbanceUserControl(newModelDisturbance);
            //control.Dock = DockStyle.Fill;
            control.Text = "Помеха " + (Disturbs.IndexOf(newModelDisturbance));
            tabbedView1.AddDocument(control);
        }
        void tabbedView1_CustomHeaderButtonClick(object sender, CustomHeaderButtonEventArgs e)
        {
            if (e.Button.Caption == "Add") {
                AddDisturb();
            }
        }
    }
}
