using BirdResAWSBot.SBT.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BirdResAWSBot.SBT
{
 public   class BookingJsons
    {
        
        public string GetSubmitBookRequest(List<CustomFlightDetailModel> detailModel, string custName,string offid,string custid,string token,string DeviceId)
        {
            //string rtdate = detailModel.DepartureDate.Remove(6)+ Convert.ToInt32(detailModel.Return == "" ? "0" : detailModel.Return);


            //new List<FLDETAIL2>
            //                    {
            //                        new FLDETAIL2
            //                        {

            //                ARRDATE= detailModel.ArrivalDate,
            //                FLNO= detailModel.AirLineCode+"-"+detailModel.FlightNumber,
            //                SEGMENTLINENO= "",
            //                SEQNO= "1",
            //                TRRQID= "",
            //                FLFROMNAME= detailModel.DepAirport,
            //                CUCODE= "INR",
            //                DEPTIME= detailModel.DepartureTime.Remove(2,1),
            //                FLTO= detailModel.ALocCode,
            //                SectorRPH= "1",
            //                FLTONAME= detailModel.ArrivalAirport,
            //                DEPDATE= detailModel.DepartureDate,
            //                CACODE= "4",
            //                BASEFARE= detailModel.FlightFare.ToString(),
            //                InPolicy= "0",
            //                RETURN=detailModel.Return==""?"0":"1",
            //                ARRTIME= "2220",
            //                TRTYPE= "I",
            //                TOTALTAX= "21430",
            //                FLFROM=detailModel.OLocCode
            //                        }
            //                    }
            //                },
            //------------------------inbound
            //new List<FLDETAIL>
            //    {
            //        new FLDETAIL
            //        {
            //ARRDATE=detailModel.ArrivalDate ,
            //FLNO= detailModel.FlightNumber,
            //SEGMENTLINENO= "",
            //SEQNO= "1",
            //TRRQID= "",
            //FLFROMNAME= detailModel.DepAirport,
            //CUCODE= "INR",
            //DEPTIME=detailModel.DepartureTime.Remove(2,1),
            //FLTO=detailModel.OLocCode ,
            //SectorRPH= "2",
            //FLTONAME= detailModel.DepAirport,
            //DEPDATE= rtdate,
            //CACODE= "4",
            //BASEFARE= detailModel.FlightFare.ToString(),
            //InPolicy= "0",
            // RETURN=detailModel.Return==""?"0":"1",
            //ARRTIME= "0415",
            //TRTYPE= "I",
            //TOTALTAX= "21430",
            //FLFROM=detailModel.ALocCode
            //        },

            //    },

            var originModel = detailModel.Where(x => x.Direction == "O").FirstOrDefault();
            var  destinationModel = detailModel.Where(x => x.Direction == "O").LastOrDefault();
            var lstOutBoundFlights = new List<FLDETAIL2>();
            var lstInBoundFlights = new List<FLDETAIL>();
            foreach (var it in detailModel)
            {
                if (it.Direction == "O")
                {

                    lstOutBoundFlights.Add(new FLDETAIL2
                    {

                        ARRDATE = it.ArrivalDate,
                        FLNO = it.AirLineCode + "-" + it.FlightNumber,
                        SEGMENTLINENO = "",
                        SEQNO = "1",
                        TRRQID = "",
                        FLFROMNAME = it.DepAirport,
                        CUCODE = "INR",
                        DEPTIME = it.DepartureTime.Remove(2, 1),
                        FLTO = it.ALocCode,
                        SectorRPH = "1",
                        FLTONAME = it.ArrivalAirport,
                        DEPDATE = it.DepartureDate,
                        CACODE = "4",
                        BASEFARE = it.FlightFare.ToString(),
                        InPolicy = "0",
                        RETURN = it.Return == "" ? "0" : "1",
                        ARRTIME = "2220",
                        TRTYPE = "I",
                        TOTALTAX = "21430",
                        FLFROM = it.OLocCode
                    });
                }
                else if(it.Direction == "I")
                {
                    lstInBoundFlights.Add(new FLDETAIL
                    {
                        ARRDATE = it.ArrivalDate,
                        FLNO = it.AirLineCode + "-" + it.FlightNumber,
                        SEGMENTLINENO = "",
                        SEQNO = "1",
                        TRRQID = "",
                        FLFROMNAME = it.DepAirport,
                        CUCODE = "INR",
                        DEPTIME = it.DepartureTime.Remove(2, 1),
                        FLTO = it.OLocCode,
                        SectorRPH = "2",
                        FLTONAME = it.DepAirport,
                        DEPDATE = it.DepartureDate,
                        CACODE = "4",
                        BASEFARE = it.FlightFare.ToString(),
                        InPolicy = "0",
                        RETURN = it.Return == "" ? "0" : "1",
                        ARRTIME = "0415",
                        TRTYPE = "I",
                        TOTALTAX = "21430",
                        FLFROM = it.ALocCode
                    });
                }
            }

            SubmitBookRequest RequestObj = new SubmitBookRequest
            {
                UPADDTRVELREQUESTINPUT = new UPADDTRVELREQUESTINPUT
                {
                    DeviceType = "Windows",
                    ATTENDEES = "",
                    COSTCENTREDIVISIONPURPOSEPROJECT = "FALSE",
                    PositionID = "2306",
                    OFFLINE_FL_DETAILS = new OFFLINEFLDETAILS
                    {

                        FL_TO_NAME = "",
                        APT_TO_NAME = "",
                        TR_RQ_ID = "",
                        FL_TO = "",
                        CA_CODE = "",
                        APT_FROM_NAME = "",
                        PAY_MODE_DESC = "",
                        CA_NAME = "",
                        DEP_TIME = "",
                        TR_TYPE = "",
                        ARR_TIME = "",
                        TR_RETURN = "",
                        FL_FROM_NAME = "",
                        REMARKS = "",
                        DEP_DATE = "",
                        ARR_DATE = "",
                        FL_FROM = "",
                        AMOUNT = "",
                        PAY_MODE = "",
                        CU_CODE = "",




                    },
                    EXPENSEDETAILS = new List<EXPENSEDETAIL>
                    {
                        new EXPENSEDETAIL
                        {
                PAYMODE= "",
                PAYMODEDESC= "",
                CUCODE= "",
                DAYS="",
                SNo= "",
                DESCRIPTION="",
                REGIONNAME= "",
                REGIONID="",
                STATUS= "",
                EXPENSEID= "",
                AMOUNT="",
                AMOUNTPERDAY= "",
                EXPENSENAME=""

                           }
                    },
                    OnlineHotelDetail = new OnlineHotelDetail(),

                    SequenceNumber = new SequenceNumber
                    {
                        SequenceNumber1 = "4",
                        SequenceNumber2 = ""
                    },

                    MaximumPrice = "",
                    OFFLINE_RAIL_DETAILS = new OFFLINERAILDETAILS
                    {
                        RL_FROM_NAME = "",
                        RL_FROM = "",
                        REMARKS = "",
                        ARR_TIME = "",
                        RAIL_CA_CODE = "",
                        DEP_DATE = "",
                        RL_TO_NAME = "",
                        PAY_MODE_DESC = "",
                        RAIL_CA_NAME = "",
                        TR_RETURN = "",
                        ARR_DATE = "",
                        CU_CODE = "",
                        AMOUNT = "",
                        DEP_TIME = "",
                        RL_TO = "",
                        PAY_MODE = ""
                    },
                    INSURANCE = new INSURANCE
                    {
                        INSURED = new List<INSURED>
                        {
                            new INSURED
                            {

                     FIRSTNAME= "",
                    COUNTRY= "",
                    EMAILADDRESS= "",
                    SERVICETAX="",
                    TITLE= "",
                    TOTALCHARGES="",
                    PASSPORTNUMBER= "",
                    TOTALBASECHARGES= "",
                    VIEWDOB= "",
                    PREMIUM= "",
                    MOBILE= "",
                    NOMINEE= "",
                    RPH= "1",
                    PINCODE="",
                    AGE="",
                    ADDRESS1= "",
                    CITY= "",
                    STATE= "",
                    LASTNAME= "",
                    DISTRICT= "",
                    PHONENO= "",
                    RELATION= "",
                    CURRCODE= "",
                    ADDRESS2= "",
                    USERDOB= "",
                    BASECHARGES= ""
                            }
                        },
                        INSURANCEPLAN = new INSURANCEPLAN
                        {
                            PLANNAME = "",
                            DAYS = "",
                            DEPDATE = "",
                            PLANCODE = "",
                            CATEGORYCODE = "",
                            COUNTRYCODE = "",
                            COUNTRYNAME = "",
                            OFID = "",
                            CATEGORYNAME = "",
                            ARRDATE = ""
                        }
                    },
                    VISADETAILS = new VISADETAILS
                    {
                        TRRQID = "",
                        TRVISA = "0",
                        PAYMODEDESC = "",
                        VSCUCODE = "",
                        ISCUCODE = "",
                        VSAMOUNT = "",
                        ISAMOUNT = "",
                        REMARKS = "",
                        TRINSURANCE = "0",
                        PAYMODE = ""
                    },
                    TRIPID = "",
                    REASON = new REASON
                    {
                        LOW_FL_REASON = "Important Meeting",
                        REASON_NAME = "BUSINESS",
                        REASONID = "0"
                    },
                    MULTICITY = new MULTICITY
                    {
                        TYPE = "I",
                        Text = "FALSE"
                    },
                    lstIMPRESTDETAILS = new List<object>(),
                    DeviceId = DeviceId,
                    UniqueID =originModel.UniqueId ,
                    InPolicy = "0",
                    HOTELDETAILS = new HOTELDETAILS
                    {

                        DURATIONDAYS = "",
                        HTDATE = "",
                        CITYNAME = "",
                        MTNAME = "",
                        INTERNATIONAL = "",
                        CUCODE = "",
                        CITYCODE = "",
                        HTRATE = "",
                        COCODE = "",
                        PAYMODE = "",
                        PAYMODEDESC = "",
                        MTTYPE = "",
                        TRRQID = "",
                        HTNAME = "",
                        CONAME = "",
                        MTTYPENAME = "",
                        AMOUNT = "",
                        HTREMARKS = ""
                    }
                    ,
                    REQUESTEDFLIGHTDETAILS = new REQUESTEDFLIGHTDETAILS
                    {
                        SELECTEDFLIGHTDETAILS = new SELECTEDFLIGHTDETAILS
                        {
                            LOWESTRETURNFLIGHT = new LOWESTRETURNFLIGHT
                            {

                            },
                            
                            //RETURNFLIGHT = new RETURNFLIGHT
                            //{
                            //    FLDETAILS = new List<FLDETAIL>
                            //    {
                            //        new FLDETAIL
                            //        {
                            //ARRDATE= "20190827",
                            //FLNO= "UL-191",
                            //SEGMENTLINENO= "",
                            //SEQNO= "1",
                            //TRRQID= "",
                            //FLFROMNAME= "Colombo",
                            //CUCODE= "INR",
                            //DEPTIME= "0040",
                            //FLTO= "DEL",
                            //SectorRPH= "2",
                            //FLTONAME= "Delhi",
                            //DEPDATE= "20190827",
                            //CACODE= "4",
                            //BASEFARE= "8488",
                            //InPolicy= "0",
                            //RETURN= "1",
                            //ARRTIME= "0415",
                            //TRTYPE= "I",
                            //TOTALTAX= "21430",
                            //FLFROM="CMB"
                            //        },

                            //    },



                            //},
                            DEPATUREFLIGHT =new DEPATUREFLIGHT
                            {
                             FLDETAILS=lstOutBoundFlights //new List<FLDETAIL2>
                            //    {
                            //        new FLDETAIL2
                            //        {

                            //ARRDATE= detailModel.ArrivalDate,
                            //FLNO= detailModel.AirLineCode+"-"+detailModel.FlightNumber,
                            //SEGMENTLINENO= "",
                            //SEQNO= "1",
                            //TRRQID= "",
                            //FLFROMNAME= detailModel.DepAirport,
                            //CUCODE= "INR",
                            //DEPTIME= detailModel.DepartureTime.Remove(2,1),
                            //FLTO= detailModel.ALocCode,
                            //SectorRPH= "1",
                            //FLTONAME= detailModel.ArrivalAirport,
                            //DEPDATE= detailModel.DepartureDate,
                            //CACODE= "4",
                            //BASEFARE= detailModel.FlightFare.ToString(),
                            //InPolicy= "0",
                            //RETURN=detailModel.Return==""?"0":"1",
                            //ARRTIME= "2220",
                            //TRTYPE= "I",
                            //TOTALTAX= "21430",
                            //FLFROM=detailModel.OLocCode
                            //        }
                            //    }
                            },
                       PAYMODEDESC= "Billed to Client",
                                    
                       LOWESTDEPATUREFLIGHT=new LOWESTDEPATUREFLIGHT
                       {
                           
                       },
                    PAYMODE= "3",


                        },
                        SEQ_NO="1",
                        FLIGHTDETAILS=new FLIGHTDETAILS
                        {
                            RETURN=originModel.Return==""?"0":"1",
                 DESTINATION= destinationModel.ALocCode,
                ARRIVALTIME= "",
                ORIGIN=originModel.OLocCode,
                REMARKS= "",
                TRAVELTYPE= "I",
                ARRIVALDATE= destinationModel.ArrivalDate,
                RETURNDATE ="",
                SEQNO= "1",
                DESTINATIONNAME=destinationModel.ArrivalAirport,
                ORIGINNAME= originModel.DepAirport,
                DEPARTUREDATE=originModel.DepartureDate,
                RETURNTIME= "",
                CABINCLASS= "E",
                DEPARTURETIME= originModel.DepartureTime.Remove(2,1)

                       },
                        ACTION="",

                    },
                    OFFLINE_CAR_DETAILS=new OFFLINECARDETAILS
                    {
             TR_RQ_ID= "",
            DROP_DATE= "",
            CAR_TYPE= "",
            PAY_MODE_DESC= "",
            DROP_TIME= "",
            CO_CODE= "",
            PICKUP_LOCATION= "",
            PICKUP_TIME= "",
            CO_CODE_NAME= "",
            CO_NAME= "",
            CAR_CODE= "",
            CAR_MODEL= "",
            PICKUP_DATE= "",
            CITY_CODE= "",
            REMARKS="",
            CITY_NAME= "",
            CAR_TYPE_NAME= "",
            AMOUNT= "",
            DROP_LOCATION= "",
            PAY_MODE= "",
            CU_CODE= ""
                
                    },
                    OFFLINE_BUS_DETAILS=new OFFLINEBUSDETAILS
                    {
             CS_TO= "",
            REMARKS= "",
            ARR_TIME= "",
            CS_FROM_NAME= "",
            DEP_DATE= "",
            CS_CLASS_CODE_NAME= "",
            PAY_MODE_DESC= "",
            CS_TO_NAME= "",
            TR_RETURN= "",
            ARR_DATE= "",
            CU_CODE= "",
            AMOUNT="",
            DEP_TIME= "",
            CS_CLASS_CODE= "",
            CS_FROM= "",
            PAY_MODE= ""


                    },
                    APLTYPEID="1",
                    AdditionalService=new AdditionalService(),
                    OnlineCarDetail=new OnlineCarDetail
                    {
              Action= "",
            CarSno= "",
            PickLocationType= "",
            DropLocationType= "",
            CarDriverDetail=new CarDriverDetail
            {
                 PhoneNo= "",
                FirstName= "",
                CityCode= "",
                LastName= "",
                Title= "",
                CountryCode= "",
                Email= ""
            },
             JustificationCode= "",
            CarProviderSno= "",
            Remarks= ""



                    },
                    TRAVLERDETAILS=new TRAVLERDETAILS
                    {
             CCID= "",
            DEPT_ID= "15999",
            TARQID= "",
            CUSTOMERID= custid,
            CUSTOMERNAME= custName,
            OFID=offid,
            DEFAULTCURR= "INR",
            COORIDINATORCUSTOMERID= "14451",
            GUESTBOOKING= "",
            READ_FARE_RULE= "False",
            CCENABLE= "1",
            TMC_OF_ID="10278",
            DTTI_Created= ""
                    },
                    MEETINGDETAILS=new MEETINGDETAILS
                    {
             MTSTARTTIME= "",
            MTENDDATE= "20190718",
            APPROVERNAME= "",
            CONAME= "India",
            PROFITCENTREDESC= "",
            DURATIONHOURS= "",
            CCENABLE= "",
            CCNAME= "",
            COCODE= "IN",
            MTENDTIME= "",
            DURATIONDAYS= "",
            CITYNAME= "",
            TA_Remarks= "",
            CC_ID= "43348",
            TOTALCOST= "",
            BUSINESSCOSTCENTERDESC= "",
            CC_NAME= "Cctest New(Cctest Code)",
            PURPOSENAME= "",
            PURPOSEID= "",
            CHANNELID= "",
            PROFITCENTREID= "",
            BUSINESSCOSTCENTERID= "",
            PURPOSEDETAILS= "",
            BUSINESSAREADESC= "",
            LOCATIONDESC= "",
            Mobile_Data_Activation= "True",
            INTERNAL_ORDER_NO= "ION1",
            CCID= "10675",
            PURPOSEDETAILID= "-1",
            CHANNELDESC= "",
            IMPRESTLASTDATE= "",
            SPECIALREMARKS= "",
            APPROVERID= "0",
            TripReasonType= "",
            CITYCODE= "",
            BUSINESSAREAID= "",
            SEGMENTDESC= "",
            LOCATIONID= "",
            MTTYPE= "0",
            MaximumPrice= "",
            DIVISION_NAME= "Divtest New(Divtest Code)",
            SEGMENTID= "",
            DIVISION_ID= "1",
            CCEMAIL= "",
            MTSTARTDATE= "20190718"


                    },
                    EXPENSEREQUEST=new EXPENSEREQUEST
                    {
             PNRNO= "",
            ACTION= "",
            Text= "FALSE"
                    },
                    TokenID=token






                }







            };

            RETURNFLIGHT RETURNFLIGHT1 = new RETURNFLIGHT
            {
                FLDETAILS =lstInBoundFlights //new List<FLDETAIL>
            //    {
            //        new FLDETAIL
            //        {
            //ARRDATE=detailModel.ArrivalDate ,
            //FLNO= detailModel.FlightNumber,
            //SEGMENTLINENO= "",
            //SEQNO= "1",
            //TRRQID= "",
            //FLFROMNAME= detailModel.DepAirport,
            //CUCODE= "INR",
            //DEPTIME=detailModel.DepartureTime.Remove(2,1),
            //FLTO=detailModel.OLocCode ,
            //SectorRPH= "2",
            //FLTONAME= detailModel.DepAirport,
            //DEPDATE= rtdate,
            //CACODE= "4",
            //BASEFARE= detailModel.FlightFare.ToString(),
            //InPolicy= "0",
            // RETURN=detailModel.Return==""?"0":"1",
            //ARRTIME= "0415",
            //TRTYPE= "I",
            //TOTALTAX= "21430",
            //FLFROM=detailModel.ALocCode
            //        },
           
            //    },
                



            };
            if (detailModel.FirstOrDefault().Return != "")
            {
                RequestObj.UPADDTRVELREQUESTINPUT.REQUESTEDFLIGHTDETAILS.SELECTEDFLIGHTDETAILS.RETURNFLIGHT = RETURNFLIGHT1;
            }
            else
            {
                RequestObj.UPADDTRVELREQUESTINPUT.REQUESTEDFLIGHTDETAILS.SELECTEDFLIGHTDETAILS.RETURNFLIGHT = new RETURNFLIGHT
                {
                    FLDETAILS = new List<FLDETAIL>
                    {

                    }
                };
            }
            string request = JsonConvert.SerializeObject(RequestObj);
            return request;
        }
        public string GetBookRequest(EmployeeModel employee,CustomFlightDetailModel detailModel,string bookId,string token)
        {
            BookRequest BookReqObj= new BookRequest
            {
                lstPaxDetails=new List<LstPaxDetail>
                {
                    new LstPaxDetail
                    {
              Mobile= "1-1",
            Contact_No= "",
            Email=employee.Email ,
            First_Name= employee.FirstName,
            Passport=new Passport
            {
                DOB= "19230101",
                Gender= "M",
                Date_OF_Issue= "",
                Date_OF_Expiry= "",
                Number= "",
                Place_OF_Issue= ""
            },
            DOB="19230101",
            FAMILY_USER_ID="",
            Address=new Address
            {
                AddressLine= "",
                PostalCode= "",
                StateCode= "",
                CityName= "",
                CO_Code= ""


            },
            Pax_Type_Code="ADT",
            Title="Mr",
            
            lstMealDetails=new List<LstMealDetail>
            {
                new LstMealDetail
                {
                    Fax="",
                    Meals=new Meals
                    {
                         Desc= "",
                        Amount= "",
                        Code= "",
                        SegmentNo= "1"
                    },
                    Seats=new Seats
                    {
                         Code= "",
                        Desc= "",
                        SegmentNo= "1"
                    },
                    FFNNO=new FFNNO
                    {
                        Number="",
                        SegmentNo="1"
                    },
                    Others=new Others
                    {
                         Code ="",
                        Desc= "",
                        SegmentNo= "1"
                    }
                }
            },
            Last_Name="Kumar",
            RPH="1",
            

                    },
                   
                    
                    
                },
                IsTryAgain="",
                lstmiscellaneousInfo=new List<LstmiscellaneousInfo>
                {
                    new LstmiscellaneousInfo
                    { Code= "RM",
            OfficeId= employee.OffId,
            IsDisplayOnFinishing= "True",
            IsDisplayOnEmail= "True",
            LabelName= "*CUSTACCTNO",
            MisceValue= "*CUSTACCTNO-jkhjkhjk",
            IsDisplayInReport= "True"

                    }
                },
                PAYMENT_BORNE_BY="",
                TMCName=new TMCName
                {
          TMC_Name="Vickky TMC",
        Office_ID= "DEL1A0980~174"
                },
                OFID=employee.OffId,
                CreditCardDetail = new CreditCardDetail
                {
        CardExpiry= "",
        OtherOption= "FALSE",
        Card= "",
        CardNumber= "",
        TravellerProfilePaymentMode= "",
        PaymentMode= ""
                },
               UniqueID=detailModel.UniqueId ,
    requestID= bookId,
    TAID= "13464",
    ISHold= "0",
    UserID= "14451",
    lstMealDetails=new List<LstMealDetail2>
    {
        new LstMealDetail2
        {
            Fax="",
            Meals=new Meals2
            {
                  Desc= "",
                Amount= "",
                Code= "",
                SegmentNo= "1"
            },
            Seats=new Seats2
            {
                Code= "",
                Desc= "",
                SegmentNo= "1"
            },
            FFNNO=new FFNNO2
            {
                Number="",
                SegmentNo="1",

            },
            Others=new Others2
            {
                 Code= "",
                Desc= "",
                SegmentNo= "1"
            }
            
        },
        

    },
    LocatorNumber="",
    TokenID=token
                

                



            };


     




            return JsonConvert.SerializeObject(BookReqObj);
        }



    }
}
