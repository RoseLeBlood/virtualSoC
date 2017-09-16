using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcsos.mm
{
    class vmcpu : vmoperator
    {
        public string Name { get { return "CPU"; } }
        public string Info { get { return "Push to IPC"; } }

        public bool ParseAndRun(ParserFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
