using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.tools;
using org.jcodec.platform;

namespace org.jcodec.common;

public class JCodecUtil2 : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 140, 130, 115, 115, 114, 109 })]
	public static void writeBER32(ByteBuffer buffer, int value)
	{
		buffer.put((byte)(sbyte)((uint)(value >> 21) | 0x80u));
		buffer.put((byte)(sbyte)((uint)(value >> 14) | 0x80u));
		buffer.put((byte)(sbyte)((uint)(value >> 7) | 0x80u));
		buffer.put((byte)(sbyte)((uint)value & 0x7Fu));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 138, 98, 99, 103, 105, 106, 108, 227, 60,
		231, 70
	})]
	public static int readBER32(ByteBuffer input)
	{
		int size = 0;
		for (int i = 0; i < 4; i++)
		{
			int b = (sbyte)input.get();
			size = (size << 7) | (b & 0x7F);
			if ((b & 0xFF) >> 7 == 0)
			{
				break;
			}
		}
		return size;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(38)]
	public static byte[] asciiString(string fourcc)
	{
		byte[] bytes = Platform.getBytes(fourcc);
		
		return bytes;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(8)]
	public JCodecUtil2()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 114, 102, 104, 101, 105, 234, 59,
		231, 71
	})]
	public static void writeBER32Var(ByteBuffer bb, int value)
	{
		int i = 0;
		int bits = MathUtil.log2(value);
		for (; i < 4; i++)
		{
			if (bits <= 0)
			{
				break;
			}
			bits += -7;
			int @out = value >> bits;
			if (bits > 0)
			{
				@out |= 0x80;
			}
			bb.put((byte)(sbyte)@out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 130, 104, 104, 105, 104, 39, 167 })]
	public static int[] getAsIntArray(ByteBuffer yuv, int size)
	{
		byte[] b = new byte[size];
		int[] result = new int[size];
		yuv.get(b);
		for (int i = 0; i < (nint)b.LongLength; i++)
		{
			result[i] = b[i];
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 100, 99 })]
	public static string removeExtension(string name)
	{
		if (name == null)
		{
			return null;
		}
		string result = String.instancehelper_replaceAll(name, "\\.[^\\.]+$", "");
		
		return result;
	}
}
