//
//  VM.cs
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
using vminst;
using Vcsos.Komponent;

namespace Vcsos
{
	public class VM
	{
		private static VM m_instance = new VM ();
		private Assembler m_pAssembler;

		internal CPU 	CPU;
		internal Memory Ram;
		internal Framebuffer FBdev;

		public bool IsAlive { get { return m_pAssembler.IsAlive; } }
		public static VM Instance
		{
			get { return m_instance; }
		}
		internal VM()
		{
			m_pAssembler = new Assembler ();
		}

		public void CreateVM(UInt32 ramSize)
		{
			int newMemorySize = ramSize.ToBoundary(4);
			Ram = new Memory (newMemorySize);
			FBdev = new Framebuffer (800, 600, 32);

			if (newMemorySize != ramSize) 
				Console.WriteLine("VM: Memory was expanded from {0} bytes to {1} bytes to a page boundary." + System.Environment.NewLine,
					ramSize, newMemorySize);
			
			CPU = new CPU ();
		}
		public void Start(string osFile = "test.bin", int startAddr = 0x10)
		{
			if (System.IO.File.Exists (osFile)) {
				byte[] data = System.IO.File.ReadAllBytes (osFile);
				Ram.Write (data);

				CPU.Register.ip = startAddr;
				m_pAssembler.Start ();
			}
		}
	}

}

