using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DIPStudioCore.Data {
    public class DataList : BindingList<IObjectWithProperties>, ITypedList {
        public DataList() {
        }

        #region ITypedList Members

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors) {
            if(Count == 0) {
                return null;
            }
            PropertyDescriptor[] pda = new PropertyDescriptor[this[0].Properties.Count];
            string[] keys = new string[this[0].Properties.Count];
            object[] values = new object[this[0].Properties.Count];
            this[0].Properties.Keys.CopyTo(keys, 0);
            this[0].Properties.Values.CopyTo(values, 0);
            for(int i = 0; i < pda.Length; i++) {
                Type type = null;
                if(Pattern != null) {
                    type = (Type)Pattern.Properties[keys[i]];
                }
                else //if (values[i] != null) {
                {
                    type = values[i].GetType();
                }
                //else {
                //    values[i] = typeof(float);
                //    type = typeof(float);
                //}
                pda[i] = new DataPropertyDescriptor(keys[i], this[0].GetType(), type);
            }
            return new PropertyDescriptorCollection(pda);
        }

        public IObjectWithProperties Pattern { get; set; }

        public string GetListName(PropertyDescriptor[] listAccessors) {
            if(Count == 0) {
                return string.Empty;
            }
            return this[0].GetType().Name;
        }

        #endregion

    }
}

