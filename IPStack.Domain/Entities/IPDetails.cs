﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IPStack.Domain.Entities
{
    public class IPDetails : IIPDetails, IEntity
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
