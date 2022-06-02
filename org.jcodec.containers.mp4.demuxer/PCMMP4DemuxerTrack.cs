using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4.demuxer;

public class PCMMP4DemuxerTrack : AbstractMP4DemuxerTrack
{
	private int defaultSampleSize;

	private int posShift;

	protected internal int totalFrames;

	private SeekableByteChannel input;

	private MovieBox movie;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 162, 138, 104, 104, 124, 141, 99, 109,
		127, 2, 127, 1, 230, 61, 231, 69, 127, 14
	})]
	public PCMMP4DemuxerTrack(MovieBox movie, TrakBox trak, SeekableByteChannel input)
		: base(trak)
	{
		this.movie = movie;
		this.input = input;
		SampleSizesBox stsz = (SampleSizesBox)NodeBox.findFirstPath(trak, ClassLiteral<SampleSizesBox>.Value, Box.path("mdia.minf.stbl.stsz"));
		defaultSampleSize = stsz.getDefaultSize();
		int chunks = 0;
		for (int i = 1; i < (nint)sampleToChunks.LongLength; i++)
		{
			int ch = (int)(sampleToChunks[i].getFirst() - sampleToChunks[i - 1].getFirst());
			totalFrames += ch * sampleToChunks[i - 1].getCount();
			chunks += ch;
		}
		totalFrames = (int)(totalFrames + sampleToChunks[(nint)sampleToChunks.LongLength - 1].getCount() * ((nint)chunkOffsets.LongLength - chunks));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 114, 66, 125, 113, 143 })]
	public virtual int getFrameSize()
	{
		SampleEntry entry = sampleEntries[sampleToChunks[stscInd].getEntry() - 1];
		if (entry is AudioSampleEntry && defaultSampleSize == 0)
		{
			int result = ((AudioSampleEntry)entry).calcFrameSize();
			
			return result;
		}
		return defaultSampleSize;
	}

	[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 123, 98, 112, 99, 136, 116, 150, 119, 107,
		147, 105, 111, 138, 191, 47, 145, 136, 111, 127,
		18, 143
	})]
	public override MP4Packet getNextFrame(ByteBuffer buffer)
	{
		if (stcoInd >= (nint)chunkOffsets.LongLength)
		{
			return null;
		}
		int frameSize = getFrameSize();
		int se = sampleToChunks[stscInd].getEntry();
		int chSize = sampleToChunks[stscInd].getCount() * frameSize;
		long pktOff = chunkOffsets[stcoInd] + posShift;
		int pktSize = chSize - posShift;
		ByteBuffer result = readPacketData(input, buffer, pktOff, pktSize);
		long ptsRem = pts;
		int doneFrames = ((frameSize != -1) ? (pktSize / frameSize) : (-pktSize));
		shiftPts(doneFrames);
		MP4Packet.___003Cclinit_003E();
		MP4Packet pkt = new MP4Packet(result, QTTimeUtil.mediaToEdited(box, ptsRem, movie.getTimescale()), timescale, (int)(pts - ptsRem), curFrame, Packet.FrameType.___003C_003EKEY, null, 0, ptsRem, se - 1, pktOff, pktSize, psync: true);
		curFrame += doneFrames;
		posShift = 0;
		stcoInd++;
		if (stscInd < (nint)sampleToChunks.LongLength - 1 && stcoInd + 1 == sampleToChunks[stscInd + 1].getFirst())
		{
			stscInd++;
		}
		return pkt;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 98, 104, 157 })]
	public override Packet nextFrame()
	{
		int frameSize = getFrameSize();
		int chSize = sampleToChunks[stscInd].getCount() * frameSize - posShift;
		MP4Packet result = getNextFrame(ByteBuffer.allocate(chSize));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(108)]
	public override bool gotoSyncFrame(long frameNo)
	{
		bool result = gotoFrame(frameNo);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 130, 119, 124, 101, 99, 104, 103, 99,
		120, 104
	})]
	protected internal override void seekPointer(long frameNo)
	{
		stcoInd = 0;
		stscInd = 0;
		curFrame = 0L;
		while (true)
		{
			long nextFrame = curFrame + sampleToChunks[stscInd].getCount();
			if (nextFrame > frameNo)
			{
				break;
			}
			curFrame = nextFrame;
			nextChunk();
		}
		posShift = (int)((frameNo - curFrame) * getFrameSize());
		curFrame = frameNo;
	}

	[LineNumberTable(135)]
	public override long getFrameCount()
	{
		return totalFrames;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 66, 111, 109 })]
	public override DemuxerTrackMeta getMeta()
	{
		AudioSampleEntry ase = (AudioSampleEntry)getSampleEntries()[0];
		AudioCodecMeta audioCodecMeta = AudioCodecMeta.fromAudioFormat(ase.getFormat());
		DemuxerTrackMeta result = new DemuxerTrackMeta(TrackType.___003C_003EAUDIO, Codec.codecByFourcc(getFourcc()), (double)duration / (double)timescale, null, totalFrames, null, null, audioCodecMeta);
		
		return result;
	}
}
