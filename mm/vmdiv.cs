//
//  vmdiv.cs
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
	public class vmdiv : vmoperator
	{
		public string Name {
			get { return "DIV"; }
		}
		public bool ParseAndRun (ParserFactory factory)
		{
			InstructionParam2 param1 = factory.getParam(4);
			int param1V = VM.Instance.Ram.Read32 (VM.Instance.CPU.Register.ip + 5);

			if (param1 == InstructionParam2.Value)
				VM.Instance.CPU.Akku.Div (param1V);
			else if (param1 == InstructionParam2.Register) {
				VM.Instance.CPU.Akku.Div (VM.Instance.CPU.Register.Get (factory.m_pRegisters [param1V].Name));
			}

			return true;
		}
	}
}

