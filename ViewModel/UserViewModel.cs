using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class UserViewModel
    {

        [JsonProperty(PropertyName = "name")]
        // [Required(ErrorMessage = "Necessário informar o nome!")]
        // [MinLength(4, ErrorMessage = "Nome inválido!")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        // [Required(ErrorMessage = "Necessário informar o sobrenome!")]
        // [MinLength(4, ErrorMessage = "Sobrenome inválido!")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "login")]
        // [Required(ErrorMessage = "Necessário informar o login!")]
        // [MinLength(10, ErrorMessage = "Login inválido!")]
        public string Login { get; set; }  

        [JsonProperty(PropertyName = "password")] 
        // [Required(ErrorMessage = "Necessário informar uma senha!")]
        // [MinLength(6, ErrorMessage = "Senha deve conter mais que 6 caracteres!")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "document")]
        // [Required(ErrorMessage = "Necessário informar o documento!")]
        // [MinLength(14, ErrorMessage = "Documento inválido")]
        public string Document { get; set; }

        [JsonProperty(PropertyName = "phones")]
        public ICollection<PhoneViewModel> Phones { get; set; }

        [JsonProperty(PropertyName = "address")]
        public ICollection<AddressViewModel> Address { get; set; }
    }
}