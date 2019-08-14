using System;
using System.Collections.Generic;
using System.Text;

namespace BirdResAWSBot.SBT.Request
{
    public class LstSecurityOtpion
    {
        public string Security_Optoin_ID { get; set; }
        public string Sec_Value { get; set; }
        public string Security_Option_Name { get; set; }
    }

    public class LstAdminSetting
    {
        public string Type_Keyword { get; set; }
        public string Type_Desc { get; set; }
        public string Value { get; set; }
        public string Config_Value { get; set; }
    }

    public class LstMetroCity
    {
        public string City_Code { get; set; }
        public string City_Name { get; set; }
        public string CO_Code { get; set; }
    }

    public class FlightDurationWindows
    {
        public string Duration { get; set; }
    }

    public class AirTravelFareWindows
    {
        public string INT_Flight_Fare { get; set; }
        public string DOM_Flight_Fare { get; set; }
    }

    public class AirTravelTimeInt
    {
        public string Check { get; set; }
        public string One_Way_Time_Range { get; set; }
        public string Return_Time_Range { get; set; }
    }

    public class AirTravelTimeWindows
    {
        public object AirTravelTimeDom { get; set; }
        public AirTravelTimeInt AirTravelTimeInt { get; set; }
    }

    public class Agent
    {
        public string TA_ID { get; set; }
        public string User_ID { get; set; }
        public string OF_ID { get; set; }
        public string Curr_Code { get; set; }
        public string TA_CL_Amount { get; set; }
        public string TA_Account_Freezed { get; set; }
    }

    public class Errors
    {
        public string Status { get; set; }
        public object Error { get; set; }
        public string Desc { get; set; }
        public string Code { get; set; }
    }

    public class LoginResponse
    {
        public object User_Password { get; set; }
        public string Login_ID { get; set; }
        public string Customer_ID { get; set; }
        public string Email_ID { get; set; }
        public string Title { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Employee_Code { get; set; }
        public string OF_OfficeID { get; set; }
        public string Travel_Coordinator { get; set; }
        public string OF_ID { get; set; }
        public string Policy_Apply { get; set; }
        public string First_Approver { get; set; }
        public string First_Approver_Code { get; set; }
        public string Curr_Code { get; set; }
        public string OF_Name { get; set; }
        public string OF_City { get; set; }
        public object OF_City_Name { get; set; }
        public string City_Name { get; set; }
        public string CO_Name { get; set; }
        public string OF_Country { get; set; }
        public object Country_Name { get; set; }
        public string User_DOB { get; set; }
        public string Contact_No { get; set; }
        public string Mobile_No { get; set; }
        public string NATIONALITY { get; set; }
        public string Is_Travel_Policy { get; set; }
        public string Passport_No { get; set; }
        public string Address { get; set; }
        public string State_Name { get; set; }
        public object Allow_Guest_Booking { get; set; }
        public string Business_Cost_Center { get; set; }
        public string Business_Area { get; set; }
        public string Segment { get; set; }
        public string Location { get; set; }
        public string Channel { get; set; }
        public string Color { get; set; }
        public string Profit_Center { get; set; }
        public string Personal_Booking { get; set; }
        public string PB_Approval_Required { get; set; }
        public string OF_Address1 { get; set; }
        public object OF_Address2 { get; set; }
        public string Dept_ID { get; set; }
        public string Dept_Name { get; set; }
        public string Position_ID { get; set; }
        public string Position_Name { get; set; }
        public string Apl_Type_ID { get; set; }
        public string OF_Airport { get; set; }
        public string Service_Url { get; set; }
        public string IsApprover { get; set; }
        public string Account_Handler { get; set; }
        public string ProfileLocator { get; set; }
        public string CC_ID { get; set; }
        public string CURRNECY_CODE { get; set; }
        public string CONVERSION_RATE { get; set; }
        public List<LstSecurityOtpion> lstSecurityOtpions { get; set; }
        public List<LstAdminSetting> lstAdminSettings { get; set; }
        public List<LstMetroCity> lstMetroCity { get; set; }
        public object lstDomAirLineCode { get; set; }
        public object lstIntAirLineCode { get; set; }
        public List<object> lstCorporateCode { get; set; }
        public string Payment_Types { get; set; }
        public FlightDurationWindows FlightDurationWindows { get; set; }
        public AirTravelFareWindows AirTravelFareWindows { get; set; }
        public AirTravelTimeWindows AirTravelTimeWindows { get; set; }
        public Agent Agent { get; set; }
        public Errors Errors { get; set; }
        public string TokenID { get; set; }
     
    }
}
