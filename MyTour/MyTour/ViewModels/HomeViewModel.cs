using MyTour.Models;
using MyTour.Services.GetIPAdress;
using MyTour.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace MyTour.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    { 
    public string IPAdress;
    public ObservableCollection<Tour> SaleTours { get; }
    public ObservableCollection<Tour> RecTours { get; set; }
    public Command TapAreaCommand { get; set; }
    public Command LoadRecToursCommand { get; set; }
    public Command LoadSaleToursCommand { get; set; }
    Tour _tour;
    public HomeViewModel()
    {

        SaleTours = new ObservableCollection<Tour>();
        RecTours = new ObservableCollection<Tour>();
        Task.Run(async () => await ExecuteLoadSaleToursCommand());
        Task.Run(async () => await ExecuteLoadRecToursCommand());
        IPAdress = GetIPAddress.Instance.IPAddress();
        //LoadRecToursCommand = new Command(async () => await ExecuteLoadRecToursCommand());
        //LoadSaleToursCommand = new Command(async () => await ExecuteLoadSaleToursCommand());
        
        }
    public Tour Tour
    {
        get { return _tour; }
        set
        {
            _tour = value;
            OnPropertyChanged();
        }
    }
        //Sale Tours
        public ICommand BackCommand => new Command(OnBack);

        async Task ExecuteLoadSaleToursCommand()
        {
            // var tours = new ObservableCollection<Tour>(); // new collection
            IsBusy = true;
            try
            {
                SaleTours.Clear();
                var saletours = await TourService.Instance.GetSaleTourAsync();
                foreach (var tour in saletours)
                {
                    if (!tour.Anh.Contains("/images/"))
                    {
                        tour.Anh = "http://" + IPAdress + "/images/" + tour.Anh;
                    }
                    if (SaleTours.Any(Tour => Tour.Matour == tour.Matour) == false) SaleTours.Add(tour);
                    //OnPropertyChanged(nameof(SaleTours)); // raise a property change in whatever way is right for your VM
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        //Recommend Tours
        async Task ExecuteLoadRecToursCommand()
        {
            IsBusy = true;

            try
            {
                RecTours.Clear();
                var rectours = await TourService.Instance.GetRecTourAsync();
                int i = 0;
                foreach (var tour in rectours)
                {
                    i++;
                    if (!tour.Anh.Contains("/images/"))
                    {
                        tour.Anh = "http://" + IPAdress + "/images/" + tour.Anh;
                    }
                    RecTours.Add(tour);
                    if (i == 4) break;
                }
                RecTours.Distinct();


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


        //Area
        public ICommand TapAreaTours => new Command<string>(ExecuteTabAreaCommand);
        async void ExecuteTabAreaCommand(string param)
        {
            KhuvucTour KV = new KhuvucTour
            {
                MaKV = Convert.ToInt32(param)
            };
            NavigationService.Instance.NavigateToAsync<ToursAreaViewModel>(KV);
        }
        public ICommand DetailCommand => new Command<Tour>(OnNavigate);
        void OnNavigate(Tour parameter)
        {
            NavigationService.Instance.NavigateToAsync<TourDetailsViewModel>(parameter);
        }


    }
}
