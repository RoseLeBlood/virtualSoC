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
    /// <summary>
    /// Akku des Virtualen SoCs
    /// </summary>
	public class Akku
	{
        /// <summary>
        /// verweis auf die CPU 
        /// </summary>
		private CPU	m_pCpu;

        /// <summary>
        /// Konstruktor des Akkus
        /// </summary>
        /// <param name="pCpu">Der zu verwendene CPU</param>
		public Akku (CPU pCpu)
		{
			m_pCpu = pCpu;
		}
        /// <summary>
        /// Weist dem CPU-Register AX ein wert zu 
        /// </summary>
        /// <param name="data"></param>
		public void MoveAX(Int32 data) // MOV A, #6
		{
			m_pCpu.L2.ax = data;
		}
        /// <summary>
        /// lese die Daten aus dem Register AX
        /// </summary>
        /// <returns>Daten aus dem Register AX</returns>
		public int MoveFromAX() // MOV REG, A
		{
			return m_pCpu.L2.ax;
		}
        /// <summary>
        /// lese die Daten aus dem Register BX
        /// </summary>
        /// <returns>Daten aus dem Register BX</returns>
		public int MoveFromBX() // MOX REG, B
		{
			return m_pCpu.L2.bx;
		}
        /// <summary>
        /// Addiere den Wert aus Parameter mit dem Register AX
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Die Summe der Addition</returns>
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
        /// <summary>
        /// Decriment der Daten 
        /// </summary>
        /// <param name="data">Die zu Decrimente Zahl</param>
        /// <returns>Das ergebniss</returns>
		public int Dec(int data)
		{
			return Add (data, -1);
		}
        /// <summary>
        /// Increnent
        /// </summary>
        /// <param name="data">die zu Incremierende Daten</param>
        /// <returns></returns>
		public int Inc(int data)
		{
			return Add (data, +1);
		}
        /// <summary>
        /// Berechne das 2er Kompliment
        /// </summary>
        /// <param name="data">Die Daten von denen das 2er Kompliment erstellt werden soll</param>
        /// <returns>Das 2er Kompliment</returns>
		public int CmplTwo(int data) 
		{
			return ~data + 1;
		}
        /// <summary>
        /// Multipliziere Data mit Register AX
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Das Ergebniss der Multiplikation</returns>
		public int Mul(int data)
		{
            //AkkuHelp Flag (intern) zuweisen
			m_pCpu.L2.AkkuHelp = (data > 0);
            // Testen ob AkkuHelp true ist dann erstelle das 2er Kompliment von data und weise
            // es data zu (2er Kompliment wenn data eine positive zahl enthält)
			data = (!m_pCpu.L2.AkkuHelp) ? CmplTwo(data) : data;

            // Weise dem Register BX den Wert aus dem Register BX zu
			m_pCpu.L2.bx = m_pCpu.L2.ax;
		
            // Addiere Register AX mit Register BX per Schleife (data-1)
			for (uint i = 0; i < data -1; i++) {
				m_pCpu.L2.ax = Add (m_pCpu.L2.bx, m_pCpu.L2.ax);
			}
            // Wenn Register AX kleiner als Register BX ist dann liegt ein Overflow vor
			m_pCpu.L2.OverFlow = (m_pCpu.L2.ax < m_pCpu.L2.bx);
            // Wenn Register AkkuHelp false ist erstelle das 2er Compliment vom Register AX 
            // und weise dies Register AX ist sonst Weise Register AX Revister AX zu
			m_pCpu.L2.ax = (!m_pCpu.L2.AkkuHelp) ? CmplTwo( m_pCpu.L2.ax) 
				: m_pCpu.L2.ax;
			
            // Return das Etgebnoss der Multiplikation
			return m_pCpu.L2.ax;
		}
        /// <summary>
        /// Dividiere Register AX mit data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Das ergebniss der Operation</returns>
		public int Div(int data)
		{
            if (m_pCpu.L2.ax == 0) // Ist Register AX gleich 0...
            {
                // Setze Flag DivByZero
                m_pCpu.L2.DivByZero = true;
                // gebe null zurück
                return 0;
            }
            // Ist data positive dann setze AkkuHelp auf true
            m_pCpu.L2.AkkuHelp = (data > 0);
            // weise data zu - Ist AkkuHelp true dann das 2er Kompliment sonst data
            data = (!m_pCpu.L2.AkkuHelp) ? CmplTwo(data) : data;

            // Weise dem Register CX den Wert aus Register AX zu
			m_pCpu.L2.cx = m_pCpu.L2.ax;
            // Weise dem Register BX zu - Ist Register AX negative das 2er Kompliment von Register AX
            // sonst den Wert aus Register AX
			m_pCpu.L2.bx = (m_pCpu.L2.ax < 0) ? 
				CmplTwo(m_pCpu.L2.ax) : m_pCpu.L2.ax;
            // Weise dem Register AX null zu
			m_pCpu.L2.ax = 0;

            // Führe die Division als Addition durch 
			while (m_pCpu.L2.bx >= data) { // solange wie Register BX größer data ist 
                // Weise dem Register BX das Ergebniss aus der Addition von Register BX und
                // dem 2er Kompliment von data zu
				m_pCpu.L2.bx = Add(m_pCpu.L2.bx, ~data + 1);
                // Weise dem Carry Flag false zu
				m_pCpu.L2.CarryFlag = false;
                // Addiere auf Register AX eine 1 
				Add(1);
			}
            // Weise dem Register AX das Ergebniss der Division zu
			m_pCpu.L2.ax = (!m_pCpu.L2.AkkuHelp ^ !(m_pCpu.L2.cx > 0)) ? CmplTwo( m_pCpu.L2.ax) 
				: m_pCpu.L2.ax;
			
            // Gebe das Ergebniss aus dem Register AX zurück
			return m_pCpu.L2.ax;
		}
        /// <summary>
        /// Subtration vom Register AX 
        /// </summary>
        /// <param name="data">Die Zahl die von AX subrrahiert werden soll</param>
        /// <returns>Ergebniss der Subtration</returns>
		public int Sub(int data)
		{
			int result = 0; // result : speichern des Ergebnisses
			int carry = (int)(m_pCpu.L2.CarryFlag ? 1 : 0); // carry : carry-flag als int from bool

            // Subtraction per Zweiterkompliment und Addition
            // benutze old carry und speichere das ergebniss in result
			m_pCpu.L2.OverFlow = Add (m_pCpu.L2.ax, ~data + 1, ref carry, ref result);
            // speichern des Ergebnisses der Addition in den Register AX
			m_pCpu.L2.ax = result;
            // speichern des Carry Flags
			m_pCpu.L2.CarryFlag = carry == 1;

            // ist OverFlow (OF) flag gesetzt...
			if (m_pCpu.L2.OverFlow) {
                // Dann löscbe das Flag
				m_pCpu.L2.OverFlow = false;
			}
			else
                // Wenn nicht dann schaue ob underflow vorliegt (TODO)
				m_pCpu.L2.UnderFlow = (result <= int.MaxValue);

            // rückgabe der variable result
            return result;
		}
        /// <summary>
        /// Addition zweier Zahlen (32bit)
        /// </summary>
        /// <param name="A">Erster Summand der Addition</param>
        /// <param name="B">Zweiter Summand der Addition</param>
        /// <returns>Ergebniss der Addition von A und B</returns>
		private int Add(int A, int B)
		{
			int result = 0;
			int carry = (int)(m_pCpu.L2.CarryFlag ? 1 : 0);

			m_pCpu.L2.OverFlow = Add (A, B, ref carry, ref result);
			m_pCpu.L2.CarryFlag = carry == 1;
			return result;
		}
        /// <summary>
        /// Interne Addition  Bitweise 
        /// </summary>
        /// <param name="A">Erster Summand der Addition</param>
        /// <param name="B">Zweiter Summand der Addition</param>
        /// <param name="carry">reference des Carry bits</param>
        /// <param name="result">reference auf das Ergebniss der Addition</param>
        /// <returns>true bei OverFlow</returns>
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

