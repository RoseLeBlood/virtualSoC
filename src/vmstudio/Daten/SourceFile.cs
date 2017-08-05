using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmstudio.Daten
{
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
}
