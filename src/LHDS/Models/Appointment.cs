using System.ComponentModel.DataAnnotations;

namespace LHDS.Models
{
    public class Appointment
    {
        [Required]
        public string DatePicker { get; set; }

        [Required]
        public string TimePicker { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid Name! Name Ex. John")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid Name! Name Ex. Doe")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Invalid Email Address! Email Ex. test@yahoo.com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]( |-)?)?(\(?[0-9]{3}\)?|[0-9]{3})( |-)?([0-9]{3}( |-)?[0-9]{4}|[a-zA-Z0-9]{7})$", ErrorMessage = "Invalid phone number! Phone Ex. 1-(123)-123-1234 | 123 123 1234")]
        public string Phone { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Please enter a valid permit number")]
        public string LearnersPermitNumber { get; set; }

        public bool SuccessfullySent { get; set; }

        public string Result { get; set; }
    }
}