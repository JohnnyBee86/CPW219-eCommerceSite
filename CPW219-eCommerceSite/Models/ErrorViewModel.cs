namespace CPW219_eCommerceSite.Models
{
    /// <summary>
    /// ViewModel for errors logging in
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// The requested Id
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// The error message to be displayed
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}