using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
	/// <summary>
	/// The controller to handle shopping cart interactions
	/// </summary>
	public class CartController : Controller
	{
		private readonly GameContext _context;

		public CartController(GameContext context)
		{
			_context = context;
		}

		public IActionResult Add(int id)
		{
			Game? gameToAdd = _context.Games.Where(g => g.GameId == id).SingleOrDefault();

			// does this game id exist in db?
			if (gameToAdd == null)
			{
				TempData["Message"] = "Sorry that game no longer exists";
				return RedirectToAction("Index", "Games");
			}

			// ToDo: add cart cookie
			TempData["Message"] = "Game added to cart";
			return RedirectToAction("Index", "Games");
		}
	}
}
