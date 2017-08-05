using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vmstudio.Daten;

namespace vmstudio.asm
{
    public class TemporyFileOutput : IOutput
    {
        public Stream MakeOutput(byte[] daten,  WorkSpace space)
        {
            string name = space.getCurrentSettings().ConfigName;
            string path = "Memory";

            System.Console.WriteLine("[{1}]Raw output file written to: {0}",
                Path.Combine(path, "output.bin"), name);

            return new MemoryStream(daten);
        }
    }
}
