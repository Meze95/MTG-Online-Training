using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTG_OnlineTraining.Data;
using MTG_OnlineTraining.Models;
using MTG_OnlineTraining.Services;
using MTG_OnlineTraining.ViewModel;
using Newtonsoft.Json;

namespace MTG_OnlineTraining.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbConntext _db;
        private readonly IAdminServices _adminServices;
        private readonly IAccountServices _accountServices;
        private readonly IStudentServices _studentServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDropDownServices _dropDownServices;

        public AdminController(ApplicationDbConntext db, IAdminServices adminServices, UserManager<ApplicationUser> userManager, IAccountServices accountServices, IStudentServices studentServices, IDropDownServices dropDownServices)
        {
            _db = db;
            _adminServices = adminServices;
            _userManager = userManager;
            _accountServices = accountServices;
            _studentServices = studentServices;
            _dropDownServices = dropDownServices;
        }

        //GET
        [HttpGet]
        public IActionResult Activated()
        {
            var allStudents = _accountServices.GetApprovedStudentsFromTheTable();
            return View(allStudents);
        }

        //GET
        [HttpGet]
        public IActionResult PendingReg()
        {
            var pendingStudents = _accountServices.GetAllPendingStudentsRegFromTheTable();
            return View(pendingStudents);
        }

        //GET
        [HttpGet]
        public IActionResult AdminProgramIndex()
        {
            var programList = _adminServices.GetProgramFromTheTable();
            return View(programList);
        }

        //GET
        [HttpGet]
        public IActionResult AdminProgramCreate()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminProgramCreate(AdminProgramViewModel adminProgramViewModel)
        {
            if (adminProgramViewModel != null)
            {
                var registerr = _adminServices.ProgramCreate(adminProgramViewModel);

                if (registerr.Contains("Successfully"))
                {
                    TempData["Success"] = "Program Created successfully";
                    return RedirectToAction("AdminProgramIndex", "Admin");
                }
            }
            TempData["error"] = "Program Creation Failed";
            return View("adminProgramViewModel");
        }

        //GET ACTION FOR PROGRAM EDIT
        [HttpGet]
        public IActionResult AdminProgramEdit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            var programToEdit = _adminServices.EditandDeleteProgramView(Id);
            if(programToEdit == null)
            {
                return NotFound();
            }
            return View(programToEdit);
        }

        //POST ACTION FOR PROGRAM EDIT
        [HttpPost]
        public IActionResult AdminProgramEdit(AdminProgramViewModel adminProgramViewModel, int? Id)
        {
            if(adminProgramViewModel == null)
            {
                return NotFound();
            }
            var programToEdit = _adminServices.UpdateTheEditedProgram(adminProgramViewModel, Id);
            if (programToEdit.Contains("Upload"))
            {
                TempData["error"] = "Upload Image";
                return View(adminProgramViewModel);
            }
            else if (programToEdit.Contains("Successfully"))
            {
                TempData["Success"] = "Program Updated successfully";
                return RedirectToAction("AdminProgramIndex");
            }
            else
            TempData["error"] = "Program Update Failed";
            return View(adminProgramViewModel);
        }

        //Get ACTION FOR PROGRAM DELETE
        [HttpGet]
        public IActionResult AdminProgramDelete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var programToDelete = _adminServices.EditandDeleteProgramView(Id);
            if (programToDelete == null)
            {
                return NotFound();
            }
            return View(programToDelete);
        }

        //POST ACTION FOR PROGRAM DELETE
        [HttpPost]
        public IActionResult AdminProgramDelete(AdminProgramViewModel adminProgramViewModel, int? Id)
        {
            if (adminProgramViewModel == null)
            {
                return NotFound();
            }
            var programToDelete = _adminServices.DeleteSelectedProgram(adminProgramViewModel, Id);
            if (programToDelete.Contains("Successfully"))
            {
                TempData["Success"] = "Program Deleted successfully";
                return RedirectToAction("AdminProgramIndex");
            }
            TempData["error"] = "Program Deletion Failed";
            return View(adminProgramViewModel);
        }

        //GET
        [HttpGet]
        public IActionResult AdminMaterialIndex()
        {
            ViewBag.AllProgram = _adminServices.GetProgramFromTheTable();
            var materailList = _adminServices.GetMaterialsFromTheTable();
            return View(materailList);
        }

        //GET
        [HttpGet]
        public IActionResult AdminMaterialCreate()
        {
            ViewBag.AllProgram = _adminServices.GetProgramFromTheTable();
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult AdminMaterialCreate(MaterialsViewModel materialsViewModel)
        {
            if (materialsViewModel != null)
            {
                var materiall = _adminServices.MaterialCreate(materialsViewModel);
                if (materiall.Contains("Successfully"))
                {
                    TempData["Success"] = "Material Uploaded successfully";
                    return RedirectToAction("AdminMaterialIndex");
                }
            }
            return View();
        }


        public IActionResult AdminMaterialEdit()
        {
            ViewBag.AllProgram = _adminServices.GetProgramFromTheTable();
            return View();
        }

        [HttpGet]
        public JsonResult EditMaterialView(int Id)
        {
            try
            {
                ViewBag.AllProgram = _dropDownServices.GetPrograms().Result;
                if (Id != null)
                {
                    var material = _db.Materials.Where(m => m.Id == Id).FirstOrDefault();
                    return Json(material);
                }
                else
                {
                    return Json(new { isError = true, msg = "Material Not Found" });
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

        //POST ACTION FOR STUDENT PROGRAM EDIT
        [HttpPost]
        public IActionResult EditMaterialPost(string materialsViewModel, string base64)
        {
            if (materialsViewModel == null)
            {
                return NotFound();
            }
            var materailDetails = JsonConvert.DeserializeObject<MaterialsViewModel>(materialsViewModel);
            var editedMaterail = _adminServices.UpdateEditedMaterial(materailDetails, base64);
            if (editedMaterail.Contains("Document"))
            {
                return Json(new { isError = true, msg = "Training File Cannot be Empty, please Upload" });
            }
            else if (editedMaterail.Contains("Successfully"))
            {
                return Json(new { isError = false, msg = "Material Updated successfully" });
            }
            else
            {
                return Json(new { isError = true, msg = "Material Update Failed" });
            }
        }



        public IActionResult AdminMaterialDelete()
        {
            ViewBag.AllProgram = _adminServices.GetProgramFromTheTable();
            return View();
        }

        //GET
        [HttpGet]
        public IActionResult ProgramRegList()
        {
            var username = User.Identity.Name;
            var myProgramList = _studentServices.AllProgramReg();
            return View(myProgramList);
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        public IActionResult ProgramActivation(Guid Id)
        {
            var ActiveMe = _adminServices.ProgramActivation(Id);

            if (ActiveMe != null)
            {
                TempData["Success"] = "Activated successfully";
                return RedirectToAction("ProgramRegList");

            }
            TempData["error"] = "Activation Failed";
            return RedirectToAction("ProgramRegList");
        }

        public IActionResult ProgramDeactivation(Guid Id)
        {
            var DeactiveMe = _adminServices.ProgramDeactivation(Id);

            if (DeactiveMe != null)
            {
                TempData["Success"] = "Deactivated successfully";
                return RedirectToAction("ProgramRegList");

            }
            TempData["error"] = "Deactivated Failed";
            return RedirectToAction("ProgramRegList");
        }

    }
}
