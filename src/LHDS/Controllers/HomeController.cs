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
using System.Threading.Tasks;
using System.Net.Http;

namespace LHDS.Controllers
{
    public class HomeController : Controller
    {
        public class CaptchaResponseViewModelForContact
        {
            public bool Success { get; set; }
            [JsonProperty(PropertyName = "error-codes")]
            public IEnumerable<string> ErrorCodes { get; set; }
            [JsonProperty(PropertyName = "challenge_ts")]
            public DateTime ChallengeTime { get; set; }
            public string HostName { get; set; }
            public double Score { get; set; }
            public string Action { get; set; }
        }

        public class CaptchaResponseViewModelForAppointment
        {
            public bool Success { get; set; }
            [JsonProperty(PropertyName = "error-codes")]
            public IEnumerable<string> ErrorCodes { get; set; }
            [JsonProperty(PropertyName = "challenge_ts")]
            public DateTime ChallengeTime { get; set; }
            public string HostName { get; set; }
            public double Score { get; set; }
            public string Action { get; set; }
        }

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
             return View();
        }

        [HttpPost]
        public async Task<ActionResult> ContactUs(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isCaptchaValidForContact = await IsCaptchaValidForContact(model.GoogleCaptchaTokenForContact);
                if (isCaptchaValidForContact)
                {
                    ViewBag.Message = "Your mail sent.";

                    string body = "Hi,<br /><br />The following information has been submitted from the Learning Hub Driving School Contact form:" + "<br/>" + "<br/>" +
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
                       "</tr>" + "</table>" + "<br/>Thanks and regards,<br/><b>Learning Hub Driving School Development Team.</b>" + "<br/>[This is an automatically generated email, please do not reply]";


                    MailMessage mail = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient(StaticData.SMTPClient);
                    smtpServer.Credentials = new System.Net.NetworkCredential(StaticData.MailSendingCredentialEmail, StaticData.MailSendingCredentialPassword);
                    //smtpServer.Port = 10;
                    smtpServer.EnableSsl = false;

                    mail.From = new MailAddress(StaticData.EmailFrom);
                    mail.To.Add(StaticData.InfoEmailAddress);
                    //mail.To.Add(StaticData.TestEmailAddress);
                    mail.CC.Add(StaticData.AdminEmailAddress);
                    mail.Bcc.Add(StaticData.AdminEmailAddress);
                    mail.Subject = "Contact request from Learning Hub Driving School website";
                    mail.IsBodyHtml = true;
                    mail.Body = body;
                    smtpServer.Send(mail);
                    TempData["message"] = "Sent";
                    ModelState.Clear();

                    return RedirectToAction("ContactUs", model);
                }
                else
                {
                    ModelState.AddModelError("GoogleCaptcha", "The captcha is not valid");
                }
            }

            return View("ContactUs", model);
        }

        private async Task<bool> IsCaptchaValidForContact(string responseForContact)
        {
            try
            {
                var secretForContact = ConfigurationManager.AppSettings["ContactSecretKey"];
                using (var clientForContact = new HttpClient())
                {
                    var valuesForContact = new Dictionary<string, string>
                    {
                        { "secret", secretForContact },
                        { "response", responseForContact },
                        { "remoteip", Request.UserHostAddress }
                    };

                    var contentForContact = new FormUrlEncodedContent(valuesForContact);
                    var verifyForContact = await clientForContact.PostAsync("https://www.google.com/recaptcha/api/siteverify", contentForContact);
                    var captchaResponseJsonForContact = await verifyForContact.Content.ReadAsStringAsync();
                    var captchaResultForContact = JsonConvert.DeserializeObject<CaptchaResponseViewModelForContact>(captchaResponseJsonForContact);
                    return captchaResultForContact.Success
                        && captchaResultForContact.Action == "contact_us"
                        && captchaResultForContact.Score > 0.5;
                }
            }
            catch(Exception)
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Appointment(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isCaptchaValidForAppointment = await IsCaptchaValidForAppointment(model.GoogleCaptchaTokenForAppointment);
                if (isCaptchaValidForAppointment)
                {
                    ViewBag.Message = "Your Email Sent.";

                    string body = "Hi,<br/><br/>The following information has been submitted from the Learning Hub Driving School Appointment form:" + "<br/>" + "<br/>" +
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
                           "</tr>" + "</table>" + "<br/>Thanks and regards,<br/><b>Learning Hub Driving School Development Team.</b>" + "<br/>[This is an automatically generated email, please do not reply]";

                    MailMessage mail = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient(StaticData.SMTPClient);
                    smtpServer.Credentials = new NetworkCredential(StaticData.MailSendingCredentialEmail, StaticData.MailSendingCredentialPassword);
                    //smtpServer.Port = 10;
                    smtpServer.EnableSsl = false;

                    mail.From = new MailAddress(StaticData.EmailFrom);
                    mail.To.Add(StaticData.InfoEmailAddress);
                    //mail.To.Add(StaticData.TestEmailAddress);
                    mail.CC.Add(StaticData.AdminEmailAddress);
                    mail.Bcc.Add(StaticData.AdminEmailAddress);
                    mail.Subject = "Appointment request from Learning Hub Driving School website";
                    mail.IsBodyHtml = true;
                    mail.Body = body;
                    smtpServer.Send(mail);
                    TempData["message"] = "Sent";
                    ModelState.Clear();

                    return RedirectToAction("ContactUs", model);
                }
            }
            return View("ContactUs", model);
        }

        private async Task<bool> IsCaptchaValidForAppointment(string responseForAppointment)
        {
            try
            {
                var secretForAppointment = ConfigurationManager.AppSettings["AppointmentSecretKey"];
                using (var clientForAppointment = new HttpClient())
                {
                    var valuesForAppointment = new Dictionary<string, string>
                    {
                        { "secret", secretForAppointment },
                        { "response", responseForAppointment },
                        { "remoteip", Request.UserHostAddress }
                    };

                    var contentForAppointment = new FormUrlEncodedContent(valuesForAppointment);
                    var verifyForAppointment = await clientForAppointment.PostAsync("https://www.google.com/recaptcha/api/siteverify", contentForAppointment);
                    var captchaResponseJsonForAppointment = await verifyForAppointment.Content.ReadAsStringAsync();
                    var captchaResultForAppointment = JsonConvert.DeserializeObject<CaptchaResponseViewModelForAppointment>(captchaResponseJsonForAppointment);
                    return captchaResultForAppointment.Success
                        && captchaResultForAppointment.Action == "appointment"
                        && captchaResultForAppointment.Score > 0.5;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // GET: Home
        //[Route("Services")]
        //public ActionResult Services()
        //{
        //    return View();
        //}

        // GET: Home
        //[Route("Services")]
        public ActionResult InCarTraining()
        {
            return View();
        }

        // GET: Home
        //[Route("Services")]
        public ActionResult TPSTDrivingTest()
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