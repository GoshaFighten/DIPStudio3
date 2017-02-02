using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data;

namespace SelectionObjects {
    public class View {
        public View() {
            fColumns = new List<IDataColumnInfo>();
        }

        private List<IDataColumnInfo> fColumns;

        public List<IDataColumnInfo> Columns {
            get {
                return fColumns;
            }
        }
    }
}
