// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
namespace LiveAdd.Pages
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using LiveAdd.ViewModels;
    using Windows.UI.Popups;
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            this.InitializeComponent();

            this.ViewModel = new SignUpPageViewModel();
        }

        public SignUpPageViewModel ViewModel
        {
            get
            {
                return this.DataContext as SignUpPageViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void OnSignUpButtonClick(object sender, RoutedEventArgs e)
        {
            bool isInputValid = this.ViewModel.ValidateInput();
            if (!isInputValid)
            {
                this.ShowInformationMessage("All input fields are required.");
                return;
            }
            else
            {
                this. RegisterUser();
            }
        }

        public async void RegisterUser()
        {
            bool singUpSuccess = await this.ViewModel.SignUp();
            if (singUpSuccess)
            {
                this.ShowInformationMessage("You successfully registered.");
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                this.ShowInformationMessage(this.ViewModel.ServerErrorMessage);
            }
        }

        private async void ShowInformationMessage(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}
