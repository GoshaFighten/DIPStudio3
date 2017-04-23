using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using DIPStudioCore.Data;

namespace SimInTechApplication
{
    public class SimInTechApplication : DIPApplicationBase {
        private SimInTechApplication() {
            series = new List<Series>();
        }
        public override void AddSeries(Series series) {
            Series existingSeries = GetSeriesByName(series.Name);
            if (existingSeries == null) {
                this.Series.Add(series);
            }
        }

        public override void AddTable(Table resultTable) {
            throw new NotImplementedException();
        }

        public static SimInTechApplication RequestApplication() {
            var instance = GetInstance();
            if (instance == null) {
                instance = new SimInTechApplication();
                SetInstance(instance);
            }
            return (SimInTechApplication)GetInstance();
        }

        private IList<Series> series;
        public IList<Series> Series {
            get { return series; }
        }

        public System.Drawing.Bitmap GetImage(string seriesName, int time) {
            return GetSeriesByName(seriesName)[time].Image;
        }

        public override bool CheckSeriesName(IPluginSettings validatedPluginSettings) {
            throw new NotImplementedException();
        }

        public override bool CheckTableName(IPluginSettings validatedPluginSettings) {
            throw new NotImplementedException();
        }

        public override Series GetSeriesByName(string seriesName) {
            foreach (Series series in Series) {
                if (series.Name == seriesName) {
                    return series;
                }
            }
            return null;
        }

        public override Series GetSeriesByNameOrCreateNew(string seriesName) {
            var series = GetSeriesByName(seriesName);
            if (series != null)
                return series;                     
            return new Series() {
                Name = seriesName
            };
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
