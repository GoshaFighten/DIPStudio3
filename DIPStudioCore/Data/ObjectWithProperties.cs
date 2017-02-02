using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DIPStudioCore.Data {
    public interface IObjectWithProperties {
        Dictionary<string, object> Properties { get; set; }

        void Assign(IObjectWithProperties source);
    }

    public class ObjectWithProperties : IObjectWithProperties {
        public ObjectWithProperties() {
            properties = new Dictionary<string, object>();
        }

        public Dictionary<string, object> properties;

        public void Assign(IObjectWithProperties source) {
            foreach(KeyValuePair<string, object> kvp in source.Properties) {
                if(properties.ContainsKey(kvp.Key)) {
                    properties[kvp.Key] = kvp.Value;
                }
                else {
                    properties.Add(kvp.Key, kvp.Value);
                }
            }
        }

        Dictionary<string, object> IObjectWithProperties.Properties {
            get { return properties; }
            set { properties = value; }
        }
    }

    public class ViewObject : ObjectWithProperties {
        internal const string STR_Name = "Name";

        internal const string STR_X = "X";

        internal const string STR_Y = "Y";

        internal const string STR_Width = "Width";

        internal const string STR_Height = "Height";

        public ViewObject() {
            properties.Add(STR_Name, null);
            properties.Add(STR_X, null);
            properties.Add(STR_Y, null);
            properties.Add(STR_Width, null);
            properties.Add(STR_Height, null);
        }

        public ViewObject(string name, double x, double y, int width, int height)
            : this() {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Name = name;
        }

        public double X {
            get { return (double)properties[STR_X]; }
            set { properties[STR_X] = value; }
        }

        public double Y {
            get { return (double)properties[STR_Y]; }
            set { properties[STR_Y] = value; }
        }

        public int Width {
            get { return (int)properties[STR_Width]; }
            set { properties[STR_Width] = value; }
        }

        public int Height {
            get { return (int)properties[STR_Height]; }
            set { properties[STR_Height] = value; }
        }

        public string Name {
            get { return (string)properties[STR_Name]; }
            set { properties[STR_Name] = value; }
        }

        static IObjectWithProperties pattern;

        public static IObjectWithProperties GetPattern() {
            return pattern;
        }

        static ViewObject() {
            pattern = new ObjectWithProperties();
            pattern.Properties.Add(STR_Name, typeof(string));
            pattern.Properties.Add(STR_X, typeof(double));
            pattern.Properties.Add(STR_Y, typeof(double));
            pattern.Properties.Add(STR_Width, typeof(int));
            pattern.Properties.Add(STR_Height, typeof(int));
        }
    }
}
