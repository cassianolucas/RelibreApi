using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RelibreApi.Models;
using static RelibreApi.Utils.Constants;

namespace RelibreApi.Utils
{
    public class Util
    {
        public static DateTime CurrentDateTime()
        {
            return Convert.ToDateTime(
                DateTime.Now
                    .ToString(
                        Constants.FormatDateTimeDefault));
        }
        public static string Encrypt(string valor)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes;

            using (HashAlgorithm hash = SHA256.Create())
                hashBytes = hash.ComputeHash(encoding.GetBytes(valor));

            StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);

            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
        public static string Claim(HttpContext httpContext, string nome)
        {
            if (string.IsNullOrEmpty(nome)) return null;

            if (httpContext == null) return null;

            return httpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(nome)).Value;
        }
        public static string CreateToken(IConfiguration configuration, User user)
        {
            var setting = new Setting();
            configuration.GetSection(Constants.Configuration).Bind(setting);

            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
                new Claim("email_login", user.Login),
                new Claim("name", user.Person.Name + " " + user.Person.LastName),
                new Claim("profile_id", user.Profile.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var Created = CurrentDateTime();
            var Expires = Created + TimeSpan.FromSeconds(3600);

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Key));

            var SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(setting.Issuer, setting.Audience, Claims,
                        notBefore: Created, expires: Expires, SigningCredentials);

            var access_token = new JwtSecurityTokenHandler().WriteToken(Token);

            return access_token;
        }
        public static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;

            double dist = Math.Sin(Deg2Rad(lat1))
                * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1))
                * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));

            dist = Math.Acos(dist);

            dist = Rad2Deg(dist);

            dist = dist * 60 * 1.1515;

            dist = dist * 1.609344;

            return (dist);
        }
        private static double Deg2Rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private static double Rad2Deg(double rad)
        {
            return (rad * 180 / Math.PI);
        }
        public static object ReturnException(Exception ex)
        {
            var environment = Environment
                .GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

            if (environment.ToString().Contains("RelibreDevelopment"))
            {
                return new
                {
                    Message = ex.Message,
                    Error = ex.GetBaseException()
                };
            }

            return new { Message = Constants.MessageExceptionDefault };
        }
        public static string RemoveSpecialCharacter(string valor)
        {
            if (string.IsNullOrEmpty(valor)) return valor;

            var rgx = new Regex(Constants.SpecialCharacter);

            return rgx.Replace(valor, "");
        }
        public static string CreateButtonEmail(string link, string message)
        {
            var strBody = new StringBuilder();
            strBody.AppendLine("<html>");
            strBody.AppendLine("<head>");
            strBody.AppendLine(@"<link rel=""stylesheet"" href=""https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"" 
                integrity='sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z' crossorigin='anonymous'>");
            strBody.AppendLine("</head>");
            strBody.AppendLine("<body>");
            strBody.AppendLine($"<form action='{link}' method='post'>");
            strBody.AppendLine($"<button type='submit' class='btn btn-outline-info'>{message}</button>");
            strBody.AppendLine("</form>");
            strBody.AppendLine("</body>");
            strBody.AppendLine("</html>");

            return strBody.ToString();
        }

        public static void SendEmailAsync(IConfiguration configuration, string email, string message, string endpoint)
        {
            var emailSettings = new EmailSettings(configuration);

            var myMessage = new MailMessage();
            myMessage.IsBodyHtml = true;
            myMessage.From = new MailAddress(emailSettings.Email, "Relibre");
            myMessage.To.Add(new MailAddress(email));
            myMessage.Subject = message;

            myMessage.AlternateViews.Add(
                AlternateView.CreateAlternateViewFromString(
                    message, null, MediaTypeNames.Text.Plain));

            myMessage.AlternateViews.Add(
                AlternateView.CreateAlternateViewFromString(
                    CreateButtonEmail(string.Format(emailSettings.RedirectLink, endpoint), message),
                    null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient(emailSettings.Smtp, emailSettings.Port);
            NetworkCredential credentials = new NetworkCredential(emailSettings.Email, emailSettings.Password);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;

            smtpClient.SendMailAsync(myMessage);
        }

        public static StreamReader HttpRequest(string endpoint, Requests typeRequest)
        {
            if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException();
            
            var request = WebRequest.CreateHttp(endpoint);

            request.Method = Enum.GetName(typeof(Requests), typeRequest);
            request.UserAgent = "RelibreRequest";

            using (var response = request.GetResponse())
            {
                var streamData = response.GetResponseStream();
                var reader = new StreamReader(streamData);

                response.Close();

                return reader;
            }
        }
    }
}