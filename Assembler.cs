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
	public class Assembler
	{
		private System.Timers.Timer m_pTimer;
		private bool m_bIsAlive;

		private ParserFactory m_pParser;

		public bool IsAlive { get { return m_bIsAlive; } }

		public Assembler ()
		{
			// bischen dröseln
			m_pTimer = new System.Timers.Timer (1000.0 / (CPU.BaudMhz));
			m_pTimer.Elapsed += TimerElapsed;
			m_pTimer.AutoReset = false;

			m_pParser = new ParserFactory ();
			m_bIsAlive = true;

		}
		public void Start()
		{
			m_pTimer.Start ();
		}
		void TimerElapsed (object sender, System.Timers.ElapsedEventArgs e)
		{
			int op = VM.Instance.Ram.Read32 (VM.Instance.CPU.L2.ip);

			m_bIsAlive = m_pParser.ParseAndRun (op);
			if(m_bIsAlive)
				m_pTimer.Start ();

			VM.Instance.CPU.Tick ();
		}
	}
}

