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
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Vcsos;
using Vcsos.Komponent;
using OpenTK.Graphics;


namespace vmcli
{
    public class FramebufferForm : GameWindow
	{
		private FrameBufferInfo m_pInfo;
		//private Memory m_ppbuffer;
		//private bool   m_bReDraw;

		public FramebufferForm () : base(320,320, GraphicsMode.Default, 
			"", GameWindowFlags.Default, DisplayDevice.Default, 2,0,
			OpenTK.Graphics.GraphicsContextFlags.Default)
		{
			//VM.Instance.FBdev.UpdateFunction = UpdateBuffer;
			VM.Instance.FBdev.InitFunction = InitFrameBuffer;

			string versionOpenGL = GL.GetString(StringName.Version);

			Console.WriteLine ("vmGPU on OpenGL {0}", versionOpenGL);
		}
		protected override void OnRenderFrame( FrameEventArgs e )
		{
			GL.Clear( ClearBufferMask.ColorBufferBit );
			View ();
			Draw();

			SwapBuffers();
		}
		private void View()
		{
            this.ClientSize = new Size(m_pInfo.Width, m_pInfo.Height);
            
			GL.Viewport (0, 0, m_pInfo.Width, m_pInfo.Height);
			GL.MatrixMode (MatrixMode.Projection);
			GL.LoadIdentity ();
			GL.Ortho (0, m_pInfo.Width, 0, m_pInfo.Height, -1, 1);

			GL.MatrixMode (MatrixMode.Modelview);
			GL.LoadIdentity ();
		}
		private void Draw()
		{

			{
				GL.Begin (PrimitiveType.Points);

				for (int x = 0; x < m_pInfo.Width; x++) {
					for (int y = 0; y < m_pInfo.Height; y++) {
						var c = GetPixel (x, y);
						GL.Color3 (Color.FromArgb (c));
						GL.Vertex2 (x, y);
					}
				}
				GL.End ();
			}
		}
		private int GetPixel(int x, int y)
		{
			//int offset = y * m_pInfo.Width * 3 + x * 3;

			/*byte r = m_ppbuffer [offset + 0];
			byte g = m_ppbuffer [offset + 1];
			byte b = m_ppbuffer [offset + 2];

			return ((r & 0xff) << 16) | ((g & 0xff) << 8) | (b & 0xff);*/

			int offset = (int)(Framebuffer.FBBASE + (y * m_pInfo.Width * 3 + x * 3));

			byte r = MemoryMap.Read8 (offset + 0);
			byte g = MemoryMap.Read8 (offset + 1);
			byte b = MemoryMap.Read8 (offset + 2);

			return ((r & 0xff) << 16) | ((g & 0xff) << 8) | (b & 0xff);
		}
		/*private void UpdateBuffer(Framebuffer FB)
		{
			//m_ppbuffer = FB.Memory;
			//m_bReDraw = true;
		}*/
		private void InitFrameBuffer(FrameBufferInfo buffer)
		{
			ClientSize = new Size (buffer.Width, buffer.Height);
			m_pInfo = buffer;

			Title = buffer.ToString ();
		}
	}
}

