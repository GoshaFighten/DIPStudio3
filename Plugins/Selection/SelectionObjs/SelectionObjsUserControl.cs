using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;
using System.Collections;
using DIPStudioUICore.Extensions;

namespace SelectionObjects {
    public partial class SelectionObjsUserControl : BasePluginUserControl {
        public SelectionObjsUserControl(SelectionObjsSettings settings)
            : base(settings) {
            InitializeComponent();
            cmbInputTable.Properties.Application = fApplication;
            BindToControl(settings, s => s.InputTable, cmbInputTable, c => c.EditValue);
            BindToControl(settings, s => s.Criterias, gcCriterias, c => c.DataSource);
            BindValueObject(settings, s => s.Strob, phStrob);
            BindValueObject(settings, s => s.Threshold, phThreshold);
            BindToControl(settings, s => s.ResultName, teResult, c => c.EditValue, settings.Plugin.ShortName);
            BindToControl(settings, s => s.Prognosis, cePrognosis, c => c.EditValue);
            BindToControl(settings, s => s.FollowName, ceFollowName, c => c.EditValue);
            BindToControl(settings, s => settings.Intermediate, ceIntermediate, c => c.EditValue);
            gvCriterias.MakeGridReadOnly();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            Criteria criteria = (Criteria)gvCriterias.GetFocusedRow();
            Criteria tmp = new Criteria();
            tmp.Assign(criteria);
            if (ShowCustomizationForm(tmp) == DialogResult.OK)
                criteria.Assign(tmp);
        }

        private DialogResult ShowCustomizationForm(Criteria criteria) {
            CriteriaCustomizationUserControl uc = new CriteriaCustomizationUserControl(criteria, (string)cmbInputTable.SelectedItem);
            using (BasePluginForm frm = new BasePluginForm(uc, ((BasePluginForm)this.FindForm()).fModify) { Text = "Настройка критерия" }) {
                return frm.ShowDialog();
            }
        }

        private void gcCriterias_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e) {
            switch (e.Button.ButtonType) {
                case DevExpress.XtraEditors.NavigatorButtonType.Append: {
                        e.Handled = true;
                        Criteria criteria = new Criteria();
                        if (ShowCustomizationForm(criteria) == DialogResult.OK)
                            ((IList)gcCriterias.DataSource).Add(criteria);
                        break;
                    };
                case DevExpress.XtraEditors.NavigatorButtonType.Remove: {
                        e.Handled = DIPMessageBox.ShowTwoButtonDialog(this, "Удалить выделенный критерий?") == DialogResult.No;
                        break;
                    };
            }
        }
    }
}
