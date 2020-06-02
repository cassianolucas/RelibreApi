namespace RelibreApi.Models
{
    public class Setting
    {
        public string Key { get; set; }   
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ConnectionString { get; set; }

    }
}