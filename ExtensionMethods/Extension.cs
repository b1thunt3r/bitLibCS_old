using System;

namespace Bit.BitLib.ExtensionMethods
{
    /// <summary>
    /// Collection of static functions
    /// </summary>
    public static class Extension
    {
        private static LogLevels m_LogLevel = LogLevels.Verbose;

        public static LogLevels LogLevel
        {
            get { return m_LogLevel; }
            set { m_LogLevel = value; }
        }

        private static LogHandles m_LogHandle = LogHandles.VisualAndFile;

        public static LogHandles LogHandle
        {
            get { return m_LogHandle; }
            set { m_LogHandle = value; }
        }

        public static void Pause<T>(this T obj)
        {
            Pause(obj, "Press any key to continue... ");
        }

        public static void Pause<T>(this T obj, string text)
        {
            Console.Write("\r\n" + text);
            Console.ReadKey();
        }

        public static T ExceptionHandle<T>(this T obj, LogLevels loglevel, Exception ex)
        {
            return ExceptionHandle<T>(obj, loglevel, ex.Message);
        }

        public static T ExceptionHandle<T>(this T obj, LogLevels loglevel, string ex)
        {
            return ExceptionHandle<T>(obj, loglevel, m_LogHandle, ex);
        }

        public static T ExceptionHandle<T>(this T obj, LogLevels loglevel, LogHandles hadle, Exception ex)
        {
            return ExceptionHandle<T>(obj, loglevel, hadle, ex.Message);
        }

        public static T ExceptionHandle<T>(this T obj, LogLevels loglevel, LogHandles handle, String ex)
        {
            if (handle.Equals(LogHandles.VisualOnly) || handle.Equals(LogHandles.VisualAndFile))
            {
                HandleLog.VisualLog(loglevel, obj.GetType().FullName, ex);
            }
            if (handle.Equals(LogHandles.FileOnly) || handle.Equals(LogHandles.VisualAndFile))
            {
                HandleLog.LogToFile(loglevel, obj.GetType().FullName, ex);
            }

            return default(T);
        }
    }
}