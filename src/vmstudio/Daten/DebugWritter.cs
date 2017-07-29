using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace vmstudio.Daten
{
    /// <summary>
    /// TextWriter ausgabe in eine TextBox (Debug Console)
    /// </summary>
    public class DebugWritter : TextWriter
    {
        TextBox m_txtOutput = null; 

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
        public DebugWritter(TextBox output) { m_txtOutput = output; }

        public override void Write(char value)
        {
            base.Write(value);
            
            m_txtOutput.Dispatcher.BeginInvoke(new Action(() =>
            {
                m_txtOutput.AppendText(value.ToString());
            }));
        }
    }
}
