using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale;

public class BicubicResampler : BaseResampler
{
	private short[][] horizontalTaps;

	private short[][] verticalTaps;

	private static double alpha;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		136,
		98,
		104,
		127,
		6,
		105,
		105,
		104,
		108,
		111,
		108,
		106,
		101,
		137,
		107,
		105,
		105,
		119,
		127,
		25,
		123,
		142,
		byte.MaxValue,
		40,
		51,
		236,
		80,
		108,
		233,
		45,
		236,
		85
	})]
	private static short[][] buildFilterTaps(int to, int from)
	{
		double[] taps = new double[4];
		int[] array = new int[2];
		int num = (array[1] = 4);
		num = (array[0] = to);
		short[][] tapsOut = (short[][])ByteCodeHelper.multianewarray(typeof(short[][]).TypeHandle, array);
		double ratio = (double)from / (double)to;
		double toByFrom = (double)to / (double)from;
		double srcPos = 0.0;
		for (int i = 0; i < to; i++)
		{
			double fraction = srcPos - (double)ByteCodeHelper.d2i(srcPos);
			for (int t = -1; t < 3; t++)
			{
				double d = (double)t - fraction;
				if (to < from)
				{
					d *= toByFrom;
				}
				double x = Math.abs(d);
				double xx = x * x;
				double xxx = xx * x;
				if (d >= -1.0 && d <= 1.0)
				{
					taps[t + 1] = (2.0 - alpha) * xxx + (-3.0 + alpha) * xx + 1.0;
				}
				else if (d < -2.0 || d > 2.0)
				{
					taps[t + 1] = 0.0;
				}
				else
				{
					taps[t + 1] = (0.0 - alpha) * xxx + 5.0 * alpha * xx - 8.0 * alpha * x + 4.0 * alpha;
				}
			}
			BaseResampler.normalizeAndGenerateFixedPrecision(taps, 7, tapsOut[i]);
			srcPos += ratio;
		}
		return tapsOut;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 107, 120, 120 })]
	public BicubicResampler(Size from, Size to)
		: base(from, to)
	{
		horizontalTaps = buildFilterTaps(to.getWidth(), from.getWidth());
		verticalTaps = buildFilterTaps(to.getHeight(), from.getHeight());
	}

	[LineNumberTable(56)]
	protected internal override short[] getTapsX(int dstX)
	{
		return horizontalTaps[dstX];
	}

	[LineNumberTable(61)]
	protected internal override short[] getTapsY(int dstY)
	{
		return verticalTaps[dstY];
	}

	[LineNumberTable(66)]
	protected internal override int nTaps()
	{
		return 4;
	}

	[LineNumberTable(16)]
	static BicubicResampler()
	{
		alpha = 0.6;
	}
}
