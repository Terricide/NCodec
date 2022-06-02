using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class Sequence : MXFStructuralComponent
{
	private UL[] structuralComponentsRefs;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 106 })]
	public Sequence(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 136, 130, 136, 120, 141, 159, 0, 119, 131,
		127, 36, 134, 103, 102
	})]
	protected internal override void read(Map tags)
	{
		base.read(tags);
		Iterator it = tags.entrySet().iterator();
		while (it.hasNext())
		{
			Map.Entry entry = (Map.Entry)it.next();
			if (((Integer)entry.getKey()).intValue() == 4097)
			{
				structuralComponentsRefs = MXFMetadata.readULBatch((ByteBuffer)entry.getValue());
				it.remove();
			}
			else
			{
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
			}
		}
	}

	[LineNumberTable(44)]
	public virtual UL[] getStructuralComponentsRefs()
	{
		return structuralComponentsRefs;
	}
}
