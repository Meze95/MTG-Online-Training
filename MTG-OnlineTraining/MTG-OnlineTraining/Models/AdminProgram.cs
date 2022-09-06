using System.ComponentModel.DataAnnotations;

namespace MTG_OnlineTraining.Models
{
    public class AdminProgram
    {
        [Key]
        public int Id { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
        public string ProgramImg { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Duration { get; set; }
    }
}
