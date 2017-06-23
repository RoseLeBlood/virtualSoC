using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace vmstudio
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        internal FileStream StdOut { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            
        }
        
    }
}
