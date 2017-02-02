using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using DIPStudioCore.Data;

namespace SizeFiltering {
    public class SizeFiltering : Plugin<SizeFilteringSettings, SizeFilteringUserControl> {
        public SizeFiltering()
            : base(DIPStudioCore.ProjectKind.Selection, "Размерная фильтрация", "[РФ]")
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
            Logic(t, index);
        }
        internal const string STR_S = "S";
        private void Logic(int t, int index) {
            DIPApplication app = DIPApplication.GetInstance();
            Table inputTable = app.GetTableByName(this.PluginSettings.InputTable);
            if (inputTable == null) {
                throw new PluginException(string.Format("Table {0} does not exist", PluginSettings.InputTable));
            }
            DataFrame inputFrame = inputTable[t];

            DataFrame resultFrame = new DataFrame(t);
            resultFrame.Name = inputFrame.Name;
            resultFrame.Objects.Pattern = new ObjectWithProperties();
            resultFrame.Objects.Pattern.Assign(inputFrame.Objects.Pattern);

            double minSize = PluginSettings.MinSize.GetValue(t);
            double maxSize = PluginSettings.MaxSize.GetValue(t);
            foreach (IObjectWithProperties obj in inputFrame.Objects) {
                int s = (int)obj.Properties[STR_S];
                if (s >= minSize && s <= maxSize) {
                    IObjectWithProperties resultObj = new ViewObject();
                    resultObj.Assign(obj);
                    resultFrame.Objects.Add(resultObj);
                }
            }

            Table resultTable = app.GetTableByNameOrCreateNew(ShortName + PluginSettings.ResultName, inputTable.SourceSeries.Name);
            resultTable.Add(resultFrame);
            app.AddTable(resultTable);
        }

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }
}
