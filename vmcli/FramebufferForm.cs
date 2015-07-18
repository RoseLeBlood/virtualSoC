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
using OpenTK.Graphics.OpenGL;


namespace vmcli
{
	public class FramebufferForm : Form
	{
		private Bitmap buffer;
		private OpenTK.GLControl openGLControl1;
		private int[] m_pBuffer;

		public FramebufferForm ()
		{
			openGLControl1 = new OpenTK.GLControl (OpenTK.Graphics.GraphicsMode.Default,
				2,0, OpenTK.Graphics.GraphicsContextFlags.Default);
	
			openGLControl1.Dock = DockStyle.Fill;
			openGLControl1.Paint += glControl1_Paint;
			openGLControl1.BackColor = System.Drawing.SystemColors.ControlDark;
			openGLControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			openGLControl1.Location = new System.Drawing.Point(0, 0);
			openGLControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			openGLControl1.Name = "glControl1";
			openGLControl1.Size = new System.Drawing.Size(320, 320);
			openGLControl1.Resize += OpenGLControl1_Resize;
			openGLControl1.VSync = false;

			this.Controls.Add (openGLControl1);

			VM.Instance.FBdev.UpdateFunction = UpdateBuffer;
			VM.Instance.FBdev.InitFunction = InitFrameBuffer;

			Text = "vmcpu Framebuffer";
			Size = new Size(320,320);
			ResizeRedraw = true;

			this.Resize += FrameBuffer_Resize;

			FrameBuffer_Resize(this, null);


		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			OpenGLControl1_Resize(this, EventArgs.Empty);   // Ensure the Viewport is set up correctly
			GL.ClearColor(Color.Crimson);
		}
		void OpenGLControl1_Resize (object sender, EventArgs e)
		{
			if (openGLControl1.ClientSize.Height == 0)
				openGLControl1.ClientSize = new System.Drawing.Size(openGLControl1.ClientSize.Width, 1);

			GL.Viewport(0, 0, openGLControl1.ClientSize.Width, openGLControl1.ClientSize.Height);
		}
		bool draw = false;

		private void glControl1_Paint(object sender, PaintEventArgs e)
		{
			if (!draw)
				return;
			draw = false;

			GL.MatrixMode (MatrixMode.Projection);
			GL.LoadIdentity ();

			GL.Ortho (0.0, Width, 0.0, Height, -1, 1);
			GL.MatrixMode (MatrixMode.Modelview);

			GL.ClearColor(Color.SkyBlue);

			openGLControl1.MakeCurrent();

			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Point);
			GL.Begin (PrimitiveType.Points);

			for (int x = 0; x < Size.Width; x++) {
				for (int y = 0; y < Size.Height; y++) {
					var c = Color.FromArgb (m_pBuffer [x * y]);

					GL.Color3 (c);
					GL.PointSize (1.0f);
					GL.Vertex2 (x, y);

				}
			}

			GL.End ();

			openGLControl1.SwapBuffers();


		}

		private void FrameBuffer_Resize(object sender, EventArgs e)
		{
			openGLControl1.Size = new System.Drawing.Size(Width, Height);
		}
		private void UpdateBuffer(Framebuffer FB)
		{
			m_pBuffer = FB.Memory;
			draw = true;
		}
		private void InitFrameBuffer(FrameBufferInfo buffer)
		{
			Size = new Size (buffer.Width, buffer.Height);
			FrameBuffer_Resize(this, null);
			Text = buffer.ToString ();
		}
	}
}

