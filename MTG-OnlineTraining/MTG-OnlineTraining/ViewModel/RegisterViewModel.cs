using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTG_OnlineTraining.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string StateOfOrigin { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ModeOfTraining { get; set; }
        [Required]
        [NotMapped]
        public IFormFile ImageUrl { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

    }
}
