using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace vmstudio.Daten
{
    public enum SourceFileTyp
    {
        include,
        source,
        textfile,
        other
    }
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("Workspace")]
    public class SourceFile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public SourceFileTyp Type { get; set; }


        public SourceFile()
        {

        }
        public SourceFile(string strName, string strPath)
        {
            Name = strName;
            Path = strPath;
        }
        public string Open()
        {
            return File.ReadAllText(Path + "/" + Name);
        }
        public void Save(string strText)
        {
            using (FileStream st = new FileStream(Path + "/" + Name, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamWriter writter = new StreamWriter(st))
                    writter.Write(strText);
            }
        }
    }
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("Workspace")]
    public class WorkSpace
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<string> IncludePath { get; set; }
        public List<SourceFile> Files { get; set; }
        public WorkSpaceSettings Settings { get; set; }

        public WorkSpace() { }
        public WorkSpace(string strName, string strPath)
        {
            Name = strName; Path = strPath;
            IncludePath = new List<string>();
            Files = new List<SourceFile>();
            Settings = new WorkSpaceSettings();

            var includepath = System.IO.Path.Combine(Path, "include");
            System.IO.Directory.CreateDirectory(includepath);

            try
            {
                IncludePath.Add(includepath);
                Files.Add(new SourceFile("system.inc", includepath));
                Files.Add(new SourceFile("main.asm", Path));

                File.Copy("system.inc", System.IO.Path.Combine(includepath, "system.inc"));
                File.Copy("main.asm", System.IO.Path.Combine(Path, "main.asm"));
            }
            catch
            {

            }
            Save();
        }

        public void Save()
        {
            XmlSerializer xm = new XmlSerializer(typeof(WorkSpace));
            using (var sa = new FileStream(Path + "\\" + Name, FileMode.OpenOrCreate, FileAccess.Write))
            {
                xm.Serialize(sa, this);
            }
        }
        public static WorkSpace Open(string path)
        {
            XmlSerializer xm = new XmlSerializer(typeof(WorkSpace));
            WorkSpace sp = null;

            using (var sa = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                sp = (WorkSpace)xm.Deserialize(sa);
            }
            return sp;
        }
        public SourceFile getFile(string name)
        {
            SourceFile file = null;
            foreach (var item in Files)
            {
                if(item.Name == name)
                {
                    file = item;
                }
            }
            return file;
        }
    }

   
}
