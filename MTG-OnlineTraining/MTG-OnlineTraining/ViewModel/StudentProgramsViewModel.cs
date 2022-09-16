using MTG_OnlineTraining.Data.Enum;
using MTG_OnlineTraining.Models;
using System.ComponentModel.DataAnnotations;

namespace MTG_OnlineTraining.ViewModel
{
    public class StudentProgramsViewModel
    {
        
        public int? AdminProgramId { get; set; }
        public Guid Id { get; set; }
        public virtual AdminProgram AdminProgram { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser Trainees { get; set; }
        [Required]
        public TrainingEnum ModeOfTraining { get; set; }
        [Required]
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
}