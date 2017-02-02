using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DIPStudioCore;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Globalization;
using System.ComponentModel;
using DIPStudio3.Controls;
using System.Drawing;
using GraphViewer;

namespace DIPStudio3
{
    public class SaveOpenProjectHelper
    {
        private const string STR_ProjectSettings = "ProjectSettings";
        private const string STR_StartTime = "StartTime";
        private const string STR_EndTime = "EndTime";
        private const string STR_Discretization = "Discretization";
        private const string STR_Direction = "Direction";
        private const string STR_PluginSettings = "PluginSettings";
        private const string STR_Value = "Value";
        private const string STR_Type = "Type";

        private const string STR_SourceTable = "SourceTable";
        private const string STR_SourceField = "SourceField";
        private const string STR_ColorR = "ColorR";
        private const string STR_ColorG = "ColorG";
        private const string STR_ColorB = "ColorB";
        private const string STR_KeepLine = "KeepLine";
        private const string STR_ObjectNum = "ObjectNum";
        private const string STR_Name = "Name";
        private const string STR_Title = "Title";
        private const string STR_OX = "OX";
        private const string STR_OY = "OY";


        private const string defaultExtension = "dipx";
        private const string defaultFilter = "DIPStudio files (*.dipx)|*.dipx|All files (*.*)|*.*";
        public static void Open(ProjectManager project)
        {
            using (OpenFileDialog dialog = GetOpenFileDialog()) {
                if (dialog.ShowDialog() != DialogResult.OK) {
                    return;
                }
                OpenCore(project, dialog.FileName);
            }
        }

        public static void Open(DIPStudio3.ProjectManager project, string file)
        {
            OpenCore(project, file);
        }

        private static void OpenCore(ProjectManager project, string file)
        {
            project.ResetProject(true);
            XDocument doc = XDocument.Load(file);
            XElement root = doc.Root;
            XElement projectSettings = root.Element(STR_ProjectSettings);
            project.StartTime = (int)ReadElement(projectSettings, STR_StartTime);
            project.EndTime = (int)ReadElement(projectSettings, STR_EndTime);
            project.Discretization = (int)ReadElement(projectSettings, STR_Discretization);
            project.Horiz = (bool)ReadElement(projectSettings, STR_Direction);
            project.ClearOperations();
            XElement pluginSettings = root.Element(STR_PluginSettings);
            foreach (XNode plugin in pluginSettings.Nodes()) {
                XElement element = (XElement)plugin;
                string key = element.Name.LocalName;
                Dictionary<string, object> list = new Dictionary<string, object>();
                ReadProperties(element, list);
                project.AddOperation(key, list);
            }
            if (doc.Root.Element("ProjectGraph") != null)
                OpenGraph(project.Viewer, doc);
            project.Name = file;
            project.IsModified = false;
        }

        private static void ReadProperties(XElement root, Dictionary<string, object> list)
        {
            foreach (XNode parameter in root.Nodes()) {
                list.Add(((XElement)parameter).Name.LocalName, ReadElement(root, ((XElement)parameter).Name.LocalName));
            }
        }

        private static object ReadElement(XElement root, string name)
        {
            XElement element = root.Element(name);
            if (element == null)
                return null;
            if (element.HasAttributes) {
                string value = element.Attribute(STR_Value).Value;
                string typeName = element.Attribute(STR_Type).Value;
                Type type = Type.GetType(typeName);
                return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
            }
            Dictionary<string, object> list = new Dictionary<string, object>();
            ReadProperties(element, list);
            return list;
        }

        public static bool Save(ProjectManager project)
        {
            using (SaveFileDialog dialog = GetSaveFileDialog()) {
                if (dialog.ShowDialog() != DialogResult.OK) {
                    return false;
                }
                return SaveCore(project, dialog.FileName);
            }
        }


        private static bool SaveCore(ProjectManager project, string file)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("Project");
            doc.Add(root);
            XElement projectSettings = new XElement(STR_ProjectSettings);
            root.Add(projectSettings);
            WriteElement(projectSettings, STR_StartTime, project.StartTime);
            WriteElement(projectSettings, STR_EndTime, project.EndTime);
            WriteElement(projectSettings, STR_Discretization, project.Discretization);
            WriteElement(projectSettings, STR_Direction, project.Horiz);
            XElement pluginSettings = new XElement(STR_PluginSettings);
            root.Add(pluginSettings);
            foreach (Operation operation in project.Operations) {
                XElement element = new XElement(operation.Plugin.Key);
                pluginSettings.Add(element);
                Dictionary<string, object> list = operation.Plugin.PluginSettings.Save();
                WriteProperties(element, list);
            }
            SaveGraph(project.Viewer, doc);
            doc.Save(file);
            project.Name = file;
            project.IsModified = false;
            return true;
        }
        public static void SaveImg(Series series)
        {

            using (FolderBrowserDialog dialog = new FolderBrowserDialog()) {
                if (dialog.ShowDialog() != DialogResult.OK) {
                    return;
                }
                foreach (Frame frame in series) {
                    frame.Image.Save(dialog.SelectedPath + "\\" + frame.Time + ".bmp");
                }
            }
        }

        public static void SaveGraph(GraphControl sourceControl, XDocument doc)
        {
            XElement graphRoot = new XElement("ProjectGraph");
            doc.Root.Add(graphRoot);
            BindingList<GraphData> sourceData = sourceControl.Tables;
            foreach (GraphData graphs in sourceData) {
                XElement control = new XElement("control" + sourceData.IndexOf(graphs));
                graphRoot.Add(control);
                WriteElement(control, STR_Title, graphs.Title);
                WriteElement(control, STR_OX, graphs.OX);
                WriteElement(control, STR_OY, graphs.OY);
                XElement curvas = new XElement("Curvas");
                control.Add(curvas);
                foreach (GraphViewer.Curva curva in graphs.Curvas) {
                    XElement element = new XElement("element" + graphs.Curvas.IndexOf(curva));
                    curvas.Add(element);
                    WriteElement(element, STR_KeepLine, curva.KeepLine);
                    WriteElement(element, STR_SourceTable, curva.SourceTable);
                    WriteElement(element, STR_SourceField, curva.SourceField);
                    WriteElement(element, STR_ColorR, curva.Color.R);
                    WriteElement(element, STR_ColorG, curva.Color.G);
                    WriteElement(element, STR_ColorB, curva.Color.B);
                    WriteElement(element, STR_ObjectNum, curva.ObjectNum);
                    WriteElement(element, STR_Name, curva.Label.Text);
                }
            }
        }
        public static void OpenGraph(GraphControl sourceControl, XDocument doc)
        {
            var result = new BindingList<GraphData>();
            XElement graphRoot =  doc.Root.Element("ProjectGraph");
            foreach (XNode contrNode in graphRoot.Nodes()) {
                XElement controlRes = (XElement)contrNode;
                var graph = new GraphData();
                graph.Title = (string)ReadElement(controlRes, STR_Title);
                graph.OX = (string)ReadElement(controlRes, STR_OX);
                graph.OY = (string)ReadElement(controlRes, STR_OY);
                if (controlRes.Element("Curvas") != null)
                    foreach (XNode elNode in controlRes.Element("Curvas").Nodes()) {
                        XElement element = (XElement)elNode;
                        string name = (string)ReadElement(element, STR_Name);
                        Color color = Color.FromArgb((byte)ReadElement(element, STR_ColorR), (byte)ReadElement(element, STR_ColorG), (byte)ReadElement(element, STR_ColorB));
                        var curva = new GraphViewer.Curva(name, color);
                        curva.KeepLine = (bool)ReadElement(element, STR_KeepLine);
                        curva.SourceTable = (string)ReadElement(element, STR_SourceTable);
                        curva.SourceField = (string)ReadElement(element, STR_SourceField);
                        curva.ObjectNum = (int)ReadElement(element, STR_ObjectNum);
                        graph.Curvas.Add(curva);
                    }

                result.Add(graph);
            }
            sourceControl.Tables = result;
            sourceControl.UpdatePanels();
        }
        private static void WriteProperties(XElement root, Dictionary<string, object> list)
        {
            foreach (KeyValuePair<string, object> kvp in list) {
                WriteElement(root, kvp.Key, kvp.Value);
            }
        }

        private static void WriteElement(XElement root, string name, object value)
        {
            XElement element = new XElement(name);
            if (value is Dictionary<string, object>) {
                WriteProperties(element, (Dictionary<string, object>)value);
            }
            else if (value == null)
                return;
            else {
                element.SetAttributeValue(STR_Type, value.GetType());
                element.SetAttributeValue(STR_Value, Convert.ToString(value, CultureInfo.InvariantCulture));
            }
            root.Add(element);
        }

        private static OpenFileDialog GetOpenFileDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = defaultExtension;
            dialog.Filter = defaultFilter;
            dialog.Title = "Открытие проекта ...";
            return dialog;
        }

        private static SaveFileDialog GetSaveFileDialog()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = defaultExtension;
            dialog.Filter = defaultFilter;
            dialog.Title = "Сохранение проекта ...";
            return dialog;
        }
    }
}
