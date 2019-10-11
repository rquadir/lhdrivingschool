using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using LHDS.Models;
using Newtonsoft.Json;
using System.Configuration;
using CaptchaMvc.HtmlHelpers;

namespace LHDS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        // GET: Home
        //[Route("ContactUs")]

        public ActionResult ContactUs()
        {
            //ContactViewModel model = new ContactViewModel();
            //model.Contacts = new ContactUs();
            //model.Appointments = new Appointment();


            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //if (!this.IsCaptchaValid(""))
                //{
                //    ViewBag.ErrorMessage1 = "Please validate the captcha.";
                //    return View();
                //}
                //else { }

                ViewBag.Message = "Your mail sent.";

                string body = "Hi,<br /><br />The following information has been submitted from the Long Horn Driving School Contact form:" + "<br/>" + "<br/>" +
                    "<table style=\"color:black !important;\">" +
                   "<tr>" +
                   "<td style=\"width:150px;\">" + "<strong>Name</strong>" + "</td>" +
                   "<td>" + Server.HtmlEncode(model.Contacts.Name) + "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td style=\"width:150px;\">" + "<strong>Email</strong>" + "</td>" +
                   "<td>" + Server.HtmlEncode(model.Contacts.Email) + "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td style=\"width:150px;\">" + "<strong>Phone</strong>" + "</td>" +
                   "<td>" + Server.HtmlEncode(model.Contacts.Phone) + "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td style=\"width:150px;\">" + "<strong>Message</strong>" + "</td>" +
                   "<td>" + Server.HtmlEncode(model.Contacts.Message) + "</td>" +
                   "</tr>" + "</table>" + "<br/>Thanks and regards,<br/><b>Long Horn Driving School Development Team.</b>" + "<br/>[This is an automatically generated email, please do not reply]";


                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(StaticData.SMTPClient);
                smtpServer.Credentials = new System.Net.NetworkCredential(StaticData.MailSendingCredentialEmail, StaticData.MailSendingCredentialPassword);
                //smtpServer.Port = 10;
                smtpServer.EnableSsl = false;

                mail.From = new MailAddress(StaticData.EmailFrom);
                mail.To.Add(StaticData.InfoEmailAddress);
                mail.CC.Add(StaticData.AdminEmailAddress);
                mail.Bcc.Add(StaticData.AdminEmailAddress);
                mail.Subject = "Contact request from Long Horn Driving School website";
                mail.IsBodyHtml = true;
                mail.Body = body;
                smtpServer.Send(mail);
                TempData["message"] = "Sent";
                ModelState.Clear();
            }
            
            return View("ContactUs", model);
        }

        //[CaptchaMvc.Attributes.CaptchaVerify("Please validate the captcha.")]
        [HttpPost]
        public ActionResult Appointment(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //if (!ModelState.IsValid)
                //{
                //    ViewBag.ErrorMessage2 = "Please validate the captcha.";
                //    //TempData["ErrorMessage2"] = "Please validate the captcha.";
                //    //ViewBag.data = TempData["ErrorMessage2"].ToString();
                //    //TempData["ErrorMessage2"] = string.Format("Please validate the captcha.");
                //    return View();
                //}
                //else { }

                ViewBag.Message = "Your Email Sent.";

                string body = "Hi,<br/><br/>The following information has been submitted from the Long Horn Driving School Appointment form:" + "<br/>" + "<br/>" +
                        "<table style=\"color:black !important;\">" +
                       "<tr>" +
                       "<td style=\"width:150px;\">" + "<strong>Date</strong>" + "</td>" +
                       "<td>" + Server.HtmlEncode(model.Appointments.DatePicker) + "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td style=\"width:150px;\">" + "<strong>Time</strong>" + "</td>" +
                       "<td>" + Server.HtmlEncode(model.Appointments.TimePicker) + "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td style=\"width:150px;\">" + "<strong>Name</strong>" + "</td>" +
                       "<td>" + Server.HtmlEncode(model.Appointments.FirstName) + " " + Server.HtmlEncode(model.Appointments.LastName) + "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td style=\"width:150px;\">" + "<strong>Email</strong>" + "</td>" +
                       "<td>" + Server.HtmlEncode(model.Appointments.Email) + "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td style=\"width:150px;\">" + "<strong>Phone</strong>" + "</td>" +
                       "<td>" + Server.HtmlEncode(model.Appointments.Phone) + "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td style=\"width:150px;\">" + "<strong>Permit Number</strong>" + "</td>" +
                       "<td>" + Server.HtmlEncode(model.Appointments.LearnersPermitNumber) + "</td>" +
                       "</tr>" + "</table>" + "<br/>Thanks and regards,<br/><b>Long Horn Driving School Development Team.</b>" + "<br/>[This is an automatically generated email, please do not reply]";

                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(StaticData.SMTPClient);
                smtpServer.Credentials = new NetworkCredential(StaticData.MailSendingCredentialEmail, StaticData.MailSendingCredentialPassword);
                //smtpServer.Port = 10;
                smtpServer.EnableSsl = false;

                mail.From = new MailAddress(StaticData.EmailFrom);
                mail.To.Add(StaticData.InfoEmailAddress);
                mail.CC.Add(StaticData.AdminEmailAddress);
                mail.Bcc.Add(StaticData.AdminEmailAddress);
                mail.Subject = "Appointment request from Long Horn Driving School website";
                mail.IsBodyHtml = true;
                mail.Body = body;
                smtpServer.Send(mail);
                TempData["message"] = "Sent";
                ModelState.Clear();
            }
            return View("ContactUs", model);
        }

        // GET: Home
        //[Route("Services")]
        public ActionResult Services()
        {
            return View();
        }

        // GET: Home
        //[Route("Vision")]
        public ActionResult Vision()
        {
            return View();
        }

        // GET: Home
        //[Route("Commitment")]
        public ActionResult Commitment()
        {
            return View();
        }
    }
}