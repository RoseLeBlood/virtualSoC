using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcsos.mm
{
    class vmvariable : vmoperator
    {
        public string Name { get { return "DIM";  } }

        public string Info { get { return ""; } }

        public bool ParseAndRun(ParserFactory factory)
        {
            int size = VM.Instance.Ram.Read32(VM.Instance.CurrentCore.Register.ip + 4);
            VM.Instance.CurrentCore.Register.ip += size;
            return true;
        }
    }
}
