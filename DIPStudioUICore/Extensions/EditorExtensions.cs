using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Repository;

namespace DIPStudioUICore.Extensions {
    public static class EditorExtensions {
        public static void SetIntMask(this RepositoryItemTextEdit ri) {
            ri.Mask.EditMask = "n0";
            ri.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
        }
    }
}
