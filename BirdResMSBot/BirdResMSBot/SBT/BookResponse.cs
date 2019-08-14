using System;
using System.Collections.Generic;
using System.Text;

namespace BirdResAWSBot.SBT
{
    

    public class BookingReferenceID
    {
        public string Type { get; set; }
        public string ID { get; set; }
    }

    public class DepartureAirport
    {
        public string LocationCode { get; set; }
        public string Terminal { get; set; }
        public string AirportName { get; set; }
        public string CityName { get; set; }
    }

    public class ArrivalAirport
    {
        public string LocationCode { get; set; }
        public string Terminal { get; set; }
        public string AirportName { get; set; }
        public string CityName { get; set; }
    }

    public class Flight
    {
        public string DirectionInd { get; set; }
        public string AirlineLogo { get; set; }
        public string AirlineName { get; set; }
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalDate { get; set; }
        public string ArrivalTime { get; set; }
        public string Duration { get; set; }
        public string StopQuantity { get; set; }
        public string FareType { get; set; }
        public string RPH { get; set; }
        public string DepartureLocationCode { get; set; }
        public string DepartureCityName { get; set; }
        public string ArrivalLocationCode { get; set; }
        public string ArrivalCityName { get; set; }
        public string TicketType { get; set; }
        public BookingReferenceID BookingReferenceID { get; set; }
        public DepartureAirport DepartureAirport { get; set; }
        public ArrivalAirport ArrivalAirport { get; set; }
    }

    public class PaxFares
    {
        public string TotalBaseFare { get; set; }
        public string TaxAmount { get; set; }
        public string TaxCurrenyCode { get; set; }
        public string TotalFare { get; set; }
        public string CurrencyCode { get; set; }
        public object FareType { get; set; }
    }

    public class LstTax
    {
        public string TaxCode { get; set; }
        public string TaxDesc { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public object Level { get; set; }
        public object ServiceFeeOnEticket { get; set; }
        public object ServiceFeeOnItinerary { get; set; }
        public object AddOnBaseFare { get; set; }
    }

    public class PaxFares2
    {
        public string TotalBaseFare { get; set; }
        public string TaxAmount { get; set; }
        public object TaxCurrenyCode { get; set; }
        public string TotalFare { get; set; }
        public object CurrencyCode { get; set; }
        public string FareType { get; set; }
        public object InOutPolicy { get; set; }
        public object MaximumPrice { get; set; }
        public object CorporateCode { get; set; }
        public string FareBasicCode { get; set; }
        public List<LstTax> lstTaxes { get; set; }
    }

    public class PassengerTypeQuantity
    {
        public string Code { get; set; }
        public string Quantity { get; set; }
        public string BaseFareAmount { get; set; }
        public string TaxAmount { get; set; }
        public string TransactionAmount { get; set; }
        public string TotalAmount { get; set; }
        public PaxFares2 PaxFares { get; set; }
    }

    public class PaxInfos
    {
        public List<PassengerTypeQuantity> PassengerTypeQuantity { get; set; }
    }

    public class PricedItinerary
    {
        public string SequenceNumber { get; set; }
        public string SourceId { get; set; }
        public string DirectionInd { get; set; }
        public string SectorInd { get; set; }
        public List<Flight> Flights { get; set; }
        public PaxFares PaxFares { get; set; }
        public PaxInfos PaxInfos { get; set; }
        public BookingReferenceID BookingReferenceID { get; set; }
    }

    public class PaxRetreive
    {
        public string RPH { get; set; }
        public string Title { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
    }

    public class BookingReference
    {
        public string Brn { get; set; }
        public string Status_Code { get; set; }
        public string Status_Name { get; set; }
        public string Created_Date { get; set; }
    }

    public class BookResponse
    {
        public string ResultStatus { get; set; }
        public string ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public List<PricedItinerary> PricedItinerary { get; set; }
        public List<PaxRetreive> PaxRetreive { get; set; }
        public BookingReference BookingReference { get; set; }
        public string TR_RQ_ID { get; set; }
    }
}
