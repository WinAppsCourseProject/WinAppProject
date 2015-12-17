namespace LiveAdd.ViewModels
{
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

    }
}
