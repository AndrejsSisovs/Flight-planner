﻿using System.Text.Json.Serialization;

namespace FlightPlanner.Core.Models
{
    public class UserSearchFlights : Entity
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? DepartureDate { get; set; }
    }
}
