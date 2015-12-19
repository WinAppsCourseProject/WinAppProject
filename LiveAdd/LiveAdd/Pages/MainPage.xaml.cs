// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LiveAdd
{
    using LiveAdd.ViewModels;
    using Pages;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.ViewModel = new MainPageViewModel();
        }

        public MainPageViewModel ViewModel
        {
            get
            {
                return this.DataContext as MainPageViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void btnMenuPanel_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.IsPaneOpen = !MenuPanel.IsPaneOpen;
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuPanelView_LogOut(object sender, System.EventArgs e)
        {
            this.ViewModel.LogOut();
            this.Frame.Navigate(typeof(LoginPage));
        }

        private void MenuPanelView_CreateNewAdv(object sender, System.EventArgs e)
        {
            this.Frame.Navigate(typeof(AddContentPage));
        }
    }
}
