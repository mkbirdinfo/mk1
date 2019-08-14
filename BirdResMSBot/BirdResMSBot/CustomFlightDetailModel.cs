using System;
using System.Collections.Generic;
using System.Text;

namespace BirdResAWSBot
{
  public  class CustomFlightDetailModel
    {
        public string FlightNumber { get; set; }
        public decimal FlightFare { get; set; }
        public string ArrivalAirport { get; set; }
        public string DepAirport { get; set; }
        public string InterMediateStops { get; set; }
        public string ArrivalDate { get; set; }
        public string Baggage { get; set; }
        public string Currency { get; set; }
        public string DepartureDate { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string Duration { get; set; }
        public string ALocCode { get; set; }
        public string OLocCode { get; set; }
        public string AirLineCode { get; set; }
        public string Return { get; set; }
        public string UniqueId { get; set; }
        public string ImageUrl { get; set; }
        public int Group { get; set; }
        public string Direction { get; set; }
        public string AirlineName { get; set; }
        public string Layover { get; set; }
    }
}
