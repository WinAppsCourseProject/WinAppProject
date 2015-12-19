// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
namespace LiveAdd.Pages
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using LiveAdd.ViewModels;
    using Windows.UI.Popups;
    using Helpers;    /// <summary>
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
                Notifier.ShowNotification("All input fields are required.");
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
                Notifier.ShowNotification("You successfully registered.");
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                // TODO - see if we can do something about that error
                //Notifier.ShowNotification(this.ViewModel.ServerErrorMessage);

                Notifier.ShowNotification("User with same parameters already exists");
            }
        }
    }
}
