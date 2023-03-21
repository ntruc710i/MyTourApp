using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace MyTour.Controls
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (String.IsNullOrWhiteSpace(Source)) return null;

            string imageSource = String.Empty;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    {
                        imageSource = Source;
                        break;
                    }
                case Device.Android:
                    {
                        imageSource = Source;
                        break;
                    }
                case Device.UWP:
                    {
                        imageSource = String.Concat("Images/", Source);
                        break;
                    }
            }
            return ImageSource.FromFile(imageSource);
        }
    }
}
