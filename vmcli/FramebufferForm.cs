//
//  FramebufferForm.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Vcsos.Komponent;
using Vcsos;
using DotArgs;

namespace vmcli
{
	public class FramebufferForm : Form
	{
		Framebuffer fb ;

		public FramebufferForm ()
		{
			fb = VM.Instance.FBdev;
			Text = "vmcpu Framebuffer";
			Size = fb.Size;
			ResizeRedraw = true;

			Paint += new PaintEventHandler(OnPaint);
			CenterToScreen();

		}
		void OnPaint(object sender, PaintEventArgs e)
		{ 
			System.Drawing.Graphics g = e.Graphics;
			g.Clear (Color.Crimson);



			g.Dispose ();
		}
	}
}

