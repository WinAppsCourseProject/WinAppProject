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
        private double latitude;
        private double longitude;

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
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus != GeolocationAccessStatus.Allowed)
            {
                Notifier.ShowNotification("Unauthorized access to location.");
            }
            else
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync();
                this.latitude = geoposition.Coordinate.Point.Position.Latitude;
                this.longitude = geoposition.Coordinate.Point.Position.Longitude;
            }

            var ads = await new ParseQuery<AddModel>().FindAsync();

            this.Advertisements = ads.AsQueryable()
                .Where(a => this.CheckAvailableAds(a))
                .Select(AddViewModel.FromModel);
        }

        private bool CheckAvailableAds(AddModel model)
        {
            var parseLatitude = model.Location.Latitude;
            var parseLongitude = model.Location.Longitude;

            var dlat = (latitude - parseLatitude) / 180 * Math.PI;
            var dlon = (longitude - parseLongitude) / 180 * Math.PI;
            var x = (dlon) * Math.Cos((latitude + parseLatitude) / 2 / 180 * Math.PI);
            var d = Math.Sqrt(x * x + dlat * dlat) * EarthRadius;

            return model.IsActive == 1 && d <= MaximumDistanceInKilometers;
        }
    }
}
