//
//  vmoperator.cs
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
using System.Collections.Generic;
using Vcsos.Komponent;

namespace Vcsos.mm
{
	public interface vmoperator
	{
		string Name { get; }
		bool ParseAndRun(ParserFactory factory);
	}

	public class ParserFactory
	{
		internal List<vmoperator> m_pOperators;
		internal Instructions m_pInstructions;
		internal Registers	m_pRegisters;

		internal ParserFactory()
		{
			m_pOperators = new List<vmoperator> ();
			m_pInstructions = new Instructions ();
			m_pRegisters = new Registers ();

			m_pOperators.Add (new vmnop ());
			m_pOperators.Add (new vmend ());
			m_pOperators.Add (new vmadd ());
			m_pOperators.Add (new vmclr ());
			m_pOperators.Add (new vmdiv ());
			m_pOperators.Add (new vmjmp ());
			m_pOperators.Add (new vmmul ());
			m_pOperators.Add (new vmpeek ());
			m_pOperators.Add (new vmpop ());
			m_pOperators.Add (new vmpush ());
			m_pOperators.Add (new vmsub ());
			m_pOperators.Add (new vmmov ());
		}
		internal  bool ParseAndRun(int pos)
		{
			Instruction c = m_pInstructions [pos];

			#if DEBUG
			Console.WriteLine ("{3} [{2}] {0} {1}", pos, c.OpCode, 
				VM.Instance.CPU.Register.ip, CPU.Ticks);
			#endif

			bool ret = getOperator (c).ParseAndRun (this);
			VM.Instance.CPU.Register.ip += c.OpParam1;
			return ret;
		}
		private vmoperator getOperator(Instruction c)
		{
			vmoperator ope = null;
			foreach (var item in m_pOperators) {
				if (item.Name == c.OpCode) {
					ope = item;
					break;
				}
			}
			return ope;
		}

		internal InstructionParam2 getParam(int ipa)
		{
			return (InstructionParam2)VM.Instance.Ram [VM.Instance.CPU.Register.ip + ipa];
		}
	}

}

