using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.containers.raw;

[Implements(new string[] { "org.jcodec.common.Muxer", "org.jcodec.common.MuxerTrack" })]
public class RawMuxer : Object, Muxer, MuxerTrack
{
	private SeekableByteChannel ch;

	private bool hasVideo;

	private bool hasAudio;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105, 104 })]
	public RawMuxer(SeekableByteChannel destStream)
	{
		ch = destStream;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 105, 113, 104 })]
	public virtual MuxerTrack addVideoTrack(Codec codec, VideoCodecMeta meta)
	{
		if (hasAudio)
		{
			
			throw new RuntimeException("Raw muxer supports either video or audio track but not both.");
		}
		hasVideo = true;
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 105, 113, 104 })]
	public virtual MuxerTrack addAudioTrack(Codec codec, AudioCodecMeta meta)
	{
		if (hasVideo)
		{
			
			throw new RuntimeException("Raw muxer supports either video or audio track but not both.");
		}
		hasAudio = true;
		return this;
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(41)]
	public virtual void finish()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 131, 98, 120 })]
	public virtual void addFrame(Packet outPacket)
	{
		ch.write(outPacket.getData().duplicate());
	}
}
