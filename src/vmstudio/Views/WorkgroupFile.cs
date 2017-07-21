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
                        return Char.ConvertFromUtf32(0xE82D);//
                    case SourceFileTyp.source:
                        return Char.ConvertFromUtf32(0xE70B);//
                    case SourceFileTyp.textfile:
                        return Char.ConvertFromUtf32(0xE8BD);//
                    case SourceFileTyp.other:
                        return Char.ConvertFromUtf32(0xE18A);//
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
