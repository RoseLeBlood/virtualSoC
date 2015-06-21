//
//  Akku.cs
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
	public class Akku
	{
		private CPU	m_pCpu;


		public Akku (CPU pCpu)
		{
			m_pCpu = pCpu;
		}
		public void MoveAX(Int32 data) // MOV A, #6
		{
			m_pCpu.Register.ax = data;
		}
		public int MoveFromAX() // MOV REG, A
		{
			return m_pCpu.Register.ax;
		}
		public int MoveFromBX() // MOX REG, B
		{
			return m_pCpu.Register.bx;
		}
		public int Add(int data)
		{
			int result = 0;
			int carry = (int)(m_pCpu.Register.CarryFlag ? 1 : 0);

			m_pCpu.Register.OverFlow = Add (m_pCpu.Register.ax, data, ref carry, ref result);
			m_pCpu.Register.ax = result;
			m_pCpu.Register.CarryFlag = carry == 1;
			m_pCpu.Register.UnderFlow = false;

			return result;
		}
		public int Dec(int data)
		{
			return Add (data, -1);
		}
		public int Inc(int data)
		{
			return Add (data, +1);
		}
		public int CmplTwo(int data) 
		{
			return ~data + 1;
		}
		public int Mul(int data)
		{
			m_pCpu.Register.AkkuHelp = (data > 0);
			data = (!m_pCpu.Register.AkkuHelp) ? CmplTwo(data) : data;

			m_pCpu.Register.bx = m_pCpu.Register.ax;
		
			for (uint i = 0; i < data -1; i++) {
				m_pCpu.Register.ax = Add (m_pCpu.Register.bx, m_pCpu.Register.ax);
			}
			m_pCpu.Register.OverFlow = (m_pCpu.Register.ax < m_pCpu.Register.bx);
			m_pCpu.Register.ax = (!m_pCpu.Register.AkkuHelp) ? CmplTwo( m_pCpu.Register.ax) 
				: m_pCpu.Register.ax;
			

			return m_pCpu.Register.ax;
		}
		public int Div(int data)
		{
			if (m_pCpu.Register.ax == 0)
				m_pCpu.Register.DivByZero = true;

			m_pCpu.Register.AkkuHelp = (data > 0);
			data = (!m_pCpu.Register.AkkuHelp) ? CmplTwo(data) : data;

			m_pCpu.Register.cx = m_pCpu.Register.ax;

			m_pCpu.Register.bx = (m_pCpu.Register.ax < 0) ? 
				CmplTwo(m_pCpu.Register.ax) : m_pCpu.Register.ax;
			m_pCpu.Register.ax = 0;


			while (m_pCpu.Register.bx >= data) {
				m_pCpu.Register.bx = Add(m_pCpu.Register.bx, ~data + 1);
				m_pCpu.Register.CarryFlag = false;
				Add(1);
			}

			m_pCpu.Register.ax = (!m_pCpu.Register.AkkuHelp ^ !(m_pCpu.Register.cx > 0)) ? CmplTwo( m_pCpu.Register.ax) 
				: m_pCpu.Register.ax;
			
			return m_pCpu.Register.ax;
		}
		public int Sub(int data)
		{
			int result = 0;
			int carry = (int)(m_pCpu.Register.CarryFlag ? 1 : 0);

			m_pCpu.Register.OverFlow = Add (m_pCpu.Register.ax, ~data + 1, ref carry, ref result);
			m_pCpu.Register.ax = result;
			m_pCpu.Register.CarryFlag = carry == 1;

			if (m_pCpu.Register.OverFlow) {
				m_pCpu.Register.OverFlow = false;
			}
			else
				m_pCpu.Register.UnderFlow = (result <= int.MaxValue);

			return result;
		}
		private int Add(int A, int B)
		{
			int result = 0;
			int carry = (int)(m_pCpu.Register.CarryFlag ? 1 : 0);

			m_pCpu.Register.OverFlow = Add (A, B, ref carry, ref result);
			m_pCpu.Register.CarryFlag = carry == 1;
			return result;
		}
		private bool Add(int A, int B, ref int carry, ref int result)
		{
			for(int i=0; i<32; i++)
			{
				int n1 = (int)((A & (1<<(i)))>>(i)); //find the nth bit of p
				int n2 = (int)((B & (1<<(i)))>>(i)); //find the nth bit of q

				int s = (int)(n1 ^ n2 ^ carry); //sum of bits
				carry = (carry==0) ? (n1&n2): (n1 | n2); //calculate the carry for next step
				result = result | (s<<(i)); //calculate resultant bit
			}
			return (A > result || B > result);
		}
	}
}

