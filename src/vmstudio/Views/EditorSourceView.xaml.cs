using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaktionslogik für EditorSourceView.xaml
    /// </summary>
    public partial class EditorSourceView : UserControl
    {
        private WorkgroupFile m_grFile;

        public EditorSourceView()
        {
            InitializeComponent();
        }

        public EditorSourceView(WorkgroupFile m_grFile)
        {
            this.m_grFile = m_grFile;
            
            InitializeComponent();
            txtSource.Text = m_grFile.Text;
        }

        private void txtSource_TextChanged(object sender, EventArgs e)
        {
            m_grFile.Text = txtSource.Text;
        }

        internal void Save()
        {
            m_grFile.Text = txtSource.Text;
            m_grFile.Save();

            (Application.Current.MainWindow as MetroWindow).ShowMessageAsync("virtual SoC Studio 2018 ",
                string.Format("File {0} saved", m_grFile.Name));
        }
    }
}
