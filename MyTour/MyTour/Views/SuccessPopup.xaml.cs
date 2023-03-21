using MyTour.Services;
using MyTour.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTour.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public SuccessPopup()
        {
            InitializeComponent();
        }
        private void OnClose(object sender, EventArgs e)
        {
             PopupNavigation.Instance.PopAsync();
             Navigation.PopToRootAsync();
        }

        private async void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
           
            await Navigation.PopToRootAsync();
        }
    }
}