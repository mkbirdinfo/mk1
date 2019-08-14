// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveCards;
using BirdResAWSBot;
using BirdResAWSBot.SBT;
using BirdResAWSBot.SBT.Request;
using BirdResAWSBot.SBT.Request.Response;
using BirdResMSBot.Controllers;
using EchoBot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Schema;
using Newtonsoft.Json.Linq;
using static EchoBot.GiveJson;

namespace BirdResMSBot
{
    public class EmptyBot:IBot 
    {
       public static string  DeviceId;
        public static readonly MemoryStorage _myStorage = new MemoryStorage();
        private LuisRecognizer Recognizer { get; } = null;
        public  List<CustomFlightDetailModel> ulog { get; set; }
        public string flag { get; set; }
        public EmptyBot()
        {

        }
        public EmptyBot(LuisRecognizer luisRecognizer)
        {
            Recognizer = luisRecognizer;
        }
        //protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        //{

        //}

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {




            //  DeviceId = turnContext.Activity.From.Id;


            RecognizerResult re =new RecognizerResult();
           

            EmployeeModel ems = new EmptyBot().GiveEmployee();
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                DeviceId = turnContext.Activity.From.Id.Substring(0, 36);
               // await turnContext.SendActivityAsync(DeviceId);
                var recognizerResult = await this.Recognizer.RecognizeAsync(turnContext, cancellationToken);
                var topIntent = recognizerResult?.GetTopScoringIntent();
                string strIntent = (topIntent != null) ? topIntent.Value.intent : "";
                double dblIntentScore = (topIntent != null) ? topIntent.Value.score : 0.0;
                re = recognizerResult;
                if (turnContext.Activity.Value != null)
                {
                    JObject data = turnContext.Activity.Value as JObject;
                    if (data.GetValue("code1") != null)
                    {
                        string[] codes = data.GetValue("code1").ToString().Split(",");

                       // await turnContext.SendActivityAsync(data.GetValue("code1").ToString());
                       


                        if (ems.Allow_ticketing == "FALSE")
                        {
                            await turnContext.SendActivityAsync("You are not able to create booking");
                        }
                        else
                        {



                        
                                await turnContext.SendActivityAsync(new RequestToBookApi().BookFlight(recognizerResult.Entities, codes));
                         


                        }
                    }
                    else if (data.GetValue("LoginID", StringComparison.CurrentCulture) != null)
                    {
                        //JObject login = turnContext.Activity.Value as JObject;


                        await turnContext.SendActivityAsync(new LoginIntent().Login(data.GetValue("LoginID", StringComparison.CurrentCulture).ToString(), data.GetValue("Password", StringComparison.CurrentCulture).ToString(), DeviceId, ""));

                    }
                }
              
                else
                {


                    string LoginId = turnContext.Activity.From.Id.Substring(turnContext.Activity.From.Id.IndexOf("|") + 1, turnContext.Activity.From.Id.LastIndexOf("|") - (turnContext.Activity.From.Id.IndexOf("|") + 1));
                    string Password = turnContext.Activity.From.Id.Substring(turnContext.Activity.From.Id.LastIndexOf("|") + 1);
                    new LoginIntent().Login(LoginId, Password, DeviceId, "");
                    List<CustomFlightDetailModel> cfdm = new EmptyBot().GetIteneraries();

                    switch (strIntent)
                    {


                        case "Welcome":
                            Random rm = new Random();
                            int num = rm.Next(1, 8);
                            EmployeeModel e = new EmptyBot().GiveEmployee();
                            switch (num)
                            {
                                case 1:
                                    await turnContext.SendActivityAsync(" How can I help you today?");
                                    break;
                                case 2:
                                    await turnContext.SendActivityAsync("Hello");
                                    break;
                                case 8:
                                    await turnContext.SendActivityAsync(DateTime.Now.Hour < 12 && DateTime.Now.Hour > 5 ? "Good morning" : DateTime.Now.Hour > 16 ? "Good evening" : "Good afternoon");
                                    break;
                                case 7:
                                    await turnContext.SendActivityAsync("Welcome " + e.FirstName);
                                    break;
                                case 4:
                                    await turnContext.SendActivityAsync("Namaste!! What can I assist you with today?");
                                    break;
                                case 3:
                                    await turnContext.SendActivityAsync("How can I assist you today?");
                                    break;
                                case 5:
                                    await turnContext.SendActivityAsync("Hi " + e.FirstName);
                                    break;
                                case 6:
                                    await turnContext.SendActivityAsync("Hello " + e.FirstName);
                                    break;
                                default:
                                    await turnContext.SendActivityAsync("hope you are having a great day? How can I assist you today?");
                                    break;


                            }




                            break;
                        case "Flight_availability":

                            //await turnContext.SendActivityAsync(DeviceId);
                            //await turnContext.SendActivityAsync(LoginId);
                            //await turnContext.SendActivityAsync(Password);
                            EmployeeModel em = GiveEmployee();
                            if (em.Email == null)
                            {
                                var inputcard = turnContext.Activity.CreateReply();
                                var inputjson = new LoginIntent().InputCard();
                                 //var inputjson = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + @"/simplecard.json");
                                Attachment attachment = new Attachment
                                {
                                    ContentType = AdaptiveCard.ContentType,
                                    Content = AdaptiveCard.FromJson(inputjson).Card
                                };
                                inputcard.Attachments = new List<Attachment>()
                                {attachment };
                                await turnContext.SendActivityAsync("please login to proceed..");
                                await turnContext.SendActivityAsync(inputcard);
                            }
                            else
                            {


                                await turnContext.SendActivityAsync("Search is in progress...");
                                var response = turnContext.Activity.CreateReply();
                                var x = new GiveJson().CreateAdaptiveCardFromJsonModified(recognizerResult.Entities);
                                response.Attachments = x;
                                if (x != null)
                                {
                                    Random rm1 = new Random();
                                    int num1 = rm1.Next(1, 6);
                                    switch (num1)
                                    {
                                        case 1:
                                            await turnContext.SendActivityAsync("Here is the snapshot");
                                            break;
                                        case 2:
                                            await turnContext.SendActivityAsync("Hi " + em.FirstName + ", as per policy the lowest fare is available");
                                            break;
                                        case 3:
                                            await turnContext.SendActivityAsync("Certainly " + em.FirstName + ", Here are the details");
                                            break;
                                        case 4:
                                            await turnContext.SendActivityAsync("Sharing results for flights ");
                                            break;
                                        case 5:
                                            await turnContext.SendActivityAsync("Sure, Here you are!!Please select the flight to book");
                                            break;
                                        case 6:
                                            await turnContext.SendActivityAsync("Hi" + em.FirstName + ", you have booked for " + recognizerResult.Entities.GetValue("City").First + " to " + recognizerResult.Entities.GetValue("City").Last + ". Here are the flight details");
                                            break;


                                    }


                                    await turnContext.SendActivityAsync(response);
                                    //await turnContext.SendActivityAsync("would you like to search for other flights??");
                                    var myitem = new UtteranceLog();




                                    myitem.UtteranceList.AddRange(PIT.GiveItenerary());


                                    IDictionary<string, object> changes = new Dictionary<string, object>();
                                    {
                                        changes.Add(DeviceId + "pricedIteneraries", myitem);
                                    }
                                    await _myStorage.WriteAsync(changes, cancellationToken);
                                  //  await turnContext.SendActivityAsync(DeviceId);

                                }
                                else
                                {
                                    await turnContext.SendActivityAsync("Sorry no flights are available");
                                }

                            }
                            break;
                        case "Flight_details":


                            if (cfdm.Count > 0)
                            {
                                await turnContext.SendActivityAsync(new FlightDetails().GetFlightDetails(recognizerResult.Entities));
                            }
                            else
                            {
                                await turnContext.SendActivityAsync("Please seacrh for available flights to see details");
                            }
                            break;
                        case "mail":

                            if (cfdm.Count > 0)
                            {
                                await turnContext.SendActivityAsync(new SendMail().Mail(recognizerResult.Entities));
                            }
                            else
                            {
                                await turnContext.SendActivityAsync("Please seacrh for available flights to see details");
                            }

                            break;
                        case "Fares":

                            if (cfdm.Count > 0)
                            {
                                await turnContext.SendActivityAsync(new SendMail().FareDetail(recognizerResult.Entities));
                            }
                            else
                            {
                                await turnContext.SendActivityAsync("Please seacrh for available flights to see fares");
                            }
                            break;

                        case "Queries":
                            EmployeeModel em2 = GiveEmployee();
                            if (em2.Email == null)
                            {
                                var inputcard = turnContext.Activity.CreateReply();
                                var inputjson = new LoginIntent().InputCard();
                                Attachment attachment = new Attachment
                                {
                                    ContentType = AdaptiveCard.ContentType,
                                    Content = AdaptiveCard.FromJson(inputjson).Card
                                };
                                inputcard.Attachments = new List<Attachment>()
                                {attachment };
                                await turnContext.SendActivityAsync("please login to proceed..");
                                await turnContext.SendActivityAsync(inputcard);
                            }
                            else
                            {


                                Query Query = new Query();
                                string[] str1 = new string[] { DeviceId + "query" };
                                var temp1 = _myStorage.ReadAsync<UtteranceLog>(str1);
                                if (temp1.Result.Count > 0)
                                {
                                    Query = temp1.Result[DeviceId + "query"].query;
                                }
                                var qresponse = turnContext.Activity.CreateReply();
                                var r = new QueryResponse().CreateAdaptiveCardFromJson(recognizerResult.Entities, Query);

                                if (r != null && !(r is List<Attachment>))
                                {
                                    await turnContext.SendActivityAsync(r);
                                }

                                if (r != null && (r is List<Attachment>))
                                {
                                    qresponse.Attachments = r;

                                    Random rm1 = new Random();
                                    int num1 = rm1.Next(1, 6);
                                    switch (num1)
                                    {
                                        case 1:
                                            await turnContext.SendActivityAsync("Here is the snapshot");
                                            break;
                                        case 2:
                                            await turnContext.SendActivityAsync("Hi " + ems.FirstName + ", as per policy the lowest fare is available");
                                            break;
                                        case 3:
                                            await turnContext.SendActivityAsync("Certainly " + ems.FirstName + ", Here are the details");
                                            break;
                                        case 4:
                                            await turnContext.SendActivityAsync("Sharing results for flights ");
                                            break;
                                        case 5:
                                            await turnContext.SendActivityAsync("Sure, Here you are!!Please select the flight to book");
                                            break;
                                        case 6:
                                            await turnContext.SendActivityAsync("Hi" + ems.FirstName + ", you have booked for " + recognizerResult.Entities.GetValue("City").First + " to " + recognizerResult.Entities.GetValue("City").Last + ". Here are the flight details");
                                            break;


                                    }
                                    await turnContext.SendActivityAsync(qresponse);

                                    //await turnContext.SendActivityAsync("would you like to search for other flights??");
                                    var myitem = new UtteranceLog();




                                    myitem.UtteranceList.AddRange(PIT.GiveItenerary());


                                    IDictionary<string, object> changes = new Dictionary<string, object>();
                                    {
                                        changes.Add(DeviceId + "pricedIteneraries", myitem);
                                    }
                                    await _myStorage.WriteAsync(changes, cancellationToken);

                                }

                                if (r == null)
                                {
                                    await turnContext.SendActivityAsync("Sorry no flights are available");
                                }
                            }
                            break;
                        case "BookFlight":

                            if (cfdm.Count > 0)
                            {
                                EmployeeModel em1 = GiveEmployee();

                                if (em1.Allow_ticketing == "FALSE")
                                {
                                    await turnContext.SendActivityAsync("You are not able to create booking");
                                }
                                else
                                {
                                    if (recognizerResult.Entities.GetValue("FlightNumber") != null)
                                    {


                                        await turnContext.SendActivityAsync(new RequestToBookApi().BookFlight(recognizerResult.Entities, new string[] { }));
                                    }
                                    else
                                    {
                                        await turnContext.SendActivityAsync("Please select flight");
                                    }

                                }

                            }
                            else
                            {
                                await turnContext.SendActivityAsync("Please seacrh for available flights to book flights");
                            }

                            break;
                        case "CustomFlights":

                            var res = turnContext.Activity.CreateReply();
                            res.Attachments = new List<Attachment>{
                                new FlightDetails().Customflights(recognizerResult.Entities)
                                };
                            await turnContext.SendActivityAsync(res);

                            break;
                        default:
                            await turnContext.SendActivityAsync("Sorry i did not understand");
                            break;



                    }
                }


               
            }
            //else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate&& turnContext.Activity.MembersAdded.Count==1)
            //{
            //    await turnContext.SendActivityAsync("Hello!! I am SPARROW, a virtual travel assistant. How can I help you today?"+ DeviceId);


            //    //try
            //    //{
            //    //    await turnContext.SendActivityAsync("Hello!! I am SPARROW, a virtual travel assistant. How can I help you today?");
            //    //    WebRequest request = WebRequest.Create("http://online-corporate-traveller.amadeus.com/live/V17_2_0/webapi/api/SBTMobile/FlightSearch");
            //    //    request.Method = "POST";
            //    //    request.ContentType = "application/json";
            //    //    Stream dataStream = request.GetRequestStream();

            //    //    // getJson(date, direct, type, quantity, origin, desti);
            //    //    // JObject abc = new SearchRequest().GetJson();
            //    //    new LoginIntent().Login(re.Entities);
            //    //    EmployeeModel em = new EmptyBot().GiveEmployee();
            //    //    byte[] byteArray = Encoding.UTF8.GetBytes(new SearchRequest().GetJson("DEL", "BOM", DateTime.Now.AddDays(3), "0", "1",em.TokenID, "AI"));
            //    //    request.ContentLength = byteArray.Length;
            //    //    dataStream.Write(byteArray, 0, byteArray.Length);
            //    //    dataStream.Close();
            //    //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //    //    using (Stream receiveStream = Flight_Availability.GetStreamForResponse(response))
            //    //    {
            //    //        StreamReader sr = new StreamReader(receiveStream);
            //    //        string responseFromServer = sr.ReadToEnd();
            //    //        await turnContext.SendActivityAsync(responseFromServer);
            //    //    }

            //    //    dataStream.Close();
            //    //    response.Close();
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    throw ex;
            //    //}

            //}

            else if(turnContext.Activity.Type==ActivityTypes.Event)
            {
                await turnContext.SendActivityAsync("Hello!! I am SPARROW, a virtual travel assistant.");


            }
           
            }

        

    public    EmployeeModel GiveEmployee()
        {
          
            EmployeeModel em = new EmployeeModel();
            string[] str1 = new string[] { DeviceId+"employee" };

            var temp1 = _myStorage.ReadAsync<UtteranceLog>(str1);
            if (temp1.Result.Count > 0)
            {
                em = temp1.Result[DeviceId+"employee"].employee;
            }
            return em;
        }

   public List<CustomFlightDetailModel> GetIteneraries()
        {
            string[] str = new string[] { DeviceId+"pricedIteneraries" };
            var temp = _myStorage.ReadAsync<UtteranceLog>(str);
            if (temp.Result.Count > 0)
            {
                ulog = temp.Result[DeviceId+"pricedIteneraries"].UtteranceList;
            }
            return ulog;
        }

    }
}
