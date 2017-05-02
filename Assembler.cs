//
//  Assembler.cs
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
using vminst;
using Vcsos.Komponent;
using Vcsos.mm;

namespace Vcsos
{
	public enum VMExecptionType : int
	{
		Error,
		Hardware
	}

    public class VMExections : Exception
    {
        public int ErrorCode { get; set; }
    }
    /// <summary>
    /// Assembler Klasse. Diese Klasse führt die Bearbeitzng aus
    /// </summary>
	public class Assembler
	{
        /// <summary>
        /// Timer der Sincronisation und Takt gabe 
        /// </summary>
		private System.Timers.Timer m_pTimer;
        /// <summary>
        /// Lebt das System noch
        /// </summary>
		private bool m_bIsAlive;

        /// <summary>
        /// Gewählter Parser
        /// </summary>
		private ParserFactory m_pParser;

        /// <summary>
        /// get ob system noch läuft
        /// </summary>
		public bool IsAlive { get { return m_bIsAlive; } }

		public Assembler ()
		{
			// bischen drlsseln des Timers
			m_pTimer = new System.Timers.Timer (1000.0 / (CPU.BaudMhz));
            // weise dem Timer event Elapsed die Function TimerElapsed zu
            m_pTimer.Elapsed += TimerElapsed;
            // Setze Timer AutoRest zu false (aus)
			m_pTimer.AutoReset = false;

            // Erstelle die Parser Faktory
			m_pParser = new ParserFactory ();
            // Setze die Variable m_bIsAlive auf true
            m_bIsAlive = true;

		}
        /// <summary>
        /// Starte das System
        /// </summary>
		public void Start()
		{
            // Starte den Timer somit das System
			m_pTimer.Start ();
		}
        /// <summary>
        /// Timer Elapsed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void TimerElapsed (object sender, System.Timers.ElapsedEventArgs e)
		{
			try // versuche ...
			{
                // weise op, den Aktuellen OpCode zu aus der Position  im RAM
                // Position: Register IP
				int op = VM.Instance.Ram.Read32 (VM.Instance.CPU.L2.ip);

                // Wandele und Führe den OpCode aus 
                // weise den rückgabe wert m_bIsAlive zu
                m_bIsAlive = m_pParser.ParseAndRun (op);
                // Führe die Funktion CPU.Tick() aus
				VM.Instance.CPU.Tick ();

			}
            // bei fehler
			catch(System.Exception ex) {
				int errCode = -1; // dekkariere errCode und weise der Variable -1 zu
				if (ex is VMExections) // ist die Exception eine VMExections dann..
                    errCode = (ex as VMExections).ErrorCode; // weuse errCode die VMExections.errCode zu

                // Ist Exceptions Flg gesetzt dann... 
                if (VM.Instance.CPU.L2.Exections) { 
                    // Push Register IP auf den Stack
					VM.Instance.CPU.L3.Push32 (VM.Instance.CPU.L2.ip);
                    // Push errCode auf den Szack
					VM.Instance.CPU.L2.Stack.Push32 (errCode);
                    // Push VMExecptionType.Error auf dem Stack
                    VM.Instance.CPU.L2.Stack.Push32 ((int)VMExecptionType.Error);
                    // Setze den Register IP auf 4
					VM.Instance.CPU.L2.ip = 4;
				} else { // wenn Exception nicht aktiviert sind...
                    // dann schalte das system auf tot und gib den Fehler auf die Console aus
					Console.WriteLine (ex.ToString ());
					m_bIsAlive = false;
				}	
			}
            // Wenn m_bIsAlive true ist dann
            if (m_bIsAlive)
				m_pTimer.Start (); // starte den Server neu
		}
	}

}

