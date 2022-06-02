using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.codecs.mpeg4.es;

public class SL : Descriptor
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 111 })]
	public SL()
		: base(tag(), 0)
	{
	}

	[LineNumberTable(25)]
	public static int tag()
	{
		return 6;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.put(2);
	}
}
