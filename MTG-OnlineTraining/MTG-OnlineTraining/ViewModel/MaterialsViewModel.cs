using MTG_OnlineTraining.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTG_OnlineTraining.ViewModel
{
    public class MaterialsViewModel
    {
        public int Id { get; set; }
        [Required]
        public int? AdminProgramId { get; set; }
        public virtual AdminProgram AdminProgram { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string FlowOrder { get; set; }
        [Required]
        public string File { get; set; }
        [Required]
        [NotMapped]
        public IFormFile HandOutUrl { get; set; }
    }
}
