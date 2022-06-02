using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class Preface : MXFInterchangeObject
{
	private Date lastModifiedDate;

	private int objectModelVersion;

	private UL op;

	private UL[] essenceContainers;

	private UL[] dmSchemes;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 106 })]
	public Preface(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 134, 66, 120, 109, 109, 159, 39, 109, 134,
		109, 134, 109, 131, 109, 131, 109, 131, 127, 36,
		134, 103, 102
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
			case 15106:
				lastModifiedDate = MXFMetadata.readDate(_bb);
				break;
			case 15111:
				objectModelVersion = _bb.getInt();
				break;
			case 15113:
				op = UL.read(_bb);
				break;
			case 15114:
				essenceContainers = MXFMetadata.readULBatch(_bb);
				break;
			case 15115:
				dmSchemes = MXFMetadata.readULBatch(_bb);
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(60)]
	public virtual Date getLastModifiedDate()
	{
		return lastModifiedDate;
	}

	[LineNumberTable(64)]
	public virtual int getObjectModelVersion()
	{
		return objectModelVersion;
	}

	[LineNumberTable(68)]
	public virtual UL getOp()
	{
		return op;
	}

	[LineNumberTable(72)]
	public virtual UL[] getEssenceContainers()
	{
		return essenceContainers;
	}

	[LineNumberTable(76)]
	public virtual UL[] getDmSchemes()
	{
		return dmSchemes;
	}
}
