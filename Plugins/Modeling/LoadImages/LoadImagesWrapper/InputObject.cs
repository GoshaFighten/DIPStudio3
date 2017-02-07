using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPlugin {
    public class InputObject {
        public string FolderName { get; set; }
        public int StartTime { get; set; }
        public int D { get; set; }
        public bool Convert { get; set; }
        public string ResultName { get; set; }
    }
}
