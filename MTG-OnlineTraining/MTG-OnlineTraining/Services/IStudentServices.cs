using MTG_OnlineTraining.Models;
using MTG_OnlineTraining.ViewModel;

namespace MTG_OnlineTraining.Services
{
    public interface IStudentServices
    {
        public List<StudentPrograms> MyRegisteredPrograms(string username);
        public List<StudentPrograms> AllProgramReg();
        public string StudentProgramRegList(StudentProgramsViewModel studentProgramsViewModel, string username);
        public List<DropDown> GetDropDownEnumsList();
        public List<Materials> StudentActiveMaterials(string username);
        public string UpdateEditedStudentProgram(StudentProgramsViewModel studentProgramsViewModel);
        public string DeleteSelectedStudentProgram(StudentProgramsViewModel studentProgramsViewModel);
    }
}
