using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore.Base;
using System.ComponentModel;

namespace DIPStudioCore.Data {
    public class Table : SortedBindingList<DataFrame>, ITypedList {
        public Table(Series sourceSeries)
        {
            fSourceSeries = sourceSeries;
        }
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors) {
            if(Pattern == null) {
                return TypeDescriptor.GetProperties(typeof(DataFrame));
            }
            PropertyDescriptor[] pda = new PropertyDescriptor[Pattern.Properties.Count];
            string[] keys = new string[Pattern.Properties.Count];
            object[] values = new object[Pattern.Properties.Count];
            Pattern.Properties.Keys.CopyTo(keys, 0);
            Pattern.Properties.Values.CopyTo(values, 0);
            for(int i = 0; i < pda.Length; i++) {
                pda[i] = new DataPropertyDescriptor(keys[i], this.GetElementByIndex(0).GetType(), (Type)Pattern.Properties[keys[i]]);
            }
            return new PropertyDescriptorCollection(pda);
        }

        public string GetListName(PropertyDescriptor[] listAccessors) {
            throw new NotImplementedException();
        }
        public IObjectWithProperties Pattern { get; set; }

        private Series fSourceSeries;
        public Series SourceSeries
        {
            get { return fSourceSeries; }
        }
    }
}
