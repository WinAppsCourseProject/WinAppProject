namespace LiveAdd.ViewModels
{
    using Helpers;
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
                    ImgUrl = model.Image == null ? "http://www.designofsignage.com/application/symbol/building/image/600x600/no-photo.jpg" : model.Image.Url.AbsoluteUri,
                    Address = model.Address
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