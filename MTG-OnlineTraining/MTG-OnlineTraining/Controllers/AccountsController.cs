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
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(IAccountServices accountServices, UserManager<ApplicationUser> userManager, IEmailConfiguration emailConfiguration)
        {
            _accountServices = accountServices;
            _userManager = userManager;
            _emailConfiguration = emailConfiguration;
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
            string linkToClick = HttpContext.Request.Scheme.ToString() + "://" +
                                 HttpContext.Request.Host.ToString() + "/Accounts/EmailActivation";

            var ussss = _emailConfiguration.SmtpUsername;

            var registerr = _accountServices.UserRegistraion(registerViewModel, linkToClick).Result;

            if (registerr.Contains("passport"))
            {
                TempData["error"] = "Upload Passport";
            }
            else if (registerr.Contains("confirmation"))
            {
                TempData["error"] = "Password and it confirmation doesnot match";
            }
            else if (registerr.Contains("required"))
            {
                TempData["error"] = "Fill all the required fields";
            }
            else if (registerr.Contains("PhoneNumber"))
            {
                TempData["error"] = "Phone already Exist";
            }
            else if (registerr.Contains("Email"))
            {
                TempData["error"] = "Email already Exist";
            }
            else
            {
                TempData["Success"] = "Registraion Submitted successfully";
                return View("Login");
            }
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

        public IActionResult EmailActivation(string userId)
        {
            var ActiveMe = _accountServices.StudentActivation(userId);

            if (ActiveMe.Contains("Successfully"))
            {
                TempData["Success"] = "Activated successfully";
                return View("Login");

            }
            TempData["error"] = "Activation Failed";
            return View("Register");
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
