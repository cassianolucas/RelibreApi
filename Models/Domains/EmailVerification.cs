using System;
using Newtonsoft.Json;

namespace RelibreApi.Models
{
    public class EmailVerification : BaseModel
    {        
        public string Login { get; set; }
        public string CodeVerification { get; set; }
    }
}