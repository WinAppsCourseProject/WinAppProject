namespace LiveAdd.ViewModels
{
    public interface IPageViewModel
    {
        string Title { get; }

        IContentViewModel ContentViewModel { get; set; }
    }
}
