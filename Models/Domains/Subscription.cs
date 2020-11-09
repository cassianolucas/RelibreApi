using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Subscription : BaseModel
    {
        public string Description { get; set; }
        public double Value { get; set; }
        public int Period { get; set; }
        public virtual ICollection<PersonSubscription> PersonSubscriptions { get; set; }
    }
}