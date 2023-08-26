using System.ComponentModel.DataAnnotations;

namespace CPW219_eCommerceSite.Models
{
    /// <summary>
    /// Represents a single member
    /// </summary>
    public class Member
    {
        /// <summary>
        /// The members unique id
        /// </summary>
        [Key]
        public int MemberId { get; set; }

        /// <summary>
        /// The members email
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// The members password
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// The members preferred phone number
        /// </summary>
        public string? Phone { get; set; } // ? makes them optional in the DB

        /// <summary>
        /// The members user-name
        /// </summary>
        public string? Username { get; set; } // ? makes them optional in the DB
    }

    /// <summary>
    /// View Model for registering a new member
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// The new members email
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

		/// <summary>
		/// The new members email entered again to confirm
		/// </summary>
		[Required]
        [Compare(nameof(Email))]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; } = null!;

        /// <summary>
        /// The new members password
        /// </summary>
        [Required]
        [StringLength(75, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

		/// <summary>
		/// The new members password entered again to confirm
		/// </summary>
		[Required]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }

    /// <summary>
    /// View Model for a member to log in
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// The members email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// The members password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
