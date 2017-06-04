using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcsos.Komponent
{
    public enum FpuRegister
    {
        RFP0,
        RFP1,

    }
    
    class fpu : vmKomponente
    {
        private Memory m_mRFP0;
        private Memory m_mRFP1;

        private Core m_cCore;

        
        public fpu(Core core) : base("FPU" + core.CoreNumber, "Anna-Sophia Schröck")
        {
            m_mRFP0 = new Memory(256, "RFP0-" + core.CoreNumber, 147);
            m_mRFP1 = new Memory(256, "RFP1-" + core.CoreNumber, 147);

            m_cCore = core;
        } 
        bool Set(HalfFloat n0, HalfFloat n1, 
                 HalfFloat n2, HalfFloat n3,
                 string register)  
        {
            return false;
        }
        bool Set(SingleFloat n0, SingleFloat n1, 
                 SingleFloat n2, SingleFloat n3,
                 string register)
        {
            return false;
        }

        bool Set(DoubleFloat n0, DoubleFloat n1,
                 DoubleFloat n2, DoubleFloat n3,
                 string register)
        {
            return false;
        }
        bool Set(QuadFloat n0, QuadFloat n1,
                 string register)
        {
            return false;
        }
    }
}
