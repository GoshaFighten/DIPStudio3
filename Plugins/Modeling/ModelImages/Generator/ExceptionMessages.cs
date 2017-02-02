using System;
using System.Collections.Generic;
using System.Text;

namespace ModelImages.Generator {
    public class ExceptionMessages {
        public static object ArgumentNull { get; set; }

        public static object ArgumentOutOfRangeGreaterEqual {
            get { return "{0} must be greater than or equal to {1}."; }
        }

        public static object ArgumentRangeLessEqual {
            get { return "The range between {0} and {1} must be less than or equal to {2}."; }
        }

        public ExceptionMessages() {
        }
    }
}
