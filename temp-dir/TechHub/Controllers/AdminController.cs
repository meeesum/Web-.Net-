using Microsoft.AspNetCore.Mvc;
using TechHub.Models.Entities;
using TechHub.Models.Repository;

namespace TechHub.Controllers
{
    public class AdminController : Controller
    {

        private readonly AccountRepository _accountRepository;

        public AdminController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult AdminDashboard()
        {
            List<Employee> employees = _accountRepository.GetAllEmployees();
            return View(employees);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _accountRepository.AddEmployee(employee);
                return RedirectToAction("AdminDashboard");
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            Employee employee = _accountRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult EditEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = _accountRepository.UpdateEmployee(employee);
                if (isUpdated)
                {
                    return RedirectToAction("AdminDashboard");
                }
                ModelState.AddModelError("", "Failed to update employee.");
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult DeleteEmployee(int id)
        {
            Employee employee = _accountRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        public IActionResult ConfirmDeleteEmployee(int id)
        {
            bool isDeleted = _accountRepository.DeleteEmployee(id);
            if (isDeleted)
            {
                return RedirectToAction("AdminDashboard");
            }
            ModelState.AddModelError("", "Failed to delete employee.");
            return View();
        }


        public IActionResult Details(int id)
        {
            // Fetch the employee details from the database or repository
            var employee = _accountRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound(); // or redirect to an error page
            }

            return View(employee);
        }
    }
}

        //[HttpPost]
        //public async Task<IActionResult> UploadImage(IFormFile imageFile, int id)
        //{
        //    if (imageFile != null && imageFile.Length > 0)
        //    {
        //        try
        //        {
        //            var employee = _accountRepository.GetEmployeeById(id);
        //            if (employee == null)
        //            {
        //                return Json(new { success = false, message = "Employee not found." });
        //            }

        //            // Create uploads folder if it doesn't exist
        //            if (!Directory.Exists(_uploadsFolderPath))
        //            {
        //                Directory.CreateDirectory(_uploadsFolderPath);
        //            }

        //            var fileName = Path.GetFileName(imageFile.FileName);
        //            var filePath = Path.Combine(_uploadsFolderPath, fileName);

        //            // Save the file to the server
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await imageFile.CopyToAsync(stream);
        //            }

        //            // Update the employee record with the new image path
        //            employee.ImagePath = $"/uploads/{fileName}";
        //            _accountRepository.UpdateEmployee(employee); // Ensure this method updates the employee record in the database

        //            return Json(new { success = true, imagePath = $"/uploads/{fileName}" });
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the exception or handle it as needed
        //            return Json(new { success = false, message = "An error occurred while uploading the image." });
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "No file selected." });
        //    }
    //    }
    //}

//}


