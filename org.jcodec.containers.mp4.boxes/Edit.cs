using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.containers.mp4.boxes;

public class Edit : Object
{
	private long duration;

	private long mediaTime;

	private float rate;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105, 104, 104, 105 })]
	public Edit(long duration, long mediaTime, float rate)
	{
		this.duration = duration;
		this.mediaTime = mediaTime;
		this.rate = rate;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public static Edit createEdit(Edit edit)
	{
		Edit result = new Edit(edit.duration, edit.mediaTime, edit.rate);
		
		return result;
	}

	[LineNumberTable(26)]
	public virtual long getDuration()
	{
		return duration;
	}

	[LineNumberTable(30)]
	public virtual long getMediaTime()
	{
		return mediaTime;
	}

	[LineNumberTable(34)]
	public virtual float getRate()
	{
		return rate;
	}

	[LineNumberTable(new byte[] { 159, 133, 130, 111 })]
	public virtual void shift(long shift)
	{
		mediaTime += shift;
	}

	[LineNumberTable(new byte[] { 159, 132, 130, 104 })]
	public virtual void setMediaTime(long l)
	{
		mediaTime = l;
	}

	[LineNumberTable(new byte[] { 159, 131, 130, 104 })]
	public virtual void setDuration(long duration)
	{
		this.duration = duration;
	}
}
