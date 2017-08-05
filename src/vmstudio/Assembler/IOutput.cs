using System.IO;

namespace vmstudio.asm
{
    interface IOutput
    {
        Stream MakeOutput(byte[] daten, Daten.WorkSpace space);
    }
}
