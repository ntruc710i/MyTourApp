using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MyTour.Models;
using MyTour.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using MyTour.Views;
using Rg.Plugins.Popup.Services;
using static Xamarin.Essentials.Permissions;
using Xamarin.Forms;
using System.Linq;

namespace MyTour.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string _Email { get; set; }
        public string _Password { get; set; }
        public KhachHang KH { get; set; }

        public ICommand LoginCommand => new Command(async () => await ExecuteLoginCommand());
        async Task ExecuteLoginCommand()
        {
            try
            {
                string email = _Email;
                string password = _Password;
                var response = await LoginService.Instance.GetLoginAsync(email, password);
                KhachHang KH = new KhachHang();
                KH = response;

                if(!KH.Email.Contains('0'))
                {
                    KhachHangService._CurrentUser = null;
                    KhachHangService.LoadUser(KH);
                    LoginService.IsLogin = true;
                    App.Current.MainPage = new NavigationPage(new MainPage());

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Thông Báo", "Sai email hoặc mật khẩu", "OK");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
    }
}
