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
		public static void AkkuTest(int a, int b, int t)
		{
			switch (t) {
			case 0: // add
				VM.Instance.CPU.Akku.MoveAX (a);
				VM.Instance.CPU.Akku.Add  (b);
				Console.WriteLine (a + " + " + b + " = " + VM.Instance.CPU.Akku.MoveFromAX ().ToString ());
				break;
			case 1: // sub
				VM.Instance.CPU.Akku.MoveAX (a);
				VM.Instance.CPU.Akku.Sub  (b);
				Console.WriteLine (a + " - " + b + " = " + VM.Instance.CPU.Akku.MoveFromAX ().ToString ());

				break;
			case 2: // mul
				VM.Instance.CPU.Akku.MoveAX (a);
				VM.Instance.CPU.Akku.Mul  (b);
				Console.WriteLine (a + " * " + b + " = " + VM.Instance.CPU.Akku.MoveFromAX ().ToString ());

				break;
			case 3: // div
				VM.Instance.CPU.Akku.MoveAX (a);
				VM.Instance.CPU.Akku.Div  (b);
				Console.WriteLine (a + " ÷ " + b + " = " + VM.Instance.CPU.Akku.MoveFromAX ().ToString ());

				break;
			}
		}
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
		public static void Main (string[] args)
		{
			VM VirtualMaschine = VM.Instance;

			VirtualMaschine.CreateVM (512);
			VirtualMaschine.Start ();
			while (VirtualMaschine.IsAlive) {
			}
			string r = VirtualMaschine.Ram.ToString ();
			Console.WriteLine (r + System.Environment.NewLine);
			Console.WriteLine (VirtualMaschine.CPU.Register.ToString ());

			/*short a = (short)GetInput ("Geben Sie eine Zahl für 'A' ein: ");
			short b = (short)GetInput ("Geben Sie eine Zahl für 'B' ein: ");
			for (int i = 0; i < 5; i++) {
				AkkuTest (+a, +b, i);
				AkkuTest (+a, -b, i);
				AkkuTest (-a, +b, i);
				AkkuTest (-a, -b, i);
				Console.WriteLine ();
			}*/
		}
	}
}
