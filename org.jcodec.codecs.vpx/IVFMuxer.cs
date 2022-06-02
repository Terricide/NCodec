using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.vpx;

[Implements(new string[] { "org.jcodec.common.Muxer", "org.jcodec.common.MuxerTrack" })]
public class IVFMuxer : Object, Muxer, MuxerTrack
{
	private SeekableByteChannel ch;

	private int nFrames;

	private Size dim;

	private int frameRate;

	private bool headerWritten;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 162, 105, 104 })]
	public IVFMuxer(SeekableByteChannel ch)
	{
		this.ch = ch;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 125, 130, 105, 141, 106, 106, 106, 106, 105,
		106, 109, 116, 116, 110, 105, 105, 136, 110
	})]
	private void writeHeader()
	{
		ByteBuffer ivf = ByteBuffer.allocate(32);
		ivf.order(ByteOrder.LITTLE_ENDIAN);
		ivf.put(68);
		ivf.put(75);
		ivf.put(73);
		ivf.put(70);
		ivf.putShort(0);
		ivf.putShort(32);
		ivf.putInt(808996950);
		ivf.putShort((short)dim.getWidth());
		ivf.putShort((short)dim.getHeight());
		ivf.putInt(frameRate);
		ivf.putInt(1);
		ivf.putInt(1);
		ivf.clear();
		ch.write(ivf);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 105, 109, 103, 168, 105, 109, 104,
		110, 111, 136, 110, 142, 111
	})]
	public virtual void addFrame(Packet pkt)
	{
		if (!headerWritten)
		{
			frameRate = pkt.getTimescale();
			writeHeader();
			headerWritten = true;
		}
		ByteBuffer fh = ByteBuffer.allocate(12);
		fh.order(ByteOrder.LITTLE_ENDIAN);
		ByteBuffer frame = pkt.getData();
		fh.putInt(frame.remaining());
		fh.putLong(nFrames);
		fh.clear();
		ch.write(fh);
		ch.write(frame);
		nFrames++;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 128, 98, 112, 116 })]
	public virtual void close()
	{
		ch.setPosition(24L);
		NIOUtils.writeIntLE(ch, nFrames);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 162, 105, 113, 109 })]
	public virtual MuxerTrack addVideoTrack(Codec codec, VideoCodecMeta meta)
	{
		if (dim != null)
		{
			
			throw new RuntimeException("IVF can not have multiple video tracks.");
		}
		dim = meta.getSize();
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(92)]
	public virtual MuxerTrack addAudioTrack(Codec codec, AudioCodecMeta meta)
	{
		
		throw new RuntimeException("Video-only container");
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(97)]
	public virtual void finish()
	{
	}
}
