using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mxf.model;

public class MXFPartition : Object
{
	private MXFPartitionPack pack;

	private long essenceFilePos;

	private bool closed;

	private bool complete;

	private long essenceLength;

	[LineNumberTable(new byte[] { 159, 132, 130, 114 })]
	internal static long roundToKag(long position, int kag_size)
	{
		long num = kag_size;
		long ret = ((num != -1) ? (position / num) : (-position)) * kag_size;
		return (ret != position) ? (ret + kag_size) : ret;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 161, 70, 105, 104, 104, 104, 104, 105 })]
	public MXFPartition(MXFPartitionPack pack, long essenceFilePos, bool closed, bool complete, long essenceLength)
	{
		this.pack = pack;
		this.essenceFilePos = essenceFilePos;
		this.closed = closed;
		this.complete = complete;
		this.essenceLength = essenceLength;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 114, 145, 104, 136, 117, 115, 147 })]
	public static MXFPartition read(UL ul, ByteBuffer bb, long packSize, long nextPartition)
	{
		int closed = (((ul.get(14) & 1) == 0) ? 1 : 0);
		int complete = ((ul.get(14) > 2) ? 1 : 0);
		MXFPartitionPack pp = new MXFPartitionPack(ul);
		pp.readBuf(bb);
		long essenceFilePos = roundToKag(pp.getThisPartition() + packSize, pp.getKagSize()) + roundToKag(pp.getHeaderByteCount(), pp.getKagSize()) + roundToKag(pp.getIndexByteCount(), pp.getKagSize());
		MXFPartition result = new MXFPartition(pp, essenceFilePos, (byte)closed != 0, (byte)complete != 0, nextPartition - essenceFilePos);
		
		return result;
	}

	[LineNumberTable(47)]
	public virtual MXFPartitionPack getPack()
	{
		return pack;
	}

	[LineNumberTable(51)]
	public virtual long getEssenceFilePos()
	{
		return essenceFilePos;
	}

	[LineNumberTable(55)]
	public virtual bool isClosed()
	{
		return closed;
	}

	[LineNumberTable(59)]
	public virtual bool isComplete()
	{
		return complete;
	}

	[LineNumberTable(63)]
	public virtual long getEssenceLength()
	{
		return essenceLength;
	}
}
