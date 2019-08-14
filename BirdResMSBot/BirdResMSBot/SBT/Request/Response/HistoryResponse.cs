using System;
using System.Collections.Generic;
using System.Text;

namespace BirdResAWSBot.SBT.Request.Response
{
      
    public class LstGetCustomerFlightFromTo
    {
        public string CUSTOMER_ID { get; set; }
        public string FL_FROM { get; set; }
        public string FL_TO { get; set; }
        public string TR_RQ_ID { get; set; }
        public string FL_NO { get; set; }
        public string TOTAL_AMOUNT { get; set; }
    }

    public class HistoryResponse
    {
        public string ResultStatus { get; set; }
        public string ResultDescription { get; set; }
        public List<LstGetCustomerFlightFromTo> lstGetCustomerFlightFromTo { get; set; }
    }
}
