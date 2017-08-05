using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vmstudio.Daten;

namespace vmstudio.asm
{
    public class GzipOutputFile : IOutput
    {
        public Stream MakeOutput(byte[] daten,  WorkSpace space)
        {
            string name = space.getCurrentSettings().ConfigName;
            string path = Path.Combine(space.Path, "bin", name);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            Stream str = new FileStream(Path.Combine(path, "output.bin.gz"), FileMode.Create, FileAccess.ReadWrite);
            GZipStream gstr = new GZipStream(str, CompressionLevel.Optimal, false);
            gstr.Write(daten, 0, daten.Length);
            gstr.Position = 0;


            System.Console.WriteLine("[{1}]gzip output file written to: {0}",
                Path.Combine(path, "output.bin.gz"), name);

            return gstr;

        }
    }
}
