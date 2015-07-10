//
//  Program.cs
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
using System.Collections.Generic;

namespace vmasm
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string input = "";
			string output = "";

			if (args.Length == 1) {
				input = args [0];
				output = System.IO.Path.GetFileNameWithoutExtension (args [0]) + ".bin.gz";
			} else if (args.Length == 2) {
				input = args [0];
				output = args [1];
			} else {

				Console.WriteLine ("Using:\n\tvmasm.exe input.asm : output write to input.bin.gz");
				Console.WriteLine ("\tor vmasm.exe input output");

				return;
			}
			Console.WriteLine ("Input File: {0} output: {1}", input, output);

			Assembler asm = new Assembler ();
			asm.l = PreProcess (input);
			asm.Comp (output);
		}
		private static string[] PreProcess(string input)
		{
			//System.IO.File.WriteAllText(".output.tmp", System.IO.File.ReadAllText (input));
			string[] text = System.IO.File.ReadAllLines(input);
			for(int i = 0; i < text.Length; i++) {
				string item = text [i];
				if (CheakLineOfInclude (item)) {
					string _incText = GetFileFromPaths (GetSubstringByString ('\'', item), new string[] { "." });

					text [i] = _incText;
				}
				// TODO: Text in Temp File schreiben und dann einlesen weider und asm übergeben!!
			}
			System.IO.File.WriteAllLines (".comp.tml", text);
			string[] l = System.IO.File.ReadAllLines (".comp.tml");
			System.IO.File.Delete (".comp.tml");

			return l;
		}
		private static bool CheakLineOfInclude(string li)
		{
			return li.Contains("#include");
		}
		private static string GetSubstringByString(char a, string c)
		{
			return c.Split (a)[1];
		}
		private static string GetFileFromPaths(string file, string[] paths)
		{
			foreach (var i in paths) {
				string path = System.IO.Path.Combine (i, file);
				if (System.IO.File.Exists (path)) {
					return System.IO.File.ReadAllText (path);
				}
			}
			throw new Exception ("File " + file + " not found");
		}
	}
}
