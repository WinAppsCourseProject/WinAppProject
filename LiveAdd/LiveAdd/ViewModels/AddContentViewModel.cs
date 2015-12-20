namespace LiveAdd.ViewModels
{
    using System;
    using System.Windows.Input;
    using LiveAdd.Helpers;
    using Models;
    using Parse;
    using Windows.Devices.Geolocation;
    using Windows.Media.Capture;
    using System.IO;
    public class AddContentViewModel : ViewModelBase
    {
        private static Geolocator geolocator = new Geolocator();
        private static readonly GoogleReverseGeolovationService addressService = new GoogleReverseGeolovationService();

        private ICommand publishCommand;
        private double price;
        private string imgUrl;
        private ICommand takePicture;

        public string ServerErrorMessage { get; set; }

        public string Name { get; set; }

        public string ImgUrl
        {
            // TODO this will change soon :)
            get { return this.imgUrl; }
            set
            {
                this.imgUrl = value;
                this.RaisePropertyChanged("ImgUrl");
            }
        }

        public string Description { get; set; }

        public string Address { get; set; }

        public double Price
        {
            get { return this.price; }
            set
            {
                // this takes care of both negative and non numeric values.
                if (value <= 0)
                {
                    this.price = -1;
                }
                else
                {
                    this.price = value;
                }

                this.RaisePropertyChanged("Price");
            }
        }

        public ParseFile Image { get; set; }

        public ICommand TakePicture
        {
            get
            {
                if (this.takePicture == null)
                {
                    this.takePicture = new DelegateCommand(this.ExecuteTakePictureCommand);
                }
                return this.takePicture;
            }
        }

        public ICommand Publish
        {
            get
            {
                if (this.publishCommand == null)
                {
                    this.publishCommand = new DelegateCommand(this.ExecutePublishCommand);
                }
                return this.publishCommand;
            }
        }
        
        private async void ExecuteTakePictureCommand()
        {
            var camera = new CameraCaptureUI();

            var photo = await camera.CaptureFileAsync(CameraCaptureUIMode.Photo);
            
            if (photo != null)
            {
                try
                {
                    this.ImgUrl = photo.Path;
                    Byte[] imageFile = File.ReadAllBytes(photo.Path);
                    this.Image = new ParseFile("somePhoto", imageFile, ".jpg");
                }
                catch (Exception ex)
                {
                    Notifier.ShowNotification(ex.Message);
                }
            }
            else
            {
                Notifier.ShowNotification("Unsuccessful shooting. Of a picture, no worries :)");
            }
        }

        private async void ExecutePublishCommand()
        {
            try
            {
                this.ValidateInput();

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

                this.Address = addressService.GetAddressFromCoordinates(latitude, longitude);

                var add = new AddModel()
                {
                    Creator = ParseUser.CurrentUser,
                    Name = this.Name,
                    Price = this.Price,
                    IsActive = 1,
                    Description = this.Description,
                    Address = this.Address,
                    Location = new ParseGeoPoint(latitude, longitude)
                };

                if (this.Image != null)
                {
                    add.Image = this.Image;
                }

                await add.SaveAsync();

                Notifier.ShowNotification("You successfully added you advertisement.");
            }
            catch (Exception ex)
            {
                Notifier.ShowNotification(ex.Message);
            }
        }

        private void ValidateInput()
        {
            if (!InputValidator.ValidateRegularTextInput(this.Name))
            {
                throw new ArgumentException("Invalid name - cannot be null ot empty.");
            }

            if (!InputValidator.ValidateRegularTextInput(this.Description))
            {
                throw new ArgumentException("Invalid description - cannot be null ot empty.");
            }

            if (!InputValidator.ValidatePriceInput(this.Price))
            {
                throw new ArgumentException("Invalid price - should be a positive number.");
            }
        }
    }
}
