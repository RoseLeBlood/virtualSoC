
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
    /// <summary>
    /// Typ arten eines Parameter
    /// </summary>
	public enum InstructionParam2 : int
	{
        /// <summary>
        /// Nachfolgene Bits ist ein Adresse zu einen Register
        /// </summary>
		Register = 0,
        /// <summary>
        /// Nachfolgene Bits ist eine Value
        /// </summary>
		Value = 1,
        /// <summary>
        /// Nachfolgene Bits ist ein zeiger
        /// </summary>
		Pointer = 2,
        /// <summary>
        /// Nachfolgene Bits ist ein Label
        /// </summary>
		Lable = 3,
		No = 9
	}
    /// <summary>
    /// Basis Klasse jeder Instruction
    /// </summary>
	public class Instruction
	{
        /// <summary>
        /// Name / String des Codes
        /// </summary>
		public string OpCode;
        /// <summary>
        /// param1 = instruction Lenght in bytes; Param2 Parameter Anzahl
        /// </summary>
		public int OpParam1, OpParam2; //Param1 = IP Lenght int byte; Param2 = Parameter Count

        /// <summary>
        /// Konstruter zum erstellen eines OpCode
        /// </summary>
        /// <param name="text">Name des OpCodes</param>
        /// <param name="p1">Lenght in bytes </param>
        /// <param name="p2">Parameter Count</param>
        public Instruction (string text, int p1, int p2)
		{
			OpCode = text;
			OpParam1 = p1;
			OpParam2 = p2;
		}
        /// <summary>
        /// ToString()
        /// </summary>
        /// <returns>instruction als string</returns>
		public override string ToString ()
		{
			return string.Format ("{0} {1} {3}", OpCode, OpParam1, OpParam2);
		}
	}
    /// <summary>
    /// Liste aller vorhandenen Instructionen
    /// </summary>
	public class Instructions : System.Collections.Generic.List<Instruction>
	{
		public Instructions()
		{
			Add (new Instruction (	"NOP", 	4, 	 0)); // OP(4)
			Add (new Instruction ( 	"PUSH",	9,   1)); // OP (4) R@#(2) 3(4)
			Add (new Instruction (	"POP", 	9, 	 1)); // OP (4) R@#(2) 3(4)
			Add (new Instruction (	"PEEK", 9, 	 1)); // OP (4) R@#(2) 3(4)
			Add (new Instruction (  "HLT",  0,   0));
			Add (new Instruction ( 	"JMP", 	0,   1)); 
			Add (new Instruction (	"LCK",  9,   1)); // not imp
			Add (new Instruction (  "UCK",  9,   1)); // not imp 
			Add (new Instruction (  "ADD",  9,   1)); 
			Add (new Instruction (  "SUB",  9,   1));
			Add (new Instruction (  "MUL",  9,   1));
			Add (new Instruction (  "DIV",  9,   1));
			Add (new Instruction (  "MOV", 14,   2)); // MOV(4) T(1)V(4) T(1)V(4) 
			Add (new Instruction ( 	"CLR",	9,   1)); // register, pointer, flag löschen
			Add (new Instruction (  "OR",  19, 	 3)); //+ OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "XOR", 19, 	 3)); //+ OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "AND", 19, 	 3)); //+ OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "NAND",19, 	 3)); // OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "NOR", 19, 	 3)); //+ OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "NXOR",19,   3)); //+ OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "NOT", 14, 	 2)); //+ OP(2) T(1)V(4)  T(1)V(4) 
			Add (new Instruction ( 	"RET",	0,   0));
			Add (new Instruction (  "CALL", 0, 	 1));
            Add (new Instruction (  "ADR",  19,   3)); // ADD return to V1 not to AX OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
			Add (new Instruction (  "SBR",  19,   3)); // SUB return to V1 not to AX OP(2) T(1)V(4)  T(1)V(4) T(1)V(4) 
			Add (new Instruction (  "MLR",  19,   3)); // MUL return to V1 not to AX OP(2) T(1)V(4)  T(1)V(4) T(1)V(4) 
			Add (new Instruction (  "DVR",  19,   3)); // DIV return to V1 not to AX OP(2) T(1)V(4)  T(1)V(4) T(1)V(4) 
            Add (new Instruction (  "JC",    0,   0)); // Jump Carry OP(4) T(1)V(4)
            Add (new Instruction (  "JNC",   0,   0)); // Jump not carry
            Add (new Instruction (  "JO",    0,   0)); // Jump overflow
            Add (new Instruction (  "JNO",   0,   0)); // Jump not overflow 
            Add (new Instruction (  "INC",   9,   1));
            Add (new Instruction (  "DEC",   9,   1));
            Add (new Instruction(   "INV",   9,   1)); // register, flags, pointer invetieren
            Add (new Instruction(   "STO",   9,   1)); // register, flags, pointer = 1
            //
            Add (new Instruction("JG", 19, 0)); // Jump greater: o1 o2 addr - OP(2) T(1)V(4) > T(1)V(4) T(1)V(4)
            Add (new Instruction("JGE", 19, 0)); // Jump greater equel  OP(2) T(1)V(4) >= T(1)V(4) T(1)V(4) 
            Add (new Instruction("JL", 19, 0)); // Jump less OP(2) T(1)V(4) < T(1)V(4) T(1)V(4) 
            Add (new Instruction("JLE", 19, 0)); // Jump less equel OP(2) T(1)V(4) <= T(1)V(4) T(1)V(4) 


            Add(new Instruction("CSC", 9, 1)); // SwitchCore OP(4) T(1)V(4)
            Add(new Instruction("CSP", 14, 1)); // StartCore Prozess  OP(4) T(1)V(4) T(1)V(4) - CSP CoreID IP 
            Add(new Instruction("CPU", 9, 1)); // Push to IPC OP(4) T(1)V(4)
            Add(new Instruction("CPO", 9, 1)); // Pop to IPC OP(4) T(1)V(4)
            Add(new Instruction("CEX", 19, 1)); // Send Exception CEX ExID {User daten} (push to IPC and stopp cC)
            

            Add(new Instruction (  "FBI",   4,   0));
			Add (new Instruction (  "FBSET",19,   3));
		}
	}
    public enum RegisterType : int
    {
        Cpu = 0,
        Flags = 2,
        Cache = 3
    }
    /// <summary>
    /// Item der Registers Liste
    /// </summary>
	public class Register
	{
        /// <summary>
        /// Name des Registers
        /// </summary>
		public string Name;
        /// <summary>
        /// Typ des Registers
        /// </summary>
		public RegisterType Type;

        /// <summary>
        /// Konstructor für ein Register
        /// </summary>
        /// <param name="n">Name des Registers</param>
        /// <param name="t">Typ des Registers</param>
		public Register(string n, int t) { Name = n; Type = (RegisterType)t; }
	}
    /// <summary>
    /// Liste aller vorhandenen Register
    /// </summary>
	public class Registers : System.Collections.Generic.List<Register>
	{
		public Registers()
		{
			Add (new Register ("SP", 0)); //0 Stack Pointer 
			Add (new Register ("IP", 0)); //1 Intrsuction Pointer
			Add (new Register ("AX", 0)); //2 Akku A
			Add (new Register ("BX", 0)); //3 Akku B
			Add (new Register ("TIK", 0)); //4 Current Tik
		
			Add (new Register ("CF", 2)); //5 Carry Flag
			Add (new Register ("ZF", 2)); //6 Zero Flag
			Add (new Register ("UF", 2)); //7 Underflow Flag
			Add (new Register ("OF", 2)); //8 Overflow Flag
			Add (new Register ("SF", 2));  //9 Sign Flag

			Add (new Register ("CSP", 3)); // Cache Stack Pointer
			Add (new Register ("CM", 3));  // Cache Max Size 
			Add (new Register ("CP", 3));  // Cache Peek
		}
        /// <summary>
        /// Ist der Register Name in der Liste vorhanden
        /// </summary>
        /// <param name="str">Register name der gesucht wird</param>
        /// <returns>true wenn der Registername in der Liste ist</returns>
		public bool Contains(string str)
		{
			foreach (var item in this) {
				if (item.Name == str)
					return true;
			}
			return false;
		}
        /// <summary>
        /// Index of des gesuchten Registers
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Index des Registers in der liste</returns>
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

