using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioCore;
using System.Drawing;
using System.Drawing.Imaging;
using DIPStudioUICore;
using DIPStudioCore.Data;

namespace DymAnaliz3
{
    public class DymAnaliz3 : Plugin<DymAnaliz3Settings, DymAnaliz3UserControl>
    {
        [NonSerialized]
        double ACFa = 0;
        [NonSerialized]
        double Zasa = 0;
        [NonSerialized]
        List<string> data = new List<string>() { "Time", "Result", "ACFdo", "ZasDo" };

        public DymAnaliz3()
            : base(ProjectKind.Selection, "Анализ прозрачности", "[ДЫМ]")
        {
        }

        protected override void CustomizeForm(BasePluginForm frm)
        {
            base.CustomizeForm(frm);
            frm.Width = 600;
            frm.Height = 700;
        }

        protected override void Run(int t, int index, int tFinish)
        {

            DIPApplication application = DIPApplication.GetInstance();
            Series inputSeries = application.GetSeriesByName(this.PluginSettings.InputSeries);
            if (this.PluginSettings.newSeries) {
                AddSeries(t, inputSeries);
            }

            Table table = application.GetTableByNameOrCreateNew(ShortName + PluginSettings.ResultData, inputSeries.Name);
            DataFrame frame = new DataFrame(t);
            string[] name = inputSeries[t].Name.Split(new char[] { '\\', ':' }, StringSplitOptions.None);
            frame.Name = name[name.Length - 1];

            List<double> resultDataFrame = Logic(t, index==0, inputSeries, table);
            ObjectWithProperties obj = new ObjectWithProperties();
            obj.Assign(table.Pattern);
            if (PluginSettings.IsZasUse)
                obj.properties = GetPattern(4, resultDataFrame).properties;
            else
                obj.properties = GetPattern(2, resultDataFrame).properties;
            frame.Assign(obj);
            table.Add(frame);
            application.AddTable(table);      
        }

        private void AddSeries(int t, Series inputSeries)
        {
            DIPApplication application = DIPApplication.GetInstance();
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName);
            Frame resultFrame = new Frame(t, DrawStrob(inputSeries[t], PluginSettings));
            resultFrame.Name = inputSeries[t].Name;
            series.Add(resultFrame);
            application.AddSeries(series);
        }

        private List<double> Logic(int t, bool first, Series inputSeries, Table table)
        {
            Rectangle strob = new Rectangle(PluginSettings.NcsX, PluginSettings.NcsY, PluginSettings.StrobX, PluginSettings.StrobY);
            Rectangle ZasStrob = new Rectangle(PluginSettings.NcsXzas, PluginSettings.NcsYzas, PluginSettings.StrobXzas, PluginSettings.StrobYzas);
            Bitmap im = inputSeries[t].Image;
            double ACFb = 0;
            double ZasDo = 0;
            double Zasb = 0;
            int[,] A, Az, Bz;
            if (first) {
                ACFa = 0;
                Zasa = 0;
                if (PluginSettings.IsZasUse)
                    table.Pattern = GetPattern(3);
                else
                    table.Pattern = GetPattern(1);
                A = ImageMath.ImgToInt(im, strob);
                foreach (var dot in A)
                    ACFa += dot;
                if (PluginSettings.IsZasUse) {
                    Az = ImageMath.ImgToInt(im, ZasStrob);
                    foreach (var dot in Az)
                        Zasa += dot;
                }
            }
            int[,] B = ImageMath.ImgToInt(im, strob);
            foreach (byte dot in B)
                ACFb += dot;
            double ACFdo = (ACFa / ACFb);
            double corrected = ACFdo;
            if (PluginSettings.IsZasUse) {
                Bz = ImageMath.ImgToInt(im, ZasStrob);
                foreach (var dot in Bz)
                    Zasb += dot;
                ZasDo = Zasa / Zasb;
                corrected = ACFdo - (ZasDo - 1);
            }
            List<double> resultDataFrame = new List<double>() { t, corrected, ACFdo, ZasDo };
            return resultDataFrame;
        }

        public Bitmap DrawStrob(Frame sourceFrame, DymAnaliz3Settings settings)
        {

            Rectangle strob = new Rectangle(settings.NcsX, settings.NcsY, settings.StrobX, settings.StrobY);
            Bitmap tempImg = new Bitmap(sourceFrame.Image.Width, sourceFrame.Image.Height);
            Graphics gr = Graphics.FromImage(tempImg);
            gr.DrawImage(sourceFrame.Image, Point.Empty);
            gr.DrawRectangle(Pens.Red, strob);
            if (settings.IsZasUse)
                gr.DrawRectangle(Pens.AntiqueWhite, settings.NcsXzas, settings.NcsYzas, settings.StrobXzas, settings.StrobYzas);
            gr.Dispose();
            return tempImg;
        }

        private ObjectWithProperties GetPattern(int dataCount)
        {
            ObjectWithProperties pattern = new ObjectWithProperties();
            for (int i = 0; i <= dataCount; i++) {
                pattern.properties.Add(data[i],typeof(float));
            }
            return pattern;
        }

        private ObjectWithProperties GetPattern(int dataCount, List<double> dataToTable)
        {
            ObjectWithProperties pattern = new ObjectWithProperties();
            for (int i = 0; i < dataCount; i++) {
                pattern.properties.Add(data[i], (float)dataToTable[i]);
            }
            return pattern;
        }

        protected override string GetHelpString()
        {
            return "Перед настройкой данного плагина рекомендуется выполнить загрузку хотя бы одного изображения из требуемого видеоряда. " + Environment.NewLine + "Выберите видеоряд в первом поле, выделите мышкой область анализа в окне предпросмотра, подкорректируйте результаты при необходимости и выберите имя для сохраняемого видеоряда. Плагин сохранит общую яркость указанной области на первом кадре и будет сравнивать ее с яркостью области на остальных изображениях видеоряда. При использовании засветочного строба выделение областей основной рабочей области и засветочной происходят поочередно. красным цветом - рабочая область, серым - засветочная. данные яркости засветки вычитаются из данных рабочей области";
        }
    }
}
