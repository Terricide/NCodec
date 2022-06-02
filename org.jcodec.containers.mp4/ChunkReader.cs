using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4;

public class ChunkReader : Object
{
	private int curChunk;

	private int sampleNo;

	private int s2cIndex;

	private int ttsInd;

	private int ttsSubInd;

	private long chunkTv;

	private long[] chunkOffsets;

	private SampleToChunkBox.SampleToChunkEntry[] sampleToChunk;

	private SampleSizesBox stsz;

	private TimeToSampleBox.TimeToSampleEntry[] tts;

	private SampleDescriptionBox stsd;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 130, 109, 127, 12, 105, 143 })]
	private int getFrameSize()
	{
		int size = stsz.getDefaultSize();
		Box box = (Box)stsd.getBoxes().get(sampleToChunk[s2cIndex].getEntry() - 1);
		if (box is AudioSampleEntry)
		{
			int result = ((AudioSampleEntry)box).calcFrameSize();
			
			return result;
		}
		return size;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 233, 55, 104, 104, 233, 72, 104,
		109, 104, 104, 109, 137, 100, 143, 109, 110, 109
	})]
	public ChunkReader(TrakBox trakBox)
	{
		ttsInd = 0;
		ttsSubInd = 0;
		chunkTv = 0L;
		TimeToSampleBox stts = trakBox.getStts();
		tts = stts.getEntries();
		ChunkOffsetsBox stco = trakBox.getStco();
		ChunkOffsets64Box co64 = trakBox.getCo64();
		stsz = trakBox.getStsz();
		SampleToChunkBox stsc = trakBox.getStsc();
		if (stco != null)
		{
			chunkOffsets = stco.getChunkOffsets();
		}
		else
		{
			chunkOffsets = co64.getChunkOffsets();
		}
		sampleToChunk = stsc.getSampleToChunk();
		stsd = trakBox.getStsd();
	}

	[LineNumberTable(52)]
	public virtual bool hasNext()
	{
		return curChunk < (nint)chunkOffsets.LongLength;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 66, 112, 131, 127, 18, 111, 148, 99,
		99, 125, 116, 148, 104, 106, 127, 13, 104, 143,
		118, 239, 58, 234, 74, 100, 100, 111, 139, 191,
		2, 117, 159, 5, 118, 111, 111
	})]
	public virtual Chunk next()
	{
		if (curChunk >= (nint)chunkOffsets.LongLength)
		{
			return null;
		}
		if (s2cIndex + 1 < (nint)sampleToChunk.LongLength && curChunk + 1 == sampleToChunk[s2cIndex + 1].getFirst())
		{
			s2cIndex++;
		}
		int sampleCount = sampleToChunk[s2cIndex].getCount();
		int[] samplesDur = null;
		int sampleDur = 0;
		if (ttsSubInd + sampleCount <= tts[ttsInd].getSampleCount())
		{
			sampleDur = tts[ttsInd].getSampleDuration();
			ttsSubInd += sampleCount;
		}
		else
		{
			samplesDur = new int[sampleCount];
			for (int i = 0; i < sampleCount; i++)
			{
				if (ttsSubInd >= tts[ttsInd].getSampleCount() && ttsInd < (nint)tts.LongLength - 1)
				{
					ttsSubInd = 0;
					ttsInd++;
				}
				samplesDur[i] = tts[ttsInd].getSampleDuration();
				ttsSubInd++;
			}
		}
		int size = 0;
		int[] sizes = null;
		if (stsz.getDefaultSize() > 0)
		{
			size = getFrameSize();
		}
		else
		{
			sizes = Platform.copyOfRangeI(stsz.getSizes(), sampleNo, sampleNo + sampleCount);
		}
		int dref = sampleToChunk[s2cIndex].getEntry();
		Chunk chunk = new Chunk(chunkOffsets[curChunk], chunkTv, sampleCount, size, sizes, sampleDur, samplesDur, dref);
		chunkTv += chunk.getDuration();
		sampleNo += sampleCount;
		curChunk++;
		return chunk;
	}

	[LineNumberTable(107)]
	public virtual int size()
	{
		return chunkOffsets.Length;
	}
}
