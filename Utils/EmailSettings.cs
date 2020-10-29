using Microsoft.Extensions.Configuration;

namespace RelibreApi.Utils
{
    public class EmailSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RedirectLink { get; set; }
        public string Smtp { get; set; }
        public int Port { get; set; }

        public EmailSettings(IConfiguration configuration)
        {
            configuration.GetSection(
                Constants.EmailSettings).Bind(this);
        }
    }
}