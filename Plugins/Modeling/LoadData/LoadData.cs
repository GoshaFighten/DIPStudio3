using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using DIPStudioUICore;
using DIPStudioCore.Data;
using System.IO;

namespace LoadData {
    public class LoadData : Plugin<LoadDataSettings, LoadDataUserControl> {
        public LoadData()
            : base(ProjectKind.Modeling, "Загрузить данные", "[ЗД]")
        {

        }

        private void Logic(int index) {
            if (index != 0) {
                return;
            }
            Table table = DIPApplicationBase.GetInstance().GetTableByNameOrCreateNew(ShortName + PluginSettings.ResultName, string.Empty);
            if (!File.Exists(PluginSettings.FileName)) {
                throw new PluginException(string.Format("{0} файл не существует!", PluginSettings.FileName));
            }
            ObjectWithProperties pattern = new ObjectWithProperties();
            Dictionary<int, string> map = new Dictionary<int, string>();
            string[] lines = File.ReadAllLines(PluginSettings.FileName, Encoding.Default);
            char separator = '\t';
            foreach (string line in lines) {
                if (Array.IndexOf(lines, line) == 0) {
                    string[] columns = line.Split(separator);
                    for (int i = 0; i < columns.Length; i++) {
                        string column = columns[i];
                        if (i == 0)
                            pattern.properties.Add(column, typeof(int));
                        else
                            pattern.properties.Add(column, typeof(double));
                        map.Add(i, column);
                    }
                    table.Pattern = pattern;
                }
                else {
                    string processedLine = line.Replace(",", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    processedLine = processedLine.Replace(".", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    string[] row = processedLine.Split(separator);
                    DataFrame frame = null;
                    ObjectWithProperties obj = new ObjectWithProperties();
                    obj.Assign(pattern);
                    for (int i = 0; i < row.Length; i++) {
                        string cell = row[i];
                        if (i == 0) {
                            int time = Convert.ToInt32(cell.Trim());
                            frame = new DataFrame(time);
                            obj.properties[map[i]] = time;
                        }
                        else
                            obj.properties[map[i]] = Convert.ToDouble(cell.Trim());
                    }
                    frame.Assign(obj);
                    table.Add(frame);
                }
            }
            DIPApplicationBase.GetInstance().AddTable(table);
        }

        protected override void Run(int t, int index, int tFinish)
        {
            Logic(index);
        }

        protected override string GetHelpString()
        {
            return "Выберите текстовый файл с разделителями табуляции в первом окне. указанные в первой строке значения станут заголовками колонок. Данные первой колонки будут считаться временем, время должно быть целочисленным. Данные остальных колонок должны содержать только цифры.";
        }
    }
}
