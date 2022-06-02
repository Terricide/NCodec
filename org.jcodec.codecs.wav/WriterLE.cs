using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;

namespace org.jcodec.codecs.wav;

public abstract class WriterLE : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public WriterLE()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 139, 129, 67, 110, 114 })]
	public static void writeShort(OutputStream @out, short s)
	{
		@out.write(s & 0xFF);
		@out.write((s >> 8) & 0xFF);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 138, 162, 110, 112, 113, 115 })]
	public static void writeInt(OutputStream @out, int i)
	{
		@out.write(i & 0xFF);
		@out.write((i >> 8) & 0xFF);
		@out.write((i >> 16) & 0xFF);
		@out.write((i >> 24) & 0xFF);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 136, 130, 112, 114, 115, 115, 115, 115, 115,
		117
	})]
	public static void writeLong(OutputStream @out, long l)
	{
		@out.write((int)(l & 0xFFu));
		@out.write((int)((l >> 8) & 0xFFu));
		@out.write((int)((l >> 16) & 0xFFu));
		@out.write((int)((l >> 24) & 0xFFu));
		@out.write((int)((l >> 32) & 0xFFu));
		@out.write((int)((l >> 40) & 0xFFu));
		@out.write((int)((l >> 48) & 0xFFu));
		@out.write((int)((l >> 56) & 0xFFu));
	}
}
