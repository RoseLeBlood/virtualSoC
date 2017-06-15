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

        }

        private void cmdNewFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdOpenWork_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSaveFile_Click(object sender, RoutedEventArgs e)
        {
            EditorSourceView view = tabView.SelectedContent as EditorSourceView;
            if (view != null)
            {
                view.Save();
                
            }
        }

        private void cmdBuild_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdUndo_Click(object sender, RoutedEventArgs e)
        {
            EditorSourceView view = tabView.SelectedContent as EditorSourceView;
            if(view != null)
            {
                view.txtSource.Undo();
            }
        }

        private void cmdRedo_Click(object sender, RoutedEventArgs e)
        {
            EditorSourceView view = tabView.SelectedContent as EditorSourceView;
            if (view != null)
            {
                view.txtSource.Redo();

            }
        }

        private void cmdCopy_Click(object sender, RoutedEventArgs e)
        {
            EditorSourceView view = tabView.SelectedContent as EditorSourceView;
            if (view != null)
            {
                view.txtSource.Copy();
            }
        }

        private void cmdPaste_Click(object sender, RoutedEventArgs e)
        {
            EditorSourceView view = tabView.SelectedContent as EditorSourceView;
            if (view != null)
            {
                view.txtSource.Paste();
            }
        }

        private void cmdCut_Click(object sender, RoutedEventArgs e)
        {
            EditorSourceView view = tabView.SelectedContent as EditorSourceView;
            if (view != null)
            {
                view.txtSource.Cut();
            }
        }

        private void cmdAddFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void trWorkspace_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (trWorkspace.SelectedItem == null) return;
           if(trWorkspace.SelectedItem is WorkgroupFile)
            {
                m_grFile = trWorkspace.SelectedItem as WorkgroupFile;
            }
        }

        private void ContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            if (sender is ContentControlTree)
            {
                string text = (sender as ContentControlTree).UserData;
                string name = text.Replace(".", "0");
                

                foreach (var item in this.tabView.Items)
                {
                    if((item as TabItem).Name == name)
                    {
                        tabView.SelectedItem = item;
                        return;
                    }
                }
                
                {
                    MetroTabItem item = new MetroTabItem();
                    item.Header = text;
                    item.Name = name;
                    item.CloseButtonEnabled = true;
                    
                    item.Background = new SolidColorBrush( (Color)ColorConverter.ConvertFromString("#FF444444"));
                    EditorSourceView view = new EditorSourceView(m_grFile);
                    item.Content = view;

                    this.tabView.SelectedIndex =
                        tabView.Items.Add(item);
                }

            }
        }
    }
}
