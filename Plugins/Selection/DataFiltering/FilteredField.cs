using System;
using System.Collections.Generic;
using System.Text;

namespace DataFiltering
{
    [Serializable]
    public class FilteredField {
        private string fField;
        public string Field {
            get {
                return fField;
            }
            set {
                fField = value;
            }
        }

        private int fDepth = 1;
        public int Depth {
            get {
                return fDepth;
            }
            set {
                fDepth = value;
            }
        }

        public string Caption
        {
            get
            {
                return Field;
            }
        }

        public FilteredField(string field, int depth) {
            fField = field;
            fDepth = depth;
        }

        public FilteredField() {
        }
    }
}
