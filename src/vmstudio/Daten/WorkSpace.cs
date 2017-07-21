﻿using System;
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
        public bool IsMain { get; set; }

        public SourceFileTyp Type { get; set; }
        public List<string> FileLog { get; set; }

        public SourceFile()
        {
            FileLog = new List<string>();
        }
        public SourceFile(string strName, string strPath, bool ismain, SourceFileTyp type)
        {
            FileLog = new List<string>();
            Name = strName;
            Path = strPath;
            IsMain = ismain;
            Type = type;
            FileLog.Add(string.Format("[{0}] File Created", DateTime.Now.ToString()));
        }
        public string Open()
        {
            return File.ReadAllText(Path + "/" + Name);
        }
        public void Save(string strText)
        {
            var old = Open();
            var time = string.Format("[{0}] Save file old content: {1}", DateTime.Now.ToString(), old);
            FileLog.Add(time);

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
        public List<string> WorkspaceLog { get; set; }

        public WorkSpace() { }
        public WorkSpace(string strName, string strPath)
        {
            Name = strName; Path = strPath;
            IncludePath = new List<string>();
            Files = new List<SourceFile>();
            Settings = new WorkSpaceSettings();
            WorkspaceLog = new List<string>();

            var includepath = System.IO.Path.Combine(Path, "include");
            System.IO.Directory.CreateDirectory(includepath);

            try
            {
                IncludePath.Add(includepath);
                Files.Add(new SourceFile("system.inc", includepath, false, SourceFileTyp.include));
                Files.Add(new SourceFile("main.asm", Path, true, SourceFileTyp.source));

                File.Copy("system.inc", System.IO.Path.Combine(includepath, "system.inc"));
                File.Copy("main.asm", System.IO.Path.Combine(Path, "main.asm"));
            }
            catch
            {

            }
            Save();
        }
        public SourceFile AddFile(string name, string filename, SourceFileTyp type, bool main)
        {
            var _path = System.IO.Path.Combine(Path, "include");

            switch (type)
            {
                case SourceFileTyp.include:
                    _path = System.IO.Path.Combine(Path, "include"); break;
                case SourceFileTyp.source:
                    break;
                case SourceFileTyp.textfile:
                    _path = System.IO.Path.Combine(Path, "etc"); break;
                case SourceFileTyp.other:
                    _path = System.IO.Path.Combine(Path, "share"); break;
                default:
                    break;
            }
            SourceFile file = new SourceFile(name, _path, main, type);
            File.Copy(filename, System.IO.Path.Combine(Path, System.IO.Path.GetFileName(filename)));
            Files.Add(file);
            return file;
        }
        public SourceFile AddNewFile(string name, SourceFileTyp type, string text, bool main)
        {
            var path = System.IO.Path.Combine(Path, "include");

            switch (type)
            {
                case SourceFileTyp.include:
                    path = System.IO.Path.Combine(Path, "include"); break;
                case SourceFileTyp.source:
                    break;
                case SourceFileTyp.textfile:
                    path = System.IO.Path.Combine(Path, "etc"); break;
                case SourceFileTyp.other:
                    path = System.IO.Path.Combine(Path, "share"); break;                  
                default:
                    break;
            }

            File.WriteAllText(System.IO.Path.Combine(path, name), text);
           
            SourceFile file = new SourceFile(name, path, main, type);
            Files.Add(file);

            return file;
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
                    file = item; break;
                }
            }
            return file;
        }
        public SourceFile getMainFile()
        {
            SourceFile file = null;
            foreach (var item in Files)
            {
                if (item.IsMain)
                {
                    file = item; break;
                }
            }
            return file;
        }

        internal string getSourceCode()
        {
            string text = "";
            foreach (var item in Files)
            {
                if (item.IsMain) continue;
                else if(item.Type == SourceFileTyp.source)
                {
                    text += item.Open() + "\n";
                }
            }
            text += getMainFile().Open();

            return text;
        }
    }

   
}
