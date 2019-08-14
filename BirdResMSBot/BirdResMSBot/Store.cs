using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BirdResAWSBot.SBT.Request.Response;
using BirdResAWSBot.SBT;
using BirdResAWSBot;

namespace EchoBot
{


        public class UtteranceLog : IStoreItem
        {
            // A list of things that users have said to the bot
            public List<CustomFlightDetailModel> UtteranceList { get; } = new List<CustomFlightDetailModel>();

            // The number of conversational turns that have occurred        
            public string TurnNumber { get; set; } 
           public EmployeeModel employee = new EmployeeModel();
        public Query query { get; set; }

        // Create concurrency control where this is used.
        public string ETag { get; set; } = "*";
        }
    }

