using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace DIPStudioCore.Base {
    public abstract class FrameElement : INotifyPropertyChanged, IDisposable {
        public FrameElement(int time) {
            fTime = time;
        }

        // Fields...
        private string fName;

        private int fTime;

        [DisplayName("Время")]
        public int Time {
            get { return fTime; }
        }

        [DisplayName("Имя")]
        public string Name {
            get { return fName; }
            set {
                if(fName == value) {
                    return;
                }
                fName = value;
                OnPropertyChanged("Name");
            }
        }

        protected virtual void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private IList fOwner;

        public IList Owner {
            get { return fOwner; }
            set { fOwner = value; }
        }

        public FrameElement GetNextElement(int offset) {
            int index = Owner.IndexOf(this) + offset;
            if(index >= Owner.Count) {
                return null;
            }
            return (FrameElement)Owner[index];
        }

        public FrameElement GetPrevElement(int offset) {
            int index = Owner.IndexOf(this) - offset;
            if(index <= -1) {
                return null;
            }
            return (FrameElement)Owner[index];
        }

        public FrameElement GetNextElement() {
            return GetNextElement(1);
        }

        public FrameElement GetPrevElement() {
            return GetPrevElement(1);
        }

        public void Dispose()
        {
            DisposeCore();
        }

        protected virtual void DisposeCore() {
            this.Owner = null;
        }
    }
}
