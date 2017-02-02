using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DIPStudioCore.Data;

namespace DIPStudioCore {
    public class ValueObject : INotifyPropertyChanged {
        private bool fUseField;
        [DisplayName("Использовать файл данных")]
        public bool UseField
        {
            get { return fUseField; }
            set
            {
                if (fUseField == value)
                    return;
                OnPropertyChanged("UseField");
                fUseField = value;
            }
        }

        private double fConstant;
        [DisplayName("Константа")]
        public double Constant
        {
            get { return fConstant; }
            set
            {
                if (fConstant == value)
                    return;
                OnPropertyChanged("Constant");
                fConstant = value;
            }
        }

        private string fData = string.Empty;
        [DisplayName("Имя вкладки данных")]
        public string Data
        {
            get { return fData; }
            set
            {
                if (fData == value)
                    return;
                OnPropertyChanged("Data");
                fData = value;
            }
        }

        private string fField = string.Empty;
        [DisplayName("Поле")]
        public string Field {
            get { return fField; }
            set
            {
                if (fField == value)
                    return;
                OnPropertyChanged("Field");
                fField = value;
            }
        }

        public override string ToString() {
            return UseField ? Field : Constant.ToString();
        }

        public override bool Equals(object obj) {
            if (obj == null) {
                return false;
            }
            ValueObject source = (ValueObject)obj;
            return UseField == source.UseField && Constant == source.Constant && Data == source.Data && Field == source.Field;
        }

        public double GetValue(int t) {
            if (!UseField || Data == ""|| Field == "") {
                return Constant;
            }
            Table table = DIPApplication.GetInstance().GetTableByName(Data);            
            DataFrame frame = table[t];
            double time0 = frame.Time;
            double x0 = Convert.ToDouble(((IObjectWithProperties)frame).Properties[Field]);
            DataFrame nextFrame = frame.GetNextElement();
            if (nextFrame == null)
                return x0;
            double time1 = nextFrame.Time;
            double x1 = Convert.ToDouble(((IObjectWithProperties)nextFrame).Properties[Field]);
            return (x1 - x0) * (t - time0) / (time1 - time0) + x0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
