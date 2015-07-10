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
using DotArgs;

namespace vmasm
{
	class MainClass
	{
		internal static UInt32 GetValueFromArg(CommandLineArgs cmd, string val)
		{
			UInt32 outv;
			if (!UInt32.TryParse (cmd.GetValue<string> (val), out outv))
				throw  new ArgumentException ();

			return outv;
		}
		public static void Main (string[] args)
		{
			CommandLineArgs cmd = new CommandLineArgs();

			cmd.RegisterArgument( "i", new OptionArgument( null, true ) { HelpMessage="inputfile" } );
			cmd.RegisterArgument( "o", new OptionArgument( ".bin", false )  { HelpMessage="write output to an outfile" });
			cmd.RegisterArgument( "f", new OptionArgument( "raw", false) { HelpMessage=" select an output format RAW,gz,deflate" } );
			cmd.SetDefaultArgument( "i" );
			cmd.RegisterHelpArgument();

			if( !cmd.Validate(args) )
			{
				cmd.PrintHelp();
				return;
			}

			string input = cmd.GetValue<string> ("i");
			string output = cmd.GetValue<string> ("o");
			if(output == ".bin")
				output = System.IO.Path.GetFileNameWithoutExtension(input) + ".bin";
			
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
