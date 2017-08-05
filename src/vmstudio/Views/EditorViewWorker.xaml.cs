using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace vmstudio.Views
{
    public partial class EditorView : UserControl
    {
       
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            asm.Handler pHandler = (asm.Handler)e.Argument;

            asm.PreProcess.Process(ref pHandler);
            (sender as BackgroundWorker).ReportProgress(30);

            try
            {
                asm.Compiler.Compiling(ref pHandler);
                (sender as BackgroundWorker).ReportProgress(100);
            }
            catch (Exception exp)
            {

            }
            System.Threading.Thread.Sleep(1);
            e.Result = pHandler;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("{0} %", e.ProgressPercentage);
            ((MainWindow)(App.Current.MainWindow)).prgStatus.Value = e.ProgressPercentage;
            //progressBar1.Value = e.ProgressPercentage;
        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.m_pHandler = (asm.Handler)e.Result;
            this.IsEnabled = true;
        }


    }
}
