using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class RgbToYuv420jHiBD : Object, TransformHiBD
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105 })]
	public RgbToYuv420jHiBD()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 126, 98, 115, 115, 115, 107, 107, 139, 107,
		125, 125
	})]
	public static void rgb2yuv(int r, int g, int b, int[] Y, int offY, int[] U, int offU, int[] V, int offV)
	{
		int y = 77 * r + 150 * g + 15 * b;
		int u = -43 * r - 85 * g + 128 * b;
		int v = 128 * r - 107 * g - 21 * b;
		y = y + 128 >> 8;
		u = u + 128 >> 8;
		v = v + 128 >> 8;
		Y[offY] = clip(y);
		int num = offU;
		int[] array = U;
		array[num] += clip(u + 128);
		num = offV;
		array = V;
		array[num] += clip(v + 128);
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(78)]
	private static int clip(int val)
	{
		return (val >= 0) ? ((val <= 255) ? val : 255) : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 98, 106, 136, 122, 115, 115, 103, 135,
		159, 3, 139, 159, 15, 145, 133, 159, 5, 139,
		159, 17, 113, 133, 109, 141, 101, 231, 37, 236,
		93, 102, 232, 33, 236, 97
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
				dstData[0][offLuma] = dstData[0][offLuma];
				rgb2yuv(y[offSrc + strideSrc], y[offSrc + strideSrc + 1], y[offSrc + strideSrc + 2], dstData[0], offLuma + strideDst, dstData[1], offChr, dstData[2], offChr);
				dstData[0][offLuma + strideDst] = dstData[0][offLuma + strideDst];
				offLuma++;
				rgb2yuv(y[offSrc + 3], y[offSrc + 4], y[offSrc + 5], dstData[0], offLuma, dstData[1], offChr, dstData[2], offChr);
				dstData[0][offLuma] = dstData[0][offLuma];
				rgb2yuv(y[offSrc + strideSrc + 3], y[offSrc + strideSrc + 4], y[offSrc + strideSrc + 5], dstData[0], offLuma + strideDst, dstData[1], offChr, dstData[2], offChr);
				dstData[0][offLuma + strideDst] = dstData[0][offLuma + strideDst];
				offLuma++;
				dstData[1][offChr] = dstData[1][offChr] >> 2;
				dstData[2][offChr] = dstData[2][offChr] >> 2;
				offChr++;
				offSrc += 6;
			}
			offLuma += strideDst;
			offSrc += strideSrc;
		}
	}
}
