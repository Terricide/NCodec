using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.codecs.mpeg12.bitstream;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mps;

namespace org.jcodec.codecs.mpeg12;

public class MPEGES : SegmentReader
{
	private int frameNo;

	public long lastKnownDuration;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 66, 107 })]
	public MPEGES(ReadableByteChannel channel, int fetchSize)
		: base(channel, fetchSize)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 131, 66, 136, 127, 4, 131, 119, 131, 137,
		127, 5, 131, 136, 141
	})]
	public virtual MPEGPacket frame(ByteBuffer buffer)
	{
		ByteBuffer dup = buffer.duplicate();
		while (curMarker != 256 && curMarker != 435 && skipToMarker())
		{
		}
		while (curMarker != 256 && readToNextMarker(dup))
		{
		}
		readToNextMarker(dup);
		while (curMarker != 256 && curMarker != 435 && readToNextMarker(dup))
		{
		}
		dup.flip();
		PictureHeader ph = MPEGDecoder.getPictureHeader(dup.duplicate());
		object result;
		if (dup.hasRemaining())
		{
			MPEGPacket.___003Cclinit_003E();
			long pts = 0L;
			long duration = 0L;
			int num = frameNo;
			frameNo = num + 1;
			result = new MPEGPacket(dup, pts, 90000, duration, num, (ph.picture_coding_type > 1) ? Packet.FrameType.___003C_003EINTER : Packet.FrameType.___003C_003EKEY, null);
		}
		else
		{
			result = null;
		}
		return (MPEGPacket)result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 124, 98, 127, 4, 131, 167, 118, 170, 200,
		127, 4, 138, 104, 141
	})]
	public virtual MPEGPacket getFrame()
	{
		while (curMarker != 256 && curMarker != 435 && skipToMarker())
		{
		}
		ArrayList buffers = new ArrayList();
		while (curMarker != 256 && !done)
		{
			readToNextMarkerBuffers(buffers);
		}
		readToNextMarkerBuffers(buffers);
		while (curMarker != 256 && curMarker != 435 && !done)
		{
			readToNextMarkerBuffers(buffers);
		}
		ByteBuffer dup = NIOUtils.combineBuffers(buffers);
		PictureHeader ph = MPEGDecoder.getPictureHeader(dup.duplicate());
		object result;
		if (dup.hasRemaining())
		{
			MPEGPacket.___003Cclinit_003E();
			long pts = 0L;
			long duration = 0L;
			int num = frameNo;
			frameNo = num + 1;
			result = new MPEGPacket(dup, pts, 90000, duration, num, (ph.picture_coding_type > 1) ? Packet.FrameType.___003C_003EINTER : Packet.FrameType.___003C_003EKEY, null);
		}
		else
		{
			result = null;
		}
		return (MPEGPacket)result;
	}
}
