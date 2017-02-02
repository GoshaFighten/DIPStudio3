using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore.Data;

namespace DIPStudioCore {
    public abstract class DIPApplicationBase {
        private static DIPApplicationBase fInstance;
        public static DIPApplicationBase GetInstance() {
            return fInstance;
        }
        protected static void SetInstance(DIPApplicationBase instance) {
            if (fInstance != null) {
                throw new DIPStudiogException("Application already created");
            }
            fInstance = instance;
        }
        public virtual Rights Rights { get; protected set; }

        abstract public Table GetTableByName(string data);

        abstract public Table GetTableByNameOrCreateNew(string v, string name);

        abstract public void AddTable(Table resultTable);

        abstract public Series GetSeriesByName(string inputImages);

        abstract public Series GetSeriesByNameOrCreateNew(string v);

        abstract public void AddSeries(Series resultSeries);

        abstract public bool CheckSeriesName(IPluginSettings validatedPluginSettings);

        abstract public bool CheckTableName(IPluginSettings validatedPluginSettings);

        abstract public void RemoveSeries(Series inputSeries);

        abstract public void RemoveTable(Table inputTable);

        abstract public List<Series> GetSeriesList();

        abstract public List<Table> GetTableList();
    }
}
