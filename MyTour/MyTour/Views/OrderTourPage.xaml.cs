using MyTour.Models;
using MyTour.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTour.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderTourPage : ContentPage
    {
        public double gia;
        

        public OrderTourPage()
        {
            InitializeComponent();
            BindingContext = new OrderTourViewModel();
            NgayKhoiHanh.MinimumDate = DateTime.Now.Date;
            NavigationPage.SetHasNavigationBar(this, false);
            lblTotal.Text = "0";
        }
        
        void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            gia = Convert.ToDouble(lblGia.Text);
            lbldisp.Text = String.Format("{0}", e.NewValue);
            lblTotal.Text = String.Format("{0:0,0}", gia * e.NewValue);
        }
    }
}