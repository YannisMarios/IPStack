namespace IPStack.Domain.Entities
{
    public interface IIPDetails
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
