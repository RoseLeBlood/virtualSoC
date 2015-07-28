//
//  RawInputType.cs
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

namespace vmcli
{
	public class RawInputType : IInputType
	{
		#region IInputType implementation
		public byte[] LoadFromStream (System.IO.Stream stream)
		{
			if (stream == null)
				return new byte[] { (byte)4 };
			byte[] str = new byte[stream.Length];
			stream.Position = 0;
			stream.Read (str, 0, (int)stream.Length);

			return str;
		}
		public byte[] LoadFromFile (string path)
		{
			try
			{
				return LoadFromStream (new System.IO.FileStream (path, System.IO.FileMode.Open));
			}
			catch {
				throw new Exception ("file not found");
			}
		}
		#endregion
		
	}
}

