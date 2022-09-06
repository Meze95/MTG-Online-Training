using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTG_OnlineTraining.Models;
using MTG_OnlineTraining.Services;
using MTG_OnlineTraining.ViewModel;

namespace MTG_OnlineTraining.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStudentServices _studentServices;
        private readonly IAdminServices _adminServices;
        private readonly IAccountServices _accountServices;
        public StudentsController(UserManager<ApplicationUser> userManager, IAdminServices adminServices, IStudentServices studentServices, IAccountServices accountServices)
        {
            _userManager = userManager;
            _adminServices = adminServices;
            _studentServices = studentServices;
            _accountServices = accountServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userName = User.Identity.Name;
            if(userName != null)
            {
                var userDetails = _userManager.FindByNameAsync(userName).Result;
                if (userDetails != null)
                {
                    var myProgramList = _adminServices.AllPrograms();

                    var userViewModel = new UserViewModel
                    {
                        ApplicationUser = userDetails,
                        AdminProgramList = myProgramList,
                    };
                    return View(userViewModel);
                }

            }
            return RedirectToAction("login", "Accounts");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var userName = User.Identity.Name;
            if (userName != null)
            {
                var userDetails = _userManager.FindByNameAsync(userName).Result;
                if (userDetails != null)
                {
                    return View(userDetails);
                }
            }
            return RedirectToAction("login", "Accounts");
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            var username = User.Identity.Name;
            if (username != null)
            {
                var userProfile = _accountServices.EditStudentProfile(username);
                if (userProfile != null)
                {
                    return View(userProfile);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel editProfileViewModel)
        {
            var userName = User.Identity.Name;
            if (userName != null)
            {
                var NewUserProfile = _accountServices.UpdateTheEditedProfile(editProfileViewModel);
                if (NewUserProfile.Contains("Successfully"))
                {
                    TempData["Success"] = "Program Updated successfully";
                    return RedirectToAction("Profile");
                }
            }
            TempData["error"] = "Program Update Failed";
            return View(editProfileViewModel);
        }


        [HttpGet]
        public IActionResult StudentProgramIndex()
        {
            ViewBag.ModeOfTraining = _studentServices.GetDropDownEnumsList();
            ViewBag.AllProgram = _adminServices.GetProgramFromTheTable();

            var username = User.Identity.Name;
            var myProgramList = _studentServices.MyRegisteredPrograms(username);
            return View(myProgramList);
        }

        //GET
        [HttpGet]
        public IActionResult StudentProgramCreate()
        {

            ViewBag.ModeOfTraining = _studentServices.GetDropDownEnumsList();
            ViewBag.AllProgram = _adminServices.GetProgramFromTheTable();
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult StudentProgramCreate(StudentProgramsViewModel studentProgramsViewModel)
        {
            ViewBag.ModeOfTraining = _studentServices.GetDropDownEnumsList();
            ViewBag.AllProgram = _adminServices.GetProgramFromTheTable();
            var username = User.Identity.Name;

            if (studentProgramsViewModel != null)
            {
                var newProgramRegistration = _studentServices.StudentProgramRegList(studentProgramsViewModel, username);
                if (newProgramRegistration.Contains("Successfully"))
                    {
                        TempData["Success"] = "Program Registered successfully";
                        return RedirectToAction("StudentProgramIndex");
                    }
            }
            TempData["error"] = "Program Registraion Failed";
            return View();
        }


        [HttpGet]
        public IActionResult StudentProgramEdit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StudentProgramDelete()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StudentMaterial()
        {
            var username = User.Identity.Name;
            var myProgramList = _studentServices.StudentActiveMaterials(username);
            return View(myProgramList);
        }
        
        [HttpGet]
        public IActionResult login()
        {
            return RedirectToAction("login", "Accounts");
        }

    }

}
