using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using DIPStudioCore.Data;
using DevExpress.Data.Filtering.Helpers;

namespace SelectionObjects {
    public class Logic {
        private readonly SelectionObjsSettings fSettings;
        private readonly int fTime;
        private readonly int fIndex;
        public Logic(SelectionObjsSettings settings, int time, int index) {
            fIndex = index;
            fTime = time;
            fSettings = settings;
        }
        public void Run() {
            DIPApplication app = DIPApplication.GetInstance();
            Table inputTable = app.GetTableByName(fSettings.InputTable);
            if (inputTable == null) {
                throw new PluginException(string.Format("Table {0} does not exist", fSettings.InputTable));
            }            

            DataFrame inputFrame = inputTable[fTime];

            DataFrame resultFrame = new DataFrame(fTime);
            resultFrame.Name = inputFrame.Name;
            resultFrame.Objects.Pattern = new ObjectWithProperties();
            resultFrame.Objects.Pattern.Assign(inputFrame.Objects.Pattern);
            resultFrame.Objects.Pattern.Properties.Add("K", typeof(double));
            resultFrame.Objects.Pattern.Properties.Add("TraceName", typeof(string));

            Table resultTable = app.GetTableByNameOrCreateNew(fSettings.Plugin.ShortName + fSettings.ResultName, inputTable.SourceSeries.Name);
            resultTable.Add(resultFrame);
            app.AddTable(resultTable);
            
            double minK = double.MaxValue;
            IObjectWithProperties selectedObj = null;
            foreach (IObjectWithProperties obj in inputFrame.Objects) {
                double currentK = 0d;
                double sumK = 0d;
                foreach (Criteria criteria in fSettings.Criterias) {
                    double k = criteria.Coefficient.GetValue(fTime);
                    sumK += k;
                    double n = criteria.Nominal.GetValue(fTime);
                    ExpressionEvaluator eval = new ExpressionEvaluator(inputFrame.Objects.GetItemProperties(null), criteria.Parameter);
                    double p = Convert.ToDouble(eval.Evaluate(obj));
                    currentK += Math.Abs(k * (p - n) * (p - n) / p / n);
                }
                currentK /= sumK;
                if (currentK < minK) {
                    selectedObj = obj;
                    minK = currentK;
                }
            }
            if (selectedObj == null)
                return;
            
            IObjectWithProperties resultObj = new ObjectWithProperties();
            resultObj.Assign(selectedObj);
            resultObj.Properties.Add("K", minK);
            resultObj.Properties.Add("TraceName", resultObj.Properties["Name"]);
            resultObj.Properties["Name"] = "1";
            resultFrame.Objects.Add(resultObj);

            
            //Table intermediateTable = null;
            //if (fSettings.Intermediate)
            //    intermediateTable = app.GetTableByNameOrCreateNew(GetIntermediateTableName());
        }

        private string GetIntermediateTableName() {
            return "Промежуточная" + fSettings.ResultName;
        }
    }
}
