using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using vmstudio.Daten;

namespace vmstudio.Debug
{
    internal static class viConsole
    {
        static DebugWritter m_txtWritter; 

        public static void Setup(TextBox box)
        {
            m_txtWritter = new DebugWritter(box);
            Console.SetOut(m_txtWritter);

            Console.WriteLine("viSoC Studio 2018 started ...");
            Console.WriteLine("Version 0.3.11 Beta GPL by annasophia.schroeck@outlook.de ");
        }
        public static void Error(string text)
        {
            Console.WriteLine("> ERROR: {0}", text);
        }
        public static void Info(string text)
        {
            Console.WriteLine("> INFO: {0}", text);
        }
        public static void Critical(string text)
        {
            Console.WriteLine("> CRITICAL: {0}", text);
        }
    }
}
