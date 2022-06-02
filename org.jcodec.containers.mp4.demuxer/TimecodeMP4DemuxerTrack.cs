using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using java.util.regex;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4.demuxer;

public class TimecodeMP4DemuxerTrack : java.lang.Object
{
	private TrakBox box;

	private TimeToSampleBox.TimeToSampleEntry[] timeToSamples;

	private int[] sampleCache;

	private TimecodeSampleEntry tse;

	private SeekableByteChannel input;

	private MovieBox movie;

	private long[] chunkOffsets;

	private SampleToChunkBox.SampleToChunkEntry[] sampleToChunks;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 131, 98, 105, 104, 104, 136, 146, 119, 119,
		120, 152, 109, 123, 109, 107, 179, 121
	})]
	public TimecodeMP4DemuxerTrack(MovieBox movie, TrakBox trak, SeekableByteChannel input)
	{
		box = trak;
		this.input = input;
		this.movie = movie;
		NodeBox stbl = trak.getMdia().getMinf().getStbl();
		TimeToSampleBox stts = (TimeToSampleBox)NodeBox.findFirst(stbl, ClassLiteral<TimeToSampleBox>.Value, "stts");
		SampleToChunkBox stsc = (SampleToChunkBox)NodeBox.findFirst(stbl, ClassLiteral<SampleToChunkBox>.Value, "stsc");
		ChunkOffsetsBox stco = (ChunkOffsetsBox)NodeBox.findFirst(stbl, ClassLiteral<ChunkOffsetsBox>.Value, "stco");
		ChunkOffsets64Box co64 = (ChunkOffsets64Box)NodeBox.findFirst(stbl, ClassLiteral<ChunkOffsets64Box>.Value, "co64");
		timeToSamples = stts.getEntries();
		chunkOffsets = ((stco == null) ? co64.getChunkOffsets() : stco.getChunkOffsets());
		sampleToChunks = stsc.getSampleToChunk();
		if ((nint)chunkOffsets.LongLength == 1)
		{
			cacheSamples(sampleToChunks, chunkOffsets);
		}
		tse = (TimecodeSampleEntry)box.getSampleEntries()[0];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 114, 66, 110, 99, 103, 107, 107, 120, 101,
		102, 111, 114, 106, 46, 233, 57, 234, 75, 109,
		112
	})]
	private void cacheSamples(SampleToChunkBox.SampleToChunkEntry[] sampleToChunks, long[] chunkOffsets)
	{
		lock (input)
		{
			int stscInd = 0;
			IntArrayList ss = IntArrayList.createIntArrayList();
			for (int chunkNo = 0; chunkNo < (nint)chunkOffsets.LongLength; chunkNo++)
			{
				int nSamples = sampleToChunks[stscInd].getCount();
				if (stscInd < (nint)sampleToChunks.LongLength - 1 && chunkNo + 1 >= sampleToChunks[stscInd + 1].getFirst())
				{
					stscInd++;
				}
				long offset = chunkOffsets[chunkNo];
				input.setPosition(offset);
				ByteBuffer buf = NIOUtils.fetchFromChannel(input, nSamples * 4);
				for (int i = 0; i < nSamples; i++)
				{
					ss.add(buf.getInt());
				}
			}
			sampleCache = ss.toArray();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 120, 66, 105, 138, 142, 101, 127, 5, 142,
		114, 114, 111, 110, 111, 112
	})]
	private int getTimecodeSample(int sample)
	{
		//Discarded unreachable code: IL_00b3
		if (sampleCache != null)
		{
			return sampleCache[sample];
		}
		lock (input)
		{
			int stscInd = 0;
			int stscSubInd;
			for (stscSubInd = sample; stscInd < (nint)sampleToChunks.LongLength && stscSubInd >= sampleToChunks[stscInd].getCount(); stscInd++)
			{
				stscSubInd -= sampleToChunks[stscInd].getCount();
			}
			long offset = chunkOffsets[stscInd] + (java.lang.Math.min(stscSubInd, sampleToChunks[stscInd].getCount() - 1) << 2);
			if (input.position() != offset)
			{
				input.setPosition(offset);
			}
			ByteBuffer buf = NIOUtils.fetchFromChannel(input, 4);
			return buf.getInt();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(108)]
	private static TapeTimecode _getTimecode(int startCounter, int frameNo, TimecodeSampleEntry entry)
	{
		TapeTimecode result = TapeTimecode.tapeTimecode(frameNo + startCounter, entry.isDropFrame(), (sbyte)entry.getNumFrames() & 0xFF);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 101, 162, 108, 121, 126, 131 })]
	private static bool isValidTimeCode(string input)
	{
		Pattern p = Pattern.compile("[0-9][0-9]:[0-5][0-9]:[0-5][0-9]:[0-2][0-9]");
		CharSequence charSequence = input;
		Matcher i = p.matcher(charSequence);
		if (input != null && !java.lang.String.instancehelper_equals(java.lang.String.instancehelper_trim(input), "") && i.matches())
		{
			return true;
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 106, 130, 109, 127, 2, 137, 127, 6, 47 })]
	public virtual int parseTimecode(string tc)
	{
		string[] split = java.lang.String.instancehelper_split(tc, ":");
		TimecodeSampleEntry tmcd = (TimecodeSampleEntry)NodeBox.findFirstPath(box, ClassLiteral<TimecodeSampleEntry>.Value, Box.path("mdia.minf.stbl.stsd.tmcd"));
		int nf = (sbyte)tmcd.getNumFrames();
		return Integer.parseInt(split[3]) + Integer.parseInt(split[2]) * nf + Integer.parseInt(split[1]) * 60 * nf + Integer.parseInt(split[0]) * 3600 * nf;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 125, 98, 159, 17, 101, 114, 112, 103, 99,
		103, 101, 125, 229, 57, 234, 74, 159, 39
	})]
	public virtual MP4Packet getTimecode(MP4Packet pkt)
	{
		long tv = QTTimeUtil.editedToMedia(box, box.rescale(pkt.getPts(), pkt.getTimescale()), movie.getTimescale());
		int ttsInd = 0;
		int ttsSubInd = 0;
		int sample;
		for (sample = 0; sample < (nint)sampleCache.LongLength - 1; sample++)
		{
			int dur = timeToSamples[ttsInd].getSampleDuration();
			if (tv < dur)
			{
				break;
			}
			tv -= dur;
			ttsSubInd++;
			if (ttsInd < (nint)timeToSamples.LongLength - 1 && ttsSubInd >= timeToSamples[ttsInd].getSampleCount())
			{
				ttsInd++;
			}
		}
		long num = 2u * tv * tse.getTimescale();
		long num2 = box.getTimescale();
		long num3 = ((num2 != -1) ? (num / num2) : (-num));
		long num4 = tse.getFrameDuration();
		int frameNo = (int)(((num4 != -1) ? (num3 / num4) : (-num3)) + 1u) / 2;
		MP4Packet result = MP4Packet.createMP4PacketWithTimecode(pkt, _getTimecode(getTimecodeSample(sample), frameNo, tse));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Obsolete]
	[LineNumberTable(138)]
	public virtual int getStartTimecode()
	{
		int timecodeSample = getTimecodeSample(0);
		
		return timecodeSample;
	}

	[LineNumberTable(142)]
	public virtual TrakBox getBox()
	{
		return box;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[] { 159, 104, 162, 108, 119, 110, 112, 159, 9, 154 })]
	public virtual int timeCodeToFrameNo(string timeCode)
	{
		if (isValidTimeCode(timeCode))
		{
			int movieFrame = parseTimecode(java.lang.String.instancehelper_trim(timeCode)) - sampleCache[0];
			int frameRate = (sbyte)tse.getNumFrames();
			long framesInTimescale = movieFrame * tse.getTimescale();
			TrakBox trak = box;
			long num = frameRate;
			long mediaToEdited = QTTimeUtil.mediaToEdited(trak, (num != -1) ? (framesInTimescale / num) : (-framesInTimescale), movie.getTimescale()) * frameRate;
			long num2 = box.getTimescale();
			return (int)((num2 != -1) ? (mediaToEdited / num2) : (-mediaToEdited));
		}
		return -1;
	}
}
