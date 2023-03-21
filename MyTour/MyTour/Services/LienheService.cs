using MyTour.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyTour.Services.GetIPAdress;

namespace MyTour.Services
{
    internal class LienheService
    {
        static LienheService _instance;
        public string IPAdress;
        public LienheService()
        {
           
            IPAdress = GetIPAddress.Instance.IPAddress();
        }
        public static LienheService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LienheService();

                return _instance;
            }
        }
        public async Task<HttpResponseMessage> newLienHe( string ten , string sdt ,string matour ,string ghichu )
        {

            
                string url = "http://" + IPAdress + "/api/lienhe";
                HttpClient client = new HttpClient();
                var lienhe = new Lienhe
                {
                    ten = ten,
                    sdt = sdt,
                    matour = matour,
                    ghichu = ghichu
                };
                var json = JsonConvert.SerializeObject(lienhe);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                return response;
        }
    }
}
