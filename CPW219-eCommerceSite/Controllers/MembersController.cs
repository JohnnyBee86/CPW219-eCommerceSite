using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    /// <summary>
    /// The controller for all 'Members' views
    /// </summary>
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

                // log them in
                LogUserIn(newMember.Email);

                // Redirect to homepage
                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel) 
        {
            if (ModelState.IsValid)
            {
                // check db for credentials
                Member? m = (from member in  _context.Members
                           where member.Email == loginModel.Email &&
                                member.Password == loginModel.Password
                           select member).SingleOrDefault();

                // if exists send to homepage
                if (m != null)
				{
					LogUserIn(loginModel.Email);
					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError(string.Empty, "Credentials not found!");
            }
            // if no record matches or model state is invalid, display error
            return View(loginModel);
        }

		private void LogUserIn(string email)
		{
			HttpContext.Session.SetString("Email", email);
		}

		public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
