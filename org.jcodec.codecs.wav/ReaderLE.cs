using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;

namespace org.jcodec.codecs.wav;

public abstract class ReaderLE : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(13)]
	public ReaderLE()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 139, 162, 104, 136, 105, 131 })]
	public static short readShort(InputStream input)
	{
		int b2 = input.read();
		int b1 = input.read();
		if (b1 == -1 || b2 == -1)
		{
			return -1;
		}
		return (short)((b1 << 8) + b2);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 136, 98, 105, 105, 105, 137, 117, 131 })]
	public static int readInt(InputStream input)
	{
		long b4 = input.read();
		long b3 = input.read();
		long b2 = input.read();
		long b1 = input.read();
		if (b1 == -1 || b2 == -1 || b3 == -1 || b4 == -1)
		{
			return -1;
		}
		return (int)((b1 << 24) + (b2 << 16) + (b3 << 8) + b4);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 105, 105, 105, 105, 106, 106, 106,
		138, 127, 14, 132
	})]
	public static long readLong(InputStream input)
	{
		long b8 = input.read();
		long b7 = input.read();
		long b6 = input.read();
		long b5 = input.read();
		long b4 = input.read();
		long b3 = input.read();
		long b2 = input.read();
		long b1 = input.read();
		if (b1 == -1 || b2 == -1 || b3 == -1 || b4 == -1 || b5 == -1 || b6 == -1 || b7 == -1 || b8 == -1)
		{
			return -1L;
		}
		return (int)((b1 << 56) + (b2 << 48) + (b3 << 40) + (b4 << 32) + (b5 << 24) + (b6 << 16) + (b7 << 8) + b8);
	}
}
