using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.wav;

[Implements(new string[] { "org.jcodec.common.Muxer", "org.jcodec.common.MuxerTrack" })]
public class WavMuxer : Object, Muxer, MuxerTrack
{
	protected internal SeekableByteChannel @out;

	protected internal WavHeader header;

	protected internal int written;

	private AudioFormat format;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 130, 105, 104 })]
	public WavMuxer(SeekableByteChannel @out)
	{
		this.@out = @out;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 133, 66, 127, 0 })]
	public virtual void addFrame(Packet outPacket)
	{
		written += @out.write(outPacket.getData());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 132, 66, 111, 127, 9, 110 })]
	public virtual void close()
	{
		@out.setPosition(0L);
		WavHeader.createWavHeader(format, format.bytesToFrames(written)).write(@out);
		NIOUtils.closeQuietly(@out);
	}

	[LineNumberTable(47)]
	public virtual MuxerTrack addVideoTrack(Codec codec, VideoCodecMeta meta)
	{
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 115, 141, 191, 5, 3, 98, 141 })]
	public virtual MuxerTrack addAudioTrack(Codec codec, AudioCodecMeta meta)
	{
		header = WavHeader.createWavHeader(meta.getFormat(), 0);
		format = meta.getFormat();
		IOException ex;
		try
		{
			header.write(@out);
			return this;
		}
		catch (IOException x)
		{
			ex = ByteCodeHelper.MapException<IOException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		IOException e = ex;
		
		throw new RuntimeException(e);
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(65)]
	public virtual void finish()
	{
	}
}
