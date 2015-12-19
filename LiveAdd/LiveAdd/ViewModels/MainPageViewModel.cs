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
        // Gives us radius of 50 km
        private const int MaximumDistanceInRadians = 50/6371;

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

            var parseGeoPoint = new ParseGeoPoint(latitude, longitude);
            var parseGeoDistance = new ParseGeoDistance(MaximumDistanceInRadians);

            var ads = await new ParseQuery<AddModel>()
                .Where(a => a.IsActive == true)
                .WhereWithinDistance("location", parseGeoPoint, parseGeoDistance)
                .FindAsync();

            this.Advertisements = ads.AsQueryable().Select(AddViewModel.FromModel);
        }
    }
}
