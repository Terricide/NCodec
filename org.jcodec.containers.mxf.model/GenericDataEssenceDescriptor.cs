using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class GenericDataEssenceDescriptor : FileDescriptor
{
	private UL dataEssenceCoding;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 106 })]
	public GenericDataEssenceDescriptor(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 136, 162, 136, 120, 141, 141, 159, 0, 109,
		131, 127, 36, 134, 103, 102
	})]
	protected internal override void read(Map tags)
	{
		base.read(tags);
		Iterator it = tags.entrySet().iterator();
		while (it.hasNext())
		{
			Map.Entry entry = (Map.Entry)it.next();
			ByteBuffer _bb = (ByteBuffer)entry.getValue();
			if (((Integer)entry.getKey()).intValue() == 15873)
			{
				dataEssenceCoding = UL.read(_bb);
				it.remove();
			}
			else
			{
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ FileDescriptor: ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
			}
		}
	}

	[LineNumberTable(47)]
	public virtual UL getDataEssenceCoding()
	{
		return dataEssenceCoding;
	}
}
