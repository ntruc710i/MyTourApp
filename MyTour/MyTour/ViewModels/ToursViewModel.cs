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

namespace MyTour.ViewModels
{

    public class ToursViewModel : BaseViewModel
    {
        public string IPAdress;
        public ObservableCollection<Tour> Tours { get; }
        public ObservableCollection<KhuvucTour> Areas { get; }
        public Command LoadToursCommand { get; }
        Tour _tour;
        bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public ToursViewModel()
        {
            
            Tours = new ObservableCollection<Tour>();
            Areas = new ObservableCollection<KhuvucTour>();
            Task.Run(async () => await ExecuteLoadAreasCommand());
            Task.Run(async () => await ExecuteLoadToursCommand());
            IPAdress = GetIPAddress.Instance.IPAddress();
        }
       

        public ICommand BackCommand => new Command(OnBack);
        async Task ExecuteLoadToursCommand()
        {
            IsBusy = true;
            
            try
            {   
                IsRefreshing = true;
                Tours.Clear();
                
                var tours = await TourService.Instance.GetTourAsync();
                foreach (var tour in tours)
                {
                    if (!tour.Anh.Contains("/images/"))
                    {
                        tour.Anh = "http://" + IPAdress + "/images/" + tour.Anh;
                    }
                    Tours.Add(tour);

                }
                OnPropertyChanged(nameof(Tours));
                IsRefreshing = false;
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

        async Task ExecuteLoadAreasCommand()
        {
            IsBusy = true;

            try
            {
                Areas.Clear();
                var areas = await TourService.Instance.GetAreaAsync();
                foreach (var area in areas)
                {
                    Areas.Add(area);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteTapToursCommand()
        {
            IsBusy = true;

            try
            {
                Tours.Clear();
                var tours = await TourService.Instance.GetTourAsync();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;

        }
 

        void OnBack()
        {
            NavigationService.Instance.NavigateBackAsync();
        }
        public ICommand DetailCommand => new Command<Tour>(OnNavigate);
        void OnNavigate(Tour parameter)
        {
            NavigationService.Instance.NavigateToAsync<TourDetailsViewModel>(parameter);
        }

        public ICommand AreaCommand => new Command<KhuvucTour>(AreaTours);
        async void AreaTours(KhuvucTour area)
        {

            IsRefreshing = true;
            Tours.Clear();
           
            var tours = await TourService.Instance.GetAreaTourAsync(area.MaKV.ToString());
            foreach (var tour in tours)
            {
                if (!tour.Anh.Contains("/images/"))
                {
                    tour.Anh = "http://" + IPAdress + "/images/" + tour.Anh;
                }
                
                Tours.Add(tour);
            }
            Tours.Distinct();
            OnPropertyChanged(nameof(Tours));
            IsRefreshing = false;
        }
    }
}
