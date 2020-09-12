using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using RelibreApi.Models;

namespace RelibreApi.ViewModel
{
    public class LibraryBookViewModel : BaseViewModel
    {
        [JsonProperty(PropertyName = "contact")]
        [Required(ErrorMessage = "Necessário informar o contato!")]
        public ContactViewModel Contact { get; set; }

        [JsonProperty(PropertyName = "images")]
        public ICollection<ImageViewModel> Images { get; set; }

        [JsonProperty(PropertyName = "reating")]
        public string Reating { get; set; }

        [JsonProperty(PropertyName = "book")]
        [Required(ErrorMessage = "Necessário informar o livro!")]
        public BookViewModel Book { get; set; }

        [JsonProperty(PropertyName = "type")]
        [Required(ErrorMessage = "Necessário informar o tipo!")]
        public TypeViewModel Type { get; set; }

        [JsonProperty(PropertyName = "id_library")]
        public long IdLibrary { get; set; }
    }
}