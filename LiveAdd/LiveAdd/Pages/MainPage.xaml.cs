﻿// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LiveAdd
{
    using System.Collections.Generic;
    using LiveAdd.ViewModels;
    using Windows.UI.Xaml.Controls;


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var contentViewModel = new AddContentViewModel();
            contentViewModel.Advertisements = new List<AddViewModel>()
                {
                    new AddViewModel("Mercedes", "https://istatic.bazar.bg/photosbazar/20/pics/208773bbb7017a97f2743a2b6211b339.jpg", 200),
                    new AddViewModel("Audi", "https://istatic.bazar.bg/photosbazar/20/pics/208773bbb7017a97f2743a2b6211b339.jpg", 500),
                    new AddViewModel("BMW", "https://istatic.bazar.bg/photosbazar/20/pics/208773bbb7017a97f2743a2b6211b339.jpg", 600),
                    new AddViewModel("Mazda", "https://istatic.bazar.bg/photosbazar/20/pics/208773bbb7017a97f2743a2b6211b339.jpg", 1000)

                };
            this.DataContext = new MainPageViewModel(contentViewModel);
        }
    }
}
