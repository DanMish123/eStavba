using System.ComponentModel.DataAnnotations;

namespace eStavba.Models
{
    public class ReportProblemModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
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
        public string ApartmentNumber { get; set; }

        [Required]
        [Display(Name = "Problem Description")]
        [DataType(DataType.MultilineText)]
        public string ProblemDescription { get; set; }
    }
}
