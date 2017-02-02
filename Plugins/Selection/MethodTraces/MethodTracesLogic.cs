using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPStudioCore;
using DIPStudioCore.Data;

namespace MethodTraces {
    public class MethodTracesLogic {
        int traceCount;
        const double msInSecs = 1000d;
        public MethodTracesLogic() {
            traceCount = 0;
        }
        const string tracePrefix = "Trace ";
        public void Run(int t, int index, MethodTracesSettings settings) {
            DIPApplication app = DIPApplication.GetInstance();
            Table inputTable = app.GetTableByName(settings.InputTable);
            if (inputTable == null) {
                throw new PluginException(string.Format("Table {0} does not exist", settings.InputTable));
            }
            DataFrame inputFrame = inputTable[t];
            DataFrame resultFrame = new DataFrame(t);
            resultFrame.Name = inputFrame.Name;
            resultFrame.Objects.Pattern = Point.GetPattern();
            resultFrame.Objects.Pattern.Assign(inputFrame.Objects.Pattern);
            Table resultTable = app.GetTableByNameOrCreateNew(settings.Plugin.ShortName + settings.ResultName, inputTable.SourceSeries.Name);
            resultTable.Add(resultFrame);
            DataFrame prevFrame = resultFrame.GetPrevElement();
            if (prevFrame == null) {
                for (int i = 0; i < inputFrame.Objects.Count; i++) {
                    ViewObject item = (ViewObject)inputFrame.Objects[i];
                    Point point = new Point();
                    point.Assign(item);
                    FillEmptyPoint(point);
                    point.Name = tracePrefix + traceCount++;
                    point.OldName = item.Name;
                    resultFrame.Objects.Add(point);
                }
            }
            else {
                PairFrames pairFrames = new PairFrames(inputFrame, prevFrame);
                Dictionary<PointOnTrace, PointOnTrace> result = pairFrames.CalculateTrace();
                foreach (PointOnTrace currentPoint in result.Keys) {
                    PointOnTrace prevPoint = result[currentPoint];
                    Point point = new Point();
                    point.Assign(currentPoint.Point);
                    point.OldName = currentPoint.Point.Name;  
                    resultFrame.Objects.Add(point);
                    if (prevPoint == null){
                        FillEmptyPoint(point);
                        point.Name = tracePrefix + traceCount++;
                        continue;
                    }                                             
                    point.Name = prevPoint.Point.Name;
                                      
                    double discretization = (inputFrame.Time - prevFrame.Time) / msInSecs;
                    point.R = PairFrames.Length(currentPoint.Point, prevPoint.Point);
                    point.V = point.R / discretization;
                    point.Vx = (point.X - prevPoint.Point.X) / discretization;
                    point.Vy = (point.Y - prevPoint.Point.Y) / discretization;
                    DataFrame prePrevFrame = prevFrame.GetPrevElement();
                    Point prePrevPoint = null;
                    if (prePrevFrame != null) {
                        prePrevPoint = prePrevFrame.Objects.FirstOrDefault(o => {
                            Point p = (Point)o;
                            return p.Name == point.Name;
                        }) as Point;
                    }
                    double dir = 0;
                    if (prePrevPoint == null) {
                        point.Angle = double.NaN;
                        point.AV = double.NaN;
                        point.Radius = double.NaN;
                    }
                    else {
                        point.Angle = angle_point(point, prevPoint.Point, prePrevPoint);
                        point.AV = (Math.PI - point.Angle) / discretization;
                        point.Radius = radius(point, prevPoint.Point, point.Angle);
                        dir = direction(point, prevPoint.Point, prePrevPoint);
                    }
                    point.Direction = Math.Sign(dir);
                }
            }
            app.AddTable(resultTable);
        }

        private static double direction(ViewObject a, ViewObject b, ViewObject c) {
            double x1 = a.X - b.X, x2 = b.X - c.X;
            double y1 = a.Y - b.Y, y2 = b.Y - c.Y;
            //return x2 * y1 - x1 * y2;
            return x1 * y2 - x2 * y1;
        }

        private static double radius(ViewObject a, ViewObject b, double angle) {
            double x = a.X - b.X;
            double y = a.Y - b.Y;
            return Math.Sqrt(x * x + y * y) / 2 / Math.Cos(angle / 2);
        }

        private static double angle_point(ViewObject a, ViewObject b, ViewObject c) {
            double x1 = a.X - b.X, x2 = c.X - b.X;
            double y1 = a.Y - b.Y, y2 = c.Y - b.Y;
            double d1 = Math.Sqrt(x1 * x1 + y1 * y1);
            double d2 = Math.Sqrt(x2 * x2 + y2 * y2);
            return Math.Acos((x1 * x2 + y1 * y2) / (d1 * d2));
        }

        private static void FillEmptyPoint(Point point) {
            point.R = -1;
            point.V = -1;
            point.Vx = -1;
            point.Vy = -1;
            point.Angle = double.NaN;
            point.Radius = double.NaN;
            point.Direction = 0;
        }
    }

    public class PairFrames {
        private DataFrame prevFrame;
        private DataFrame currentFrame;
        private Dictionary<PointOnTrace, PointOnTrace> result;
        private int[] indexes;
        private bool[] fix;

        public PairFrames(DataFrame currentFrame, DataFrame prevFrame) {
            this.prevFrame = prevFrame;
            this.currentFrame = currentFrame;
            result = new Dictionary<PointOnTrace, PointOnTrace>();
        }

        public Dictionary<PointOnTrace, PointOnTrace> CalculateTrace() {
            FindNearestPoints();
            SolveConflicts();
            for (int i = 0; i < indexes.Length; i++) {
                PointOnTrace key = new PointOnTrace(currentFrame.Objects[i], currentFrame.Name);
                PointOnTrace value;
                if (indexes[i] == -1) {
                    value = null;
                }
                else {
                    value = new PointOnTrace(prevFrame.Objects[indexes[i]], prevFrame.Name);
                }
                result.Add(key, value);
            }
            return result;
        }

        public static double Length(IObjectWithProperties p1, IObjectWithProperties p2) {
            ViewObject v1 = (ViewObject)p1;
            ViewObject v2 = (ViewObject)p2;
            return Math.Sqrt(Math.Pow((v1.X - v2.X), 2) + Math.Pow((v1.Y - v2.Y), 2));
        }

        /// <summary>
        /// Ставит в соответствие точке p на первом кадре ближайшую точку на втором кадре
        /// </summary>
        /// <param name="p">Точка на первом кадре</param>
        /// <returns>Точка на втором кадре</returns>
        private int FindNearestPoint(int p) {
            int index = -1;
            double minLenght = 0;
            if (prevFrame.Objects.Count == 0) {
                return index;
            }
            for (int i = 0; i < prevFrame.Objects.Count; i++) {
                if (fix[i]) {
                    continue;
                }
                minLenght = Length(currentFrame.Objects[p], prevFrame.Objects[i]);
                index = i;
                break;
            }
            for (int i = 1; i < prevFrame.Objects.Count; i++) {
                if (fix[i]) {
                    continue;
                }
                double l = Length(currentFrame.Objects[p], prevFrame.Objects[i]);
                if (l < minLenght) {
                    minLenght = l;
                    index = i;
                }
            }
            return index;
        }

        /// <summary>
        /// Ставит в соответствие всем точкам на первом кадре ближайшие точки на втором кадре
        /// </summary>
        private void FindNearestPoints() {
            indexes = new int[currentFrame.Objects.Count];
            fix = new bool[prevFrame.Objects.Count];
            for (int i = 0; i < fix.Length; i++) {
                fix[i] = false;
            }
            for (int i = 0; i < indexes.Length; i++) {
                indexes[i] = FindNearestPoint(i);
            }
        }

        /// <summary>
        /// Ищем конфликты
        /// </summary>
        /// <returns>Номер конфликтующей точки на втором изображении</returns>
        private int FindConflict() {
            int[] test_array = new int[prevFrame.Objects.Count];
            for (int i = 0; i < prevFrame.Objects.Count; i++) {
                test_array[i] = 0;
            }
            foreach (int index in indexes) {
                if (index >= 0) {
                    test_array[index]++;
                }
            }
            for (int i = 0; i < prevFrame.Objects.Count; i++) {
                if (test_array[i] > 1) {
                    return i;
                }
            }
            return -1;
        }

        private bool IsOnePoint() {
            int points = 0;
            for (int i = 0; i < prevFrame.Objects.Count; i++) {
                if (fix[i]) {
                    continue;
                }
                points++;
                if (points > 1) {
                    return false;
                }
            }
            return true;
        }

        private void ProcessOnePoint(int err) {
            double minLenght = 0;
            int minIndex = -1;
            for (int i = 0; i < currentFrame.Objects.Count; i++) {
                if (indexes[i] != err) {
                    continue;
                }
                minLenght = Length(currentFrame.Objects[i], prevFrame.Objects[err]);
                minIndex = i;
                break;
            }
            for (int i = 0; i < currentFrame.Objects.Count; i++) {
                if (indexes[i] != err) {
                    continue;
                }
                int near = FindNearestPoint(i);
                indexes[i] = near;
                double lenght = Length(currentFrame.Objects[i], prevFrame.Objects[err]);
                if (lenght < minLenght) {
                    minLenght = lenght;
                    minIndex = i;
                }
            }
            indexes[minIndex] = err;
        }

        /// <summary>
        /// Разрешаем конфликты
        /// </summary>
        private void SolveConflicts() {
            int err;
            while ((err = FindConflict()) != -1) {
                fix[err] = true;
                if (IsOnePoint()) {
                    ProcessOnePoint(err);
                    continue;
                }
                double maxLenght = 0;
                int maxIndex = -1;
                for (int i = 0; i < currentFrame.Objects.Count; i++) {
                    if (indexes[i] != err) {
                        continue;
                    }
                    int near = FindNearestPoint(i);
                    double lenght1 = near > -1 ? Length(currentFrame.Objects[i], prevFrame.Objects[near]) : 0;
                    double lenght2 = Length(currentFrame.Objects[i], prevFrame.Objects[err]);
                    maxLenght = Math.Abs(lenght2 - lenght1);
                    maxIndex = i;
                    break;
                }
                for (int i = 0; i < currentFrame.Objects.Count; i++) {
                    if (indexes[i] != err) {
                        continue;
                    }
                    int near = FindNearestPoint(i);
                    indexes[i] = near;
                    double lenght1 = near > -1 ? Length(currentFrame.Objects[i], prevFrame.Objects[near]) : 0;
                    double lenght2 = Length(currentFrame.Objects[i], prevFrame.Objects[err]);
                    double lenght = Math.Abs(lenght2 - lenght1);
                    if (lenght > maxLenght) {
                        maxLenght = lenght;
                        maxIndex = i;
                    }
                }
                indexes[maxIndex] = err;
            }
        }
    }

    public class PointOnTrace {
        public ViewObject Point { get; set; }

        public string Name { get; set; }

        public PointOnTrace(IObjectWithProperties p, string name) {
            Point = (ViewObject)p;
            Name = name;
        }
    }

    public class Point : ViewObject {
        private const string STR_R = "R";
        private const string STR_V = "V";
        private const string STR_Vx = "Vx";
        private const string STR_Vy = "Vy";
        private const string STR_Angle = "Angle";
        private const string STR_Direction = "Direction";
        private const string STR_Radius = "Radius";
        private const string STR_AV = "AV";
        private const string STR_OldName = "OldName";
        public Point() {
            properties.Add(STR_R, null);
            properties.Add(STR_V, null);
            properties.Add(STR_Vx, null);
            properties.Add(STR_Vy, null);
            properties.Add(STR_Angle, null);
            properties.Add(STR_AV, null);
            properties.Add(STR_Direction, null);
            properties.Add(STR_Radius, null);
            properties.Add(STR_OldName, null);
        }

        public Point(string name, double x, double y, int width, int height)
            : base(name, x, y, width, height) {
        }

        public double R {
            get { return (double)properties[STR_R]; }
            set { properties[STR_R] = value; }
        }

        public double V {
            get { return (double)properties[STR_V]; }
            set { properties[STR_V] = value; }
        }

        public double Vx {
            get { return (double)properties[STR_Vx]; }
            set { properties[STR_Vx] = value; }
        }

        public double Vy {
            get { return (double)properties[STR_Vy]; }
            set { properties[STR_Vy] = value; }
        }

        public double Angle {
            get { return (double)properties[STR_Angle]; }
            set { properties[STR_Angle] = value; }
        }

        public double AV {
            get { return (double)properties[STR_AV]; }
            set { properties[STR_AV] = value; }
        }

        public int Direction {
            get { return (int)properties[STR_Direction]; }
            set { properties[STR_Direction] = value; }
        }

        public double Radius {
            get { return (double)properties[STR_Radius]; }
            set { properties[STR_Radius] = value; }
        }

        public string OldName {
            get { return (string)properties[STR_OldName]; }
            set { properties[STR_OldName] = value; }
        }

        private static IObjectWithProperties pattern;
        public static new IObjectWithProperties GetPattern() {
            return pattern;
        }

        static Point() {
            pattern = new ObjectWithProperties();
            IObjectWithProperties source = ViewObject.GetPattern();
            pattern.Assign(source);
            pattern.Properties.Add(STR_R, typeof(double));
            pattern.Properties.Add(STR_V, typeof(double));
            pattern.Properties.Add(STR_Vx, typeof(double));
            pattern.Properties.Add(STR_Vy, typeof(double));
            pattern.Properties.Add(STR_Angle, typeof(double));
            pattern.Properties.Add(STR_AV, typeof(double));
            pattern.Properties.Add(STR_Direction, typeof(int));
            pattern.Properties.Add(STR_Radius, typeof(double));
            pattern.Properties.Add(STR_OldName, typeof(string));

        }
    }
}
