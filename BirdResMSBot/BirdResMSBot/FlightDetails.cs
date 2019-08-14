using AdaptiveCards;
using BirdResMSBot;
using EchoBot;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BirdResAWSBot
{
 public   class FlightDetails
    {

        public string GetFlightDetails(JObject entities)
        {

            List<CustomFlightDetailModel> pricedItineraries = new EmptyBot().GetIteneraries();
            string alcode = entities.GetValue("FlightNumber").FirstOrDefault().ToString().Substring(0, 2);
            string fn = entities.GetValue("FlightNumber").FirstOrDefault().ToString().Substring(2);
            //FlightNumber
            StringBuilder Result = new StringBuilder("Showing flight details of : ");
            Result.Append(entities.GetValue("FlightNumber").FirstOrDefault().ToString());
            var result = (from s in pricedItineraries
                          where s.FlightNumber == fn
                          select s).FirstOrDefault();
            if (result != null)
            {
                //var sum = result.Duration;
                Result.Append(Environment.NewLine);
                Result.Append("From : ").
                   
                Append(result.DepAirport).
                 Append(Environment.NewLine)
                .Append("\nTo : ")
                .Append(result.ArrivalAirport)
                .Append(Environment.NewLine)
                 .Append("\nArrival date : ").
                 Append(result.ArrivalDate)
                  .Append(Environment.NewLine)
                 .Append("\nIntermediate Stops : ")
                .Append("\n" + result.InterMediateStops)
                .Append(Environment.NewLine)
                .Append("\nArrivalTime")
                .Append(result.ArrivalTime)
                .Append(Environment.NewLine)
                .Append("\nDeparture time : ")
               .Append(result.DepartureTime)
                .Append(Environment.NewLine)
                .Append("\n Duration : ")
                .Append(result.Duration+" minutes")
                //.Append("\n available seats : ")
                //.Append(z.SeatAvailable)
                .Append(Environment.NewLine)
                .Append("\n Baggage Allowance : ")
                .Append(result.Baggage )
                 .Append(Environment.NewLine)
                .Append("\nTotal fare : " + result.Currency + " ")
                .Append(result.FlightFare);


            }

            return Result.ToString();



        }

  public Attachment Customflights(JObject entities)
        {

            List<CustomFlightDetailModel> pricedItineraries = new EmptyBot().GetIteneraries();
            
            if (entities.GetValue("Meridian")!=null)
            {
                
                if (entities.GetValue("Meridian").FirstOrDefault().ToString().ToLower() == "morning")
                {
                    pricedItineraries = (from itenarary in pricedItineraries
                                   
                                   where Convert.ToInt32(itenarary.DepartureTime.Remove(2,1)) < 1200
                                   select itenarary).Take(10).ToList();
                }
                else if (entities.GetValue("Meridian").FirstOrDefault().ToString().ToLower() == "evening")
                {
                    pricedItineraries = (from itenarary in pricedItineraries
                                         
                                   where Convert.ToInt32(itenarary.DepartureTime.Remove(2, 1)) > 1800
                                   select itenarary).Take(10).ToList();

                }
                else if (entities.GetValue("Meridian").FirstOrDefault().ToString().ToLower() == "afternoon")
                {
                    pricedItineraries = (from itenarary in pricedItineraries
                                        
                                   where Convert.ToInt32(itenarary.DepartureTime.Remove(2, 1)) > 1200 && Convert.ToInt32(itenarary.DepartureTime.Remove(2, 1)) <= 1800
                                   select itenarary).Take(10).ToList();

                }

                return new Attachment
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = AdaptiveCard.FromJson(GiveJson.GetJson(pricedItineraries)).Card
                };

            }
            else
            {

               
            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = AdaptiveCard.FromJson(GiveJson.GetJson(pricedItineraries)).Card
            };
            }

          

           
        }

      



    }
}
