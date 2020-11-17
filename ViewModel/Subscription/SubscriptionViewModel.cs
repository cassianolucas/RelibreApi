using System;

namespace RelibreApi.ViewModel
{
    public class SubscriptionViewModel : BaseViewModel
    {
        public string Description { get; set; }
        public bool Validate { get; set; }
        public DateTime PaidAt { get; set; }
        public decimal Price { get; set; }
        public int Period { get; set; }
    }
}