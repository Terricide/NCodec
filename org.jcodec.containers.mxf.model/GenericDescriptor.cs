using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class GenericDescriptor : MXFInterchangeObject
{
	private UL[] locators;

	private UL[] subDescriptors;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 106 })]
	public GenericDescriptor(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 136, 162, 120, 141, 141, 159, 11, 109, 131,
		109, 131, 127, 36, 134, 103, 102
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
			case 12033:
				locators = MXFMetadata.readULBatch(_bb);
				break;
			case 16129:
				subDescriptors = MXFMetadata.readULBatch(_bb);
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
	public virtual UL[] getLocators()
	{
		return locators;
	}

	[LineNumberTable(52)]
	public virtual UL[] getSubDescriptors()
	{
		return subDescriptors;
	}
}
