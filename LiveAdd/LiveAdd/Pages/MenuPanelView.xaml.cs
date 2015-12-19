using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace LiveAdd.Pages
{
    public sealed partial class MenuPanelView : UserControl
    {
        public event EventHandler LogOut;
        public MenuPanelView()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
        private void OnLogoutButtonClicked(object sender, RoutedEventArgs e)
        {
            if (this.LogOut != null)
            {
                this.LogOut(this, null);
            }
        }

    }
}
