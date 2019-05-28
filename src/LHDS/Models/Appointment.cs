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
        [RegularExpression(@"([a-zA-Z0-9_\.\+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-\.]+)", ErrorMessage = "Invalid Email Address! Email Ex. test@yahoo.com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Invalid phone number! Phone number must be 10 digits.")]
        public string Phone { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Please enter a valid permit number")]
        public string LearnersPermitNumber { get; set; }

        public bool SuccessfullySent { get; set; }

        public string Result { get; set; }
    }
}