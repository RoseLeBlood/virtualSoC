using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using vmstudio.Daten;

namespace vmstudio.Views
{
    public class WorkspaceListViewItem : ListViewItem
    {
        private string m_pritem;
        public string Workspace { get { return m_pritem; } }

        public WorkspaceListViewItem(string space) : base()
        {
            m_pritem = space;
            base.Tag = space;
        }
    }
}
