using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DIPStudioCore.Base;

namespace DIPStudioCore.Data {
    public class DataFrame : FrameElement, IObjectWithProperties {
        public DataFrame(int time)
            : base(time) {
            fObjects = new DataList();
        }

        public new DataFrame GetNextElement(int offset) {
            return (DataFrame)base.GetNextElement(offset);
        }

        public new DataFrame GetPrevElement(int offset) {
            return (DataFrame)base.GetPrevElement(offset);
        }

        public new DataFrame GetNextElement() {
            return this.GetNextElement(1);
        }

        public new DataFrame GetPrevElement() {
            return this.GetPrevElement(1);
        }

        private Dictionary<string, object> fProperties;

        private DataList fObjects;

        public DataList Objects {
            get { return fObjects; }
        }
        [DisplayName("Число объектов")]
        public int Count { get { return fObjects.Count; } }

        Dictionary<string, object> IObjectWithProperties.Properties {
            get { return fProperties; }
            set { fProperties = value; }
        }

        public void Assign(IObjectWithProperties source) {
            ((IObjectWithProperties)this).Properties = source.Properties;
        }
    }
}
