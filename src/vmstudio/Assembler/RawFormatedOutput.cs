using System.IO;

namespace vmstudio.asm
{
    public class RawFormatedOutput : IOutput
    {
        public Stream MakeOutput(byte[] daten, Daten.WorkSpace space)
        {
            string name = space.getCurrentSettings().ConfigName;
            string path = Path.Combine(space.Path, "bin", name);

            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            Stream str = new FileStream(Path.Combine(path, "output.bin"), FileMode.Create, FileAccess.ReadWrite);
            str.Write(daten, 0, daten.Length);
            str.Position = 0;

            System.Console.WriteLine("[{1}]Raw output file written to: {0}", 
                Path.Combine(path, "output.bin"), name);
            return str;
        }
    }
}
