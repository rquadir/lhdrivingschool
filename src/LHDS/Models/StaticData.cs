using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LHDS.Models
{
    public class StaticData
    {
        public static string AdminEmailAddress = (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["AdminEmailAddress"].ToString())) ? ConfigurationManager.AppSettings["AdminEmailAddress"].ToString() : "admin@lhdrivingschool.com";
        public static string InfoEmailAddress = (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["InfoEmailAddress"].ToString())) ? ConfigurationManager.AppSettings["InfoEmailAddress"].ToString() : "info@lhdrivingschool.com";
        public static string EmailFrom = (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailFrom"].ToString())) ? ConfigurationManager.AppSettings["EmailFrom"].ToString() : "noreply@lhdrivingschool.com";
        public static string ErrorEmailTo = (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ErrorEmailTo"].ToString())) ? ConfigurationManager.AppSettings["ErrorEmailTo"].ToString() : "developer@lhdrivingschool.com";

        public static string SMTPClient = "mail.lhdrivingschool.com";
        public static string MailSendingCredentialEmail = "noreply@lhdrivingschool.com";
        public static string MailSendingCredentialPassword = "noreply!234";
    }
}