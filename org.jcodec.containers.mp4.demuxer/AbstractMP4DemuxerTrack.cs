using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4.demuxer;

[Implements(new string[] { "org.jcodec.common.SeekableDemuxerTrack" })]
public abstract class AbstractMP4DemuxerTrack : Object, SeekableDemuxerTrack, DemuxerTrack
{
	protected internal TrakBox box;

	private MP4TrackType type;

	private int no;

	protected internal SampleEntry[] sampleEntries;

	protected internal TimeToSampleBox.TimeToSampleEntry[] timeToSamples;

	protected internal SampleToChunkBox.SampleToChunkEntry[] sampleToChunks;

	protected internal long[] chunkOffsets;

	protected internal long duration;

	protected internal int sttsInd;

	protected internal int sttsSubInd;

	protected internal int stcoInd;

	protected internal int stscInd;

	protected internal long pts;

	protected internal long curFrame;

	protected internal int timescale;

	protected internal abstract void seekPointer(long l);

	public abstract long getFrameCount();

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 95, 98, 127, 0, 106 })]
	private void seekFrame(long frameNo)
	{
		int num = 0;
		int num2 = num;
		sttsSubInd = num;
		num = num2;
		int num3 = num;
		sttsInd = num;
		pts = num3;
		shiftPts(frameNo);
	}

	[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 98, 102, 113, 106, 131, 100, 99, 136,
		16, 115, 156, 127, 11, 22, 212, 127, 9, 106,
		159, 4, 137
	})]
	public virtual bool seekPts(long pts)
	{
		if (pts < 0u)
		{
			
			throw new IllegalArgumentException("Seeking to negative pts");
		}
		if (pts >= duration)
		{
			return false;
		}
		long prevDur = 0L;
		int frameNo = 0;
		sttsInd = 0;
		while (pts > prevDur + timeToSamples[sttsInd].getSampleCount() * timeToSamples[sttsInd].getSampleDuration() && sttsInd < (nint)timeToSamples.LongLength - 1)
		{
			prevDur += timeToSamples[sttsInd].getSampleCount() * timeToSamples[sttsInd].getSampleDuration();
			frameNo += timeToSamples[sttsInd].getSampleCount();
			sttsInd++;
		}
		long num = pts - prevDur;
		long num2 = timeToSamples[sttsInd].getSampleDuration();
		sttsSubInd = (int)((num2 != -1) ? (num / num2) : (-num));
		frameNo += sttsSubInd;
		this.pts = prevDur + timeToSamples[sttsInd].getSampleDuration() * sttsSubInd;
		seekPointer(frameNo);
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 66, 127, 9, 113, 127, 13, 127, 1,
		127, 1, 148, 127, 9
	})]
	protected internal virtual void shiftPts(long frames)
	{
		pts -= sttsSubInd * timeToSamples[sttsInd].getSampleDuration();
		sttsSubInd = (int)(sttsSubInd + frames);
		while (sttsInd < (nint)timeToSamples.LongLength - 1 && sttsSubInd >= timeToSamples[sttsInd].getSampleCount())
		{
			pts += timeToSamples[sttsInd].getSegmentDuration();
			sttsSubInd -= timeToSamples[sttsInd].getSampleCount();
			sttsInd++;
		}
		pts += sttsSubInd * timeToSamples[sttsInd].getSampleDuration();
	}

	[LineNumberTable(107)]
	public virtual SampleEntry[] getSampleEntries()
	{
		return sampleEntries;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 66, 105, 114, 109, 159, 34, 146, 119,
		119, 120, 152, 109, 109, 155, 111, 108, 30, 201,
		136, 109
	})]
	public AbstractMP4DemuxerTrack(TrakBox trak)
	{
		no = trak.getTrackHeader().getNo();
		type = TrakBox.getTrackType(trak);
		sampleEntries = (SampleEntry[])NodeBox.findAllPath(trak, ClassLiteral<SampleEntry>.Value, new string[5] { "mdia", "minf", "stbl", "stsd", null });
		NodeBox stbl = trak.getMdia().getMinf().getStbl();
		TimeToSampleBox stts = (TimeToSampleBox)NodeBox.findFirst(stbl, ClassLiteral<TimeToSampleBox>.Value, "stts");
		SampleToChunkBox stsc = (SampleToChunkBox)NodeBox.findFirst(stbl, ClassLiteral<SampleToChunkBox>.Value, "stsc");
		ChunkOffsetsBox stco = (ChunkOffsetsBox)NodeBox.findFirst(stbl, ClassLiteral<ChunkOffsetsBox>.Value, "stco");
		ChunkOffsets64Box co64 = (ChunkOffsets64Box)NodeBox.findFirst(stbl, ClassLiteral<ChunkOffsets64Box>.Value, "co64");
		timeToSamples = stts.getEntries();
		sampleToChunks = stsc.getSampleToChunk();
		chunkOffsets = ((stco == null) ? co64.getChunkOffsets() : stco.getChunkOffsets());
		for (int i = 0; i < (nint)timeToSamples.LongLength; i++)
		{
			TimeToSampleBox.TimeToSampleEntry ttse = timeToSamples[i];
			duration += ttse.getSampleCount() * ttse.getSampleDuration();
		}
		box = trak;
		timescale = trak.getTimescale();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 130, 119, 99, 111, 125, 102, 99, 102,
		241, 59, 234, 71
	})]
	public virtual int pts2Sample(long _tv, int _timescale)
	{
		long num = _tv * timescale;
		long num2 = _timescale;
		long tv = ((num2 != -1) ? (num / num2) : (-num));
		int sample = 0;
		int ttsInd;
		for (ttsInd = 0; ttsInd < (nint)timeToSamples.LongLength - 1; ttsInd++)
		{
			int a = timeToSamples[ttsInd].getSampleCount() * timeToSamples[ttsInd].getSampleDuration();
			if (tv < a)
			{
				break;
			}
			tv -= a;
			sample += timeToSamples[ttsInd].getSampleCount();
		}
		int num3 = sample;
		long num4 = tv;
		long num5 = timeToSamples[ttsInd].getSampleDuration();
		return num3 + (int)((num5 != -1) ? (num4 / num5) : (-num4));
	}

	[LineNumberTable(99)]
	public virtual MP4TrackType getType()
	{
		return type;
	}

	[LineNumberTable(103)]
	public virtual int getNo()
	{
		return no;
	}

	[LineNumberTable(111)]
	public virtual TrakBox getBox()
	{
		return box;
	}

	[LineNumberTable(115)]
	public virtual long getTimescale()
	{
		return timescale;
	}

	[LineNumberTable(121)]
	public virtual bool canSeek(long pts)
	{
		return (pts >= 0u && pts < duration) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 162, 112, 98, 143, 127, 18, 143 })]
	protected internal virtual void nextChunk()
	{
		if (stcoInd < (nint)chunkOffsets.LongLength)
		{
			stcoInd++;
			if (stscInd + 1 < (nint)sampleToChunks.LongLength && stcoInd + 1 == sampleToChunks[stscInd + 1].getFirst())
			{
				stscInd++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 100, 130, 102, 113, 106, 99, 106, 131, 104,
		136
	})]
	public virtual bool gotoFrame(long frameNo)
	{
		if (frameNo < 0u)
		{
			
			throw new IllegalArgumentException("negative frame number");
		}
		if (frameNo >= getFrameCount())
		{
			return false;
		}
		if (frameNo == curFrame)
		{
			return true;
		}
		seekPointer(frameNo);
		seekFrame(frameNo);
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 96, 98, 119 })]
	public virtual void seek(double second)
	{
		seekPts(ByteCodeHelper.d2l(second * (double)timescale));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(194)]
	public virtual RationalLarge getDuration()
	{
		RationalLarge.___003Cclinit_003E();
		RationalLarge result = new RationalLarge(box.getMediaDuration(), box.getTimescale());
		
		return result;
	}

	[LineNumberTable(201)]
	public virtual long getCurFrame()
	{
		return curFrame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;")]
	[LineNumberTable(new byte[] { 159, 91, 98, 127, 2, 100, 106 })]
	public virtual List getEdits()
	{
		EditListBox editListBox = (EditListBox)NodeBox.findFirstPath(box, ClassLiteral<EditListBox>.Value, Box.path("edts.elst"));
		if (editListBox != null)
		{
			List edits = editListBox.getEdits();
			
			return edits;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 89, 66, 127, 2 })]
	public virtual string getName()
	{
		return ((NameBox)NodeBox.findFirstPath(box, ClassLiteral<NameBox>.Value, Box.path("udta.name")))?.getName();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 88, 98, 104, 111, 115 })]
	public virtual string getFourcc()
	{
		SampleEntry[] entries = getSampleEntries();
		return ((entries != null && entries.Length != 0) ? entries[0] : null)?.getHeader().getFourcc();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 86, 98, 104, 105, 105, 107, 112, 104 })]
	protected internal virtual ByteBuffer readPacketData(SeekableByteChannel input, ByteBuffer buffer, long offset, int size)
	{
		ByteBuffer result = buffer.duplicate();
		lock (input)
		{
			input.setPosition(offset);
			NIOUtils.readL(input, result, size);
		}
		result.flip();
		return result;
	}

	[Throws(new string[] { "java.io.IOException" })]
	public abstract MP4Packet getNextFrame(ByteBuffer bb);

	[LineNumberTable(237)]
	public virtual ByteBuffer convertPacket(ByteBuffer _in)
	{
		return _in;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(242)]
	public virtual DemuxerTrackMeta getMeta()
	{
		DemuxerTrackMeta result = MP4DemuxerTrackMeta.fromTrack(this);
		
		return result;
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public abstract Packet nextFrame();

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public abstract bool gotoSyncFrame(long P_0);
}
