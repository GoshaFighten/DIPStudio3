using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorrelationAnalysis
{
    internal class DataPurpose {
        /// <summary>
        /// Номер кадра эталона
        /// </summary>
        public static double Kadr { get; set; }
        /// <summary>
        /// Массив даннвх
        /// </summary>
        public static byte[,] Massiv { get; set; }
        public static int Xstrob { get; set; }
        public static int Ystrob { get; set; }
        public static int strobX { get; set; }
        public static int strobY { get; set; }
        /// <summary>
        /// Показывает включен или выключен инерционный режим
        /// </summary>
        public static bool IR { get; set; }

        public static int sdvigX {get;set;}

        public static int sdvigY { get; set; }

        public static void Dispoce() {
            Kadr = default(double);
            Xstrob = default(int);
            Ystrob = default(int);
            strobX = default(int);
            strobY = default(int);
            return;
        }
    }
}
