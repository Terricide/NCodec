using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.containers.mp4;

public class MP4Packet : Packet
{
	private long mediaPts;

	private int entryNo;

	private long fileOff;

	private int size;

	private bool psync;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(53)]
	public virtual int getEntryNo()
	{
		return entryNo;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 161, 68, 118, 105, 105, 105, 105, 104 })]
	public MP4Packet(ByteBuffer data, long pts, int timescale, long duration, long frameNo, FrameType iframe, TapeTimecode tapeTimecode, int displayOrder, long mediaPts, int entryNo, long fileOff, int size, bool psync)
		: base(data, pts, timescale, duration, frameNo, iframe, tapeTimecode, displayOrder)
	{
		this.mediaPts = mediaPts;
		this.entryNo = entryNo;
		this.fileOff = fileOff;
		this.size = size;
		this.psync = psync;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(15)]
	public static MP4Packet createMP4PacketWithTimecode(MP4Packet other, TapeTimecode timecode)
	{
		MP4Packet result = createMP4Packet(other.data, other.pts, other.timescale, other.duration, other.frameNo, other.frameType, timecode, other.displayOrder, other.mediaPts, other.entryNo);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(26)]
	public static MP4Packet createMP4Packet(ByteBuffer data, long pts, int timescale, long duration, long frameNo, FrameType iframe, TapeTimecode tapeTimecode, int displayOrder, long mediaPts, int entryNo)
	{
		MP4Packet result = new MP4Packet(data, pts, timescale, duration, frameNo, iframe, tapeTimecode, displayOrder, mediaPts, entryNo, 0L, 0, psync: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(20)]
	public static MP4Packet createMP4PacketWithData(MP4Packet other, ByteBuffer frm)
	{
		MP4Packet result = createMP4Packet(frm, other.pts, other.timescale, other.duration, other.frameNo, other.frameType, other.tapeTimecode, other.displayOrder, other.mediaPts, other.entryNo);
		
		return result;
	}

	[LineNumberTable(57)]
	public virtual long getMediaPts()
	{
		return mediaPts;
	}

	[LineNumberTable(61)]
	public virtual long getFileOff()
	{
		return fileOff;
	}

	[LineNumberTable(65)]
	public virtual int getSize()
	{
		return size;
	}

	[LineNumberTable(69)]
	public virtual bool isPsync()
	{
		return psync;
	}

	[HideFromJava]
	static MP4Packet()
	{
		Packet.___003Cclinit_003E();
	}
}
