using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mpeg4;

public class MPEG4Interpolator : Object
{
	private static byte[] qpi;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 128, 161, 68, 104, 136, 141, 102, 106, 167,
		102, 106, 167, 159, 45, 114, 134, 123, 123, 134,
		123, 134, 123, 125, 134, 123, 123, 134, 127, 0,
		127, 0, 120, 120, 134, 127, 0, 120, 120, 134,
		127, 0, 127, 2, 120, 120, 134, 123, 134, 127,
		0, 127, 0, 120, 134, 127, 0, 120, 134, 127,
		0, 127, 2, 120, 134, 123, 125, 134, 127, 0,
		127, 0, 120, 121, 134, 127, 0, 120, 121, 134,
		127, 0, 127, 2, 120, 187
	})]
	public static void interpolate16x16QP(byte[] dst, byte[] @ref, int x, int y, int w, int h, int dx, int dy, int refs, bool rounding)
	{
		int xRef = x * 4 + dx;
		int yRef = y * 4 + dy;
		int location = (dx & 3) | ((dy & 3) << 2);
		int xFull = xRef / 4;
		if (xRef < 0 && ((uint)xRef & 3u) != 0)
		{
			xFull += -1;
		}
		int yFull = yRef / 4;
		if (yRef < 0 && ((uint)yRef & 3u) != 0)
		{
			yFull += -1;
		}
		switch (location)
		{
		case 0:
			fulpel16x16(dst, @ref, xFull, yFull, w, h, refs);
			break;
		case 1:
			horzMiddle16(dst, @ref, xFull, yFull, w, h, 16, refs, rounding ? 1 : 0);
			qOff(dst, @ref, xFull, yFull, w, h, 16, refs, rounding ? 1 : 0);
			break;
		case 2:
			horzMiddle16(dst, @ref, xFull, yFull, w, h, 16, refs, rounding ? 1 : 0);
			break;
		case 3:
			horzMiddle16(dst, @ref, xFull, yFull, w, h, 16, refs, rounding ? 1 : 0);
			qOff(dst, @ref, xFull + 1, yFull, w, h, 16, refs, rounding ? 1 : 0);
			break;
		case 4:
			vertMiddle16(dst, @ref, xFull, yFull, w, h, 16, refs, rounding ? 1 : 0);
			qOff(dst, @ref, xFull, yFull, w, h, 16, refs, rounding ? 1 : 0);
			break;
		case 5:
			horzMiddle16(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			qOff(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			vertMiddle16Safe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			qOffSafe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			break;
		case 6:
			horzMiddle16(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			vertMiddle16Safe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			qOffSafe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			break;
		case 7:
			horzMiddle16(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			qOff(qpi, @ref, xFull + 1, yFull, w, h, 17, refs, rounding ? 1 : 0);
			vertMiddle16Safe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			qOffSafe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			break;
		case 8:
			vertMiddle16(dst, @ref, xFull, yFull, w, h, 16, refs, rounding ? 1 : 0);
			break;
		case 9:
			horzMiddle16(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			qOff(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			vertMiddle16Safe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			break;
		case 10:
			horzMiddle16(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			vertMiddle16Safe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			break;
		case 11:
			horzMiddle16(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			qOff(qpi, @ref, xFull + 1, yFull, w, h, 17, refs, rounding ? 1 : 0);
			vertMiddle16Safe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			break;
		case 12:
			vertMiddle16(dst, @ref, xFull, yFull, w, h, 16, refs, rounding ? 1 : 0);
			qOff(dst, @ref, xFull, yFull + 1, w, h, 16, refs, rounding ? 1 : 0);
			break;
		case 13:
			horzMiddle16(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			qOff(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			vertMiddle16Safe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			qOffSafe(dst, qpi, 16, 16, 16, rounding ? 1 : 0);
			break;
		case 14:
			horzMiddle16(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			vertMiddle16Safe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			qOffSafe(dst, qpi, 16, 16, 16, rounding ? 1 : 0);
			break;
		case 15:
			horzMiddle16(qpi, @ref, xFull, yFull, w, h, 17, refs, rounding ? 1 : 0);
			qOff(qpi, @ref, xFull + 1, yFull, w, h, 17, refs, rounding ? 1 : 0);
			vertMiddle16Safe(dst, qpi, 0, 16, 16, rounding ? 1 : 0);
			qOffSafe(dst, qpi, 16, 16, 16, rounding ? 1 : 0);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 92, 97, 68, 104, 137, 141, 102, 115, 167,
		102, 115, 167, 159, 45, 117, 134, 123, 123, 134,
		123, 134, 123, 125, 134, 123, 123, 134, 127, 1,
		127, 1, 120, 120, 134, 127, 1, 120, 120, 134,
		127, 1, 127, 3, 120, 120, 134, 123, 134, 127,
		1, 127, 1, 120, 134, 127, 1, 120, 134, 127,
		1, 127, 3, 120, 134, 123, 125, 134, 127, 1,
		127, 1, 120, 121, 134, 127, 1, 120, 121, 134,
		127, 1, 127, 3, 120, 187
	})]
	public static void interpolate8x8QP(byte[] dst, int dstO, byte[] @ref, int x, int y, int w, int h, int dx, int dy, int refs, bool rounding)
	{
		int xRef = x * 4 + dx;
		int yRef = y * 4 + dy;
		int quads = (dx & 3) | ((dy & 3) << 2);
		int xInt = xRef / 4;
		if (xRef < 0)
		{
			if (4 != -1 && xRef % 4 != 0)
			{
				xInt += -1;
			}
		}
		int yInt = yRef / 4;
		if (yRef < 0)
		{
			if (4 != -1 && yRef % 4 != 0)
			{
				yInt += -1;
			}
		}
		switch (quads)
		{
		case 0:
			fulpel8x8(dst, dstO, 16, @ref, xInt, yInt, w, h, refs);
			break;
		case 1:
			horzMiddle8(dst, dstO, @ref, xInt, yInt, w, h, 8, refs, rounding ? 1 : 0);
			qOff8x8(dst, dstO, @ref, xInt, yInt, w, h, 8, refs, rounding ? 1 : 0);
			break;
		case 2:
			horzMiddle8(dst, dstO, @ref, xInt, yInt, w, h, 8, refs, rounding ? 1 : 0);
			break;
		case 3:
			horzMiddle8(dst, dstO, @ref, xInt, yInt, w, h, 8, refs, rounding ? 1 : 0);
			qOff8x8(dst, dstO, @ref, xInt + 1, yInt, w, h, 8, refs, rounding ? 1 : 0);
			break;
		case 4:
			vertMiddle8(dst, dstO, @ref, xInt, yInt, w, h, 8, refs, rounding ? 1 : 0);
			qOff8x8(dst, dstO, @ref, xInt, yInt, w, h, 8, refs, rounding ? 1 : 0);
			break;
		case 5:
			horzMiddle8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			qOff8x8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			vertMiddle8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			qOff8x8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			break;
		case 6:
			horzMiddle8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			vertMiddle8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			qOff8x8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			break;
		case 7:
			horzMiddle8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			qOff8x8(qpi, 0, @ref, xInt + 1, yInt, w, h, 9, refs, rounding ? 1 : 0);
			vertMiddle8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			qOff8x8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			break;
		case 8:
			vertMiddle8(dst, dstO, @ref, xInt, yInt, w, h, 8, refs, rounding ? 1 : 0);
			break;
		case 9:
			horzMiddle8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			qOff8x8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			vertMiddle8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			break;
		case 10:
			horzMiddle8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			vertMiddle8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			break;
		case 11:
			horzMiddle8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			qOff8x8(qpi, 0, @ref, xInt + 1, yInt, w, h, 9, refs, rounding ? 1 : 0);
			vertMiddle8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			break;
		case 12:
			vertMiddle8(dst, dstO, @ref, xInt, yInt, w, h, 8, refs, rounding ? 1 : 0);
			qOff8x8(dst, dstO, @ref, xInt, yInt + 1, w, h, 8, refs, rounding ? 1 : 0);
			break;
		case 13:
			horzMiddle8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			qOff8x8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			vertMiddle8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			qOff8x8Safe(dst, dstO, qpi, 16, 8, 16, rounding ? 1 : 0);
			break;
		case 14:
			horzMiddle8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			vertMiddle8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			qOff8x8Safe(dst, dstO, qpi, 16, 8, 16, rounding ? 1 : 0);
			break;
		case 15:
			horzMiddle8(qpi, 0, @ref, xInt, yInt, w, h, 9, refs, rounding ? 1 : 0);
			qOff8x8(qpi, 0, @ref, xInt + 1, yInt, w, h, 9, refs, rounding ? 1 : 0);
			vertMiddle8Safe(dst, dstO, qpi, 0, 8, 16, rounding ? 1 : 0);
			qOff8x8Safe(dst, dstO, qpi, 16, 8, 16, rounding ? 1 : 0);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 12, 161, 68, 105, 137, 159, 0, 114, 131,
		115, 131, 115, 131, 181
	})]
	public static void interpolate8x8Planar(byte[] dst, int dstOff, int dstStride, byte[] refn, int x, int y, int w, int h, int dx, int dy, int stride, bool rounding)
	{
		int x_ = x + (dx >> 1);
		int y_ = y + (dy >> 1);
		switch (((dx & 1) << 1) + (dy & 1))
		{
		case 0:
			fulpel8x8(dst, dstOff, dstStride, refn, x_, y_, w, h, stride);
			break;
		case 1:
			interpolate8PlanarVer(dst, dstOff, dstStride, refn, x_, y_, w, h, stride, rounding);
			break;
		case 2:
			interpolate8x8PlanarHor(dst, dstOff, dstStride, refn, x_, y_, w, h, stride, rounding);
			break;
		default:
			interpolate8x8PlanarBoth(dst, dstOff, dstStride, refn, x_, y_, w, h, stride, rounding);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 133, 130, 156, 104, 104, 111, 111, 240, 61,
		39, 233, 72, 105, 106, 106, 55, 41, 233, 70
	})]
	public static void fulpel16x16(byte[] dst, byte[] src, int srcCol, int srcRow, int srcWidth, int srcHeight, int srcStride)
	{
		if (srcCol < 0 || srcRow < 0 || srcCol > srcWidth - 16 || srcRow > srcHeight - 16)
		{
			for (int k = 0; k < 16; k++)
			{
				for (int i = 0; i < 16; i++)
				{
					int y = MathUtil.clip(srcRow + k, 0, srcHeight - 1);
					int x = MathUtil.clip(srcCol + i, 0, srcWidth - 1);
					dst[(k << 4) + i] = src[srcStride * y + x];
				}
			}
			return;
		}
		int srcOffset = srcRow * srcStride + srcCol;
		for (int l = 0; l < 16; l++)
		{
			for (int j = 0; j < 16; j++)
			{
				dst[(l << 4) + j] = src[srcOffset + (l * srcStride + j)];
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159,
		59,
		162,
		156,
		99,
		107,
		114,
		106,
		100,
		100,
		110,
		115,
		118,
		117,
		245,
		60,
		236,
		70,
		122,
		253,
		54,
		234,
		76,
		108,
		100,
		105,
		120,
		21,
		201,
		253,
		58,
		236,
		73,
		230,
		41,
		234,
		89,
		134,
		105,
		100,
		109,
		108,
		100,
		100,
		108,
		121,
		28,
		201,
		124,
		byte.MaxValue,
		0,
		56,
		236,
		74,
		108,
		100,
		105,
		61,
		169,
		254,
		59,
		236,
		72,
		104,
		232,
		44,
		236,
		87
	})]
	private static void horzMiddle16(byte[] dst, byte[] src, int x, int y, int w, int h, int height, int srcStride, int rounding)
	{
		if (x < 0 || y < 0 || x > w - 17 || y > h - height)
		{
			int dstOffset = 0;
			for (int row = 0; row < height; row++)
			{
				int o0 = MathUtil.clip(y + row, 0, h - 1) * srcStride;
				for (int j = 0; j < 4; j++)
				{
					int sum3 = 0;
					int sum5 = 0;
					for (int n = 0; n < 5 + j; n++)
					{
						int srcOffset3 = o0 + MathUtil.clip(x + n, 0, w - 1);
						int srcOffset4 = o0 + MathUtil.clip(x + 16 - n, 0, w - 1);
						sum3 += MPEG4Consts.FILTER_TAB[j][n] * src[srcOffset3];
						sum5 += MPEG4Consts.FILTER_TAB[j][n] * src[srcOffset4];
					}
					dst[dstOffset + j] = (byte)(sbyte)MathUtil.clip(sum3 + 16 - rounding >> 5, -128, 127);
					dst[dstOffset + 15 - j] = (byte)(sbyte)MathUtil.clip(sum5 + 16 - rounding >> 5, -128, 127);
				}
				for (int i = 0; i < 8; i++)
				{
					int sum = 0;
					for (int m = 0; m < 8; m++)
					{
						int srcOffset = o0 + MathUtil.clip(x + m + i + 1, 0, w - 1);
						sum += MPEG4Consts.FILTER_TAB[3][m] * src[srcOffset];
					}
					dst[dstOffset + i + 4] = (byte)(sbyte)MathUtil.clip(sum + 16 - rounding >> 5, -128, 127);
				}
				dstOffset += 16;
			}
			return;
		}
		int srcOffset2 = y * srcStride + x;
		int dstOffset2 = 0;
		for (int row2 = 0; row2 < height; row2++)
		{
			for (int l = 0; l < 4; l++)
			{
				int sum4 = 0;
				int sum6 = 0;
				for (int k3 = 0; k3 < 5 + l; k3++)
				{
					sum4 += MPEG4Consts.FILTER_TAB[l][k3] * src[srcOffset2 + k3];
					sum6 += MPEG4Consts.FILTER_TAB[l][k3] * src[srcOffset2 + 16 - k3];
				}
				dst[dstOffset2 + l] = (byte)(sbyte)MathUtil.clip(sum4 + 16 - rounding >> 5, -128, 127);
				dst[dstOffset2 + 15 - l] = (byte)(sbyte)MathUtil.clip(sum6 + 16 - rounding >> 5, -128, 127);
			}
			for (int k = 0; k < 8; k++)
			{
				int sum2 = 0;
				for (int k2 = 0; k2 < 8; k2++)
				{
					sum2 += MPEG4Consts.FILTER_TAB[3][k2] * src[srcOffset2 + k2 + k + 1];
				}
				dst[dstOffset2 + k + 4] = (byte)(sbyte)MathUtil.clip(sum2 + 16 - rounding >> 5, -128, 127);
			}
			srcOffset2 += srcStride;
			dstOffset2 += 16;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 66, 156, 109, 114, 104, 114, 17, 11,
		236, 72, 150
	})]
	private static void qOff(byte[] dst, byte[] src, int x, int y, int w, int h, int height, int srcStride, int round)
	{
		if (x < 0 || y < 0 || x > w - 16 || y > h - height)
		{
			int row = 0;
			int dstOff = 0;
			for (; row < height; row++)
			{
				int o0 = MathUtil.clip(y + row, 0, h - 1) * srcStride;
				int col = 0;
				while (col < 16)
				{
					int srcOffset = o0 + MathUtil.clip(x + col, 0, w - 1);
					dst[dstOff] = (byte)(sbyte)(dst[dstOff] + src[srcOffset] + 1 >> 1);
					col++;
					dstOff++;
				}
			}
		}
		else
		{
			qOffSafe(dst, src, y * srcStride + x, height, srcStride, round);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 35, 66, 156, 99, 107, 107, 108, 100, 100,
		111, 127, 3, 159, 6, 118, 246, 59, 236, 71,
		120, 248, 53, 246, 77, 102, 108, 100, 105, 127,
		0, 105, 245, 61, 233, 69, 248, 57, 241, 74,
		229, 38, 234, 92, 99, 150
	})]
	private static void vertMiddle16(byte[] dst, byte[] src, int x, int y, int w, int h, int width, int srcStride, int rounding)
	{
		if (x < 0 || y < 0 || x > w - width || y > h - 17)
		{
			int dstOffset = 0;
			for (int col = 0; col < width; col++)
			{
				int dstStart = dstOffset;
				int dstEnd = dstOffset + 240;
				int j = 0;
				while (j < 4)
				{
					int sum2 = 0;
					int sum3 = 0;
					for (int l = 0; l < 5 + j; l++)
					{
						int ss = MathUtil.clip(y + l, 0, h - 1) * srcStride + MathUtil.clip(x + col, 0, w - 1);
						int es = MathUtil.clip(y - l + 16, 0, h - 1) * srcStride + MathUtil.clip(x + col, 0, w - 1);
						sum2 += MPEG4Consts.FILTER_TAB[j][l] * src[ss];
						sum3 += MPEG4Consts.FILTER_TAB[j][l] * src[es];
					}
					dst[dstStart] = (byte)(sbyte)MathUtil.clip(sum2 + 16 - rounding >> 5, -128, 127);
					dst[dstEnd] = (byte)(sbyte)MathUtil.clip(sum3 + 16 - rounding >> 5, -128, 127);
					j++;
					dstStart += 16;
					dstEnd += -16;
				}
				dstStart = dstOffset + 64;
				int i = 0;
				while (i < 8)
				{
					int sum = 0;
					for (int k = 0; k < 8; k++)
					{
						int srcPos = MathUtil.clip(y + i + k + 1, 0, h - 1) * srcStride + MathUtil.clip(x + col, 0, w - 1);
						sum += MPEG4Consts.FILTER_TAB[3][k] * src[srcPos];
					}
					dst[dstStart] = (byte)(sbyte)MathUtil.clip(sum + 16 - rounding >> 5, -128, 127);
					i++;
					dstStart += 16;
				}
				dstOffset++;
			}
		}
		else
		{
			vertMiddle16Safe(dst, src, y * srcStride + x, width, srcStride, rounding);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 44, 98, 99, 106, 107, 108, 100, 100, 100,
		105, 108, 118, 118, 104, 232, 60, 233, 70, 120,
		248, 52, 246, 78, 102, 103, 108, 100, 101, 105,
		53, 176, 248, 58, 248, 73, 102, 229, 36, 234,
		94
	})]
	private static void vertMiddle16Safe(byte[] dst, byte[] src, int srcOffset, int width, int srcStride, int rounding)
	{
		int dstOffset = 0;
		for (int col = 0; col < width; col++)
		{
			int dstStart = dstOffset;
			int dstEnd = dstOffset + 240;
			int j = 0;
			while (j < 4)
			{
				int sum2 = 0;
				int sum3 = 0;
				int ss = srcOffset;
				int es = srcOffset + (srcStride << 4);
				for (int l = 0; l < 5 + j; l++)
				{
					sum2 += MPEG4Consts.FILTER_TAB[j][l] * src[ss];
					sum3 += MPEG4Consts.FILTER_TAB[j][l] * src[es];
					ss += srcStride;
					es -= srcStride;
				}
				dst[dstStart] = (byte)(sbyte)MathUtil.clip(sum2 + 16 - rounding >> 5, -128, 127);
				dst[dstEnd] = (byte)(sbyte)MathUtil.clip(sum3 + 16 - rounding >> 5, -128, 127);
				j++;
				dstStart += 16;
				dstEnd += -16;
			}
			dstStart = dstOffset + 64;
			int srcCoeff0Pos = srcOffset + srcStride;
			int i = 0;
			while (i < 8)
			{
				int sum = 0;
				int srcPos = srcCoeff0Pos;
				int k = 0;
				while (k < 8)
				{
					sum += MPEG4Consts.FILTER_TAB[3][k] * src[srcPos];
					k++;
					srcPos += srcStride;
				}
				dst[dstStart] = (byte)(sbyte)MathUtil.clip(sum + 16 - rounding >> 5, -128, 127);
				i++;
				dstStart += 16;
				srcCoeff0Pos += srcStride;
			}
			srcOffset++;
			dstOffset++;
		}
	}

	[LineNumberTable(new byte[] { 159, 104, 66, 105, 104, 50, 43, 237, 69 })]
	private static void qOffSafe(byte[] dst, byte[] src, int srcOffset, int height, int srcStride, int round)
	{
		int row = 0;
		int dstOff = 0;
		while (row < height)
		{
			int col = 0;
			while (col < 16)
			{
				dst[dstOff] = (byte)(sbyte)(dst[dstOff] + src[srcOffset + col] + 1 >> 1);
				col++;
				dstOff++;
			}
			row++;
			srcOffset += srcStride;
		}
	}

	[LineNumberTable(new byte[] { 159, 99, 162, 104, 103, 50, 44, 242, 69 })]
	private static void qOff8x8Safe(byte[] dst, int dstOff, byte[] src, int srcOffset, int height, int srcStride, int round)
	{
		int row = 0;
		while (row < height)
		{
			int col = 0;
			while (col < 8)
			{
				dst[dstOff] = (byte)(sbyte)(dst[dstOff] + src[srcOffset + col] + 1 >> 1);
				col++;
				dstOff++;
			}
			row++;
			srcOffset += srcStride;
			dstOff += 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 138, 98, 158, 103, 103, 112, 112, 238, 61,
		39, 238, 72, 107, 105, 105, 46, 41, 245, 70
	})]
	public static void fulpel8x8(byte[] dst, int dstOff, int dstStride, byte[] src, int srcCol, int srcRow, int srcWidth, int srcHeight, int srcStride)
	{
		if (srcCol < 0 || srcRow < 0 || srcCol > srcWidth - 8 || srcRow > srcHeight - 8)
		{
			int k = 0;
			while (k < 8)
			{
				for (int i = 0; i < 8; i++)
				{
					int y = MathUtil.clip(srcRow + k, 0, srcHeight - 1);
					int x = MathUtil.clip(srcCol + i, 0, srcWidth - 1);
					dst[dstOff + i] = src[srcStride * y + x];
				}
				k++;
				dstOff += dstStride;
			}
			return;
		}
		int srcOffset = srcRow * srcStride + srcCol;
		int l = 0;
		while (l < 8)
		{
			for (int j = 0; j < 8; j++)
			{
				dst[dstOff + j] = src[srcOffset + j];
			}
			l++;
			dstOff += dstStride;
			srcOffset += srcStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 69, 130, 158, 107, 106, 99, 99, 116, 110,
		113, 115, 118, 246, 60, 236, 70, 121, 251, 53,
		234, 78, 231, 49, 239, 83, 106, 109, 108, 100,
		100, 108, 121, 27, 201, 123, 253, 56, 236, 75,
		104, 231, 51, 236, 80
	})]
	private static void horzMiddle8(byte[] dst, int dstOffset, byte[] src, int x, int y, int w, int h, int height, int srcStride, int rounding)
	{
		if (x < 0 || y < 0 || x > w - 9 || y > h - height)
		{
			for (int row = 0; row < height; row++)
			{
				for (int i = 0; i < 4; i++)
				{
					int sum0 = 0;
					int sum2 = 0;
					int o0 = MathUtil.clip(y + row, 0, h - 1) * srcStride;
					for (int k = 0; k < 5 + i; k++)
					{
						int o1 = MathUtil.clip(x + k, 0, w - 1);
						int o2 = MathUtil.clip(x + 8 - k, 0, w - 1);
						sum0 += MPEG4Consts.FILTER_TAB[i][k] * src[o0 + o1];
						sum2 += MPEG4Consts.FILTER_TAB[i][k] * src[o0 + o2];
					}
					dst[dstOffset + i] = (byte)(sbyte)MathUtil.clip(sum0 + 16 - rounding >> 5, -128, 127);
					dst[dstOffset + 7 - i] = (byte)(sbyte)MathUtil.clip(sum2 + 16 - rounding >> 5, -128, 127);
				}
				dstOffset += 16;
			}
			return;
		}
		int srcOffset = y * srcStride + x;
		for (int row2 = 0; row2 < height; row2++)
		{
			for (int j = 0; j < 4; j++)
			{
				int sum = 0;
				int sum3 = 0;
				for (int l = 0; l < 5 + j; l++)
				{
					sum += MPEG4Consts.FILTER_TAB[j][l] * src[srcOffset + l];
					sum3 += MPEG4Consts.FILTER_TAB[j][l] * src[srcOffset + 8 - l];
				}
				dst[dstOffset + j] = (byte)(sbyte)MathUtil.clip(sum + 16 - rounding >> 5, -128, 127);
				dst[dstOffset + 7 - j] = (byte)(sbyte)MathUtil.clip(sum3 + 16 - rounding >> 5, -128, 127);
			}
			srcOffset += srcStride;
			dstOffset += 16;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 96, 66, 157, 107, 115, 103, 113, 16, 12,
		241, 72, 152
	})]
	private static void qOff8x8(byte[] dst, int dstOff, byte[] src, int x, int y, int w, int h, int height, int srcStride, int round)
	{
		if (x < 0 || y < 0 || x > w - 8 || y > h - height)
		{
			int row = 0;
			while (row < height)
			{
				int o0 = MathUtil.clip(y + row, 0, h - 1) * srcStride;
				int col = 0;
				while (col < 8)
				{
					int srcOffset = o0 + MathUtil.clip(x + col, 0, w - 1);
					dst[dstOff] = (byte)(sbyte)(dst[dstOff] + src[srcOffset] + 1 >> 1);
					col++;
					dstOff++;
				}
				row++;
				dstOff += 8;
			}
		}
		else
		{
			qOff8x8Safe(dst, dstOff, src, y * srcStride + x, height, srcStride, round);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 20, 98, 158, 107, 106, 99, 99, 110, 127,
		4, 127, 6, 115, 243, 60, 236, 70, 124, 254,
		54, 234, 77, 230, 50, 236, 81, 184
	})]
	private static void vertMiddle8(byte[] dst, int dstOffset, byte[] src, int x, int y, int w, int h, int width, int srcStride, int rounding)
	{
		if (x < 0 || y < 0 || x > w - width || y > h - 9)
		{
			for (int col = 0; col < width; col++)
			{
				for (int i = 0; i < 4; i++)
				{
					int sum0 = 0;
					int sum1 = 0;
					for (int j = 0; j < 5 + i; j++)
					{
						int os = MathUtil.clip(y + j, 0, h - 1) * srcStride + MathUtil.clip(x + col, 0, w - 1);
						int of = MathUtil.clip(y + 8 - j, 0, h - 1) * srcStride + MathUtil.clip(x + col, 0, w - 1);
						sum0 += MPEG4Consts.FILTER_TAB[i][j] * src[os];
						sum1 += MPEG4Consts.FILTER_TAB[i][j] * src[of];
					}
					dst[dstOffset + i * 16] = (byte)(sbyte)MathUtil.clip(sum0 + 16 - rounding >> 5, -128, 127);
					dst[dstOffset + (7 - i) * 16] = (byte)(sbyte)MathUtil.clip(sum1 + 16 - rounding >> 5, -128, 127);
				}
				dstOffset++;
			}
		}
		else
		{
			vertMiddle8Safe(dst, dstOffset, src, y * srcStride + x, width, srcStride, rounding);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 26, 130, 107, 106, 99, 99, 100, 105, 107,
		115, 115, 104, 232, 60, 233, 70, 124, 254, 52,
		234, 79, 102, 230, 47, 234, 83
	})]
	private static void vertMiddle8Safe(byte[] dst, int dstOffset, byte[] src, int srcOffset, int width, int srcStride, int rounding)
	{
		for (int col = 0; col < width; col++)
		{
			for (int i = 0; i < 4; i++)
			{
				int sum0 = 0;
				int sum1 = 0;
				int os = srcOffset;
				int of = srcOffset + (srcStride << 3);
				for (int j = 0; j < 5 + i; j++)
				{
					sum0 += MPEG4Consts.FILTER_TAB[i][j] * src[os];
					sum1 += MPEG4Consts.FILTER_TAB[i][j] * src[of];
					os += srcStride;
					of -= srcStride;
				}
				dst[dstOffset + i * 16] = (byte)(sbyte)MathUtil.clip(sum0 + 16 - rounding >> 5, -128, 127);
				dst[dstOffset + (7 - i) * 16] = (byte)(sbyte)MathUtil.clip(sum1 + 16 - rounding >> 5, -128, 127);
			}
			srcOffset++;
			dstOffset++;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 1, 129, 68, 105, 159, 0, 108, 108, 127,
		5, 127, 7, 245, 61, 44, 243, 73, 107, 111,
		105, 63, 6, 41, 240, 69
	})]
	private static void interpolate8PlanarVer(byte[] dst, int dstOff, int dstStride, byte[] src, int x, int y, int w, int h, int stride, bool rounding)
	{
		int rnd = ((!rounding) ? 1 : 0);
		if (x < 0 || y < 0 || x > w - 8 || y > h - 9)
		{
			int k = 0;
			int d = dstOff;
			while (k < 8)
			{
				for (int i = 0; i < 8; i++)
				{
					int srcOffset2 = MathUtil.clip(y + k, 0, h - 1) * stride + MathUtil.clip(x + i, 0, w - 1);
					int srcOffset3 = MathUtil.clip(y + k + 1, 0, h - 1) * stride + MathUtil.clip(x + i, 0, w - 1);
					dst[d + i] = (byte)(sbyte)(src[srcOffset2] + src[srcOffset3] + rnd >> 1);
				}
				k++;
				d += dstStride;
			}
			return;
		}
		int srcOffset = y * stride + x;
		int l = 0;
		int d2 = dstOff;
		while (l < 8 * stride)
		{
			for (int j = 0; j < 8; j++)
			{
				dst[d2 + j] = (byte)(sbyte)(src[srcOffset + l + j] + src[srcOffset + l + stride + j] + rnd >> 1);
			}
			l += stride;
			d2 += dstStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 6, 65, 68, 105, 159, 0, 108, 108, 127,
		5, 127, 7, 245, 61, 44, 243, 73, 107, 111,
		105, 63, 5, 41, 240, 69
	})]
	private static void interpolate8x8PlanarHor(byte[] dst, int dstOffset, int dstStride, byte[] src, int x, int y, int w, int h, int stride, bool rounding)
	{
		int rnd = ((!rounding) ? 1 : 0);
		if (x < 0 || y < 0 || x > w - 9 || y > h - 8)
		{
			int k = 0;
			int d = dstOffset;
			while (k < 8)
			{
				for (int i = 0; i < 8; i++)
				{
					int srcOffset2 = MathUtil.clip(y + k, 0, h - 1) * stride + MathUtil.clip(x + i, 0, w - 1);
					int srcOffset3 = MathUtil.clip(y + k, 0, h - 1) * stride + MathUtil.clip(x + i + 1, 0, w - 1);
					dst[d + i] = (byte)(sbyte)(src[srcOffset2] + src[srcOffset3] + rnd >> 1);
				}
				k++;
				d += dstStride;
			}
			return;
		}
		int srcOffset = y * stride + x;
		int l = 0;
		int d2 = dstOffset;
		while (l < 8 * stride)
		{
			for (int j = 0; j < 8; j++)
			{
				dst[d2 + j] = (byte)(sbyte)(src[srcOffset + l + j] + src[srcOffset + l + j + 1] + rnd >> 1);
			}
			l += stride;
			d2 += dstStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		158,
		251,
		65,
		68,
		105,
		159,
		1,
		108,
		108,
		127,
		5,
		127,
		7,
		127,
		7,
		127,
		9,
		byte.MaxValue,
		0,
		59,
		44,
		243,
		77,
		107,
		114,
		105,
		63,
		35,
		41,
		243,
		71
	})]
	private static void interpolate8x8PlanarBoth(byte[] dst, int dstOff, int dstStride, byte[] src, int x, int y, int w, int h, int stride, bool rounding)
	{
		int rnd = (rounding ? 1 : 2);
		if (x < 0 || y < 0 || x > w - 9 || y > h - 9)
		{
			int k = 0;
			int d = dstOff;
			while (k < 8)
			{
				for (int i = 0; i < 8; i++)
				{
					int srcOffset2 = MathUtil.clip(y + k, 0, h - 1) * stride + MathUtil.clip(x + i, 0, w - 1);
					int srcOffset3 = MathUtil.clip(y + k, 0, h - 1) * stride + MathUtil.clip(x + i + 1, 0, w - 1);
					int srcOffset4 = MathUtil.clip(y + k + 1, 0, h - 1) * stride + MathUtil.clip(x + i, 0, w - 1);
					int srcOffset5 = MathUtil.clip(y + k + 1, 0, h - 1) * stride + MathUtil.clip(x + i + 1, 0, w - 1);
					dst[d + i] = (byte)(sbyte)(src[srcOffset2] + src[srcOffset3] + src[srcOffset4] + src[srcOffset5] + rnd >> 2);
				}
				k++;
				d += dstStride;
			}
			return;
		}
		int srcOffset = y * stride + x;
		int l = 0;
		int d2 = dstOff;
		while (l < 8 * stride)
		{
			for (int j = 0; j < 8; j++)
			{
				dst[d2 + j] = (byte)(sbyte)(src[srcOffset + l + j] + src[srcOffset + l + j + 1] + src[srcOffset + l + stride + j] + src[srcOffset + l + stride + j + 1] + rnd >> 2);
			}
			l += stride;
			d2 += dstStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public MPEG4Interpolator()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 14, 161, 68, 120, 122, 126, 127, 3 })]
	public static void interpolate16x16Planar(byte[] dst, byte[] refn, int x, int y, int w, int h, int dx, int dy, int stride, bool rounding)
	{
		interpolate8x8Planar(dst, 0, 16, refn, x, y, w, h, dx, dy, stride, rounding);
		interpolate8x8Planar(dst, 8, 16, refn, x + 8, y, w, h, dx, dy, stride, rounding);
		interpolate8x8Planar(dst, 128, 16, refn, x, y + 8, w, h, dx, dy, stride, rounding);
		interpolate8x8Planar(dst, 136, 16, refn, x + 8, y + 8, w, h, dx, dy, stride, rounding);
	}

	[LineNumberTable(13)]
	static MPEG4Interpolator()
	{
		qpi = new byte[272];
	}
}
