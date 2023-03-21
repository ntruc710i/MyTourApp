using MyTour.Models;
using MyTour.Services;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyTour.ViewModels
{
    internal class LienheViewModel
    {
        public string ten { get; set; }
        public string sdt { get; set; }
        public string matour { get; set; }
        public string ghichu { get; set; }
        public ICommand LienHeCommand => new Command(async () => await ExecuteLienHeCommand());
        async Task ExecuteLienHeCommand()
        {
            try
            {
                
                var response = await LienheService.Instance.newLienHe(ten,sdt,matour,ghichu);
                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Thông Báo", "Liên hệ thành công", "OK");
                }
                else
                {
                    
                    CrossToastPopUp.Current.ShowToastError("Liên hệ thất bại");
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
           
        }
    }
}
