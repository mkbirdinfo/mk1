
using AdaptiveCards;
using BirdResAWSBot.SBT;
using BirdResAWSBot.SBT.Request;
using BirdResAWSBot.SBT.Request.Response;
using BirdResMSBot;
using EchoBot;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BirdResAWSBot
{
    public class LoginIntent
    {
        public string Login(string id, string pass,string DeviceId, string offid="")
        {




            //entities.GetValue("UserId").ToString()
            //entities.GetValue("Password").ToString()
            
            EmployeeModel employee = CallApi("vk032017", "Admin@123", DeviceId, "");
            employee.DeviceId = DeviceId;
            var myitem = new UtteranceLog();
                myitem.employee = employee;
            IDictionary<string, object> changes = new Dictionary<string, object>();
            {
                changes.Add(DeviceId+"employee", myitem);
            }
            System.Threading.CancellationToken s;
             EmptyBot._myStorage.WriteAsync(changes, s);

            string content = employee.Email != null ? "Thanks " + employee.FirstName + " you are logged in successfully" : "Wrong Credentilas";

            return employee.Email != null ? "Thanks " + employee.FirstName + " you are logged in successfully" : "Wrong Credentilas";
        }

        EmployeeModel CallApi(string id, string pass,string DeviceId, string offid)
        {

            EmployeeModel Em;
            try
            {
                WebRequest request = WebRequest.Create("http://online-corporate-traveller.amadeus.com/live/V17_2_0/webapi/api/UserLogin/DoLogin");
                request.Method = "POST";
                request.ContentType = "application/json";
                Stream dataStream = request.GetRequestStream();

                // getJson(date, direct, type, quantity, origin, desti);
                // JObject abc = new SearchRequest().GetJson();

                byte[] byteArray = Encoding.UTF8.GetBytes(GetLoginJson(id, pass,DeviceId, offid));
                request.ContentLength = byteArray.Length;
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream receiveStream = Flight_Availability.GetStreamForResponse(response))
                {
                    StreamReader sr = new StreamReader(receiveStream);
                    string responseFromServer = sr.ReadToEnd();


                    LoginResponse lr = JsonConvert.DeserializeObject<LoginResponse>(responseFromServer);
                    if (lr.Email_ID != null)
                    {
                        Em = new EmployeeModel
                        {
                            FirstName = lr.First_Name,
                            CustId = lr.Customer_ID,
                            OffId = lr.OF_ID,
                            EmpCode = lr.Employee_Code,
                            Email = lr.Email_ID,
                            TokenID = lr.TokenID

                        };

                        Em.Allow_ticketing = (from s in lr.lstAdminSettings where s.Type_Keyword == "AllowTicketing" select s).FirstOrDefault().Value;
                        Em.ALLOW_PREFERRED_FL = (from s in lr.lstAdminSettings where s.Type_Keyword == "ALLOW_PREFERRED_FL" select s).FirstOrDefault().Value;
                        Em.Approval_mail = (from s in lr.lstAdminSettings where s.Type_Keyword == "ApprovalMail" select s).FirstOrDefault().Value;

                    }
                    else
                    {
                        Em = new EmployeeModel();
                    };


                }
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }



            return Em;

        }



        public string GetLoginJson(string id, string pass, string device,string offid = "")
        {

            LoginRequest reqObj = new LoginRequest
            {
                LoginID = id,
                Password = pass,
                OF_OfficeID = offid,
                DeviceID= device


            };






            return JsonConvert.SerializeObject(reqObj);
        }

        public string TripHistory(string CUSTOMER, string or, string dest, string Token)
        {
            string airpref = "";
            string req = JsonConvert.SerializeObject(new HistoryRequest
            {
                CUSTOMER_ID = CUSTOMER,
                FL_FROM = or,
                FL_TO = dest,
                TokenID = Token
            });
            try
            {
                WebRequest request = WebRequest.Create("http://online-corporate-traveller.amadeus.com/live/V17_2_0/webapi/api/Common/GetCustomerFlightFromTo");
                request.Method = "POST";
                request.ContentType = "application/json";
                Stream dataStream = request.GetRequestStream();

                // getJson(date, direct, type, quantity, origin, desti);
                // JObject abc = new SearchRequest().GetJson();

                byte[] byteArray = Encoding.UTF8.GetBytes(req);
                request.ContentLength = byteArray.Length;
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream receiveStream = Flight_Availability.GetStreamForResponse(response))
                {
                    StreamReader sr = new StreamReader(receiveStream);
                    string responseFromServer = sr.ReadToEnd();

                    HistoryResponse hr = JsonConvert.DeserializeObject<HistoryResponse>(responseFromServer);

                    if (hr != null && hr.lstGetCustomerFlightFromTo != null)
                    {
                        var results = from a in hr.lstGetCustomerFlightFromTo
                                      group a by a.FL_NO.ToString().Substring(0, 2) into newGroup
                                      select new { fl_no = newGroup.Key, fl_no_cnt = newGroup.Count() };

                        airpref = results.ToList().OrderByDescending(e => e.fl_no_cnt).FirstOrDefault().fl_no.ToString();
                    }
                    else
                    {
                        airpref = "AI";
                    }
                    //var results = hr.lstGetCustomerFlightFromTo.GroupBy(x => x.FL_NO);






                }
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }





            return airpref;
        }
        public string InputCard()
        {
            List<Body> body = new List<Body>
           {
                new Body
                {
                      type= "TextBlock",
                      text= "UserId"
                },
                new Body  {
            type= "Input.Text",
            id="LoginID",
            placeholder= "enter userid",
           
            },
            new Body
                {
                      type= "TextBlock",
                      text= "Password"
                },
                 new Body  {
            type= "Input.Text",
            id="Password",
            placeholder= "enter password",

            },

    };
            List<Actions> actions = new List<Actions>
            { 
                new Actions
                {
                    type="Action.Submit",
                    title="LOGIN"

                }

            };


            var jsonObj = new FlightItinerary
            {
                version = "1.0",
                type = "AdaptiveCard",
                speak = "Your flight is confirmed for you and 3 other passengers from San Francisco to Amsterdam on Friday, October 10 8:30 AM",
                body = body,
                actions=actions
            };








            return JsonConvert.SerializeObject(jsonObj, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public static  Attachment getText(string text)
        {

            List<Body> body = new List<Body>
           {
                new Body
                {
                      type= "TextBlock",
                      text= text
                },
               

    };
           

            var jsonObj = new FlightItinerary
            {
                version = "1.0",
                type = "AdaptiveCard",
                speak = "Your flight is confirmed for you and 3 other passengers from San Francisco to Amsterdam on Friday, October 10 8:30 AM",
                body = body
               
            };








           var Text=  JsonConvert.SerializeObject(jsonObj, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            Attachment attachment = new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = AdaptiveCard.FromJson(Text).Card
            };

            return attachment;
        }
    }
}
