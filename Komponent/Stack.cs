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
				_l[i] = VM.Instance.Ram [VM.Instance.CurrentCore.Register.sp+1+i];
			}

			return _l.ToInt ();
		}
		public void Push(byte data)
		{
			VM.Instance.Ram [VM.Instance.CurrentCore.Register.sp] = data;
			VM.Instance.CurrentCore.Register.sp -= 1;
		}
		public byte Pop()
		{
			byte b = VM.Instance.Ram [VM.Instance.CurrentCore.Register.sp+1];
			VM.Instance.Ram [VM.Instance.CurrentCore.Register.sp + 1] = 0;
			VM.Instance.CurrentCore.Register.sp += 1;
			return b;
		}
		public byte Peek()
		{
			return VM.Instance.Ram [VM.Instance.CurrentCore.Register.sp+1];
		}
		public override string ToString ()
		{
			return string.Format ("[Stack] Peek32: {0}", Peek32(), Peek());
		}
	}
	public class CacheStack
	{
		protected Memory    m_pCache;

		public int MaxAdress
		{
			get { return m_pCache.Read16 (2); }
		}
		public short SP
		{
			get { return m_pCache.Read16 (0); }
			set { m_pCache.Write (value, 0); }
		}
		public CacheStack(int size, string name)
		{
			m_pCache = new Memory (size, name);
			m_pCache.Write ((ushort)(m_pCache.Size-1), 0); // Current Adress
			m_pCache.Write ((ushort)(m_pCache.Size-1), 2); // Max Adresse
		}
		public void Push(byte data)
		{
			if (SP > 7) {
				m_pCache [(int)SP] = data; SP = (short)(SP - 1);
			} else {
				// INTERRUPT
			}
		}
		public byte Pop()
		{
			if (SP != MaxAdress) {
				byte b = m_pCache [(int)(++SP)];
				m_pCache [(int)SP] = 0;
				return b;
			} else {
				// INTERRUPT
				return 0;
			}
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
		public byte Peek()
		{
			return m_pCache [(int)(SP + 1)];
		}
		public int Peek32()
		{
			byte[] _l = new byte[4];

			for (int i = 0; i < 4; i++) {
				_l[i] = VM.Instance.Ram [VM.Instance.CurrentCore.Register.sp+1+i];
			}

			return _l.ToInt ();
		}
		public override string ToString ()
		{
			return m_pCache.ToString ();
		}
	}
}

