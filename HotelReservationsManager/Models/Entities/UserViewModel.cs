using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HotelReservationsManager.Models.Entities
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        [Display(Name = "EGN")]
        public string EGN { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fire Date")]
        public DateTime FireDate { get; set; }
    }
}
