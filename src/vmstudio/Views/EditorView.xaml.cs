
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        private Workgroup m_current = null;
        private asm.Handler m_pHandler;

        public EditorView()
        {
            InitializeComponent();

            m_current = new Workgroup(new Daten.WorkSpace("test",
                 System.AppDomain.CurrentDomain.BaseDirectory + "test"));
            List<Workgroup> lst = new List<Workgroup>();
            lst.Add(m_current);
            trWorkspace.ItemsSource = lst;

        }

        private void cmdNewFile_Click(object sender, RoutedEventArgs e)
        {
            NewFileDialog dialog = new NewFileDialog();
            dialog.ShowDialog();

            if(dialog.Success)
            {  
                m_current.AddSourceFile(dialog.FileName, "");
            }
            
            
            List<Workgroup> lst = new List<Workgroup>();
            lst.Add(m_current);
            trWorkspace.ItemsSource = lst;
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
            this.IsEnabled = false;

            Console.WriteLine("Start build ...");

            foreach (var item in tabView.Items)
            {
                EditorSourceView view = item as EditorSourceView;
                if (view != null) view.Save(false);
            }

            m_pHandler = new asm.Handler(m_current);

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync(m_pHandler);

            


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
           
          
        }

        private void ContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WorkgroupFile file = null;
            if (trWorkspace.SelectedItem is WorkgroupFile)
            {
                file = trWorkspace.SelectedItem as WorkgroupFile;
            }
            else
                return;

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
                    EditorSourceView view = new EditorSourceView(file);
                    item.Content = view;

                    this.tabView.SelectedIndex =
                        tabView.Items.Add(item);
                }

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (trWorkspace.SelectedItem == null)
                return;
            WorkgroupFile file = trWorkspace.SelectedItem as WorkgroupFile;
            if (file == null) return;


            foreach (var item in this.tabView.Items)
            {
                if ((item as TabItem).Name == file.Name.Replace(".","0"))
                {
                    tabView.SelectedItem = item;
                    return;
                }
            }

            {
                MetroTabItem item = new MetroTabItem();
                item.Header = file.Name;
                item.Name = file.Name.Replace(".","0");
                item.CloseButtonEnabled = true;

                item.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF444444"));
                EditorSourceView view = new EditorSourceView(file);
                item.Content = view;

                this.tabView.SelectedIndex =
                    tabView.Items.Add(item);
            }
        }
    }
}
