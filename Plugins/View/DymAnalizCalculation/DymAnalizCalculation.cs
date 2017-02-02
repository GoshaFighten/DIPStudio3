using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing;
using System.Drawing.Imaging;
using Meta.Numerics.Functions;
using DIPStudioCore;
using DIPStudioUICore;
using DIPStudioCore.Data;


namespace DymAnalizCalculation
{
    public class DymAnalizCalculation : Plugin<DymAnalizCalculationSettings, DymAnalizCalculationUserControl>
    {
        DymAnalizCalculationSettings set;

        public DymAnalizCalculation()
            : base(ProjectKind.View, "Расчет мощности дымообразования", "[РМД]")
        {
            set = this.PluginSettings;
        }
        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 650;
            frm.Height = 600;
        }
        protected override void Run(int t, int index, int tFinish)
        {
            DIPApplication application = DIPApplication.GetInstance();
            Table table = application.GetTableByNameOrCreateNew(ShortName + PluginSettings.ResultData, string.Empty);
            DataFrame frame = new DataFrame(t);
            frame.Name = t.ToString();
            if (index == 0)
                table.Pattern = GetPattern();
            ObjectWithProperties obj = new ObjectWithProperties();
            obj.Assign(table.Pattern);
            if (t >= PluginSettings.tStart && t < PluginSettings.tPros) {
                double Koef = PluginSettings.Koef.GetValue(t);
                double P = PluginSettings.P.GetValue(t);
                obj.properties = GetPattern(t, Logic(Koef, P)).properties;
            }
            else
                obj.properties = GetPattern(t, 0).properties;
            frame.Assign(obj);
            table.Add(frame);
            application.AddTable(table);  
        }
        private ObjectWithProperties GetPattern()
        {
            ObjectWithProperties pattern = new ObjectWithProperties();
            pattern.properties.Add("Time", typeof(float));
            pattern.properties.Add("N", typeof(float));
            return pattern;
        }
        private ObjectWithProperties GetPattern(double t, double N)
        {
            ObjectWithProperties pattern = new ObjectWithProperties();
            pattern.properties.Add("Time", t.ToString());
            pattern.properties.Add("N", (float)N);
            return pattern;
        }

        private double GetDataByTableName(string tableName, string fieldName, int t)
        {
            DIPApplication app = DIPApplication.GetInstance();
            Table inputTable = app.GetTableByName(tableName);
            if (inputTable == null)
                throw new PluginException(string.Format("Table {0} does not exist", tableName));
            DataFrame inputFrame = inputTable[t];
            return (double)inputFrame.Objects[0].Properties[fieldName];
        }

        #region Main logic

        private double Logic(double Koef, double P)
        {
            #region Variable initialize

            double da = new double(); double ma = new double();
            double pa = new double(); double fk = new double();

            double Fkr = Math.PI * set.dkr * set.dkr / 4;
            double F1 = Fkr - 0.0000001;
            double Fa = Math.PI * set.da1 * set.da1 / 4;
            double F2 = Fa + 0.00000001;
            double G_k = P * set.Gsr / set.Psr;
            double Pkr = P * (Math.Pow(2 / (set.K + 1), (set.K / (set.K - 1))));
            #endregion

            ma = FindZero(y1, set.M1, 1, 8, 0.0001);//it's works!!!
            double L1 = L(set.M1);
            double l2 = L(ma);
            //double KoefVal = GetDataByTableName(set.Koef, i);
            pa = Pkr * pip(l2) / pip(L1);
            if (pa >= 1)
                da = set.da1;
            else {
                pa = 1;
                l2 = FindZero(y2, pa, Pkr, 1, 0.99999 * Math.Sqrt((set.K + 1) / (set.K - 1)), 0.0001);
                ma = M(l2);
                Fa = FindZero(y3, ma, set.M1, F1, F2, 0.00000001);
                da = Math.Sqrt(4 * Fa / Math.PI);
            }
            if (pa >= 2)
                fk = 1.35 * Math.Pow(pa, -0.6);
            else
                fk = -0.1 * Math.Pow(pa, 1) + 1.1;
            //all calculations provided by Ivan Kiryhin 41-13 comp 103-23
            //if some data will be requed in vector mode, intialize it as Vector and replace double by .value[i] 
            double ta = set.Tk * Math.Pow((pa / P), ((set.K - 1) / set.K)); //температура на срезе сопла
            double va = ma * Math.Sqrt(ta * set.K * set.R); //скорость пороховых газов
            double roa = G_k / (va * Fa * set.g); //удельная плотность пороховых газов
            double fx = 1.02 * Math.Pow((set.Rov / roa), (0.44));
            if (fx < 0.5 || fx > 4)
                throw new PluginException("Calculation error");
            double fv = Math.Exp((-1.63) * (set.U1 / va));
            double fl = 1 - 0.04 * Math.Pow(ma, 2.3);
            double e0 =  0.005* fv * fk * fx * fl; //коэффициент турбулентной вязкости струи
            double G1 = da * da / (128 * e0 * e0);
            double G2 = 1 / (0.0115 * fv * fv);
            double a1 = (G1 * set.X1 * set.X1 - set.C * Math.Pow(set.X1, 4)) / (G1 * G2);
            double Yzv = Math.Sqrt(Math.Abs(a1));
            double N0 = 2 * va * Yzv * G2 * (Math.PI * da * da / 4) * Math.Log(1 / Koef) / (Math.PI * set.X1 * set.X1 * (1 - Math.Exp(-G1 / (set.X1 * set.X1))) * AdvancedMath.Erf(Math.Sqrt(G2) * Yzv / set.X1)); //полная мощность дымообразования
            double nud = N0 / G_k;

            return nud;
        }


        private double FindZero(Func<double, double, double> func, double funcArg0, double start, double finish, double step)
        {
            double zero = start;
            for (double i = start; i <= finish; i += step)
                if (!double.IsNaN(func(i, funcArg0)))
                    zero = Zero(func(zero, funcArg0), func(i, funcArg0), zero, i);
            return zero;
        }

        private double FindZero(Func<double, double, double, double> func, double funcArg0, double funcArg1, double start, double finish, double step)
        {
            if (Math.Sign(func(start, funcArg0, funcArg1)) == Math.Sign(func(finish, funcArg0, funcArg1)))
                throw new PluginException("function have to change sign at the interval");
            double zero = start;
            for (double i = start; i <= finish; i += step)
                if (!double.IsNaN(func(i, funcArg0, funcArg1)))
                    zero = Zero(func(zero, funcArg0, funcArg1), func(i, funcArg0, funcArg1), zero, i);
            return zero;
        }

        private double Zero(double prevResult, double result, double prevIter, double iter)
        {
            if (Math.Sign(result) != Math.Sign(prevResult) && Math.Abs(result) < Math.Abs(prevResult))
                return iter;
            if (result == 0)
                return iter;
            return prevIter;
        }

        private double pip(double L)
        {
            return Math.Pow((1 - L * L * (set.K - 1) / (set.K + 1)), (set.K / (set.K - 1)));
        }   
        private double L(double M)
        {
            return Math.Sqrt((0.5 * (set.K + 1) * M * M) / (1 + 0.5 * (set.K - 1) * M * M));
        }
        private double M(double L)
        {
            return Math.Sqrt((2 * L * L / (set.K + 1)) / (1 - (set.K - 1) * L * L / (set.K + 1)));
        }
        private double y1(double M, double M1)
        {
            double Fa = Math.PI * set.da1 * set.da1 / 4;
            double Fkr = Math.PI * set.dkr * set.dkr / 4;
            return Math.Log((set.K - 1) * M * M + 2) / (2 * (set.K - 1)) - Math.Log((set.K - 1) * M1 * M1 + 2) / (2 * (set.K - 1)) - Math.Log(M) - Math.Log(Fa / Fkr) + (set.K * Math.Log((set.K - 1) * M * M + 2)) / (2 * (set.K - 1)) - (set.K * Math.Log((set.K - 1) * M1 * M1 + 2)) / (2 * (set.K - 1)) + Math.Log(M1);
        }
        private double y2(double L, double Pai, double Pkri)
        {
            return Pai * pip(1) / Pkri - pip(L);
        }
        private double y3(double F, double Mai, double M1)
        {
            double Fkr = Math.PI * set.dkr * set.dkr / 4;
            return Math.Log((set.K - 1) * Mai * Mai + 2) / (2 * (set.K - 1)) - Math.Log((set.K - 1) * M1 * M1 + 2) / (2 * (set.K - 1)) - Math.Log(Mai) - Math.Log(F / Fkr) + (set.K * Math.Log((set.K - 1) * Mai * Mai + 2)) / (2 * (set.K - 1)) - (set.K * Math.Log((set.K - 1) * M1 * M1 + 2)) / (2 * (set.K - 1)) + Math.Log(M1);
        }

        #endregion

        protected override string GetHelpString()
        {
            return "Перед настройкой данного плагина рекомендуется выполнить загрузку данных давления и прозрачности. Предоставленный вектор давления должен совпадать с началом горения на видеоряде, время кадра с первой вспышкой будет временем первого кадра в матрице давления. Задание конечного времени процесса желательно для отсечения нелинейностей после окончания процесса";
        }
    }
}
