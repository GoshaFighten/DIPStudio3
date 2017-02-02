using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using System.ComponentModel;
using DIPStudioUICore;

namespace ModelImages
{
    public class ModelImagesSettings : ValidatedPluginSettings {
        public ModelImagesSettings(IPlugin plugin)
            : base(plugin) {
            ModelReceiver = new ModelReceiver();
            ModelTarget = new ModelTarget();
            ModelDisturbance = new ModelDisturbance(plugin);
            Disturbs = new BindingList<ModelDisturbance>();
            ModelMath = new ModelMath(ModelReceiver, ModelTarget);
            ModelBackground = new ModelBackground();
            ModelFrame = new ModelFrame();
            ModelFrame.Background = ModelBackground;
        }
        [SavePropertyAttribute(false)]
        public BindingList<ModelDisturbance> Disturbs { get; set; }

        [SavePropertyAttribute(false)]
        public ModelReceiver ModelReceiver {get; set;}

        [SavePropertyAttribute(false)]
        public ModelTarget ModelTarget { get; set; }

        [SavePropertyAttribute(false)]
        public ModelDisturbance ModelDisturbance { get; set; }

        [SavePropertyAttribute(false)]
        public ModelMath ModelMath { get; set; }

        [SavePropertyAttribute(false)]
        public ModelBackground ModelBackground { get; set; }

        [SavePropertyAttribute(false)]
        public ModelFrame ModelFrame { get; set; }

        [DisplayName ("Ширина кадра, пикс")]
        public int Width
        {
            get { return ModelFrame.Width; }
            set { ModelFrame.Width = value; }
        }
        [DisplayName("Высота кадра, пикс")]
        public int Height
        {
            get { return ModelFrame.Height; }
            set { ModelFrame.Height = value; }
        }

        //фон
        [DisplayName("Амплитуда случайной составляющей")]
        public int BARnd
        {
            get { return ModelBackground.ARnd; }
            set { ModelBackground.ARnd = value; }
        }
        [DisplayName("Амплитуда постоянной составляющей")]
        public int A
        {
            get { return ModelBackground.A; }
            set { ModelBackground.A = value; }
        }
        [DisplayName("Горизонтальный диаметр сферы винтирования")]
        public int W
        {
            get { return ModelBackground.W; }
            set { ModelBackground.W = value; }
        }
        [DisplayName("Вертикальный диаметр сферы винтирования")]
        public int H
        {
            get { return ModelBackground.H; }
            set { ModelBackground.H = value; }
        }
        [DisplayName("Использовать видеоряд")]
        public bool UseImages
        {
            get { return ModelBackground.UseImages; }
            set { ModelBackground.UseImages = value; }
        }
        [DisplayName("Видеоряд для фона")]
        public string BackgroundImages
        {
            get { return ModelBackground.BackgroundImages; }
            set { ModelBackground.BackgroundImages = value; }
        }
        //Объект
        //геомертия
        [DisplayName("Размер тела свечения, мм")]
        public double AM
        {
            get { return ModelTarget.AM; }
            set { ModelTarget.AM = value; }
        }
        [DisplayName("Относительный максимальный разброс размеров трассера")]
        public double DeltaD
        {
            get { return ModelTarget.DeltaD; }
            set { ModelTarget.DeltaD = value; }
        }
        [DisplayName("Радиус вращения центра по Х, мм")]
        public double XC
        {
            get { return ModelTarget.XC; }
            set { ModelTarget.XC = value; }
        }
        [DisplayName("Случ. составляющая вращения центра по Х, мм")]
        public double XCNRnd
        {
            get { return ModelTarget.XCNRnd; }
            set { ModelTarget.XCNRnd = value; }
        }
        [DisplayName("Частота вращения центра, рад/с")]
        public double WC
        {
            get { return ModelTarget.WC; }
            set { ModelTarget.WC = value; }
        }
        [DisplayName("Радиус вращения трассера, мм")]
        public double RM
        {
            get { return ModelTarget.RM; }
            set { ModelTarget.RM = value; }
        }
        [DisplayName("Радиус вращения центра по Y, мм")]
        public double YC
        {
            get { return ModelTarget.YC; }
            set { ModelTarget.YC = value; }
        }
        [DisplayName("Случ. составляющая вращения центра по Y, мм")]
        public double YCNRnd
        {
            get { return ModelTarget.YCNRnd; }
            set { ModelTarget.YCNRnd = value; }
        }
        [DisplayName("Случ. составляющая вращения трассера по Х, мм")]
        public double XMRnd
        {
            get { return ModelTarget.XMRnd; }
            set { ModelTarget.XMRnd = value; }
        }
        [DisplayName("Случ. составляющая вращения трассера по Y, мм")]
        public double YMRnd
        {
            get { return ModelTarget.YMRnd; }
            set { ModelTarget.YMRnd = value; }
        }
        //яркость
        [DisplayName("Частота периодической составляющей яркости, рад/м")]
        public double P
        {
            get { return ModelTarget.P; }
            set { ModelTarget.P = value; }
        }
        [DisplayName("Амплитуда случайной составляющей яркости, рад")]
        public double ARnd
        {
            get { return ModelTarget.ARnd; }
            set { ModelTarget.ARnd = value; }
        }
        [DisplayName("Коэффициент, определяющий характер распределения яркости, 1/м")]
        public double Alpha
        {
            get { return ModelTarget.Alpha; }
            set { ModelTarget.Alpha = value; }
        }
        //Помеха
        //закон по оси Х
        [DisplayName("Х0, пикс")]
        public double X0
        {
            get { return ModelDisturbance.X0; }
            set { ModelDisturbance.X0 = value; }
        }
        [DisplayName("Скорость по оси Х, пикс/с")]
        public double Vx
        {
            get { return ModelDisturbance.Vx; }
            set { ModelDisturbance.Vx = value; }
        }
        [DisplayName("Амплитуда по оси Х, пикс")]
        public double Ax
        {
            get { return ModelDisturbance.Ax; }
            set { ModelDisturbance.Ax = value; }
        }
        [DisplayName("Угловая скорость по оси Х, рад/с")]
        public double Wx
        {
            get { return ModelDisturbance.Wx; }
            set { ModelDisturbance.Wx = value; }
        }
        [DisplayName("Начальная фаза по оси Х, рад")]
        public double Px
        {
            get { return ModelDisturbance.Px; }
            set { ModelDisturbance.Px = value; }
        }
        //закон по оси У
        [DisplayName("Y0, пикс")]
        public double Y0
        {
            get { return ModelDisturbance.Y0; }
            set { ModelDisturbance.Y0 = value; }
        }
        [DisplayName("Скорость по оси Y, пикс/с")]
        public double Vy
        {
            get { return ModelDisturbance.Vy; }
            set { ModelDisturbance.Vy = value; }
        }
        [DisplayName("Амплитуда по оси Y, пикс")]
        public double Ay
        {
            get { return ModelDisturbance.Ay; }
            set { ModelDisturbance.Ay = value; }
        }
        [DisplayName("Угловая скорость по оси Y, рад/с")]
        public double Wy
        {
            get { return ModelDisturbance.Wy; }
            set { ModelDisturbance.Wy = value; }
        }
        [DisplayName("Начальная фаза по оси Y, рад")]
        public double Py
        {
            get { return ModelDisturbance.Py; }
            set { ModelDisturbance.Py = value; }
        }

        [DisplayName("Размер, пикс")]
        public double S
        {
            get { return ModelDisturbance.S; }
            set { ModelDisturbance.S = value; }
        }
        //яркость
        [DisplayName("Максимальная яркость, доля")]
        public double IMAX
        {
            get { return ModelDisturbance.IMAX; }
            set { ModelDisturbance.IMAX = value; }
        }
        [DisplayName("Коэффициент, определяющий характер распределения яркости, 1/пикс")]
        public double TA
        {
            get { return ModelDisturbance.A; }
            set { ModelDisturbance.A = value; }
        }
        [DisplayName("Случ. составляющая координаты, пикс")]
        public double RandPosition
        {
            get { return ModelDisturbance.RandPosition; }
            set { ModelDisturbance.RandPosition = value; }
        }
        [DisplayName("Случ. составляющая площади, пикс")]
        public double RandSize
        {
            get { return ModelDisturbance.RandSize; }
            set { ModelDisturbance.RandSize = value; }
        }
        //Приемник
        [DisplayName("F0, мм")]
        public double F0
        {
            get { return ModelReceiver.F0; }
            set { ModelReceiver.F0 = value; }
        }
        [DisplayName("F1, мм")]
        public double F1
        {
            get { return ModelReceiver.F1; }
            set { ModelReceiver.F1 = value; }
        }
        [DisplayName("Время переключения оптики, с")]
        public double T0
        {
            get { return ModelReceiver.T0; }
            set { ModelReceiver.T0 = value; }
        }
        [DisplayName("Размер ячейки, мкм")]
        public double Cell
        {
            get { return ModelReceiver.Cell; }
            set { ModelReceiver.Cell = value; }
        }
        [DisplayName("Смещение по оси Х, пикс")]
        public double X0Constant
        {
            get { return ModelReceiver.X0.Constant; }
            set { ModelReceiver.X0.Constant = value; }
        }
        [DisplayName("Смещение по оси Y, пикс")]
        public double Y0Constant
        {
            get { return ModelReceiver.Y0.Constant; }
            set { ModelReceiver.Y0.Constant = value; }
        }

        // Определяет, если плагин создает только видеокадры (true - если да, false - если только данные)
        // Если возвращается несколько видеорядов или таблиц данных, необходимо переписать RegisterOutputSeries и/или RegisterOutputTables методы
        // чтобы зарегестрировать имена выходных видеокадров или таблиц данных
        protected override bool IsResultSeries {
            get { return true; }
        }
    }
}
