﻿using MyTour.ViewModels;
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
    public partial class LienhePage : ContentPage
    {
        public LienhePage()
        {
            InitializeComponent();
            BindingContext = new LienheViewModel();
        }
    }
}