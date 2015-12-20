namespace LiveAdd.ViewModels
{
    using System;
    using System.Threading.Tasks;

    using Helpers;
    using Parse;

    public class LoginPageViewModel : ViewModelBase
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ServerErrorMessage { get; set; }

        public bool ValidateInput()
        {
            if (!InputValidator.ValidateRegularTextInput(this.Username))
            {
                return false;
            }

            if (!InputValidator.ValidatePasswordTextInput(this.Password))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SignIn()
        {
            try
            {
                await ParseUser.LogInAsync(this.Username, this.Password);
                return true;
            }
            catch (Exception ex)
            {
                this.ServerErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
