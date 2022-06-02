using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.api;
using org.jcodec.common.io;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.io.write;

public class CAVLCWriter : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 99, 99, 104, 108, 99, 131, 234,
		59, 231, 71, 105, 104, 109
	})]
	public static void writeUE(BitWriter @out, int value)
	{
		int bits = 0;
		int cumul = 0;
		for (int i = 0; i < 15; i++)
		{
			if (value < cumul + (1 << i))
			{
				bits = i;
				break;
			}
			cumul += 1 << i;
		}
		@out.writeNBit(0, bits);
		@out.write1Bit(1);
		@out.writeNBit(value - cumul, bits);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 162, 111 })]
	public static void writeSE(BitWriter @out, int value)
	{
		writeUE(@out, MathUtil.golomb(value));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 98, 101, 138, 110 })]
	public static void writeTE(BitWriter @out, int value, int max)
	{
		if (max > 1)
		{
			writeUE(@out, value);
		}
		else
		{
			@out.write1Bit((value ^ -1) & 1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 162, 104, 123 })]
	public static void writeUEtrace(BitWriter @out, int value, string message)
	{
		writeUE(@out, value);
		Debug.trace(message, Integer.valueOf(value));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 65, 67, 110, 127, 2 })]
	public static void writeBool(BitWriter @out, bool value, string message)
	{
		@out.write1Bit(value ? 1 : 0);
		Debug.trace(message, Integer.valueOf(value ? 1 : 0));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 98, 107 })]
	public static void writeU(BitWriter @out, int i, int n)
	{
		@out.writeNBit(i, n);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 98, 103, 52, 167, 123 })]
	public static void writeNBit(BitWriter @out, long value, int n, string message)
	{
		for (int i = 0; i < n; i++)
		{
			@out.write1Bit((int)(value >> n - i - 1) & 1);
		}
		Debug.trace(message, Long.valueOf(value));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 109, 123 })]
	public static void writeSEtrace(BitWriter @out, int value, string message)
	{
		writeUE(@out, MathUtil.golomb(value));
		Debug.trace(message, Integer.valueOf(value));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 66, 104, 105 })]
	public static void writeTrailingBits(BitWriter @out)
	{
		@out.write1Bit(1);
		@out.flush();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105 })]
	private CAVLCWriter()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 105, 123 })]
	public static void writeUtrace(BitWriter @out, int value, int n, string message)
	{
		@out.writeNBit(value, n);
		Debug.trace(message, Integer.valueOf(value));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(85)]
	public static void writeSliceTrailingBits()
	{
		
		throw new NotImplementedException("todo");
	}
}
