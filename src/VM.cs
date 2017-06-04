//
//  VM.cs
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
using Vcsos;
using System.Collections.Generic;

namespace Vcsos
{
    /// <summary>
    /// Diese Klasse beschreibt die Virtualle Maschine und ist eine
    /// zusammenstellung der verwendete Komponenten
    /// </summary>
	public class VM : List<IVMKomponente>
	{
        /// <summary>
        /// statische Instance der Klasse VM - Singleton
        /// </summary>
		private static VM m_instance = new VM ();
        /// <summary>
        /// Die Assembler Klasse schalt zentrale
        /// </summary>
		private DeAssembler m_pAssembler;

		public Core 	CurrentCore		{ get { return ((CPU)this[1]).CurrentCore; } }
        public CPU      CPU             { get { return (CPU)this[1]; } }
     
		public Memory Ram				{ get { return (Memory)this[0]; } }
		public Framebuffer FBdev		{ get { return (Framebuffer)this[2]; } }
        public Timer Timer0             { get { return (Timer)this[3]; } }
        public Timer Timer1             { get { return (Timer)this[4]; } }

        public bool IsAlive { get { return m_pAssembler.IsAlive; } }

        /// <summary>
        /// Gibt die Singleton Instance zzrück
        /// </summary>
		public static VM Instance
		{
			get { return m_instance; }
		}
        /// <summary>
        /// Erstellt die Virtaule maschine
        /// </summary>
		internal VM()
		{
			

		}
        /// <summary>
        /// Erstellt die Virtuale Maschine
        /// </summary>
        /// <param name="ramSize">Größe des Arbeitsspeicher</param>
		public void CreateVM(UInt32 ramSize, int iNumCores)
		{
            m_pAssembler = new DeAssembler(iNumCores);
            int newMemorySize = ramSize.ToBoundary(4);
			Add( new Memory (newMemorySize, "RAM", 500));

			if (newMemorySize != ramSize) 
				Console.WriteLine("VM: Memory was expanded from {0} bytes to {1} bytes to a page boundary." + System.Environment.NewLine,
					ramSize, newMemorySize);

			Add (new CPU (iNumCores));
			Add (new Framebuffer ());
			Add (new Timer (0));
            Add (new Timer (1));
			
		}
        /// <summary>
        /// Startet die Virtualle Maschine
        /// </summary>
        /// <param name="data">Programm code </param>
        /// <returns>rückgabe true bei keinen ausführ fehler</returns>
		public bool Start(byte[] data)
		{
			if (data.Length >= Ram.Size) {
                return false;
			} else {
				Ram.Write (data);

				CPU[0].Register.ip = 16;
				m_pAssembler.Start ();
				return true;
			}
		}
	}

}

