namespace LiveAdd.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Parse;

    public class UserViewModel : ViewModelBase
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }
    }
}
