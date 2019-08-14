using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBot
{
    public class ACode
    {
        public static List<ACode> ACodes=new List<ACode>();
        //public ACode()
        //{
        //    var json = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + @"/cities.json");
        //    ACodes = JsonConvert.DeserializeObject<List<ACode>>(json);


        //}
        public string City { get; set; }
        public string Code { get; set; }
 //       public static List<ACode> ACodes=new List<ACode>
 //       {
 //           new ACode
 //           {
 //               City="Mumbai",
 //               Code="BOM"
 //           },
 //            new ACode
 //           {
 //               City="Delhi",
 //               Code="DEL"
 //           },
 //             new ACode
 //           {
 //               City="Hydrabad",
 //               Code="HYD"
 //           },
 //              new ACode
 //           {
 //               City="Chennai",
 //               Code="MAA"
 //           },
 //               new ACode
 //           {
 //               City="Goa",
 //               Code="GOI"
 //           },
 //            new ACode
 //           {
 //               City="Cambridge",
 //               Code="CBG"
 //           },
 //          new ACode
 //           {
 //               City="London",
 //               Code="LCY"
 //           },
 //            new ACode
 //           {
 //               City="Manchester",
 //               Code="ZMP"
 //           },
 //               new ACode
 //           {
 //               City="Perth",
 //               Code="ZXP"
 //           },
 //             new ACode
 //           {
 //               City="Atlanta",
 //               Code="ATL"
 //           },
 //              new ACode
 //           {
 //               City="New York",
 //               Code="JFK"
 //           }
 //              ,
 //              new ACode
 //           {
 //               City="Banglore",
 //               Code="BLR"
 //           }
 //,
 //              new ACode
 //           {
 //               City="Trivandrum",
 //               Code="TRV"
 //           }
 //               ,
 //              new ACode
 //           {
 //               City="Kanpur",
 //               Code="KNU"
 //           }
 //                       ,
 //              new ACode
 //           {
 //               City="Agra",
 //               Code="AGR"
 //           }
 //                          ,
 //              new ACode
 //           {
 //               City="bareilly",
 //               Code="BEK"
 //           }
 //                                 ,
 //              new ACode
 //           {
 //               City="Kolkata",
 //               Code="CCU"
 //           },
 //              new ACode
 //              {
 //                  City="Mysore",
 //                  Code="MYQ"
 //              },
 //             new ACode
 //              {
 //                  City="Pondicherry ",
 //                  Code="PNY"
 //              },
 //             new ACode
 //              {
 //                  City="Calicut",
 //                  Code="CCJ"
 //              } ,
 //             new ACode
 //              {
 //                  City="Bellary",
 //                  Code="BEP"
 //              } ,
 //          new ACode
 //              {
 //                  City="Vijayawada",
 //                  Code="VGA"
 //              }


 //       };

        public static string GetAirPortCodeByCity(string City)
        {

            var json = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + @"/cities.json");
            ACodes = JsonConvert.DeserializeObject<List<ACode>>(json);


            string Code = string.Empty;


            Code = ACodes.Where(x => x.City.ToLower().Contains(City.ToLower())).FirstOrDefault().Code;
                
             
                



            return Code;
        }
        public static string GetCityByAirPortCode(string Code)
        {



            string City = string.Empty;


            var CityColl = ACodes.Where(x => x.Code.ToLower() == Code.ToLower());
            if (CityColl.FirstOrDefault() != null)
            {
                City = CityColl.FirstOrDefault().City;
            }
            else
            {
                City = Code;
            }
          




            return City;
        }

    }
}
