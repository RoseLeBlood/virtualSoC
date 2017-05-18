using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcsos.Komponent
{
    public class CPU : vmKomponente
    {
        protected CacheStack m_pCacheStack; // L1 IPC  
        private List<Core> m_lstCores;

        public CacheStack Ipc
        {
            get { return m_pCacheStack; }
        }
        public Core MasterCore
        {
            get { return m_lstCores[0]; }
        }
        public Core[] SlaveCores
        {
            get
            {
                Core[] temp = new Core[m_lstCores.Count - 1];
                m_lstCores.CopyTo(temp.ToArray(), 1);
                return temp;
            }
        }
        public Core this[int index]
        {
            get { return m_lstCores[index]; }
        }

        public CPU(int coreNumbers) : base("Referenz CPU", "Anna-Sophia Schroeck")
        {
            m_lstCores = new List<Core>();

            for (int i = 0; i < coreNumbers; i++)
                m_lstCores.Add(new Core(i));
            m_pCacheStack = new CacheStack(256, "CPU-IPC ");
        }
        
    }
}
