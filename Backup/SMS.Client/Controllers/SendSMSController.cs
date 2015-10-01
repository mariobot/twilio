using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMS.Client.Models;
using Twilio;
using System.Configuration;

namespace SMS.Client.Controllers
{
    public class SendSMSController : Controller
    {
        // Find your Account Sid and Auth Token at twilio.com/user/account 
        string AccountSid = ConfigurationManager.AppSettings["AccountSid"].ToString();
        // Token de la aplicacion
        string AuthToken = ConfigurationManager.AppSettings["AuthToken"].ToString();
        // El destinatario es la linea que asignan en la api y si no se coloca esta no funciona la API
        string destinatario = ConfigurationManager.AppSettings["destinatario"].ToString();
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(MessageToSend msg)
        {  
	  var twilio = new TwilioRestClient(AccountSid, AuthToken);

	  var messageModel = twilio.SendMessage(destinatario,msg.to,msg.body);

	  ViewBag.temp = messageModel;

	  return View("Index");
        }

        public ActionResult GetMessages()
        {
	  var twilio = new TwilioRestClient(AccountSid, AuthToken);

	  // Build the parameters 
 	  var options = new MessageListRequest();

	  var ListMessagesResponse = twilio.ListMessages(options);

	  return View(ListMessagesResponse);
        }

        public ActionResult GetMessageById(string MessageSid)
        {
	  var twilio = new TwilioRestClient(AccountSid, AuthToken);

	  var message = twilio.GetMessage(MessageSid);

	  ViewBag.temp = message;

	  return View();
        }

    }
}
