using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.wav;

[Implements(new string[] { "org.jcodec.common.Demuxer", "org.jcodec.common.DemuxerTrack" })]
public class WavDemuxer : java.lang.Object, Demuxer, Closeable, AutoCloseable, DemuxerTrack
{
	private const int FRAMES_PER_PKT = 1024;

	private SeekableByteChannel ch;

	private WavHeader header;

	private long dataSize;

	private short frameSize;

	private int frameNo;

	private long pts;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 132, 98, 105, 104, 109, 116, 119 })]
	public WavDemuxer(SeekableByteChannel ch)
	{
		this.ch = ch;
		header = WavHeader.readChannel(ch);
		dataSize = ch.size() - ch.position();
		frameSize = header.getFormat().getFrameSize();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 123, 66, 103, 105 })]
	public virtual List getTracks()
	{
		ArrayList result = new ArrayList();
		((List)result).add((object)this);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 130, 130, 110 })]
	public virtual void close()
	{
		ch.close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 129, 162, 121, 105, 99, 104, 119, 112 })]
	public virtual Packet nextFrame()
	{
		ByteBuffer data = NIOUtils.fetchFromChannel(ch, frameSize * 1024);
		if (!data.hasRemaining())
		{
			return null;
		}
		long oldPts = pts;
		int num = data.remaining();
		short num2 = frameSize;
		int duration = ((num2 != -1) ? (num / num2) : (-num));
		pts += duration;
		int frameRate = header.getFormat().getFrameRate();
		int num3 = data.remaining();
		short num4 = frameSize;
		long duration2 = ((num4 != -1) ? (num3 / num4) : (-num3));
		int num5 = frameNo;
		frameNo = num5 + 1;
		Packet result = Packet.createPacket(data, oldPts, frameRate, duration2, num5, Packet.FrameType.___003C_003EKEY, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 162, 109, 104, 121 })]
	public virtual DemuxerTrackMeta getMeta()
	{
		AudioFormat format = header.getFormat();
		AudioCodecMeta audioCodecMeta = AudioCodecMeta.fromAudioFormat(format);
		long num = dataSize;
		long num2 = format.getFrameSize();
		long totalFrames = ((num2 != -1) ? (num / num2) : (-num));
		DemuxerTrackMeta result = new DemuxerTrackMeta(TrackType.___003C_003EAUDIO, Codec.___003C_003EPCM, (double)totalFrames / (double)format.getFrameRate(), null, (int)totalFrames, null, null, audioCodecMeta);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 122, 162, 103 })]
	public virtual List getVideoTracks()
	{
		return new ArrayList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(89)]
	public virtual List getAudioTracks()
	{
		List tracks = getTracks();
		
		return tracks;
	}

	public void Dispose()
	{
		close();
	}
}
