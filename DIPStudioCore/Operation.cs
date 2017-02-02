using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace DIPStudioCore {
    public class Operation : INotifyPropertyChanged {
        private int? fCurrentTime;

        private long? fTimeElapsed;

        private OperationStatus fStatus;

        private IPlugin fPlugin;

        public IPlugin Plugin {
            get { return fPlugin; }
            set {
                if(fPlugin == value)
                    return;
                fPlugin = value;
                OnPropertyChanged("Plugin");
                OnPropertyChanged("Caption");
            }
        }

        public string Caption
        {
            get { return String.Format("[{0}] {1}", Plugin.Caption, Plugin.PluginSettings.ResultName); }
        }

        public OperationStatus Status {
            get { return fStatus; }
            set {
                if(fStatus == value)
                    return;
                OnPropertyChanged("Status");
                fStatus = value;
            }
        }

        public long? TimeElapsed {
            get { return fTimeElapsed; }
            set {
                if(fTimeElapsed == value)
                    return;
                fTimeElapsed = value;
                OnPropertyChanged("TimeElapsed");
            }
        }

        public int? CurrentTime {
            get { return fCurrentTime; }
            set {
                if(fCurrentTime == value)
                    return;
                fCurrentTime = value;
                OnPropertyChanged("CurrentTime");
            }
        }

        public void Execute(int t, int index, int tFinish)
        {
            if (Status == OperationStatus.Error)
                return; //don't do nothing, if there is some mistake
            Stopwatch watch = new Stopwatch();

            watch.Start();

                try {
                    Plugin.Run(t, index, tFinish);
                }
                catch {
                    Status = OperationStatus.Error;
                }
                finally {
                    watch.Stop();
                }
            TimeElapsed = TimeElapsed != null ? TimeElapsed + watch.ElapsedMilliseconds : watch.ElapsedMilliseconds;
            CurrentTime = t;  
        }

        protected virtual void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Operation Clone()
        {
            Operation result = new Operation() { 
                fCurrentTime = this.fCurrentTime,
                fTimeElapsed = this.fTimeElapsed,
                fStatus = this.fStatus,
                fPlugin = this.fPlugin,
            };
            return result;
        }
    }
    
    public enum OperationStatus {
        NotExecuted = 0,
        Executed,
        Error
    }
}
