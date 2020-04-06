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
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        public class CaptchaResponseViewModel
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
        //[Route("ContactUs")]
        public ActionResult ContactUs()
        {
             return View();
        }

        [HttpPost]
        public async Task<ActionResult> ContactUsAsync(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isCaptchaValidForContact = await IsCaptchaValidForContact(model.Contacts.GoogleCaptchaToken);
                if (isCaptchaValidForContact)
                {
                    return RedirectToAction("ContactUs");
                }
                else
                {
                    ModelState.AddModelError("GoogleCaptcha", "The captcha is not valid");
                }

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

        private async Task<bool> IsCaptchaValidForContact(string response)
        {
            try
            {
                var secret = "6LeyHecUAAAAAN1i7W82JtEMJ6IS0h3bdoInlUMc";
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        {"secret", secret},
                        {"response", response},
                        {"remoteip", Request.UserHostAddress}
                    };

                    var content = new FormUrlEncodedContent(values);
                    var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                    var captchaResponseJson = await verify.Content.ReadAsStringAsync();
                    var captchaResult = JsonConvert.DeserializeObject<CaptchaResponseViewModel>(captchaResponseJson);
                    return captchaResult.Success
                           && captchaResult.Action == "contact"
                           && captchaResult.Score > 0.5;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        [HttpPost]
        public async Task<ActionResult> AppointmentAsync(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isCaptchaValidForAppointment = await IsCaptchaValidForAppointment(model.Appointments.GoogleCaptchaToken);
                if (isCaptchaValidForAppointment)
                {
                    return RedirectToAction("ContactUs");
                }
                else
                {
                    ModelState.AddModelError("GoogleCaptcha", "The captcha is not valid");
                }

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

        private async Task<bool> IsCaptchaValidForAppointment(string response)
        {
            try
            {
                var secret = "6LfbHecUAAAAACoDdtC8RXpY2rrpvQVo17CYk-kl";
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        {"secret", secret},
                        {"response", response},
                        {"remoteip", Request.UserHostAddress}
                    };

                    var content = new FormUrlEncodedContent(values);
                    var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                    var captchaResponseJson = await verify.Content.ReadAsStringAsync();
                    var captchaResult = JsonConvert.DeserializeObject<CaptchaResponseViewModel>(captchaResponseJson);
                    return captchaResult.Success
                           && captchaResult.Action == "appointment"
                           && captchaResult.Score > 0.5;
                }
            }
            catch (Exception)
            {
                return false;
            }

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