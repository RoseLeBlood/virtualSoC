//
//  Ram.cs
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
using Vcsos.Komponent;

namespace Vcsos
{
	public static class MemoryMap 
	{
		public static int Write(byte[] data, int addr, bool fb = false)
		{
			return VM.Instance.Ram.Write (data, addr);
		}
		public static void Write(byte data, int addr, bool fb = false)
		{
			VM.Instance.Ram[addr] = data;
		}
		public static int Write(Int16 data, int addr, bool fb = false)
		{
			return VM.Instance.Ram.Write (data, addr);
		}
		public static int Write(Int32 data, uint addr, bool fb = false)
		{
			return VM.Instance.Ram.Write (data, addr);
		}
		public static Int32 Read32(Int32 addr, bool fb = false)
		{
			return VM.Instance.Ram.Read32 (addr);
		}
		public static Int16 Read16(Int32 addr, bool fb = false)
		{
			return VM.Instance.Ram.Read16 (addr);
		}
	}
}

