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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using DotArgs;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Vcsos;
using Vcsos.Komponent;


namespace vmcli
{
	public class FramebufferForm : GameWindow
	{
		private FrameBufferInfo m_pInfo;
		private int[] m_ppbuffer;

		public FramebufferForm () : base(320,320, OpenTK.Graphics.GraphicsMode.Default,
			"", GameWindowFlags.FixedWindow, DisplayDevice.Default, 1,1,
			OpenTK.Graphics.GraphicsContextFlags.Embedded)
		{
			VM.Instance.FBdev.UpdateFunction = UpdateBuffer;
			VM.Instance.FBdev.InitFunction = InitFrameBuffer;
		}
		protected override void OnRenderFrame( FrameEventArgs e )
		{
			GL.Clear( ClearBufferMask.ColorBufferBit );

			DrawImage(0);

			SwapBuffers();
		}
		private void DrawImage(int image)
		{
			if (m_ppbuffer != null) {
				GL.Viewport (0, 0, m_pInfo.Width, m_pInfo.Height);
				GL.MatrixMode (MatrixMode.Projection);
				GL.LoadIdentity ();
				GL.Ortho (0, m_pInfo.Width, 0, m_pInfo.Height, -1, 1);

				GL.MatrixMode (MatrixMode.Modelview);
				GL.LoadIdentity ();


				GL.Begin (PrimitiveType.Points);

				for (int x = 0; x < m_pInfo.Width; x++) {
					for (int y = 0; y < m_pInfo.Height; y++) {
						GL.Color3 (Color.FromArgb (m_ppbuffer [x * y]));
						GL.Vertex2 (x, y);
					}
				}
				GL.End ();
			}
		}
		private void UpdateBuffer(Framebuffer FB)
		{
			m_ppbuffer = FB.Memory;
		}
		private void InitFrameBuffer(FrameBufferInfo buffer)
		{
			Size = new Size (buffer.Width, buffer.Height);
			m_pInfo = buffer;
			Title = buffer.ToString ();
		}
	}
}

