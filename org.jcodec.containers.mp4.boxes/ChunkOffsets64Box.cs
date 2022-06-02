using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class ChunkOffsets64Box : FullBox
{
	private long[] chunkOffsets;

	[LineNumberTable(18)]
	public static string fourcc()
	{
		return "co64";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 106 })]
	public ChunkOffsets64Box(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 115, 104 })]
	public static ChunkOffsets64Box createChunkOffsets64Box(long[] offsets)
	{
		ChunkOffsets64Box co64 = new ChunkOffsets64Box(Header.createHeader(fourcc(), 0L));
		co64.chunkOffsets = offsets;
		return co64;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 104, 104, 109, 103, 47, 167 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		int length = input.getInt();
		chunkOffsets = new long[length];
		for (int i = 0; i < length; i++)
		{
			chunkOffsets[i] = input.getLong();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 98, 104, 111, 109, 106, 9, 199 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(chunkOffsets.Length);
		for (int i = 0; i < (nint)chunkOffsets.LongLength; i++)
		{
			long offset = chunkOffsets[i];
			@out.putLong(offset);
		}
	}

	[LineNumberTable(51)]
	public override int estimateSize()
	{
		return (int)(16 + (nint)chunkOffsets.LongLength * 8);
	}

	[LineNumberTable(55)]
	public virtual long[] getChunkOffsets()
	{
		return chunkOffsets;
	}

	[LineNumberTable(new byte[] { 159, 128, 162, 104 })]
	public virtual void setChunkOffsets(long[] chunkOffsets)
	{
		this.chunkOffsets = chunkOffsets;
	}
}
