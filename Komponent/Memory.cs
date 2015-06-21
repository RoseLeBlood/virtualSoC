//
//  Memory.cs
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
using System.IO;
using Vcsos;

namespace Vcsos.Komponent
{
	public class Memory
	{
		private byte[] m_pMemory;
		private int m_iSize;

		public int Size 
		{
			get { return m_iSize; }
		}
		public byte this[int adress]
		{
			get { return m_pMemory [adress]; }
			set { m_pMemory [adress] = value; }
		}

		public Memory (int mSize)
		{
			m_pMemory = new byte[mSize];
			m_iSize = mSize;
		}
		public void Write(byte[] data)
		{
			for (int i = 0; i < data.Length; i++) {
				m_pMemory [i] = data [i];
			}
		}
		public void Write(Int16 data, uint addr)
		{
			byte[] _d = data.ToBytes ();
			for (uint i = 0; i < _d.Length; i++)
				m_pMemory [addr + i] = _d [i];
		}
		public void Write(Int32 data, UInt32 addr)
		{
			byte[] _d = data.ToBytes ();
			for (uint i = 0; i < _d.Length; i++)
				m_pMemory [addr + i] = _d [i];

		}
		public Int32 Read32(Int32 addr)
		{
			byte[] _d = Int32.MinValue.ToBytes();

			for (int i = 0; i < _d.Length; i++)
				_d [i] = m_pMemory [addr + i];

			return _d.ToUint ();
		}
		public Int16 Read16(Int32 addr)
		{
			byte[] _d = Int16.MinValue.ToBytes();

			for (int i = 0; i < _d.Length; i++)
				_d [i] = m_pMemory [addr + i];

			return _d.ToShort ();
		}
		public override string  ToString()
		{
			var output = new StringWriter ();

			int address = 0;
			foreach (byte b in m_pMemory)
			{
				if (address == 0 || address%16==0)
					output.Write(System.Environment.NewLine + "{0,-4:000} ", address);
				address++;
				output.Write(" {0:X2} ",(int)b);

			}
			return output.ToString ();
		}
	}
}

