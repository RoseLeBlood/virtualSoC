using Dragablz;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
        private WorkgroupFile m_grFile = null;

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
            m_grFile.Save();
            (Application.Current.MainWindow as MetroWindow).ShowMessageAsync("virtual SoC Studio 2018 ",
                string.Format("File {0} saved", m_grFile.Name));
        }

        private void cmdBuild_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSource_TextChanged(object sender, EventArgs e)
        {
            m_grFile.Text = txtSource.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdUndo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdRedo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdCopy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdPaste_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdCut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdNewFile_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void cmdAddFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdBuild_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void trWorkspace_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (trWorkspace.SelectedItem == null) return;
           if(trWorkspace.SelectedItem is WorkgroupFile)
            {
                m_grFile = trWorkspace.SelectedItem as WorkgroupFile;
                //MessageBox.Show(m_grFile.Text);
                txtSource.Text = m_grFile.Text;
            }
        }
    }
}
