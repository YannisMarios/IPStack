﻿namespace IPStack.WebApi.DTOs
{
    public class IPDetailsDTO
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
