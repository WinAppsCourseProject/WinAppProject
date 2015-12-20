namespace LiveAdd.ViewModels
{
    using Helpers;
    using LiveAdd.Extensions;
    using Models;
    using Parse;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.Devices.Geolocation;
    public class UserAdsPageViewModel : ViewModelBase
    {
        private static Geolocator geolocator = new Geolocator();

        private double latitude;
        private double longitude;
        private ObservableCollection<AddViewModel> advertisements;
        private bool isLoading;
        private int count;
        private ICommand removeCommand;
        private AddViewModel selectedAdd;
        private bool isBtnVisible;

        public UserAdsPageViewModel()
        {
            this.LoadAddsAsync();
        }

        public bool IsRemoveBtnVisible
        {
            get { return this.isBtnVisible; }
            set
            {
                this.isBtnVisible = value;
                RaisePropertyChanged("IsRemoveBtnVisible");
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                if (this.removeCommand == null)
                {
                    this.removeCommand = new DelegateCommand(this.ExecuteRemoveCommand);
                }

                return this.removeCommand;
            }
        }

        public AddViewModel SelectedAdd
        {
            get { return this.selectedAdd; }
            set
            {
                this.selectedAdd = value;
                RaisePropertyChanged("SelectedAdd");
            }
        }

        public IEnumerable<AddViewModel> UserAdvertisements
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

        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                this.isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        public int Count
        {
            get
            {
                this.count = this.UserAdvertisements.Count();
                return this.count;
            }
            set
            {
                this.count = value;
                RaisePropertyChanged("Count");
            }
        }

        public string CreatorName
        {
            get
            {
                return ParseUser.CurrentUser.Username;
            }
        }

        public string CreatorPhone
        {
            get
            {
                return (string)ParseUser.CurrentUser["telephone"];
            }
        }

        private async void ExecuteRemoveCommand()
        {
            try
            {
                this.IsRemoveBtnVisible = false;
                var ads = await new ParseQuery<AddModel>().FindAsync();

                var adModel = ads.AsQueryable()
                                 .Where(a => a.Name == this.SelectedAdd.Name && a.Description == this.SelectedAdd.Description)
                                 .FirstOrDefault();

                adModel.IsActive = 0;

                await adModel.SaveAsync();

                Notifier.ShowNotification(string.Format("You successfully removed the add {0}.", this.SelectedAdd.Name));

                this.LoadAddsAsync();
            }
            catch (Exception e)
            {
                Notifier.ShowNotification(e.Message);
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

            this.UserAdvertisements = ads.AsQueryable()
                .Where(a => this.CheckAvailableAds(a))
                .Select(AddViewModel.FromModel);

            var users = await new ParseQuery<ParseUser>().FindAsync();

            foreach (var ad in this.UserAdvertisements)
            {
                if (ad.Worker != null)
                {
                    ad.Worker = users.AsQueryable()
                    .Where(u => u.ObjectId == ad.Worker.ObjectId)
                    .FirstOrDefault();
                }
            }

            this.Count = this.UserAdvertisements.Count();
            this.IsLoading = false;
        }

        private bool CheckAvailableAds(AddModel model)
        {
            return model.IsActive == 1 && model.Creator.ObjectId == ParseUser.CurrentUser.ObjectId;
        }
    }
}
