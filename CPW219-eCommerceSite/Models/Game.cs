﻿using System.ComponentModel.DataAnnotations;

namespace CPW219_eCommerceSite.Models
{
    /// <summary>
    /// Represents a single game for purchase
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The unique identifier for each game product
        /// </summary>
        [Key]
        public int GameId { get; set; }

        /// <summary>
        /// The title of the campaign
        /// </summary>
        [Required]
        public string Title { get; set; } = null!;

        /// <summary>
        /// The genre of the setting (fantasy, sci-fi,...)
        /// </summary>
        [Required]
        public string Setting { get; set; } = null!;

        /// <summary>
        /// How many players will be in the game
        /// </summary>
        [Range(1, 10)]
        public int PlayerCount { get; set; }
    }

    /// <summary>
    /// A single game that has been added to a shopping cart cookie
    /// </summary>
    public class CartGameViewModel
    {
        /// <summary>
        /// The unique identifier of the game in the cart
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// The name of the game in the cart
        /// </summary>
        public string Title { get; set; }

		/// <summary>
		/// How many players will be in the game
		/// </summary>
		[Range(1, 10)]
		public int PlayerCount { get; set; }
	}
}
