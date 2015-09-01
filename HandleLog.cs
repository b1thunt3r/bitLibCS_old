using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Bit.BitLib
{
    public enum LogLevels
    {
        Off = 0,
        Error = 1,
        Warning = 2,
        Info = 3,
        Verbose = 4
    }

    public enum LogHandles
    {
        VisualOnly = 0,
        VisualAndFile = 1,
        FileOnly = 2
    }

    public class HandleLog
    {
        private static LogLevels m_LogLevel = LogLevels.Verbose;

        public static LogLevels LogLevel
        {
            get { return m_LogLevel; }
            set { m_LogLevel = value; }
        }

        public static void LogToFile(LogLevels loglevel, string type, String ex)
        {
            if (loglevel <= m_LogLevel)
            {
                // craete output to file
                TextWriterTraceListener writer = new TextWriterTraceListener(@"Error.log");
                Trace.Listeners.Add(writer);
                //output error
                if (System.Diagnostics.Trace.Listeners.Count > 0)
                {
                    Trace.Write(new TraceData(loglevel, DateTime.Now, type, ex));
                }
                //close streams
                writer.Close();
            }
        }

        public static void VisualLog(LogLevels loglevel, string type, String ex)
        {
            // create output to console
            TextWriterTraceListener console = new TextWriterTraceListener();
            console.Writer = Console.Out;
            Trace.Listeners.Add(console);
            //output error
            if (System.Diagnostics.Trace.Listeners.Count > 0)
            {
                Trace.Write(new TraceData(loglevel, DateTime.Now, type, ex));
            }
            //close streams
            console.Close();

            if (!ApplicationType.GetApplicationType().Equals(ApplicationType.Type.Console))
            {
                //messagebox
                MessageBoxIcon icon = MessageBoxIcon.None;
                switch (loglevel)
                {
                    case LogLevels.Error:
                        icon = MessageBoxIcon.Error;
                        break;
                    case LogLevels.Warning:
                        icon = MessageBoxIcon.Warning;
                        break;
                    case LogLevels.Info:
                        icon = MessageBoxIcon.Information;
                        break;
                }
                MessageBox.Show(ex, type, MessageBoxButtons.OK, icon);
            }
        }

        public class TraceData
        {
            public TraceData(LogLevels level, DateTime timestamp, string source, string message)
            {
                m_Level = level;
                m_TimeStamp = timestamp;
                m_Source = source;
                m_Message = message;
            }

            private LogLevels m_Level = LogLevels.Error;

            public LogLevels TraceMessageType
            {
                get { return m_Level; }
                set { m_Level = value; }
            }

            private String m_Source = String.Empty;

            public String Source
            {
                get { return m_Source; }
                set { m_Source = value; }
            }

            private DateTime m_TimeStamp = DateTime.Now;

            public DateTime TimeStamp
            {
                get { return m_TimeStamp; }
                set { m_TimeStamp = value; }
            }

            private String m_Message = String.Empty;

            public String Message
            {
                get { return m_Message; }
                set { m_Message = value; }
            }

            public override String ToString()
            {
                return String.Format("[{2}] [{0}] [{1}] {3}\r\n", m_Level, m_Source, m_TimeStamp, m_Message);
            }
        }
    }
}
