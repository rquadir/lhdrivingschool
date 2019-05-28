using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace LHDS.Models
{
    public class ContactUs
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid Name! Name Ex. John Doe")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"([a-zA-Z0-9_\.\+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-\.]+)", ErrorMessage = "Invalid Email Address! Email Ex. test@yahoo.com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Invalid phone number! Phone number must be 10 digits.")]
        public string Phone { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 4, ErrorMessage = "Please enter your message.")]
        public string Message { get; set; }

        public bool SuccessfullySent { get; set; }

        public string Result { get; set; }
    }
}