using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vminst;

namespace Vcsos.mm
{
    class vmcsp : vmoperator
    {
        public string Name {  get { return "CSP"; } }
        public string Info { get { return "Set Core IP - and stop the core"; } }

        public bool ParseAndRun(ParserFactory factory)
        {
            InstructionParam2 param1 = factory.getParam(4);
            int param1V = VM.Instance.Ram.Read32(VM.Instance.CurrentCore.Register.ip + 5);

            InstructionParam2 param2 = factory.getParam(9); // 110 4 114
            int param2V = VM.Instance.Ram.Read32(VM.Instance.CurrentCore.Register.ip + 10); //115 

            if (param1 == InstructionParam2.Value)
                VM.Instance.CurrentCore.Register.Stack.Push32(param1V);
            else if (param1 == InstructionParam2.Register)
            {
                VM.Instance.CurrentCore.Register.Stack.Push32(VM.Instance.CurrentCore.Register.Get(factory.m_pRegisters[param1V].Name));
            }

            if (param2 == InstructionParam2.Value)
            {
                VM.Instance.CPU[VM.Instance.CurrentCore.Register.Stack.Peek32()].Register.ip = param2V;
                VM.Instance.CPU[VM.Instance.CurrentCore.Register.Stack.Pop32()].Running = false;
            }
            else if (param2 == InstructionParam2.Register)
            {
                VM.Instance.CPU[VM.Instance.CurrentCore.Register.Stack.Peek32()].Register.ip =
                    VM.Instance.CurrentCore.Register.Get(factory.m_pRegisters[param2V].Name);
                VM.Instance.CPU[VM.Instance.CurrentCore.Register.Stack.Pop32()].Running = false;            
            }
            return true;
        }
    }
}
