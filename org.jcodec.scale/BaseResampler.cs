using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.scale;

public abstract class BaseResampler : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[Signature("Ljava/lang/ThreadLocal<[I>;")]
	private ThreadLocal tempBuffers;

	private Size toSize;

	private Size fromSize;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private double scaleFactorX;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private double scaleFactorY;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		118,
		130,
		114,
		104,
		100,
		127,
		1,
		141,
		148,
		114,
		114,
		107,
		152,
		100,
		105,
		63,
		7,
		169,
		245,
		56,
		44,
		234,
		78,
		114,
		114,
		107,
		146,
		100,
		105,
		63,
		3,
		169,
		byte.MaxValue,
		20,
		56,
		44,
		236,
		48,
		234,
		94
	})]
	public virtual void resample(Picture src, Picture dst)
	{
		int[] temp = (int[])tempBuffers.get();
		int taps = nTaps();
		if (temp == null)
		{
			temp = new int[toSize.getWidth() * (fromSize.getHeight() + taps)];
			tempBuffers.set(temp);
		}
		for (int p = 0; p < src.getColor().nComp; p++)
		{
			for (int y2 = 0; y2 < src.getPlaneHeight(p) + taps; y2++)
			{
				for (int x2 = 0; x2 < dst.getPlaneWidth(p); x2++)
				{
					short[] tapsXs = getTapsX(x2);
					int srcX = ByteCodeHelper.d2i(scaleFactorX * (double)x2) - taps / 2 + 1;
					int sum2 = 0;
					for (int j = 0; j < taps; j++)
					{
						sum2 += ((sbyte)getPel(src, p, srcX + j, y2 - taps / 2 + 1) + 128) * tapsXs[j];
					}
					temp[y2 * toSize.getWidth() + x2] = sum2;
				}
			}
			for (int y = 0; y < dst.getPlaneHeight(p); y++)
			{
				for (int x = 0; x < dst.getPlaneWidth(p); x++)
				{
					short[] tapsYs = getTapsY(y);
					int srcY = ByteCodeHelper.d2i(scaleFactorY * (double)y);
					int sum = 0;
					for (int i = 0; i < taps; i++)
					{
						sum += temp[x + (srcY + i) * toSize.getWidth()] * tapsYs[i];
					}
					dst.getPlaneData(p)[y * dst.getPlaneWidth(p) + x] = (byte)(sbyte)(MathUtil.clip(sum + 8192 >> 14, 0, 255) - 128);
				}
			}
		}
	}

	protected internal abstract int nTaps();

	protected internal abstract short[] getTapsX(int i);

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 130, 101, 100, 101, 100, 105, 103, 102,
		105, 103, 134
	})]
	private static byte getPel(Picture pic, int plane, int x, int y)
	{
		if (x < 0)
		{
			x = 0;
		}
		if (y < 0)
		{
			y = 0;
		}
		int w = pic.getPlaneWidth(plane);
		if (x > w - 1)
		{
			x = w - 1;
		}
		int h = pic.getPlaneHeight(plane);
		if (y > h - 1)
		{
			y = h - 1;
		}
		return pic.getData()[plane][x + y * w];
	}

	protected internal abstract short[] getTapsY(int i);

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105, 104, 104, 119, 119, 108 })]
	public BaseResampler(Size from, Size to)
	{
		toSize = to;
		fromSize = from;
		scaleFactorX = (double)from.getWidth() / (double)to.getWidth();
		scaleFactorY = (double)from.getHeight() / (double)to.getHeight();
		tempBuffers = new ThreadLocal();
	}

	[LineNumberTable(new byte[]
	{
		159, 127, 98, 103, 104, 40, 167, 99, 104, 106,
		112, 106, 108, 106, 232, 59, 233, 71, 101, 104,
		100, 106, 127, 0, 5, 201, 117, 101, 110, 134,
		106, 121, 112, 249, 61, 233, 69
	})]
	public static void normalizeAndGenerateFixedPrecision(double[] taps, int precBits, short[] @out)
	{
		double sum = 0.0;
		for (int l = 0; l < (nint)taps.LongLength; l++)
		{
			sum += taps[l];
		}
		int sumFix = 0;
		int precNum = 1 << precBits;
		for (int k = 0; k < (nint)taps.LongLength; k++)
		{
			double d = taps[k] * (double)precNum / sum + (double)precNum;
			int s = ByteCodeHelper.d2i(d);
			taps[k] = d - (double)s;
			@out[k] = (short)(s - precNum);
			sumFix += @out[k];
		}
		long tapsTaken = 0L;
		while (sumFix < precNum)
		{
			int maxI = -1;
			for (int j = 0; j < (nint)taps.LongLength; j++)
			{
				if ((tapsTaken & (1 << j)) == 0u && (maxI == -1 || taps[j] > taps[maxI]))
				{
					maxI = j;
				}
			}
			int num = maxI;
			@out[num]++;
			sumFix++;
			tapsTaken |= 1 << maxI;
		}
		for (int i = 0; i < (nint)taps.LongLength; i++)
		{
			int num = i;
			double[] array = taps;
			array[num] += (double)@out[i];
			if ((tapsTaken & (1 << i)) != 0u)
			{
				num = i;
				array = taps;
				array[num] -= 1.0;
			}
		}
	}
}
