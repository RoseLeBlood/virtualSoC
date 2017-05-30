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
		private Core	m_pCpu;

        /// <summary>
        /// Konstruktor des Akkus
        /// </summary>
        /// <param name="pCpu">Der zu verwendene CPU</param>
		public Akku (Core pCpu)
		{
			m_pCpu = pCpu;
		}
        /// <summary>
        /// Weist dem CPU-Register AX ein wert zu 
        /// </summary>
        /// <param name="data"></param>
		public void MoveAX(Int32 data) // MOV A, #6
		{
			m_pCpu.Register.eax = data;
		}
        /// <summary>
        /// lese die Daten aus dem Register AX
        /// </summary>
        /// <returns>Daten aus dem Register AX</returns>
		public int MoveFromAX() // MOV REG, A
		{
			return m_pCpu.Register.eax;
		}
        /// <summary>
        /// lese die Daten aus dem Register BX
        /// </summary>
        /// <returns>Daten aus dem Register BX</returns>
		public int MoveFromBX() // MOX REG, B
		{
			return m_pCpu.Register.ebx;
		}
        /// <summary>
        /// Addiere den Wert aus Parameter mit dem Register AX
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Die Summe der Addition</returns>
		public int Add(int data)
		{
			int result = 0;
			int carry = (int)(m_pCpu.Register.CarryFlag ? 1 : 0);

			m_pCpu.Register.OverFlow = Add (m_pCpu.Register.eax, data, ref carry, ref result);
			m_pCpu.Register.eax = result;
			m_pCpu.Register.CarryFlag = carry == 1;
			m_pCpu.Register.UnderFlow = false;

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
			m_pCpu.Register.AkkuHelp = (data > 0);
            // Testen ob AkkuHelp true ist dann erstelle das 2er Kompliment von data und weise
            // es data zu (2er Kompliment wenn data eine positive zahl enthält)
			data = (!m_pCpu.Register.AkkuHelp) ? CmplTwo(data) : data;

            // Weise dem Register BX den Wert aus dem Register BX zu
			m_pCpu.Register.ebx = m_pCpu.Register.eax;
		
            // Addiere Register AX mit Register BX per Schleife (data-1)
			for (uint i = 0; i < data -1; i++) {
				m_pCpu.Register.eax = Add (m_pCpu.Register.ebx, m_pCpu.Register.eax);
			}
            // Wenn Register AX kleiner als Register BX ist dann liegt ein Overflow vor
			m_pCpu.Register.OverFlow = (m_pCpu.Register.eax < m_pCpu.Register.ebx);
            // Wenn Register AkkuHelp false ist erstelle das 2er Compliment vom Register AX 
            // und weise dies Register AX ist sonst Weise Register AX Revister AX zu
			m_pCpu.Register.eax = (!m_pCpu.Register.AkkuHelp) ? CmplTwo( m_pCpu.Register.eax) 
				: m_pCpu.Register.eax;
			
            // Return das Etgebnoss der Multiplikation
			return m_pCpu.Register.eax;
		}

        
        public int Mul(int a, int data)
        {
            //AkkuHelp Flag (intern) zuweisen
            m_pCpu.Register.AkkuHelp = (data > 0);
            // Testen ob AkkuHelp true ist dann erstelle das 2er Kompliment von data und weise
            // es data zu (2er Kompliment wenn data eine positive zahl enthält)
            data = (!m_pCpu.Register.AkkuHelp) ? CmplTwo(data) : data;

            // Weise dem Register BX den Wert aus dem Register BX zu
            m_pCpu.Register.ebx = a;

            // Addiere Register AX mit Register BX per Schleife (data-1)
            for (uint i = 0; i < data - 1; i++)
            {
                a = Add(m_pCpu.Register.ebx, a);
            }
            // Wenn Register AX kleiner als Register BX ist dann liegt ein Overflow vor
            m_pCpu.Register.OverFlow = (a < m_pCpu.Register.ebx);
            // Wenn Register AkkuHelp false ist erstelle das 2er Compliment vom Register AX 
            // und weise dies Register AX ist sonst Weise Register AX Revister AX zu
            return (!m_pCpu.Register.AkkuHelp) ? CmplTwo(a)
                : a;
        }
        /// <summary>
        /// <summary>
        /// Dividiere Register AX mit data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Das ergebniss der Operation</returns>
		public int Div(int data)
		{
            if (m_pCpu.Register.eax == 0) // Ist Register AX gleich 0...
            {
                // Setze Flag DivByZero
                m_pCpu.Register.DivByZero = true;
                // gebe null zurück
                return 0;
            }
            // Ist data positive dann setze AkkuHelp auf true
            m_pCpu.Register.AkkuHelp = (data > 0);
            // weise data zu - Ist AkkuHelp true dann das 2er Kompliment sonst data
            data = (!m_pCpu.Register.AkkuHelp) ? CmplTwo(data) : data;

            // Weise dem Register CX den Wert aus Register AX zu
			m_pCpu.Register.ecx = m_pCpu.Register.eax;
            // Weise dem Register BX zu - Ist Register AX negative das 2er Kompliment von Register AX
            // sonst den Wert aus Register AX
			m_pCpu.Register.ebx = (m_pCpu.Register.eax < 0) ? 
				CmplTwo(m_pCpu.Register.eax) : m_pCpu.Register.eax;
            // Weise dem Register AX null zu
			m_pCpu.Register.eax = 0;

            // Führe die Division als Addition durch 
			while (m_pCpu.Register.ebx >= data) { // solange wie Register BX größer data ist 
                // Weise dem Register BX das Ergebniss aus der Addition von Register BX und
                // dem 2er Kompliment von data zu
				m_pCpu.Register.ebx = Add(m_pCpu.Register.ebx, ~data + 1);
                // Weise dem Carry Flag false zu
				m_pCpu.Register.CarryFlag = false;
                // Addiere auf Register AX eine 1 
				Add(1);
			}
            // Weise dem Register AX das Ergebniss der Division zu
			m_pCpu.Register.eax = (!m_pCpu.Register.AkkuHelp ^ !(m_pCpu.Register.ecx > 0)) ? CmplTwo( m_pCpu.Register.eax) 
				: m_pCpu.Register.eax;
			
            // Gebe das Ergebniss aus dem Register AX zurück
			return m_pCpu.Register.eax;
		}
        public int Div(int a, int data)
        {
            if (a == 0) // Ist a gleich 0...
            {
                // Setze Flag DivByZero
                m_pCpu.Register.DivByZero = true;
                // gebe null zurück
                return 0;
            }
            // Ist data positive dann setze AkkuHelp auf true
            m_pCpu.Register.AkkuHelp = (data > 0);
            // weise data zu - Ist AkkuHelp true dann das 2er Kompliment sonst data
            data = (!m_pCpu.Register.AkkuHelp) ? CmplTwo(data) : data;

            // Weise dem Register CX den Wert a zu
            m_pCpu.Register.ecx = a;
            // Weise dem Register BX zu - Ist a negative das 2er Kompliment von a
            // sonst a
            m_pCpu.Register.ebx = (a < 0) ?
                CmplTwo(a) : a;
            // Weise dem Register a null zu
            a = 0;

            // Führe die Division als Addition durch 
            while (m_pCpu.Register.ebx >= data)
            { // solange wie Register BX größer data ist 
              // Weise dem Register BX das Ergebniss aus der Addition von Register BX und
              // dem 2er Kompliment von data zu
                m_pCpu.Register.ebx = Add(m_pCpu.Register.ebx, ~data + 1);
                // Weise dem Carry Flag false zu
                m_pCpu.Register.CarryFlag = false;
                // Addiere auf a eine 1 
                Add(a, 1);
            }
            // gibt das Ergebniss der Division zu
            return (!m_pCpu.Register.AkkuHelp ^ !(m_pCpu.Register.ecx > 0)) ? CmplTwo(a) : a;
        }
        /// <summary>
        /// Subtration vom Register AX 
        /// </summary>
        /// <param name="data">Die Zahl die von AX subrrahiert werden soll</param>
        /// <returns>Ergebniss der Subtration</returns>
		public int Sub(int data)
		{
			int result = 0; // result : speichern des Ergebnisses
			int carry = (int)(m_pCpu.Register.CarryFlag ? 1 : 0); // carry : carry-flag als int from bool

            // Subtraction per Zweiterkompliment und Addition
            // benutze old carry und speichere das ergebniss in result
			m_pCpu.Register.OverFlow = Add (m_pCpu.Register.eax, ~data + 1, ref carry, ref result);
            // speichern des Ergebnisses der Addition in den Register AX
			m_pCpu.Register.eax = result;
            // speichern des Carry Flags
			m_pCpu.Register.CarryFlag = carry == 1;

            // ist OverFlow (OF) flag gesetzt...
			if (m_pCpu.Register.OverFlow) {
                // Dann löscbe das Flag
				m_pCpu.Register.OverFlow = false;
			}
			else
                // Wenn nicht dann schaue ob underflow vorliegt (TODO)
				m_pCpu.Register.UnderFlow = (result <= int.MaxValue);

            // rückgabe der variable result
            return result;
		}
        public int Sub(int a, int b)
        {
            int result = 0; // result : speichern des Ergebnisses
            int carry = (int)(m_pCpu.Register.CarryFlag ? 1 : 0); // carry : carry-flag als int from bool

            // Subtraction per Zweiterkompliment und Addition
            // benutze old carry und speichere das ergebniss in result
            m_pCpu.Register.OverFlow = Add(a, ~b + 1, ref carry, ref result);

            // speichern des Carry Flags
            m_pCpu.Register.CarryFlag = carry == 1;

            // ist OverFlow (OF) flag gesetzt...
            if (m_pCpu.Register.OverFlow)
            {
                // Dann löscbe das Flag
                m_pCpu.Register.OverFlow = false;
            }
            else
                // Wenn nicht dann schaue ob underflow vorliegt (TODO)
                m_pCpu.Register.UnderFlow = (result <= int.MaxValue);

            // rückgabe der variable result
            return result;
        }
        /// <summary>
        /// Addition zweier Zahlen (32bit)
        /// </summary>
        /// <param name="A">Erster Summand der Addition</param>
        /// <param name="B">Zweiter Summand der Addition</param>
        /// <returns>Ergebniss der Addition von A und B</returns>
		public int Add(int A, int B)
		{
			int result = 0;
			int carry = (int)(m_pCpu.Register.CarryFlag ? 1 : 0);

			m_pCpu.Register.OverFlow = Add (A, B, ref carry, ref result);
			m_pCpu.Register.CarryFlag = carry == 1;
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

