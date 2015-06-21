﻿//
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
	public class CPU
	{
		protected CacheStack m_pCacheStack; // L1 IPC  
		private Register	 m_pRegister; 	// L2 Current Register

		protected Akku 		 m_pAkku;
		public const float 	 BaudMhz = 22.1184f;

		public static ulong   Ticks { get; private set; }

		public Register Register
		{
			get { return m_pRegister; }
			set { m_pRegister = value; }
		}
		public Akku Akku
		{
			get { return m_pAkku; }
			set { m_pAkku = value; }
		}
		public CacheStack Cache
		{
			get { return m_pCacheStack; }
		}
		public CPU ()
		{
			m_pRegister = new Register ();
			m_pAkku = new Akku (this);
			m_pCacheStack = new CacheStack ();
		}
		public virtual void Run()
		{

		}

		public void And(ref int v1, int v2)
		{
			for (int i = 0; i < sizeof(int); i++)
				v1.Set ( v1.Get (i) & v2.Get (i), i);
		}
		public void Or(ref int v1, int v2)
		{
			for (int i = 0; i < 32; i++)
				v1.Set (v1.Get (i) | v2.Get (i), i);
		}
		public void Xor(ref int v1, int v2)
		{
			for (int i = 0; i < 32; i++)
				v1.Set (v1.Get (i) ^ v2.Get (i),i);
		}
		public void Neg(ref int v1)
		{
			for (int i = 0; i < 32; i++)
				v1.Set (!v1.Get (i),i);
		}
		public void XNor(ref int v1, int v2)
		{
			Xor (ref v1, v2);
			Neg (ref v1);
		}
		public void Nand(ref int v1, int v2)
		{
			And (ref v1, v2);
			Neg (ref v1);
		}
		internal void Tick() {
			Ticks += 1;
		}
	}
}

