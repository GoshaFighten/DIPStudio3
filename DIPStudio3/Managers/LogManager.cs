using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace DIPStudio3 {
    public class LogManager {
        static FileInfo errorLog = new FileInfo("errors_" + DateTime.Today.ToShortDateString() + ".txt");

        public static void ProcessError(Exception exception) {
            ReportError("CallStack");
            ReportError(exception.StackTrace);
        }

        public static void ReportError(string message) {
            StreamWriter writer = errorLog.AppendText();
            writer.WriteLine(DateTime.Now.ToString());
            writer.WriteLine(message);
            writer.Close();
        }
    }
}
