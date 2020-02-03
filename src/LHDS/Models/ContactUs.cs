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
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Invalid Email Address! Email Ex. test@yahoo.com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]( |-)?)?(\(?[0-9]{3}\)?|[0-9]{3})( |-)?([0-9]{3}( |-)?[0-9]{4}|[a-zA-Z0-9]{7})$", ErrorMessage = "Invalid phone number! Phone Ex. 1-(123)-123-1234 | 123 123 1234")]
        public string Phone { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 4, ErrorMessage = "Please enter your message.")]
        public string Message { get; set; }

        public string GoogleCaptchaToken { get; set; }

        public bool SuccessfullySent { get; set; }

        public string Result { get; set; }
    }
}