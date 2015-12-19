// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LiveAdd
{
    using System.Collections.Generic;
    using LiveAdd.ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml;
    using Pages;

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
        private void OnLogoutButtonClicked(object sender, RoutedEventArgs e)
        {
            this.ViewModel.LogOut();
            this.Frame.Navigate(typeof(LoginPage));
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
