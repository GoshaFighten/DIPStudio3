using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DIPStudioCore.Data {
    public class DataPropertyDescriptor : PropertyDescriptor {
        public DataPropertyDescriptor(string name, Type componentType, Type propertyType)
            : base(name, null) {
            this.componentType = componentType;
            this.propertyType = propertyType;
        }

        private Type componentType;

        private Type propertyType;

        public override bool CanResetValue(object component) {
            throw new NotImplementedException();
        }

        public override Type ComponentType {
            get { return componentType; }
        }

        public override object GetValue(object component) {
            IObjectWithProperties viewObject = (IObjectWithProperties)component;
            if (!viewObject.Properties.ContainsKey(Name))
                return null;
            return viewObject.Properties[Name];
        }

        public override bool IsReadOnly {
            get { return true; }
        }

        public override Type PropertyType {
            get { return propertyType; }
        }

        public override void ResetValue(object component) {
            throw new NotImplementedException();
        }

        public override void SetValue(object component, object value) {
            throw new NotImplementedException();
        }

        public override bool ShouldSerializeValue(object component) {
            throw new NotImplementedException();
        }

        public override string DisplayName {
            get { return Name; }
        }
    }
}

