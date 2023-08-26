using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CPW219_eCommerceSite.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameContext _context;

        public GamesController(GameContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            // 'page' size
            const int NumGamesToDisplayPerPage = 3;
            // 'page' offset to keep from skipping first page
            const int PageOffset = 1;

            int currPage = id ?? 1; // set current page to id if it has a value, else use 1
            /* above is conditional operation for:
             * if (id.HasValue)
             * {
             *  currPage = id.Value;
             * }
             * else
             * {
             *  currPage = 1;
             * }
             */

            // get total number of games in the database
            int totalNumOfGames = await _context.Games.CountAsync();
            // find max number of pages
            double maxNumPages = Math.Ceiling((double)totalNumOfGames / NumGamesToDisplayPerPage);
            // round up to an int
            int lastPage = Convert.ToInt32(maxNumPages);

            // get all games from db
            // method syntax with pagination logic
            List<Game> games = await 
                       _context.Games
                       .Skip(NumGamesToDisplayPerPage * (currPage - PageOffset))
                       .Take(NumGamesToDisplayPerPage)
                       .ToListAsync();


            // query syntax with pagination logic
            /*
             * List<Game> games = (from game in _context.Games
             *                   select game)
             *                   .Skip(NumGamesToDisplayPerPage * (currPage - PageOffset))
             *                   .Take(NumGamesToDisplayPerPage)
             *                   .ToList();
            */

            // create catalog view model
            GameCatalogViewModel catalogModel = new(games, lastPage, currPage);

            // show them on the page
            return View(catalogModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Game newGame)
        {
            if (ModelState.IsValid)
            {
                // add to db
                _context.Games.Add(newGame);        // prepares insert
                await _context.SaveChangesAsync();  // executes pending insert

                // show success message on page
                ViewData["Message"] = $"{newGame.Title} was added successfully!";
                return View();
            }
            return View(newGame);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Game? gameToEdit = await _context.Games.FindAsync(id);

            if (gameToEdit == null)
            {
                return NotFound();
            }

            return View(gameToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Game gameModel)
        {
            if (ModelState.IsValid)
            {
                _context.Games.Update(gameModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{gameModel.Title} was updated successfully!";
                return RedirectToAction("Index");
            }
            return View(gameModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Game? gameToDelete = await _context.Games.FindAsync(id);

            if (gameToDelete == null)
            {
                return NotFound();
            }

            return View(gameToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Game? gameToDelete = await _context.Games.FindAsync(id); // must check db to delete

            if (gameToDelete != null)
            {
                _context.Games.Remove(gameToDelete); // "stage" remove
                await _context.SaveChangesAsync(); // execute delete

                TempData["Message"] = $"{gameToDelete.Title} was deleted successfully!";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "This game was already deleted";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) 
        {
            Game? gameDetails = await _context.Games.FindAsync(id);

            if (gameDetails == null)
            {
                return NotFound();
            }

            return View(gameDetails);
        }
    }
}
