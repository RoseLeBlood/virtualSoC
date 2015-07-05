﻿//
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
				output = System.IO.Path.GetFileNameWithoutExtension (args [0]) + ".bin";
			} else if (args.Length == 2) {
				input = args [0];
				output = args [1];
			} else {

				Console.WriteLine ("Using:\n\tvmasm.exe input.asm : output write to input.bin");
				Console.WriteLine ("\tor vmasm.exe input.asm output.bin");

				return;
			}
			Console.WriteLine ("Input File: {0} output: {1}", input, output);

			Assembler asm = new Assembler ();
			asm.l  = System.IO.File.ReadAllLines (input);
			asm.Comp (output);
		}
	}
}
