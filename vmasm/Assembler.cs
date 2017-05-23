//
//  Assembler.cs
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
using vminst;
using System.Globalization;
using NCalc;

namespace vmasm
{
	public class Assembler
	{
		Instructions m_pInstruction = new Instructions ();
		Registers m_pRegisters = new Registers ();
		System.Collections.Generic.Dictionary<string, int> m_pLabels = new System.Collections.Generic.Dictionary<string, int>();
		DefinesDef m_pDefines;

		string[] m_r;
		public  string[] l {
			get { return m_r; }
			set {
				m_r = new string[value.Length];
				for(int i=0; i < value.Length; i++)
					m_r[i] = value[i].Trim ('\t').Replace("\t".ToString(), " ");
			}
		}
        public string InstructionsList
        {
            get { return m_pInstruction.ToString(); }
        }
		public  byte[] Comp()
		{
			m_pLabels = new System.Collections.Generic.Dictionary<string, int> ();
			m_pDefines = new DefinesDef();
			MemoryStream Stream = new MemoryStream ();
			//string[] l = System.IO.File.ReadAllLines (inPutFile);
			var writer = new BinaryWriter (Stream);
			{
				for (int i = 0; i < l.Length; i++) {
					string l0 = l [i];
					string l1 = l0.Split (';') [0].Trim();

					if (l1 == string.Empty)
						continue;

					if (m_pDefines.isLineADefine (l1))
						continue;

					if (l1.ToUpper ().Contains ("ORG")) {
						int Org = int.Parse (l1.Split (' ') [1].Replace ("0x", ""), NumberStyles.HexNumber);
						Console.WriteLine ("[ORG] {0}", Org);
						writer.BaseStream.Seek (Org, SeekOrigin.Begin);
						continue;
					} else if (l1.Contains (":")) {
						// LABEL: EQU 34
						CompLabel(l1,writer);
						continue;
					} else if (l1[0] == 'L' && l1[1] == 'B') { // Label vordifinieren

						string ll = l1.Split(' ')[1];
						if (!m_pLabels.ContainsKey ("." + ll)) {
							m_pLabels.Add ("." + ll, int.Parse (l1.Split (' ') [2]));
						}

					} else {
						if (!CompLine (l1, writer))
							return null;
					}
				}


				
			}
			Stream.Position = 0;
			byte[] date = new byte[Stream.Length];
			Stream.Read (date, 0, date.Length);
			return date;
		}
		private void CompLabel(string l1, BinaryWriter writer)
		{
			// Label: ...
			// Label: EQU 32
			string[] la = l1.Split(':');
			// 0 = LABEL
			// 1 = NULL or (EQU 32)
			int Pos = (int)writer.BaseStream.Position;
			string label = la [0];

			if (la.Length == 2 && la[1].Contains("EQU")) {
				string t = la [1].TrimStart (' ').Remove (0, 3).Trim ();

				t = m_pDefines.Get (t);
				t = t.Replace ("#", "");

				Pos = ParseNumber (t);
			}

			if (!m_pLabels.ContainsKey ("." + label)) {
				m_pLabels.Add ("." + label, Pos);

			}
			Console.WriteLine ("[Label] {0}  {1}", label, Pos);
		}
		private bool CompLine(string line, BinaryWriter stream)
		{
			string[] l = line.Split (' ');
			Console.Write ("{0}: ", stream.BaseStream.Position);

			if (l.Length == 1)
				return CompOpp (OpID (l [0]), stream);
			else if (l.Length == 2 && l [1].Contains (",")) {
				string[] k = l [1].Split (','); // MOV 2
				if(k.Length == 2) return CompOpp ((OpID (l [0])), k [0], k [1], stream);
				if(k.Length == 3) return CompOpp ((OpID (l [0])), k [0], k [1], k [2], stream);
			} else if (l.Length == 2)
				return CompOpp ((OpID (l [0])), l [1], stream);

			return false;
		}
		private bool CompOpp(int op, BinaryWriter stream)
		{
			stream.Write (op.ToBytes());
			Console.WriteLine ("[Inst 1] {0} ({1}) ", op, stream.BaseStream.Position);
			return true;
		}
		private bool CompOpp(int op, string p1, BinaryWriter stream)
		{

			stream.Write (op.ToBytes (), 0, 4);
			Console.Write ("[Inst 1] {0} ({1}) ", op, stream.BaseStream.Position);
			if (m_pInstruction [op].OpCode == "JMP") {
				ComOppJmp (p1, stream);
			} else {
				ParamTest (p1, stream);
			}
			Console.WriteLine ();
			return true;
		}
		private void ComOppJmp(string p1, BinaryWriter stream)
		{
			int poss = 0; InstructionParam2 p = InstructionParam2.Value;
			if (p1 == "$") {
				p = InstructionParam2.No;
				stream.Write ((byte)InstructionParam2.No);
				poss = ((int)stream.BaseStream.Position - 4);
			} else if (p1.Contains (".")) {
				p = InstructionParam2.Lable;
				stream.Write ((byte)InstructionParam2.Lable);
				poss = ((int)getLabelPos(p1));
			}

			stream.Write (poss.ToBytes());
			Console.Write ("{1} {0}", poss, p.ToString());

		}
		private int getLabelPos(string name)
		{
			int pos = 0;
			if (m_pLabels.TryGetValue (name, out pos))
				return pos;
			else
				throw new Exception ("Address not found");
		}
		private bool CompOpp(int op, string p1, string p2, BinaryWriter stream)
		{
			stream.Write (op.ToBytes (), 0, 4);
			Console.Write ("[Inst 2] {0} ({1}) ", op, stream.BaseStream.Position);
			ParamTest (p1, stream);
			ParamTest (p2, stream);
			Console.WriteLine ();
			return true;
		}

		private bool CompOpp(int op, string p1, string p2, string p3, BinaryWriter stream)
		{
			stream.Write (op.ToBytes (), 0, 4);
			Console.Write ("[Inst 3] {0} ({1}) ", op, stream.BaseStream.Position);
			ParamTest (p1, stream);
			ParamTest (p2, stream);
			ParamTest (p3, stream);
			Console.WriteLine ();
			return true;
		}

		private int OpID(string text)
		{
			foreach (var item in m_pInstruction) {
				if (item.OpCode.ToUpper () == text.ToUpper ())
					return m_pInstruction.IndexOf (item);
			}
			throw new InstructionNotFoundException (text);
		}
		private void ParamTest(string p1, BinaryWriter stream, bool findLable = false)
		{
			bool isLable = false;

			p1 = m_pDefines.Get (p1);




			if (p1.Contains ("#")) {
				stream.Write ((byte)InstructionParam2.Value);
				Console.Write ("{0} ", InstructionParam2.Value);
				p1 = p1.Replace ("#", "");
			} else if (m_pRegisters.Contains (p1.ToUpper())) {
				stream.Write ((byte)InstructionParam2.Register);
				Console.Write ("{1} {0} ", m_pRegisters.IndexOf(p1), InstructionParam2.Register);
				stream.Write(m_pRegisters.IndexOf(p1).ToBytes(), 0, 4);
				return;
			} else if (p1.Contains ("@")) {
				stream.Write ((byte)InstructionParam2.Pointer);
				Console.Write ("{0} ", InstructionParam2.Pointer);
				p1 = p1.Replace ("@", "");
			} else if (p1.Contains (".")) {
				stream.Write ((byte)InstructionParam2.Lable);
				//p1 = p1.Replace (".", "");
				Console.Write ("{0} {1}", InstructionParam2.Lable, getLabelPos(p1));
				isLable = true;
			}
			if (!isLable) {
				int p = ParseNumber (p1);
				Console.Write ("{0} ", p);
				stream.Write (p.ToBytes (), 0, 4);
				return;
			} else {
				stream.Write(getLabelPos(p1));
				isLable = false;
			}
		}
		private int ParseNumber(string l1)
		{
			// 56d = decimal //
			// 56 = decimal //
			// 56h = HEX //
			// 56o = Octal

			// TODO: Cheak of Math Funktion and Parse hear
			Expression e = new Expression(l1);
			e.EvaluateParameter += delegate(string name, ParameterArgs args) {
				if (name.ToLower ().Contains ("h"))
					args.Result = ParseHex (name);
				else if (name.ToLower ().Contains ("b"))
					args.Result =    ParseBin (name);
				else
					args.Result =    ParseDec (name);

			};
			return (int)e.Evaluate ();
		}

		private int ParseBin(string i)
		{
			i = i.ToLower().Replace ("b", "");
			return Convert.ToInt32(i, 2);
		}
		private int ParseDec(string i)
		{
			i = i.ToLower().Replace ("d", "");
			return int.Parse (i, NumberStyles.None);
		}
		private int ParseHex(string i)
		{
			i = i.ToLower().Replace ("h", "");
			return int.Parse (i, NumberStyles.HexNumber);
		}
	}
}

