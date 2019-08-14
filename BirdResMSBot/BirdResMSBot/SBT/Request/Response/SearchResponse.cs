using System;
using System.Collections.Generic;
using System.Text;

namespace BirdResAWSBot.SBT.Request.Response
{
    public class SearchResponse
    {
        public FlightSearchResult FlightSearchResult { get; set; }
    }
    public class PassengerTypeQuantity
    {
        public string Code { get; set; }
        public string Quantity { get; set; }
        public PaxFare PaxFares { get; set; }
    }
    public class FlightSearchResult
    {
        public string ResultStatus { get; set; }
        public string ResultDescription { get; set; }
        public string CabinPref { get; set; }
        public string AvailType { get; set; }
        public string WarringMessage { get; set; }
        public REASON REASON { get; set; }
        public List<PricedItinerary> PricedItinerary { get; set; }
        public UpgradeRule UpgradeRule { get; set; }
        public List<CustomFields> lstCustomFields { get; set; }
        public List<PaxDetails> lstPaxDetails { get; set; }
        public List<FL_SEARCHAVAILABILITY_OUTPUT> FL_SEARCHAVAILABILITY_OUTPUT { get; set; }
    }


    public class REASON
    {
        public string REASONID { get; set; }
        public string LOW_FL_REASON { get; set; }
        public string REASON_NAME { get; set; }
    }

    public class CustomFields
    {
        public string EditId { get; set; }
        public string IsEnable { get; set; }
        public string RemarksId { get; set; }
        public string LabelName { get; set; }
        public string IsMandatory { get; set; }
        public string IsReadOnly { get; set; }
        public string IsDisplayOnEmpProfile { get; set; }
        public string IsDisplayonFinish { get; set; }
        public string CategoryType { get; set; }
        public string RecordType { get; set; }
        public string DefaultValue { get; set; }
        public string IsDeleted { get; set; }
        public string OF_ID { get; set; }
        public string OF_Name { get; set; }
        public string DropDownValues { get; set; }
        public string RM_Name { get; set; }
        public string IsDisplayOnEmail { get; set; }
        public string IsDisplayInReport { get; set; }




    }





    public class Flight
    {
        public string DirectionInd { get; set; }
        public string AirlineLogo { get; set; }
        public string AirlineName { get; set; }
        public string AirlineCode { get; set; }
        public string OpertaingAirlineLogo { get; set; }
        public string OpertaingAirlineName { get; set; }
        public string OpertaingAirlineCode { get; set; }
        public FlightMiscInfo MiscInfo { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalDate { get; set; }
        public string ArrivalTime { get; set; }
        public string Duration { get; set; }
        public string StopQuantity { get; set; }
        public string FareType { get; set; }
        public string Cabin { get; set; }
        public string RPH { get; set; }
        public string DepartureLocationCode { get; set; }
        public string DepartureCityName { get; set; }
        public string ArrivalLocationCode { get; set; }
        public string ArrivalCityName { get; set; }
        public string DepartureTerminal { get; set; }
        public string ArrivalTerminal { get; set; }
        public string ResBookDesigCode { get; set; }
        public string TicketType { get; set; }
        public string DepartureAirportName { get; set; }
        public string ArrivalAirportName { get; set; }
        public string AirEquipType { get; set; }
        public string LayoverTime { get; set; }
        public string LayoverHours { get; set; }
        public string Baggage { get; set; }
        public IGSegmentDetail IGSegmentDetail { get; set; }

    }
    public class FlightMiscInfo
    {
        public Equipment Equipment { get; set; }
        public TechnicalStops TechnicalStops { get; set; }
    }
    public class Equipment
    {
        public string Name { get; set; }
        public string Seat { get; set; }
        public string Type { get; set; }
        public string AirEquipType { get; set; }
    }
    public class IGSegmentDetail
    {
        public string FareApplicationType { get; set; }
        public string StopAirport { get; set; }
        public string ProductClass { get; set; }
        public string XrefClassOfService { get; set; }
        public string FareClassOfService { get; set; }
        public string RuleNumber { get; set; }
        public string RuleTariff { get; set; }
        public string ClassType { get; set; }
        public string ClassOfService { get; set; }
        public string FareKey { get; set; }
        public string SegmentKey { get; set; }
        public string JourneyKey { get; set; }
    }
    public class PaxFare
    {
        public string TotalBaseFare { get; set; }
        public string TaxAmount { get; set; }
        public string TaxCurrenyCode { get; set; }
        public string TotalFare { get; set; }
        public string CurrencyCode { get; set; }
        public string FareType { get; set; }
        public string InOutPolicy { get; set; }
        public string MaximumPrice { get; set; }
        public string CorporateCode { get; set; }
        public string FareBasicCode { get; set; }
        public List<Taxes> lstTaxes { get; set; }
        //public FareBasicCodes FareBasicCodes { get; set; }
    }
    public class Taxes
    {
        public string TaxCode { get; set; }
        public string TaxDesc { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Level { get; set; }
        public string ServiceFeeOnEticket { get; set; }
        public string ServiceFeeOnItinerary { get; set; }
        public string AddOnBaseFare { get; set; }


    }
    public class FareBasicCodes
    {
        public string Rebooking { get; set; }
        public string Refundable { get; set; }
        public FareBasicCode FareBasicCode { get; set; }
    }
    public class FareBasicCode
    {
        public string SegRef { get; set; }
        public string BreakPoint { get; set; }
        public string CorporateCode { get; set; }
        public string Code { get; set; }
    }
    public class PaxInfo
    {
        public List<PassengerTypeQuantity> PassengerTypeQuantity { get; set; }
    }
    public class PricedItinerary
    {
        public string SequenceNumber { get; set; }
        public string SourceId { get; set; }
        public string DirectionInd { get; set; }
        public string SectorInd { get; set; }
        public string SelectedPricingSource { get; set; }
        public string DisplayRankId { get; set; }
        public string PreferedAirline { get; set; }
        public string PrefAirline { get; set; }
        public string HighestInpolicyFare { get; set; }
        public string TotalLowestFare { get; set; }
        public List<Flight> Flights { get; set; }
        public PaxFare PaxFares { get; set; }
        public PaxInfo PaxInfos { get; set; }
    }
    public class UpgradeRule
    {
        public string Status { get; set; }
        public string Cabin { get; set; }
        public string Type { get; set; }
        public string PurchaseRestrictionPolicy { get; set; }
    }


    public class FL_SEARCHAVAILABILITY_OUTPUT
    {
        public string UpgradeRule { get; set; }
        public string UpgradeCabin { get; set; }
        public string UpgradeType { get; set; }
        public string PurchaseRestrictionPolicy { get; set; }
        public string SEG_NO { get; set; }
        public List<PricedItinerary> PricedItinerary { get; set; }
    }
    public class PaxDetails
    {
        public string Customer_ID { get; set; }
        public string RPH { get; set; }
        public string Pax_Line_No { get; set; }
        public string Title { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Pax_Type_Code { get; set; }
        public string Mobile { get; set; }
        public string Contact_No { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public Passport Passport { get; set; }
        public string REDRESS_NO { get; set; }
        public string FAMILY_USER_ID { get; set; }
        public List<SeatMealDetails> lstMealDetails { get; set; }


    }

    public class Passport
    {
        public string Number { get; set; }
        public string Place_OF_Issue { get; set; }
        public string Date_OF_Issue { get; set; }
        public string Date_OF_Expiry { get; set; }
        public string Issue_Authority { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
    }


    public class Address
    {
        public string AddressLine { get; set; }
        public string CityName { get; set; }
        public string PostalCode { get; set; }
        public string StateCode { get; set; }
        public string CO_Code { get; set; }
    }

    public class TechnicalStops
    {
        public List<StopDetail> StopDetail { get; set; }
    }

    public class StopDetail
    {
        public string LocationCode { get; set; }
        public string ArrivalDateTime { get; set; }
        public string DepartureDateTime { get; set; }
    }


    public class SeatMealDetails
    {
        public Others Others { get; set; }
        public Meals Meals { get; set; }
        public Seats Seats { get; set; }
        public FFNNO FFNNO { get; set; }
        public string Fax { get; set; }
    }
    public class Others
    {
        public string Code { get; set; }
        public string Desc { get; set; }
        public string SegmentNo { get; set; }
    }
    public class Meals
    {
        public string Code { get; set; }
        public string Amount { get; set; }
        public string Desc { get; set; }
        public string SegmentNo { get; set; }
    }
    public class Seats
    {
        public string Code { get; set; }
        public string Desc { get; set; }
        public string SegmentNo { get; set; }
    }
    public class FFNNO
    {
        public string Number { get; set; }
        public string SegmentNo { get; set; }
    }
}
