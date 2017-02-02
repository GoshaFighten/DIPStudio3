using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioUICore;
using DevExpress.XtraEditors.Design;
using DIPStudioCore.Data;

namespace SelectionObjects {
    public partial class CriteriaCustomizationUserControl : BasePluginUserControl {
        string fTableName;
        public CriteriaCustomizationUserControl(Criteria criteria, string tableName)
            : base(criteria) {
            InitializeComponent();
            BindToControl(criteria, s => s.Name, teName, c => c.EditValue);            
            BindToControl(criteria, s => s.Expression, beExpression, c => c.EditValue);
            BindValueObject(criteria, s => s.Coefficient, phC);
            BindValueObject(criteria, s => s.Nominal, phNominal);
            fTableName = tableName;
        }

        Criteria Criteria {
            get {
                return (Criteria)Settings;
            }
        }

        private void beExpression_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            View view = new View();
            Table table = fApplication.GetTableByName(fTableName);

            foreach (PropertyDescriptor column in table.GetElementByIndex(0).Objects.GetItemProperties(null))
            {
                view.Columns.Add(new Column(column.PropertyType, column.Name));
            }
            UnboundColumnExpressionEditorForm frm = new UnboundColumnExpressionEditorForm(new ColumnWrapper(view, Criteria.Parameter), null);
            if (frm.ShowDialog() == DialogResult.OK) {
                Criteria.Parameter = frm.Expression;
                DevExpress.Data.ExpressionEditor.IExpressionEditor expressionEditor = frm as DevExpress.Data.ExpressionEditor.IExpressionEditor;
                beExpression.Text = expressionEditor.ExpressionMemoEdit.Text;
            }
        }
    }
}
