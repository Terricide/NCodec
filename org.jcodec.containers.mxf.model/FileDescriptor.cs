using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;
using org.jcodec.common.model;

namespace org.jcodec.containers.mxf.model;

public class FileDescriptor : GenericDescriptor
{
	private int linkedTrackId;

	private Rational sampleRate;

	private long containerDuration;

	private UL essenceContainer;

	private UL codec;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 106 })]
	public FileDescriptor(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 134, 66, 136, 120, 141, 141, 159, 26, 109,
		134, 125, 134, 109, 131, 109, 131, 109, 131, 127,
		36, 134, 103, 102
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
			case 12294:
				linkedTrackId = _bb.getInt();
				break;
			case 12289:
				Rational.___003Cclinit_003E();
				sampleRate = new Rational(_bb.getInt(), _bb.getInt());
				break;
			case 12290:
				containerDuration = _bb.getLong();
				break;
			case 12292:
				essenceContainer = UL.read(_bb);
				break;
			case 12293:
				codec = UL.read(_bb);
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(64)]
	public virtual int getLinkedTrackId()
	{
		return linkedTrackId;
	}

	[LineNumberTable(68)]
	public virtual Rational getSampleRate()
	{
		return sampleRate;
	}

	[LineNumberTable(72)]
	public virtual long getContainerDuration()
	{
		return containerDuration;
	}

	[LineNumberTable(76)]
	public virtual UL getEssenceContainer()
	{
		return essenceContainer;
	}

	[LineNumberTable(80)]
	public virtual UL getCodec()
	{
		return codec;
	}
}
