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
    /// <summary>
    /// Memory Helper der SoC - Ram, Serial, Framebuffer
    /// </summary>
	public static class MemoryMap 
	{
        /// <summary>
        /// Schreibt daten in einer bestimmten position
        /// </summary>
        /// <param name="data">Die zu schreibene Daten</param>
        /// <param name="addr">Start Adresse im Speicher</param>
        /// <returns></returns>
		public static int Write(byte[] data, int addr)
		{
			if (IsAddressFB (addr)) // weist die Addresse auf den Framebufferspeicher
                // Wenn ja dann schreibe die Daten in den Framebuffer
				return VM.Instance.FBdev.Memory.Write (data, (int)(addr - Framebuffer.FBBASE));
			else
                // wenn nein schreibr die Daten in den RAN
				return VM.Instance.Ram.Write (data, addr);
	
		}
        /// <summary>
        /// Schreibt ein byte in fbdev oder ram
        /// </summary>
        /// <param name="data">die zh s hreibene daten</param>
        /// <param name="addr">addresse </param>
		public static void Write(byte data, int addr)
		{
			if (IsAddressFB (addr)) {
				VM.Instance.FBdev.Memory [(int)(addr - Framebuffer.FBBASE)] = data;
			} else {
				VM.Instance.Ram [addr] = data;// (data, addr);
			}
		}
		public static int Write(Int16 data, int addr)
		{
			if (IsAddressFB (addr)) {
				return VM.Instance.FBdev.Memory.Write (data, (int)(addr - Framebuffer.FBBASE));
			} else {
				return VM.Instance.Ram.Write (data, addr);
			}
		}
		public static int Write(Int32 data, uint addr)
		{
			if (IsAddressFB ((int)addr)) {
				return VM.Instance.FBdev.Memory.Write (data, (uint)(addr - Framebuffer.FBBASE));
			} else {
				return VM.Instance.Ram.Write (data, addr);
			}
		}
        /// <summary>
        /// Lese ein 32Bit Wert 
        /// </summary>
        /// <param name="addr">Addresse des zulesenden wertes </param>
        /// <returns>gelesene Integer31</returns>
        public static Int32 Read32(Int32 addr)
		{
			if (IsAddressFB (addr)) {
				return VM.Instance.FBdev.Memory.Read32 ((int)(addr - Framebuffer.FBBASE));
			} else {
				return VM.Instance.Ram.Read32 (addr);
			}
		}
        /// <summary>
        /// Lese ein 16Bit Wert 
        /// </summary>
        /// <param name="addr">Addresse des zulesenden wertes </param>
        /// <returns>gelesene integer</returns>
        public static Int16 Read16(Int32 addr)
		{
			if (IsAddressFB (addr)) {
				return VM.Instance.FBdev.Memory.Read16 ((int)(addr - Framebuffer.FBBASE));
			} else {
				return VM.Instance.Ram.Read16 (addr);
			}
		}
        /// <summary>
        /// Lese ein 8Bit Wert 
        /// </summary>
        /// <param name="addr">Addresse des zulesenden wertes </param>
        /// <returns>gelesene byte</returns>
		public static byte Read8(Int32 addr)
		{
			if (IsAddressFB (addr)) {
				return VM.Instance.FBdev.Memory[ ((int)(addr - Framebuffer.FBBASE)) ];
			} else {
				return VM.Instance.Ram [addr];
			}
		}
        /// <summary>
        /// Weißt die Addresse auf den Framebuffer zu
        /// </summary>
        /// <param name="addr">Zu testende Addresse</param>
        /// <returns>true wenn die Adresse im Framebuffer speicher liegt</returns>
		private static bool IsAddressFB(int addr)
		{
            // deklariere Result und weise true zu
			bool Result = true;
            // ist die adresse nicht im framebuffer bereich...
			if (addr < Framebuffer.FBBASE || addr > (int)(Framebuffer.FBBASE + VM.Instance.FBdev.Memory.Length)) {
                //wenn ja weise Result false zu
                Result = false;
			}
            // gebe den Wert aus der Variable Result zurück 
			return Result;
		}
	}
}

