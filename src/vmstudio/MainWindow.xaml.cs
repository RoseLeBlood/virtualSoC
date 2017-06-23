using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace vmstudio
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        DispatcherTimer m_timer;

        public MainWindow()
        {
            IHighlightingDefinition asmSyntax;
            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create("./Syntax/asm.xshd"))
                asmSyntax = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            HighlightingManager.Instance.RegisterHighlighting("ASM", new string[] { "" }, asmSyntax);

            

            InitializeComponent();
            m_timer = new DispatcherTimer();
            m_timer.Tick += new EventHandler(uhrzeit_timertick);
            m_timer.Interval = new TimeSpan(0, 0, 1);
            m_timer.Start();
        }
        private void uhrzeit_timertick(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm");

            CommandManager.InvalidateRequerySuggested();
            lblclock.Text = time;

            m_timer.Start();
        }


        private void cmdSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {

          /* this.txtSourceCode.Text = "#include 'vmcpu.inc'\n" +
                                      ".dim testVar 'Hallo Welt'\n" +
                                      ".dim arNumber d0\n\n" +
                                      "CALL.InitFramebuffer; Init Framebuffer define in vmcpu.inc\n"+
                                        "MOV AX,#h34 ; Move 0x34 to AX\n" +
                                        "ADD #d148   ; Add 2d to AX = 200d\n\n" +
                                        "FBSET AX,AX,#h00FF00 ; SetPixel to (200)x(200) in green\n"+
                                        "HLT";*/
           // clsLog.WriteOutput("viSoC Studio 2018 loaded", Colors.White);

            /*avManagerRoot.DocumentsSource*/
            
        }
        

        private void cmdNewProject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdOpenProject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdOpenFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdNewFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSaveAll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdViewRam_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdViewCores_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdViewRam_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void cmdViewHexDump_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HamburgerMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.HamburgerMenuControl.Content = e.ClickedItem;
            // close the pane
            this.HamburgerMenuControl.IsPaneOpen = false;
        }
    }
}
