using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using Meta.Numerics.Functions;
using DIPStudioCore.Data;

namespace DetectionPotential {
    public class DetectionPotential : Plugin<DetectionPotentialSettings, DetectionPotentionUserControl> {
        public DetectionPotential()
            : base(ProjectKind.Selection, "Вероятность обнаружения", "[ВО]")
        {
        }
        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 600;
            frm.Height = 400;
        }
        protected override void Run(int t, int index, int tFinish)
        {
            DIPApplicationBase app = DIPApplicationBase.GetInstance();
            Table inputTable = app.GetTableByName(this.PluginSettings.InputTable);
            if (inputTable == null) {
                throw new PluginException(string.Format("Table {0} does not exist", PluginSettings.InputTable));
            }
            DataFrame inputFrame = inputTable[t];

            double q;
            double f;
            double p;
            if (inputFrame.Objects.Count == 0) {
                q = -1;
                f = -1;
                p = -1;
            }
            else {
                double mba = (double)inputFrame.Objects[1].Properties[this.PluginSettings.MBA];
                double mbo = (double)inputFrame.Objects[1].Properties[this.PluginSettings.MBO];
                double qba = (double)inputFrame.Objects[1].Properties[this.PluginSettings.QBA];
                int frameWidth = (int)inputFrame.Objects[1].Properties[this.PluginSettings.FrameWidth];
                int frameHeight = (int)inputFrame.Objects[1].Properties[this.PluginSettings.FrameHeight];
                int objWidth = (int)inputFrame.Objects[1].Properties[this.PluginSettings.ObjWidth];
                int objHeight = (int)inputFrame.Objects[1].Properties[this.PluginSettings.ObjHeight];
                q = Q(mba, mbo, qba, frameWidth, frameHeight, objWidth, objHeight, this.PluginSettings.UseStrob, this.PluginSettings.KStrob);
                f = this.PluginSettings.F.GetValue(t);
                p = P(f, q);
            }

            DataFrame resultFrame = new DataFrame(t);

            ObjectWithProperties pattern = new ObjectWithProperties();
            pattern.properties.Add("Time", typeof(int));
            pattern.properties.Add("Name", typeof(string));
            pattern.properties.Add("P", typeof(double));
            pattern.properties.Add("F", typeof(double));
            pattern.properties.Add("Q", typeof(double));

            ObjectWithProperties obj = new ObjectWithProperties();
            obj.Assign(pattern);

            obj.properties["Time"] = t;
            obj.properties["Name"] = inputFrame.Name;
            obj.properties["P"] = p;
            obj.properties["F"] = f;
            obj.properties["Q"] = q;


            resultFrame.Assign(obj);

            Table resultTable = app.GetTableByNameOrCreateNew(ShortName + PluginSettings.ResultName, string.Empty);
            resultTable.Pattern = pattern;
            resultTable.Add(resultFrame);
            app.AddTable(resultTable);
        }

        private double Q(double mba, double mbo, double qba, int frameWidth, int frameHeight, int objWidth, int objHeight, bool useStrob, int kStrob) {
            double k = (double)objWidth * (double)objHeight / (double)frameWidth / (double)frameHeight;
            if (useStrob) {
                k = 1.0 / (double)kStrob / (double)kStrob;
            }
            k = 1.0;
            return ((mbo * k) - mba) / qba / 100;
        }

        private double P(double f, double q) {
            //return (1 - AdvancedMath.Erf(AdvancedMath.InverseErf(1 - 2 * f) - q)) / 2;
            return 0.5 - F(InverseF(0.5 - f) - q);
        }

        private double F(double x) {
            return AdvancedMath.Erf(x / Math.Sqrt(2)) / 2.0;
        }

        private double InverseF(double x) {
            return Math.Sqrt(2) * AdvancedMath.InverseErf(2.0 * x);
        }

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }
}
