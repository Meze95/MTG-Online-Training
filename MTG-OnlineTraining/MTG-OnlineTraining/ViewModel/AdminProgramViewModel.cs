using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTG_OnlineTraining.ViewModel
{
    public class AdminProgramViewModel
    {
        [Required]
        public string ProgramName { get; set; }
        [Required]
        public string ProgramDescription { get; set; }
        [Required]
        public string ProgramImg { get; set; }
        [Required]
        [NotMapped]
        public IFormFile ProgramImageUrl { get; set; }
        [Required]
        public int Duration { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
