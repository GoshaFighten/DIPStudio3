using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DIPStudioCore;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using CustomEditor;

namespace DIPStudioUICore {
    public partial class BasePluginUserControl : BaseUserControl {
        protected readonly ValidatedPluginSettings fSettings;

        protected readonly DIPApplication fApplication;



        public BasePluginUserControl() {
            fApplication = DIPApplication.GetInstance();
            InitializeComponent();
        }

        public BasePluginUserControl(ValidatedPluginSettings settings)
            : this() {
            fSettings = settings;
        }

        public ValidatedPluginSettings Settings {
            get { return fSettings; }
        }

        protected void BindToControl<TSettings, TSettingProperty, TControl, TControlProperty>(TSettings settings, System.Linq.Expressions.Expression<Func<TSettings, TSettingProperty>> settingPropertySelector, TControl control, System.Linq.Expressions.Expression<Func<TControl, TControlProperty>> propertyPropertySelector)
            //where TSettings : ValidatedPluginSettings
            where TControl : Control {
                BindToControl(settings, settingPropertySelector, control, propertyPropertySelector, "");
            }

        protected void BindToControl<TSettings, TSettingProperty, TControl, TControlProperty>(TSettings settings, System.Linq.Expressions.Expression<Func<TSettings, TSettingProperty>> settingPropertySelector, TControl control, System.Linq.Expressions.Expression<Func<TControl, TControlProperty>> propertyPropertySelector, string addToText)
            //where TSettings : ValidatedPluginSettings
    where TControl : Control
        {
            Bind(settings, settingPropertySelector, control, propertyPropertySelector);
            LayoutControlItem item = layoutControl1.GetItemByControl(control);
            if (item != null) {
                item.Text = GetDisplayText(settings, settingPropertySelector) + " " + addToText;
                if (control is CheckEdit) {
                    (control as CheckEdit).Text = item.Text;
                    item.TextVisible = false;
                }
                item.Name = Guid.NewGuid().ToString();
                item.CustomizationFormText = string.Empty;
            }
        }

        private void Bind<TObj, TObjProperty, TControl, TControlProperty>(TObj settings, System.Linq.Expressions.Expression<Func<TObj, TObjProperty>> settingPropertySelector, TControl control, System.Linq.Expressions.Expression<Func<TControl, TControlProperty>> propertyPropertySelector)
            where TControl : Control {
            System.Linq.Expressions.MemberExpression mExpression = (System.Linq.Expressions.MemberExpression)settingPropertySelector.Body;
            string fieldName = mExpression.Member.Name;
            mExpression = (System.Linq.Expressions.MemberExpression)propertyPropertySelector.Body;
            string propertyName = mExpression.Member.Name;
            control.DataBindings.Add(propertyName, settings, fieldName, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private static string GetDisplayText<TObj, TResult>(TObj obj, System.Linq.Expressions.Expression<Func<TObj, TResult>> selector) {
            System.Linq.Expressions.MemberExpression mExpression = (System.Linq.Expressions.MemberExpression)selector.Body;
            DisplayNameAttribute[] attrs = (DisplayNameAttribute[])mExpression.Member.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (attrs.Length != 0)
                return attrs[0].DisplayName;
            else
                return mExpression.Member.Name;
        }

        private static bool GetReadOnly<TObj, TResult>(TObj obj, System.Linq.Expressions.Expression<Func<TObj, TResult>> selector) {
            System.Linq.Expressions.MemberExpression mExpression = (System.Linq.Expressions.MemberExpression)selector.Body;
            UserAccessAttribute[] attrs = (UserAccessAttribute[])mExpression.Member.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            return DIPApplication.GetInstance().Rights == Rights.User && attrs.Length == 0;
        }

        //protected void BindValueObject<TSettings, TResult>(TSettings settings, System.Linq.Expressions.Expression<Func<TSettings, TResult>> selector, EmptySpaceItem placeHolder, PropertyChangedEventHandler objectChangedCallback)
        //    //where TSettings : ValidatedPluginSettings
        //    where TResult : ValueObject
        //{            
        //    System.Linq.Expressions.MemberExpression mExpression = (System.Linq.Expressions.MemberExpression)selector.Body;

        //    Func<TSettings, TResult> func = selector.Compile();
        //    ValueObject vo = func.Invoke(settings);

        //    if (objectChangedCallback != null) {
        //        vo.PropertyChanged += objectChangedCallback;
        //        layoutControl1.Disposed += (s, e) => {
        //            vo.PropertyChanged -= objectChangedCallback;
        //        };
        //    }

        //    LayoutGroup parent = placeHolder.Parent;
        //    LayoutGroup group = parent.AddGroup();
        //    group.Text = GetDisplayText(settings, selector);

        //    LayoutControlItem item = new LayoutControlItem();
        //    item.Name = Guid.NewGuid().ToString();
        //    CheckEdit cb = new CheckEdit();
        //    cb.Name = Guid.NewGuid().ToString();
        //    cb.Text = GetDisplayText(vo, v => v.UseField);
        //    item.Control = cb;
        //    item.TextVisible = false;
        //    item.Text = cb.Text;
        //    group.AddItem(item);

        //    item = new LayoutControlItem();
        //    item.Name = Guid.NewGuid().ToString();
        //    SpinEdit seC = new SpinEdit();
        //    seC.Name = Guid.NewGuid().ToString();
        //    Bind(vo, s => s.Constant, seC, c => c.EditValue);
        //    item.Control = seC;
        //    item.Text = GetDisplayText(vo, v => v.Constant);
        //    group.AddItem(item);

        //    item = new LayoutControlItem();
        //    item.Name = Guid.NewGuid().ToString();
        //    TableComboBox txtData = new TableComboBox();
        //    txtData.Properties.Application = DIPApplication.GetInstance();
        //    txtData.Name = Guid.NewGuid().ToString();
        //    Bind(vo, s => s.Data, txtData, c => c.EditValue);
        //    txtData.Enabled = false;
        //    item.Control = txtData;
        //    item.Text = GetDisplayText(vo, v => v.Data);
        //    group.AddItem(item);

        //    item = new LayoutControlItem();
        //    item.Name = Guid.NewGuid().ToString();
        //    FieldComboBox txtField = new FieldComboBox();
        //    txtField.Properties.Application = DIPApplication.GetInstance();
        //    txtData.SelectedIndexChanged += (s, e) => {
        //        TableComboBox cmb = (TableComboBox)s;
        //        txtField.Properties.TableName = (string)cmb.EditValue;
        //    };
        //    txtField.Name = Guid.NewGuid().ToString();
        //    Bind(vo, s => s.Field, txtField, c => c.EditValue);
        //    txtField.Enabled = false;
        //    item.Control = txtField;
        //    item.Text = GetDisplayText(vo, v => v.Field);
        //    group.AddItem(item);

        //    group.Move(placeHolder, DevExpress.XtraLayout.Utils.InsertType.Left);
        //    parent.Remove(placeHolder);

        //    cb.CheckedChanged += (s, e) => {
        //        seC.Enabled = !cb.Checked;
        //        txtData.Enabled = cb.Checked;
        //        txtField.Enabled = cb.Checked;
        //    };
        //    Bind(vo, s => s.UseField, cb, c => c.EditValue);
        //}

        protected void BindValueObject<TSettings, TResult>(TSettings settings, System.Linq.Expressions.Expression<Func<TSettings, TResult>> selector, EmptySpaceItem placeHolder, PropertyChangedEventHandler objectChangedCallback)
            //where TSettings : ValidatedPluginSettings
            where TResult : ValueObject
        {
            System.Linq.Expressions.MemberExpression mExpression = (System.Linq.Expressions.MemberExpression)selector.Body;

            Func<TSettings, TResult> func = selector.Compile();
            ValueObject vo = func.Invoke(settings);

            if (objectChangedCallback != null) {
                vo.PropertyChanged += objectChangedCallback;
                layoutControl1.Disposed += (s, e) => {
                    vo.PropertyChanged -= objectChangedCallback;
                };
            }

            LayoutGroup parent = placeHolder.Parent;
            LayoutGroup group = parent.AddGroup();
            group.Text = GetDisplayText(settings, selector);

            LayoutControlItem item = new LayoutControlItem();
            item.Name = Guid.NewGuid().ToString();
            CheckEdit cb = new CheckEdit();
            cb.Name = Guid.NewGuid().ToString();
            cb.Text = GetDisplayText(vo, v => v.UseField);
            item.Control = cb;
            item.TextVisible = false;
            item.Text = cb.Text;
            group.AddItem(item);

            item = new LayoutControlItem();
            item.Name = Guid.NewGuid().ToString();
            SpinEdit seC = new SpinEdit();
            seC.Name = Guid.NewGuid().ToString();
            Bind(vo, s => s.Constant, seC, c => c.EditValue);
            item.Control = seC;
            item.Text = GetDisplayText(vo, v => v.Constant);
            group.AddItem(item);

            item = new LayoutControlItem();
            item.Name = Guid.NewGuid().ToString();
            TableComboBox txtData = new TableComboBox();
            txtData.Properties.Application = DIPApplication.GetInstance();
            txtData.Name = Guid.NewGuid().ToString();
            Bind(vo, s => s.Data, txtData, c => c.EditValue);
            txtData.Enabled = false;
            item.Control = txtData;
            item.Text = GetDisplayText(vo, v => v.Data);
            group.AddItem(item);

            item = new LayoutControlItem();
            item.Name = Guid.NewGuid().ToString();
            FieldComboBox txtField = new FieldComboBox();
            txtField.Properties.Application = DIPApplication.GetInstance();
            txtData.SelectedIndexChanged += (s, e) => {
                TableComboBox cmb = (TableComboBox)s;
                txtField.Properties.TableName = (string)cmb.EditValue;
            };
            txtField.Name = Guid.NewGuid().ToString();
            Bind(vo, s => s.Field, txtField, c => c.EditValue);
            txtField.Enabled = false;
            item.Control = txtField;
            item.Text = GetDisplayText(vo, v => v.Field);
            group.AddItem(item);

            group.Move(placeHolder, DevExpress.XtraLayout.Utils.InsertType.Left);
            parent.Remove(placeHolder);

            cb.CheckedChanged += (s, e) => {
                seC.Enabled = !cb.Checked;
                txtData.Enabled = cb.Checked;
                txtField.Enabled = cb.Checked;
            };
            Bind(vo, s => s.UseField, cb, c => c.EditValue);
        }

        protected void BindValueObject<TSettings, TResult>(TSettings settings, System.Linq.Expressions.Expression<Func<TSettings, TResult>> selector, EmptySpaceItem placeHolder)
            //where TSettings : ValidatedPluginSettings
            where TResult : ValueObject {
                BindValueObject(settings, selector, placeHolder, null);
        }
        public void BestFitLayout() {
            layoutControl1.BestFit();
        }
    }
}
