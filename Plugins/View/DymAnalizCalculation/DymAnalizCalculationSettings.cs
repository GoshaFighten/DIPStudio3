using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;
using System.ComponentModel;

namespace DymAnalizCalculation
{
    public class DymAnalizCalculationSettings : ValidatedPluginSettings
    {
        private string fInputSeries = string.Empty;

        public DymAnalizCalculationSettings(IPlugin plugin)
            : base(plugin)
        {
            //start intialization
            U1 = 0; //начальная скорость м/с
            M1 = 1; // Число Маха в критическом сечении
            Rov = 0.1228; // удельная плотность воздуха 
            g = 9.81; //гравитационная постоянная
            C = 0.05; //относительная концентрация
            //Pn = 101325; // давление окружающей среды Па
            Koef.UseField = true;
            P.UseField = true;

            dkr = 0.0054; //диаметр критического сечения сопла м
            da1 = 0.0108; //диаметр выходного сечения сопла м
            K = 1.228; //показатель адиабаты продуктов сгорания 
            R = 336.8989; // газовая постоянная продуктов сгорания Дж/(кг*К)
            Tk = 2680; // температура в камере К

            X1 = 2.4; // расстояние от среза сопла до мерного сечения м
            Gsr = 0.3920; // экспериментальный средний расход двигателя кг/с
            Psr = 79.2; // экспериментальное среднее давление в двигателе кгс/см^2
            tPros = 20; // конечное время процесса
            tStart = 4; // время первого кадра в матрице давления
        }
        //input data
        private ValueObject fKoef = new ValueObject();
        [DisplayName("коэффициенты прозрачности")]
        public ValueObject Koef
        {
            get { return fKoef; }
            set { fKoef = value; }
        }
        private ValueObject fP = new ValueObject();
        [DisplayName("матрица давления")]
        public ValueObject P
        {
            get { return fP; }
            set { fP = value; }
        }
        //result data
        [DisplayName("Имя результата")]
        public string ResultData { get; set; }

        //const
        [DisplayName("скорость двигателя")]
        public double U1 { get; set; }
        [DisplayName("число Маха в начальном сечении")]
        public double M1 { get; set; }
        [DisplayName("удельная плотность воздуха")]
        public double Rov { get; set; }
        [DisplayName("ускорение свободного падения")]
        public double g { get; set; }
        [DisplayName("относительная концентрация")]
        public double C { get; set; }
        //[DisplayName("давление окружающей среды Па")]
        //public double Pn { get; set; }
        
        //engine properties 
        [DisplayName("диаметр критического сечения сопла м")]
        public double dkr { get; set; }
        [DisplayName("диаметр выходного сечения сопла м")]
        public double da1 { get; set; }
        [DisplayName("показатель адиабаты продуктов сгорания")]
        public double K { get; set; }
        [DisplayName("газовая постоянная продуктов сгорания Дж/(кг*К")]
        public double R { get; set; }
        [DisplayName("расстояние от среза сопла до мерного сечения м")]
        public double X1 { get; set; }

        //experiment data
        [DisplayName("температура в камере К")]
        public double Tk { get; set; }
        [DisplayName("экспериментальный средний расход двигателя кг/с")]
        public double Gsr { get; set; }
        [DisplayName("экспериментальное среднее давление в двигателе кгс/см^2")]
        public double Psr { get; set; }
        [DisplayName("конечное время процесса")]
        public double tPros { get; set; }
        [DisplayName("время первого кадра в матрице давления")]
        public double tStart{ get; set; }


        protected override bool IsResultSeries
        {
            get { return false; }
        }
    }
}
