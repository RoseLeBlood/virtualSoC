//
//  Stack.cs
//
//  Author:
//       anna-sophia <${AuthorEmail}>
//
//  Copyright (c) 2015 anna-sophia
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace Vcsos.Komponent
{
	public class Stack 
	{
        private Memory m_pMemory;

        internal int Size
        {
            get { return m_pMemory.Size-1; }
        }
        internal Stack(int size, string name ="")
        {
            m_pMemory = new Memory(size, name);
        }
		public void Push32(int value)
		{
			byte[] _l = value.ToBytes ();
			Array.Reverse (_l);
			for (int i = 0; i < _l.Length; i++)
				Push (_l [i]);
			//Push (0);
		}
		public int Pop32()
		{
			byte[] _l = new byte[4];
			for (int i = 0; i < 4; i++)
				_l [i] = Pop ();
			
			return _l.ToInt ();
		}
		public int Peek32()
		{
			byte[] _l = new byte[4];

			for (int i = 0; i < 4; i++) {
				_l[i] = m_pMemory[VM.Instance.CurrentCore.Register.sp+1+i];
			}

			return _l.ToInt ();
		}
		public void Push(byte data)
		{
            m_pMemory[VM.Instance.CurrentCore.Register.sp] = data;
			VM.Instance.CurrentCore.Register.sp -= 1;
		}
		public byte Pop()
		{
			byte b = m_pMemory[VM.Instance.CurrentCore.Register.sp+1];
            m_pMemory[VM.Instance.CurrentCore.Register.sp + 1] = 0;
			VM.Instance.CurrentCore.Register.sp += 1;
			return b;
		}
		public byte Peek()
		{
			return m_pMemory[VM.Instance.CurrentCore.Register.sp+1];
		}
		public override string ToString ()
		{
			return string.Format ("[Stack] Peek32: {0} {1}", Peek32(), Peek());
		}
	}
}

