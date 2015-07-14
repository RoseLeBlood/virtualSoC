﻿//
//  vmor.cs
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
	public class vmor : vmoperator
	{
		public string Name {
			get { return "OR"; }
		}

		public bool ParseAndRun (ParserFactory factory)
		{
			InstructionParam2 param1 = factory.getParam(4); // 101 4 105
			int param1V = VM.Instance.Ram.Read32 (VM.Instance.CPU.L2.ip + 5); //106

			InstructionParam2 param2 = factory.getParam(9); // 110 4 114
			int param2V = VM.Instance.Ram.Read32 (VM.Instance.CPU.L2.ip + 10); //115 

			InstructionParam2 param3 = factory.getParam(14); // 119 4 123 
			int param3V = VM.Instance.Ram.Read32 (VM.Instance.CPU.L2.ip + 15);


			if (param2 == InstructionParam2.Value)
				VM.Instance.CPU.L2.Stack.Push32 (param2V);
			else if (param2 == InstructionParam2.Register) {
				VM.Instance.CPU.L2.Stack.Push32 (VM.Instance.CPU.L2.Get (factory.m_pRegisters [param2V].Name));
			}
			if (param3 == InstructionParam2.Value)
				VM.Instance.CPU.L2.Stack.Push32 (param3V);
			else if (param3 == InstructionParam2.Register) {
				VM.Instance.CPU.L2.Stack.Push32 (VM.Instance.CPU.L2.Get (factory.m_pRegisters [param3V].Name));
			}
				
			if (param1 == InstructionParam2.Value)
				VM.Instance.Ram.Write (VM.Instance.CPU.Or( VM.Instance.CPU.L2.Stack.Pop32 (), VM.Instance.CPU.L2.Stack.Pop32 () ), (uint)param1V);
			else if (param1 == InstructionParam2.Register)
				VM.Instance.CPU.L2.Set (factory.m_pRegisters [param1V].Name, VM.Instance.CPU.Or( VM.Instance.CPU.L2.Stack.Pop32 (), VM.Instance.CPU.L2.Stack.Pop32 () ));
			
			return true;
		}
	}
}

