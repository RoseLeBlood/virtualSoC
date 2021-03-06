﻿//
//  Timer.cs
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
    public class TimerVMException : VMInterrupt
    {
        public TimerVMException(int id, int errorCode) : base()
        {
            base.DeviceID = (VMInterruptDeviceID)id;
            base.Code = errorCode;
            this.Type = VMInterruptType.Hardware;
        }

    }

    public class Timer : vmKomponente
	{
		private Vcsos.TimerIDs m_pTimer;

		public Timer (int id) : base("Timer R" + id.ToString(), "Anna-Sophia Schroeck")
		{
            m_pTimer = new Vcsos.TimerIDs(id);
		}
		public void Create()
		{
			m_pTimer.Interval =  VM.Instance.CurrentCore.Register.Stack.Pop32();
			m_pTimer.AutoReset = VM.Instance.CurrentCore.Register.Stack.Pop32() == 1;
			m_pTimer.Elapsed += TimerElapsed;

		}
		public void Start()
		{
			m_pTimer.Start ();
		}
		void TimerElapsed (object sender, System.Timers.ElapsedEventArgs e)
		{
            throw new TimerVMException(0, 0);

        }
	}
}

