using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vmstudio.Views;

namespace vmstudio.asm
{
    struct Handler
    {
        public Workgroup Workspace;

        public string[] IncludePaths;
        public string[] SourceCode;
        public string[] PreCompiledSourceCode;

        public Stream Binary;

        internal Handler(Workgroup space)
        {
            Workspace = space;
            IncludePaths = space.WorkSpace.IncludePath.ToArray();
            SourceCode = space.getSource();

            PreCompiledSourceCode = null;
            Binary = null;

            
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
