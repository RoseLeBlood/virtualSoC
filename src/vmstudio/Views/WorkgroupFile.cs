using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get;
            set;
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
}
