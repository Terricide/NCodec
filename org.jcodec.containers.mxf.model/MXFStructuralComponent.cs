using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class MXFStructuralComponent : MXFInterchangeObject
{
	private long duration;

	private UL dataDefinitionUL;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 106 })]
	public MXFStructuralComponent(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 135, 98, 120, 141, 159, 11, 119, 131, 119,
		131, 127, 36, 134, 103, 102
	})]
	protected internal override void read(Map tags)
	{
		Iterator it = tags.entrySet().iterator();
		while (it.hasNext())
		{
			Map.Entry entry = (Map.Entry)it.next();
			switch (((Integer)entry.getKey()).intValue())
			{
			case 514:
				duration = ((ByteBuffer)entry.getValue()).getLong();
				break;
			case 513:
				dataDefinitionUL = UL.read((ByteBuffer)entry.getValue());
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(48)]
	public virtual long getDuration()
	{
		return duration;
	}

	[LineNumberTable(52)]
	public virtual UL getDataDefinitionUL()
	{
		return dataDefinitionUL;
	}
}
