using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace MyTour.Services.GetIPAdress
{
    internal class GetIPAddress
    {
        private static GetIPAddress _instance;

        public static GetIPAddress Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GetIPAddress();

                return _instance;
            }
        }
        public string IPAddress()
        {
            string ipadress = Dns.GetHostEntry(Dns.GetHostName())
            .AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
            .ToString();
            return "172.20.10.4:8000";
        }
    }
}
