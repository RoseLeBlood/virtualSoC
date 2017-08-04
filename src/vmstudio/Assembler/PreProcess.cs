using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vmasm;

namespace vmstudio.asm
{
    class PreProcess
    {
        public static void Process(ref Handler hdl)
        {
            string[] text = hdl.SourceCode;

            for (int i = 0; i < hdl.SourceCode.Length; i++)
            {
                string item = text[i];
                if (CheakLineOfInclude(item))
                {
                    string _incText = GetFileFromPaths(GetSubstringByString('\'', item), hdl.IncludePaths);

                    text[i] = _incText;
                }
            }
            System.IO.File.WriteAllLines(".comp.tml", text);
            hdl.PreCompiledSourceCode = System.IO.File.ReadAllLines(".comp.tml");
            System.IO.File.Delete(".comp.tml");
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
