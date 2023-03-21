using MyTour.Models;
using MyTour.Services.GetIPAdress;
using MyTour.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyTour.ViewModels
{
    public class OrderHistoryViewModel : BaseViewModel
    {
        public string IPAdress;
        public ObservableCollection<LSDonHang> Orders { get; }
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
        public OrderHistoryViewModel()
        {

            Orders = new ObservableCollection<LSDonHang>();
            Task.Run(async () => await ExecuteLoadOrdersCommand());
            IPAdress = GetIPAddress.Instance.IPAddress();
        }


        public ICommand BackCommand => new Command(OnBack);
        async Task ExecuteLoadOrdersCommand()
        {
            IsBusy = true;

            try
            {
                Orders.Clear();
                var orders = await DonHangService.Instance.GetAreaOrderAsync(KhachHangService.GetCurrentUser.MaKH.ToString());
                foreach (var order in orders)
                {
                    if (!order.Anh.Contains("/images/"))
                    {
                        order.Anh = "http://" + IPAdress + "/images/" + order.Anh;
                    }
                    Orders.Add(order);
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
        void OnBack()
        {
            NavigationService.Instance.NavigateBackAsync();
        }
    }
}
