using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class EssenceContainerData : MXFInterchangeObject
{
	private UL linkedPackageUID;

	private int indexSID;

	private int bodySID;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 106 })]
	public EssenceContainerData(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 135, 98, 120, 141, 109, 159, 22, 109, 131,
		109, 131, 109, 131, 127, 36, 134, 103, 102
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
			case 9985:
				linkedPackageUID = UL.read(_bb);
				break;
			case 16134:
				indexSID = _bb.getInt();
				break;
			case 16135:
				bodySID = _bb.getInt();
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ EssenceContainerData: ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(52)]
	public virtual UL getLinkedPackageUID()
	{
		return linkedPackageUID;
	}

	[LineNumberTable(56)]
	public virtual int getIndexSID()
	{
		return indexSID;
	}

	[LineNumberTable(60)]
	public virtual int getBodySID()
	{
		return bodySID;
	}
}
