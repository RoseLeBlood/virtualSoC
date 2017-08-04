using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmstudio.asm
{
    struct Handler
    {
        public string[] IncludePaths;
        public string[] SourceCode;
        public string[] PreCompiledSourceCode;

        public MemoryStream Binary;

        internal Handler(string[] include, string[] source)
        {
            IncludePaths = new string[include.Length];
            SourceCode = new string[source.Length];
            PreCompiledSourceCode = null;
            Binary = new MemoryStream();

            source.CopyTo(SourceCode, 0);
            include.CopyTo(IncludePaths, 0);
        }

#if DEBUG
        public void PreCompToConsole()
        {
            foreach (var item in PreCompiledSourceCode)
            {
                Console.WriteLine("Debug: {0}", item);
            }
        }
#endif
        internal string CompilingInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Binary length: {0} bytes", Binary.Length);
            return sb.ToString();
        }
    }
}
