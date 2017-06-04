//
//  IModuleOutput.cs
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

namespace vmasm
{
	public enum ModuleOutputType
	{
		RAW,
		GZIP,
		Deflate,
        Stream
	}

	public interface IModuleOutput
	{
		string Name { get; }
		bool WriteToFile(byte[] asmdata, string file);
        MemoryStream WriteToStream(byte[] asmdata);
	}



	public class ModuleOutputFactory
	{
		public static bool WriteToFile(byte[] asmdata, string file, string strType)
		{
			strType = strType.ToUpper ();
			return WriteToFile (asmdata, file, (strType == "RAW") ? ModuleOutputType.RAW :
				(strType == "GZ") ? ModuleOutputType.GZIP : ModuleOutputType.Deflate);
		}
		public static bool WriteToFile(byte[] asmdata, string file, ModuleOutputType eType)
		{
			IModuleOutput output = null;
			switch (eType) {
			case ModuleOutputType.RAW:
				output = new RawOutput ();
				break;
			case ModuleOutputType.Deflate:
				output = new DeflateOutput ();
				break;
			case ModuleOutputType.GZIP:
				output = new GzipOutput ();
				break;
            
			default:
				break;
			}
			if (output != null) {
				return output.WriteToFile (asmdata, file);
			}
			return false;
		}
        public static MemoryStream WriteToFile(byte[] asmdata, ModuleOutputType eType)
        {
            IModuleOutput output = null;
            switch (eType)
            {
                case ModuleOutputType.RAW:
                    output = new RawOutput();
                    break;
                case ModuleOutputType.Deflate:
                    output = new DeflateOutput();
                    break;
                case ModuleOutputType.GZIP:
                    output = new GzipOutput();
                    break;

                default:
                    break;
            }
            if (output != null)
            {
                return output.WriteToStream(asmdata);
            }
            return null;
        }
    }
}

