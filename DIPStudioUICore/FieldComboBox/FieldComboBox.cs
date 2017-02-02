using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace CustomEditor {
    [ToolboxItem(true)]
    public class FieldComboBox : ComboBoxEdit {
        //The static constructor which calls the registration method
        static FieldComboBox() {
            RepositoryItemFieldComboBox.RegisterCustomEdit();
        }

        //Initialize the new instance
        public FieldComboBox() {
            //...
        }

        //Return the unique name
        public override string EditorTypeName {
            get { return RepositoryItemFieldComboBox.CustomEditName; }
        }

        //Override the Properties property
        //Simply type-cast the object to the custom repository item type
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemFieldComboBox Properties {
            get { return base.Properties as RepositoryItemFieldComboBox; }
        }
    }
}