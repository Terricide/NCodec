using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class SourceClip : MXFStructuralComponent
{
	private long startPosition;

	private int sourceTrackId;

	private UL sourcePackageUid;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 106 })]
	public SourceClip(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 136, 120, 141, 141, 159, 22, 109,
		131, 109, 131, 109, 131, 127, 36, 134, 103, 102
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
			case 4609:
				startPosition = _bb.getLong();
				break;
			case 4353:
				sourcePackageUid = UL.read(_bb);
				break;
			case 4354:
				sourceTrackId = _bb.getInt();
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(54)]
	public virtual UL getSourcePackageUid()
	{
		return sourcePackageUid;
	}

	[LineNumberTable(58)]
	public virtual long getStartPosition()
	{
		return startPosition;
	}

	[LineNumberTable(62)]
	public virtual int getSourceTrackId()
	{
		return sourceTrackId;
	}
}
