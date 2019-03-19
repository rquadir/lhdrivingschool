using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using LHDS.Models;

namespace LHDS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home
        public ActionResult About()
        {
            return View();
        }

        // GET: Home
        public ActionResult DriversEducation()
        {
            return View();
        }

        // GET: Home
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            ViewBag.Message = "Your Email Sent.";

            string body = "Hi,<br/><br />The following information has been submitted from the Long Horn Driving School Contact form:" + "<br/>" + "<br/>" +
                    "<table style=\"color:black !important;\">" +
                   "<tr>" +
                   "<td style=\"width:150px;\">" + "<strong>Name</strong>" + "</td>" +
                   "<td>" + "Raisul Showmin" + "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td style=\"width:150px;\">" + "<strong>Email</strong>" + "</td>" +
                   "<td>" + "Simplexhub@something.com" + "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td style=\"width:150px;\">" + "<strong>Phone</strong>" + "</td>" +
                   "<td>" + "0123456789" + "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td style=\"width:150px;\">" + "<strong>Message</strong>" + "</td>" +
                   "<td>" + "Test Message" + "</td>" +
                   "</tr>" + "</table>" + "<br/><br/><br/>Thanks and regards,<br/>Long Horn Driving School Development Team.";

            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Credentials = new System.Net.NetworkCredential("longhorn0786@gmail.com", "Dhaka123");
            smtpServer.Port = 587;
            smtpServer.EnableSsl = true;

            mail.From = new MailAddress("longhorn@gmail.com");
            mail.To.Add("rishowmin.seu38@gmail.com");
            //mail.CC.Add("");
            mail.Subject = "Contact request from Long Horn Driving School website";
            mail.IsBodyHtml = true;
            mail.Body = body;

            smtpServer.Send(mail);

            model.SuccessfullySent = true;

            return View();
        }

        // GET: Home
        public ActionResult AboutService1()
        {
            return View();
        }


        // GET: Home
        public ActionResult AboutService2()
        {
            return View();
        }

        // GET: Home
        public ActionResult AboutService3()
        {
            return View();
        }

        // GET: Home
        public ActionResult AboutService4()
        {
            return View();
        }

        // GET: Home
        public ActionResult AboutService5()
        {
            return View();
        }
    }
}