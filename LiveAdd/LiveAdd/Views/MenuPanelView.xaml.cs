using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace LiveAdd.Views
{
    public sealed partial class MenuPanelView : UserControl
    {
        public event EventHandler LogOut;

        public event EventHandler CreateNewAdv;

        public event EventHandler GoHomePage;
        public MenuPanelView()
        {
            this.InitializeComponent();
        }

        private void OnLogoutButtonClicked(object sender, RoutedEventArgs e)
        {
            if (this.LogOut != null)
            {
                this.LogOut(this, null);
            }
        }

        private void CreateNewAddv_Click(object sender, RoutedEventArgs e)
        {
            if (this.CreateNewAdv != null)
            {
                this.CreateNewAdv(this, null);
            }
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            if (this.GoHomePage != null)
            {
                this.GoHomePage(this, null);
            }
        }
    }
}
