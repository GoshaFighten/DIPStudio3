using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using DIPStudioCore.Data;
using System.Drawing;

namespace DetectionPotential2
{
    public class DetectionPotential2 : Plugin<DetectionPotentialSettings, DetectionPotentialUserControl>
    {
        public DetectionPotential2()
            : base(ProjectKind.Selection, "Вероятность обнаружения 2", "[ВО2]")
        {
        }

        protected override void Run(int t, int index, int tFinish)
        {
            DIPApplication application = DIPApplication.GetInstance();
            Series inputSeries = application.GetSeriesByName(this.PluginSettings.InputSeries);
            if (inputSeries == null) {
                throw new PluginException(string.Format("Series {0} does not exist", this.PluginSettings.InputSeries));
            }
            Table inputTable = application.GetTableByName(this.PluginSettings.InputTable);
            if (inputTable == null) {
                throw new PluginException(string.Format("Table {0} does not exist", this.PluginSettings.InputTable));
            }
            Frame sourceFrame = inputSeries[t];
            DataFrame inputFrame = inputTable[t];
            if (inputFrame.Count == 0) {
                // process empty data
                return;
            }
            IObjectWithProperties selectedObject = inputFrame.Objects[0];
            double centerX = (double)selectedObject.Properties["X"];
            double centerY = (double)selectedObject.Properties["Y"];
            int width = (int)selectedObject.Properties["Width"] * this.PluginSettings.K;
            int height = (int)selectedObject.Properties["Height"] * this.PluginSettings.K;
            int x = (int)Math.Floor(centerX - width / 2);
            int y = (int)Math.Floor(centerY - height / 2);
            Rectangle objectRectangle = new Rectangle(x, y, width, height);
            //Frame objectFrame = ImageMath.GetProcess(sourceFrame, (time, pixel) => { return pixel; }, objectRectangle, objectRectangle.Width, t);
            int[,] sourceArray = ImageMath.ImgToInt(sourceFrame.Image);
            //int[,] objectArray = ImageMath.ImgToInt(objectFrame.Image);
            double sourceMean = ImageMath.Mean(sourceArray);
            double sourceStd = ImageMath.Std(sourceArray);
            double backgroundMean = ImageMath.Mean(sourceArray, objectRectangle, false);
            double backgroundStd = ImageMath.Std(sourceArray, objectRectangle, false);
            Meta.Numerics.Statistics.Distributions.NormalDistribution sourceD = new Meta.Numerics.Statistics.Distributions.NormalDistribution(sourceMean, sourceStd);
            Meta.Numerics.Statistics.Distributions.NormalDistribution backgroundD = new Meta.Numerics.Statistics.Distributions.NormalDistribution(backgroundMean, backgroundStd);
            double pS = sourceD.RightProbability(this.PluginSettings.Tresholder);
            double pB = backgroundD.RightProbability(this.PluginSettings.Tresholder);
            double p = pS - pB;

            Table resultTable = application.GetTableByNameOrCreateNew(ShortName + this.PluginSettings.ResultName, inputTable.SourceSeries.Name);
            DataFrame resultFrame = new DataFrame(t);

            ObjectWithProperties pattern = new ObjectWithProperties();
            pattern.properties.Add("Time", typeof(int));
            pattern.properties.Add("Name", typeof(string));
            pattern.properties.Add("P", typeof(double));
            pattern.properties.Add("PS", typeof(double));
            pattern.properties.Add("PB", typeof(double));
            pattern.properties.Add("MS", typeof(double));
            pattern.properties.Add("SS", typeof(double));
            pattern.properties.Add("MB", typeof(double));
            pattern.properties.Add("SB", typeof(double));

            ObjectWithProperties obj = new ObjectWithProperties();
            obj.Assign(pattern);

            obj.properties["Time"] = t;
            obj.properties["Name"] = inputFrame.Name;
            obj.properties["P"] = p;
            obj.properties["PS"] = pS;
            obj.properties["PB"] = pB;
            obj.properties["MS"] = sourceMean;
            obj.properties["SS"] = sourceStd;
            obj.properties["MB"] = backgroundMean;
            obj.properties["SB"] = backgroundStd;


            resultFrame.Assign(obj);

            resultTable.Pattern = pattern;
            resultTable.Add(resultFrame);
            application.AddTable(resultTable);
        }

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }
}
