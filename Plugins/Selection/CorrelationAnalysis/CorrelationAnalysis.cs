using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using System.Drawing;
using DIPStudioCore.Data;

namespace CorrelationAnalysis {
    public class CorrelationAnalysis : Plugin<CorrelationAnalysisSettings, CorrelationAnalysisUserControl> {
        public CorrelationAnalysis()
            : base(DIPStudioCore.ProjectKind.Selection, "Корреляционный анализ", "[КА]")
        {

        }
        
        protected override void Run(int t, int index, int tFinish) {
            DIPApplicationBase application = DIPApplicationBase.GetInstance();
            Series inputSeries = application.GetSeriesByName(this.PluginSettings.InputSeries);
            if (inputSeries == null) {
                throw new PluginException(string.Format("Series {0} does not exist", this.PluginSettings.InputSeries));
            }
            Series series = application.GetSeriesByNameOrCreateNew(ShortName + PluginSettings.ResultName );
            Frame sourceFrame = inputSeries[t];
            Bitmap resultImage = new Bitmap(sourceFrame.Image.Width, sourceFrame.Image.Height);
            Graphics gr = Graphics.FromImage(resultImage);
            gr.DrawImage(sourceFrame.Image, Point.Empty);
            gr.Dispose();
            Frame resultFrame = new Frame(t, resultImage);
            resultFrame.Name = sourceFrame.Name;
            series.Add(resultFrame);
            application.AddSeries(series);
            Table table = application.GetTableByNameOrCreateNew(ShortName + PluginSettings.ResultName, inputSeries.Name);
            DataFrame frame = new DataFrame(t);
            string[] name = sourceFrame.Name.Split(new char[] { '\\', ':' }, StringSplitOptions.None);
            frame.Name = name[name.Length - 1];
            ObjectWithProperties pattern = new ObjectWithProperties();
            pattern.properties.Add("Error", typeof(float));
            pattern.properties.Add("X", typeof(float));
            pattern.properties.Add("Y", typeof(float));
            frame.Objects.Pattern = pattern;
            ObjectWithProperties properties = new ObjectWithProperties();


            if (((this.PluginSettings.MethodPer == true && this.PluginSettings.OBper * this.PluginSettings.PgrPO < this.PluginSettings.ACF && this.PluginSettings.ACF < this.PluginSettings.PgrPO) || (this.PluginSettings.MethodPer == false && t % this.PluginSettings.PrKadr == 0) || t == 0) && DataPurpose.IR == false) {
                Bitmap ImagePer, ImagePerRes;
                if (t == 0) { ImagePer = sourceFrame.Image; ImagePerRes = resultFrame.Image; }
                else { ImagePer = sourceFrame.GetPrevElement().Image; ImagePerRes = resultFrame.GetPrevElement().Image; }

                Areas zah = new Areas(this.PluginSettings.NcsX, this.PluginSettings.NcsY, this.PluginSettings.StrobX, this.PluginSettings.StrobY, ImagePer);
                CaptureClass SP = new CaptureClass(ImagePer, this.PluginSettings.MethodBin, this.PluginSettings.Uroven, this.PluginSettings.Constt, zah);

                if (CaptureClass.Perezapis) {
                    DataPurpose.Xstrob = this.PluginSettings.NcsX = SP.xstrobSP; DataPurpose.Ystrob = this.PluginSettings.NcsY = SP.ystrobSP;
                    DataPurpose.strobX = this.PluginSettings.StrobX = SP.strobxSP; DataPurpose.strobY = this.PluginSettings.StrobY = SP.strobySP;
                    this.PluginSettings.A = CaptureClass.MassivIM(ImagePer);
                    Graphics.FromImage(ImagePerRes).DrawString("Э", SystemFonts.CaptionFont, Brushes.AliceBlue, 5, 5);
                    Graphics.FromImage(ImagePerRes).DrawRectangle(Pens.Aqua, zah.xop, zah.yop, zah.opx, zah.opy);
                    Graphics.FromImage(ImagePerRes).DrawRectangle(Pens.Aqua, this.PluginSettings.NcsX, this.PluginSettings.NcsY, this.PluginSettings.StrobX, this.PluginSettings.StrobY);
                }
                else {
                    this.PluginSettings.NcsX = DataPurpose.Xstrob; this.PluginSettings.NcsY = DataPurpose.Ystrob; this.PluginSettings.StrobX = DataPurpose.strobX; this.PluginSettings.StrobY = DataPurpose.strobY;
                    Graphics.FromImage(ImagePerRes).DrawString("Эталон не перезаписан", SystemFonts.CaptionFont, Brushes.AliceBlue, 5, 5);
                }
            }

            byte[,] B = CaptureClass.MassivIM(sourceFrame.Image);

            Areas OP = new Areas(this.PluginSettings.NcsX, this.PluginSettings.NcsY, this.PluginSettings.StrobX, this.PluginSettings.StrobY, sourceFrame.Image, DataPurpose.sdvigX, DataPurpose.sdvigY, DataPurpose.IR, this.PluginSettings.N);
            DataPurpose.sdvigX = this.PluginSettings.NcsX;
            DataPurpose.sdvigY = this.PluginSettings.NcsY;
            this.PluginSettings.ACF = this.PluginSettings.PgrPO;

            for (int chi = OP.yop; chi < OP.opy + OP.yop - DataPurpose.strobY; chi++)                // ch -  новые координаты строба 
                for (int chj = OP.xop; chj < OP.opx + OP.xop - DataPurpose.strobX; chj++) {

                    double ACFpr = 0;
                    int i1 = DataPurpose.Ystrob;

                    for (int i = chi; i <= DataPurpose.strobY + chi; i++) {

                        int j1 = DataPurpose.Xstrob;

                        for (int j = chj; j <= DataPurpose.strobX + chj; j++) {

                            ACFpr += Math.Abs(B[i, j] - this.PluginSettings.A[i1, j1]);
                            j1++;
                        }

                        i1++;
                    }
                    if (this.PluginSettings.ACF > ACFpr / (this.PluginSettings.StrobX * this.PluginSettings.StrobY)) {
                        this.PluginSettings.ACF = ACFpr / (this.PluginSettings.StrobX * this.PluginSettings.StrobY); //запомнить новые координаты стробы
                        this.PluginSettings.NcsY = chi;
                        this.PluginSettings.NcsX = chj;
                    }
                }

            if (this.PluginSettings.ACF >= this.PluginSettings.PgrPO) {
                properties.properties.Add("Error", this.PluginSettings.ACF);
                properties.properties.Add("X", 0);
                properties.properties.Add("Y", 0);
                Graphics.FromImage(resultFrame.Image).DrawRectangle(Pens.Black, OP.xop, OP.yop, OP.opx, OP.opy);
                DataPurpose.sdvigX = 0;
                DataPurpose.sdvigY = 0;
                DataPurpose.IR = true;
            }
            else {
                properties.properties.Add("Error", this.PluginSettings.ACF);
                properties.properties.Add("X", (float)(this.PluginSettings.NcsX * 2 + this.PluginSettings.StrobX) / 2);
                properties.properties.Add("Y", (float)(this.PluginSettings.NcsY * 2 + this.PluginSettings.StrobY) / 2);
                Graphics.FromImage(resultFrame.Image).DrawRectangle(Pens.Red, this.PluginSettings.NcsX, this.PluginSettings.NcsY, this.PluginSettings.StrobX, this.PluginSettings.StrobY);
                Graphics.FromImage(resultFrame.Image).DrawRectangle(Pens.Green, OP.xop, OP.yop, OP.opx, OP.opy);
                Graphics.FromImage(resultFrame.Image).DrawLine(Pens.Blue, (this.PluginSettings.NcsX * 2 + this.PluginSettings.StrobX) / 2 - 5, (this.PluginSettings.NcsY * 2 + this.PluginSettings.StrobY) / 2, (this.PluginSettings.NcsX * 2 + this.PluginSettings.StrobX) / 2 + 5, (this.PluginSettings.NcsY * 2 + this.PluginSettings.StrobY) / 2);
                Graphics.FromImage(resultFrame.Image).DrawLine(Pens.Blue, (this.PluginSettings.NcsX * 2 + this.PluginSettings.StrobX) / 2, (this.PluginSettings.NcsY * 2 + this.PluginSettings.StrobY) / 2 - 5, (this.PluginSettings.NcsX * 2 + this.PluginSettings.StrobX) / 2, (this.PluginSettings.NcsY * 2 + this.PluginSettings.StrobY) / 2 + 5);
                if (DataPurpose.IR) {
                    DataPurpose.sdvigX = 0;
                    DataPurpose.sdvigY = 0;
                }
                else {
                    DataPurpose.sdvigX = this.PluginSettings.NcsX - DataPurpose.sdvigX;
                    DataPurpose.sdvigY = this.PluginSettings.NcsY - DataPurpose.sdvigY;
                }
                DataPurpose.IR = false;
            }

            frame.Objects.Add(properties);
            table.Add(frame);
            application.AddTable(table);
        }

        protected override void CustomizeForm(BasePluginForm frm) {
            base.CustomizeForm(frm);
            frm.Width = 600;
            frm.Height = 600;
        }

        protected override string GetHelpString()
        {
            throw new NotImplementedException();
        }
    }
}
