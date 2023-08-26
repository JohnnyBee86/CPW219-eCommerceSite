namespace CPW219_eCommerceSite.Models
{
    /// <summary>
    /// ViewModel for errors logging in
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}