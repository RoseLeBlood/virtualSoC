using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vmasm;

namespace vmstudio.asm
{
    class Compiler
    {
        Assembler m_asm;

        
        internal byte[] Compile(string input, string[] include)
        {
            m_asm = new Assembler();
            m_asm.l = PreProcess(input, include);
            byte[] data = m_asm.Comp();

            return data;
        }
        private static string[] PreProcess(string input, string[] include)
        {
            //System.IO.File.WriteAllText(".output.tmp", System.IO.File.ReadAllText (input));
            string[] text = System.IO.File.ReadAllLines(input);
            for (int i = 0; i < text.Length; i++)
            {
                string item = text[i];
                if (CheakLineOfInclude(item))
                {
                    string _incText = GetFileFromPaths(GetSubstringByString('\'', item), include);

                    text[i] = _incText;
                }
            }
            System.IO.File.WriteAllLines(".comp.tml", text);
            string[] l = System.IO.File.ReadAllLines(".comp.tml");
            System.IO.File.Delete(".comp.tml");

            return l;
        }
        private static bool CheakLineOfInclude(string li)
        {
            return li.Contains("#include");
        }
        private static string GetSubstringByString(char a, string c)
        {
            return c.Split(a)[1];
        }
        private static string GetFileFromPaths(string file, string[] paths)
        {
            foreach (var i in paths)
            {
                string path = System.IO.Path.Combine(i, file);
                if (System.IO.File.Exists(path))
                {
                    return System.IO.File.ReadAllText(path);
                }
            }
            throw new Exception("File " + file + " not found");
        }
    }
}
