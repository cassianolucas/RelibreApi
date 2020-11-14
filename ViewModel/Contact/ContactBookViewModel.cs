using System;
using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class ContactBookViewModel
    {
        [JsonProperty(PropertyName = "id_contact")]
        public long IdContact { get; set; }

        [JsonProperty(PropertyName = "id_book")]
        public long IdLibraryBook { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "approved")]
        public bool Approved { get; set; }

        [JsonProperty(PropertyName = "denied")]
        public bool Denied { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }

        [JsonProperty(PropertyName = "distance")]
        public double Distance { get; set; }

        [JsonProperty(PropertyName = "book")]
        public BookViewModel Book { get; set; }
    }
}