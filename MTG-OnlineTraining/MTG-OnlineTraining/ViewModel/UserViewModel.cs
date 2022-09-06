using MTG_OnlineTraining.Models;

namespace MTG_OnlineTraining.ViewModel
{
    public class UserViewModel
    {
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual AdminProgram Program { get; set; }
        public virtual List<AdminProgram> AdminProgramList { get; set; }
        public virtual List<Materials> materialsList { get; set; }
        public virtual Materials Materials { get; set; }
        public virtual StudentPrograms ActivePrograms { get; set; }
}
}
