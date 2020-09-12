using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class ContactViewModel : BaseViewModel
    {
        [JsonProperty(PropertyName = "email")]
        [Required(ErrorMessage = "Necessário informar o email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phone")]
        [Required(ErrorMessage = "Necessário informar o telefone para contato")]
        public string Phone { get; set; }        
    }
}