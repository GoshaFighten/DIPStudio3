using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data;

namespace SelectionObjects {
    public class ColumnWrapper : IDataColumnInfo {
        public View View { get; set; }
        private DataControllerBase fController;
        private string fExpression;
        public ColumnWrapper(View view, string expression) {
            View = view;
            fController = new DataControllerBase();
            fExpression = expression;
        }

        public string Caption {
            get {
                return string.Empty;
            }
        }

        public List<IDataColumnInfo> Columns {
            get {
                return View.Columns;
            }
        }

        public DataControllerBase Controller {
            get {
                return fController;
            }
        }

        public string FieldName {
            get {
                return Caption;
            }
        }

        public Type FieldType {
            get {
                return typeof(object);
            }
        }

        public string Name {
            get {
                return Caption;
            }
        }

        public string UnboundExpression {
            get {
                return fExpression;
            }
        }
    }
}
