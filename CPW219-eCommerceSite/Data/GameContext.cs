using CPW219_eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace CPW219_eCommerceSite.Data
{
    /// <summary>
    /// The context for the Games database
    /// </summary>
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options) 
        {
            
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Member> Members { get; set; }
    }
}
