using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTG_OnlineTraining.ViewModel
{
    public class EditProfileViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string StateOfOrigin { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [NotMapped]
        public IFormFile ImageUrl { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
