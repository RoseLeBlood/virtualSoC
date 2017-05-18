//
//  Register.cs
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
using System.Collections;

namespace Vcsos.Komponent
{
	public class Register
	{
		Memory			m_pMemRegister;
		Stack    		m_pStack;
		//BitArray 		m_flags;

		public Stack Stack
		{
			get { return m_pStack; }
			set { m_pStack = value; }
		}
		public bool SignFlag 
		{
			get { return m_pMemRegister.Read16(20).Get(0); }
			set { m_pMemRegister.Write(m_pMemRegister.Read16(20).Set(value, 0), 20); }
		}
		public bool ZeroFlag 
		{
			get { return m_pMemRegister.Read16(20).Get(1); }
			set { m_pMemRegister.Write(m_pMemRegister.Read16(20).Set(value, 1), 20); }
		}
		public bool OverFlow
		{
			get { return m_pMemRegister.Read16(20).Get(2); }
			set { m_pMemRegister.Write(m_pMemRegister.Read16(20).Set(value, 2), 20); }
		}
		public bool CarryFlag
		{
			get { return m_pMemRegister.Read16(20).Get(3); }
			set { m_pMemRegister.Write(m_pMemRegister.Read16(20).Set(value, 3), 20); }
		}
		public bool UnderFlow
		{
			get { return m_pMemRegister.Read16(20).Get(4); }
			set { m_pMemRegister.Write(m_pMemRegister.Read16(20).Set(value, 4), 20); }
		}
		public bool DivByZero
		{
			get { return m_pMemRegister.Read16(20).Get(5); }
			set { m_pMemRegister.Write(m_pMemRegister.Read16(20).Set(value, 5), 20); }
		}
		internal bool AkkuHelp
		{
			get { return m_pMemRegister.Read16(20).Get(6); }
			set { m_pMemRegister.Write(m_pMemRegister.Read16(20).Set(value, 6), 20); }
		}
		internal bool Exections
		{
			get { return m_pMemRegister.Read16(20).Get(7); }
			set { m_pMemRegister.Write(m_pMemRegister.Read16(20).Set(value, 7), 20); }
		}
		public int sp
		{
			get { return m_pMemRegister.Read32(0); }
			set	{ m_pMemRegister.Write(value,0); }
		}
		public int ip
		{
			get { return m_pMemRegister.Read32(4); }
			set	{ m_pMemRegister.Write(value,4); }
		}
		public int ax
		{
			get { return m_pMemRegister.Read32(8); }
			set	
			{ 
				m_pMemRegister.Write(value,8);
				if (ax == 0) {
					ZeroFlag = true;
				}
				else if(ax <= 0) {
					SignFlag = false;
				} else {
					ZeroFlag = false;
					SignFlag = true;
				}
			}
		}
		public int bx
		{
			get { return m_pMemRegister.Read32(12); }
			set	{ m_pMemRegister.Write(value,12); }
		}
		public int cx
		{
			get { return m_pMemRegister.Read32(16); }
			set	{ m_pMemRegister.Write(value,16); }
		}
		public int pid
		{
			get { return m_pMemRegister.Read32(24); }
			set	{ m_pMemRegister.Write(value,24); }
		}
		public Register (int stackAddr = -1)
		{
			m_pMemRegister = new Memory (28, "Register");

			sp = (stackAddr == -1 ? VM.Instance.Ram.Size - 1 : stackAddr);
			ip = 0;
			ax = ax.RandR ();
			bx = bx.RandR ();

			DivByZero = false;
			OverFlow = false;
			UnderFlow = false;
			Exections = false;
			pid = 0;
			m_pStack = new Stack ();
		}
		public override string ToString ()
		{
			return string.Format ("[Register: SignFlag={0}, ZeroFlag={1}, OverFlow={2}, CarryFlag={3}, UnderFlow={4}, DivByZero={5}, " +
				"sp={6}, ip={7}, ax={8}, bx={9}, cx={10}]", SignFlag, ZeroFlag, OverFlow, CarryFlag, UnderFlow, DivByZero, sp, ip, ax, bx, cx);
		}
		public int Get(string name)
		{
			switch (name) {
			case "AX":
				return ax;
			case "BX":
				return bx;
			case "SP":
				return sp;
			case "IP":
				return ip;
			case "TIK":
				return (int)Core.Ticks;
			default:
				throw new Exception ("Register/Flag with Name: " + name + " not found");
			}
		}
		public void Set(string name, int v)
		{
			switch (name) {
			case "AX":
				ax = v;
				break;
			case "BX":
				bx = v;
				break;
			case "SP":
				sp = v;
				break;
			case "IP":
				ip = v;
				break;
			default:
				throw new Exception ("Register/Flag with Name: " + name + " not found");
			}
		}
	}
}

