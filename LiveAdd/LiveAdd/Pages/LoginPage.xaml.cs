// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LiveAdd.Pages
{
    using Helpers;
    using LiveAdd.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();

            this.ViewModel = new LoginPageViewModel();
        }

        public LoginPageViewModel ViewModel
        {
            get
            {
                return this.DataContext as LoginPageViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void OnSignUpButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignUpPage));
        }

        private void OnLoginButtonClicked(object sender, RoutedEventArgs e)
        {
            this.signInUser();
        }

        private async void signInUser()
        {
            bool singInSuccess = await this.ViewModel.SignIn();
            if (singInSuccess)
            {
                Notifier.ShowNotification("Successfully logged in");
                this.Frame.Navigate(typeof(AddContentPage));
            }
            else
            {
                // TODO - see if we can do something about this error
                //Notifier.ShowNotification(this.ViewModel.ServerErrorMessage);

                Notifier.ShowNotification("Incorrect login parameters");
            }
        }
    }
}
