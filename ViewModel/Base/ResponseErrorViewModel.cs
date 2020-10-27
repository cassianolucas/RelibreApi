using System.Collections.Generic;
using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class ResponseErrorViewModel : ResponseViewModel
    {
        [JsonProperty(PropertyName = "errors")]
        public List<object> Errors { get; set; }
    }
}