//
//  IInputType.cs
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
using DotArgs;

namespace vmcli
{
	public interface IInputType
	{
		byte[] LoadFromStream(System.IO.Stream stream);
		byte[] LoadFromFile(string path);
	}

	public class InputFactory
	{
		public static IInputType GetInputClass(CommandLineArgs cmd)
		{
			if (cmd.GetValue<string> ("t") == "gz") {
				return new GzipInputType();
			}
			if (cmd.GetValue<string> ("t") == "deflate") {
				return new DeflateInputType ();
			}
			return new RawInputType();
		}
	}
}

