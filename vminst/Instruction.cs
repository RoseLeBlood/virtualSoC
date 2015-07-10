//
//  Instruction.cs
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

namespace vminst
{
	public enum InstructionParam2 : int
	{
		Register = 0,
		Value = 1,
		Pointer = 2,
		Lable = 3,
		No = 9
	}
	public class Instruction
	{
		public string OpCode;
		public int OpParam1, OpParam2; //Param1 = IP Lenght int byte; Param2 = Parameter Count

		public Instruction (string text, int p1, int p2)
		{
			OpCode = text;
			OpParam1 = p1;
			OpParam2 = p2;
		}
	}
	public class Instructions : System.Collections.Generic.List<Instruction>
	{
		public Instructions()
		{
			Add (new Instruction (	"NOP", 	4, 	 0)); // OP(2)
			Add (new Instruction ( 	"PUSH",	9,   1)); // OP (2) R@#(2) 3(4)
			Add (new Instruction (	"POP", 	9, 	 1)); // OP (2) R@#(2) 3(4)
			Add (new Instruction (	"PEEK", 9, 	 1)); // OP (2) R@#(2) 3(4)
			Add (new Instruction (  "END",  4,   0));
			Add (new Instruction ( 	"JMP", 	9,   1)); 
			Add (new Instruction (	"LCK",  9,   1));
			Add (new Instruction (  "UCK",  9,   1));  
			Add (new Instruction (  "ADD",  9,   1)); 
			Add (new Instruction (  "SUB",  9,   1));
			Add (new Instruction (  "MUL",  9,   1));
			Add (new Instruction (  "DIV",  9,   1));
			Add (new Instruction (  "MOV", 14,   2)); // MOV(2) T(1)V(4) T(1)V(4) 
			Add (new Instruction ( 	"CLR",	9,   1));
			//
			Add (new Instruction (  "OR",  19, 	 3)); // OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "XOR", 19, 	 3)); // OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "AND", 19, 	 3)); // OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "NAND",19, 	 3)); // OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "NOR", 19, 	 3)); // OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "NOT", 14, 	 2)); // OP(2) T(1)V(4)  T(1)V(4) 
			Add (new Instruction (  "TIK",  9,   1)); // OP(2) T(1)V(4)

			Add (new Instruction (  "ADR",  13,   2)); // ADD Regster MOV(2) R(4) T(1)V(4) 
			Add (new Instruction (  "SBR",  13,   2)); // Sub Register MOV(2) R(4) T(1)V(4) 
			Add (new Instruction (  "MLR",  13,   2)); // MUL Register MOV(2) R(4) T(1)V(4) 
			Add (new Instruction (  "DVR",  13,   2)); // DIV Register MOV(2) R(4) T(1)V(4) 
			Add (new Instruction ( 	"RET",	4,    0));
		}
	}
	public class Register
	{
		public string Name;
		public int    Type;

		public Register(string n, int t) { Name = n; Type = t; }
	}
	public class Registers : System.Collections.Generic.List<Register>
	{
		public Registers()
		{
			Add (new Register ("SP", 0)); // Stack Pointer
			Add (new Register ("IP", 0)); // Intrsuction Pointer
			Add (new Register ("AX", 0)); // Akku A
			Add (new Register ("BX", 0)); // Akku B
		
			Add (new Register ("CF", 2)); // Carry Flag
			Add (new Register ("ZF", 2)); // Zero Flag
			Add (new Register ("UF", 2)); // Underflow Flag
			Add (new Register ("OF", 2)); // Overflow Flag
			Add (new Register ("SF", 2));  // Sign Flag

			Add (new Register ("CSP", 3)); // Cache Stack Pointer
			Add (new Register ("CM", 3));  // Cache Max Size 
			Add (new Register ("CP", 3));  // Cache Peek
		}
		public bool Contains(string str)
		{
			foreach (var item in this) {
				if (item.Name == str)
					return true;
			}
			return false;
		}
		public int IndexOf(string name)
		{
			for (int i = 0; i < Count; i++) {
				if (this [i].Name == name.ToUpper())
					return i;
			}
			return -1;
		}
	}
}

