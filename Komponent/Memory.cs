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
	public class Memory : System.IO.Stream
	{
		private byte[] m_pMemory;
		private int m_iSize;
		private string m_strName;
		public int Size 
		{
			get { return m_iSize; }
		}
		public byte this[int adress]
		{
			get { return m_pMemory[adress]; }
			set { m_pMemory[adress] = value; }
		}

		public Memory (int mSize, string name)
			: base ()
		{
			m_pMemory = new byte[mSize];
			m_iSize = mSize;
			m_strName = name;

			this.Write (Utils.RandMemory (mSize), 0, mSize);
		}
		public int Write(byte[] data, int addr = 0)
		{
			for (int i = addr; i < data.Length; i++) {
				m_pMemory [i] = data [i];
			}
			return addr + data.Length;
		}
		public int Write(Int16 data, int addr)
		{
			byte[] _d = data.ToBytes ();
			for (uint i = 0; i < _d.Length; i++)
				m_pMemory [addr + i] = _d [i];

			return addr + _d.Length;
		}
		public int Write(Int32 data, uint addr)
		{
			byte[] _d = data.ToBytes ();
			for (uint i = 0; i < _d.Length; i++)
				m_pMemory [addr + i] = _d [i];

			return (int)(addr + _d.Length);
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
			output.WriteLine (m_strName + ":");
			foreach (var b in m_pMemory) {

				if (address == 0 || address%16==0)
					output.Write(System.Environment.NewLine + "{0,-4:000} ", address);
				address++;
				output.Write(" {0:X2} ",(int)b);

			}
			return output.ToString ();
		}

		#region implemented abstract members of Stream

		public override void Flush ()
		{
			
		}

		public override long Seek (long offset, SeekOrigin origin)
		{
			return 0;
		}

		public override void SetLength (long value)
		{
			
		}

		public override int Read (byte[] buffer, int offset, int count)
		{
			int read = Math.Min (count, Size);

			for (int i = offset; i < read; i++)
				buffer [i] = m_pMemory [i];

			return read;
		}

		public override void Write (byte[] buffer, int offset, int count)
		{
			for (int i = offset; i < count; i++)
				m_pMemory [i] = buffer [i];
		}

		public override bool CanRead {
			get { return true; }
		}

		public override bool CanSeek {
			get { return 	false; }
		}

		public override bool CanWrite {
			get { return true; }
		}

		public override long Length {
			get { return Size; }
		}

		public override long Position {
			get { return 0; }
			set {  }
		}

		#endregion
	}
}