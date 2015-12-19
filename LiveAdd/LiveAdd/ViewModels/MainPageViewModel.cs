namespace LiveAdd.ViewModels
{
    using Parse;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Windows.Devices.Geolocation;

    using Extensions;
    using System.Threading.Tasks;
    using Models;
    using Helpers;

    public class MainPageViewModel : ViewModelBase
    {
        private const int EarthRadius = 6371;
        private const int MaximumDistanceInKilometers = 50;

        private static Geolocator geolocator = new Geolocator();

        private ObservableCollection<AddViewModel> advertisements;

        public MainPageViewModel()
        {
            this.LoadAddsAsync();
        }

        public IEnumerable<AddViewModel> Advertisements
        {
            get
            {
                if (this.advertisements == null)
                {
                    this.advertisements = new ObservableCollection<AddViewModel>();
                }
                return this.advertisements;
            }
            set
            {
                if (this.advertisements == null)
                {
                    this.advertisements = new ObservableCollection<AddViewModel>();
                }
                this.advertisements.Clear();
                value.ForEach(this.advertisements.Add);
            }
        }

        public string Title
        {
            get
            {
                return "LiveAdd";
            }
        }

        public void LogOut()
        {
            ParseUser.LogOut();
        }

        private async void LoadAddsAsync()
        {
            double latitude = 0.0;
            double longitude = 0.0;

            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus != GeolocationAccessStatus.Allowed)
            {
                Notifier.ShowNotification("Unauthorized access to location.");
            }
            else
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync();
                latitude = geoposition.Coordinate.Point.Position.Latitude;
                longitude = geoposition.Coordinate.Point.Position.Longitude;
            }

            var ads = await new ParseQuery<AddModel>()
            .Where(a => this.CheckAvailableAds(a, latitude, longitude))
            .FindAsync();

            this.Advertisements = ads.AsQueryable().Select(AddViewModel.FromModel);
        }

        private bool CheckAvailableAds(AddModel model, double latitude, double longitude)
        {
            var parseLatitude = model.Location.Latitude;
            var parseLongitude = model.Location.Longitude;

            var dlat = (latitude - parseLatitude) / 180 * Math.PI;
            var dlon = (longitude - parseLongitude) / 180 * Math.PI;
            var x = (dlon) * Math.Cos((latitude + parseLatitude) / 2 / 180 * Math.PI);
            var d = Math.Sqrt(x * x + dlat * dlat) * EarthRadius;

            return model.IsActive == true && d <= MaximumDistanceInKilometers;
        }
    }
}
