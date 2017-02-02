using System;
using System.Collections.Generic;
using System.Text;
using DIPStudioCore;

namespace ModelImages {
    [Serializable]
    public class ModelReceiver {
        private double fF0 = 60;

        public double F0 {
            get {
                return fF0;
            }
            set {
                fF0 = value;
            }
        }

        private double fF1 = 600;

        public double F1 {
            get {
                return fF1;
            }
            set {
                fF1 = value;
            }
        }

        private double fT0 = 1.7;

        public double T0 {
            get {
                return fT0;
            }
            set {
                fT0 = value;
            }
        }

        private double fCell = 12;

        public double Cell {
            get {
                return fCell;
            }
            set {
                fCell = value;
            }
        }

        private ValueObject fX0 = new ValueObject();

        public ValueObject X0 {
            get {
                return fX0;
            }
            set {
                fX0 = value;
            }
        }

        private ValueObject fY0 = new ValueObject();

        public ValueObject Y0 {
            get {
                return fY0;
            }
            set {
                fY0 = value;
            }
        }
    }
}
