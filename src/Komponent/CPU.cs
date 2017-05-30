using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcsos.Komponent
{
    public class CPU : vmKomponente
    {
        protected Stack m_pCacheStack; // L1 IPC  
        private List<Core> m_lstCores;
        private int m_iCurCore;

        public Stack Ipc
        {
            get { return m_pCacheStack; }
        }
        public int CurrentCoreID
        {
            get { return m_iCurCore; }
            set { if (this[value].Running) { m_iCurCore = value; } }
        }
        

        public Core CurrentCore
        {
            get { return m_lstCores[m_iCurCore]; }
        }

        public int Cores { get { return m_lstCores.Count; } }

        public Core this[int index]
        {
            get { return m_lstCores[index]; }
        }

        public CPU(int coreNumbers) : base("Referenz CPU", "Anna-Sophia Schroeck")
        {
            m_lstCores = new List<Core>();

            for (int i = 0; i < coreNumbers; i++)
                m_lstCores.Add(new Core(i, true));
            m_pCacheStack = new Stack(512, 2, "ProzessorCache (512)");
            m_iCurCore = 0;
        }
        
    }
}
