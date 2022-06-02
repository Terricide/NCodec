using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using org.jcodec.common.io;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4;

public class ChunkWriter : Object
{
	private long[] offsets;

	private SampleEntry[] entries;

	private SeekableByteChannel[] inputs;

	private int curChunk;

	private SeekableByteChannel @out;

	internal byte[] buf;

	private TrakBox trak;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 162, 109, 114, 100, 103, 168, 104, 100,
		103, 168, 108, 140, 104, 106, 103, 9, 201
	})]
	private void cleanDrefs(TrakBox trak)
	{
		MediaInfoBox minf = trak.getMdia().getMinf();
		DataInfoBox dinf = trak.getMdia().getMinf().getDinf();
		if (dinf == null)
		{
			dinf = DataInfoBox.createDataInfoBox();
			minf.add(dinf);
		}
		DataRefBox dref = dinf.getDref();
		if (dref == null)
		{
			dref = DataRefBox.createDataRefBox();
			dinf.add(dref);
		}
		dref.getBoxes().clear();
		dref.add(AliasBox.createSelfRef());
		SampleEntry[] sampleEntries = trak.getSampleEntries();
		for (int i = 0; i < (nint)sampleEntries.LongLength; i++)
		{
			SampleEntry entry = sampleEntries[i];
			entry.setDrefInd(1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 162, 113 })]
	private SeekableByteChannel getInput(Chunk chunk)
	{
		SampleEntry se = entries[chunk.getEntry() - 1];
		return inputs[se.getDrefInd() - 1];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 98, 105, 113, 109, 104, 136, 100, 139,
		105, 136, 109, 104, 104
	})]
	public ChunkWriter(TrakBox trak, SeekableByteChannel[] inputs, SeekableByteChannel @out)
	{
		buf = new byte[8092];
		entries = trak.getSampleEntries();
		ChunkOffsetsBox stco = trak.getStco();
		ChunkOffsets64Box co64 = trak.getCo64();
		int size = ((stco == null) ? co64.getChunkOffsets().Length : stco.getChunkOffsets().Length);
		this.inputs = inputs;
		offsets = new long[size];
		this.@out = @out;
		this.trak = trak;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 162, 127, 2, 157, 114, 111 })]
	public virtual void apply()
	{
		NodeBox stbl = (NodeBox)NodeBox.findFirstPath(trak, ClassLiteral<NodeBox>.Value, Box.path("mdia.minf.stbl"));
		stbl.removeChildren(new string[2] { "stco", "co64" });
		stbl.add(ChunkOffsets64Box.createChunkOffsets64Box(offsets));
		cleanDrefs(trak);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 120, 66, 105, 110, 141, 122, 124 })]
	public virtual void write(Chunk chunk)
	{
		SeekableByteChannel input = getInput(chunk);
		input.setPosition(chunk.getOffset());
		long pos = @out.position();
		@out.write(NIOUtils.fetchFromChannel(input, (int)chunk.getSize()));
		long[] array = offsets;
		int num = curChunk;
		curChunk = num + 1;
		array[num] = pos;
	}
}
