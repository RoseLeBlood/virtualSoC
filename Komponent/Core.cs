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
		protected CacheStack m_pCallStack;  // L3 Call Stack  
		private Register	 m_pRegister; 	// L2 Current Register
        private int          m_iCoreNumber;
		protected Akku 		 m_pAkku;
		public const float 	 BaudMhz = 22.1184f;

		public static ulong   Ticks { get; private set; }


		public Akku Akku
		{
			get { return m_pAkku; }
			set { m_pAkku = value; }
		}
		
		public Register Register
		{
			get { return m_pRegister; }
			set { m_pRegister = value; }
		}
		public CacheStack Stack
		{
			get { return m_pCallStack; }
		}
        public int CoreNumber
        {
            get { return m_iCoreNumber; }
        }
		public Core (int number) : base("Referenz Core " + number.ToString(), "Anna-Sophia Schroeck")
		{
            m_iCoreNumber = number;
            m_pRegister = new Register ();
			m_pAkku = new Akku (this);
			
			m_pCallStack = new CacheStack  (135, "Core-Register" + number.ToString()); // 32 Unterprogramme
		}
		public virtual void Run()
		{

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

