using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace vmstudio.Views
{
    /// <summary>
    /// Interaktionslogik für CreateOpenWorkspace.xaml
    /// </summary>
    public partial class CreateOpenWorkspace : MetroWindow
    {
        public CreateOpenWorkspace()
        {
            InitializeComponent();
            
        }

        private void cmdCreate_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Title = "Create Workspace";
            dialog.AddExtension = true;
            
            dialog.Filter = "viSoC 2018 Workspave | *.viss";
            //dialog.SafeFileName = "workspace.viss";

            var ret = dialog.ShowDialog();

            if (ret.HasValue)
            {
                if (ret.Value)
                {
                    string strfile = dialog.FileName;
                    string strName = System.IO.Path.GetFileName(strfile);
                    string path = strfile.Replace(strName, "");
                    if (!System.IO.File.Exists(strfile))
                        Daten.WorkSpaceHelper.Instance.Current = new Daten.WorkSpace(strName, path);
                    else
                        Daten.WorkSpaceHelper.Instance.Open(strfile);

                    this.Close();
                }
            }
        }

        private void cmdOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow main = new vmstudio.MainWindow();
            main.Show();
        }
    }
}
