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
using MyTour.Views;
namespace MyTour.ViewModels
{
    internal class TourDetailsViewModel : BaseViewModel
    {
        
        Tour _tour;
        public Command OCommand { get; }
        public TourDetailsViewModel()
        {
            OCommand = new Command(async () => await Order());
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

        public ICommand BackCommand => new Command(OnBack);

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Tour destination)
                Tour = destination;

            return base.InitializeAsync(navigationData);
        }

        void OnBack()
        {
            NavigationService.Instance.NavigateBackAsync();
        }

        public ICommand OrderCommand => new Command<Tour>(OnNavigate);
        void OnNavigate(Tour parameter)
        {
            NavigationService.Instance.NavigateToAsync<TourDetailsViewModel>(parameter);
        }

        async Task Order()
        {
            
            NavigationService.Instance.NavigateToAsync<OrderTourViewModel>(Tour);
            //await Shell.Current.GoToAsync($"{nameof(OrderTourPage)}?{nameof(OrderTourViewModel.MaTour)}={Tour.Matour}");
        }
       
    }
}
