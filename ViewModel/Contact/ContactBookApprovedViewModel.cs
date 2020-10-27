using System;
using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class ContactBookApprovedViewModel : ContactBookViewModel
    {
        
        [JsonProperty(PropertyName = "approve")]
        public bool Available { get; set; }        

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
    }
}