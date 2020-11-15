using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
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
            return DateTime.Now;
            // return Convert.ToDateTime(
            //     DateTime.Now
            //         .ToString(
            //             Constants.FormatDateTimeDefault));
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
        public static string GetClaim(HttpContext httpContext, string value)
        {
            if (string.IsNullOrEmpty(value)) return null;

            if (httpContext == null) return null;

            return httpContext.User.Claims
                .SingleOrDefault(x => x.Type.Equals(value)).Value;
        }
        public static string CreateToken(IConfiguration configuration, User user)
        {
            var setting = new Setting();
            configuration.GetSection(Constants.Configuration).Bind(setting);

            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
                new Claim(Constants.UserClaimIdentifier, user.Login),
                new Claim("name", user.Person.Name + " " + user.Person.LastName),
                new Claim("profile_id", user.Profile.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var Created = CurrentDateTime();
            var Expires = Created.AddHours(3);

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Key));

            var SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(setting.Issuer, setting.Audience, Claims,
                        notBefore: Created, expires: Expires, SigningCredentials);

            var access_token = new JwtSecurityTokenHandler().WriteToken(Token);

            return access_token;
        }
        public static decimal Distance(double lat1, double lon1, double lat2, double lon2)
        {
            try
            {
                double theta = lon1 - lon2;

                double dist = Math.Sin(Deg2Rad(lat1))
                    * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1))
                    * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));

                dist = Math.Acos(dist);

                dist = Rad2Deg(dist);

                dist = dist * 60 * 1.1515;

                dist = dist * 1.609344;

                // var mt = (dist / 1000);

                return Decimal.Parse(dist.ToString("F"));
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static decimal Distance(Address address1, Address address2)
        {
            if (address1 != null && address2 != null)
            {
                return Distance(Double.Parse(address1.Latitude),
                    Double.Parse(address1.Longitude),
                    Double.Parse(address2.Latitude),
                    Double.Parse(address2.Longitude));
            }
            return 0;
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
        public static string LoadHtmlBase(IConfiguration configuration, string name, string codeVerification, HtmlEmailType type)
        {
            var settings = new HtmlSettings();

            var emailSettings = new EmailSettings(configuration);

            configuration
                .GetSection(Constants.HtmlSettings).Bind(settings);
            
            string htmlFile = Path.GetFullPath(settings.IdetifierHtmlFile);

            // não localizou o arquivo
            if (!File.Exists(htmlFile))
                throw new ArgumentNullException("Arquivo base " + Constants.NotFound);

            var arquivo = new FileStream(htmlFile, FileMode.Open);

            HtmlDocument doc = new HtmlDocument();

            // abrir arquivo
            doc.Load(arquivo);

            // capturara elemento nome
            var elemento = doc.GetElementbyId(settings.IdentifierName);

            // alterar nome a ser apresentado
            ReplaceTextHtml(elemento, string.Format(Constants.HtmlEmailName, name));

            // capturar elemento descricao
            elemento = doc.GetElementbyId(settings.IdentifierDescription);

            // alterar descrição
            ReplaceTextHtml(elemento, (type == HtmlEmailType.ForgetPassword ?
                Constants.HtmlEmailDescriptionPassword :
                Constants.HtmlEmailDescriptionAccount));

            // capturar elemento informação
            elemento = doc.GetElementbyId(settings.IdentifierInformation);

            // alterar informação
            ReplaceTextHtml(elemento, (type == HtmlEmailType.ForgetPassword ?
                Constants.HtmlEmailInformationPassword :
                Constants.HtmlEmailInformationAccount));

            // capturar elemento do botão
            elemento = doc.GetElementbyId(settings.IdentifierButton);

            ReplaceTextHtml(elemento, (type == HtmlEmailType.ForgetPassword ?
                Constants.HtmlEmailButtonTextPassword :
                Constants.HtmlEmailButtonTextAccount));            

            // alterar valor de redirecionamento
            elemento.SetAttributeValue("href", string.Format((type == HtmlEmailType.ForgetPassword ? 
                settings.RedirectLinkPassword: settings.RedirectLinkAccount), codeVerification));
            
            elemento.SetAttributeValue("data-saferedirecturl", string.Format((type == HtmlEmailType.ForgetPassword ? 
                settings.RedirectLinkPassword: settings.RedirectLinkAccount), codeVerification));

            var result = doc.DocumentNode.OuterHtml;

            arquivo.Close();

            return result;            
        }
        private static void ReplaceTextHtml(HtmlNode elemento, string newText)
        {
            var textNode = (HtmlTextNode)elemento.SelectSingleNode("text()");

            if (textNode != null)
            {
                textNode.Text = newText;
            }
        }
        public static void SendEmailAsync(IConfiguration configuration, string codeVerification, string email, string name, HtmlEmailType type)
        {            
            var htmlEmail = LoadHtmlBase(configuration, name, codeVerification, type);
            
            var emailSend = new EmailSend
            {
                EmailTo = email,
                Message = "",
                Subject = (type == HtmlEmailType.ForgetPassword? 
                    "Redefinição de senha": "Confirmação de conta"),
                Body = htmlEmail
            };

            var emailSerialize = JsonConvert.SerializeObject(emailSend);

            var data = new StringContent(emailSerialize, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => { return true; };

            var endPoint = "https://ec2-18-229-164-33.sa-east-1.compute.amazonaws.com/api/v1/email";

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);

            client.PostAsync(endPoint, data);
        }
        private static HttpClient ClientRequest()
        {
            HttpClientHandler clientHandler =
                new HttpClientHandler();

            clientHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            return client;
        }
        public async static Task<string> GetAddressByLatitudeLogintude(
                IConfiguration configuration, string latitude, string longitude)
        {
            var client = ClientRequest();

            var endPoint = string.Format(configuration.GetValue<string>(
                             Constants.GeolocationApi), latitude, longitude);

            var result = await client.GetAsync(endPoint);

            var customerJsonString = await result.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject(customerJsonString);

            using (JsonDocument doc = JsonDocument.Parse(obj.ToString()))
            {
                JsonElement root = doc.RootElement;
                var results = root.GetProperty("results");
                var formatted = results[0].GetProperty("formatted");
                return formatted.ToString();
            }
        }
        public async static Task<bool> UploadImage(IConfiguration configuration, IFormFile file, string name)
        {
            var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            var key = configuration.GetSection(Constants.AccessKeyS3).Value;

            var secretKey = configuration.GetSection(Constants.SecretKeyS3).Value;

            IAmazonS3 clietn = new AmazonS3Client(key, secretKey, RegionEndpoint.SAEast1);

            var bucketName = configuration.GetSection(Constants.BucketNameS3).Value;

            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = string.Concat(name, ".png"),
                InputStream = memoryStream,
                ContentType = "image/png"
            };

            var response = await clietn.PutObjectAsync(putRequest);

            return (response.HttpStatusCode == HttpStatusCode.OK);
        }
    }
}