using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace System
{
    public static class ExceptionsLogging
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly IList<log4net.ILog> logs = log4net.LogManager.GetCurrentLoggers();
        public static void Log(this Exception e, object msg)
        {
            log.Error("\r\n" + msg, e);
        }
        public static void Log(this Exception e)
        {
            log.Error("\r\n", e);
        }

        public static void Info(this string message)
        {
            log.Info(Environment.NewLine + message);
        }

        public static void LogTo(string logger, string msg)
        {
            logs.FirstOrDefault(l => l.Logger.Name.ToLower().Trim() == logger.ToLower().Trim());
        }

    }
}

namespace AddressBook.Helpers
{
    public class TraceExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TraceExceptionLogger));

        public override void Log(ExceptionLoggerContext context)
        {
            string postData = string.Empty;

            try
            {
                var data = ((System.Web.Http.ApiController)
          context.ExceptionContext.ControllerContext.Controller)
          .ActionContext.ActionArguments.Values;

                postData = JsonConvert.SerializeObject(data);
            }
            catch (Exception e)
            {
                e.Log("TraceExceptionLogger Error");
                // DO SOMETHING ??
            }

            Logger.FatalFormat("Fatal error WEBAPI:  URL:{ 0}  »» METHOD: {1}  »» POST DATA: {2} »» Exception: {3}", context.Request.RequestUri, context.Request.Method, postData, context.ExceptionContext.Exception);
        }
    }
}