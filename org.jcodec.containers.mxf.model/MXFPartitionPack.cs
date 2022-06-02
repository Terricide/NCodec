using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.mxf.model;

public class MXFPartitionPack : MXFMetadata
{
	private int kagSize;

	private long thisPartition;

	private long prevPartition;

	private long footerPartition;

	private long headerByteCount;

	private long indexByteCount;

	private int indexSid;

	private int bodySid;

	private UL op;

	private int nbEssenceContainers;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 106 })]
	public MXFPartitionPack(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 98, 109, 137, 109, 109, 109, 109, 109,
		109, 109, 105, 109, 109, 109
	})]
	public override void readBuf(ByteBuffer bb)
	{
		bb.order(ByteOrder.BIG_ENDIAN);
		NIOUtils.skip(bb, 4);
		kagSize = bb.getInt();
		thisPartition = bb.getLong();
		prevPartition = bb.getLong();
		footerPartition = bb.getLong();
		headerByteCount = bb.getLong();
		indexByteCount = bb.getLong();
		indexSid = bb.getInt();
		NIOUtils.skip(bb, 8);
		bodySid = bb.getInt();
		op = UL.read(bb);
		nbEssenceContainers = bb.getInt();
	}

	[LineNumberTable(54)]
	public virtual long getThisPartition()
	{
		return thisPartition;
	}

	[LineNumberTable(50)]
	public virtual int getKagSize()
	{
		return kagSize;
	}

	[LineNumberTable(66)]
	public virtual long getHeaderByteCount()
	{
		return headerByteCount;
	}

	[LineNumberTable(70)]
	public virtual long getIndexByteCount()
	{
		return indexByteCount;
	}

	[LineNumberTable(58)]
	public virtual long getPrevPartition()
	{
		return prevPartition;
	}

	[LineNumberTable(62)]
	public virtual long getFooterPartition()
	{
		return footerPartition;
	}

	[LineNumberTable(74)]
	public virtual int getIndexSid()
	{
		return indexSid;
	}

	[LineNumberTable(78)]
	public virtual int getBodySid()
	{
		return bodySid;
	}

	[LineNumberTable(82)]
	public virtual UL getOp()
	{
		return op;
	}

	[LineNumberTable(86)]
	public virtual int getNbEssenceContainers()
	{
		return nbEssenceContainers;
	}
}
