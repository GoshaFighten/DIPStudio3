using System;
using System.Collections.Generic;
using ICommonInterface;

namespace ImageModel {
    public class DataHelper {
        ViewFrame _CurrentFrame;

        FrameList _FrameList;

        bool fCreateData;

        public DataHelper(bool createData) {
            fCreateData = createData;
        }

        public void Initialize() {
            _FrameList = new FrameList();
        }

        public bool IsWorking {
            get { return fCreateData; }
        }

        public void AddFrame(double time) {
            if(!IsWorking) {
                return;
            }
            _CurrentFrame = _FrameList.AddFrame(_FrameList.Count.ToString(), time);
        }

        public void Log(string name, double x0, double y0, double width, double height, double time) {
            if(!IsWorking) {
                return;
            }
            ViewObject obj = new ViewObject();
            obj.Name = name;
            obj.X = x0;
            obj.Y = y0;
            obj.Width = width;
            obj.Height = height;
            _CurrentFrame.Objects.Add(obj);
        }

        public void Finalize(IApp app, double startTime, double discretization, string name) {
            if(!IsWorking) {
                return;
            }
            Data data = new Data(_FrameList) { Name = name, Discretization = discretization, StartTime = startTime };
            app.AddData(data);
        }
    }
}
