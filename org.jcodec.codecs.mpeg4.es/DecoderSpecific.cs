using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg4.es;

public class DecoderSpecific : Descriptor
{
	private ByteBuffer data;

	[LineNumberTable(27)]
	public static int tag()
	{
		return 5;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 111, 104 })]
	public DecoderSpecific(ByteBuffer data)
		: base(tag(), 0)
	{
		this.data = data;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 111 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		NIOUtils.write(@out, data);
	}

	[LineNumberTable(31)]
	public virtual ByteBuffer getData()
	{
		return data;
	}
}
