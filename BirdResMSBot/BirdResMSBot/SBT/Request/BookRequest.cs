using BirdResMSBot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BirdResAWSBot.SBT.Request
{
   
    public class Passport
    {
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Date_OF_Issue { get; set; }
        public string Date_OF_Expiry { get; set; }
        public string Number { get; set; }
        public string Place_OF_Issue { get; set; }
    }

    public class Address
    {
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string StateCode { get; set; }
        public string CityName { get; set; }
        public string CO_Code { get; set; }
    }

    public class Meals
    {
        public string Desc { get; set; }
        public string Amount { get; set; }
        public string Code { get; set; }
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

    public class Others
    {
        public string Code { get; set; }
        public string Desc { get; set; }
        public string SegmentNo { get; set; }
    }

    public class LstMealDetail
    {
        public string Fax { get; set; }
        public Meals Meals { get; set; }
        public Seats Seats { get; set; }
        public FFNNO FFNNO { get; set; }
        public Others Others { get; set; }
    }

    public class LstPaxDetail
    {
        public string Mobile { get; set; }
        public string Contact_No { get; set; }
        public string Email { get; set; }
        public string First_Name { get; set; }
        public Passport Passport { get; set; }
        public string DOB { get; set; }
        public string FAMILY_USER_ID { get; set; }
        public Address Address { get; set; }
        public string Pax_Type_Code { get; set; }
        public string Title { get; set; }
        public List<LstMealDetail> lstMealDetails { get; set; }
        public string Last_Name { get; set; }
        public string RPH { get; set; }
    }

    public class LstmiscellaneousInfo
    {
        public string Code { get; set; }
        public string OfficeId { get; set; }
        public string IsDisplayOnFinishing { get; set; }
        public string IsDisplayOnEmail { get; set; }
        public string LabelName { get; set; }
        public string MisceValue { get; set; }
        public string IsDisplayInReport { get; set; }
    }

    public class TMCName
    {
        public string TMC_Name { get; set; }
        public string Office_ID { get; set; }
    }

    public class CreditCardDetail
    {
        public string CardExpiry { get; set; }
        public string OtherOption { get; set; }
        public string Card { get; set; }
        public string CardNumber { get; set; }
        public string TravellerProfilePaymentMode { get; set; }
        public string PaymentMode { get; set; }
    }

    public class Meals2
    {
        public string Desc { get; set; }
        public string Amount { get; set; }
        public string Code { get; set; }
        public string SegmentNo { get; set; }
    }

    public class Seats2
    {
        public string Code { get; set; }
        public string Desc { get; set; }
        public string SegmentNo { get; set; }
    }

    public class FFNNO2
    {
        public string Number { get; set; }
        public string SegmentNo { get; set; }
    }

    public class Others2
    {
        public string Code { get; set; }
        public string Desc { get; set; }
        public string SegmentNo { get; set; }
    }

    public class LstMealDetail2
    {
        public string Fax { get; set; }
        public Meals2 Meals { get; set; }
        public Seats2 Seats { get; set; }
        public FFNNO2 FFNNO { get; set; }
        public Others2 Others { get; set; }
    }

    public class BookRequest
    {
        public List<LstPaxDetail> lstPaxDetails { get; set; }
        public string IsTryAgain { get; set; }
        public List<LstmiscellaneousInfo> lstmiscellaneousInfo { get; set; }
        public string PAYMENT_BORNE_BY { get; set; }
        public TMCName TMCName { get; set; }
        public string OFID { get; set; }
        public CreditCardDetail CreditCardDetail { get; set; }
        public string TokenID { get; set; }
        public string UniqueID { get; set; }
        public string requestID { get; set; }
        public string TAID { get; set; }
        public string ISHold { get; set; }
        public string UserID { get; set; }
        public List<LstMealDetail2> lstMealDetails { get; set; }
        public string LocatorNumber { get; set; }
    }

    public class SubmitResponse
    {
        public string ResultStatus { get; set; }
        public string ResultDescription { get; set; }
        public string TripID { get; set; }
        public string CorporateID { get; set; }
    }

    public class RequestToBookApi
    {
        public string BookFlight(JObject entities,string[] codes)
        {

            string Result = "";
            SubmitResponse submitResponse;
            string x = "";
            try
            {
                
                var bot = new EmptyBot();
                List<CustomFlightDetailModel> detail = bot.GetIteneraries();
                // new LoginIntent().Login(entities);
                List<CustomFlightDetailModel> iterlst = new List<CustomFlightDetailModel>();
                EmployeeModel employee = bot.GiveEmployee();
                if (entities.GetValue("FlightNumber") == null && codes.Length >= 1)
                {
                    foreach (var c in codes)
                    {
                        string code = c.Remove(0, 3);
                        iterlst.Add((from d in detail where d.FlightNumber == code select d).FirstOrDefault());
                    }
                }
                else
                {
                    string alcode = entities.GetValue("FlightNumber").FirstOrDefault().ToString().Substring(0, 2);
                    string fn = entities.GetValue("FlightNumber").FirstOrDefault().ToString().Substring(2);
                    iterlst = (from d in detail where d.FlightNumber == fn select d).ToList();
                }

                WebRequest request = WebRequest.Create("http://online-corporate-traveller.amadeus.com/live/V17_2_0/webapi/api/SBTMobile/SubmitFlightRequest");
                request.Method = "POST";
                request.ContentType = "application/json";
                Stream dataStream = request.GetRequestStream();

                // getJson(date, direct, type, quantity, origin, desti);
                // JObject abc = new SearchRequest().GetJson();
                x = new BookingJsons().GetSubmitBookRequest(iterlst, employee.FirstName, employee.OffId, employee.CustId,employee.TokenID,employee.DeviceId);
                byte[] byteArray = Encoding.UTF8.GetBytes(x);
                request.ContentLength = byteArray.Length;
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream receiveStream =Flight_Availability.GetStreamForResponse(response))
                {
                    StreamReader sr = new StreamReader(receiveStream);
                    string responseFromServer = sr.ReadToEnd();

                    submitResponse = JsonConvert.DeserializeObject<SubmitResponse>(responseFromServer);
                  
                    if (employee.Approval_mail == "FALSE")
                    {
                        Result = GetBookResult(employee, iterlst.FirstOrDefault(), submitResponse.TripID);


                    }
                    else
                    {


                        //SendDetail("TripId : " + submitResponse.TripID, employee.Email);
                        //return "Flight Details / Booking Details sent with tripid :"
                        Result= "Flight Details/Booking Details sent with tripid : " +submitResponse.TripID+" successfully on " +employee.Email;
                        //  await _myStorage.DeleteAsync(new string[] { (DeviceId + "employee"), (DeviceId + "pricedIteneraries") },cancellationToken);



                    }

                }
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
               // Result = ex.StackTrace + " " + x;
            }
          
           
            return Result;

                   
        }

  public string GetBookResult(EmployeeModel employee,CustomFlightDetailModel iter,string tripid)
        {
            BookResponse bookResponse;

            string Result = "";
            try
            {
                WebRequest request = WebRequest.Create("http://online-corporate-traveller.amadeus.com/live/V17_2_0/webapi/api/FlightBook/RequestFlightBook1");
                request.Method = "POST";
                request.ContentType = "application/json";
                Stream dataStream = request.GetRequestStream();

                // getJson(date, direct, type, quantity, origin, desti);
                // JObject abc = new SearchRequest().GetJson();

                byte[] byteArray = Encoding.UTF8.GetBytes(new BookingJsons().GetBookRequest(employee, iter, tripid, employee.TokenID));
                request.ContentLength = byteArray.Length;
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream receiveStream = Flight_Availability.GetStreamForResponse(response))
                {
                    StreamReader sr = new StreamReader(receiveStream);
                    string responseFromServer = sr.ReadToEnd();

                    bookResponse = JsonConvert.DeserializeObject<BookResponse>(responseFromServer);
                    if (bookResponse.ResultDescription != "")
                    {
                        Result = bookResponse.ResultDescription;
                    }
                    else
                    {
                        var c = bookResponse.PricedItinerary.FirstOrDefault().Flights.FirstOrDefault();

                        string Message = "Origin :" + c.DepartureCityName + " Desitination : " + bookResponse.PricedItinerary.FirstOrDefault().Flights.LastOrDefault().ArrivalCityName + " flight number :" +
                             c.FlightNumber + " FareType : " + c.FareType + " ArrivalDate: " + c.ArrivalDate + "DepartureDate : " + c.DepartureDate + "DepartureTime :" + c.DepartureTime;
                        // SendDetail(Message, employee.Email);
                        //string Result = " DepCity : " + c.Flights.FirstOrDefault().DepartureCityName + " ArrivalCity : " + c.Flights.LastOrDefault().ArrivalCityName;
                        Result = "Confirmed.The detail has  been mailed to you on " + employee.Email;
                    }

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

    void  SendDetail(string message1,string to)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("mukeshkprajapati05@gmail.com");
            message.To.Add(new MailAddress(to));
            message.Subject = "Flight Details";
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = message1;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("mukeshkprajapati05@gmail.com", "mukesh@328533");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

    }

}
