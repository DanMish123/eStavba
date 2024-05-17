using System.ComponentModel.DataAnnotations;

namespace eStavba.Models
{
    public class ReportProblemModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "First name should only contain letters.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Last name should only contain letters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Apartment Number")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Apartment number should only contain digits.")]
        public string ApartmentNumber { get; set; }

        [Required]
        [Display(Name = "Problem Description")]
        [DataType(DataType.MultilineText)]
        public string ProblemDescription { get; set; }
    }
}
