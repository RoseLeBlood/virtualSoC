namespace vmstudio.asm
{
    static class OutputFactory
    {
        public static IOutput GetOutputFormater(Daten.WorkSpace current)
        {
            Daten.WorkSpaceSettings output = current.getCurrentSettings();
            if (output == null) return null;

            switch (output.BinaryOutput)
            {
                case Daten.WorkSpaceOuput.RawFile:
                    return new RawFormatedOutput();
                case Daten.WorkSpaceOuput.TemporyFile:
                    return new TemporyFileOutput();
                case Daten.WorkSpaceOuput.ZipedFile:
                    return new GzipOutputFile();
            }
            return null;
        }
    }
}
