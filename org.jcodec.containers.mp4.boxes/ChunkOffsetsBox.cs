using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class ChunkOffsetsBox : FullBox
{
	private long[] chunkOffsets;

	[LineNumberTable(26)]
	public static string fourcc()
	{
		return "stco";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 106 })]
	public ChunkOffsetsBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 118, 104 })]
	public static ChunkOffsetsBox createChunkOffsetsBox(long[] chunkOffsets)
	{
		
		ChunkOffsetsBox stco = new ChunkOffsetsBox(new Header(fourcc()));
		stco.chunkOffsets = chunkOffsets;
		return stco;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 66, 104, 104, 109, 103, 52, 167 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		int length = input.getInt();
		chunkOffsets = new long[length];
		for (int i = 0; i < length; i++)
		{
			chunkOffsets[i] = Platform.unsignedInt(input.getInt());
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 104, 111, 109, 106, 10, 199 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(chunkOffsets.Length);
		for (int i = 0; i < (nint)chunkOffsets.LongLength; i++)
		{
			long offset = chunkOffsets[i];
			@out.putInt((int)offset);
		}
	}

	[LineNumberTable(56)]
	public override int estimateSize()
	{
		return (int)(16 + (nint)chunkOffsets.LongLength * 4);
	}

	[LineNumberTable(60)]
	public virtual long[] getChunkOffsets()
	{
		return chunkOffsets;
	}

	[LineNumberTable(new byte[] { 159, 126, 66, 104 })]
	public virtual void setChunkOffsets(long[] chunkOffsets)
	{
		this.chunkOffsets = chunkOffsets;
	}
}
