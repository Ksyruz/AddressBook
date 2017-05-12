using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class Config
    {
        public static string GetSMTPServer() => ConfigurationManager.AppSettings["SMTPServer"];

        public static string GetSMTPServerPort() => ConfigurationManager.AppSettings["SMTPServerPort"];

        public static string GetSMTPServerUserName() => ConfigurationManager.AppSettings["SMTPServerUserName"];

        public static string GetSMTPServerPassword() => ConfigurationManager.AppSettings["SMTPServerPassword"];

        public static string GetFromEmail() => ConfigurationManager.AppSettings["FromEmail"];
    }
}