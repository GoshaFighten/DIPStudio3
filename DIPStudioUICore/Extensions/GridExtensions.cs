using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;

namespace DIPStudioUICore.Extensions {
    public static class GridExtensions {
        public static void MakeGridReadOnly(this GridView view) {
            view.OptionsBehavior.ReadOnly = true;
            view.OptionsCustomization.AllowFilter = false;
            view.OptionsCustomization.AllowGroup = false;
            view.OptionsCustomization.AllowSort = false;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsCustomization.AllowColumnMoving = false;
            view.OptionsMenu.EnableColumnMenu = false;
        }
    }
}
