using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.vpx;

public class VP8FixedRateControl : Object, RateControl
{
	private int rate;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 105, 104 })]
	public VP8FixedRateControl(int rate)
	{
		this.rate = rate;
	}

	[LineNumberTable(23)]
	public virtual int[] getSegmentQps()
	{
		return null;
	}

	[LineNumberTable(29)]
	public virtual int getSegment()
	{
		return 0;
	}

	[LineNumberTable(36)]
	public virtual void report(int bits)
	{
	}

	[LineNumberTable(42)]
	public virtual void reset()
	{
	}
}
