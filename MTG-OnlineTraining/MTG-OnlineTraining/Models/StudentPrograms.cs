using MTG_OnlineTraining.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTG_OnlineTraining.Models
{
    public class StudentPrograms 
    {
        [Key]
        public Guid Id { get; set; }
        public int? AdminProgramId { get; set; }
        [Display(Name = "AdminProgram")]
        [ForeignKey("AdminProgramId")]
        public virtual AdminProgram AdminProgram { get; set; }

        public string UserId { get; set; }
        [Display(Name = "User")]
        [ForeignKey("UserId")]
        public virtual ApplicationUser Trainees { get; set; }
        public bool IsActive { get; set; }
        public TrainingEnum ModeOfTraining { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
}
