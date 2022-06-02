using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg4.es;

public abstract class Descriptor : Object
{
	private int _tag;

	private int size;

	protected internal abstract void doWrite(ByteBuffer bb);

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105, 104, 104 })]
	public Descriptor(int tag, int size)
	{
		_tag = tag;
		this.size = size;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 104, 105, 136, 113, 111, 106 })]
	public virtual void write(ByteBuffer @out)
	{
		ByteBuffer fork = @out.duplicate();
		NIOUtils.skip(@out, 5);
		doWrite(@out);
		int length = @out.position() - fork.position() - 5;
		fork.put((byte)(sbyte)_tag);
		JCodecUtil2.writeBER32(fork, length);
	}

	[LineNumberTable(37)]
	internal virtual int getTag()
	{
		return _tag;
	}
}
