
using Newtonsoft.Json;
using System.Collections.Generic;

using System.Linq;
using System;
using BirdResAWSBot.SBT.Request.Response;
using Microsoft.Bot.Schema;
using Newtonsoft.Json.Linq;
using BirdResAWSBot;
using AdaptiveCards;
using BirdResMSBot;

namespace EchoBot
{


    public class Item
    {
        public string type { get; set; }
        public string text { get; set; }
        public bool isSubtle { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string spacing { get; set; }
        public string url { get; set; }
        public string horizontalAlignment { get; set; }
        public string weight { get; set; }
        public List<Actions> actions { get; set; }
    }

    public class Column
    {
        public string type { get; set; }
        public object width { get; set; }
        public List<Item> items { get; set; }
        public string spacing { get; internal set; }
    }

    public class Body
    {
        public string type { get; set; }
        public string text { get; set; }
        public string weight { get; set; }
        public bool isSubtle { get; set; }
        public bool? separator { get; set; }
        public string spacing { get; set; }
        public string horizontalAlignment { get; set; }
        public List<Column> columns { get; set; }
        public string id { get; set; }
        public string placeholder { get; set; }

        public string width { get; internal set; }
        public List<Item> items { get; set; }
    }

    public class FlightItinerary
    {
        public string version { get; set; }
        public string type { get; set; }
        public string speak { get; set; }
        public List<Body> body { get; set; }
        public List<Actions> actions { get; set; }
        public string width { get; internal set; }
    }

    public class Actions
    {
        public string type { get; set; }
        public string title { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string code1 { get; set; }
        public string email { get; set; }


    }

    public class QueryResponse
    {
        public dynamic CreateAdaptiveCardFromJson(JObject j, Query query)
        {
            dynamic y = new QResult().QueriResult(j, query, EmptyBot.DeviceId);
           
            List<Attachment> attachments = new List<Attachment>();
            if (!(y is string))
            {
                List<int> lstDistinctFlights = (y as List<CustomFlightDetailModel>).Select(x => x.Group).Distinct().ToList();
                foreach(var yy in lstDistinctFlights)
                {
                    List<CustomFlightDetailModel> lst = (y as List<CustomFlightDetailModel>).Where(x => x.Group == yy).ToList();
                    attachments.Add(new Attachment
                    {
                        ContentType = AdaptiveCard.ContentType,
                        Content = AdaptiveCard.FromJson(GiveJson.GetModifiedJson(lst)).Card
                    });
                }
                return attachments;
            }
            else
            {
                return y.ToString();
            }
        }

        //------------------------------------------
        //public List<Attachment> CreateAdaptiveCardFromJsonModified(JObject j, Query query)
        //{
        //    var y = new Flight_Availability().AvailableFlights(j);
        //    List<int> lstDistinctFlights = y.Select(x => x.Group).Distinct().ToList();
        //    List<Attachment> attachments = new List<Attachment>();
        //    foreach (int i in lstDistinctFlights)
        //    {
        //        List<CustomFlightDetailModel> lst = y.Where(x => x.Group == i).ToList();
        //        if (lst.Count() > 0)
        //        {

        //            attachments.Add(new Attachment
        //            {
        //                ContentType = AdaptiveCard.ContentType,
        //                Content = AdaptiveCard.FromJson(GiveJson.GetModifiedJson(lst)).Card
        //            });
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
        //    return attachments;

        //}


    }

  
    public class GiveJson
    {
        public Attachment CreateAdaptiveCardFromJson(JObject j)
        {
            var y = new Flight_Availability().AvailableFlights(j);

            if (y.Count() > 0)
            {

                return new Attachment
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = AdaptiveCard.FromJson(GetJson(y)).Card
                };
            }
            else
            {
                return null;
            }
        }
        public List<Attachment> CreateAdaptiveCardFromJsonModified(JObject j)
        {
            var y = new Flight_Availability().AvailableFlights(j);
            List<int> lstDistinctFlights = y.Select(x => x.Group).Distinct().ToList();
            List<Attachment> attachments = new List<Attachment>();
            foreach (int i in lstDistinctFlights)
            {
                List<CustomFlightDetailModel> lst = y.Where(x => x.Group == i).ToList();
                if (lst.Count() > 0)
                {

                    attachments.Add(new Attachment
                    {
                        ContentType = AdaptiveCard.ContentType,
                        Content = AdaptiveCard.FromJson(GetModifiedJson(lst)).Card
                    });
                }
                else
                {
                    continue;
                }
            }
            return attachments;

        }

        public static string GetJson(List<CustomFlightDetailModel> details)
        {
            List<int> lstDistinctFlights = details.Select(x => x.Group).Distinct().ToList();
            string Result = string.Empty;
            List<Body> body1 = new List<Body>();


            foreach (int i in lstDistinctFlights)
            {
                List<CustomFlightDetailModel> lst = details.Where(x => x.Group == i).ToList();
                string codes = "";
                foreach (var y in lst)
                {
                    codes += y.FlightNumber + ",";
                }
                codes.Remove(codes.LastIndexOf(","));
                int iSegment = 0;
                foreach (var d in lst)
                {
                    iSegment++;
                    if (iSegment > 1)
                    {
                        body1.Add(new Body
                        {
                            type = "ColumnSet",
                            separator = false,
                            columns = new List<Column>
                 {
                    new Column
                    {
                        type="Column",
                        width="stretch",
                        items=new List<Item>
                        {
                            new Item
                             {
                        type= "TextBlock",
                       text= d.DepAirport,
                         isSubtle= true
                         },
                       new Item     {
              type= "TextBlock",
              size= "extraLarge",
              color= "accent",
              text= d.OLocCode,
              spacing= "none"
            }
                       }
                    },
                    new Column
                    {type="Column",
                     width="stretch",
                     items=new List<Item>
                     {
                         new Item
                         {
                         type="TextBlock",
                          text= d.AirLineCode+d.FlightNumber,
                          size="small"
                             },
                         new Item
                         {

                             type= "Image",
                             url="https://online-corporate-traveller.amadeus.com/live/V17_2_0/CT/images/Flights/Logos/"+d.ImageUrl ,//"http://adaptivecards.io/content/airplane.png"
                              size="small",
                             spacing="none"
                         }
                     }

                    },
                    new Column
                    {type="Column",
                     width="stretch",
                     items=new List<Item>
                     {
                         new Item
                         {
                             type="TextBlock",
                         horizontalAlignment= "right",
                           text=d.ArrivalAirport,
                           isSubtle= true
                         },
                         new Item
                         {
                              type= "TextBlock",
                         horizontalAlignment= "right",
                            size= "extraLarge",
                           color= "accent",
                             text=d.ALocCode,
                              spacing= "none"
                         }
                     }


                    }
                 }
                        });
                        body1.Add(new Body
                        {
                            type = "ColumnSet",
                            separator = false,
                            columns = new List<Column>
                    {
                        new Column
                        {type="Column",
                         width=1,
                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                                text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },


                         },


                        },
                        new Column
                        {
                        type="Column",
                         width=1,
                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                           horizontalAlignment="right",
                           text = "Arrival  : " + DateTime.ParseExact(d.ArrivalDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.ArrivalTime,
                             }
                         }
                        }
                    },


                        });

                    }
                    else
                    {
                        body1.Add(new Body
                        {
                            type = "ColumnSet",
                            separator = false,
                            columns = new List<Column>
                 {
                    new Column
                    {
                        type="Column",
                        width="stretch",
                        items=new List<Item>
                        {
                            new Item
                             {
                        type= "TextBlock",
                       text= d.DepAirport,
                         isSubtle= true
                         },
                       new Item     {
              type= "TextBlock",
              size= "extraLarge",
              color= "accent",
              text= d.OLocCode,
              spacing= "none"
            }
                       }
                    },
                    new Column
                    {type="Column",
                     width="stretch",
                     items=new List<Item>
                     {
                         new Item
                         {
                         type="TextBlock",
                          text= d.AirLineCode+d.FlightNumber,
                          size="small"
                             },
                         new Item
                         {

                             type= "Image",
                             url="https://online-corporate-traveller.amadeus.com/live/V17_2_0/CT/images/Flights/Logos/"+d.ImageUrl ,//"http://adaptivecards.io/content/airplane.png"
                              size="small",
                             spacing="none"
                         }
                     }

                    },
                    new Column
                    {type="Column",
                     width="stretch",
                     items=new List<Item>
                     {
                         new Item
                         {
                             type="TextBlock",
                         horizontalAlignment= "right",
                           text=d.ArrivalAirport,
                           isSubtle= true
                         },
                         new Item
                         {
                              type= "TextBlock",
                         horizontalAlignment= "right",
                            size= "extraLarge",
                           color= "accent",
                             text=d.ALocCode,
                              spacing= "none"
                         }
                     }


                    }
                 }
                        });
                        body1.Add(new Body
                        {
                            type = "ColumnSet",
                            separator = false,
                            columns = new List<Column>
                    {
                        new Column
                        {type="Column",
                         width=1,
                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                                text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },


                         },


                        },
                        new Column
                        {
                        type="Column",
                         width=1,
                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                           horizontalAlignment="right",
                           text = "Arrival  : " + DateTime.ParseExact(d.ArrivalDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.ArrivalTime,
                             }
                         }
                        }
                    },


                        });

                    }

                }
                body1.Add(new Body
                {
                    type = "ColumnSet",
                    spacing = "medium",
                    columns = new List<Column>
                 {
                     new Column
                     {
                         type="Column",
                         width=1,
                         items=new List<Item>
                         {
                             new Item
                             {
                                  type= "TextBlock",
                                   text= "Total",
                                  size="medium",
                                  isSubtle= true
                             },

                         }


                     },
                     new Column
                     {
                      type="Column",
                         width=1,
                         items=new List<Item>
                         {
                             new Item
                             {
                            type= "TextBlock",
                            horizontalAlignment= "right",
                            text=lst.FirstOrDefault().Currency+" " +lst.FirstOrDefault().FlightFare,
                            size="medium"

                             }
                         }
                     }
                 }

                });
                body1.Add(new Body
                {
                    type = "Container",
                    items = new List<Item>
                    {
                        new Item
                        {
                            type="ActionSet",
                            horizontalAlignment= "right",
                              actions=new List<Actions>
                              {
                                  new Actions
                                  {
                                      type="Action.Submit",
                                      title="Book",
                                      data=new Data
                                      {
                                          code1= codes
                                      }

                                  }
                              }

                        }
                    }

                    //    columns = new List<Column>
                    //   {
                    //       new Column
                    //       {
                    //           type="Column",
                    //     width="stretch",

                    //     items=new List<Item>
                    //     {
                    //         new Item
                    //         {
                    //             type="ActionSet",
                    //         horizontalAlignment= "right",
                    //         text="Book",
                    //          actions=new List<Actions>
                    //          {
                    //              new Actions
                    //              {
                    //                  type="Action.Submit",
                    //                  title="Book",
                    //                  data=new Data
                    //                  {
                    //                      code1= codes
                    //                  }

                    //              }
                    //          }

                    //         },
                    //       }
                    //   }
                    //}
                });
                body1.Add(new Body
                {
                    type = "TextBlock",
                    separator = true,
                    text = "  ",

                });
            }



            //foreach (var d in details)
            //{

            //    body1.Add(new Body
            //    {
            //        type = "ColumnSet",
            //        separator = false,
            //        columns = new List<Column>
            //     {
            //        new Column
            //        {
            //            type="Column",
            //            width="stretch",
            //            items=new List<Item>
            //            {
            //                new Item
            //                 {
            //            type= "TextBlock",
            //           text= d.DepAirport,
            //             isSubtle= true
            //             },
            //           new Item     {
            //  type= "TextBlock",
            //  size= "extraLarge",
            //  color= "accent",
            //  text= d.OLocCode,
            //  spacing= "none"
            //}
            //           }
            //        },
            //        new Column
            //        {type="Column",
            //         width="stretch",
            //         items=new List<Item>
            //         {
            //             new Item
            //             {
            //             type="TextBlock",
            //              text= d.AirLineCode+d.FlightNumber,
            //              size="small"
            //                 },
            //             new Item
            //             {

            //                 type= "Image",
            //                 url="https://online-corporate-traveller.amadeus.com/live/V17_2_0/CT/images/Flights/Logos/"+d.ImageUrl ,//"http://adaptivecards.io/content/airplane.png"
            //                  size="small",
            //                 spacing="none"
            //             }
            //         }

            //        },
            //        new Column
            //        {type="Column",
            //         width="stretch",
            //         items=new List<Item>
            //         {
            //             new Item
            //             {
            //                 type="TextBlock",
            //             horizontalAlignment= "right",
            //               text=d.ArrivalAirport,
            //               isSubtle= true
            //             },
            //             new Item
            //             {
            //                  type= "TextBlock",
            //             horizontalAlignment= "right",
            //                size= "extraLarge",
            //               color= "accent",
            //                 text=d.ALocCode,
            //                  spacing= "none"
            //             }
            //         }


            //        }
            //     }
            //    });

            //    body1.Add(new Body
            //    {
            //        type = "ColumnSet",
            //        separator = false,
            //        columns = new List<Column>
            //        {
            //            new Column
            //            {type="Column",
            //             width=1,
            //             items=new List<Item>
            //             {
            //                 new Item
            //                 {type="TextBlock",
            //                    text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
            //                 },


            //             },


            //            },
            //            new Column
            //            {
            //            type="Column",
            //             width=1,
            //             items=new List<Item>
            //             {
            //                 new Item
            //                 {type="TextBlock",
            //               horizontalAlignment="right",
            //               text = "Arrival  : " + DateTime.ParseExact(d.ArrivalDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.ArrivalTime,
            //                 }
            //             }
            //            }
            //        },


            //    });
            //    body1.Add(new Body
            //    {
            //        type = "ColumnSet",
            //        spacing = "medium",
            //        columns = new List<Column>
            //     {
            //         new Column
            //         {
            //             type="Column",
            //             width=1,
            //             items=new List<Item>
            //             {
            //                 new Item
            //                 {
            //                      type= "TextBlock",
            //                       text= "Total",
            //                      size="medium",
            //                      isSubtle= true
            //                 },

            //             }


            //         },
            //         new Column
            //         {
            //          type="Column",
            //             width=1,
            //             items=new List<Item>
            //             {
            //                 new Item
            //                 {
            //                type= "TextBlock",
            //                horizontalAlignment= "right",
            //                text=d.Currency+" " +d.FlightFare,
            //                size="medium"

            //                 }
            //             }
            //         }
            //     }

            //    });
            //    body1.Add(new Body
            //    {
            //        type = "TextBlock",
            //        separator = true,
            //        text = "  ",

            //    });

            //}

            var jsonObj = new FlightItinerary
            {
                version = "1.0",
                type = "AdaptiveCard",
                speak = "Your flight is confirmed for you and 3 other passengers from San Francisco to Amsterdam on Friday, October 10 8:30 AM",
                body = body1
            };


            Result = JsonConvert.SerializeObject(jsonObj, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }

                );





            return Result;
        }



        public static string GetModifiedJson(List<CustomFlightDetailModel> details)
        {
            
            string Result = string.Empty;
            List<Body> body1 = new List<Body>();
            int iSegment = 0;
            foreach (var d in details)
            {
                iSegment++;
                if (iSegment > 1)
                {
                    body1.Add(new Body
                    {
                        type = "ColumnSet",
                   
                        columns = new List<Column>
                 {
                    new Column
                    {
                        type="Column",
                        width="stretch",
                        items=new List<Item>
                        {
                            new Item
                             {
                        type= "TextBlock",
                       text= d.AirlineName+" | "+d.AirLineCode+d.FlightNumber,
                         isSubtle= true
                         }
                      
                       }
                    },
         
                  
                 }
                    });
                    body1.Add(new Body
                    {
                        type = "ColumnSet",
                        separator = false,
                        columns = new List<Column>
                    {
                        new Column
                        {type="Column",
                          width="auto",
                          items=new List<Item>
                         {
                             new Item
                             {type="Image",
                        url="https://online-corporate-traveller.amadeus.com/live/V17_2_0/CT/images/Flights/Logos/"+d.ImageUrl ,//"http://adaptivecards.io/content/airplane.png"
                        size="Small"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },


                         },


                        },
                         new Column
                        {type="Column",
                        
                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                              text=d.DepartureTime,
                               color="default",
                              weight="bolder",
                               size="Large"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                              new Item
                             {type="TextBlock",
                              text=d.DepAirport,
                        size="Small"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                                   new Item
                             {type="TextBlock",
                              text=DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM"),
                        size="Small",
                        
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },

                         },


                        },
                             new Column
                        {type="Column",
                         
                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                              text=d.Duration.Insert(2,":"),
                              size="Small",
                              horizontalAlignment="center"
                            
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                              new Item
                             {type="Image",
                             url= d.Direction=="I"?"http://203.89.132.9/ChatBot/returnflighticon.svg" : "http://203.89.132.9/ChatBot/flighticon.svg",
                            size="Medium"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                              new Item
                             {type="TextBlock",
                              text=d.Layover==""?"Non Stop":d.Layover,
                        size="small",
                        horizontalAlignment="center"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },

                         },


                        },
                             new Column
                        {type="Column",
                        width="stretch",
                         spacing="Medium",
                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                              text=d.ArrivalTime,
                               color="default",
                              weight="bolder",
                        size="Large"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                              new Item
                             {type="TextBlock",
                              text=d.ArrivalAirport,
                               size="Small",
                               
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                                   new Item
                             {type="TextBlock",
                              text=DateTime.ParseExact(d.ArrivalDate , "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM"),
                        size="Small",
                        
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },

                         },


                        },
                        //     new Column
                        //{type="Column",

                        // items=new List<Item>
                        // {
                        //     new Item
                        //     {type="TextBlock",
                        //      text="      ",
                        //      color="default",
                        //      weight="bolder"
                              
                        //        //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                        //     }


                        // }


                        //}

                    }


                    });

                }
                else
                {  
                    body1.Add(new Body
                    {
                        type = "ColumnSet",

                        columns = new List<Column>
                 {
                    new Column
                    {
                        type="Column",
                        width="stretch",
                        items=new List<Item>
                        {
                            new Item
                             {
                        type= "TextBlock",
                       text= d.AirlineName+" | "+d.AirLineCode+d.FlightNumber,
                         isSubtle= true
                         }

                       }
                    },
                         new Column
                        {type="Column",

                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                              text="",
                              color="default",
                              weight="bolder"
                              
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             }


                         }


                        },
                              new Column
                        {type="Column",
                         spacing="Medium",
                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                              text=d.Currency+" "+d.FlightFare,
                              color="default",
                              weight="bolder",size="Medium",
                              horizontalAlignment="right"
                              
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             }


                         }


                        }


                 }
                    });
                    body1.Add(new Body
                    {
                        type = "ColumnSet",
                        separator = false,
                        columns = new List<Column>
                    {
                        new Column
                        {type="Column",
                         width="auto",
                         items=new List<Item>
                         {
                             new Item
                             {type="Image",
                        url="https://online-corporate-traveller.amadeus.com/live/V17_2_0/CT/images/Flights/Logos/"+d.ImageUrl ,//"http://adaptivecards.io/content/airplane.png"
                        size="Small"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },


                         },


                        },
                         new Column
                        {type="Column",

                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                              text=d.DepartureTime,
                               color="default",
                              weight="bolder",
                               size="Large"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                              new Item
                             {type="TextBlock",
                              text=d.DepAirport,
                        size="Small"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                                new Item
                             {type="TextBlock",
                              text=DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM"),
                        size="Small",
                       
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },

                         },


                        },
                             new Column
                        {type="Column",

                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                              text=d.Duration.Insert(2,":"),
                                size="Small",
                                     horizontalAlignment="center"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                              new Item
                             {type="Image",
                             url= d.Direction=="I"?"http://203.89.132.9/ChatBot/returnflighticon.svg" : "http://203.89.132.9/ChatBot/flighticon.svg",
                            size="Medium"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                              new Item
                             {type="TextBlock",
                              text=d.Layover==""?"Non Stop":d.Layover,
                        size="small",
                        horizontalAlignment="center"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },

                         },


                        },
                             new Column
                        {type="Column",
                        width="stretch",
                         spacing="Medium",
                         items=new List<Item>
                         {
                             new Item
                             {type="TextBlock",
                              text=d.ArrivalTime,
                               color="default",
                              weight="bolder",
                        size="Large"
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                              new Item
                             {type="TextBlock",
                              text=d.ArrivalAirport,
                        size="Small"

                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                                   new Item
                             {type="TextBlock",
                              text=DateTime.ParseExact(d.ArrivalDate , "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM"),
                               size="Small",
                                 
                                //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                             },
                         },


                        },
                        //     new Column
                        //{type="Column",

                        // items=new List<Item>
                        // {
                        //     new Item
                        //     {type="TextBlock",
                        //      text=d.Currency+" "+d.FlightFare,
                        //      color="default",
                        //      weight="bolder"
                              
                        //        //text = "Departure  :" + DateTime.ParseExact(d.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM") + " " + d.DepartureTime,
                        //     }


                        // }


                        //}

                    }


                    });

                }
            }
            string codes = "";
            foreach (var y in details)
            {
                codes += y.AirLineCode + "-"+y.FlightNumber + ",";
            }
            codes = codes.Remove(codes.LastIndexOf(","));
            var actions = new List<Actions>
            {
                new Actions
                {
                    type="Action.Submit",
                    title="Book",
                    data=new Data{
                        code1=codes,
                        email=new EmptyBot().GiveEmployee().Email
                    }
                }

            };


            var jsonObj = new FlightItinerary
            {
                version = "1.0",
                width= "stretch",
                type = "AdaptiveCard",
                speak = "Your flight is confirmed for you and 3 other passengers from San Francisco to Amsterdam on Friday, October 10 8:30 AM",
                body = body1,
                actions=actions
            };


            Result = JsonConvert.SerializeObject(jsonObj, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }

                );


            return Result;
        }


    }


}