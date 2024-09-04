using Microsoft.AspNetCore.Mvc;
using TechHub.Models.Entities;
using TechHub.Models.Repository;

namespace TechHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository _accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult LoginChoice()
        {
            return View();
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = _accountRepository.UserLogin(Email, Password);
                int id = _accountRepository.GetUserIdByEmail(Email);

                if (isRegistered)
                {
                    // Set session variables
                    HttpContext.Session.SetString("UserEmail", Email);
                    HttpContext.Session.SetInt32("UserId", id);

                    return RedirectToAction("UserDashboard", "User", new { id });
                }
                ModelState.AddModelError("", "Login failed. Please try again.");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                bool isAdmin = _accountRepository.AdminLogin(Email, Password);

                if (isAdmin)
                {
                    // Set session variables
                    HttpContext.Session.SetString("AdminEmail", Email);

                    return RedirectToAction("AdminDashboard", "Admin");
                }
                ModelState.AddModelError("", "Login failed. Please try again.");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear the session
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                string adminEmail = HttpContext.Session.GetString("AdminEmail");

                if (string.IsNullOrEmpty(adminEmail))
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }

                bool isChanged = _accountRepository.ChangeAdminPassword(adminEmail, oldPassword, newPassword, confirmPassword);

                if (isChanged)
                {
                    ViewBag.Message = "Password changed successfully.";
                }
                else
                {
                    ModelState.AddModelError("", "Failed to change the password. Please check your old password and try again.");
                }
            }
            return View();
        }

    }
}
