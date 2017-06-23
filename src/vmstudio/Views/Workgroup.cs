using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using vmstudio.Daten;

namespace vmstudio.Views
{
    
    public class Workgroup
    {
        private WorkSpace m_space;

        private ObservableCollection<WorkgroupFile> m_fileSource;
        public ObservableCollection<WorkgroupFile> FileSources { get { return m_fileSource; } set { m_fileSource = value; } }

        public string Name { get; set; }
        public string Image { get; set; }
        public WorkSpace WorkSpace { get { return m_space; } internal set { m_space = value; } }

        public Workgroup()
        {
            m_fileSource = new ObservableCollection<WorkgroupFile>();
            Name = "test";
            Image = "";
        }
        public Workgroup(Daten.WorkSpace space)
        {
            m_fileSource = new ObservableCollection<WorkgroupFile>();
            Name = space.Name;
            m_space = space;
            foreach (var item in space.Files)
            {
                m_fileSource.Add(new WorkgroupFile(item));
            }
        }

        internal void Save()
        {
            m_space.Save();
        }
        internal Workgroup Reload()
        {
            m_fileSource.Clear();

            foreach (var item in m_space.Files)
            {
                m_fileSource.Add(new WorkgroupFile(item));
            }

            return this;
        }
        internal Workgroup AddSourceFile(string name, string text)
        {
            m_space.AddNewFile(name + ".asm", SourceFileTyp.source, text, false);
            Save();
            return Reload();
        }
        internal Workgroup AddIncludeFile(string name, string text)
        {
            m_space.AddNewFile(name + ".inc", SourceFileTyp.include, text, false);
            Save();
            return Reload();
        }
    }
}
