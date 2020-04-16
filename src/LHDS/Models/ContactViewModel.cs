using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LHDS.Models
{
    public class ContactViewModel
    {
        public ContactUs Contacts { get; set; }

        public Appointment Appointments { get; set; }

        public string GoogleCaptchaTokenForContact { get; set; }

        public string GoogleCaptchaTokenForAppointment { get; set; }
    }
}