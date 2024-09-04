using Microsoft.AspNetCore.Mvc;
using TechHub.Models;

namespace TechHub.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                // Handle the form submission 

                TempData["SuccessMessage"] = "Thank you for contacting us! We will get back to you soon.";
                return RedirectToAction("Contact"); // Redirect to the same action to show the success message
            }

            // If the form data is invalid, return the view with validation errors
            return View(contact);
        }
    }
}
