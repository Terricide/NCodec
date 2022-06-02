using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.common.model;

public class AudioFrame : AudioBuffer
{
	private long pts;

	private long duration;

	private long timescale;

	private int frameNo;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 108, 105, 105, 105, 105 })]
	public AudioFrame(ByteBuffer buffer, AudioFormat format, int nFrames, long pts, long duration, long timescale, int frameNo)
		: base(buffer, format, nFrames)
	{
		this.pts = pts;
		this.duration = duration;
		this.timescale = timescale;
		this.frameNo = frameNo;
	}

	[LineNumberTable(29)]
	public virtual long getPts()
	{
		return pts;
	}

	[LineNumberTable(33)]
	public virtual long getDuration()
	{
		return duration;
	}

	[LineNumberTable(37)]
	public virtual long getTimescale()
	{
		return timescale;
	}

	[LineNumberTable(41)]
	public virtual int getFrameNo()
	{
		return frameNo;
	}
}
