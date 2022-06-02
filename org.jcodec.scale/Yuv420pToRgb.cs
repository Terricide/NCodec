using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.scale;

public class Yuv420pToRgb : Object, Transform
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 105 })]
	public Yuv420pToRgb()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 115, 97, 87, 110, 140, 117, 120, 154, 127,
		5, 127, 11, 159, 5, 121, 111, 146, 121, 113,
		148, 121, 113, 116
	})]
	public static void YUV420pToRGBH2H(byte yh, byte yl, byte uh, byte ul, byte vh, byte vl, int nlbi, byte[] data, byte[] lowBits, int nlbo, int off)
	{
		int yh2 = (sbyte)yh;
		int yl2 = (sbyte)yl;
		int uh2 = (sbyte)uh;
		int ul2 = (sbyte)ul;
		int vh2 = (sbyte)vh;
		int vl2 = (sbyte)vl;
		int clipMax = (1 << nlbo << 8) - 1;
		int round = 1 << nlbo >> 1;
		int c = (yh2 + 128 << nlbi) + yl2 - 64;
		int d = (uh2 + 128 << nlbi) + ul2 - 512;
		int e = (vh2 + 128 << nlbi) + vl2 - 512;
		int r = MathUtil.clip(298 * c + 409 * e + 128 >> 8, 0, clipMax);
		int g = MathUtil.clip(298 * c - 100 * d - 208 * e + 128 >> 8, 0, clipMax);
		int b = MathUtil.clip(298 * c + 516 * d + 128 >> 8, 0, clipMax);
		int valR = MathUtil.clip(r + round >> nlbo, 0, 255);
		data[off] = (byte)(sbyte)(valR - 128);
		lowBits[off] = (byte)(sbyte)(r - (valR << nlbo));
		int valG = MathUtil.clip(g + round >> nlbo, 0, 255);
		data[off + 1] = (byte)(sbyte)(valG - 128);
		lowBits[off + 1] = (byte)(sbyte)(g - (valG << nlbo));
		int valB = MathUtil.clip(b + round >> nlbo, 0, 255);
		data[off + 2] = (byte)(sbyte)(valB - 128);
		lowBits[off + 2] = (byte)(sbyte)(b - (valB << nlbo));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 118, 129, 74, 102, 122, 127, 0, 122, 121,
		123, 123
	})]
	public static void YUV420pToRGBN2N(byte y, byte u, byte v, byte[] data, int off)
	{
		int y2 = (sbyte)y;
		int v2 = (sbyte)v;
		int u2 = (sbyte)u;
		int c = y2 + 112;
		int r = 298 * c + 409 * v2 + 128 >> 8;
		int g = 298 * c - 100 * u2 - 208 * v2 + 128 >> 8;
		int b = 298 * c + 516 * u2 + 128 >> 8;
		data[off] = (byte)(sbyte)(MathUtil.clip(r, 0, 255) - 128);
		data[off + 1] = (byte)(sbyte)(MathUtil.clip(g, 0, 255) - 128);
		data[off + 2] = (byte)(sbyte)(MathUtil.clip(b, 0, 255) - 128);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 66, 105, 105, 105, 99, 100, 100, 137,
		101, 102, 103, 135, 106, 118, 119, 105, 137, 103,
		105, 115, 115, 103, 104, 159, 22, 191, 28, 191,
		31, 223, 42, 126, 159, 3, 159, 5, 223, 9,
		231, 40, 236, 90, 110, 139, 126, 159, 5, 167,
		234, 28, 236, 102, 110, 115, 103, 126, 159, 3,
		231, 59, 236, 71, 107, 139, 158, 167
	})]
	public void transform(Picture src, Picture dst)
	{
		byte[] yh = src.getPlaneData(0);
		byte[] uh = src.getPlaneData(1);
		byte[] vh = src.getPlaneData(2);
		byte[] yl = null;
		byte[] ul = null;
		byte[] vl = null;
		byte[][] low = src.getLowBits();
		if (low != null)
		{
			yl = low[0];
			ul = low[1];
			vl = low[2];
		}
		byte[] data = dst.getPlaneData(0);
		byte[] lowBits = ((dst.getLowBits() != null) ? dst.getLowBits()[0] : null);
		int hbd = ((src.isHiBD() && dst.isHiBD()) ? 1 : 0);
		int lowBitsNumSrc = src.getLowBitsNum();
		int lowBitsNumDst = dst.getLowBitsNum();
		int offLuma = 0;
		int offChroma = 0;
		int stride = dst.getWidth();
		for (int i = 0; i < dst.getHeight() >> 1; i++)
		{
			for (int k2 = 0; k2 < dst.getWidth() >> 1; k2++)
			{
				int m = k2 << 1;
				if (hbd != 0)
				{
					YUV420pToRGBH2H(yh[offLuma + m], yl[offLuma + m], uh[offChroma], ul[offChroma], vh[offChroma], vl[offChroma], lowBitsNumSrc, data, lowBits, lowBitsNumDst, (offLuma + m) * 3);
					YUV420pToRGBH2H(yh[offLuma + m + 1], yl[offLuma + m + 1], uh[offChroma], ul[offChroma], vh[offChroma], vl[offChroma], lowBitsNumSrc, data, lowBits, lowBitsNumDst, (offLuma + m + 1) * 3);
					YUV420pToRGBH2H(yh[offLuma + m + stride], yl[offLuma + m + stride], uh[offChroma], ul[offChroma], vh[offChroma], vl[offChroma], lowBitsNumSrc, data, lowBits, lowBitsNumDst, (offLuma + m + stride) * 3);
					YUV420pToRGBH2H(yh[offLuma + m + stride + 1], yl[offLuma + m + stride + 1], uh[offChroma], ul[offChroma], vh[offChroma], vl[offChroma], lowBitsNumSrc, data, lowBits, lowBitsNumDst, (offLuma + m + stride + 1) * 3);
				}
				else
				{
					YUV420pToRGBN2N(yh[offLuma + m], uh[offChroma], vh[offChroma], data, (offLuma + m) * 3);
					YUV420pToRGBN2N(yh[offLuma + m + 1], uh[offChroma], vh[offChroma], data, (offLuma + m + 1) * 3);
					YUV420pToRGBN2N(yh[offLuma + m + stride], uh[offChroma], vh[offChroma], data, (offLuma + m + stride) * 3);
					YUV420pToRGBN2N(yh[offLuma + m + stride + 1], uh[offChroma], vh[offChroma], data, (offLuma + m + stride + 1) * 3);
				}
				offChroma++;
			}
			if (((uint)dst.getWidth() & (true ? 1u : 0u)) != 0)
			{
				int l = dst.getWidth() - 1;
				YUV420pToRGBN2N(yh[offLuma + l], uh[offChroma], vh[offChroma], data, (offLuma + l) * 3);
				YUV420pToRGBN2N(yh[offLuma + l + stride], uh[offChroma], vh[offChroma], data, (offLuma + l + stride) * 3);
				offChroma++;
			}
			offLuma += 2 * stride;
		}
		if (((uint)dst.getHeight() & (true ? 1u : 0u)) != 0)
		{
			for (int n = 0; n < dst.getWidth() >> 1; n++)
			{
				int k = n << 1;
				YUV420pToRGBN2N(yh[offLuma + k], uh[offChroma], vh[offChroma], data, (offLuma + k) * 3);
				YUV420pToRGBN2N(yh[offLuma + k + 1], uh[offChroma], vh[offChroma], data, (offLuma + k + 1) * 3);
				offChroma++;
			}
			if (((uint)dst.getWidth() & (true ? 1u : 0u)) != 0)
			{
				int j = dst.getWidth() - 1;
				YUV420pToRGBN2N(yh[offLuma + j], uh[offChroma], vh[offChroma], data, (offLuma + j) * 3);
				offChroma++;
			}
		}
	}
}
