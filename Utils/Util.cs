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
                new Claim("name", user.Name + " " + user.LastName),
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
        public static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;

            double dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));

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
    }
}