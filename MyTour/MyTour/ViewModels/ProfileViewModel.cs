using MyTour.Models;
using MyTour.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyTour.ViewModels
{
    public class ProfileViewModel: BaseViewModel
    {
        public Boolean IsUser { get; set; }
        public KhachHang KH { get; set; }
        public ProfileViewModel() { 
            IsUser = false;
            if(LoginService.IsLogin)
            {
                IsUser = true;
                KH = new KhachHang();
                KH = KhachHangService.GetCurrentUser;
                
            }

        
        }
        public ICommand LoginCommand => new Command(OnNavigate);
        void OnNavigate()
        {
            NavigationService.Instance.NavigateToAsync<LoginViewModel>();
        }

        public ICommand LogoutCommand => new Command(async () => await ExecuteLogoutCommand());
        async Task ExecuteLogoutCommand()
        {
            try
            {
                KhachHangService._CurrentUser = null;
                LoginService.IsLogin = false;
                App.Current.MainPage = new NavigationPage(new MainPage());

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        public ICommand OrderHistoryCommand => new Command(OrderHistory);
        void OrderHistory()
        {
            NavigationService.Instance.NavigateToAsync<OrderHistoryViewModel>();
        }
    }
}
