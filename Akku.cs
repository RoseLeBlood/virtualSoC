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
			m_pCpu.L2.ax = data;
		}
		public int MoveFromAX() // MOV REG, A
		{
			return m_pCpu.L2.ax;
		}
		public int MoveFromBX() // MOX REG, B
		{
			return m_pCpu.L2.bx;
		}
		public int Add(int data)
		{
			int result = 0;
			int carry = (int)(m_pCpu.L2.CarryFlag ? 1 : 0);

			m_pCpu.L2.OverFlow = Add (m_pCpu.L2.ax, data, ref carry, ref result);
			m_pCpu.L2.ax = result;
			m_pCpu.L2.CarryFlag = carry == 1;
			m_pCpu.L2.UnderFlow = false;

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
			m_pCpu.L2.AkkuHelp = (data > 0);
			data = (!m_pCpu.L2.AkkuHelp) ? CmplTwo(data) : data;

			m_pCpu.L2.bx = m_pCpu.L2.ax;
		
			for (uint i = 0; i < data -1; i++) {
				m_pCpu.L2.ax = Add (m_pCpu.L2.bx, m_pCpu.L2.ax);
			}
			m_pCpu.L2.OverFlow = (m_pCpu.L2.ax < m_pCpu.L2.bx);
			m_pCpu.L2.ax = (!m_pCpu.L2.AkkuHelp) ? CmplTwo( m_pCpu.L2.ax) 
				: m_pCpu.L2.ax;
			

			return m_pCpu.L2.ax;
		}
		public int Div(int data)
		{
			if (m_pCpu.L2.ax == 0)
				m_pCpu.L2.DivByZero = true;

			m_pCpu.L2.AkkuHelp = (data > 0);
			data = (!m_pCpu.L2.AkkuHelp) ? CmplTwo(data) : data;

			m_pCpu.L2.cx = m_pCpu.L2.ax;

			m_pCpu.L2.bx = (m_pCpu.L2.ax < 0) ? 
				CmplTwo(m_pCpu.L2.ax) : m_pCpu.L2.ax;
			m_pCpu.L2.ax = 0;


			while (m_pCpu.L2.bx >= data) {
				m_pCpu.L2.bx = Add(m_pCpu.L2.bx, ~data + 1);
				m_pCpu.L2.CarryFlag = false;
				Add(1);
			}

			m_pCpu.L2.ax = (!m_pCpu.L2.AkkuHelp ^ !(m_pCpu.L2.cx > 0)) ? CmplTwo( m_pCpu.L2.ax) 
				: m_pCpu.L2.ax;
			
			return m_pCpu.L2.ax;
		}
		public int Sub(int data)
		{
			int result = 0;
			int carry = (int)(m_pCpu.L2.CarryFlag ? 1 : 0);

			m_pCpu.L2.OverFlow = Add (m_pCpu.L2.ax, ~data + 1, ref carry, ref result);
			m_pCpu.L2.ax = result;
			m_pCpu.L2.CarryFlag = carry == 1;

			if (m_pCpu.L2.OverFlow) {
				m_pCpu.L2.OverFlow = false;
			}
			else
				m_pCpu.L2.UnderFlow = (result <= int.MaxValue);

			return result;
		}
		private int Add(int A, int B)
		{
			int result = 0;
			int carry = (int)(m_pCpu.L2.CarryFlag ? 1 : 0);

			m_pCpu.L2.OverFlow = Add (A, B, ref carry, ref result);
			m_pCpu.L2.CarryFlag = carry == 1;
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

