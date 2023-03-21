using MyTour.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MyTour.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using MyTour.Services.GetIPAdress;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Security.Cryptography;

namespace MyTour.ViewModels
{

    public class ToursAreaViewModel : BaseViewModel
    {
        public string IPAdress;
        public string makv;
        public ObservableCollection<Tour> Tours { get; }
        KhuvucTour _kvtour;
        public Command ToursAreaCommand { get; }
        public ToursAreaViewModel()
        {
            Tours = new ObservableCollection<Tour>();
            //Task.Run(async () => await AreaTours());
            IPAdress = GetIPAddress.Instance.IPAddress();
            ToursAreaCommand = new Command(async () => await ToursArea());


        }
        public void OnAppearing()
        {
            IsBusy = true;
        }

        public KhuvucTour KvTour
        {
            get { return _kvtour; }
            set
            {
                _kvtour = value;
                OnPropertyChanged();
            }
        }
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is KhuvucTour destination)
                KvTour = destination;

            return base.InitializeAsync(navigationData);
        }
        public ICommand BackCommand => new Command(OnBack);
        void OnBack()
        {
            NavigationService.Instance.NavigateBackAsync();
        }
        public ICommand DetailCommand => new Command<Tour>(OnNavigate);
        void OnNavigate(Tour parameter)
        {
            NavigationService.Instance.NavigateToAsync<TourDetailsViewModel>(parameter);
        }
        //public ICommand ToursAreaCommand => new Command(ToursArea);
        async Task ToursArea()
        {
            
            IsBusy = true;

            try
            {
                Tours.Clear();
                var tours = await TourService.Instance.GetAreaTourAsync(KvTour.MaKV.ToString()) ;
                foreach (var tour in tours)
                {
                    if (!tour.Anh.Contains("/images/"))
                    {
                        tour.Anh = "http://" + IPAdress + "/images/" + tour.Anh;
                    }
                    if (Tours.Any(Tour => Tour.Matour == tour.Matour) == false) Tours.Add(tour);

                }
                OnPropertyChanged(nameof(Tours));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Lỗi :" + ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
    }

}
