using Microsoft.AspNetCore.Identity;
using MTG_OnlineTraining.Data;
using MTG_OnlineTraining.Models;
using MTG_OnlineTraining.ViewModel;

namespace MTG_OnlineTraining.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbConntext _db;
        private readonly IAdminServices _adminServices;
        private readonly IEmailServices _emailServices;
        private bool isPersistent;
        private bool lockoutOnFailure;

        public AccountServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment webHostEnvironment, ApplicationDbConntext db, IAdminServices adminServices, IEmailServices emailServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _db = db;
            _adminServices = adminServices;
            _emailServices = emailServices;
        }

        public async Task<string> UserRegistraion(RegisterViewModel registerViewModel, string linkToClick)
        {
            string passportFilePath = string.Empty;
            if(registerViewModel.ImageUrl == null)
            {
                return ("Upload passport");
            }
            else
            {
                passportFilePath = _adminServices.UploadedFile(registerViewModel.ImageUrl, "/StudentsPassport/");
            }
            if (registerViewModel.Password != registerViewModel.ConfirmPassword)
            {
                return ("Password does not match with its confirmation");
            }
            else if (registerViewModel.FirstName == null && registerViewModel.LastName == null && registerViewModel.DOB == null && registerViewModel.PhoneNumber == null && registerViewModel.StateOfOrigin == null && registerViewModel.Address == null && registerViewModel.Email == null)
            {
                return ("Fill all the required fields");
            }
            var EmailCheck = await _userManager.FindByEmailAsync(registerViewModel.Email);
            var phoneCheck = await _userManager.FindByEmailAsync(registerViewModel.PhoneNumber);
            if (phoneCheck != null)
            {
                return ("PhoneNumber Already Exist");
            }
            else if (EmailCheck != null)
            {
                return ("Email Already Exist");
            }
            else 
            {
                var users = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    MiddleName = registerViewModel.MiddleName,
                    DOB = registerViewModel.DOB,
                    PhoneNumber = registerViewModel.PhoneNumber,
                    StateOfOrigin = registerViewModel.StateOfOrigin,
                    Address = registerViewModel.Address,
                    Passport = passportFilePath,
                    Email = registerViewModel.Email,
                    Deactivated = true,
                    IsAdmin = false,
                };

             var result = await _userManager.CreateAsync(users, registerViewModel.Password);

                if (result.Succeeded)
                {
                    var createdStudent = _userManager.FindByEmailAsync(users.Email).Result;
                    if (createdStudent != null)
                    {
                        await _userManager.AddToRoleAsync(createdStudent, "Student");
                        string toEmail = createdStudent.Email;
                        string subject = "Registration Initiated";
                        string message = "Dear " + createdStudent.FirstName + ", You Initiated a registraion in our platform" + " Please click on the link below to complete your registration" + "<br>" +
                               "<a  href='" + linkToClick + "?userId=" +  createdStudent.Id + "' target='_blank'>" + "<button style='color:white; background-color:#018DE4; padding:10px; border:10px;'>Activate</button>" + "</a>";

                        _emailServices.SendEmail(toEmail, subject, message);

                        return ("Please Log into you mail to complete your registraion");
                    }
                    return ("Account registered successful");
                }
                return "Error! Password does not meet the criteria. Hint: Uppercase and Digit required";
            }
            return "Email already exsit!";
        }

        public async Task<string> AdminRegistraion(AdminRegistrationViewModel adminRegistrationViewModel)
        {
            var EmailCheck = await _userManager.FindByEmailAsync(adminRegistrationViewModel.Email);
            if (EmailCheck == null)
            {
                var users = new ApplicationUser
                {
                    UserName = adminRegistrationViewModel.Email,
                    FirstName = adminRegistrationViewModel.FirstName,
                    LastName = adminRegistrationViewModel.LastName,
                    PhoneNumber = adminRegistrationViewModel.PhoneNumber,
                    Email = adminRegistrationViewModel.Email,
                    Deactivated = false,
                    IsAdmin = true,
                };

                var result = await _userManager.CreateAsync(users, adminRegistrationViewModel.Password);

                if (result.Succeeded)
                {
                   var createdUser = _userManager.FindByEmailAsync(users.Email).Result;
                    if (createdUser != null)
                    {
                        await _userManager.AddToRoleAsync(createdUser, "Admin");
                    }
                    return ("Account registered successful");
                }
                return "Error! Password does not meet the criteria. Hint: Uppercase and Digit required";
            }
            return "Email already exsit!";
        }

        public async Task<bool> UserLogin(LoginViewModel loginViewModel)
        {
            var EmailCheck = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (EmailCheck != null && EmailCheck.Deactivated != true)
            {
                 var logger = _signInManager.PasswordSignInAsync(EmailCheck, loginViewModel.Password, isPersistent = true, lockoutOnFailure = false).Result;
                if (logger.Succeeded)
                {

                    return true;
                }
            };
            return false;
        }

        public async Task<bool> LogOut()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public List<ApplicationUser> GetApprovedStudentsFromTheTable()
        {
            var allStudents = _userManager.Users.Where(u => u.Email != null && u.Deactivated == false && u.IsAdmin == false).ToList();
            if (allStudents.Any())
            {
                return allStudents;
            }
            return allStudents;
        }

        public List<ApplicationUser> GetAllPendingStudentsRegFromTheTable()
        {
            var pendingReg = _userManager.Users.Where(u => u.Email != null && u.Deactivated == true && u.IsAdmin == false).ToList();
            if (pendingReg.Any())
            {
                return pendingReg;
            }
            return pendingReg;
        }

        public EditProfileViewModel EditStudentProfile(string username)
        {
            var profileDetail = _userManager.FindByNameAsync(username).Result;
            if (profileDetail != null)
            {
                var newprofile = new EditProfileViewModel
                {
                    Email = profileDetail.Email,
                    FirstName = profileDetail.FirstName,
                    LastName = profileDetail.LastName,
                    MiddleName = profileDetail.MiddleName,
                    DOB = profileDetail.DOB,
                    PhoneNumber = profileDetail.PhoneNumber,
                    StateOfOrigin = profileDetail.StateOfOrigin,
                    Address = profileDetail.Address,
                };

                if (newprofile != null)
                {
                    return newprofile;
                }
            }
            return null;
        }

        public string UpdateTheEditedProfile(EditProfileViewModel editProfileViewModel)
        {
            string passportFilePath = string.Empty;
            if (editProfileViewModel.ImageUrl == null)
            {
                return ("Upload passport");
            }
            else
            {
                passportFilePath = _adminServices.UploadedFile(editProfileViewModel.ImageUrl, "/StudentsPassport/");
            }

            var oldProfile = _userManager.FindByEmailAsync(editProfileViewModel.Email).Result;
            if (oldProfile != null)
            {
                oldProfile.FirstName = editProfileViewModel.FirstName;
                oldProfile.LastName = editProfileViewModel.LastName;
                oldProfile.MiddleName = editProfileViewModel.MiddleName;
                oldProfile.DOB = editProfileViewModel.DOB;
                oldProfile.PhoneNumber = editProfileViewModel.PhoneNumber;
                oldProfile.StateOfOrigin = editProfileViewModel.StateOfOrigin;
                oldProfile.Address = editProfileViewModel.Address;
                oldProfile.Passport = passportFilePath;

                _db.ApplicationUser.Update(oldProfile);
                _db.SaveChanges();

                return "Profile Updated Successfully";
            }
            else
            {
                return "Profile Upload Failed";
            }

        }

        public string StudentActivation(string userId)
        {
            var activated = _userManager.FindByIdAsync(userId).Result;
            if (activated != null)
            {
                activated.Deactivated = false;

                _db.ApplicationUser.Update(activated);
                _db.SaveChanges();

                return "Profile Activated Successfully";
            }
            else
            {
                return "Activation Fail";
            }

        }

        public string StudentDeactivation(string userId)
        {
            var deactivated = _userManager.FindByIdAsync(userId).Result;
            if (deactivated != null)
            {
                deactivated.Deactivated = true;

                _db.ApplicationUser.Update(deactivated);
                _db.SaveChanges();

                return "Profile Deactivated Successfully";
            }
            else
            {
                return "ctivation Fail";
            }

        }
    }
}
