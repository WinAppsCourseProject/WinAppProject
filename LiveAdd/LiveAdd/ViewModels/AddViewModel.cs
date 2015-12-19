namespace LiveAdd.ViewModels
{
    using Models;
    using Parse;
    using System;
    using System.Linq.Expressions;
    public class AddViewModel : ViewModelBase
    {
        public static Expression<Func<AddModel, AddViewModel>> FromModel
        {
            get
            {
                return model => new AddViewModel()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Creator = model.Creator,
                    Worker = model.Worker,
                    ImgUrl = model.Image.Url.AbsoluteUri,
                    Address = "Somewhere" // This to be done using the google reverse geolocation api in some static service class
                };
            }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public double Price { get; set; }

        public string Address { get; set; }

        public ParseUser Creator { get; set; }

        public ParseUser Worker { get; set; }
    }
}