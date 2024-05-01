using System;
using System.Diagnostics;

namespace Providers.Common
{
    /// <summary>
    /// Clase estatica para loguear informacion al Trace de Diagnostico
    /// </summary>
    public static class Log
    {
        private static readonly object locker = new object();

        /// <summary>
        /// Log
        /// </summary>
        public static void Information(string message, params object[] args)
        {
            lock (locker)
            {
                WriteTrace(message, args);
            }
        }

        /// <summary>
        /// Log
        /// </summary>
        public static void Warning(string message, params object[] args)
        {
            lock (locker)
            {
                WriteTrace(message, args);
            }
        }

        /// <summary>
        /// Log
        /// </summary>
        public static void Error(string message, params object[] args)
        {
            lock (locker)
            {
                WriteTrace(message, args);
            }
        }

        private static void WriteTrace(string message, params object[] args)
        {
            lock (locker)
            {
                Trace.WriteLine($"{DateTime.Now} - {string.Format(message, args)}");
            }
        }
    }
}