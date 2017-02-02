using System;
using System.ComponentModel;
using System.Windows.Forms;
using DIPStudioUICore;
using DevExpress.XtraBars.Docking2010.Views;
using GraphViewer;

namespace DIPStudio3.Controls
{
    public partial class GraphControl : BaseUserControl
    {

        private BindingList<GraphData> fTables = new BindingList<GraphData>();

        public BindingList<GraphData> Tables
        {
            get { return fTables; }
            set { fTables = value; }
        }

        public GraphControl()
        {
            InitializeComponent();
            AddTable();
            tabbedView1.CustomHeaderButtonClick += new CustomHeaderButtonEventHandler(tabbedView1_CustomHeaderButtonClick);
            tabbedView1.GotFocus += new EventHandler(tabbedView1_GotFocus);
        }

        void tabbedView1_GotFocus(object sender, EventArgs e)
        {
            ((GrViewer)tabbedView1.ActiveDocument.Control).UpDateGraphPane();
        }

        private void AddTable()
        {
            GraphData newGraphData = new GraphData();
            Tables.Add(newGraphData);
            AddPanel(newGraphData);
        }

        void tabbedView1_CustomHeaderButtonClick(object sender, CustomHeaderButtonEventArgs e)
        {
            if (e.Button.Caption == "Add") {
                AddTable();
            }
        }

        public void UpdatePanels()
        {
            while (tabbedView1.Documents.Count != 0) {
                tabbedView1.RemoveDocument(tabbedView1.Documents[0].Control);
            } // this bullshit replace clear method, because of stange bug, all documents becomes assigned with one control

            foreach (var item in Tables) {
                AddPanel(item);
            }
        }
        public void AddPanel(GraphData table)
        {
            GrViewer control = new GrViewer(table);
            control.Dock = DockStyle.Fill;
            control.Text = "Графики " + (Tables.IndexOf(table));
            tabbedView1.AddDocument(control);
        }

        private void tabbedView1_DocumentClosing(object sender, DocumentCancelEventArgs e)
        {
            GrViewer control = (GrViewer)e.Document.Control;
            if (Tables.Count == 1) {
                AddTable();
            }
            if (control != null)
                Tables.Remove(control.GraphStory);
        }
    }
}
