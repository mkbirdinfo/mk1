using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdResAWSBot.SBT.Request
{
    class SearchRequest
    {
 public  string  GetJson(string OriginCode,string DestCode, DateTime date, string type,string Pax,string uniqueid,string TokenId ,string airpref="" )
        {

            DateTime rtdate = date.AddDays(Convert.ToInt32(type == "" ? "0" : type));
          
            string month = date.Month > 9 ? date.Month.ToString() : "0" + date.Month;
            string day = date.Day > 9 ? date.Day.ToString() : "0" + date.Day;
            string returnday = rtdate.Day > 9 ? rtdate.Day.ToString() : "0" + rtdate.Day;
            string rtmonth = rtdate.Month > 9 ? rtdate.Month.ToString() : "0" + rtdate.Month;
            string formatedDate = date.Year + month + day ;
            string ReturnDate = rtdate.Year + rtmonth + returnday ;
            OriginDestinationInformation round = new OriginDestinationInformation
            {
                OLocationCode =DestCode ,
                OLocationName = "Mumbai",
                OAirportOnly = "1",
                DLocationCode = OriginCode,
                DLocationName = "Delhi",
                DepartureDateTime = new DepartureDateTime
                {
                    WINDOW_PRD = "",
                    AfterDays = "",
                    DepTime = "0000",
                    WindowAfter = "",
                    ToCityFlag = "",
                    DT_Fix = "",
                    TimeWindow = "",
                    FromCityFlag = "",
                    WindowBefore = ReturnDate,
                    ReturnTime = "0000",
                    ToDTFix = "",
                    BeforeDays = "",
                    PreAirLine = airpref,
                    DeptTimeNew = "0000",
                    FromRadius = "0",
                    FromDTFix = "",
                    ToRadius = "0"

                }
            };





            Search RequestObj = new Search()
            {
                OF_ID = "10279",
                IsTryAgain = "0",
                ReturnAvailability = new ReturnAvailability
                {
                    SpecialFare = "FALSE",
                    Status = type!=""?"TRUE":"FALSE",

                },
                IsUpgradePolicy = "0",
                TicketTypePref = "E",
                MultiCity = type != "" ? "FALSE" : "TRUE",
                MulitCityType = "I",
                MiscInfo = new List<MiscInfo>()
                 {
                     new MiscInfo
                     {
                         RefundableFlightCategory="",
                         ResultAgainstArrivalAirportOnly="0",
                         ResultAgainstDepartureAirportOnly="0",
                         DirectFlightCategory="",
                         Refundable_Fare=""
                     }
                 },
                UniqueID = uniqueid,
                OriginDestinationInformation=new List<OriginDestinationInformation>()
                {
                    new OriginDestinationInformation
                    {
                        OLocationCode=OriginCode,
                        OLocationName="Delhi",
                        OAirportOnly="1",
                        DLocationCode=DestCode,
                        DLocationName="Mumbai",
                        DepartureDateTime=new DepartureDateTime
                        {
                            WINDOW_PRD="",
                            AfterDays="",
                            DepTime="0000",
                            WindowAfter="",
                            ToCityFlag="",
                            DT_Fix="",
                            TimeWindow="",
                            FromCityFlag="",
                            WindowBefore=formatedDate,
                            ReturnTime="0000",
                            ToDTFix="",
                            BeforeDays="",
                            PreAirLine = "",//"9W,6E,"+airpref
                            DeptTimeNew="0000",
                            FromRadius="0",
                            FromDTFix="",
                            ToRadius="0"                    
                            
                        }
                    },
              

                },
                DestinationCountryCode=DestCode,
                ReasonID="0",
                CabinPref="Economy",
                ReasonName= "BUSINESS",
                PositionID="2306",
                TravelerInfoSummary=new TravelerInfoSummary
                {
                    PassengerTypeQuantity=new List<PassengerTypeQuantity>
                    {
                        new PassengerTypeQuantity
                        {
                            Code="ADT",
                            Quantity=Pax,

                        },
                       

                    },
                    Nationality="IN"
                },
                TMC_OF_ID="10278",
                TokenID=TokenId



            };
            if (type != "")
            {
                RequestObj.OriginDestinationInformation.Add(round);
            }
            string JsonRequestObj =JsonConvert.SerializeObject(RequestObj);

            return JsonRequestObj;
        }
        
    }
}

  