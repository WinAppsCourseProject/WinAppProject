namespace LiveAdd.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Windows.Devices.Geolocation;

    using Extensions;
    using LiveAdd.Helpers;
    using Parse;
    using Models;
    using System;
    public class AddContentViewModel : ViewModelBase
    {
        static Geolocator geolocator = new Geolocator();

        private ICommand publishCommand;

        public string ServerErrorMessage { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }  

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
