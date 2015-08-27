using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Client.Models
{
    public class MessageToSend
    {
        public string to { get; set; }
        public string from { get; set; }
        public string body { get; set; }
        public string MediaURL { get; set; }
    }
}