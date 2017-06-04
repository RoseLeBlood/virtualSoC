
//
//  DeflateOutput.cs
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
using System.IO;
using System.IO.Compression;

namespace vmasm
{
	public class DeflateOutput : IModuleOutput
	{
		public string Name {
			get { return "Deflate"; }
		}

		public bool WriteToFile (byte[] asmdata, string file)
		{

			file = file.Contains("bin.df") ? file : file  + "bin.df";

			using(var sfile = new System.IO.FileStream(file, System.IO.FileMode.Create))
			{
				using(DeflateStream gStream = new DeflateStream(sfile, CompressionMode.Compress))
					gStream.Write (asmdata, 0, asmdata.Length);
			}
			return true;
		}
        public MemoryStream WriteToStream(byte[] asmdata)
        {
            MemoryStream st = new MemoryStream();
            using (DeflateStream gStream = new DeflateStream(st, CompressionMode.Compress))
                gStream.Write(asmdata, 0, asmdata.Length);
            return st;
        }
    }
}

