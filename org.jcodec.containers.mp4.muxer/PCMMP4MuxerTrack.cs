using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4.muxer;

public class PCMMP4MuxerTrack : AbstractMP4MuxerTrack
{
	private int frameDuration;

	private int frameSize;

	private int framesInCurChunk;

	private LongArrayList chunkOffsets;

	private int totalFrames;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 111, 108, 104, 118, 110, 141, 117 })]
	public PCMMP4MuxerTrack(int trackId, AudioFormat format)
		: base(trackId, MP4TrackType.___003C_003ESOUND)
	{
		chunkOffsets = LongArrayList.createLongArrayList();
		frameDuration = 1;
		frameSize = (format.getSampleSizeInBits() >> 3) * format.getChannels();
		addSampleEntry(AudioSampleEntry.audioSampleEntryPCM(format));
		_timescale = format.getSampleRate();
		setTgtChunkDuration(new Rational(1, 2), Unit.___003C_003ESEC);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 66, 142, 119, 143, 111, 151, 105 })]
	public virtual void addSamples(ByteBuffer buffer)
	{
		curChunk.add(buffer);
		int num = buffer.remaining();
		int num2 = frameSize;
		int frames = ((num2 != -1) ? (num / num2) : (-num));
		totalFrames += frames;
		framesInCurChunk += frames;
		chunkDuration += frames * frameDuration;
		outChunkIfNeeded();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 122, 66, 159, 10, 122, 116, 105, 127, 5,
		125, 137
	})]
	private void outChunkIfNeeded()
	{
		Preconditions.checkState((tgtChunkDurationUnit == Unit.___003C_003EFRAME || tgtChunkDurationUnit == Unit.___003C_003ESEC) ? true : false, (object)"");
		if (tgtChunkDurationUnit == Unit.___003C_003EFRAME && framesInCurChunk * tgtChunkDuration.getDen() == tgtChunkDuration.getNum())
		{
			outChunk();
		}
		else if (tgtChunkDurationUnit == Unit.___003C_003ESEC && chunkDuration > 0u && chunkDuration * tgtChunkDuration.getDen() >= tgtChunkDuration.getNum() * _timescale)
		{
			outChunk();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 119, 66, 105, 130, 151, 127, 2, 110, 99,
		140, 120, 159, 3, 141, 143, 104, 105
	})]
	private void outChunk()
	{
		if (framesInCurChunk != 0)
		{
			chunkOffsets.add(@out.position());
			Iterator iterator = curChunk.iterator();
			while (iterator.hasNext())
			{
				ByteBuffer b = (ByteBuffer)iterator.next();
				@out.write(b);
			}
			curChunk.clear();
			if (samplesInLastChunk == -1 || framesInCurChunk != samplesInLastChunk)
			{
				samplesInChunks.add(new SampleToChunkBox.SampleToChunkEntry(chunkNo + 1, framesInCurChunk, 1));
			}
			samplesInLastChunk = framesInCurChunk;
			chunkNo++;
			framesInCurChunk = 0;
			chunkDuration = 0L;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 126, 66, 116 })]
	public override void addFrame(Packet outPacket)
	{
		addSamples(outPacket.getData().duplicate());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 114, 162, 105, 145, 135, 136, 103, 104, 104,
		127, 28, 31, 48, 199, 105, 136, 136, 103, 104,
		127, 7, 39, 171, 127, 0, 137, 104, 105, 111,
		126, 105, 114, 138, 104, 136, 127, 4, 127, 4,
		121, 125, 38, 134, 152
	})]
	protected internal override Box finish(MovieHeaderBox mvhd)
	{
		if (finished)
		{
			
			throw new IllegalStateException("The muxer track has finished muxing");
		}
		outChunk();
		finished = true;
		TrakBox trak = TrakBox.createTrakBox();
		Size dd = getDisplayDimensions();
		int num = trackId;
		long num2 = (long)mvhd.getTimescale() * (long)totalFrames * frameDuration;
		long num3 = _timescale;
		TrackHeaderBox tkhd = TrackHeaderBox.createTrackHeaderBox(num, (num3 != -1) ? (num2 / num3) : (-num2), dd.getWidth(), dd.getHeight(), new Date().getTime(), new Date().getTime(), 1f, 0, 0L, new int[9] { 65536, 0, 0, 0, 65536, 0, 0, 0, 1073741824 });
		tkhd.setFlags(15);
		trak.add(tkhd);
		tapt(trak);
		MediaBox media = MediaBox.createMediaBox();
		trak.add(media);
		media.add(MediaHeaderBox.createMediaHeaderBox(_timescale, totalFrames * frameDuration, 0, new Date().getTime(), new Date().getTime(), 0));
		HandlerBox hdlr = HandlerBox.createHandlerBox("mhlr", type.getHandler(), "appl", 0, 0);
		media.add(hdlr);
		MediaInfoBox minf = MediaInfoBox.createMediaInfoBox();
		media.add(minf);
		mediaHeader(minf, type);
		minf.add(HandlerBox.createHandlerBox("dhlr", "url ", "appl", 0, 0));
		addDref(minf);
		NodeBox stbl = new NodeBox(new Header("stbl"));
		minf.add(stbl);
		putEdits(trak);
		putName(trak);
		stbl.add(SampleDescriptionBox.createSampleDescriptionBox((SampleEntry[])sampleEntries.toArray(new SampleEntry[0])));
		stbl.add(SampleToChunkBox.createSampleToChunkBox((SampleToChunkBox.SampleToChunkEntry[])samplesInChunks.toArray(new SampleToChunkBox.SampleToChunkEntry[0])));
		stbl.add(SampleSizesBox.createSampleSizesBox(frameSize, totalFrames));
		stbl.add(TimeToSampleBox.createTimeToSampleBox(new TimeToSampleBox.TimeToSampleEntry[1]
		{
			new TimeToSampleBox.TimeToSampleEntry(totalFrames, frameDuration)
		}));
		stbl.add(ChunkOffsets64Box.createChunkOffsets64Box(chunkOffsets.toArray()));
		return trak;
	}

	[LineNumberTable(164)]
	public override long getTrackTotalDuration()
	{
		return totalFrames * frameDuration;
	}
}
