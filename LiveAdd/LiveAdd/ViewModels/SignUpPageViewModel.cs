namespace LiveAdd.ViewModels
{
    using System;
    using System.Threading.Tasks;

    using Helpers;
    using Parse;

    public class SignUpPageViewModel : ViewModelBase
    {
        public UserViewModel User { get; set; }

        public string ServerErrorMessage { get; set; }

        public SignUpPageViewModel()
        {
            this.User = new UserViewModel();
        }

        public bool ValidateInput()
        {
            if (!InputValidator.ValidateRegularTextInput(this.User.Username))
            {
                return false;
            }

            if (!InputValidator.ValidateEmailTextInput(this.User.Email))
            {
                return false;
            }

            if (!InputValidator.ValidatePasswordTextInput(this.User.Password))
            {
                return false;
            }

            if (!InputValidator.ValidatePhoneTextInput(this.User.Telephone))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SignUp()
        {
            var parseUser = new ParseUser()
            {
                Username = this.User.Username,
                Password = this.User.Password,
                Email = this.User.Email
            };

            parseUser["telephone"] = this.User.Telephone;
            try
            {
                await parseUser.SignUpAsync();
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
