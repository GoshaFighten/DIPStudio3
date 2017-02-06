using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using DIPStudioCore.Data;

namespace SimInTechApplication
{
    public class SimInTechApplication : DIPApplicationBase {
        public override void AddSeries(Series resultSeries) {
            throw new NotImplementedException();
        }

        public override void AddTable(Table resultTable) {
            throw new NotImplementedException();
        }

        public override bool CheckSeriesName(IPluginSettings validatedPluginSettings) {
            throw new NotImplementedException();
        }

        public override bool CheckTableName(IPluginSettings validatedPluginSettings) {
            throw new NotImplementedException();
        }

        public override Series GetSeriesByName(string inputImages) {
            throw new NotImplementedException();
        }

        public override Series GetSeriesByNameOrCreateNew(string v) {
            throw new NotImplementedException();
        }

        public override List<Series> GetSeriesList() {
            throw new NotImplementedException();
        }

        public override Table GetTableByName(string data) {
            throw new NotImplementedException();
        }

        public override Table GetTableByNameOrCreateNew(string v, string name) {
            throw new NotImplementedException();
        }

        public override List<Table> GetTableList() {
            throw new NotImplementedException();
        }

        public override void RemoveSeries(Series inputSeries) {
            throw new NotImplementedException();
        }

        public override void RemoveTable(Table inputTable) {
            throw new NotImplementedException();
        }
    }
}
