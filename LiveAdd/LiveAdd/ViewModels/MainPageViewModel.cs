namespace LiveAdd.ViewModels
{
    using Parse;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Extensions;
    using System.Threading.Tasks;
    using Models;
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<AddViewModel> advertisements;

        public MainPageViewModel()
        {
            //this.LoadAddsAsync();
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
            var ads = await new ParseQuery<AddModel>()
                .Where(x => true)
                .FindAsync();

            this.Advertisements = ads.AsQueryable().Select(AddViewModel.FromModel);
        }
    }
}
