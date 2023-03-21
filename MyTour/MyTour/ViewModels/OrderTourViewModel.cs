using MyTour.Models;
using MyTour.Services;
using MyTour.Views;
using Plugin.Toast;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace MyTour.ViewModels
{
    //[QueryProperty(nameof(MaTour), nameof(MaTour))]
    internal class OrderTourViewModel : BaseViewModel
    {
        Tour _tour;
        public Boolean IsUser { get; set; }

        //public int MaDH { get; set; }
        public int MaTour { get; set; }
        //public int MaKH { get; set; }
        //public DateTime NgayDat { get; set; }
        public DateTime NgayKhoiHanh { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }
        //public int TrangThai { get; set; }
        public string GhiChu { get; set; }

        
        public string _HoVaTen { get; set; }
        public string _Phone { get; set; }
        public string _Email { get; set; }
        public string _DiaChi { get; set; }

        
        public Tour Tour
        {
            get { return _tour; }
            set
            {
                _tour = value;
                OnPropertyChanged();
            }
        }


        public KhachHang CurrentUser { get; set; }
        public OrderTourViewModel()
        {
            if (LoginService.IsLogin)
            {
                IsUser = true;
                CurrentUser = new KhachHang();
                CurrentUser = KhachHangService.GetCurrentUser;

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

        public ICommand OrderTourCommand => new Command(async () => await ExecuteOrderCommand());
        async Task ExecuteOrderCommand()
        {
            try
            {
                KhachHang KH = new KhachHang
                {
                    HoVaTen = _HoVaTen,
                    Phone = _Phone,
                    Email = _Email,
                    DiaChi = _DiaChi
                };
                MaTour = Tour.Matour;
                ThanhTien = Tour.Gia * SoLuong;
                var response = await DonHangService.Instance.newDonHang(MaTour, NgayKhoiHanh, SoLuong, ThanhTien,GhiChu,KH);
                if (response.IsSuccessStatusCode)
                {
                   
                    await PopupNavigation.Instance.PushAsync(new SuccessPopup());
                    //
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Thông Báo", "Thất bại", "OK");
                    
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        //Thêm đơn hàng khi đã đăng nhập
        public ICommand OrderTourLoginedCommand => new Command(async () => await ExecuteOrderLoginedCommand());
        async Task ExecuteOrderLoginedCommand()
        {
            try
            {
                int MaKH = KhachHangService.GetCurrentUser.MaKH;
                MaTour = Tour.Matour;
                ThanhTien = Tour.Gia * SoLuong;
                var response = await DonHangService.Instance.newDonHangLogined(MaTour, NgayKhoiHanh, SoLuong, ThanhTien, GhiChu, MaKH);
                if (response.IsSuccessStatusCode)
                {

                    await PopupNavigation.Instance.PushAsync(new SuccessPopup());
                    //
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Thông Báo", "Thất bại", "OK");

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
    }
}
