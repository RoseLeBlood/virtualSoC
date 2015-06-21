//
//  Serial.cs
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
using System.IO.Ports;

namespace Komponent
{
	public class Serial
	{
		private SerialPort m_pPort;
		private bool m_pReciveData;

		public byte SBUF // (99h)
		{
			get { return (byte)m_pPort.ReadByte (); }
			set { m_pPort.Write (new Byte[] { value }, 0, 1); }
		}
		public bool REN
		{
			get { return m_pPort.RtsEnable; }
		}
		public Serial ()
		{
			m_pPort = new SerialPort("COM1", 9600, Parity.Even, 8, StopBits.One);
		}


	}
}

