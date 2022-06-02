using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class RgbToYuv420pHiBD : Object, TransformHiBD
{
	private int upShift;

	private int downShift;

	private int downShiftChr;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 105, 104, 104, 106 })]
	public RgbToYuv420pHiBD(int upShift, int downShift)
	{
		this.upShift = upShift;
		this.downShift = downShift;
		downShiftChr = downShift + 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 125, 130, 115, 112, 112, 107, 107, 139, 110,
		125, 125
	})]
	public static void rgb2yuv(int r, int g, int b, int[] Y, int offY, int[] U, int offU, int[] V, int offV)
	{
		int y = 66 * r + 129 * g + 25 * b;
		int u = -38 * r - 74 * g + 112 * b;
		int v = 112 * r - 94 * g - 18 * b;
		y = y + 128 >> 8;
		u = u + 128 >> 8;
		v = v + 128 >> 8;
		Y[offY] = clip(y + 16);
		int num = offU;
		int[] array = U;
		array[num] += clip(u + 128);
		num = offV;
		array = V;
		array[num] += clip(v + 128);
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(83)]
	private static int clip(int val)
	{
		return (val >= 0) ? ((val <= 255) ? val : 255) : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 130, 106, 136, 122, 115, 115, 103, 135,
		159, 3, 159, 0, 159, 15, 159, 6, 133, 159,
		5, 159, 0, 159, 17, 127, 6, 133, 127, 0,
		159, 0, 101, 231, 37, 236, 93, 102, 232, 33,
		236, 97
	})]
	public virtual void transform(PictureHiBD img, PictureHiBD dst)
	{
		int[] y = img.getData()[0];
		int[][] dstData = dst.getData();
		int offChr = 0;
		int offLuma = 0;
		int offSrc = 0;
		int strideSrc = img.getWidth() * 3;
		int strideDst = dst.getWidth();
		for (int i = 0; i < img.getHeight() >> 1; i++)
		{
			for (int j = 0; j < img.getWidth() >> 1; j++)
			{
				dstData[1][offChr] = 0;
				dstData[2][offChr] = 0;
				rgb2yuv(y[offSrc], y[offSrc + 1], y[offSrc + 2], dstData[0], offLuma, dstData[1], offChr, dstData[2], offChr);
				dstData[0][offLuma] = dstData[0][offLuma] << upShift >> downShift;
				rgb2yuv(y[offSrc + strideSrc], y[offSrc + strideSrc + 1], y[offSrc + strideSrc + 2], dstData[0], offLuma + strideDst, dstData[1], offChr, dstData[2], offChr);
				dstData[0][offLuma + strideDst] = dstData[0][offLuma + strideDst] << upShift >> downShift;
				offLuma++;
				rgb2yuv(y[offSrc + 3], y[offSrc + 4], y[offSrc + 5], dstData[0], offLuma, dstData[1], offChr, dstData[2], offChr);
				dstData[0][offLuma] = dstData[0][offLuma] << upShift >> downShift;
				rgb2yuv(y[offSrc + strideSrc + 3], y[offSrc + strideSrc + 4], y[offSrc + strideSrc + 5], dstData[0], offLuma + strideDst, dstData[1], offChr, dstData[2], offChr);
				dstData[0][offLuma + strideDst] = dstData[0][offLuma + strideDst] << upShift >> downShift;
				offLuma++;
				dstData[1][offChr] = dstData[1][offChr] << upShift >> downShiftChr;
				dstData[2][offChr] = dstData[2][offChr] << upShift >> downShiftChr;
				offChr++;
				offSrc += 6;
			}
			offLuma += strideDst;
			offSrc += strideSrc;
		}
	}
}
