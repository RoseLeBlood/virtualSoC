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
using Vcsos.Komponent;
using Vcsos.mm;

namespace Vcsos
{
    /// <summary>
    /// Interrupt Type
    /// </summary>
    public enum VMInterruptType : int
	{
		Error,
		Hardware,
        Core
	}

    public enum VMInterruptDeviceID : int
    {
        Timer0,
        Timer1,
        Framebuffer,
        Parallel,
        Serial,
        GPIO,
        I2C,
        Flash
    }
    /// <summary>
    /// Interrupt Helper class
    /// </summary>
    public class VMInterrupt : Exception
    {
        /// <summary>
        /// Which Hardware send this Interrupt
        /// </summary>
        public VMInterruptDeviceID DeviceID { get; set; }
        /// <summary>
        /// Hardware, Software Interrupt Code (ErrorCode)
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Interrruct Type 
        /// </summary>
        public VMInterruptType Type { get; set; }
    }
    
    /// <summary>
    /// Assembler Klasse. Diese Klasse führt die Bearbeitzng aus
    /// </summary>
	public class Assembler
	{
        /// <summary>
        /// Timer der Sincronisation und Takt gabe 
        /// </summary>
		private System.Collections.Generic.List<TimerIDs> m_pTimer;
        /// <summary>
        /// Lebt das System noch
        /// </summary>
		private bool m_bIsAlive;

        private object m_objLock;

        /// <summary>
        /// Gewählter Parser
        /// </summary>
		private ParserFactory m_pParser;

        /// <summary>
        /// get ob system noch läuft
        /// </summary>
		public bool IsAlive { get { return m_bIsAlive; } }

		public Assembler (int numCores)
		{
            m_pTimer = new System.Collections.Generic.List<TimerIDs>();
            m_objLock = new object();

            for (int i = 0; i < numCores; i++)
            {
                m_pTimer.Add( new TimerIDs(1000.0 / (Core.BaudMhz), i) );


                // weise dem Timer event Elapsed die Function TimerElapsed zu
                m_pTimer[i].Elapsed += TimerElapsed;
                // Setze Timer AutoRest zu false (aus)
                m_pTimer[i].AutoReset = false;                
            }

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
            foreach (var item in m_pTimer)
            {
                item.Start();
            }
            // Starte den Timer somit das System
			//m_pTimer[0].Start ();
		}
        /// <summary>
        /// Timer Elapsed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void TimerElapsed (object sender, System.Timers.ElapsedEventArgs ea)
		{
            
            lock (m_objLock)
            {
                if (sender is TimerIDs)
                {
                    TimerIDs p = sender as TimerIDs;
                    VM.Instance.CPU.CurrentCoreID = p.ID;
                }
               
                

                try // versuche ...
                {
                    // weise op, den Aktuellen OpCode zu aus der Position  im RAM
                    // Position: Register IP
                    int op = VM.Instance.Ram.Read32(VM.Instance.CurrentCore.Register.ip);

                    // Wandele und Führe den OpCode aus 
                    // weise den rückgabe wert m_bIsAlive zu
                    m_bIsAlive = m_pParser.ParseAndRun(op);
                    // Führe die Funktion CPU.Tick() aus
                    VM.Instance.CurrentCore.Tick();
                    
                }
                // bei fehler
                catch (System.Exception ex)
                {
                    
                    if (ex is VMInterrupt) // ist die Exception eine VMExections dann..
                    {
                        VMInterrupt e = (ex as VMInterrupt); // weuse errCode die VMExections.errCode zu

                        // Ist Exceptions Flg gesetzt dann... 
                        if (VM.Instance.CurrentCore.Register.Exections)
                        {
                            // Push Register IP auf den Stack
                            VM.Instance.CurrentCore.Stack.Push32(VM.Instance.CurrentCore.Register.ip);
                            // Push devicd id
                            VM.Instance.CurrentCore.Stack.Push32((int)e.DeviceID);
                            // Push errCode auf den Stack
                            VM.Instance.CurrentCore.Stack.Push32(e.Code);
                            // Push VMExecptionType.Error auf dem Stack
                            VM.Instance.CurrentCore.Stack.Push32((int)e.Type);
                            // Setze den Register IP auf 4
                            VM.Instance.CurrentCore.Register.ip = 4;
                        }
                        else
                        { // wenn Exception nicht aktiviert sind...
                          // dann schalte das system auf tot und gib den Fehler auf die Console aus
                            Console.WriteLine(ex.ToString());
                            m_bIsAlive = false;
                        }
                    }
                }
                // Wenn m_bIsAlive true ist dann
                if (m_bIsAlive)
                {
                    m_pTimer[VM.Instance.CPU.CurrentCoreID].Start(); // starte den Core neu
                } 
                else
                {
                    // When Current Core Sync set then halt all Cores 
                    if(VM.Instance.CurrentCore.Register.Sync)
                    {
                        foreach (var item in m_pTimer)
                        {
                            item.Stop();
                        }
                    }
                }
            }
		}
        public override string ToString()
        {
            return m_pParser.ToString();
        }
    }

}

