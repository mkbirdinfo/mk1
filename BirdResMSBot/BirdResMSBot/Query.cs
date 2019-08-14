using BirdResAWSBot;
using BirdResAWSBot.SBT;
using BirdResMSBot;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EchoBot
{
    public class Query
    {
        public string Origin { get; set; }
        public string Dest { get; set; }
        public DateTime? Date { get; set; }
        public int? Quantity { get; set; }
    }




    public class QResult
    {

        public dynamic QueriResult(JObject entities, Query query,string DeviceId)
        {
            dynamic qresult = "";
            //   Query custom= new Query();
           
            if (query != null)
            {
                var check = entities.GetValue("City") != null ? entities.GetValue("City").FirstOrDefault().ToString() : "";
                //|| entities.GetValue("City").FirstOrDefault().ToString()!=query.Origin
                query.Quantity = 1;
                if (query.Origin == null||(check!=query.Origin&&check!=""))
                {
                    if (entities.GetValue("City") != null)
                    {
                        string x ="";
                        if (entities.GetValue("fromto") != null)
                        {
                            x = entities.GetValue("fromto").FirstOrDefault().ToString();
                            x = x.Length==2 ? "to" : "from";
                            
                        }
                        if (x.ToString() == "from" || x.ToString() == "")
                        {
                            query.Origin = entities.GetValue("City").FirstOrDefault().ToString();
                            qresult = Mesg(query);

                        }
                        else
                        {

                            qresult = Mesg(query);
                        }

                    }
                }
                //|| entities.GetValue("City").LastOrDefault().ToString()!=query.Dest
                if (query.Dest == null || (check != query.Dest && check != ""))
                {
                    if (entities.GetValue("City") != null)
                    {
                        dynamic x = "";
                        if (entities.GetValue("fromto")!= null)
                        {
                            x = entities.GetValue("fromto").LastOrDefault().ToString();
                        }
                          
                        if ((x == "to" || x == "")&& entities.GetValue("City").LastOrDefault().ToString()!=query.Origin)
                        {
                            query.Dest = entities.GetValue("City").LastOrDefault().ToString();

                            qresult = Mesg(query);

                        }
                    }
                }
                 if (query.Date == null)
                {
                    if(entities.GetValue("datetime") != null)
                    {
                        dynamic date1 =  entities.GetValue("datetime").FirstOrDefault() ;
                        query.Date = Convert.ToDateTime(date1["timex"].First.ToString().Replace("XXXX", DateTime.Now.Year.ToString()));
                        query.Date = query.Date.GetValueOrDefault().Month < DateTime.Now.Month ? query.Date.GetValueOrDefault().AddYears(1) : query.Date;
                        qresult = Mesg(query);
                    }
                        

                }
                 if (query.Quantity == null)
                {
                    if (entities.GetValue("quantity") != null)
                    {

                        query.Quantity = int.Parse(entities.GetValue("quantity").FirstOrDefault().ToString());
                        qresult = Mesg(query);

                    }
                }

            }
            string days = entities.GetValue("days") != null ? entities.GetValue("days").FirstOrDefault().ToString() : "";
            days = days != "" ? days.Substring(0, 1) : "";
            List<CustomFlightDetailModel> customFlights=new List<CustomFlightDetailModel>();
            List<BirdResAWSBot.SBT.Request.Response.PricedItinerary> iteneraries=new List<BirdResAWSBot.SBT.Request.Response.PricedItinerary>();
            string uniqueid = Guid.NewGuid().ToString();
            if (query.Origin != null && query.Dest != null && query.Date != null && query.Quantity != null)
            {

                EmployeeModel em = new EmptyBot().GiveEmployee();

                // List<LexResponse.LexGenericAttachments> attachments = new List<LexResponse.LexGenericAttachments>();
                iteneraries = new Flight_Availability().ApiRes(query.Date.GetValueOrDefault(),days, query.Quantity.ToString(), ACode.GetAirPortCodeByCity(query.Origin), ACode.GetAirPortCodeByCity(query.Dest), uniqueid,em.TokenID, "AI");
                //    qresult = FlightsInfo.ApiRes(query.Date.GetValueOrDefault(), false, false, query.Quantity.ToString(), ACode.GetAirPortCodeByCity(query.Origin), ACode.GetAirPortCodeByCity(query.Dest));
                int iGroup = 0;
                foreach (var iter in iteneraries)
                {
                    //var z = iter.Flights.FirstOrDefault();
                    //var l = iter.Flights.LastOrDefault();

                    //customFlights.Add(new CustomFlightDetailModel
                    //{
                    //    FlightNumber = z.FlightNumber,
                    //    ArrivalAirport = z.ArrivalAirportName,
                    //    DepAirport = z.DepartureAirportName,
                    //    ArrivalDate = z.ArrivalDate,// DateTime.ParseExact(l.ArrivalDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString(),
                    //    ArrivalTime = z.ArrivalTime.Insert(2, ":"),
                    //    DepartureTime = z.DepartureTime.Insert(2, ":"),
                    //    Currency = "INR",
                    //    FlightFare = Convert.ToDecimal(iter.TotalLowestFare),
                    //    Baggage = z.Baggage,
                    //    Duration = z.Duration,
                    //    InterMediateStops = new Flight_Availability().GetInterstops(iter),
                    //    AirLineCode = z.AirlineCode,
                    //    DepartureDate = z.DepartureDate,
                    //    OLocCode = z.DepartureLocationCode,
                    //    ALocCode = z.ArrivalLocationCode,
                    //    Return = days,
                    //    UniqueId = uniqueid,
                    //    ImageUrl = z.AirlineLogo

                    //});
                    
                    List<BirdResAWSBot.SBT.Request.Response.Flight> flights = iter.Flights;
                    iGroup = iGroup + 1;

                    int iSegment = 0;

                    foreach (BirdResAWSBot.SBT.Request.Response.Flight z in flights)
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
                            InterMediateStops = new Flight_Availability().GetInterstops(iter),
                            AirLineCode = z.AirlineCode,
                            DepartureDate = z.DepartureDate,
                            OLocCode = z.DepartureLocationCode,
                            ALocCode = z.ArrivalLocationCode,
                            Return = days,
                            UniqueId = uniqueid,
                            ImageUrl = z.AirlineLogo,
                            Group = iGroup,
                            Direction = z.DirectionInd,
                            AirlineName=z.AirlineName,
                            Layover=z.LayoverTime
                        });
                    }
                }


            

                qresult= customFlights.Take(20).ToList();
                query.Date =null ;
            }



           
            new PIT(customFlights.Take(20).ToList());


          

            var myitem = new UtteranceLog();
            myitem.query = query;
           

            IDictionary<string, object> changes = new Dictionary<string, object>();
            {
                changes.Add(DeviceId+"query", myitem);
            }
            System.Threading.CancellationToken s;
            EmptyBot._myStorage.WriteAsync(changes, s);

            return qresult;
        }

        

        public static string Mesg(Query q)
        {
            string msg="";
            if(q.Origin == null)
            {
                msg = "please provide Origin";
            }
            else if (q.Dest == null)
            {
                msg = "please provide destination";
            }
            else if (q.Date == null)
            {
                msg = "please provide date";
            }
            else if(q.Quantity==null)
            {
                msg ="please provide PAX";
            }
            return msg;
        }
       
          
    }

    


}
