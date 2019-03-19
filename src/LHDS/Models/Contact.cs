using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LHDS.Models
{
    public class Contact
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"([a-zA-Z0-9_\.\+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-\.]+)", ErrorMessage = "Invalid Email Address! Email Ex. test@yahoo.com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Message { get; set; }

        public bool SuccessfullySent { get; set; }

        public string Result { get; set; }
    }
}