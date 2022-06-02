using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.containers.mps;

public class MPEGPacket : Packet
{
	private long offset;

	private ByteBuffer seq;

	private int gop;

	private int timecode;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 117 })]
	public MPEGPacket(ByteBuffer data, long pts, int timescale, long duration, long frameNo, FrameType keyFrame, TapeTimecode tapeTimecode)
		: base(data, pts, timescale, duration, frameNo, keyFrame, tapeTimecode, 0)
	{
	}

	[LineNumberTable(20)]
	public virtual long getOffset()
	{
		return offset;
	}

	[LineNumberTable(24)]
	public virtual ByteBuffer getSeq()
	{
		return seq;
	}

	[LineNumberTable(28)]
	public virtual int getGOP()
	{
		return gop;
	}

	[LineNumberTable(32)]
	public virtual int getTimecode()
	{
		return timecode;
	}

	[HideFromJava]
	static MPEGPacket()
	{
		Packet.___003Cclinit_003E();
	}
}
