using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTG_OnlineTraining.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DOB { get; set; }
        public string StateOfOrigin { get; set; }
        public string Address { get; set; }
        public string ModeOfTraining { get; set; }
        public string Passport { get; set; }
        public bool IsAdmin { get; set; }
        public bool Deactivated { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime StartDate { get; set; }

    }
}
