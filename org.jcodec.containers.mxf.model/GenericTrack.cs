using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class GenericTrack : MXFInterchangeObject
{
	private int trackId;

	private string name;

	private UL sequenceRef;

	private int trackNumber;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 106 })]
	public GenericTrack(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 135, 98, 120, 141, 109, 159, 15, 109, 134,
		110, 131, 109, 131, 109, 131, 127, 36, 134, 103,
		102
	})]
	protected internal override void read(Map tags)
	{
		Iterator it = tags.entrySet().iterator();
		while (it.hasNext())
		{
			Map.Entry entry = (Map.Entry)it.next();
			ByteBuffer _bb = (ByteBuffer)entry.getValue();
			switch (((Integer)entry.getKey()).intValue())
			{
			case 18433:
				trackId = _bb.getInt();
				break;
			case 18434:
				name = readUtf16String(_bb);
				break;
			case 18435:
				sequenceRef = UL.read(_bb);
				break;
			case 18436:
				trackNumber = _bb.getInt();
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(55)]
	public virtual int getTrackId()
	{
		return trackId;
	}

	[LineNumberTable(59)]
	public virtual string getName()
	{
		return name;
	}

	[LineNumberTable(63)]
	public virtual UL getSequenceRef()
	{
		return sequenceRef;
	}

	[LineNumberTable(67)]
	public virtual int getTrackNumber()
	{
		return trackNumber;
	}
}
