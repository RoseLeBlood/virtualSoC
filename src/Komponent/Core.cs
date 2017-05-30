//
//  CPU.cs
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

namespace Vcsos.Komponent
{
	public class Core : vmKomponente
	{ 
        protected Stack      m_pStack;

		private Register	 m_pRegister; 	// L2 Current Register
        private int          m_iCoreNumber;
		protected Akku 		 m_pAkku;
        protected bool       m_bStarted;
		public const float 	 BaudMhz = 22.1184f;

		public ulong   Ticks { get; private set; }


		public Akku Akku
		{
			get { return m_pAkku; }
			set { m_pAkku = value; }
		}
		public bool Running
        {
            get { return m_bStarted; }
        }

        

        public Register Register
		{
			get { return m_pRegister; }
			set { m_pRegister = value; }
		}
		public Stack CallStack
		{
			get { return m_pStack; }
		}
        public Stack Stack
        {
            get { return m_pStack; }
        }
        public int CoreNumber
        {
            get { return m_iCoreNumber; }
        }
		public Core (int number, bool running=false) : base("Referenz Core " + number.ToString(), "Anna-Sophia Schroeck")
		{
            m_pStack = new Stack(1024*1024, "CoreStack" + number.ToString());
            m_iCoreNumber = number;
            m_pRegister = new Register (this, m_pStack.Size);
			m_pAkku = new Akku (this);
			
			 m_bStarted = running;
            m_pRegister.ip = 7;

        }
        public int And(int v1, int v2)
		{
			return v1 & v2;
		}
		public int Or(int v1, int v2)
		{
			return v1 | v2;
		}
		public int Xor(int v1, int v2)
		{
			return v1 ^ v2;
		}
		public int Neg(int v1)
		{
			return -v1;
		}
		public int XNor(int v1, int v2)
		{
			return -(v1 ^ v2);
		}
		public int Nor(int v1, int v2)
		{
			return -(v1 | v2);
		}
		public int Nand(int v1, int v2)
		{
			return -(v1 & v2);
		}
		internal void Tick() {
			Ticks += 1;
		}
	}
}

