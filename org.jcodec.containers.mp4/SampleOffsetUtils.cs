using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4;

public class SampleOffsetUtils : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 106, 106, 108, 104, 105, 41, 169 })]
	public static long getSampleOffset(int sample, SampleToChunkBox stsc, ChunkOffsetsBox stco, SampleSizesBox stsz)
	{
		int chunkBySample = getChunkBySample(sample, stco, stsc);
		int firstSampleAtChunk = getFirstSampleAtChunk(chunkBySample, stsc, stco);
		long offset = stco.getChunkOffsets()[chunkBySample - 1];
		int[] sizes = stsz.getSizes();
		for (int i = firstSampleAtChunk; i < sample; i++)
		{
			offset += sizes[i];
		}
		return offset;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 66, 105, 99, 99, 103, 106, 102, 105,
		131, 227, 58, 231, 72
	})]
	public static int getChunkBySample(int sampleOfInterest, ChunkOffsetsBox stco, SampleToChunkBox stsc)
	{
		int chunks = stco.getChunkOffsets().Length;
		int startSample = 0;
		int endSample = 0;
		for (int i = 1; i <= chunks; i++)
		{
			int samplesInChunk = getSamplesInChunk(i, stsc);
			endSample = startSample + samplesInChunk;
			if (sampleOfInterest >= startSample && sampleOfInterest < endSample)
			{
				return i;
			}
			startSample = endSample;
		}
		return -1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 162, 105, 99, 103, 101, 131, 105, 229,
		59, 231, 71
	})]
	public static int getFirstSampleAtChunk(int chunk, SampleToChunkBox stsc, ChunkOffsetsBox stco)
	{
		int chunks = stco.getChunkOffsets().Length;
		int samples = 0;
		for (int i = 1; i <= chunks && i != chunk; i++)
		{
			int samplesInChunk = getSamplesInChunk(i, stsc);
			samples += samplesInChunk;
		}
		return samples;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 66, 104, 99, 116, 108, 131, 233, 60,
		233, 70
	})]
	public static int getSamplesInChunk(int chunk, SampleToChunkBox stsc)
	{
		SampleToChunkBox.SampleToChunkEntry[] sampleToChunk = stsc.getSampleToChunk();
		int sampleCount = 0;
		SampleToChunkBox.SampleToChunkEntry[] array = sampleToChunk;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			SampleToChunkBox.SampleToChunkEntry sampleToChunkEntry = array[i];
			if (sampleToChunkEntry.getFirst() > chunk)
			{
				return sampleCount;
			}
			sampleCount = sampleToChunkEntry.getCount();
		}
		return sampleCount;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(24)]
	public SampleOffsetUtils()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 136, 162, 104, 125, 124, 124, 125, 109, 105,
		108, 122
	})]
	public static ByteBuffer getSampleData(int sample, File file)
	{
		MovieBox moov = MP4Util.parseMovie(file);
		MediaInfoBox minf = ((TrakBox)moov.getAudioTracks().get(0)).getMdia().getMinf();
		ChunkOffsetsBox stco = (ChunkOffsetsBox)NodeBox.findFirstPath(minf, ClassLiteral<ChunkOffsetsBox>.Value, Box.path("stbl.stco"));
		SampleToChunkBox stsc = (SampleToChunkBox)NodeBox.findFirstPath(minf, ClassLiteral<SampleToChunkBox>.Value, Box.path("stbl.stsc"));
		SampleSizesBox stsz = (SampleSizesBox)NodeBox.findFirstPath(minf, ClassLiteral<SampleSizesBox>.Value, Box.path("stbl.stsz"));
		long sampleOffset = getSampleOffset(sample, stsc, stco, stsz);
		MappedByteBuffer map = NIOUtils.mapFile(file);
		map.position((int)sampleOffset);
		map.limit(map.position() + stsz.getSizes()[sample]);
		return map;
	}
}
