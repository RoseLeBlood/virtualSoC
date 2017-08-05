using System;
using System.IO;

namespace vmstudio.asm
{
    class Compiler
    {
        public static bool Compiling(ref Handler pHandler)
        {
            try
            {
                vmasm.Assembler asm = new vmasm.Assembler();
                byte[] binary = asm.Comp(pHandler.PreCompiledSourceCode, false);

                IOutput output = OutputFactory.GetOutputFormater(pHandler.Workspace.WorkSpace);
                using (Stream stream = output.MakeOutput(binary, pHandler.Workspace.WorkSpace))
                {
                    pHandler.Binary = new MemoryStream();
                    stream.CopyTo(pHandler.Binary);
                    stream.Close();
                }

                    return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
                return false;
            } 
        }
    }
}
