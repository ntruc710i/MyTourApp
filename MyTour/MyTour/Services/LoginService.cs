using MyTour.Models;
using MyTour.Services.GetIPAdress;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyTour.Services
{
    public class LoginService
    {
        public KhachHang KH;
        static LoginService _instance;
        public string IPAdress;
        static Boolean _IsLogin { get; set; }
        public int n = 0;

        public LoginService()
        {
            KH = new KhachHang();
            IPAdress = GetIPAddress.Instance.IPAddress();
            IsLogin = false;
        }
        public static LoginService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LoginService();

                return _instance;
            }
        }
        public static Boolean IsLogin
        {
            get
            {
                return _IsLogin;
            }
            set { _IsLogin = value; }

        }
        public async Task<KhachHang> GetLoginAsync(string email, string password)
        {

            try
            {
                string url = "http://" + IPAdress + "/api/login/email=" + email +"/password="+ password;
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);
                var emptyList = JsonConvert.DeserializeObject<List<KhachHang>>(result);

                if(emptyList!= null)
                {
                    KH = emptyList[0];
                }
                
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                Debug.WriteLine(e.Message);
            }
            return await Task.FromResult(KH);
        }
        
    }
}
