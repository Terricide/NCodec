using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4.muxer;

public class MP4MuxerTrack : AbstractMP4MuxerTrack
{
	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/TimeToSampleBox$TimeToSampleEntry;>;")]
	private List sampleDurations;

	private long sameDurCount;

	private long curDuration;

	private LongArrayList chunkOffsets;

	private IntArrayList sampleSizes;

	private IntArrayList iframes;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/CompositionOffsetsBox$LongEntry;>;")]
	private List compositionOffsets;

	private long lastCompositionOffset;

	private long lastCompositionSamples;

	private long ptsEstimate;

	private int lastEntry;

	private long trackTotalDuration;

	private int curFrame;

	private bool allIframes;

	private TimecodeMP4MuxerTrack timecodeTrack;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 120, 98, 105, 145, 106, 173, 111, 127, 7,
		191, 6, 113, 111, 106, 107, 126, 104, 137, 112,
		180, 115, 109, 168, 147, 105, 150, 136, 143, 116,
		121, 127, 1, 137, 109, 112, 148, 136, 104
	})]
	public virtual void addFrameInternal(Packet pkt, int entryNo)
	{
		if (finished)
		{
			
			throw new IllegalStateException("The muxer track has finished muxing");
		}
		if (_timescale == -1)
		{
			_timescale = pkt.getTimescale();
		}
		if (_timescale != pkt.getTimescale())
		{
			long num = pkt.getPts() * _timescale;
			long num2 = pkt.getTimescale();
			pkt.setPts((num2 != -1) ? (num / num2) : (-num));
			long num3 = pkt.getPts() * _timescale;
			long duration = pkt.getDuration();
			pkt.setDuration((duration != -1) ? (num3 / duration) : (-num3));
		}
		if (type == MP4TrackType.___003C_003EVIDEO)
		{
			long compositionOffset = pkt.getPts() - ptsEstimate;
			if (compositionOffset != lastCompositionOffset)
			{
				if (lastCompositionSamples > 0u)
				{
					compositionOffsets.add(new CompositionOffsetsBox.LongEntry(lastCompositionSamples, lastCompositionOffset));
				}
				lastCompositionOffset = compositionOffset;
				lastCompositionSamples = 0L;
			}
			lastCompositionSamples++;
			ptsEstimate += pkt.getDuration();
		}
		if (lastEntry != -1 && lastEntry != entryNo)
		{
			outChunk(lastEntry);
			samplesInLastChunk = -1;
		}
		curChunk.add(pkt.getData());
		if (pkt.isKeyFrame())
		{
			iframes.add(curFrame + 1);
		}
		else
		{
			allIframes = false;
		}
		curFrame++;
		chunkDuration += pkt.getDuration();
		if (curDuration != -1 && pkt.getDuration() != curDuration)
		{
			sampleDurations.add(new TimeToSampleBox.TimeToSampleEntry((int)sameDurCount, (int)curDuration));
			sameDurCount = 0L;
		}
		curDuration = pkt.getDuration();
		sameDurCount++;
		trackTotalDuration += pkt.getDuration();
		outChunkIfNeeded(entryNo);
		lastEntry = entryNo;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 107, 130, 105, 111 })]
	private void processTimecode(Packet pkt)
	{
		if (timecodeTrack != null)
		{
			timecodeTrack.addTimecode(pkt);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 103, 162, 110, 130, 151, 127, 2, 114, 110,
		131, 125, 159, 8, 114, 143, 105, 110
	})]
	internal virtual void outChunk(int entryNo)
	{
		if (curChunk.size() != 0)
		{
			chunkOffsets.add(@out.position());
			Iterator iterator = curChunk.iterator();
			while (iterator.hasNext())
			{
				ByteBuffer bs = (ByteBuffer)iterator.next();
				sampleSizes.add(bs.remaining());
				@out.write(bs);
			}
			if (samplesInLastChunk == -1 || samplesInLastChunk != curChunk.size())
			{
				samplesInChunks.add(new SampleToChunkBox.SampleToChunkEntry(chunkNo + 1, curChunk.size(), entryNo));
			}
			samplesInLastChunk = curChunk.size();
			chunkNo++;
			chunkDuration = 0L;
			curChunk.clear();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 106, 162, 159, 5, 116, 127, 0, 106, 127,
		5, 125, 138
	})]
	private void outChunkIfNeeded(int entryNo)
	{
		Preconditions.checkState((tgtChunkDurationUnit == Unit.___003C_003EFRAME || tgtChunkDurationUnit == Unit.___003C_003ESEC) ? true : false);
		if (tgtChunkDurationUnit == Unit.___003C_003EFRAME && curChunk.size() * tgtChunkDuration.getDen() == tgtChunkDuration.getNum())
		{
			outChunk(entryNo);
		}
		else if (tgtChunkDurationUnit == Unit.___003C_003ESEC && chunkDuration > 0u && chunkDuration * tgtChunkDuration.getDen() >= tgtChunkDuration.getNum() * _timescale)
		{
			outChunk(entryNo);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 84, 98, 114, 158, 109, 102, 127, 2, 111,
		163, 115, 110, 105, 108, 159, 6, 127, 6, 118,
		195, 115, 115, 117, 31, 4, 233, 69, 144
	})]
	private void putCompositionOffsets(NodeBox stbl)
	{
		if (compositionOffsets.size() <= 0)
		{
			return;
		}
		compositionOffsets.add(new CompositionOffsetsBox.LongEntry(lastCompositionSamples, lastCompositionOffset));
		long min = minLongOffset(compositionOffsets);
		if (min > 0u)
		{
			Iterator iterator = compositionOffsets.iterator();
			while (iterator.hasNext())
			{
				CompositionOffsetsBox.LongEntry entry = (CompositionOffsetsBox.LongEntry)iterator.next();
				entry.offset -= min;
			}
		}
		CompositionOffsetsBox.LongEntry first = (CompositionOffsetsBox.LongEntry)compositionOffsets.get(0);
		if (first.getOffset() > 0u)
		{
			if (edits == null)
			{
				edits = new ArrayList();
				edits.add(new Edit(trackTotalDuration, first.getOffset(), 1f));
			}
			else
			{
				Iterator iterator2 = edits.iterator();
				while (iterator2.hasNext())
				{
					Edit edit = (Edit)iterator2.next();
					edit.setMediaTime(edit.getMediaTime() + first.getOffset());
				}
			}
		}
		CompositionOffsetsBox.Entry[] intEntries = new CompositionOffsetsBox.Entry[compositionOffsets.size()];
		for (int i = 0; i < compositionOffsets.size(); i++)
		{
			CompositionOffsetsBox.LongEntry longEntry = (CompositionOffsetsBox.LongEntry)compositionOffsets.get(i);
			intEntries[i] = new CompositionOffsetsBox.Entry(Ints.checkedCast(longEntry.count), Ints.checkedCast(longEntry.offset));
		}
		stbl.add(CompositionOffsetsBox.createCompositionOffsetsBox(intEntries));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/CompositionOffsetsBox$LongEntry;>;)J")]
	[LineNumberTable(new byte[] { 159, 76, 130, 107, 124, 110, 99 })]
	public static long minLongOffset(List offs)
	{
		long min = long.MaxValue;
		Iterator iterator = offs.iterator();
		while (iterator.hasNext())
		{
			CompositionOffsetsBox.LongEntry entry = (CompositionOffsetsBox.LongEntry)iterator.next();
			min = Math.min(min, entry.getOffset());
		}
		return min;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 66, 235, 45, 105, 233, 71, 105, 105,
		137, 200, 200, 108, 108, 108, 108, 140, 117
	})]
	public MP4MuxerTrack(int trackId, MP4TrackType type)
		: base(trackId, type)
	{
		sameDurCount = 0L;
		curDuration = -1L;
		lastCompositionOffset = 0L;
		lastCompositionSamples = 0L;
		ptsEstimate = 0L;
		lastEntry = -1;
		allIframes = true;
		sampleDurations = new ArrayList();
		chunkOffsets = LongArrayList.createLongArrayList();
		sampleSizes = IntArrayList.createIntArrayList();
		iframes = IntArrayList.createIntArrayList();
		compositionOffsets = new ArrayList();
		setTgtChunkDuration(new Rational(1, 1), Unit.___003C_003EFRAME);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 121, 66, 105, 106 })]
	public override void addFrame(Packet pkt)
	{
		addFrameInternal(pkt, 1);
		processTimecode(pkt);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 97, 98, 151, 141, 107, 159, 1, 136, 103,
		104, 104, 127, 19, 31, 48, 199, 105, 136, 136,
		103, 104, 126, 39, 171, 127, 0, 137, 104, 105,
		111, 126, 105, 114, 138, 105, 104, 136, 127, 4,
		127, 4, 120, 127, 4, 120, 119, 152
	})]
	protected internal override Box finish(MovieHeaderBox mvhd)
	{
		Preconditions.checkState((!finished) ? true : false, (object)"The muxer track has finished muxing");
		outChunk(lastEntry);
		if (sameDurCount > 0u)
		{
			sampleDurations.add(new TimeToSampleBox.TimeToSampleEntry((int)sameDurCount, (int)curDuration));
		}
		finished = true;
		TrakBox trak = TrakBox.createTrakBox();
		Size dd = getDisplayDimensions();
		int num = trackId;
		long num2 = mvhd.getTimescale() * trackTotalDuration;
		long num3 = _timescale;
		TrackHeaderBox tkhd = TrackHeaderBox.createTrackHeaderBox(num, (num3 != -1) ? (num2 / num3) : (-num2), dd.getWidth(), dd.getHeight(), new Date().getTime(), new Date().getTime(), 1f, 0, 0L, new int[9] { 65536, 0, 0, 0, 65536, 0, 0, 0, 1073741824 });
		tkhd.setFlags(15);
		trak.add(tkhd);
		tapt(trak);
		MediaBox media = MediaBox.createMediaBox();
		trak.add(media);
		media.add(MediaHeaderBox.createMediaHeaderBox(_timescale, trackTotalDuration, 0, new Date().getTime(), new Date().getTime(), 0));
		HandlerBox hdlr = HandlerBox.createHandlerBox("mhlr", type.getHandler(), "appl", 0, 0);
		media.add(hdlr);
		MediaInfoBox minf = MediaInfoBox.createMediaInfoBox();
		media.add(minf);
		mediaHeader(minf, type);
		minf.add(HandlerBox.createHandlerBox("dhlr", "url ", "appl", 0, 0));
		addDref(minf);
		NodeBox stbl = new NodeBox(new Header("stbl"));
		minf.add(stbl);
		putCompositionOffsets(stbl);
		putEdits(trak);
		putName(trak);
		stbl.add(SampleDescriptionBox.createSampleDescriptionBox((SampleEntry[])sampleEntries.toArray(new SampleEntry[0])));
		stbl.add(SampleToChunkBox.createSampleToChunkBox((SampleToChunkBox.SampleToChunkEntry[])samplesInChunks.toArray(new SampleToChunkBox.SampleToChunkEntry[0])));
		stbl.add(SampleSizesBox.createSampleSizesBox2(sampleSizes.toArray()));
		stbl.add(TimeToSampleBox.createTimeToSampleBox((TimeToSampleBox.TimeToSampleEntry[])sampleDurations.toArray(new TimeToSampleBox.TimeToSampleEntry[0])));
		stbl.add(ChunkOffsets64Box.createChunkOffsets64Box(chunkOffsets.toArray()));
		if (!allIframes && iframes.size() > 0)
		{
			stbl.add(SyncSamplesBox.createSyncSamplesBox(iframes.toArray()));
		}
		return trak;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/CompositionOffsetsBox$Entry;>;)I")]
	[LineNumberTable(new byte[] { 159, 74, 130, 103, 124, 110, 99 })]
	public static int minOffset(List offs)
	{
		int min = int.MaxValue;
		Iterator iterator = offs.iterator();
		while (iterator.hasNext())
		{
			CompositionOffsetsBox.Entry entry = (CompositionOffsetsBox.Entry)iterator.next();
			min = Math.min(min, entry.getOffset());
		}
		return min;
	}

	[LineNumberTable(283)]
	public override long getTrackTotalDuration()
	{
		return trackTotalDuration;
	}

	[LineNumberTable(287)]
	public virtual TimecodeMP4MuxerTrack getTimecodeTrack()
	{
		return timecodeTrack;
	}

	[LineNumberTable(new byte[] { 159, 70, 162, 104 })]
	public virtual void setTimecode(TimecodeMP4MuxerTrack timecodeTrack)
	{
		this.timecodeTrack = timecodeTrack;
	}
}
