using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LiveAdd.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowImagePage : Page
    {
        public ShowImagePage()
        {
            this.InitializeComponent();            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var imgSource = e.Parameter as ImageSource;
            this.AdvImage.Source = imgSource;
        }        

        private void OnCanvasManipulationDataEnabled(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var top = Canvas.GetTop(this.AdvImage);
            var left = Canvas.GetLeft(this.AdvImage);

            top += e.Delta.Translation.Y;
            left += e.Delta.Translation.X;

            var scale = e.Delta.Scale;

            var oldWidth = this.AdvImage.Width;
            var oldHeight = this.AdvImage.Height;

            this.AdvImage.Width *= scale;
            this.AdvImage.Height *= scale;

            top -= (this.AdvImage.Height - oldHeight) / 2;
            left -= (this.AdvImage.Width - oldWidth) / 2;

            Canvas.SetTop(this.AdvImage, top);
            Canvas.SetLeft(this.AdvImage, left);
        }
    }
}
