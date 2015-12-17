namespace LiveAdd.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Extensions;
    using LiveAdd.Helpers;


    public class AddContentViewModel : ViewModelBase, IContentViewModel
    {
        public ObservableCollection<AddViewModel> advertisements;
        private ICommand publishCommand;


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

        public ICommand Publish
        {
            get
            {
                if (this.publishCommand == null)
                {
                    this.publishCommand = new DelegateCommand<AddViewModel>((newAdvertisement) =>
                    {
                        this.advertisements.Add(new AddViewModel(newAdvertisement));
                    }
                    );
                }
                return this.publishCommand;
            }
        }
    }
}
