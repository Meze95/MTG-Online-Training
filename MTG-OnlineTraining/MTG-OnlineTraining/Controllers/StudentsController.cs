using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTG_OnlineTraining.Data;
using MTG_OnlineTraining.Models;
using MTG_OnlineTraining.Services;
using MTG_OnlineTraining.ViewModel;
using Newtonsoft.Json;

namespace MTG_OnlineTraining.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStudentServices _studentServices;
        private readonly IAdminServices _adminServices;
        private readonly IAccountServices _accountServices;
        private readonly IDropDownServices _dropDownServices;
        private readonly ApplicationDbConntext _db;
        public StudentsController(UserManager<ApplicationUser> userManager, IAdminServices adminServices, IStudentServices studentServices, IAccountServices accountServices, ApplicationDbConntext db, IDropDownServices dropDownServices)
        {
            _userManager = userManager;
            _adminServices = adminServices;
            _studentServices = studentServices;
            _accountServices = accountServices;
            _db = db;
            _dropDownServices = dropDownServices;
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
            return View();
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
        public IActionResult StudentProgramCreate(string studentProgramsViewModel)
        {
            ViewBag.ModeOfTraining = _studentServices.GetDropDownEnumsList();
            ViewBag.AllProgram = _adminServices.GetProgramFromTheTable();
            var newStudentProgram = JsonConvert.DeserializeObject<StudentProgramsViewModel>(studentProgramsViewModel);
            var username = User.Identity.Name;

            if (studentProgramsViewModel != null)
            {
                var newProgramRegistration = _studentServices.StudentProgramRegList(newStudentProgram, username);
                if (newProgramRegistration.Contains("Successfully"))
                    {
                        TempData["Success"] = "Program Registered successfully";
                        return Json(new { isError = false, msg = "Program Registered successfully" });
                    }
                else if (newProgramRegistration.Contains("already"))
                {
                    return Json(new { isError = true, msg = "Program Alraedy Registered" });
                }
            }
            TempData["error"] = "Program Registraion Failed";
            return Json(new { isError = true, msg = "Program Registraion Failed" });
        }
        

        [HttpGet]
        public JsonResult EditStudentProgramRegistration(Guid Id)
        {
            try
            {
                ViewBag.ModeOfTraining = _studentServices.GetDropDownEnumsList();
                ViewBag.AllProgram = _dropDownServices.GetPrograms().Result;
                if (Id != null)
                {
                    var program = _db.StudentPrograms.Where(p => p.Id == Id).FirstOrDefault();
                    return Json(program);
                }
                else
                {
                    return Json(new { isError = true, msg = "Program Not Found" });
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

        //POST ACTION FOR STUDENT PROGRAM EDIT
        [HttpPost]
        public IActionResult StudentProgramEdit(string studentProgramsViewModel)
        {
            if (studentProgramsViewModel == null)
            {
                return NotFound();
            }
            var programDetails = JsonConvert.DeserializeObject<StudentProgramsViewModel>(studentProgramsViewModel);
            var editedProgram = _studentServices.UpdateEditedStudentProgram(programDetails);
            if (editedProgram.Contains("Started"))
            {
                TempData["error"] = "Program Already Started";
                return Json(new { isError = true, msg = "Program Already Started" });
            }
            else if (editedProgram.Contains("Successfully"))
            {
                TempData["Success"] = "Program Updated successfully";
                return Json(new { isError = false, msg = "Program Updated successfully" });
            }
            else
                TempData["error"] = "Program Update Failed";
                return Json(new { isError = true, msg = "Program Update Failed" });
        }


        [HttpGet]
        public JsonResult DeleteStudentProgramRegistration(Guid Id)
        {
            try
            {
                if (Id != null)
                {
                    var program = _db.StudentPrograms.Where(p => p.Id == Id).FirstOrDefault();
                    return Json(program);
                }
                else
                {
                    return Json(new { isError = true, msg = "Program Not Found" });
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

        //POST ACTION FOR STUDENT PROGRAM DELETE
        [HttpPost]
        public IActionResult StudentProgramDelete(string studentProgramsViewModel)
        {
            if (studentProgramsViewModel == null)
            {
                return NotFound();
            }
            var programDetails = JsonConvert.DeserializeObject<StudentProgramsViewModel>(studentProgramsViewModel);
            var deleteProgram = _studentServices.DeleteSelectedStudentProgram(programDetails);
            if (deleteProgram.Contains("Started"))
            {
                TempData["error"] = "Program Already Started";
                return Json(new { isError = true, msg = "Program Already Started" });
            }
            else if (deleteProgram.Contains("Successfully"))
            {
                TempData["Success"] = "Program Deleted successfully";
                return Json(new { isError = false, msg = "Program Deleted successfully" });
            }
            else
                TempData["error"] = "Program Update Failed";
            return Json(new { isError = true, msg = "Program Deletion Failed" });
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
