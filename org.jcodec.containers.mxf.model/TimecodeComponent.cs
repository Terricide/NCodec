using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class TimecodeComponent : MXFStructuralComponent
{
	private long start;

	private int @base;

	private int dropFrame;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 106 })]
	public TimecodeComponent(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 136, 120, 141, 141, 159, 11, 109,
		131, 109, 131, 110, 131, 127, 36, 134, 103, 102
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
			case 5377:
				start = _bb.getLong();
				break;
			case 5378:
				@base = _bb.getShort();
				break;
			case 5379:
				dropFrame = (sbyte)_bb.get();
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
	public virtual long getStart()
	{
		return start;
	}

	[LineNumberTable(58)]
	public virtual int getBase()
	{
		return @base;
	}

	[LineNumberTable(62)]
	public virtual int getDropFrame()
	{
		return dropFrame;
	}
}
