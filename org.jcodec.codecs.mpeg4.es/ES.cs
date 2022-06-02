using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using java.util;

namespace org.jcodec.codecs.mpeg4.es;

public class ES : NodeDescriptor
{
	private int trackId;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 111, 104 })]
	public ES(int trackId, Collection children)
		: base(tag(), children)
	{
		this.trackId = trackId;
	}

	[LineNumberTable(21)]
	public static int tag()
	{
		return 3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 111, 105, 106 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putShort((short)trackId);
		@out.put(0);
		base.doWrite(@out);
	}

	[LineNumberTable(31)]
	public virtual int getTrackId()
	{
		return trackId;
	}
}
