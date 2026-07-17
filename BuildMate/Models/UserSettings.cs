namespace Buildmate.Models
{
    public class UserSettings
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string LanguagePreference { get; set; } = "English";
        public string TimeZone { get; set; } = "GMT+8 (Manila)";

        public bool TwoFactorEnabled { get; set; } = true;
        public string AuthenticatorApp { get; set; } = "Google Authenticator";
        public int UnusedBackupCodes { get; set; } = 5;
    }
}