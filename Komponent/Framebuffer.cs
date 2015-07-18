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
		internal void WriteToRam()
		{
			//0xAFF0
			int b = VM.Instance.Ram.Write(physbase.ToBytes(), 0xAFD0);
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
			BitsPerPixel = 32;
			Size = Width * Height;
		}
		public override string ToString ()
		{
			return string.Format ("{0}x{1}:{2} {3} [{4}:{5}]", Width, Height, BitsPerPixel,
			Orientation, physbase, Size);
		}
	}

	public delegate void UpdateBuffer(Framebuffer Memory); 
	public delegate void InitFrameBuffer(FrameBufferInfo buffer);

	public class Framebuffer
	{
		public const uint FBINFO = 0xAFD0;
		public const uint FBBASE = 0xB000;

		private FrameBufferInfo m_pInfo;
		private UpdateBuffer m_pUpdateFunction;
		private InitFrameBuffer m_pInitFunction;

		private int[] m_pMemory;

		public UpdateBuffer UpdateFunction {
			get { return m_pUpdateFunction; }
			set { m_pUpdateFunction = value; }
		}

		public InitFrameBuffer InitFunction {
			get { return m_pInitFunction; }
			set { m_pInitFunction = value; }
		}

		public Size Size {
			get {
				return new Size(m_pInfo.Width, m_pInfo.Height);
			}
		}

		public int[] Memory {
			get {
				return m_pMemory;
			}
		}
			
		public Framebuffer ()
		{
		}
		// ASM FBI // FrameBuffer Init
		public void Init()
		{
			int colorRef = VM.Instance.CPU.L2.Stack.Pop32 ();
			int mode = VM.Instance.CPU.L2.Stack.Pop32 ();

			m_pInfo = new FrameBufferInfo (mode);// = new Size (w, h);
			m_pInfo.WriteToRam();

			m_pMemory = new int[(int)(m_pInfo.Size)];// "FrameBuffer");

			if (m_pInitFunction != null)
				m_pInitFunction (m_pInfo);
			
			for (int i = 0; i < m_pInfo.Size; i++ ) {
				m_pMemory [i] = colorRef;
			}

			UpdateBuffer ();
				
		}
		// ASM: FBD // FrameBuffer Destroy
		public void Destroy()
		{
			m_pMemory = null;
			GC.Collect ();
		}
		public void SetPixel(int colorRef, int x, int y)
		{
			/*int i = x * y;
			MemoryMap.Write((byte)(colorRef & 0xff), (int)(FBBASE + i));
			MemoryMap.Write((byte)((colorRef >> 8) & 0xff), (int)(FBBASE + ++i));
			MemoryMap.Write((byte)((colorRef >> 16) & 0xff), (int)(FBBASE + ++i));
*/
			m_pMemory [x * y] = colorRef;
			UpdateBuffer ();
		}
		internal void UpdateBuffer()
		{
			if (m_pUpdateFunction != null)
				m_pUpdateFunction (this);
		}
	}
}

