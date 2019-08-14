using BirdResMSBot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BirdResAWSBot
{
  public  class SendMail
    {
        public string Mail(JObject entities)
        {



            List<CustomFlightDetailModel> pricedItineraries = new EmptyBot().GetIteneraries();
            string alcode = entities.GetValue("FlightNumber").FirstOrDefault().ToString().Substring(0, 2);
            string fn = entities.GetValue("FlightNumber").FirstOrDefault().ToString().Substring(2);
            //FlightNumber
            StringBuilder Result = new StringBuilder("flight Details of flight number : ");
            Result.Append(entities.GetValue("FlightNumber").FirstOrDefault().ToString());
            var result = (from s in pricedItineraries
                          where s.FlightNumber == fn
                          select s).FirstOrDefault();
            if (result != null)
            {
                //var sum = result.Duration;
                Result.Append(Environment.NewLine)
                .Append("\nFrom : ")
                .Append(result.DepAirport)
                .Append(Environment.NewLine)
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
                .Append("\nDeparture datetime : ")
               .Append(result.DepartureTime)
               .Append(Environment.NewLine)
                .Append("\n Duration : ")
                .Append(result.Duration + " minutes")
                //.Append("\n available seats : ")
                //.Append(z.SeatAvailable)
                .Append(Environment.NewLine)
                .Append("\n Baggage Allowance : ")
                .Append(result.Baggage + " KG")
                .Append(Environment.NewLine)
                .Append("\nTotal fare : " + result.Currency + " ")
                .Append(result.FlightFare);


            }


            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("mukeshkprajapati05@gmail.com");
            message.To.Add(new MailAddress(entities.GetValue("email").FirstOrDefault().ToString()));
            message.Subject = "Flight Details";
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = Result.ToString();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail hostSparrow@123  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("mukeshkprajapati05@gmail.com", "mukesh@328533");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);



            return "Flight details mailed to you successfully";

            



        }

        public string FareDetail(JObject entities)
        {


            List<CustomFlightDetailModel> pricedItineraries = new EmptyBot().GetIteneraries();
            string alcode = entities.GetValue("FlightNumber").FirstOrDefault().ToString().Substring(0, 2);
            string fn = entities.GetValue("FlightNumber").FirstOrDefault().ToString().Substring(2);
            var result = (from s in pricedItineraries
                          where s.FlightNumber == fn
                          select s).FirstOrDefault();
            return "Total fare : " + result.FlightFare;





        }


    }

}

