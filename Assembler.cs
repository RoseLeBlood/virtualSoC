//
//  Assembler.cs
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
	public class Assembler
	{
		private System.Timers.Timer m_pTimer;
		private bool m_bIsAlive;
		private Instructions m_pInstructions;
		private Registers	m_pRegisters;

		public bool IsAlive { get { return m_bIsAlive; } }

		public Assembler ()
		{
			// bischen dröseln
			m_pTimer = new System.Timers.Timer (1000.0 / (CPU.BaudMhz));
			m_pTimer.Elapsed += TimerElapsed;
			m_pTimer.AutoReset = false;
			m_pInstructions = new Instructions ();
			m_pRegisters = new Registers ();
			m_bIsAlive = true;

		}
		public void Start()
		{
			m_pTimer.Start ();
		}
		void TimerElapsed (object sender, System.Timers.ElapsedEventArgs e)
		{
			int op = VM.Instance.Ram.Read32 (VM.Instance.CPU.Register.ip);
			Instruction c = m_pInstructions [op];
			Console.WriteLine ("{3} [{2}] {0} {1}", op, c.OpCode, 
				VM.Instance.CPU.Register.ip, CPU.Ticks);

			if (c.OpParam2 == 0)
				ParseParamZero (c);
			else if (c.OpParam2 == 1)
				ParseParamOne (c);
			else
				ParseParamTwo (c);

			VM.Instance.CPU.Tick ();

			if(m_bIsAlive)
				m_pTimer.Start ();

			VM.Instance.CPU.Register.ip += c.OpParam1;
		}
		private InstructionParam2 getParam(int ipa)
		{
			return (InstructionParam2)VM.Instance.Ram [VM.Instance.CPU.Register.ip + ipa];
		}
		private void ParseParamZero(Instruction c)
		{
			switch (c.OpCode) {
			case "END":
				m_bIsAlive = false;
				break;
			case "NOP":
				break;
			}
		}
		private void ParseParamOne(Instruction c)
		{
			InstructionParam2 param1 = getParam(4);
			int param1V = VM.Instance.Ram.Read32 (VM.Instance.CPU.Register.ip + 5);

			switch (c.OpCode) {
			case "CLR":
				if (param1 == InstructionParam2.Value)
					VM.Instance.Ram.Write (0, (uint)param1V);
				else if (param1 == InstructionParam2.Register) {
					VM.Instance.CPU.Register.Set (m_pRegisters [param1V].Name, 0);
				}
				break;
			case "PUSH":
				if (param1 == InstructionParam2.Value)
					VM.Instance.CPU.Register.Stack.Push32 (param1V);
				else if (param1 == InstructionParam2.Register) {
					VM.Instance.CPU.Register.Stack.Push32 (VM.Instance.CPU.Register.Get (m_pRegisters [param1V].Name));
				}
				break;
			case "POP":
				if (param1 == InstructionParam2.Value)
					VM.Instance.Ram.Write (VM.Instance.CPU.Register.Stack.Pop32 (), (uint)param1V);
				else if (param1 == InstructionParam2.Register) {
					VM.Instance.CPU.Register.Set (m_pRegisters [param1V].Name, VM.Instance.CPU.Register.Stack.Pop32 ());

				}
				break;
			case "PEEK":
				if (param1 == InstructionParam2.Value)
					VM.Instance.Ram.Write (VM.Instance.CPU.Register.Stack.Peek32 (), (uint)param1V);
				else if (param1 == InstructionParam2.Register) {
					VM.Instance.CPU.Register.Set (m_pRegisters [param1V].Name, VM.Instance.CPU.Register.Stack.Peek32 ());

				}
				break;
			case "ADD":
				if (param1 == InstructionParam2.Value)
					VM.Instance.CPU.Akku.Add (param1V);
				else if (param1 == InstructionParam2.Register) {
					VM.Instance.CPU.Akku.Add (VM.Instance.CPU.Register.Get (m_pRegisters [param1V].Name));
				}
				break;
			case "SUB":
				if (param1 == InstructionParam2.Value)
					VM.Instance.CPU.Akku.Sub (param1V);
				else if (param1 == InstructionParam2.Register) {
					VM.Instance.CPU.Akku.Sub (VM.Instance.CPU.Register.Get (m_pRegisters [param1V].Name));
				}
				break;
			case "MUL":
				if (param1 == InstructionParam2.Value)
					VM.Instance.CPU.Akku.Mul (param1V);
				else if (param1 == InstructionParam2.Register) {
					VM.Instance.CPU.Akku.Mul (VM.Instance.CPU.Register.Get (m_pRegisters [param1V].Name));
				}
				break;
			case "DIV":
				if (param1 == InstructionParam2.Value)
					VM.Instance.CPU.Akku.Div (param1V);
				else if (param1 == InstructionParam2.Register) {
					VM.Instance.CPU.Akku.Div (VM.Instance.CPU.Register.Get (m_pRegisters [param1V].Name));
				}
				break;
			case "JMP":
				VM.Instance.CPU.Register.Set ("IP", param1V);
				break;
			}

		}
		private void ParseParamTwo(Instruction c)
		{
			// 97 - 110
			// OP(97,4) P(101,1) VAL(102 ,4) P(106,1) VAL(107, 4) 111 
			InstructionParam2 param1 = getParam(4); // 101 4
			int param1V = VM.Instance.Ram.Read32 (VM.Instance.CPU.Register.ip + 5); //106
			InstructionParam2 param2 = getParam(9); // 110
			int param2V = VM.Instance.Ram.Read32 (VM.Instance.CPU.Register.ip + 10); // 111


			switch (c.OpCode) {
			case "MOV":
				if (param2 == InstructionParam2.Value)
					VM.Instance.CPU.Register.Stack.Push32 (VM.Instance.Ram.Read32 (param2V));
				else if (param2 == InstructionParam2.Register) {
					VM.Instance.CPU.Register.Stack.Push32 (VM.Instance.CPU.Register.Get (m_pRegisters [param2V].Name));
				}

				if (param1 == InstructionParam2.Value)
					VM.Instance.Ram.Write (VM.Instance.CPU.Register.Stack.Pop32 (), (uint)param1V);
				else if (param1 == InstructionParam2.Register)
					VM.Instance.CPU.Register.Set (m_pRegisters [param1V].Name, VM.Instance.CPU.Register.Stack.Pop32 ());
				break;
			}
		}
	}
}

