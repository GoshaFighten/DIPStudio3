using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DIPStudioCore;
using DIPStudioUICore;

namespace SelectionObjects {
    public class Criteria : ValidatedPluginSettings, INotifyPropertyChanged {
        private string fExpression;
        private const string STR_Name = "Name";
        private const string STR_Parameter = "Parameter";
        private const string STR_Nominal = "Nominal";
        private const string STR_Coefficient = "Coefficient";
        private const string STR_Expression = "Expression";
        private string fName;
        private string fParameter;
        private ValueObject fNominal;
        private ValueObject fCoefficient;

        public Criteria()
            : base(null) {
            fNominal = new ValueObject();
            fCoefficient = new ValueObject();
        }

        [DisplayName("Имя параметра")]
        public string Name {
            get {
                return fName;
            }
            set {
                if (value == fName) {
                    return;
                }
                fName = value;
                OnPropertyChanged(STR_Name);
            }
        }

        [DisplayName("Параметра")]
        public string Parameter {
            get {
                return fParameter;
            }
            set {
                if (value == fParameter) {
                    return;
                }
                fParameter = value;
                OnPropertyChanged(STR_Parameter);
            }
        }
        [DisplayName("Выражение")]
        public string Expression {
            get {
                return fExpression;
            }
            set {
                if (value == fExpression) {
                    return;
                }
                fExpression = value;
                OnPropertyChanged(STR_Expression);
            }
        }
        [DisplayName("Номинал")]
        public ValueObject Nominal {
            get {
                return fNominal;
            }
            set {
                if (value.Equals(fNominal)) {
                    return;
                }
                fNominal = value;
                OnPropertyChanged(STR_Nominal);
            }
        }
        [DisplayName("Коэффициент")]
        public ValueObject Coefficient {
            get {
                return fCoefficient;
            }
            set {
                if (value.Equals(fCoefficient)) {
                    return;
                }
                fCoefficient = value;
                OnPropertyChanged(STR_Coefficient);
            }
        }

        public void Assign(Criteria source) {
            Name = source.Name;
            Parameter = source.Parameter;
            Expression = source.Expression;
            Nominal = source.Nominal;
            Coefficient = source.Coefficient;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected override bool IsResultSeries {
            get { throw new NotImplementedException(); }
        }

        protected override void RegisterOutputSeries(Dictionary<IPluginSettings, string> seriesMap) {
            throw new NotImplementedException();
        }

        protected override void RegisterOutputTables(Dictionary<IPluginSettings, string> tableMap) {
            throw new NotImplementedException();
        }
    }
}
