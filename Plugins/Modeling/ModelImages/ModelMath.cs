using System;
using System.Collections.Generic;
using System.Text;
using DIPStudioCore;

namespace ModelImages {
    public class ModelMath {
        [NonSerialized]

        private readonly static Random rnd = new Random();

        public ModelMath(ModelReceiver receiver, ModelTarget target) {
            fReceiver = receiver;
            fTarget = target;
        }
        [NonSerialized]
        private ModelReceiver fReceiver;

        public ModelReceiver Receiver {
            get { return fReceiver; }
        }
        [NonSerialized]
        private ModelTarget fTarget;

        public ModelTarget Target {
            get { return fTarget; }
        }

        private double R(double t) {
            //if(!Target.UseRData)
                return 1000 * t / 6;
            //return .GetParameter(Application.GetData(Target.RDataName).GetDataList(), Target.RField, t);
        }

        private double I0(double t) {
            return (1 + 0.00000000000001 * Math.Pow(R(t), 4) * Math.Cos(Target.P * R(t) + rnd.NextDouble() * Target.ARnd)) / (1 + 0.00000000000008 * Math.Pow(R(t), 4));
        }

        public double I(double t, double x, double y, double a, double b) {
            return I0(t) * Math.Exp(-Math.Sqrt(x * x + y * y) / Target.Alpha / (a * a + b * b));
        }

        public double ap(double t) {
            return Target.AM * K(t) * (1 - Target.DeltaD * Target.DeltaD * Math.Pow(t, 4) * rnd.NextDouble() * rnd.NextDouble() / 10000);
        }

        private double K(double t) {
            return (t < Receiver.T0 ? Receiver.F0 : Receiver.F1) / R(t) / Receiver.Cell;
        }

        public double x(double t) {
            return K(t) * ((Target.XC + xcn()) * Math.Cos(Target.WC * t) + (xm() + Target.RM) * Math.Cos(wtr(t) * t)) + Receiver.X0.GetValue((int)(t*1000));
        }

        public double y(double t) {
            return K(t) * ((Target.YC + ycn() * Math.Sin(Target.WC * t) + (ym() + Target.RM) * Math.Sin(wtr(t) * t))) + Receiver.Y0.GetValue((int)(t * 1000));
        }

        private double xcn() {
            return Target.XCNRnd * rnd.NextDouble();
        }

        private double ycn() {
            return Target.YCNRnd * rnd.NextDouble();
        }

        private double xm() {
            return Target.XMRnd * (0.5 - rnd.NextDouble());
        }

        private double ym() {
            return Target.YMRnd * (0.5 - rnd.NextDouble());
        }

        private double wtr(double t) {
            return 2 * Math.PI * V2(t);
        }

        private double V2(double t) {
            //if(!Target.UseVData) {
                const double t0 = 1.568;
                return t < t0 ? 8 * t : -0.8 * t + 13.8;
            //}
            //return ImageConfig.GetParameter(Application.GetData(Target.VDataName).GetDataList(), Target.VField, t);
        }
    }
}
