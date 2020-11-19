using System;
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

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "distance")]
        public decimal Distance { get; set; }
        
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
    }
}