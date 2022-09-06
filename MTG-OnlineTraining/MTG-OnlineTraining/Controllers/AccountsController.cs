using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTG_OnlineTraining.Data;
using MTG_OnlineTraining.Models;
using MTG_OnlineTraining.Services;
using MTG_OnlineTraining.ViewModel;

namespace MTG_OnlineTraining.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(IAccountServices accountServices, UserManager<ApplicationUser> userManager)
        {
            _accountServices = accountServices;
            _userManager = userManager;
        }
        //GET
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (registerViewModel != null)
            {
                if (registerViewModel.Password == registerViewModel.ConfirmPassword & registerViewModel.Email != null)
                {
                    var registerr = _accountServices.UserRegistraion(registerViewModel).Result;

                    if (registerr.Contains("successful"))
                    {
                        TempData["Success"] = "Registraion Submitted successfully";
                        return View("Login");
                    }
                }
            }
            TempData["error"] = "Registraion Failed";
            return View(registerViewModel);
        }

        //GET
        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult AdminRegister(AdminRegistrationViewModel adminRegistrationViewModel)
        {
            if (adminRegistrationViewModel != null)
            {
                if (adminRegistrationViewModel.Password == adminRegistrationViewModel.ConfirmPassword & adminRegistrationViewModel.Email != null)
                {
                    var registerr = _accountServices.AdminRegistraion(adminRegistrationViewModel).Result;

                    if (registerr.Contains("successful"))
                    {
                        TempData["Success"] = "Registraion Submitted successfully";
                        return View("Login");
                    }
                }
            }
            TempData["error"] = "Registraion Failed";
            return View(adminRegistrationViewModel);
        }

        //Login Get
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel) 
        { 
            if(ModelState.IsValid)
            {
                var loginn = _accountServices.UserLogin(loginViewModel).Result;
                if (loginn)
                {
                    var accountType = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
                    if(accountType.IsAdmin == true)
                    {
                        TempData["Success"] = "Logged in successfully";
                        return RedirectToAction("Activated", "Admin");
                    }
                    else
                    {
                        TempData["Success"] = "Logged in successfully";
                        return RedirectToAction("Index", "Students");
                    }
                }
            }
            TempData["error"] = "Login Failed";
            return View(loginViewModel);
        }

        public IActionResult LogOut()
        {
            _accountServices.LogOut();
            return RedirectToAction("Index", "Home");
        }
      
        public IActionResult Activation(string userId)
        {
            var ActiveMe = _accountServices.StudentActivation(userId);
            
            if (ActiveMe.Contains("Successfully"))
            {
                TempData["Success"] = "Activated successfully";
                return RedirectToAction("PendingReg", "Admin");
              
            }
            TempData["error"] = "Activation Failed";
            return RedirectToAction("PendingReg", "Admin");
        }

        public IActionResult DeactivationUser(string userId)
        {
            var DeactiveMe = _accountServices.StudentDeactivation(userId);

            if (DeactiveMe.Contains("Successfully"))
            {
                TempData["Success"] = "Deactivated successfully";
                return RedirectToAction("Activated", "Admin");

            }
            TempData["error"] = "Deactivation Failed";
            return RedirectToAction("Activated", "Admin");
        }
    }
}
