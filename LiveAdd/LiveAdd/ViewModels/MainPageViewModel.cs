namespace LiveAdd.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Extensions;
    using Helpers;
    using Models;
    using Parse;
    using Windows.Devices.Geolocation;
    using System.Windows.Input;

    public class MainPageViewModel : ViewModelBase
    {
        private const int EarthRadius = 6371;
        private const int MaximumDistanceInKilometers = 50;

        private static Geolocator geolocator = new Geolocator();
        private double latitude;
        private double longitude;

        private ObservableCollection<AddViewModel> searchResults;
        private ObservableCollection<AddViewModel> advertisements;
        private bool isLoading;
        private int count;
        private ICommand searchCommand;
        private string searchParams;
        private ICommand showAllCommand;

        public MainPageViewModel()
        {
            this.LoadAddsAsync();
        }

        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                this.isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        public ICommand ShowAllCommand
        {
            get
            {
                if (this.showAllCommand == null)
                {
                    this.showAllCommand = new DelegateCommand(this.ExecuteShowAllCommand);
                }

                return this.showAllCommand;
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (this.searchCommand == null)
                {
                    this.searchCommand = new DelegateCommand(this.ExecuteSearchCommand);
                }

                return this.searchCommand;
            }
        }

        public string SearchParams
        {
            get { return this.searchParams; }
            set
            {
                this.searchParams = value;
                RaisePropertyChanged("SearchParams");
            }
        }

        public int Count
        {
            get {
                this.count = this.Advertisements.Count();
                return this.count;
            }
            set
            {
                this.count = value;
                RaisePropertyChanged("Count");
            }
        }

        public IEnumerable<AddViewModel> SearchResults
        {
            get
            {
                if (this.searchResults == null)
                {
                    this.searchResults = new ObservableCollection<AddViewModel>();
                }
                return this.searchResults;
            }
            set
            {
                if (this.searchResults == null)
                {
                    this.searchResults = new ObservableCollection<AddViewModel>();
                }
                this.searchResults.Clear();
                value.ForEach(this.searchResults.Add);
            }
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

        private void ExecuteShowAllCommand()
        {
            this.SearchResults = this.Advertisements.ToList();
            this.Count = this.SearchResults.Count();
        }

        private void ExecuteSearchCommand()
        {
            if (string.IsNullOrEmpty(this.SearchParams))
            {
                return;
            }
            else
            {
                this.SearchResults = this.Advertisements.Where(a => a.Name.ToLower().IndexOf(this.SearchParams.ToLower()) != -1
                                                                     || a.Description.ToLower().IndexOf(this.SearchParams.ToLower()) != -1)
                                                         .ToList();
                this.Count = this.SearchResults.Count();
            }
        }

        private async void LoadAddsAsync()
        {
            this.IsLoading = true;

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
            
            var users = await new ParseQuery<ParseUser>().FindAsync();

            foreach (var ad in this.Advertisements)
            {
                ad.Creator = users.AsQueryable()
                    .Where(u => u.ObjectId == ad.Creator.ObjectId)
                    .FirstOrDefault();
            }

            this.SearchResults = this.Advertisements.ToList();

            this.Count = this.SearchResults.Count();
            this.IsLoading = false;
        }

        private bool CheckAvailableAds(AddModel model)
        {
            var parseLatitude = model.Location.Latitude;
            var parseLongitude = model.Location.Longitude;

            var dlat = (latitude - parseLatitude) / 180 * Math.PI;
            var dlon = (longitude - parseLongitude) / 180 * Math.PI;
            var x = (dlon) * Math.Cos((latitude + parseLatitude) / 2 / 180 * Math.PI);
            var d = Math.Sqrt(x * x + dlat * dlat) * EarthRadius;

            return model.IsActive == 1 && d <= MaximumDistanceInKilometers
                                       && model.Worker == null;
        }
    }
}
