using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTG_OnlineTraining.Models
{
    public class Materials
    {
        [Key]
        public int Id { get; set; }
        public int? AdminProgramId { get; set; }
        [Display(Name = "AdminProgram")]
        [ForeignKey("AdminProgramId")]
        public virtual AdminProgram AdminProgram { get; set; }
        public string Title { get; set; }
        public string FlowOrder { get; set; }
        public string File { get; set; }
    }
}
