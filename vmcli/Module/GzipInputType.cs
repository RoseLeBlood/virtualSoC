//
//  GzipInputType.cs
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
using System.IO.Compression;
using System.IO;

namespace vmcli
{
	public class GzipInputType : IInputType
	{
		public byte[] LoadFromFile (string path)
		{
			try
			{
				return LoadFromStream (new System.IO.FileStream (path, System.IO.FileMode.Open));
			}
			catch {
				return new byte[] { (byte)4 };
			}
		}
		public byte[] LoadFromStream (System.IO.Stream stream)
		{
			if (stream == null)
				return new byte[] { (byte)4 };

			byte[] str = null;

			using (GZipStream gStream = new GZipStream (stream, CompressionMode.Decompress)) {
				using (MemoryStream mStream = new MemoryStream ()) {
					gStream.CopyTo (mStream);
					mStream.Position = 0;
					str = new byte[mStream.Length];
					gStream.Read (str, 0, (int)stream.Length);
				}
			}
			return str;
		}
	}
}

