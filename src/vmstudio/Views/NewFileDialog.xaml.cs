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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vmstudio.Views
{
    /// <summary>
    /// Interaktionslogik für NewFileDialog.xaml
    /// </summary>
    public partial class NewFileDialog : MetroWindow
    {
        public bool Success { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }

        public NewFileDialog()
        {
            InitializeComponent();
            Success = false;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            Success = false;
            this.Close();
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == string.Empty)
                return;
            Success = true;
            FileName = txtName.Text;
            Extension = ".asm";
            this.Close();
            
        }
    }
}
