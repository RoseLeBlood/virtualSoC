//
//  DefinesDef.cs
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

namespace vmasm
{
	public class DefinesDef : System.Collections.Generic.Dictionary<string, string>
	{
		public DefinesDef ()
		{
		}

		public bool isLineADefine(string line)
		{
			if (line.Contains ("#define")) {

				string[] def = line.Split (' ');
				// 0 define
				// 1 KEY
				// 2 Value

				Add (def [1], Get(def [2]));

				return true;
			}
			return false;
		}
		public string GetOld(string key)
		{
			if (this.ContainsKey (key)) {
				this.TryGetValue (key, out key);
			}
			return key;
		}
		public string Get(string l1)
		{
			// MOV FB+1, #34
			// L1= FB+1
			foreach (var item in this) {
				if (l1.Contains (item.Key)) {
					l1 = l1.Replace (item.Key, item.Value);
				}
			}

			return l1;
		}
	}
}

