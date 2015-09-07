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
        // Configuration for Twilio
        
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

        /// <summary>
        /// This action return the information about one sended Message
        /// </summary>
        /// <param name="MessageSid">id of Message</param>        
        public ActionResult GetMessageById(string MessageSid)
        {
	  var twilio = new TwilioRestClient(AccountSid, AuthToken);
	  var message = twilio.GetMessage(MessageSid);
	  ViewBag.temp = message;
	  return View();
        }

        public ActionResult Addresses()
        {
	  return View();
        }

        [HttpPost]
        public ActionResult addAddress(Twilio.Address _address)
        {

	  var twilio = new TwilioRestClient(AccountSid, AuthToken);

	  try{
	      var addResult = twilio.AddAddress(_address.FriendlyName, _address.CustomerName, _address.Street, _address.City, _address.Region, _address.PostalCode, _address.IsoCountry);	  
	      return View("Addresses");
	  }
	  
	  catch (Exception ex){ return View();}	  
        }
        
        public ActionResult ListAddress()
        {
	  var twilio = new TwilioRestClient(AccountSid, AuthToken);
	  var options = new AddressListRequest();	  
	  var listAddress = twilio.ListAddresses(options);
	  return View(listAddress);	  
        }

        public ActionResult Edit(string idCustomer){
	  var twilio = new TwilioRestClient(AccountSid, AuthToken);
	  var Address = twilio.GetAddress(idCustomer);
	  return View(Address);
        }

        [HttpPost]
        public ActionResult EditAddress(Twilio.Address _address)
        {
	  var twilio = new TwilioRestClient(AccountSid, AuthToken);
	  var options = new AddressOptions();

	  options.City = _address.City;
	  options.CustomerName = _address.CustomerName;
	  options.FriendlyName = _address.FriendlyName;
	  options.PostalCode = _address.PostalCode;
	  options.Region = _address.Region;
	  options.Street = _address.Street;

	  var response = twilio.UpdateAddress(_address.Sid, options);

	  return RedirectToAction("ListAddress");        
        }

    }
}
