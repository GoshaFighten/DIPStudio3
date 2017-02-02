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
using DIPStudioCore.Data;

namespace DataFiltering {
    public partial class DataFilteringUserControl : BasePluginUserControl {
        private BindingList<FilteredField> fLbSource;
        public DataFilteringUserControl(DataFilteringSettings settings)
            : base(settings) {
            InitializeComponent();
            tableComboBox1.Properties.Application = this.fApplication;
            BindToControl(settings, s => s.InputTable, tableComboBox1, c => c.EditValue);
            BindToControl(settings, s => s.ResultName, textEdit1, c => c.EditValue, settings.Plugin.ShortName);

            fLbSource = new BindingList<FilteredField>();

            LbSourceFields.DataSource = fLbSource;
            GcChoosedFields.DataSource = settings.FilteredFields;

        }

        void DataFilteringUserControl_Load(object sender, EventArgs e)
        {
            FillSourceListBox();
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            DataFilteringSettings settings = (DataFilteringSettings)this.Settings;
            if (LbSourceFields.SelectedIndices.Count == 0) {
                return;
            }
            foreach (int selected in LbSourceFields.SelectedIndices) {
                FilteredField filteredField = (FilteredField)LbSourceFields.GetItem(selected);
                if (!settings.FilteredFields.Contains(filteredField)) {
                    settings.FilteredFields.Add(filteredField);
                }
            }
            GcChoosedFields.Invalidate();
        }

        void tableComboBox1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            ((DataFilteringSettings)this.Settings).FilteredFields.Clear();
            FillSourceListBox();
        }
        private void FillSourceListBox()
        {
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            DataFilteringSettings settings = (DataFilteringSettings)this.Settings;
            Table dataSource = application.GetTableByName(((DataFilteringSettings)this.Settings).InputTable);
            if (dataSource == null || dataSource.Count == 0) {
                return;
            }
            int viewIndex = 0;
            fLbSource.Clear();
            do {
                DataFrame viewFrame = dataSource.GetElementByIndex(viewIndex);
                if (viewFrame.Objects.Count == 0) {
                    viewIndex++;
                    continue;
                }
                foreach (KeyValuePair<string, object> property in viewFrame.Objects[0].Properties) {
                    fLbSource.Add(new FilteredField() {
                        Field = property.Key
                    });
                }
                break;
            }
            while (viewIndex < dataSource.Count);

            LbSourceFields.DisplayMember = "Caption";
            LbSourceFields.ValueMember = "Field";
        }
    }
}
