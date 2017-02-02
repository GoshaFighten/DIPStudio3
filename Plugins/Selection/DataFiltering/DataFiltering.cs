using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using DIPStudioCore.Data;
using System.Collections;

namespace DataFiltering {
    public class DataFiltering : Plugin<DataFilteringSettings, DataFilteringUserControl> {
        public DataFiltering()
            : base(DIPStudioCore.ProjectKind.Selection, "Фильтрация данных", "[ФД]")
        {

        }
        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 450;
            frm.Height = 300;
        }
        protected override void Run(int t, int index, int tFinish)
        {
            Logic (t);
        }

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }



        public void Logic (int t)
        {
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            Table dataSource = application.GetTableByName(this.PluginSettings.InputTable);
            if (dataSource == null) {
                throw new PluginException("no table");
            }
            Table resultTable = application.GetTableByNameOrCreateNew(ShortName + this.PluginSettings.ResultName, "");
            DataFrame newFrame = new DataFrame(dataSource[t].Time);
            foreach (ObjectWithProperties owp in dataSource[t].Objects) {
                ObjectWithProperties newViewObject = new ObjectWithProperties();
                newViewObject.Assign(owp);
                Filter(newViewObject, t);
                newFrame.Objects.Pattern = dataSource[t].Objects.Pattern;
                newFrame.Objects.Add(newViewObject);
            }
            resultTable.Add(newFrame);
            application.AddTable(resultTable);
        }

        private void Filter(ObjectWithProperties newViewObject, int index)
        {
            foreach (FilteredField field in PluginSettings.FilteredFields) {
                newViewObject.properties[field.Field] = Math.Round(GetFilteredData(newViewObject, field, index), 5);
            }
        }
        private double GetFilteredData(ObjectWithProperties newViewObject, FilteredField field, int index)
        {
            double sum = 0d;
            int count = 0;
            int realDepth = Math.Min(field.Depth, index);
            for (int i = 0; i < realDepth; i++) {
                ObjectWithProperties sourceViewObject = GetSourceViewObject(newViewObject, index - i);
                if (sourceViewObject == null)
                    continue;
                sum += Convert.ToDouble(sourceViewObject.properties[field.Field]);
                count++;
            }
            if (count == 0)
                return double.NaN;
            return sum / count;
        }
        private ObjectWithProperties GetSourceViewObject(ObjectWithProperties newViewObject, int index)
        {
            ObjectWithProperties sourceViewObject = null;
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            Table dataSource = application.GetTableByName(this.PluginSettings.InputTable);
            if (dataSource[index] != null) {
                foreach (ObjectWithProperties viewObject in dataSource[index].Objects) {
                    if (viewObject.properties["Name"] == newViewObject.properties["Name"]) {
                        sourceViewObject = viewObject;
                        break;
                    }
                }
            }
            return sourceViewObject;
        }
    }
}
