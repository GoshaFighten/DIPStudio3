using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioUICore;
using DIPStudioCore;
using System.ComponentModel;

namespace DetectionPotential {
    public class DetectionPotentialSettings : ValidatedPluginSettings {
        public DetectionPotentialSettings(IPlugin plugin)
            : base(plugin) {
        }

        protected override bool IsResultSeries {
            get { return false; }
        }

        private string fInputTable = string.Empty;
        [DisplayName("Исходные данные")]
        public string InputTable 
        {
            get { return fInputTable; }
            set { fInputTable = value; }
        }

        private ValueObject fF = new ValueObject();
        [DisplayName("F")]
        public ValueObject F 
        {
            get { return fF; }
            set { fF = value;}
        }

        private string fMBA;
        [DisplayName("MBA")]
        public string MBA 
        {
            get { return fMBA;  }
            set { fMBA = value; }
        }

        private string fMBO;
        [DisplayName("MBO")]
        public string MBO 
        {
            get { return fMBO;  }
            set { fMBO = value; }
        }

        private string fQBA;
        [DisplayName("QBA")]
        public string QBA 
        {
            get { return fQBA;  }
            set { fQBA = value; }
        }

        private string fFrameWidth;
        [DisplayName("FrameWidth")]
        public string FrameWidth 
        {
            get { return fFrameWidth;  }
            set { fFrameWidth = value; }
        }

        private string fFrameHeight;
        [DisplayName("FrameHeight")]
        public string FrameHeight 
        {
            get { return fFrameHeight;  }
            set { fFrameHeight = value; }
        }

        private string fObjWidth;
        [DisplayName("Width")]
        public string ObjWidth 
        {
            get { return fObjWidth;  }
            set { fObjWidth = value; }
        }

        private string fObjHeight;
        [DisplayName("Height")]
        public string ObjHeight 
        {
            get { return fObjHeight;  }
            set { fObjHeight = value; }
        }

        private bool fUSeStrob = false;
        [DisplayName("UseStrob")]
        public bool UseStrob 
        {
            get { return fUSeStrob; }
            set { fUSeStrob = value; }
        }

        private int fKStrob = 2;
        [DisplayName("KStrob")]
        public int KStrob 
        {
            get { return fKStrob;  }
            set { fKStrob = value; }
        }
    }
}
