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
    public class RepositoryItemTableComboBox : RepositoryItemComboBox {
        //The static constructor which calls the registration method
        static RepositoryItemTableComboBox() {
            RegisterCustomEdit();
        }

        //Initialize new properties
        public RepositoryItemTableComboBox() {
        }

        //The unique name for the custom editor
        private DIPApplicationBase fApplication;

        public const string CustomEditName = "TableComboBox";

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
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(TableComboBox), typeof(RepositoryItemTableComboBox), typeof(ComboBoxViewInfo), new ButtonEditPainter(), true, img));
        }

        //Override the Assign method
        public override void Assign(RepositoryItem item) {
            BeginUpdate();
            try {
                base.Assign(item);
                var source = item as RepositoryItemTableComboBox;
                if(source == null) {
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
                if (value == null || fApplication == value && Items.Count != 0)
                    return;
                fApplication = value;
                PopulateItems();
            }
        }

        private void PopulateItems() {
            Items.Clear();
            List<Table> list = Application.GetTableList();
            var query = from t in list
                        select t.Name;
            Items.AddRange(query.ToArray());
        }
    }
}
