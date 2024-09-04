using Microsoft.AspNetCore.Mvc;
using TechHub.Models.Entities;
using TechHub.Models.Repository;

namespace TechHub.Controllers
{
    public class UserController : Controller
    {
        private readonly AccountRepository _accountRepository;

        // Constructor injection
        public UserController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult UserDashboard(int id)
        {
            
            string username = _accountRepository.GetUsernameByUserId(id);
            List<Project> projects = _accountRepository.GetProjectsByUserId(id);

           

            UserDashboardViewModel viewModel = new UserDashboardViewModel
            {
                UserName = username,
                Projects = projects
            };

            return View(viewModel);

        }
    }
}
