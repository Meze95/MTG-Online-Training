using MTG_OnlineTraining.Models;
using MTG_OnlineTraining.ViewModel;

namespace MTG_OnlineTraining.Services
{
    public interface IAccountServices
    {
        Task<string> UserRegistraion(RegisterViewModel registerViewModel);
        Task<string> AdminRegistraion(AdminRegistrationViewModel adminRegistrationViewModel);
        Task<bool> UserLogin(LoginViewModel loginViewModel);
        Task<bool> LogOut();
        public List<ApplicationUser> GetAllPendingStudentsRegFromTheTable();
        public List<ApplicationUser> GetApprovedStudentsFromTheTable();
        public EditProfileViewModel EditStudentProfile(string username);
        public string UpdateTheEditedProfile(EditProfileViewModel editProfileViewModel);
        public string StudentDeactivation(string userId);
        public string StudentActivation(string userId);
    }
}
