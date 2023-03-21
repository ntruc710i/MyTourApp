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
    public class DonHangService
    {
        static DonHangService _instance;
        readonly List<LSDonHang> orders;
        public string IPAdress;
        public DonHangService()
        {
            orders= new List<LSDonHang>();
            IPAdress = GetIPAddress.Instance.IPAddress();
        }
        public static DonHangService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DonHangService();

                return _instance;
            }
        }
        public async Task<HttpResponseMessage> newDonHang(int matour, DateTime ngaykhoihanh,int soluong, double thanhtien, string ghichu,KhachHang kh)
        {


            string url = "http://" + IPAdress + "/api/donhang";
            HttpClient client = new HttpClient();
            var donhang = new DonHang
            {
                MaTour = matour,
                NgayKhoiHanh = ngaykhoihanh,
                SoLuong = soluong,
                ThanhTien = thanhtien,
                GhiChu = ghichu,
                KH = new KhachHang()
                {
                    HoVaTen = kh.HoVaTen,
                    DiaChi = kh.DiaChi,
                    Phone =kh.Phone,
                    Email = kh.Email
                }
                
            };
            var json = JsonConvert.SerializeObject(donhang);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            return response;
        }
        public async Task<HttpResponseMessage> newDonHangLogined(int matour, DateTime ngaykhoihanh, int soluong, double thanhtien, string ghichu, int makh)
        {


            string url = "http://" + IPAdress + "/api/donhang/isuser";
            HttpClient client = new HttpClient();
            var donhang = new DonHang
            {
                MaTour = matour,
                MaKH = makh,
                NgayKhoiHanh = ngaykhoihanh,
                SoLuong = soluong,
                ThanhTien = thanhtien,
                GhiChu = ghichu,
            };
            var json = JsonConvert.SerializeObject(donhang);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            return response;
        }

        public async Task<IEnumerable<LSDonHang>> GetAreaOrderAsync(string makh)
        {

            try
            {
                string url = "http://" + IPAdress + "/api/donhang/" + makh;
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);
                var emptyList = JsonConvert.DeserializeObject<List<LSDonHang>>(result);
                orders.Clear();
                foreach (var i in emptyList)
                {
                    orders.Add(i);
                }

            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                Debug.WriteLine(e.Message);
            }
            return await Task.FromResult(orders);
        }
    }
}
