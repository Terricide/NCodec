using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;

namespace org.jcodec.platform;

public abstract class BaseInputStream : InputStream
{
	[Throws(new string[] { "java.io.IOException" })]
	protected internal abstract int readBuffer(byte[] barr, int i1, int i2);

	[Throws(new string[] { "java.io.IOException" })]
	protected internal abstract int readByte();

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(6)]
	public BaseInputStream()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(14)]
	public override int read(byte[] b)
	{
		int result = readBuffer(b, 0, b.Length);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(19)]
	public override int read(byte[] b, int off, int len)
	{
		int result = readBuffer(b, off, len);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(24)]
	public override int read()
	{
		int result = readByte();
		
		return result;
	}
}
