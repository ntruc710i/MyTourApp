using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyTour.Models;
using MyTour.Services.GetIPAdress;
using Newtonsoft.Json;
namespace MyTour.Services
{
    public class TourService 
    {
        static TourService _instance;
        readonly List<Tour> tours;
        readonly List<Tour> areatours;
        readonly List<Tour> rectours;
        readonly List<KhuvucTour> areas;
        readonly List<Tour> saletours;
        public string IPAdress;
        public int n = 0;

        public TourService()
        {
            tours = new List<Tour>();
            areatours = new List<Tour>();
            saletours = new List<Tour>();
            rectours = new List<Tour>();
            areas = new List<KhuvucTour>();
            IPAdress = GetIPAddress.Instance.IPAddress();
        }
        public static TourService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TourService();

                return _instance;
            }
        }

        public async Task<IEnumerable<Tour>> GetTourAsync()
        {
            
            try
            {
                string url = "http://"+ IPAdress +"/api/tours";
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);
                var emptyList = JsonConvert.DeserializeObject<List<Tour>>(result);
                tours.Clear();
                foreach (var i in emptyList)
                {
                    tours.Add(i);
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                Debug.WriteLine(e.Message);
            }
            return await Task.FromResult(tours);
        }

        public async Task<IEnumerable<Tour>> GetAreaTourAsync(string makv)
        {

            try
            {
                string url = "http://" + IPAdress + "/api/tours/khuvuc/"+ makv;
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);
                var emptyList = JsonConvert.DeserializeObject<List<Tour>>(result);
                areatours.Clear();
                foreach (var i in emptyList)
                {
                    areatours.Add(i);
                }
                
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                Debug.WriteLine(e.Message);
            }
            return await Task.FromResult(areatours);
        }

        public async Task<IEnumerable<KhuvucTour>> GetAreaAsync()
        {

            try
            {
                string url = "http://" + IPAdress + "/api/areas";
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);
                var emptyList = JsonConvert.DeserializeObject<List<KhuvucTour>>(result);
                areas.Clear();
                foreach (var i in emptyList)
                {
                    areas.Add(i);
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                Debug.WriteLine(e.Message);
            }
            return await Task.FromResult(areas);
        }
        public async Task<IEnumerable<Tour>> GetSaleTourAsync()
        {
           
            try
            {

                string url = "http://" + IPAdress + "/api/saletours";
                HttpClient client1 = new HttpClient();
                var result = await client1.GetStringAsync(url);
                var emptyList = JsonConvert.DeserializeObject<List<Tour>>(result);
                saletours.Clear();
                foreach (var i in emptyList)
                {
                    saletours.Add(i);
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                Debug.WriteLine(e.Message);
            }
            return await Task.FromResult(saletours);
        }

        public async Task<IEnumerable<Tour>> GetRecTourAsync()
        {
            try
            {
                string url = "http://" + IPAdress + "/api/tours";
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);
                var emptyList = JsonConvert.DeserializeObject<List<Tour>>(result);
                rectours.Clear();
                foreach (var i in emptyList)
                {
                    rectours.Add(i);
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                Debug.WriteLine(e.Message);
            }
            return await Task.FromResult(rectours);
        }
    }
}

