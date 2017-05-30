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
        Core            m_pCore;

		public Stack Stack
		{
			get { return m_pCore.Stack; }
		}
        /// <summary>
        /// StackPointer - CoreStack not RAM
        /// </summary>
        public int sp
        {
            get { return m_pMemRegister.Read32(0); }
            set { m_pMemRegister.Write(value, 0); }
        }
        /// <summary>
        /// Instruction Pointer
        /// </summary>
        public int ip
        {
            get { return m_pMemRegister.Read32(4); }
            set { m_pMemRegister.Write(value, 4); }
        }
        /// <summary>
        /// Akku A 32Bit
        /// </summary>
        public int eax
        {
            get { return m_pMemRegister.Read32(8); }
            set
            {
                m_pMemRegister.Write(value, 8);
                if (eax == 0)
                {
                    ZeroFlag = true;
                }
                else if (eax <= 0)
                {
                    SignFlag = false;
                }
                else
                {
                    ZeroFlag = false;
                    SignFlag = true;
                }
            }
        }
        /// <summary>
        /// Akku B 32bit (Not Flags set)
        /// </summary>
        public int ebx
        {
            get { return m_pMemRegister.Read32(12); }
            set { m_pMemRegister.Write(value, 12); }
        }
        /// <summary>
        /// Akku C 32 Bit (no flags set)
        /// </summary>
        public int ecx
        {
            get { return m_pMemRegister.Read32(16); }
            set { m_pMemRegister.Write(value, 16); }
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
        internal bool Sync
        {
            get { return m_pMemRegister.Read16(28).Get(0); }
            set { m_pMemRegister.Write(m_pMemRegister.Read16(28).Set(value, 0), 28); }
        }
        
        #region Reserved

        internal bool RescA
        {
            get { return m_pMemRegister.Read16(28).Get(2); }
            set { m_pMemRegister.Write(m_pMemRegister.Read16(28).Set(value, 2), 28); }
        }
        internal bool RescB
        {
            get { return m_pMemRegister.Read16(28).Get(3); }
            set { m_pMemRegister.Write(m_pMemRegister.Read16(28).Set(value, 3), 28); }
        }
        internal bool RescC
        {
            get { return m_pMemRegister.Read16(28).Get(4); }
            set { m_pMemRegister.Write(m_pMemRegister.Read16(28).Set(value, 4), 28); }
        }
        internal bool RescD
        {
            get { return m_pMemRegister.Read16(28).Get(5); }
            set { m_pMemRegister.Write(m_pMemRegister.Read16(28).Set(value, 5), 28); }
        }
        internal bool RescE
        {
            get { return m_pMemRegister.Read16(28).Get(6); }
            set { m_pMemRegister.Write(m_pMemRegister.Read16(28).Set(value, 6), 28); }
        }
        internal bool RescF
        {
            get { return m_pMemRegister.Read16(28).Get(7); }
            set { m_pMemRegister.Write(m_pMemRegister.Read16(28).Set(value, 7), 28); }
        }
        #endregion


		public Register (Core core, int iSpAddr)
		{
            m_pCore = core;
            m_pMemRegister = new Memory (36, "CoreRegister" + core.CoreNumber.ToString(), 0);

            sp = iSpAddr;
			ip = 0;
			eax = eax.RandR ();
			ebx = ebx.RandR ();
            ecx = ecx.RandR ();

			DivByZero = false;
			OverFlow = false;
			UnderFlow = false;
			Exections = false;
			Sync = true;
            
		}
		public override string ToString ()
		{
			return string.Format ("[Register: SignFlag={0}, ZeroFlag={1}, OverFlow={2}, CarryFlag={3}, UnderFlow={4}, DivByZero={5}, " +
				"sp={6}, ip={7}, ax={8}, bx={9}, cx={10}]", SignFlag, ZeroFlag, OverFlow, CarryFlag, UnderFlow, DivByZero, sp, ip, eax, ebx, ecx);
		}
		public int Get(string name)
		{
			switch (name) {
			case "AX":
				return eax;
			case "BX":
				return ebx;
			case "SP":
				return sp;
			case "IP":
				return ip;
			case "TIK":
				return (int)VM.Instance.CurrentCore.Ticks;
			default:
				throw new Exception ("Register/Flag with Name: " + name + " not found");
			}
		}
		public void Set(string name, int v)
		{
			switch (name) {
			case "AX":
				eax = v;
				break;
			case "BX":
				ebx = v;
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

