using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class LibraryBookViewModel : BaseViewModel
    {   
        [JsonIgnore]
        public ICollection<AddressViewModel> Addresses { get; set; }

        [JsonProperty(PropertyName = "contact")]
        public ContactViewModel Contact { get; set; }

        [JsonProperty(PropertyName = "images")]
        public ICollection<ImageViewModel> Images { get; set; }        

        [JsonProperty(PropertyName = "book")]        
        public BookViewModel Book { get; set; }

        [JsonProperty(PropertyName = "types")]        
        public ICollection<TypeViewModel> Types { get; set; }

        // [JsonProperty(PropertyName = "id_library")]
        // public long IdLibrary { get; set; }        
    }
}