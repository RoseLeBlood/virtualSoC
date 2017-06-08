using Dragablz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vmstudio.Views
{
    /// <summary>
    /// Interaktionslogik für EditorView.xaml
    /// </summary>
    public partial class EditorView : UserControl
    {       
        public EditorView()
        {
            InitializeComponent();
            List<Workgroup> lst = new List<Workgroup>();
            lst.Add(new Workgroup(new Daten.WorkSpace("test", "test")));
            trWorkspace.ItemsSource = lst;


           // Daten.SourceFile file = Daten.CurrentWorkspace.Instance.Current.getFile("main.asm");
           // if (file != null) txtSource.Text = file.Open();
        }

        private void cmdNewFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdOpenWork_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSaveFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdBuild_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
