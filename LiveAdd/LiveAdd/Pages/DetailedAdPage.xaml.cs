using LiveAdd.ViewModels;
using Parse;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LiveAdd.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailedAdPage : Page
    {
        public DetailedAdPage()
        {
            this.InitializeComponent();

            this.ViewModel = new AddViewModel();
        }

        public AddViewModel ViewModel
        {
            get
            {
                return this.DataContext as AddViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.ViewModel = e.Parameter as AddViewModel;
        }

        private void btnMenuPanel_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.IsPaneOpen = !MenuPanel.IsPaneOpen;
        }

        private void MenuPanelView_CreateNewAdv(object sender, System.EventArgs e)
        {
            this.Frame.Navigate(typeof(AddContentPage));
        }
        private void MenuPanelView_LogOut(object sender, System.EventArgs e)
        {
            ParseUser.LogOut();
            this.Frame.Navigate(typeof(LoginPage));
        }

        private void MenuPanelView_GoToHomePage(object sender, System.EventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
