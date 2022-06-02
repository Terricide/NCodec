using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.vpx;

public class NopRateControl : Object, RateControl
{
	private int qp;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 105, 104 })]
	public NopRateControl(int qp)
	{
		this.qp = qp;
	}

	[LineNumberTable(22)]
	public virtual int[] getSegmentQps()
	{
		return new int[1] { qp };
	}

	[LineNumberTable(27)]
	public virtual int getSegment()
	{
		return 0;
	}

	[LineNumberTable(32)]
	public virtual void report(int bits)
	{
	}

	[LineNumberTable(37)]
	public virtual void reset()
	{
	}
}
