namespace CPW219_eCommerceSite.Models
{
    /// <summary>
    /// Represents all games in the database prepped for pagination
    /// </summary>
    public class GameCatalogViewModel
    {
        /// <summary>
        /// An object of all games in the database prepped for pagination
        /// </summary>
        /// <param name="games">List of all games</param>
        /// <param name="lastPage">The last page needed to hold all games</param>
        /// <param name="currPage">The page currently being viewed</param>
        public GameCatalogViewModel(List<Game> games, int lastPage, int currPage)
        {
            Games = games;
            LastPage = lastPage;
            CurrentPage = currPage;
        }

        /// <summary>
        /// List of all games in the database
        /// </summary>
        public List<Game> Games { get; private set; }

        /// <summary>
        /// The last page of the catalog.
        /// Calculated by the total number of products
        /// divided by the number of products per page
        /// </summary>
        public int LastPage { get; private set; }

        /// <summary>
        /// The current page the user is viewing
        /// </summary>
        public int CurrentPage { get; private set; }
    }
}
