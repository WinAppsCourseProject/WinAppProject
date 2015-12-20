// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LiveAdd
{
    using LiveAdd.ViewModels;
    using Pages;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

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
            this.MenuPanel.IsPaneOpen = !this.MenuPanel.IsPaneOpen;
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

        private void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var adsListBox = (sender as ListBox);
            var selectedObject = adsListBox.SelectedItem;
            this.Frame.Navigate(typeof(DetailedAdPage), selectedObject);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.SearchPanel.IsPaneOpen = !this.SearchPanel.IsPaneOpen;
        }

        private void MenuPanelView_GoToMyAddv(object sender, System.EventArgs e)
        {
            this.Frame.Navigate(typeof(UserAdsPage));
        }

        private void panelTitle_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var velocities = e.Velocities;
            if (velocities.Linear.X > 0)
            {
                this.MenuPanel.IsPaneOpen = !this.MenuPanel.IsPaneOpen;
            }
            else if (velocities.Linear.X < 0)
            {
                this.SearchPanel.IsPaneOpen = !this.SearchPanel.IsPaneOpen;
            }         
        }
    }
}
