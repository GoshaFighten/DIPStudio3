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

namespace CustomEditor {
    //The attribute that points to the registration method
    [UserRepositoryItem("RegisterCustomEdit")]
    public class RepositoryItemFrameComboBox : RepositoryItemComboBox {
        //The static constructor which calls the registration method
        static RepositoryItemFrameComboBox() {
            RegisterCustomEdit();
        }

        //Initialize new properties
        public RepositoryItemFrameComboBox() {
        }

        //The unique name for the custom editor
        private DIPApplication fApplication;

        public const string CustomEditName = "FrameComboBox";

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
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(FrameComboBox), typeof(RepositoryItemFrameComboBox), typeof(ComboBoxViewInfo), new ButtonEditPainter(), true, img));
        }

        //Override the Assign method
        public override void Assign(RepositoryItem item) {
            BeginUpdate();
            try {
                base.Assign(item);
                var source = item as RepositoryItemFrameComboBox;
                if(source == null) {
                    return;
                }
                this.Application = source.Application;
            }
            finally {
                EndUpdate();
            }
        }

        public DIPApplication Application {
            get { return fApplication; }
            set {
                if(fApplication == value)
                    return;
                fApplication = value;
                PopulateItems();
            }
        }

        private void PopulateItems() {
            Items.Clear();
            List<Series> list = Application.GetSeriesList();
            var query = from s in list
                        select s.Name;
            Items.AddRange(query.ToArray());
        }
    }
}
