//
//  vmfbd.cs
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

namespace Vcsos.mm
{
	public class vmfbd: vmoperator
	{
		public string Name {
			get { return "FBSET"; } // FrameBuffer DOT ( SetPixel )
		}
		public bool ParseAndRun (ParserFactory factory)
		{
			InstructionParam2 param1 = factory.getParam(4); // 101 4 105
			int param1V = VM.Instance.Ram.Read32 (VM.Instance.CPU.L2.ip + 5); //106

			InstructionParam2 param2 = factory.getParam(9); // 110 4 114
			int param2V = VM.Instance.Ram.Read32 (VM.Instance.CPU.L2.ip + 10); //115 

			InstructionParam2 param3 = factory.getParam(14); // 119 4 123 
			int param3V = VM.Instance.Ram.Read32 (VM.Instance.CPU.L2.ip + 15);

			int x = 0, y = 0, colorRef = 0;

			if (param1 == InstructionParam2.Value)
				x = (param1V);
			else if (param1 == InstructionParam2.Register) {
				x = (VM.Instance.CPU.L2.Get (factory.m_pRegisters [param1V].Name));
			} else if (param2 == InstructionParam2.Pointer) {
				x = MemoryMap.Read32 (param1V);
			}

			if (param2 == InstructionParam2.Value)
				y = (param2V);
			else if (param2 == InstructionParam2.Register) {
				y = (VM.Instance.CPU.L2.Get (factory.m_pRegisters [param2V].Name));
			} else if (param2 == InstructionParam2.Pointer) {
				y = MemoryMap.Read32 (param2V);
			}

			if (param3 == InstructionParam2.Value)
				colorRef = (param3V);
			else if (param3 == InstructionParam2.Register) {
				colorRef = (VM.Instance.CPU.L2.Get (factory.m_pRegisters [param3V].Name));
			} else if (param3 == InstructionParam2.Pointer) {
				colorRef = MemoryMap.Read32 (param2V);
			}

			VM.Instance.FBdev.SetPixel (colorRef, x, y);
			return true;
		}
	}
}

