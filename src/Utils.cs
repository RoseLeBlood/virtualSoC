﻿//
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

namespace Vcsos
{
    /// <summary>
    /// Mathematische helper classe
    /// </summary>
	public static class Utils
	{
        /// <summary>
        /// Number to boundary
        /// </summary>
        /// <param name="number"></param>
        /// <param name="boundary"></param>
        /// <returns></returns>
		public static int ToBoundary(this uint number, uint boundary)
		{
			int newNumber = (int)(boundary * ((number / boundary) + ((number % boundary > 0) ? 1: 0)));
			return newNumber;
		}
        /// <summary>
        /// extension uint to byte array
        /// </summary>
        /// <param name="UIntIn">Die Variable die in bytes zerlegt werden soll</param>
        /// <returns>byte array von der Variable </returns>
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
        /// <summary>
        /// extension int to byte array
        /// </summary>
        /// <param name="IntIn">Die Variable die in bytes zerlegt werden soll</param>
        /// <returns>byte array vin der Variable</returns>
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
        /// <summary>
        /// byte array to int
        /// </summary>
        /// <param name="BytesIn">das byte array</param>
        /// <returns></returns>
		public unsafe static int ToUint(this byte[] BytesIn)
		{
			fixed(byte* otherbytes = BytesIn)
			{
				int newUint = 0;
				int* ut = (int*)&otherbytes[0];
				newUint = *ut;
				return newUint;
			}
		}
		public unsafe static int ToInt(this byte[] BytesIn)
		{
			fixed(byte* otherbytes = BytesIn)
			{
				int newUint = 0;
				int* ut = (int*)&otherbytes[0];
				newUint = *ut;
				return newUint;
			}
		}
		public static bool Get(this int A, int pos)
		{
			return (A >> pos & 1) == 1;
		}
		public static bool Get(this short A, short pos)
		{
			return (A >> pos & 1) == 1;
		}
		public static int Set(this int A, bool bit, int pos)
		{
			int res = (!bit) ? (int)(A & ~(1 << (pos))) : (int)(A | (int)(1 << pos));
			return res;

		}
		public static short Set(this short A, bool bit, short pos)
		{
			short res = (!bit) ? (short)(A & ~(1 << (pos))) : (short)(A | (short)(1 << pos));
			return res;

		}
		public static int Chg(this int A, int pos)
		{
			return A.Set (!A.Get (pos), pos);
		}
		public unsafe static byte[] ToBytes(this Int16 UIntIn)
		{
			//turn a uint into 4 bytes
			byte[] fourBytes = new byte[2];
			short* pt = &UIntIn;
			byte* bt = (byte*)&pt[0];
			fourBytes[0] = *bt++;
			fourBytes[1] = *bt++;
			return fourBytes;
		}
		public unsafe static Int16 ToShort(this byte[] BytesIn)
		{
			fixed(byte* otherbytes = BytesIn)
			{
				short newUint = 0;
				short* ut = (short*)&otherbytes[0];
				newUint = *ut;
				return newUint;
			}
		}
		static Random r = new Random ((int)DateTime.Now.ToBinary ());

		internal static int RandR(this int a)
		{
			return r.Next (int.MinValue, int.MaxValue);
		}
		/*internal static byte RandR(this byte a)
		{
			byte val = (byte)r.Next (byte.MinValue, byte.MaxValue);
		}*/
		internal static byte[] RandMemory(int size)
		{
			byte[] buffer = new byte[size];
			r.NextBytes (buffer);
			return buffer;
		}
        public static uint SizeCalc(string value)
        {
            uint ramSize = 1;
            string str = value.ToUpper();

            if(str.Contains("G"))
            {
                str = str.Replace('G', ' ');
                ramSize = 1024 * 1024 * 1024;
            }
            else if (str.Contains("M"))
            {
                str = str.Replace('M', ' ');
                ramSize = 1024*1024;
            }
            else if (str.Contains("K"))
            {
                str = str.Replace('K', ' ');
                ramSize = 1024;
            }
            else if (str.Contains("B"))
            {
                str = str.Replace('B', ' ');
                ramSize = 1;
            }
            else
                ramSize = 1;
            uint size;

            if (uint.TryParse(str, out size))
            {
                ramSize *= size;
            }
            else
                ramSize *= 512;
            return ramSize;
        }
    }
}

