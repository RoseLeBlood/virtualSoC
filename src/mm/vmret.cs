//
//  vmret.cs
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

namespace Vcsos.mm
{
	public class vmret : vmoperator
	{
		public string Name {
			get { return "RET"; }
		}
        public string Info
        {
            get { return "Springe zurück aus dem Unterprogramm - RET"; }
        }
        public bool ParseAndRun (ParserFactory factory)
		{
			VM.Instance.CurrentCore.Register.Set ("IP", VM.Instance.CurrentCore.CallStack.Pop32 ());

			return true;
		}
	}
}

