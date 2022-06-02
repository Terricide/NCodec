using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.codecs.mjpeg;

public class FrameHeader : Object
{
	public class Component : Object
	{
		internal int index;

		internal int subH;

		internal int subV;

		internal int quantTable;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(53)]
		public Component()
		{
		}
	}

	internal int headerLength;

	internal int bitsPerSample;

	internal int height;

	internal int width;

	internal int nComp;

	internal Component[] components;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(15)]
	public FrameHeader()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 66, 99, 109, 106, 14, 199 })]
	public virtual int getHmax()
	{
		int max = 0;
		for (int i = 0; i < (nint)components.LongLength; i++)
		{
			Component c = components[i];
			max = Math.max(max, c.subH);
		}
		return max;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 98, 99, 109, 106, 14, 199 })]
	public virtual int getVmax()
	{
		int max = 0;
		for (int i = 0; i < (nint)components.LongLength; i++)
		{
			Component c = components[i];
			max = Math.max(max, c.subV);
		}
		return max;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 66, 103, 115, 116, 115, 115, 116, 114,
		112, 121, 117, 112, 114, 109, 245, 58, 234, 72
	})]
	public static FrameHeader read(ByteBuffer @is)
	{
		FrameHeader frame = new FrameHeader();
		frame.headerLength = @is.getShort() & 0xFFFF;
		frame.bitsPerSample = (sbyte)@is.get() & 0xFF;
		frame.height = @is.getShort() & 0xFFFF;
		frame.width = @is.getShort() & 0xFFFF;
		frame.nComp = (sbyte)@is.get() & 0xFF;
		frame.components = new Component[frame.nComp];
		for (int i = 0; i < (nint)frame.components.LongLength; i++)
		{
			Component[] array = frame.components;
			int num = i;
			Component component = new Component();
			int num2 = num;
			Component[] array2 = array;
			array2[num2] = component;
			Component c = component;
			c.index = (sbyte)@is.get() & 0xFF;
			int hv = (sbyte)@is.get() & 0xFF;
			c.subH = (int)((uint)(hv & 0xF0) >> 4);
			c.subV = hv & 0xF;
			c.quantTable = (sbyte)@is.get() & 0xFF;
		}
		return frame;
	}
}
