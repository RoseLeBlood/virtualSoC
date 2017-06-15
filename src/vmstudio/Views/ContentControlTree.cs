using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace vmstudio.Views
{
    public class ContentControlTree : ContentControl
    {
        public static readonly DependencyProperty UserDataProperty =
            DependencyProperty.Register("UserData", typeof(string),
            typeof(ContentControlTree), new FrameworkPropertyMetadata(""));

        public string UserData
        {
            get { return (string)GetValue(UserDataProperty); }
            set { SetValue(UserDataProperty, value); }
        }
        public ContentControlTree() : base()
        {

        }
    }
}
