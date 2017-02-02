using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;
using DIPStudioCore;
using DIPStudioCore.Data;
using CustomEditor;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace GraphViewer
{
    public partial class GrViewer : XtraUserControl
    {
        [Bindable(true)]
        public GrViewer(GraphData source)
        {
            InitializeComponent();
            _graphStory = source;
            itemBindingSource.DataSource = GraphStory.Curvas;
            this.repositoryItemTableComboBox1.Application = DIPApplication.GetInstance();
            this.repositoryItemFieldComboBox1.Application = DIPApplication.GetInstance();
            this.teTitle.DataBindings.Add(new Binding("Text", GraphStory, "Title"));
            this.teOxName.DataBindings.Add(new Binding("Text", GraphStory, "OX"));
            this.teOyName.DataBindings.Add(new Binding("Text", GraphStory, "OY"));
            ZgAjust();
            this.repositoryItemFieldComboBox1.EditValueChanged += new EventHandler(repositoryItemFieldComboBox1_EditValueChanged); ;
        }

        void repositoryItemFieldComboBox1_EditValueChanged(object sender, EventArgs e)
        {
            var currrentCurva = GraphStory.Curvas[gridView1.GetFocusedDataSourceRowIndex()];
            currrentCurva.SourceField = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).EditValue.ToString(); //manual assign control data to Graph data
            currrentCurva.Label.Text = currrentCurva.SourceTable + " " + currrentCurva.SourceField + " " + currrentCurva.ObjectNum;
            UpDateGraphPane();
        }

        private GraphData _graphStory = new GraphData();

        public GraphData GraphStory
        {
            get { return _graphStory; }
            set { _graphStory = value;}
        }
        public Color GetColor()
        {
            List<Color> ColorsList = new List<Color>() { Color.Red, Color.Green, Color.Blue, Color.Magenta, Color.Black };
            return ColorsList[GraphStory.Curvas.Count % ColorsList.Count];
        }

        private void ZgAjust()
        {
            GraphPane myPane = zg1.GraphPane;
            myPane.Title.IsVisible = true;
            myPane.Legend.Position = LegendPos.BottomCenter;
            myPane.Legend.IsVisible = true;
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.Title.Text = "Time";

            // Align the Y axis labels so they are flush to the axis
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.Scale.Align = AlignP.Inside;

            // Enable the Y2 axis display
            myPane.Y2Axis.IsVisible = true;
            // Make the Y2 axis scale blue
            myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue;
            myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue;
            // turn off the opposite tics so the Y2 tics don't show up on the Y axis
            myPane.Y2Axis.MajorTic.IsOpposite = false;
            myPane.Y2Axis.MinorTic.IsOpposite = false;
            // Display the Y2 axis grid lines
            myPane.Y2Axis.MajorGrid.IsVisible = true;
            // Align the Y2 axis labels so they are flush to the axis
            myPane.Y2Axis.Scale.Align = AlignP.Inside;

            // Enable scrollbars if needed
            //zg1.IsShowHScrollBar = true;
            //zg1.IsShowVScrollBar = true;
            //zg1.IsAutoScrollRange = true;

            // OPTIONAL: Show tooltips when the mouse hovers over a point
            zg1.IsShowPointValues = true;
            zg1.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);

            // Tell ZedGraph to calculate the axis ranges
            // Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
            // up the proper scrolling parameters
            zg1.AxisChange();
            // Make sure the Graph gets redrawn
            zg1.Invalidate();
        }


        public void UpDateGraphPane()
        {
            zg1.GraphPane.CurveList.Clear();
            try {
                foreach (Curva line in GraphStory.Curvas.Where(x => x.KeepLine)) {
                    AssignPointFromTable(line);
                    zg1.GraphPane.CurveList.Add(line);
                }
                if (gridView1.FocusedRowHandle >= 0 && !GraphStory.Curvas[gridView1.GetFocusedDataSourceRowIndex()].KeepLine) {
                    AssignPointFromTable(GraphStory.Curvas[gridView1.GetFocusedDataSourceRowIndex()]);
                    zg1.GraphPane.CurveList.Add(GraphStory.Curvas[gridView1.GetFocusedDataSourceRowIndex()]);
                }
            }
            catch (NullReferenceException) {
                throw new PluginException("no table choosed..or some other null reference shit");
            }
            zg1.AxisChange();
            zg1.Invalidate();
        }

        private void AssignPointFromTable(Curva line)
        {
            DIPApplication app = DIPApplication.GetInstance();
            Table table = app.GetTableByName(line.SourceTable);
            if (table == null ||  string.IsNullOrEmpty(line.SourceField))
                return;
            PointPairList list = new PointPairList();
            if (table.Pattern != null)
                for (int i = 0; i < table.Count; i++) {
                    ObjectWithProperties obj = new ObjectWithProperties();
                    obj.Assign(table.GetElementByIndex(i));
                    list.Add(table.GetElementByIndex(i).Time, Convert.ToDouble(obj.properties[line.SourceField]));
                }
            else {
                for (int i = 0; i < table.Count; i++) {
                    if (line.ObjectNum < table.GetElementByIndex(i).Objects.Count)
                    list.Add(table.GetElementByIndex(i).Time, Convert.ToDouble(table.GetElementByIndex(i).Objects[line.ObjectNum].Properties[line.SourceField]));
                }
            }
            line.Points = list;
        }   

        /// <summary>
        /// Display customized tooltips when the mouse hovers over a point
        /// </summary>
        private string MyPointValueHandler(ZedGraphControl control, GraphPane pane, CurveItem curve, int iPt)
        {
            // Get the PointPair that is under the mouse
            PointPair pt = curve[iPt];
            return curve.Label.Text + " is " + pt.Y.ToString("f2") + " at " + pt.X.ToString("f1") + " ms";
        }

        void repositoryItemTableComboBox1_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.SetFocusedRowCellValue("SourceField", string.Empty);

        }
        void gridView1_ShownEditor(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            if (view.FocusedColumn.FieldName == "SourceField") {
                var cmd = (FieldComboBox)view.ActiveEditor;
                string table = (string)view.GetFocusedRowCellValue("SourceTable");
                cmd.Properties.TableName = table;
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            GraphPane myPane = zg1.GraphPane;
            myPane.Title.Text = teTitle.Text;
            myPane.XAxis.Title.Text = teOxName.Text;
            myPane.YAxis.Title.Text = teOyName.Text;
            zg1.Invalidate();
        }//axis and title adjust

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GraphStory.Curvas.Add(new Curva("newLine" + GraphStory.Curvas.Count, new PointPairList(), GetColor()));
            UpDateGraphPane();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
                return;
            GraphStory.Curvas.RemoveAt(gridView1.FocusedRowHandle);
            UpDateGraphPane();
        }

        void ZgUpdate(object sender, EventArgs e)
        {
            UpDateGraphPane();
        }
    }
}
