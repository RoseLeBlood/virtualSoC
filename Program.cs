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
using Vcsos.Komponent;

namespace Vcsos
{
	class MainClass
	{
		static ushort GetInput(string frage)
		{
			ushort k;
			Console.Write (frage);
			do
			{
				if(ushort.TryParse(Console.ReadLine(), out k))
					break;
				else
					Console.Write("Falsche Eingabe\n" + frage);
			}while(true);
			return k;
		}
		static string GetInputString(string frage)
		{
			string k;
			Console.Write (frage);
			do
			{
				k = Console.ReadLine();
				if(System.IO.File.Exists(k))
					break;
				else
					Console.Write("Falsche Eingabe\n" + frage);
			}while(true);
			return k;
		}
		public static void Main (string[] args)
		{
			VM.Instance.CreateVM (512);
			string kernel = GetInputString ("Path to kernel image: ");
			ushort org = GetInput ("Start-Adress: ");

			VM.Instance.Start (kernel, org);

			while (VM.Instance.IsAlive) {
			}
			string r = VM.Instance.Ram.ToString ();
			Console.WriteLine (r + System.Environment.NewLine);
			Console.WriteLine (VM.Instance.CPU.Register.ToString ());

		}
	}
}
