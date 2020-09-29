using System;
using Newtonsoft.Json;

namespace RelibreApi.Models
{
    public class EmailVerification : BaseModelSimple
    {        
        public string Login { get; set; }
        public string CodeVerification { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}