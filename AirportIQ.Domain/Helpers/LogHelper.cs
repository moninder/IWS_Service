using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace AirportIQ.Domain.Helpers
{
    public static class LogHelper
    {
        public static void Log(string ss)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("AirportIQ.Web.Verbose");

            log.Info(ss);
            System.Diagnostics.Debug.WriteLine(ss);
        }

        public static void LogError(string ss)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("AirportIQ.Web.ERROR");

            log.Error(ss);
            System.Diagnostics.Debug.WriteLine(ss);
        }
    }
}
