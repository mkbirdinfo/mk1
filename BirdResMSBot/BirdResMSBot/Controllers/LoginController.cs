using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirdResAWSBot;
using BirdResAWSBot.SBT.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;

namespace BirdResMSBot.Controllers
{
    public class LoginController : Controller
    {
      
        [Route("api/login")]
  
        public ActionResult Index()
        {
            string DeviceId = Guid.NewGuid().ToString();
            ViewBag.DeviceId = DeviceId;

            //CookieOptions option = new CookieOptions();


            //    option.Expires = DateTime.Now.AddMinutes(10);


            //Response.Cookies.Append("mykey", DeviceId, option);
            //new LoginIntent().Login(loginRequest.LoginID, loginRequest.Password, DeviceId);
            return View();
        }

  
    }
}