using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;
using DIPStudioCore.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DIPStudioUICore.Extensions;
using DevExpress.XtraEditors;

namespace DIPStudio3.Controls
{
    public partial class DataControl : BaseUserControl
    {
        public DataControl()
        {
            InitializeComponent();
        }

        private BindingList<Table> fTables;

        public BindingList<Table> Tables
        {
            get { return fTables; }
            set
            {
                if (fTables != null) {
                    fTables.ListChanged -= TableListChanged;
                }
                fTables = value;
                fTables.ListChanged += TableListChanged;
                RePopulatePages();
            }
        }

        void TableListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded) {
                AddTab(Tables[e.NewIndex]);
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted) {
                foreach (string str in GetDifference())
                    RemoveTab(str);
            }
        }

        private void RePopulatePages()
        {
            foreach (DevExpress.XtraTab.XtraTabPage page in xtraTabControl1.TabPages) {
                page.Controls[0].Dispose();
            }
            xtraTabControl1.TabPages.Clear();
            foreach (Table table in Tables) {
                AddTab(table);
            }
        }
        
        private void AddTab(Table table)
        {
            DevExpress.XtraTab.XtraTabPage page = xtraTabControl1.TabPages.Add();
            page.Text = table.Name;
            if (table.SourceSeries != null) {
                LabelControl label = new LabelControl();
                label.Text = "Source table: " + table.SourceSeries.Name;
                label.Dock = DockStyle.Top;
                page.Controls.Add(label);
            }
            GridControl grid = new GridControl();
            grid.Dock = DockStyle.Fill;
            page.Controls.Add(grid);
            grid.DataSource = table;
            GridView view = new GridView();
            grid.ViewCollection.Add(view);
            grid.MainView = view;
            view.MakeGridReadOnly();
            view.OptionsDetail.ShowDetailTabs = false;

            GridView detail = new GridView();
            detail.MakeGridReadOnly();
            grid.MainView = detail;
            detail.OptionsDetail.ShowDetailTabs = false;
            grid.ViewCollection.Add(detail);
            grid.LevelTree.Nodes.Add("Objects", detail);
            //grid.ShowOnlyPredefinedDetails = true;

            page.Tag = table;
        }

        private void RemoveTab(string tableName)        //senichkin
        {
            DevExpress.XtraTab.XtraTabPage page = xtraTabControl1.TabPages.First(x => x.Text == tableName);
            xtraTabControl1.TabPages.Remove(page);
        }

        List<string> GetDifference()         //senichkin 
        {
            List<string> result = new List<string>();
            foreach (string tabText in xtraTabControl1.TabPages.Select(x => x.Text))
                if (Tables.FirstOrDefault(x => x.Name == tabText) == null)
                    result.Add(tabText);
            return result;
        }

        public void ExportCurrentTab()
        {
            if (!CanExport())
                return;
            Table table = (Table)xtraTabControl1.SelectedTabPage.Tag;
            Table result = table;
            if (table.Pattern == null)
                result = ConvertTable(table);
            using (ExportForm frm = new ExportForm(result)) {
                frm.ShowDialog();
            }
        }

        public bool CanExport()
        {
            return xtraTabControl1.SelectedTabPage != null && xtraTabControl1.SelectedTabPage.Tag is Table;
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            OnCanExportChanged();
        }

        public event EventHandler CanExportChanged;
        protected virtual void OnCanExportChanged()
        {
            EventHandler handler = CanExportChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        private Table ConvertTable(Table table)
        {
            Table result = new Table(null);
            IObjectWithProperties pattern = new ObjectWithProperties();
            pattern.Properties.Add("Name", typeof(string));
            pattern.Properties.Add("Time", typeof(int));
            foreach (DataFrame frame in table) {
                DataFrame resultFrame = new DataFrame(frame.Time);
                IObjectWithProperties owp = (IObjectWithProperties)resultFrame;
                owp.Properties = new Dictionary<string, object>();
                owp.Properties.Add("Name", frame.Name);
                owp.Properties.Add("Time", frame.Time);
                foreach (IObjectWithProperties item in frame.Objects) {
                    string name = (string)item.Properties["Name"];
                        //var mi = item.GetType().GetMethod("GetPattern", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                        //IObjectWithProperties sourcePattern = (IObjectWithProperties)mi.Invoke(item, new object[0]);
                        IObjectWithProperties sourcePattern = frame.Objects.Pattern;
                        foreach (var prop in sourcePattern.Properties) {
                            string newKey = string.Format("{0}: {1}", name, prop.Key);
                            if (prop.Key == "Name" || pattern.Properties.ContainsKey(newKey))
                                continue;
                            pattern.Properties.Add(newKey, prop.Value);
                        }
                    foreach (var prop in item.Properties) {
                        if (prop.Key == "Name")
                            continue;
                        owp.Properties.Add(string.Format("{0}: {1}", name, prop.Key), prop.Value);
                    }
                }
                result.Add(resultFrame);
            }
            result.Pattern = pattern;
            return result;
        }

        public void SimpleExportCurrentTab()
        {
            GridControl grid = xtraTabControl1.SelectedTabPage.Controls.OfType<GridControl>().First();
            using (SaveFileDialog dlg = new SaveFileDialog()) {
                if (dlg.ShowDialog() == DialogResult.OK)
                    grid.ExportToXlsx(dlg.FileName);
            }
        }
    }
}
