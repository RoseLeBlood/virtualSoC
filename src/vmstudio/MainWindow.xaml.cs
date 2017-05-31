using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
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

namespace vmstudio
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            IHighlightingDefinition asmSyntax;
            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create("./Syntax/asm.xshd"))
                asmSyntax = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            HighlightingManager.Instance.RegisterHighlighting("ASM", new string[] { "" }, asmSyntax);

            InitializeComponent();
        }

        private void cmdSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtSourceCode.Text = "#include 'vmcpu.inc'\n" +
                                      ".dim testVar 'Hallo Welt'\n" +
                                      ".dim arNumber d0\n\n" +
                                      "CALL.InitFramebuffer; Init Framebuffer define in vmcpu.inc\n"+
                                        "MOV AX,#h34 ; Move 0x34 to AX\n" +
                                        "ADD #d148   ; Add 2d to AX = 200d\n\n" +
                                        "FBSET AX,AX,#h00FF00 ; SetPixel to (200)x(200) in green\n"+
                                        "HLT";
            clsLog.WriteOutput("viSoC Studio 2018 loaded", Colors.White);
        }
    }
}
