using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clockwork;
using SMS.Client.Models;
using System.Configuration;
using System.Net;


namespace SMS.Client.Controllers
{
    public class ClockWorkSMSController : Controller
    {

        string apiKey = ConfigurationManager.AppSettings["apiKeyClockWorkSMS"].ToString();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(MessageToSend msg)
        {
	  try
	  {
	      if (msg.to != null && msg.body != null)
	      {
		Clockwork.API api = new API(apiKey);
		SMSResult result = api.Send(new Clockwork.SMS { To = msg.to, Message = "Hello World" });

		if (result.Success){
		    ViewBag.result = result;
		    return View();
		    //Console.WriteLine("SMS Sent to {0}, Clockwork ID: {1}", result.SMS.To, result.ID);
		}
		else		
		    Console.WriteLine("SMS to {0} failed, Clockwork Error: {1} {2}", result.SMS.To, result.ErrorCode, result.ErrorMessage);		
	      }
	      
	  }
	  catch (APIException ex)
	  {
	      // You'll get an API exception for errors
	      // such as wrong username or password
	      Console.WriteLine("API Exception: " + ex.Message);	      
	  }
	  catch (WebException ex)
	  {
	      // Web exceptions mean you couldn't reach the Clockwork server
	      Console.WriteLine("Web Exception: " + ex.Message);
	  }
	  catch (ArgumentException ex)
	  {
	      // Argument exceptions are thrown for missing parameters,
	      // such as forgetting to set the username
	      Console.WriteLine("Argument Exception: " + ex.Message);
	  }
	  catch (Exception ex)
	  {
	      // Something else went wrong, the error message should help
	      Console.WriteLine("Unknown Exception: " + ex.Message);
	  }
	  return View("/");
        
        }

    }
}
