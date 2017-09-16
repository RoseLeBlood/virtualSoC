using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcsos.Komponent
{
    public class IpcStack
    {
        private Memory m_pMemory;

        internal int Size
        {
            get { return m_pMemory.Size - 1; }
        }
        internal IpcStack(int size, int delay, string name = "")
        {
            m_pMemory = new Memory(size, name, delay);
        }
        public void Push32(int value)
        {
            byte[] _l = value.ToBytes();
            Array.Reverse(_l);
            for (int i = 0; i < _l.Length; i++)
                Push(_l[i]);
            //Push (0);
        }
        public int Pop32()
        {
            byte[] _l = new byte[4];
            for (int i = 0; i < 4; i++)
                _l[i] = Pop();

            return _l.ToInt();
        }
        public int Peek32()
        {
            byte[] _l = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                _l[i] = m_pMemory[VM.Instance.CPU.IpcStackPointer + 1 + i];
            }

            return _l.ToInt();
        }
        public void Push(byte data)
        {
            m_pMemory[VM.Instance.CPU.IpcStackPointer] = data;
            VM.Instance.CPU.IpcStackPointer -= 1;
        }
        public byte Pop()
        {
            byte b = m_pMemory[VM.Instance.CPU.IpcStackPointer + 1];
            m_pMemory[VM.Instance.CPU.IpcStackPointer + 1] = 0;
            VM.Instance.CPU.IpcStackPointer += 1;
            return b;
        }
        public byte Peek()
        {
            return m_pMemory[VM.Instance.CPU.IpcStackPointer + 1];
        }
        public override string ToString()
        {
            return string.Format("[IpcStack] Peek32: {0} {1}", Peek32(), Peek());
        }
    }
}
