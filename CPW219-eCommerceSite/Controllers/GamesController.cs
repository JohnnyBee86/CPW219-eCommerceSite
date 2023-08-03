using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameContext _context;

        public GamesController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Game newGame)
        {
            if (ModelState.IsValid)
            {
                // add to db
                _context.Games.Add(newGame); // prepares insert
                _context.SaveChanges(); // executes pending insert

                // show success message on page
                ViewData["Message"] = $"{newGame.Title} was added successfully!";
                return View();
            }
            return View(newGame);
        }
    }
}
