namespace LiveAdd.ViewModels
{
    using System;
    using System.Windows.Input;
    using LiveAdd.Helpers;
    using Models;
    using Parse;
    using Windows.Devices.Geolocation;
    public class AddContentViewModel : ViewModelBase
    {
        static Geolocator geolocator = new Geolocator();

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

        public double Price
        {
            get { return this.price; }
            set
            {
                this.price = value;
                this.RaisePropertyChanged("Price");
            }
        }

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
        
        private void ExecuteTakePictureCommand()
        {
            throw new NotImplementedException();
        }

        private async void ExecutePublishCommand()
        {
            try
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

                var add = new AddModel()
                {
                    Creator = ParseUser.CurrentUser,
                    Name = this.Name,
                    Price = this.Price,
                    IsActive = 1,
                    Description = this.Description,
                    Location = new ParseGeoPoint(latitude, longitude)
                };

                // TODO image uploading;

                //if (this.Image != null)
                //{
                //    // If the file path and name is entered properly, and user has not tapped 'cancel'..
                //    using (IRandomAccessStream stream = await this.Image.OpenReadAsync())
                //    {
                //        // Save to Parse procedure
                //        RandomAccessStreamReference rasr = RandomAccessStreamReference.CreateFromStream(stream);
                //        var streamWithContent = await rasr.OpenReadAsync();
                //        byte[] buffer = new byte[streamWithContent.Size];

                //        try
                //        {
                //            await streamWithContent.ReadAsync(buffer.AsBuffer(), (uint)streamWithContent.Size, InputStreamOptions.None);
                //            var data = buffer;

                //            if (data != null)
                //            {
                //                var pic = new Parse.ParseFile("pic.jpg", data);
                //                carpool.Image = pic;
                //            }
                //        }
                //        catch (Exception)
                //        {
                //            //TODO:
                //        }
                //    }
                //}

                await add.SaveAsync();

                Notifier.ShowNotification("You successfully added you advertisement.");
            }
            catch (Exception ex)
            {
                Notifier.ShowNotification(ex.Message);
            }
        }
    }
}
