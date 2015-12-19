namespace LiveAdd.AttachedProperties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI.Xaml;
    public class SearchDownPanel
    {


        public static bool GetShowHideSearchBox(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowHideSearchBoxProperty);
        }

        public static void SetShowHideSearchBox(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowHideSearchBoxProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShowHideSearchBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowHideSearchBoxProperty =
            DependencyProperty.RegisterAttached("ShowHideSearchBox", typeof(bool), typeof(UIElement), new PropertyMetadata(0));


    }
}
