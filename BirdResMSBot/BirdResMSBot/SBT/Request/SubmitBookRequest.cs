using System;
using System.Collections.Generic;
using System.Text;

namespace BirdResAWSBot.SBT.Request
{
 public   class SubmitBookResponse
    {
        
            public string ResultStatus { get; set; }
            public string ResultDescription { get; set; }
            public string TripID { get; set; }
            public string CorporateID { get; set; }
     
    }
    public class OFFLINEFLDETAILS
    {
        public string FL_TO_NAME { get; set; }
        public string APT_TO_NAME { get; set; }
        public string TR_RQ_ID { get; set; }
        public string FL_TO { get; set; }
        public string CA_CODE { get; set; }
        public string APT_FROM_NAME { get; set; }
        public string PAY_MODE_DESC { get; set; }
        public string CA_NAME { get; set; }
        public string DEP_TIME { get; set; }
        public string TR_TYPE { get; set; }
        public string ARR_TIME { get; set; }
        public string TR_RETURN { get; set; }
        public string FL_FROM_NAME { get; set; }
        public string REMARKS { get; set; }
        public string DEP_DATE { get; set; }
        public string ARR_DATE { get; set; }
        public string FL_FROM { get; set; }
        public string AMOUNT { get; set; }
        public string PAY_MODE { get; set; }
        public string CU_CODE { get; set; }
    }

    public class EXPENSEDETAIL
    {
        public string PAYMODE { get; set; }
        public string PAYMODEDESC { get; set; }
        public string CUCODE { get; set; }
        public string DAYS { get; set; }
        public string SNo { get; set; }
        public string DESCRIPTION { get; set; }
        public string REGIONNAME { get; set; }
        public string REGIONID { get; set; }
        public string STATUS { get; set; }
        public string EXPENSEID { get; set; }
        public string AMOUNT { get; set; }
        public string AMOUNTPERDAY { get; set; }
        public string EXPENSENAME { get; set; }
    }

    public class OnlineHotelDetail
    {
    }

    public class SequenceNumber
    {
        public string SequenceNumber1 { get; set; }
        public string SequenceNumber2 { get; set; }
    }

    public class OFFLINERAILDETAILS
    {
        public string RL_FROM_NAME { get; set; }
        public string RL_FROM { get; set; }
        public string REMARKS { get; set; }
        public string ARR_TIME { get; set; }
        public string RAIL_CA_CODE { get; set; }
        public string DEP_DATE { get; set; }
        public string RL_TO_NAME { get; set; }
        public string PAY_MODE_DESC { get; set; }
        public string RAIL_CA_NAME { get; set; }
        public string TR_RETURN { get; set; }
        public string ARR_DATE { get; set; }
        public string CU_CODE { get; set; }
        public string AMOUNT { get; set; }
        public string DEP_TIME { get; set; }
        public string RL_TO { get; set; }
        public string PAY_MODE { get; set; }
    }

    public class INSURED
    {
        public string FIRSTNAME { get; set; }
        public string COUNTRY { get; set; }
        public string EMAILADDRESS { get; set; }
        public string SERVICETAX { get; set; }
        public string TITLE { get; set; }
        public string TOTALCHARGES { get; set; }
        public string PASSPORTNUMBER { get; set; }
        public string TOTALBASECHARGES { get; set; }
        public string VIEWDOB { get; set; }
        public string PREMIUM { get; set; }
        public string MOBILE { get; set; }
        public string NOMINEE { get; set; }
        public string RPH { get; set; }
        public string PINCODE { get; set; }
        public string AGE { get; set; }
        public string ADDRESS1 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string LASTNAME { get; set; }
        public string DISTRICT { get; set; }
        public string PHONENO { get; set; }
        public string RELATION { get; set; }
        public string CURRCODE { get; set; }
        public string ADDRESS2 { get; set; }
        public string USERDOB { get; set; }
        public string BASECHARGES { get; set; }
    }

    public class INSURANCEPLAN
    {
        public string PLANNAME { get; set; }
        public string DAYS { get; set; }
        public string DEPDATE { get; set; }
        public string PLANCODE { get; set; }
        public string CATEGORYCODE { get; set; }
        public string COUNTRYCODE { get; set; }
        public string COUNTRYNAME { get; set; }
        public string OFID { get; set; }
        public string CATEGORYNAME { get; set; }
        public string ARRDATE { get; set; }
    }

    public class INSURANCE
    {
        public List<INSURED> INSURED { get; set; }
        public INSURANCEPLAN INSURANCEPLAN { get; set; }
    }

    public class VISADETAILS
    {
        public string TRRQID { get; set; }
        public string TRVISA { get; set; }
        public string PAYMODEDESC { get; set; }
        public string VSCUCODE { get; set; }
        public string ISCUCODE { get; set; }
        public string VSAMOUNT { get; set; }
        public string ISAMOUNT { get; set; }
        public string REMARKS { get; set; }
        public string TRINSURANCE { get; set; }
        public string PAYMODE { get; set; }
    }

    public class REASON
    {
        public string LOW_FL_REASON { get; set; }
        public string REASON_NAME { get; set; }
        public string REASONID { get; set; }
    }

    public class MULTICITY
    {
        public string TYPE { get; set; }
        public string Text { get; set; }
    }

    public class HOTELDETAILS
    {
        public string DURATIONDAYS { get; set; }
        public string HTDATE { get; set; }
        public string CITYNAME { get; set; }
        public string MTNAME { get; set; }
        public string INTERNATIONAL { get; set; }
        public string CUCODE { get; set; }
        public string CITYCODE { get; set; }
        public string HTRATE { get; set; }
        public string COCODE { get; set; }
        public string PAYMODE { get; set; }
        public string PAYMODEDESC { get; set; }
        public string MTTYPE { get; set; }
        public string TRRQID { get; set; }
        public string HTNAME { get; set; }
        public string CONAME { get; set; }
        public string MTTYPENAME { get; set; }
        public string AMOUNT { get; set; }
        public string HTREMARKS { get; set; }
    }

    public class LOWESTRETURNFLIGHT
    {
    }

    public class FLDETAIL
    {
        public string ARRDATE { get; set; }
        public string FLNO { get; set; }
        public string SEGMENTLINENO { get; set; }
        public string SEQNO { get; set; }
        public string TRRQID { get; set; }
        public string FLFROMNAME { get; set; }
        public string CUCODE { get; set; }
        public string DEPTIME { get; set; }
        public string FLTO { get; set; }
        public string SectorRPH { get; set; }
        public string FLTONAME { get; set; }
        public string DEPDATE { get; set; }
        public string CACODE { get; set; }
        public string BASEFARE { get; set; }
        public string InPolicy { get; set; }
        public string RETURN { get; set; }
        public string ARRTIME { get; set; }
        public string TRTYPE { get; set; }
        public string TOTALTAX { get; set; }
        public string FLFROM { get; set; }
    }

    public class RETURNFLIGHT
    {
        public List<FLDETAIL> FLDETAILS { get; set; }
    }

    public class FLDETAIL2
    {
        public string ARRDATE { get; set; }
        public string FLNO { get; set; }
        public string SEGMENTLINENO { get; set; }
        public string SEQNO { get; set; }
        public string TRRQID { get; set; }
        public string FLFROMNAME { get; set; }
        public string CUCODE { get; set; }
        public string DEPTIME { get; set; }
        public string FLTO { get; set; }
        public string SectorRPH { get; set; }
        public string FLTONAME { get; set; }
        public string DEPDATE { get; set; }
        public string CACODE { get; set; }
        public string BASEFARE { get; set; }
        public string InPolicy { get; set; }
        public string RETURN { get; set; }
        public string ARRTIME { get; set; }
        public string TRTYPE { get; set; }
        public string TOTALTAX { get; set; }
        public string FLFROM { get; set; }
    }

    public class DEPATUREFLIGHT
    {
        public List<FLDETAIL2> FLDETAILS { get; set; }
    }

    public class LOWESTDEPATUREFLIGHT
    {
    }

    public class SELECTEDFLIGHTDETAILS
    {
        public LOWESTRETURNFLIGHT LOWESTRETURNFLIGHT { get; set; }
        public RETURNFLIGHT RETURNFLIGHT { get; set; }
        public DEPATUREFLIGHT DEPATUREFLIGHT { get; set; }
        public string PAYMODEDESC { get; set; }
        public LOWESTDEPATUREFLIGHT LOWESTDEPATUREFLIGHT { get; set; }
        public string PAYMODE { get; set; }
    }

    public class FLIGHTDETAILS
    {
        public string RETURN { get; set; }
        public string DESTINATION { get; set; }
        public string ARRIVALTIME { get; set; }
        public string ORIGIN { get; set; }
        public string REMARKS { get; set; }
        public string TRAVELTYPE { get; set; }
        public string ARRIVALDATE { get; set; }
        public string RETURNDATE { get; set; }
        public string SEQNO { get; set; }
        public string DESTINATIONNAME { get; set; }
        public string ORIGINNAME { get; set; }
        public string DEPARTUREDATE { get; set; }
        public string RETURNTIME { get; set; }
        public string CABINCLASS { get; set; }
        public string DEPARTURETIME { get; set; }
    }

    public class REQUESTEDFLIGHTDETAILS
    {
        public SELECTEDFLIGHTDETAILS SELECTEDFLIGHTDETAILS { get; set; }
        public string SEQ_NO { get; set; }
        public FLIGHTDETAILS FLIGHTDETAILS { get; set; }
        public string ACTION { get; set; }
    }

    public class OFFLINECARDETAILS
    {
        public string TR_RQ_ID { get; set; }
        public string DROP_DATE { get; set; }
        public string CAR_TYPE { get; set; }
        public string PAY_MODE_DESC { get; set; }
        public string DROP_TIME { get; set; }
        public string CO_CODE { get; set; }
        public string PICKUP_LOCATION { get; set; }
        public string PICKUP_TIME { get; set; }
        public string CO_CODE_NAME { get; set; }
        public string CO_NAME { get; set; }
        public string CAR_CODE { get; set; }
        public string CAR_MODEL { get; set; }
        public string PICKUP_DATE { get; set; }
        public string CITY_CODE { get; set; }
        public string REMARKS { get; set; }
        public string CITY_NAME { get; set; }
        public string CAR_TYPE_NAME { get; set; }
        public string AMOUNT { get; set; }
        public string DROP_LOCATION { get; set; }
        public string PAY_MODE { get; set; }
        public string CU_CODE { get; set; }
    }

    public class OFFLINEBUSDETAILS
    {
        public string CS_TO { get; set; }
        public string REMARKS { get; set; }
        public string ARR_TIME { get; set; }
        public string CS_FROM_NAME { get; set; }
        public string DEP_DATE { get; set; }
        public string CS_CLASS_CODE_NAME { get; set; }
        public string PAY_MODE_DESC { get; set; }
        public string CS_TO_NAME { get; set; }
        public string TR_RETURN { get; set; }
        public string ARR_DATE { get; set; }
        public string CU_CODE { get; set; }
        public string AMOUNT { get; set; }
        public string DEP_TIME { get; set; }
        public string CS_CLASS_CODE { get; set; }
        public string CS_FROM { get; set; }
        public string PAY_MODE { get; set; }
    }

    public class AdditionalService
    {
    }

    public class CarDriverDetail
    {
        public string PhoneNo { get; set; }
        public string FirstName { get; set; }
        public string CityCode { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string CountryCode { get; set; }
        public string Email { get; set; }
    }

    public class OnlineCarDetail
    {
        public string Action { get; set; }
        public string CarSno { get; set; }
        public string PickLocationType { get; set; }
        public string DropLocationType { get; set; }
        public CarDriverDetail CarDriverDetail { get; set; }
        public string JustificationCode { get; set; }
        public string CarProviderSno { get; set; }
        public string Remarks { get; set; }
    }

    public class TRAVLERDETAILS
    {
        public string CCID { get; set; }
        public string DEPT_ID { get; set; }
        public string TARQID { get; set; }
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string OFID { get; set; }
        public string DEFAULTCURR { get; set; }
        public string COORIDINATORCUSTOMERID { get; set; }
        public string GUESTBOOKING { get; set; }
        public string READ_FARE_RULE { get; set; }
        public string CCENABLE { get; set; }
        public string TMC_OF_ID { get; set; }
        public string DTTI_Created { get; set; }
    }

    public class MEETINGDETAILS
    {
        public string MTSTARTTIME { get; set; }
        public string MTENDDATE { get; set; }
        public string APPROVERNAME { get; set; }
        public string CONAME { get; set; }
        public string PROFITCENTREDESC { get; set; }
        public string DURATIONHOURS { get; set; }
        public string CCENABLE { get; set; }
        public string CCNAME { get; set; }
        public string COCODE { get; set; }
        public string MTENDTIME { get; set; }
        public string DURATIONDAYS { get; set; }
        public string CITYNAME { get; set; }
        public string TA_Remarks { get; set; }
        public string CC_ID { get; set; }
        public string TOTALCOST { get; set; }
        public string BUSINESSCOSTCENTERDESC { get; set; }
        public string CC_NAME { get; set; }
        public string PURPOSENAME { get; set; }
        public string PURPOSEID { get; set; }
        public string CHANNELID { get; set; }
        public string PROFITCENTREID { get; set; }
        public string BUSINESSCOSTCENTERID { get; set; }
        public string PURPOSEDETAILS { get; set; }
        public string BUSINESSAREADESC { get; set; }
        public string LOCATIONDESC { get; set; }
        public string Mobile_Data_Activation { get; set; }
        public string INTERNAL_ORDER_NO { get; set; }
        public string CCID { get; set; }
        public string PURPOSEDETAILID { get; set; }
        public string CHANNELDESC { get; set; }
        public string IMPRESTLASTDATE { get; set; }
        public string SPECIALREMARKS { get; set; }
        public string APPROVERID { get; set; }
        public string TripReasonType { get; set; }
        public string CITYCODE { get; set; }
        public string BUSINESSAREAID { get; set; }
        public string SEGMENTDESC { get; set; }
        public string LOCATIONID { get; set; }
        public string MTTYPE { get; set; }
        public string MaximumPrice { get; set; }
        public string DIVISION_NAME { get; set; }
        public string SEGMENTID { get; set; }
        public string DIVISION_ID { get; set; }
        public string CCEMAIL { get; set; }
        public string MTSTARTDATE { get; set; }
    }

    public class EXPENSEREQUEST
    {
        public string PNRNO { get; set; }
        public string ACTION { get; set; }
        public string Text { get; set; }
    }

    public class UPADDTRVELREQUESTINPUT
    {
        public string DeviceType { get; set; }
        public string ATTENDEES { get; set; }
        public string COSTCENTREDIVISIONPURPOSEPROJECT { get; set; }
        public string PositionID { get; set; }
        public OFFLINEFLDETAILS OFFLINE_FL_DETAILS { get; set; }
        public List<EXPENSEDETAIL> EXPENSEDETAILS { get; set; }
        public OnlineHotelDetail OnlineHotelDetail { get; set; }
        public SequenceNumber SequenceNumber { get; set; }
        public string MaximumPrice { get; set; }
        public OFFLINERAILDETAILS OFFLINE_RAIL_DETAILS { get; set; }
        public INSURANCE INSURANCE { get; set; }
        public VISADETAILS VISADETAILS { get; set; }
        public string TRIPID { get; set; }
        public REASON REASON { get; set; }
        public MULTICITY MULTICITY { get; set; }
        public List<object> lstIMPRESTDETAILS { get; set; }
        public string DeviceId { get; set; }
        public string UniqueID { get; set; }
        public string InPolicy { get; set; }
        public HOTELDETAILS HOTELDETAILS { get; set; }
        public REQUESTEDFLIGHTDETAILS REQUESTEDFLIGHTDETAILS { get; set; }
        public OFFLINECARDETAILS OFFLINE_CAR_DETAILS { get; set; }
        public OFFLINEBUSDETAILS OFFLINE_BUS_DETAILS { get; set; }
        public string APLTYPEID { get; set; }
        public AdditionalService AdditionalService { get; set; }
        public OnlineCarDetail OnlineCarDetail { get; set; }
        public TRAVLERDETAILS TRAVLERDETAILS { get; set; }
        public MEETINGDETAILS MEETINGDETAILS { get; set; }
        public EXPENSEREQUEST EXPENSEREQUEST { get; set; }
        public string TokenID { get; set; }
    }

    public class SubmitBookRequest
    {
        public UPADDTRVELREQUESTINPUT UPADDTRVELREQUESTINPUT { get; set; }
    }
}
