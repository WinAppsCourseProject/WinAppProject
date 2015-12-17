using System.Collections.Generic;

namespace LiveAdd.ViewModels
{
    public class AddViewModel
    {
        private IEnumerable<AddViewModel> newAdvertisement;
        private decimal price;

        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public decimal Price { get; set; }

        public AddViewModel()
            : this(string.Empty, string.Empty, 0)
        {
        }

        public AddViewModel(AddViewModel newAdvertisement)
            : this(newAdvertisement.Name, newAdvertisement.ImgUrl, newAdvertisement.Price)
        {
        }

        public AddViewModel(string name, string imgUrl, decimal price)
        {
            this.Name = name;
            this.ImgUrl = imgUrl;
            this.Price = price;
        }
    }
}