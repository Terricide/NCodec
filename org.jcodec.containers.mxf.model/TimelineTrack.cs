using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;
using org.jcodec.common.model;

namespace org.jcodec.containers.mxf.model;

public class TimelineTrack : GenericTrack
{
	private Rational editRate;

	private long origin;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 106 })]
	public TimelineTrack(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 135, 98, 136, 120, 141, 109, 159, 11, 125,
		131, 109, 131, 127, 36, 134, 103, 102
	})]
	protected internal override void read(Map tags)
	{
		base.read(tags);
		Iterator it = tags.entrySet().iterator();
		while (it.hasNext())
		{
			Map.Entry entry = (Map.Entry)it.next();
			ByteBuffer _bb = (ByteBuffer)entry.getValue();
			switch (((Integer)entry.getKey()).intValue())
			{
			case 19201:
				Rational.___003Cclinit_003E();
				editRate = new Rational(_bb.getInt(), _bb.getInt());
				break;
			case 19202:
				origin = _bb.getLong();
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(51)]
	public virtual Rational getEditRate()
	{
		return editRate;
	}

	[LineNumberTable(55)]
	public virtual long getOrigin()
	{
		return origin;
	}
}
