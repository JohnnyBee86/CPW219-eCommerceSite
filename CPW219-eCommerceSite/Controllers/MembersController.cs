using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class MembersController : Controller
    {
        private readonly GameContext _context;

        public MembersController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                // Map RegisterViewModel data to a Member object
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                // Save to database
                _context.Add(newMember);
                await _context.SaveChangesAsync();

                // Redirect to homepage
                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }
    }
}
