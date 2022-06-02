using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using java.util;

namespace org.jcodec.codecs.mpeg4.es;

public class DecoderConfig : NodeDescriptor
{
	private int objectType;

	private int bufSize;

	private int maxBitrate;

	private int avgBitrate;

	[LineNumberTable(39)]
	public static int tag()
	{
		return 4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 112, 104, 104, 104, 105 })]
	public DecoderConfig(int objectType, int bufSize, int maxBitrate, int avgBitrate, Collection children)
		: base(tag(), children)
	{
		this.objectType = objectType;
		this.bufSize = bufSize;
		this.maxBitrate = maxBitrate;
		this.avgBitrate = avgBitrate;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 143, 106, 114, 111, 110, 142, 106 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.put((byte)(sbyte)objectType);
		@out.put(21);
		@out.put((byte)(sbyte)(bufSize >> 16));
		@out.putShort((short)bufSize);
		@out.putInt(maxBitrate);
		@out.putInt(avgBitrate);
		base.doWrite(@out);
	}

	[LineNumberTable(43)]
	public virtual int getObjectType()
	{
		return objectType;
	}

	[LineNumberTable(47)]
	public virtual int getBufSize()
	{
		return bufSize;
	}

	[LineNumberTable(51)]
	public virtual int getMaxBitrate()
	{
		return maxBitrate;
	}

	[LineNumberTable(55)]
	public virtual int getAvgBitrate()
	{
		return avgBitrate;
	}
}
