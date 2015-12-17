namespace LiveAdd.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IPageViewModel
    {
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
