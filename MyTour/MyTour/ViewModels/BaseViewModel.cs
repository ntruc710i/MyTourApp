using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using MyTour.Models;
using MyTour.Services;

namespace MyTour.ViewModels
{
    public class BaseViewModel : BindableObject
    {
        //public IToursData<Tour> ToursData => DependencyService.Get<IToursData<Tour>>();
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        protected bool SetProperty<T>(ref T backingStore, T value,
                [CallerMemberName] string propertyName = "",
                Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
 }
