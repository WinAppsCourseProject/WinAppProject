using System;
using LiveAdd.ViewModels;
using Parse;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LiveAdd.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserAdsPage : Page
    {
        public UserAdsPage()
        {
            this.InitializeComponent();

            this.ViewModel = new UserAdsPageViewModel();
        }

        public UserAdsPageViewModel ViewModel
        {
            get
            {
                return this.DataContext as UserAdsPageViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void ListView_Holding(object sender, HoldingRoutedEventArgs e)
        {
            var dpObject = e.OriginalSource as DependencyObject;
            var selectedListViewItem = VisualTreeHelper.GetParent(dpObject);
            while (!(selectedListViewItem is Grid))
            {
                dpObject = selectedListViewItem;
                selectedListViewItem = VisualTreeHelper.GetParent(dpObject);
            }
            var selected = (selectedListViewItem as Grid).DataContext as AddViewModel;

            this.ViewModel.SelectedAdd = selected;
            this.ViewModel.IsRemoveBtnVisible = true;
        }

        private void btnMenuPanel_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.IsPaneOpen = !MenuPanel.IsPaneOpen;
        }

        private void MenuPanelView_LogOut(object sender, System.EventArgs e)
        {
            ParseUser.LogOut();
            this.Frame.Navigate(typeof(LoginPage));
        }

        private void MenuPanelView_GoHomePage(object sender, EventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
