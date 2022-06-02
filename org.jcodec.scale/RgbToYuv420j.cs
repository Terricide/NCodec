using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.scale;

public class RgbToYuv420j : Object, Transform
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 105 })]
	public RgbToYuv420j()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 126, 97, 74, 105, 106, 106, 118, 118, 118,
		109, 109, 141, 117, 111, 111
	})]
	public static void rgb2yuv(byte r, byte g, byte b, int[] @out)
	{
		int r2 = (sbyte)r;
		int g2 = (sbyte)g;
		int b2 = (sbyte)b;
		int rS = r2 + 128;
		int gS = g2 + 128;
		int bS = b2 + 128;
		int y = 77 * rS + 150 * gS + 15 * bS;
		int u = -43 * rS - 85 * gS + 128 * bS;
		int v = 128 * rS - 107 * gS - 21 * bS;
		y = y + 128 >> 8;
		u = u + 128 >> 8;
		v = v + 128 >> 8;
		@out[0] = MathUtil.clip(y - 128, -128, 127);
		@out[1] = MathUtil.clip(u, -128, 127);
		@out[2] = MathUtil.clip(v, -128, 127);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 106, 104, 159, 7, 124, 115, 115,
		104, 136, 122, 142, 127, 4, 145, 135, 124, 142,
		127, 6, 113, 135, 127, 8, 159, 8, 103, 231,
		41, 236, 89, 104, 232, 37, 236, 93
	})]
	public virtual void transform(Picture img, Picture dst)
	{
		byte[] y = img.getData()[0];
		byte[][] dstData = dst.getData();
		int[] array = new int[2];
		int num = (array[1] = 3);
		num = (array[0] = 4);
		int[][] @out = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
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
				rgb2yuv(y[offSrc], y[offSrc + 1], y[offSrc + 2], @out[0]);
				dstData[0][offLuma] = (byte)(sbyte)@out[0][0];
				rgb2yuv(y[offSrc + strideSrc], y[offSrc + strideSrc + 1], y[offSrc + strideSrc + 2], @out[1]);
				dstData[0][offLuma + strideDst] = (byte)(sbyte)@out[1][0];
				offLuma++;
				rgb2yuv(y[offSrc + 3], y[offSrc + 4], y[offSrc + 5], @out[2]);
				dstData[0][offLuma] = (byte)(sbyte)@out[2][0];
				rgb2yuv(y[offSrc + strideSrc + 3], y[offSrc + strideSrc + 4], y[offSrc + strideSrc + 5], @out[3]);
				dstData[0][offLuma + strideDst] = (byte)(sbyte)@out[3][0];
				offLuma++;
				dstData[1][offChr] = (byte)(sbyte)(@out[0][1] + @out[1][1] + @out[2][1] + @out[3][1] + 2 >> 2);
				dstData[2][offChr] = (byte)(sbyte)(@out[0][2] + @out[1][2] + @out[2][2] + @out[3][2] + 2 >> 2);
				offChr++;
				offSrc += 6;
			}
			offLuma += strideDst;
			offSrc += strideSrc;
		}
	}
}
