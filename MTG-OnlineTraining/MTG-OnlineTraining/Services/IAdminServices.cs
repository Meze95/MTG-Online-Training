using MTG_OnlineTraining.Models;
using MTG_OnlineTraining.ViewModel;

namespace MTG_OnlineTraining.Services
{
    public interface IAdminServices
    {
        public string UploadedFile(IFormFile filesUrl, string FileLocation);
        public string ProgramCreate(AdminProgramViewModel adminProgramViewModel);
        public List<AdminProgram> GetProgramFromTheTable();
        public string MaterialCreate(MaterialsViewModel materialsViewModel);
        public List<Materials> GetMaterialsFromTheTable();
        public List<AdminProgram> AllPrograms();
        public StudentPrograms ProgramActivation(Guid Id);
        public StudentPrograms ProgramDeactivation(Guid Id);
        public AdminProgramViewModel EditandDeleteProgramView(int? Id);
        public string DeleteSelectedProgram(AdminProgramViewModel adminProgramViewModel, int? Id);
        public string UpdateTheEditedProgram(AdminProgramViewModel adminProgramViewModel, int? Id);
    }
}
