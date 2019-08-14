
using BirdResAWSBot.SBT;
using BirdResAWSBot.SBT.Request;
using BirdResAWSBot.SBT.Request.Response;
using BirdResMSBot;
using EchoBot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace BirdResAWSBot
{
 public    class Flight_Availability
    {
       static  string meridian = "";

        public List<CustomFlightDetailModel> AvailableFlights(JObject entities)
        {
            string Result;
            string[] AirportCodes = new string[2];
            // dynamic date1 = entities.GetValue("datetime") != null ? entities.GetValue("datetime") : "x";
            DateTime date=DateTime.Today;
            string days = entities.GetValue("days") != null ? entities.GetValue("days").FirstOrDefault().ToString() : "";
            days = days != "" ? days.Substring(0, 1) : "";
            if (entities.GetValue("datetime") != null)
            {

            
            foreach (var i in entities.GetValue("datetime"))
            {
                if (i["type"].ToString() == "date")
                {
                        date = Convert.ToDateTime(i["timex"].First.ToString().Replace("XXXX", DateTime.Now.Year.ToString()));
                }
                    else if(i["type"].ToString()=="duration")
                    {
                        days = i["timex"].First.ToString().Substring(1, 1);
                    }
            }
        }
            else
            {
                date = DateTime.Today.AddDays(1);
            }
            
            date = date.Month < DateTime.Now.Month ? date.AddYears(1) : date;
  
            DateTime resdate = date;
            if (entities.GetValue("City") != null)
            {
                var AirportCityList = entities.GetValue("City");
                int i = 0;
                foreach (var x in AirportCityList)
                {

                    if (x.ToString().Length > 2)
                    {
                        AirportCodes[i] = ACode.GetAirPortCodeByCity(x.ToString());
                        i += 1;
                    }
                }
            }
            else
            {
                Result = "0";
            }
            meridian = entities.GetValue("Meridian") != null ? entities.GetValue("Meridian").FirstOrDefault().ToString() : "";
            if (entities.GetValue("AirportCode") != null)
            {
                var AirportCodeList = entities.GetValue("AirportCode");
                int i = 0;
                foreach (var x in AirportCodeList)
                {

                    if (x.ToString().Length > 2)
                    {
                        AirportCodes[i] = x.ToString();
                        i += 1;
                    }
                }
                Result = "1";
            }

            string q = entities.GetValue("quantity") != null ? entities.GetValue("quantity").FirstOrDefault().ToString() : "1";


            var myitem = new UtteranceLog();
            myitem.query = new Query();
            myitem.query.Dest = ACode.GetCityByAirPortCode(AirportCodes[1]);
            myitem.query.Origin = ACode.GetCityByAirPortCode(AirportCodes[0]);
            myitem.query.Quantity =Convert.ToInt32(q??"1");

            IDictionary<string, object> changes = new Dictionary<string, object>();
            {
                changes.Add(EmptyBot.DeviceId + "query", myitem);
            }
            System.Threading.CancellationToken s;
            EmptyBot._myStorage.WriteAsync(changes, s);

            //----------------------------------------------------------
            //  EmployeeModel em = new EmptyBot().GiveEmployee();

            // new LoginIntent().Login(entities);
            EmployeeModel em = new EmptyBot().GiveEmployee();

            string pref = new LoginIntent().TripHistory(em.CustId, AirportCodes[0], AirportCodes[1],em.TokenID);
           //pref = em.ALLOW_PREFERRED_FL.ToLower() == "false" ? "" : pref;
            string uniqueid = Guid.NewGuid().ToString();
         

            // List<LexResponse.LexGenericAttachments> attachments = new List<LexResponse.LexGenericAttachments>();
            List<SBT.Request.Response.PricedItinerary> iteneraries = ApiRes(resdate, days ,q??"1", AirportCodes[0], AirportCodes[1], uniqueid,em.TokenID, "AI");
            List<CustomFlightDetailModel> customFlights = new List<CustomFlightDetailModel>();


            //session["iter"] = JsonConvert.SerializeObject(Result.Take(5));

            int iGroup = 0;
            foreach (var iter in iteneraries)
            {
                //var z = iter.Flights.FirstOrDefault();

                List<SBT.Request.Response.Flight> flights = iter.Flights;
                iGroup = iGroup + 1;

                int iSegment = 0;

                foreach (SBT.Request.Response.Flight z in flights)
                {
                    iSegment++;
                    customFlights.Add(new CustomFlightDetailModel
                    {
                        FlightNumber = z.FlightNumber,
                        ArrivalAirport = z.ArrivalAirportName,
                        DepAirport = z.DepartureAirportName,
                        ArrivalDate = z.ArrivalDate,// DateTime.ParseExact(l.ArrivalDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString(),
                        ArrivalTime = z.ArrivalTime.Insert(2, ":"),
                        DepartureTime = z.DepartureTime.Insert(2, ":"),
                        Currency = iter.PaxFares.CurrencyCode,
                        FlightFare = Convert.ToDecimal(iter.TotalLowestFare),
                        Baggage = z.Baggage,
                        Duration = z.Duration,
                        InterMediateStops = GetInterstops(iter),
                        AirLineCode = z.AirlineCode,
                        DepartureDate = z.DepartureDate,
                        OLocCode = z.DepartureLocationCode,
                        ALocCode = z.ArrivalLocationCode,
                        Return = days,
                        UniqueId = uniqueid,
                        ImageUrl = z.AirlineLogo,
                        Group = iGroup,
                        Direction = z.DirectionInd,
                        AirlineName = z.AirlineName,
                        Layover = z.LayoverTime
                    });
                }
            }
            new PIT(customFlights.Take(20).ToList());


            return   customFlights.Take(20).ToList();
            
               
        }
        public  string GetInterstops(SBT.Request.Response.PricedItinerary pricedItinerary)
        {
            StringBuilder Result = new StringBuilder();
            Result.Append(pricedItinerary.Flights.FirstOrDefault().DepartureCityName);
            foreach (var x in pricedItinerary.Flights)
            {
                Result.Append(" -> " + x.ArrivalCityName);
            }

            return Result.ToString();
        }
        public  List<SBT.Request.Response.PricedItinerary> ApiRes(DateTime date, string type, string quantity, string origin, string desti,string uniq,string token, string airpref)
        {
            SearchResponse details;
            List<SBT.Request.Response.PricedItinerary> Result;
            try
            {
                WebRequest request = WebRequest.Create("http://online-corporate-traveller.amadeus.com/live/V17_2_0/webapi/api/SBTMobile/FlightSearch");
                request.Method = "POST";
                request.ContentType = "application/json";
                Stream dataStream = request.GetRequestStream();

                // getJson(date, direct, type, quantity, origin, desti);
                // JObject abc = new SearchRequest().GetJson();
                var req = new SearchRequest().GetJson(origin, desti, date, type, quantity, uniq, token, airpref);
                byte[] byteArray = Encoding.UTF8.GetBytes(req);
                request.ContentLength = byteArray.Length;
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream receiveStream = GetStreamForResponse(response))
                {
                    StreamReader sr = new StreamReader(receiveStream);
                    string responseFromServer = sr.ReadToEnd();

                    dynamic json = JsonConvert.DeserializeObject(responseFromServer);
                    details = JsonConvert.DeserializeObject<SearchResponse>(json.ToString());
                    List<SBT.Request.Response.PricedItinerary> itenararies = new List<SBT.Request.Response.PricedItinerary>();
                   
                    if (meridian.ToLower() == "morning")
                    {
                        itenararies = (from itenarary in details.FlightSearchResult.PricedItinerary
                                       from ODO in itenarary.Flights
                                       where Convert.ToInt32(ODO.DepartureTime) < 1200
                                       select itenarary).Take(10).ToList();
                    }
                    else if (meridian.ToLower() == "evening")
                    {
                        itenararies = (from itenarary in details.FlightSearchResult.PricedItinerary
                                       from ODO in itenarary.Flights
                                       where Convert.ToInt32(ODO.DepartureTime) > 1800
                                       select itenarary).Take(10).ToList();

                    }
                    else if (meridian.ToLower() == "afternoon")
                    {
                        itenararies = (from itenarary in details.FlightSearchResult.PricedItinerary
                                       from ODO in itenarary.Flights
                                       where Convert.ToInt32(ODO.DepartureTime) > 1200 && Convert.ToInt32(ODO.DepartureTime)<=1800
                                       select itenarary).Take(10).ToList();

                    }
                    else
                    {
                        itenararies = details.FlightSearchResult.PricedItinerary.Take(10).ToList();
                    }

             Result= itenararies;

                }
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
        }
 public       static Stream GetStreamForResponse(HttpWebResponse webResponse)
        {
            Stream stream;
            if (webResponse.ContentEncoding == null)
            {
                stream = webResponse.GetResponseStream();
            }
            else
            {
                switch (webResponse.ContentEncoding.ToUpperInvariant())
                {
                    case "GZIP":
                        stream = new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress);
                        break;
                    case "DEFLATE":
                        stream = new DeflateStream(webResponse.GetResponseStream(), CompressionMode.Decompress);
                        break;

                    default:
                        stream = webResponse.GetResponseStream();
                        break;
                }

            }
            return stream;
        }

    }
    public class PIT
    {
        static List<CustomFlightDetailModel> mydetails;
        public PIT(List<CustomFlightDetailModel> details)
        {
            mydetails = details;
        }



        public static List<CustomFlightDetailModel> GiveItenerary()
        {
            return mydetails;
        }
    }
}
