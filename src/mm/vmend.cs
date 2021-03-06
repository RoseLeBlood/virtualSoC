﻿//
//  vmend.cs
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

namespace Vcsos.mm
{
	public class vmend : vmoperator
	{
		public string Name {
			get { return "HLT"; }
		}
        public string Info
        {
            get { return "Beendet das Programm - HLT "; }
        }
        public bool ParseAndRun (ParserFactory factory)
		{
			/*#if DEBUG

			string r = VM.Instance.Ram.ToString();
			Console.WriteLine(r);
            for (int i = 0; i < VM.Instance.CPU.Cores; i++)
            {
                Console.WriteLine("Core{0}: {1}", i, VM.Instance.CPU[i].Register.ToString());
            }
			#endif*/
			return false;
		}
	}
}

