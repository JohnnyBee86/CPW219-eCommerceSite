using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CPW219_eCommerceSite.Controllers
{
	/// <summary>
	/// The controller to handle shopping cart interactions
	/// </summary>
	public class CartController : Controller
	{
		private readonly GameContext _context;
		private const string Cart = "ShoppingCart";

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
                // if null, generate message and go back to catalog
                TempData["Message"] = "Sorry that game no longer exists";
                return RedirectToAction("Index", "Games");
            }

            // create object to add to cart (Mapping)
            CartGameViewModel cartGame = new()
            {
                GameId = gameToAdd.GameId,
                Title = gameToAdd.Title,
                PlayerCount = gameToAdd.PlayerCount
            };

            List<CartGameViewModel> cartGames = GetExistingCartData();
            cartGames.Add(cartGame);
            /* 
			 * If we are tracking quantity, check to see if item is already in the cookie.
			 * If it does it can just be incremented
			 */

            WriteShoppingCartCookie(cartGames);

            // ToDo: add cart cookie
            TempData["Message"] = "Game added to cart";
            return RedirectToAction("Index", "Games");
        }

        /// <summary>
        /// Writes a shopping cart list into a cookie
        /// </summary>
        /// <param name="cartGames">The list to be cookied</param>
        private void WriteShoppingCartCookie(List<CartGameViewModel> cartGames)
        {
            string cookieData = JsonConvert.SerializeObject(cartGames);

            // Serialization JSON (add cookie)
            HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });
        }

        /// <summary>
        /// Returns the current list of games in the user's shopping cart cookie.
        /// If no cookie exists it returns an empty list.
        /// </summary>
        /// <returns>A list of game data to be used in cookies</returns>
        private List<CartGameViewModel> GetExistingCartData()
		{
			string? cookie = HttpContext.Request.Cookies[Cart];

			// no cookie found
			if (string.IsNullOrWhiteSpace(cookie))
			{
				return new List<CartGameViewModel>();
			}

			return JsonConvert.DeserializeObject<List<CartGameViewModel>>(cookie);
		}

		public IActionResult Summary()
		{
			// read cart data and convert to list of view model
			List<CartGameViewModel> cartGames = GetExistingCartData();
			return View(cartGames);
		}

		public IActionResult Remove(int id)
		{
            List<CartGameViewModel> cartGames = GetExistingCartData();

			// find the game to be removed from the list
			CartGameViewModel? gameToRemove = cartGames.FirstOrDefault(g => g.GameId == id);

			if (gameToRemove != null)
            {
                cartGames.Remove(gameToRemove);
            }

            WriteShoppingCartCookie(cartGames);
            return RedirectToAction("Summary");
        }
	}
}
