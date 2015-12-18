namespace LiveAdd.ViewModels
{
    using Parse;
    using System;

    public class MainPageViewModel : ViewModelBase, IPageViewModel
    {
        public MainPageViewModel(IContentViewModel contentViewModel)
        {
            this.ContentViewModel = contentViewModel;
        }

        public string Title
        {
            get
            {
                return "LiveAdd";
            }
        }

        public IContentViewModel ContentViewModel { get; set; }

        public void LogOut()
        {
            ParseUser.LogOut();
        }
    }
}
