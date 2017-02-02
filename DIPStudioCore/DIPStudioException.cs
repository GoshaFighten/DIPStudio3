using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace DIPStudioCore {
    public class DIPStudiogException : Exception {
        public DIPStudiogException() {
        }

        public DIPStudiogException(string message)
            : base(message) {
        }

        public DIPStudiogException(string message, Exception innerException)
            : base(message, innerException) {
        }

        protected DIPStudiogException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }

    public class PluginException : DIPStudiogException {
        public PluginException() {
        }

        public PluginException(string message)
            : base(message) {
                //MessageBox.Show(message);
        }

        public PluginException(string message, Exception innerException)
            : base(message, innerException) {
        }

        protected PluginException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}
