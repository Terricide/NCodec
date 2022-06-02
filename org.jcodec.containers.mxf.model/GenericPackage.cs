using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class GenericPackage : MXFInterchangeObject
{
	private UL[] tracks;

	private UL packageUID;

	private string name;

	private Date packageModifiedDate;

	private Date packageCreationDate;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 130, 106 })]
	public GenericPackage(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 135, 162, 120, 141, 109, 159, 19, 109, 134,
		110, 134, 109, 131, 109, 131, 109, 131, 127, 36,
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
			case 17409:
				packageUID = UL.read(_bb);
				break;
			case 17410:
				name = readUtf16String(_bb);
				break;
			case 17411:
				tracks = MXFMetadata.readULBatch(_bb);
				break;
			case 17412:
				packageModifiedDate = MXFMetadata.readDate(_bb);
				break;
			case 17413:
				packageCreationDate = MXFMetadata.readDate(_bb);
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
	public virtual UL[] getTracks()
	{
		return tracks;
	}

	[LineNumberTable(64)]
	public virtual UL getPackageUID()
	{
		return packageUID;
	}

	[LineNumberTable(68)]
	public virtual string getName()
	{
		return name;
	}

	[LineNumberTable(72)]
	public virtual Date getPackageModifiedDate()
	{
		return packageModifiedDate;
	}

	[LineNumberTable(76)]
	public virtual Date getPackageCreationDate()
	{
		return packageCreationDate;
	}
}
