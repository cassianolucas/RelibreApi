using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RelibreApi.Models;

namespace RelibreApi.Utils
{
    public class Constants
    {
        public const string Configuration = "Settings"; 
        public const string DefaultContentType = "application/json";
    }
    public class Util
    {        
        public static DateTime CurrentDateTime()
        {
            return DateTime.Now;
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

        public static string CreateToken(IConfiguration configuration, User user)
        {
            var setting = new Setting();
            configuration.GetSection(Constants.Configuration).Bind(setting);

            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
                new Claim("name", user.Person.Name + " " + user.Person.LastName),
                new Claim("profile_id", user.Profile.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var Created = CurrentDateTime();
            var Expires = Created + TimeSpan.FromSeconds(3600);

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Key));

            var SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                        setting.Issuer,
                        setting.Audience,
                        Claims,
                        notBefore: Created,
                        expires: Expires,
                        SigningCredentials);

            var access_token = new JwtSecurityTokenHandler().WriteToken(Token);

            return access_token;
        }
    }
}