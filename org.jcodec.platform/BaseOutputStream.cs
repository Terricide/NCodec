using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;

namespace org.jcodec.platform;

public abstract class BaseOutputStream : OutputStream
{
	[Throws(new string[] { "java.io.IOException" })]
	protected internal abstract void writeByte(int i);

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(6)]
	public BaseOutputStream()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 139, 66, 106 })]
	public override void write(int b)
	{
		writeByte(b);
	}
}
