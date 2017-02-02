using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using System.Reflection;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DIPStudioCore;
using DIPStudioCore.Data;

namespace CustomEditor {
    //The attribute that points to the registration method
    [UserRepositoryItem("RegisterCustomEdit")]
    public class RepositoryItemFieldComboBox : RepositoryItemComboBox {
        //The static constructor which calls the registration method
        static RepositoryItemFieldComboBox() {
            RegisterCustomEdit();
        }

        //Initialize new properties
        public RepositoryItemFieldComboBox() {
        }

        //The unique name for the custom editor
        private string fTableName;
        private DIPApplicationBase fApplication;

        public const string CustomEditName = "FieldComboBox";

        //Return the unique name
        public override string EditorTypeName {
            get { return CustomEditName; }
        }

        //Register the editor
        public static void RegisterCustomEdit() {
            //Icon representing the editor within a container editor's Designer
            Image img = null;
            try {
                //img = (Bitmap)Bitmap.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DevExpress.CustomEditors.CustomEdit.bmp"));
            }
            catch {
            }
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(FieldComboBox), typeof(RepositoryItemFieldComboBox), typeof(ComboBoxViewInfo), new ButtonEditPainter(), true, img));
        }

        //Override the Assign method
        public override void Assign(RepositoryItem item) {
            BeginUpdate();
            try {
                base.Assign(item);
                var source = item as RepositoryItemFieldComboBox;
                if (source == null) {
                    return;
                }
                this.Application = source.Application;
            }
            finally {
                EndUpdate();
            }
        }

        public DIPApplicationBase Application {
            get { return fApplication; }
            set {
                if (fApplication == value)
                    return;
                fApplication = value;
                PopulateItems();
            }
        }


        public string TableName {
            get { return fTableName; }
            set {
                if (fTableName == value && Items.Count != 0)
                    return;
                fTableName = value;
                PopulateItems();
            }
        }

        private void PopulateItems() {
            if (Application == null || string.IsNullOrEmpty(TableName))
                return;
            Items.Clear();
            Table table = Application.GetTableByName(TableName);
            if (table == null)
                return;
            List<string> query = new List<string>();

            System.ComponentModel.PropertyDescriptorCollection items = table.GetItemProperties(null);
            if (table.Pattern != null)
                foreach (System.ComponentModel.PropertyDescriptor item in items) {
                    query.Add(item.DisplayName);
                }

            else //for objects
                foreach (var item in table.First(x => x.Count != 0).Objects[0].Properties) {
                    query.Add(item.Key);
                }
            Items.AddRange(query.ToArray());
        }
    }
}
