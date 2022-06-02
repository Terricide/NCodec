using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.encode;

public class DumbRateControl : Object, RateControl
{
	private const int QP = 20;

	private int bitsPerMb;

	private int totalQpDelta;

	private bool justSwitched;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(15)]
	public DumbRateControl()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 137, 162, 106, 111, 104, 163, 127, 5, 111,
		104, 131, 136
	})]
	public virtual int accept(int bits)
	{
		if (bits >= bitsPerMb)
		{
			totalQpDelta++;
			justSwitched = true;
			return 1;
		}
		if (totalQpDelta > 0 && !justSwitched && bitsPerMb - bits > bitsPerMb >> 3)
		{
			totalQpDelta--;
			justSwitched = true;
			return -1;
		}
		justSwitched = false;
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 130, 121, 116, 104, 104 })]
	public virtual int startPicture(Size sz, int maxSize, SliceType sliceType)
	{
		int totalMb = (sz.getWidth() + 15 >> 4) * (sz.getHeight() + 15 >> 4);
		int num = maxSize << 3;
		bitsPerMb = ((totalMb != -1) ? (num / totalMb) : (-num));
		totalQpDelta = 0;
		justSwitched = false;
		return 20 + ((sliceType == SliceType.___003C_003EP) ? 6 : 0);
	}

	[LineNumberTable(51)]
	public virtual int initialQpDelta()
	{
		return 0;
	}
}
