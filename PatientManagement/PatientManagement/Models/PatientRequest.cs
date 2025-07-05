using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Models
{
    public class PatientRequest
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50), MinLength(2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50), MinLength(2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [Phone]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter a 10-digit contact number")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(1, 200, ErrorMessage = "Weight must be between 1 and 200kg")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "Height is required")]
        [Range(1, 250, ErrorMessage = "Height must be between 1 and 250cm")]
        public float Height { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string MedicalComments { get; set; }

        [Display(Name = "Any Medications Taking")]
        public bool AnyMedicationsTaking { get; set; }
    }
}
