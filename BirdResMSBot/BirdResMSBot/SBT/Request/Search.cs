using System;
using System.Collections.Generic;
using System.Text;

namespace BirdResAWSBot.SBT.Request
{
    public class ReturnAvailability
    {
        public string SpecialFare { get; set; }
        public string Status { get; set; }
    }

    public class MiscInfo
    {
        public string RefundableFlightCategory { get; set; }
        public string ResultAgainstDepartureAirportOnly { get; set; }
        public string ResultAgainstArrivalAirportOnly { get; set; }
        public string DirectFlightCategory { get; set; }
        public string Refundable_Fare { get; set; }
    }

    public class DepartureDateTime
    {
        public string WINDOW_PRD { get; set; }
        public string AfterDays { get; set; }
        public string DeptTimeNew { get; set; }
        public string FromDTFix { get; set; }
        public string TimeWindow { get; set; }
        public string FromCityFlag { get; set; }
        public string WindowAfter { get; set; }
        public string ToCityFlag { get; set; }
        public string DT_Fix { get; set; }
        public string FromRadius { get; set; }
        public string ToRadius { get; set; }
        public string DepTime { get; set; }
        public string WindowBefore { get; set; }
        public string ReturnTime { get; set; }
        public string ToDTFix { get; set; }
        public string PreAirLine { get; set; }
        public string BeforeDays { get; set; }
    }

    public class OriginDestinationInformation
    {
        public string OLocationCode { get; set; }
        public string DLocationName { get; set; }
        public string OAirportOnly { get; set; }
        public string OLocationName { get; set; }
        public DepartureDateTime DepartureDateTime { get; set; }
        public string DLocationCode { get; set; }
    }

    public class PassengerTypeQuantity
    {
        public string Code { get; set; }
        public string Quantity { get; set; }
    }

    public class TravelerInfoSummary
    {
        public List<PassengerTypeQuantity> PassengerTypeQuantity { get; set; }
        public string Nationality { get; set; }
    }

    public class Search
    {
        public string OF_ID { get; set; }
        public string IsTryAgain { get; set; }
        public ReturnAvailability ReturnAvailability { get; set; }
        public string IsUpgradePolicy { get; set; }
        public string TicketTypePref { get; set; }
        public string MultiCity { get; set; }
        public string MulitCityType { get; set; }
        public List<MiscInfo> MiscInfo { get; set; }
        public string UniqueID { get; set; }
        public List<OriginDestinationInformation> OriginDestinationInformation { get; set; }
        public string DestinationCountryCode { get; set; }
        public string ReasonID { get; set; }
        public string CabinPref { get; set; }
        public string ReasonName { get; set; }
        public string PositionID { get; set; }
        public TravelerInfoSummary TravelerInfoSummary { get; set; }
        public string TMC_OF_ID { get; set; }
        public string TokenID { get; set; }
    }
}
