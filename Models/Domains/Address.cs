
namespace RelibreApi.Models
{
    public class Address : BaseModelUpdatedAt
    {        
        public long IdPerson { get; set; }
        public string NickName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FullAddress { get; set; }
        public bool Master { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Complement { get; set; }
        public virtual Person Person { get; set; }
    }
}