using System;

namespace RelibreApi.Models
{
    public class PersonSubscription : BaseModel
    {        
        public long IdPerson { get; set; }
        public virtual Person Person { get; set; }
        public long IdSubscription { get; set; }
        public virtual Subscription Subscription { get; set; }
        public DateTime PaidAt { get; set; }
        public string Validate { get; set; }
    }
}