using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DIPStudioUICore;
using DIPStudioCore;
using System.Collections;
using DevExpress.XtraGrid.Views.WinExplorer;
using DevExpress.XtraGrid;

namespace DIPStudio3.Controls {
    public partial class ImageControl : BaseUserControl {
        public ImageControl() {
            InitializeComponent();
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl1_SelectedPageChanged);
        }

        void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page != null && e.Page.Controls.Count>0)
            {
                WinExplorerView view = (WinExplorerView)((GridControl)e.Page.Controls[0]).MainView;
                Frame row = (Frame)view.GetFocusedRow();
                OnFrameChosen(row);
            }
        }

        BindingList<Series> fSeries;

        public BindingList<Series> Series {
            get { return fSeries; }
            set {
                if(fSeries != null) {
                    fSeries.ListChanged -= SeriesListChanged;
                }
                fSeries = value;
                fSeries.ListChanged += SeriesListChanged;
                RePopulateImages();
            }
        }

        void SeriesListChanged(object sender, ListChangedEventArgs e) {
            if(e.ListChangedType == ListChangedType.ItemAdded) {
                AddTab(Series[e.NewIndex]);
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                foreach (string str in GetDifference())
                RemoveTab(str);
                OnFrameChosen(null);
            }
        }

        private void RePopulateImages() {
            foreach(DevExpress.XtraTab.XtraTabPage page in xtraTabControl1.TabPages) {
                page.Controls[0].Dispose();
            }
            xtraTabControl1.TabPages.Clear();
            foreach(Series series in Series) {
                AddTab(series);
            }
        }

        private void AddTab(Series series) {
            DevExpress.XtraTab.XtraTabPage page = xtraTabControl1.TabPages.Add();
            page.Text = series.Name;
            WinExplorerView view = new WinExplorerView();
            GridControl grid = new GridControl();
            grid.Dock = DockStyle.Fill;
            page.Controls.Add(grid);
            grid.DataSource = series;
            grid.MainView = view;
            view.ColumnSet.DescriptionColumn = view.Columns["Name"];
            view.ColumnSet.TextColumn = view.Columns["Time"];
            view.ColumnSet.LargeImageColumn = view.Columns["Thumbnail"];
            view.OptionsView.Style = WinExplorerViewStyle.Large;
            view.FocusedRowChanged += view_FocusedRowChanged;
        }

        private void RemoveTab(string seriesName)        //senichkin
        {
            DevExpress.XtraTab.XtraTabPage page = xtraTabControl1.TabPages.First(x => x.Text == seriesName);
            xtraTabControl1.TabPages.Remove(page);
            page.Dispose();
        }

        List<string> GetDifference()         //senichkin 
        {
            List <string> result = new List<string>();
            foreach (string tabText in xtraTabControl1.TabPages.Select(x => x.Text))
                if (Series.FirstOrDefault(x => x.Name == tabText) == null)
                    result.Add(tabText);
            return result;
        }


        void view_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            WinExplorerView view = (WinExplorerView)sender;
            Frame row = (Frame)view.GetFocusedRow();
            OnFrameChosen(row);
        }

        protected virtual void OnFrameChosen(Frame frame) {
            EventHandler<FrameChosenEventArgs> handler = FrameChosen;
            if (handler != null && frame != null) {
                handler(this, new FrameChosenEventArgs(frame));
            }
        }

        public event EventHandler<FrameChosenEventArgs> FrameChosen;

        public void FocusFrame(Frame frame) {
            GridControl gridControl = (GridControl)xtraTabControl1.SelectedTabPage.Controls[0];
            WinExplorerView view = (WinExplorerView)gridControl.MainView;
            int rowHandle = view.GetRowHandle(((IList)view.DataSource).IndexOf(frame));
            view.FocusedRowHandle = rowHandle;
        }
        public Series FocusTab()
        {
            GridControl gridControl = (GridControl)xtraTabControl1.SelectedTabPage.Controls[0];
            WinExplorerView view = (WinExplorerView)gridControl.MainView;
            return (Series)view.DataSource;
        }
    }

    public class FrameChosenEventArgs : EventArgs {
        public FrameChosenEventArgs(Frame frame) {
            fFrame = frame;
        }

        private Frame fFrame;

        public Frame Frame {
            get { return fFrame; }
        }
    }
}
