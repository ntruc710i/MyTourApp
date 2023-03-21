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
    public partial class ToursPage : ContentPage
    {
        public ToursPage()
        {
            InitializeComponent();
            BindingContext = new ToursViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void onfocus(object obj, EventArgs e)
        {
            var myframe = obj as Frame;
            myframe.BackgroundColor = Color.Red;
        }
    }
}