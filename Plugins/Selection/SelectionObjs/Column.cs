using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data;

namespace SelectionObjects {
    public class Column : IDataColumnInfo {
        public Column() {
        }
        public Column(string name) {
            SetName(name);
        }

        public Column(Type fieldType, string name)
            : this(name) {
            SetFieldType(fieldType);
        }

        private Type fFieldType;
        private string fName;

        public string Caption {
            get {
                return Name;
            }
        }

        private void SetName(string name) {
            fName = name;
        }

        public List<IDataColumnInfo> Columns {
            get {
                return null;
            }
        }

        public DataControllerBase Controller {
            get {
                return null;
            }
        }

        public string FieldName {
            get {
                return fName;
            }
        }

        public Type FieldType {
            get {
                return fFieldType;
            }
        }

        private void SetFieldType(Type fieldType) {
            fFieldType = fieldType;
        }

        public string Name {
            get {
                return fName;
            }
        }

        public string UnboundExpression {
            get {
                return string.Empty;
            }
        }
    }
}
