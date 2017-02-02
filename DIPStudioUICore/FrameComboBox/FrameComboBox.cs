using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace CustomEditor {
    [ToolboxItem(true)]
    public class FrameComboBox : ComboBoxEdit {
        //The static constructor which calls the registration method
        static FrameComboBox() {
            RepositoryItemFrameComboBox.RegisterCustomEdit();
        }

        //Initialize the new instance
        public FrameComboBox() {
            //...
        }

        //Return the unique name
        public override string EditorTypeName {
            get { return RepositoryItemFrameComboBox.CustomEditName; }
        }

        //Override the Properties property
        //Simply type-cast the object to the custom repository item type
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemFrameComboBox Properties {
            get { return base.Properties as RepositoryItemFrameComboBox; }
        }
    }
}
