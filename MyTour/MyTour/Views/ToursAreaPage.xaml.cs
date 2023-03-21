using MyTour.ViewModels;
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
    public partial class ToursAreaPage : ContentPage
    {
       ToursAreaViewModel _viewModel;
        public ToursAreaPage()
        {

            InitializeComponent();
            BindingContext = _viewModel = new ToursAreaViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}