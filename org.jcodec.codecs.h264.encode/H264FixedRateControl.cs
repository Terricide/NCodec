using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.encode;

public class H264FixedRateControl : Object, RateControl
{
	private const int INIT_QP = 26;

	private int balance;

	private int perMb;

	private int curQp;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 105, 104, 105 })]
	public H264FixedRateControl(int bitsPer256)
	{
		perMb = bitsPer256;
		curQp = 26;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(29)]
	public virtual int startPicture(Size sz, int maxSize, SliceType sliceType)
	{
		return 26 + ((sliceType == SliceType.___003C_003EP) ? 4 : 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 159, 41, 104, 152 })]
	public virtual int initialQpDelta()
	{
		int qpDelta = ((balance < 0) ? ((balance >= -(perMb >> 1)) ? 1 : 2) : ((balance > perMb) ? ((balance <= perMb << 2) ? (-1) : (-2)) : 0));
		int prevQp = curQp;
		curQp = MathUtil.clip(curQp + qpDelta, 12, 30);
		return curQp - prevQp;
	}

	[LineNumberTable(new byte[] { 159, 131, 98, 150 })]
	public virtual int accept(int bits)
	{
		balance += perMb - bits;
		return 0;
	}

	[LineNumberTable(new byte[] { 159, 130, 162, 104, 105 })]
	public virtual void reset()
	{
		balance = 0;
		curQp = 26;
	}

	[LineNumberTable(56)]
	public virtual int calcFrameSize(int nMB)
	{
		return (256 + nMB * (perMb + 9) >> 3) + (nMB >> 6);
	}

	[LineNumberTable(new byte[] { 159, 127, 66, 104 })]
	public virtual void setRate(int rate)
	{
		perMb = rate;
	}
}
