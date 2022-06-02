using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale;

public class LanczosResampler : BaseResampler
{
	private const int _nTaps = 6;

	private int precision;

	private short[][] tapsXs;

	private short[][] tapsYs;

	private double _scaleFactorX;

	private double _scaleFactorY;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 98, 235, 57, 236, 72, 119, 119, 127,
		16, 127, 16, 121, 123
	})]
	public LanczosResampler(Size from, Size to)
		: base(from, to)
	{
		precision = 256;
		_scaleFactorX = (double)to.getWidth() / (double)from.getWidth();
		_scaleFactorY = (double)to.getHeight() / (double)from.getHeight();
		int num = precision;
		int[] array = new int[2];
		int num2 = (array[1] = 6);
		num2 = (array[0] = num);
		tapsXs = (short[][])ByteCodeHelper.multianewarray(typeof(short[][]).TypeHandle, array);
		int num3 = precision;
		array = new int[2];
		num2 = (array[1] = 6);
		num2 = (array[0] = num3);
		tapsYs = (short[][])ByteCodeHelper.multianewarray(typeof(short[][]).TypeHandle, array);
		buildTaps(6, precision, _scaleFactorX, tapsXs);
		buildTaps(6, precision, _scaleFactorY, tapsYs);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 104, 106, 104, 118, 105, 123, 127,
		5, 235, 60, 240, 70, 235, 56, 234, 74
	})]
	private static void buildTaps(int nTaps, int precision, double scaleFactor, short[][] tapsOut)
	{
		double[] taps = new double[nTaps];
		for (int i = 0; i < precision; i++)
		{
			double o = (double)i / (double)precision;
			int j = -nTaps / 2 + 1;
			int t = 0;
			while (j < nTaps / 2 + 1)
			{
				double x = 0.0 - o + (double)j;
				double sinc_val = scaleFactor * sinc(scaleFactor * x * System.Math.PI);
				double wnd_val = java.lang.Math.sin(x * System.Math.PI / (double)(nTaps - 1) + System.Math.PI / 2.0);
				taps[t] = sinc_val * wnd_val;
				j++;
				t++;
			}
			BaseResampler.normalizeAndGenerateFixedPrecision(taps, 7, tapsOut[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(31)]
	private static double sinc(double x)
	{
		return (x != 0.0) ? (java.lang.Math.sin(x) / x) : 1.0;
	}

	[LineNumberTable(new byte[] { 159, 130, 130, 120, 115 })]
	protected internal override short[] getTapsX(int dstX)
	{
		int oi = ByteCodeHelper.d2i((double)(float)(dstX * precision) / _scaleFactorX);
		int num = precision;
		int sub_pel = ((num != -1) ? (oi % num) : 0);
		return tapsXs[sub_pel];
	}

	[LineNumberTable(new byte[] { 159, 128, 98, 120, 147 })]
	protected internal override short[] getTapsY(int dstY)
	{
		int oy = ByteCodeHelper.d2i((double)(float)(dstY * precision) / _scaleFactorY);
		int num = precision;
		int sub_pel = ((num != -1) ? (oy % num) : 0);
		return tapsYs[sub_pel];
	}

	[LineNumberTable(65)]
	protected internal override int nTaps()
	{
		return 6;
	}
}
