using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AddressBook.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Init this app
            WebAppContext.Initialize(
                Server.MapPath(ConfigurationManager.AppSettings["AddressBook.DataFilePath"])
            );
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            ExceptionsLogging.Info("Application has Started, " + sender.GetType().Name);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            ExceptionsLogging.Info("Application has Begin Request, " + sender.GetType().Name);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            ExceptionsLogging.Info("Application has Authenticate Request, " + sender.GetType().Name);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            exception.Log("Fatal Error occured: \r\n");
        }

        protected void Session_End(object sender, EventArgs e)
        {
            ExceptionsLogging.Info("Application has Session End, " + sender.GetType().Name);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            ExceptionsLogging.Info("Application has Application End, " + sender.GetType().Name);
        }
    }
}