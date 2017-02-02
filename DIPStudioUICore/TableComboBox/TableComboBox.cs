using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace CustomEditor {
    [ToolboxItem(true)]
    public class TableComboBox : ComboBoxEdit {
        //The static constructor which calls the registration method
        static TableComboBox() {
            RepositoryItemTableComboBox.RegisterCustomEdit();
        }

        //Initialize the new instance
        public TableComboBox() {
            //...
        }

        //Return the unique name
        public override string EditorTypeName {
            get { return RepositoryItemTableComboBox.CustomEditName; }
        }

        //Override the Properties property
        //Simply type-cast the object to the custom repository item type
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemTableComboBox Properties {
            get { return base.Properties as RepositoryItemTableComboBox; }
        }
    }
}
