//
//  Utils.cs
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

namespace vmasm
{
	public static class Utils
	{
		public static uint ToBoundary(this uint number, uint boundary)
		{
			uint newNumber = (uint)(boundary * ((number / boundary) + ((number % boundary > 0) ? 1: 0)));
			return newNumber;
		}
        public static byte[] ToBytes(this string text)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            return ascii.GetBytes(text);
        }
		public unsafe static byte[] ToBytes(this uint UIntIn)
		{
			//turn a uint into 4 bytes
			byte[] fourBytes = new byte[4];
			uint* pt = &UIntIn;
			byte* bt = (byte*)&pt[0];
			fourBytes[0] = *bt++;
			fourBytes[1] = *bt++;
			fourBytes[2] = *bt++;
			fourBytes[3] = *bt++;
			return fourBytes;
		}
		public unsafe static byte[] ToBytes(this int IntIn)
		{
			//turn a uint into 4 bytes
			byte[] fourBytes = new byte[4];
			int* pt = &IntIn;
			byte* bt = (byte*)&pt[0];
			fourBytes[0] = *bt++;
			fourBytes[1] = *bt++;
			fourBytes[2] = *bt++;
			fourBytes[3] = *bt++;
			return fourBytes;
		}
		public unsafe static uint ToUint(this byte[] BytesIn)
		{
			fixed(byte* otherbytes = BytesIn)
			{
				uint newUint = 0;
				uint* ut = (uint*)&otherbytes[0];
				newUint = *ut;
				return newUint;
			}
		}
		public static bool Get(this uint A, int pos)
		{
			return (A >> pos & 1) == 1;
		}
		public static uint Set(this uint A, bool bit, int pos)
		{
			uint res = (!bit) ? (uint)(A & ~(1 << (pos))) : (uint)(A | (uint)(1 << pos));
			return res;

		}
		public static uint Chg(this uint A, int pos)
		{
			return A.Set (!A.Get (pos), pos);
		}
		public unsafe static byte[] ToBytes(this ushort UIntIn)
		{
			//turn a uint into 4 bytes
			byte[] fourBytes = new byte[2];
			ushort* pt = &UIntIn;
			byte* bt = (byte*)&pt[0];
			fourBytes[0] = *bt++;
			fourBytes[1] = *bt++;
			return fourBytes;
		}
		public unsafe static UInt16 ToShort(this byte[] BytesIn)
		{
			fixed(byte* otherbytes = BytesIn)
			{
				ushort newUint = 0;
				ushort* ut = (ushort*)&otherbytes[0];
				newUint = *ut;
				return newUint;
			}
		}
	}
}

