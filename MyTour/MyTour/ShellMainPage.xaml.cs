using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTour
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellMainPage : Shell
    {
        public ShellMainPage()
        {
            InitializeComponent();
        }
    }
}