using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.io;

namespace org.jcodec.containers.mxf.model;

public abstract class MXFInterchangeObject : MXFMetadata
{
	private UL generationUID;

	private UL objectClass;

	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	protected internal abstract void read(Map m);

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 106 })]
	public MXFInterchangeObject(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 162, 109, 103, 108, 110, 110, 105, 159,
		7, 109, 131, 109, 131, 109, 131, 143, 134, 106,
		106
	})]
	public override void readBuf(ByteBuffer bb)
	{
		bb.order(ByteOrder.BIG_ENDIAN);
		HashMap tags = new HashMap();
		while (bb.hasRemaining())
		{
			int tag = bb.getShort() & 0xFFFF;
			int size = bb.getShort() & 0xFFFF;
			ByteBuffer _bb = NIOUtils.read(bb, size);
			switch (tag)
			{
			case 15370:
				uid = UL.read(_bb);
				break;
			case 258:
				generationUID = UL.read(_bb);
				break;
			case 257:
				objectClass = UL.read(_bb);
				break;
			default:
				((Map)tags).put((object)Integer.valueOf(tag), (object)_bb);
				break;
			}
		}
		if (((Map)tags).size() > 0)
		{
			read(tags);
		}
	}

	[LineNumberTable(55)]
	public virtual UL getGenerationUID()
	{
		return generationUID;
	}

	[LineNumberTable(59)]
	public virtual UL getObjectClass()
	{
		return objectClass;
	}
}
