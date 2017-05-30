//
//  Framebuffer.cs
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
using System.Drawing;

namespace Vcsos.Komponent
{
	public enum FrameBufferOrientation : byte
	{
		Portrait = 0,
		Landscape = 1,
	}
	public enum FrameBufferSize
	{
		VMFB_720x480x32 = 2, 
		VMFB_720x240x32 = 4,
		VMFB_360x480x32 = 6,

		VMFB_800x600x32 = 8,
		VMFB_1280x720x32 = 10
	}
	public struct FrameBufferInfo
	{
		public int physbase; // 4
		public byte BitsPerPixel; // 1
		public int Width,Height; // 8   .. 13
		public int Size;
		public FrameBufferOrientation Orientation;

		internal void WriteToRam(int position)
		{
			//0xAFF0
			int b = VM.Instance.Ram.Write(physbase.ToBytes(), position);
			b = VM.Instance.Ram.Write (new byte[] { BitsPerPixel }, b);
			b = VM.Instance.Ram.Write (Width.ToBytes (), b);
			b = VM.Instance.Ram.Write (Height.ToBytes (), b);
			b = VM.Instance.Ram.Write (Size.ToBytes (), b);
			b = VM.Instance.Ram.Write (new byte[] { (byte)Orientation }, b);
		}

		public FrameBufferInfo(int typ)
		{
			physbase = 0xB000;
			Orientation = ( typ % 2 == 0 ) ? FrameBufferOrientation.Landscape :
				FrameBufferOrientation.Portrait;
			typ = ( typ % 2 == 0 ) ? typ : typ - 1;

			switch ((FrameBufferSize)typ) {
			case FrameBufferSize.VMFB_360x480x32:
				Width = 360;
				Height = 480;
				break;
			case FrameBufferSize.VMFB_720x240x32:
				Width = 720;
				Height = 240;
				break;
			case FrameBufferSize.VMFB_800x600x32:
				Width = 800;
				Height = 600;
				break;
			case FrameBufferSize.VMFB_1280x720x32:
				Width = 1280;
				Height = 720;
				break;
			case FrameBufferSize.VMFB_720x480x32:
				Width = 720;
				Height = 480;
				break;
			default:
				Width = 360;
				Height = 480;
				break;
			}
			if (Orientation == FrameBufferOrientation.Portrait) {
				int w = Width;
				Width = Height;
				Height = w;
			}
			BitsPerPixel = 24;
			Size = Width * Height * (BitsPerPixel/8);
		}
		public override string ToString ()
		{
			return string.Format ("{0}x{1}-{2} {3} [0x{4:X4}:0x{5:X4}]", Width, Height, BitsPerPixel,
				Orientation, physbase, physbase+Size);
		}
	}
	public delegate void InitFrameBuffer(FrameBufferInfo buffer);

	public class Framebuffer : vmKomponente
	{
		public const uint FBINFO = 0xAFD0;
		public const uint FBBASE = 0xB000;

		private FrameBufferInfo m_pInfo;
		private InitFrameBuffer m_pInitFunction;

		private Memory m_pMemory;

		public InitFrameBuffer InitFunction {
			get { return m_pInitFunction; }
			set { m_pInitFunction = value; }
		}

		public Size Size {
			get {
				return new Size(m_pInfo.Width, m_pInfo.Height);
			}
		}

		public Memory Memory {
			get {
				return m_pMemory;
			}
		}
			
		public Framebuffer () : base("Referenz GPU", "Anna-Sophia Schroeck")
		{
		}
		// ASM FBI // FrameBuffer Init
		public void Init()
		{
			int colorRef = VM.Instance.CurrentCore.Register.Stack.Pop32 ();
			int mode = VM.Instance.CurrentCore.Register.Stack.Pop32 ();

			m_pInfo = new FrameBufferInfo (mode);// = new Size (w, h);
			m_pMemory = new Memory(m_pInfo.Size, "FrameBuffer");            

            for (int x = 0; x < m_pInfo.Width; x++) {
				for (int y = 0; y < m_pInfo.Height; y++) {
					SetPixel (colorRef, x, y);
				}
			}


			if (m_pInitFunction != null)
				m_pInitFunction (m_pInfo);
		}
		// ASM: FBD // FrameBuffer Destroy
		public void Destroy()
		{
			GC.Collect ();
		}
		/// <summary>
		/// Sets the pixel.
		/// </summary>
		/// <param name="colorRef">Color RGB String</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void SetPixel(int colorRef, int x, int y)
		{
			int offset = (int)(FBBASE + (y * m_pInfo.Width * 3 + x * 3));

			MemoryMap.Write((byte)((colorRef & 0xFF0000) >> 16), offset + 0);
			MemoryMap.Write ((byte)((colorRef & 0x00FF00) >> 8), offset + 1);
			MemoryMap.Write ((byte)((colorRef & 0x0000FF)), offset + 2);
		}
		public int GetPixel(int x, int y)
		{
			int offset = (int)(FBBASE + (y * m_pInfo.Width * 3 + x * 3));

			byte r = MemoryMap.Read8 (offset + 0);
			byte g = MemoryMap.Read8 (offset + 1);
			byte b = MemoryMap.Read8 (offset + 2);

			return ((r & 0xff) << 16) | ((g & 0xff) << 8) | (b & 0xff);
		}
	}
}

