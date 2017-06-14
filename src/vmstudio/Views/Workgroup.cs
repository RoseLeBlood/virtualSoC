using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using vmstudio.Daten;

namespace vmstudio.Views
{
    public class WorkgroupFile
    {
        private SourceFile m_pItem;

        public string Name
        {
            get { return m_pItem.Name; }
        }
        public string Text
        {
            get;//get { return m_pItem.Open(); }
            set;//set { m_pItem.Save(value);  }
        }

        public string Image
        {
            get
            {
                switch (m_pItem.Type)
                {
                    case SourceFileTyp.include:
                        break;
                    case SourceFileTyp.source:
                        break;
                    case SourceFileTyp.textfile:
                        break;
                    case SourceFileTyp.other:
                        break;
                    default:
                        break;
                }
                return "";
            }
        }

        public WorkgroupFile(SourceFile item)
        {
            m_pItem = item;
            Text = item.Open();
        }
        public void Save()
        {
            m_pItem.Save(Text);
        }
    }
    public class Workgroup
    {
        private ObservableCollection<WorkgroupFile> m_fileSource;
        public ObservableCollection<WorkgroupFile> FileSources { get { return m_fileSource; } set { m_fileSource = value; } }

        public string Name { get; set; }
        public string Image { get; set; }

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
            foreach (var item in space.Files)
            {
                m_fileSource.Add(new WorkgroupFile(item));
            }
        }
    }
}
