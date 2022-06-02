using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.decode;

public class Intra4x4PredictionBuilder : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 66, 105, 102, 99, 106, 118, 124, 124,
		124, 101, 230, 58, 234, 72
	})]
	public static void predictVertical(int[] residual, bool topAvailable, byte[] topLine, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		int pixOff = (blkY << 4) + blkX;
		int toff = mbOffX + blkX;
		int rOff = 0;
		for (int i = 0; i < 4; i++)
		{
			pixOut[pixOff] = (byte)(sbyte)MathUtil.clip(residual[rOff] + topLine[toff], -128, 127);
			pixOut[pixOff + 1] = (byte)(sbyte)MathUtil.clip(residual[rOff + 1] + topLine[toff + 1], -128, 127);
			pixOut[pixOff + 2] = (byte)(sbyte)MathUtil.clip(residual[rOff + 2] + topLine[toff + 2], -128, 127);
			pixOut[pixOff + 3] = (byte)(sbyte)MathUtil.clip(residual[rOff + 3] + topLine[toff + 3], -128, 127);
			rOff += 4;
			pixOff += 16;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 120, 66, 105, 99, 106, 104, 116, 120, 120,
		120, 101, 230, 57, 234, 73
	})]
	public static void predictHorizontal(int[] residual, bool leftAvailable, byte[] leftRow, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		int pixOff = (blkY << 4) + blkX;
		int rOff = 0;
		for (int i = 0; i < 4; i++)
		{
			int j = leftRow[blkY + i];
			pixOut[pixOff] = (byte)(sbyte)MathUtil.clip(residual[rOff] + j, -128, 127);
			pixOut[pixOff + 1] = (byte)(sbyte)MathUtil.clip(residual[rOff + 1] + j, -128, 127);
			pixOut[pixOff + 2] = (byte)(sbyte)MathUtil.clip(residual[rOff + 2] + j, -128, 127);
			pixOut[pixOff + 3] = (byte)(sbyte)MathUtil.clip(residual[rOff + 3] + j, -128, 127);
			rOff += 4;
			pixOff += 16;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 97, 69, 103, 159, 44, 100, 127, 2,
		100, 191, 18, 163, 105, 100, 108, 117, 121, 121,
		121, 102, 231, 58, 236, 72
	})]
	public static void predictDC(int[] residual, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		int val = ((leftAvailable && topAvailable) ? (leftRow[blkY] + leftRow[blkY + 1] + leftRow[blkY + 2] + leftRow[blkY + 3] + topLine[mbOffX + blkX] + topLine[mbOffX + blkX + 1] + topLine[mbOffX + blkX + 2] + topLine[mbOffX + blkX + 3] + 4 >> 3) : (leftAvailable ? (leftRow[blkY] + leftRow[blkY + 1] + leftRow[blkY + 2] + leftRow[blkY + 3] + 2 >> 2) : (topAvailable ? (topLine[mbOffX + blkX] + topLine[mbOffX + blkX + 1] + topLine[mbOffX + blkX + 2] + topLine[mbOffX + blkX + 3] + 2 >> 2) : 0)));
		int pixOff = (blkY << 4) + blkX;
		int rOff = 0;
		for (int i = 0; i < 4; i++)
		{
			pixOut[pixOff] = (byte)(sbyte)MathUtil.clip(residual[rOff] + val, -128, 127);
			pixOut[pixOff + 1] = (byte)(sbyte)MathUtil.clip(residual[rOff + 1] + val, -128, 127);
			pixOut[pixOff + 2] = (byte)(sbyte)MathUtil.clip(residual[rOff + 2] + val, -128, 127);
			pixOut[pixOff + 3] = (byte)(sbyte)MathUtil.clip(residual[rOff + 3] + val, -128, 127);
			pixOff += 16;
			rOff += 4;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 109, 65, 67, 103, 123, 100, 103, 103, 104,
		168, 120, 122, 118, 114, 111, 112, 142, 106, 118,
		120, 120, 152, 121, 121, 121, 153, 121, 122, 122,
		154, 122, 122, 122, 122
	})]
	public static void predictDiagonalDownLeft(int[] residual, bool topAvailable, bool topRightAvailable, byte[] topLine, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		int to = mbOffX + blkX;
		int tr0 = topLine[to + 3];
		int tr1 = topLine[to + 3];
		int tr2 = topLine[to + 3];
		int tr3 = topLine[to + 3];
		if (topRightAvailable)
		{
			tr0 = topLine[to + 4];
			tr1 = topLine[to + 5];
			tr2 = topLine[to + 6];
			tr3 = topLine[to + 7];
		}
		int c0 = topLine[to] + topLine[to + 2] + (topLine[to + 1] << 1) + 2 >> 2;
		int c1 = topLine[to + 1] + topLine[to + 3] + (topLine[to + 2] << 1) + 2 >> 2;
		int c2 = topLine[to + 2] + tr0 + (topLine[to + 3] << 1) + 2 >> 2;
		int c3 = topLine[to + 3] + tr1 + (tr0 << 1) + 2 >> 2;
		int c4 = tr0 + tr2 + (tr1 << 1) + 2 >> 2;
		int c5 = tr1 + tr3 + (tr2 << 1) + 2 >> 2;
		int c6 = tr2 + 3 * tr3 + 2 >> 2;
		int off = (blkY << 4) + blkX;
		pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[0] + c0, -128, 127);
		pixOut[off + 1] = (byte)(sbyte)MathUtil.clip(residual[1] + c1, -128, 127);
		pixOut[off + 2] = (byte)(sbyte)MathUtil.clip(residual[2] + c2, -128, 127);
		pixOut[off + 3] = (byte)(sbyte)MathUtil.clip(residual[3] + c3, -128, 127);
		pixOut[off + 16] = (byte)(sbyte)MathUtil.clip(residual[4] + c1, -128, 127);
		pixOut[off + 17] = (byte)(sbyte)MathUtil.clip(residual[5] + c2, -128, 127);
		pixOut[off + 18] = (byte)(sbyte)MathUtil.clip(residual[6] + c3, -128, 127);
		pixOut[off + 19] = (byte)(sbyte)MathUtil.clip(residual[7] + c4, -128, 127);
		pixOut[off + 32] = (byte)(sbyte)MathUtil.clip(residual[8] + c2, -128, 127);
		pixOut[off + 33] = (byte)(sbyte)MathUtil.clip(residual[9] + c3, -128, 127);
		pixOut[off + 34] = (byte)(sbyte)MathUtil.clip(residual[10] + c4, -128, 127);
		pixOut[off + 35] = (byte)(sbyte)MathUtil.clip(residual[11] + c5, -128, 127);
		pixOut[off + 48] = (byte)(sbyte)MathUtil.clip(residual[12] + c3, -128, 127);
		pixOut[off + 49] = (byte)(sbyte)MathUtil.clip(residual[13] + c4, -128, 127);
		pixOut[off + 50] = (byte)(sbyte)MathUtil.clip(residual[14] + c5, -128, 127);
		pixOut[off + 51] = (byte)(sbyte)MathUtil.clip(residual[15] + c6, -128, 127);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 99, 130, 105, 157, 127, 6, 127, 7, 159,
		10, 116, 118, 118, 151, 124, 127, 5, 159, 8,
		120, 119, 120, 152, 125, 124, 159, 5, 120, 121,
		120, 153, 125, 123, 156, 121, 121, 121, 120
	})]
	public static void predictDiagonalDownRight(int[] residual, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, byte[] topLeft, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		int off = (blkY << 4) + blkX;
		int c0 = topLine[mbOffX + blkX] + 2 * topLeft[blkY >> 2] + leftRow[blkY] + 2 >> 2;
		int c1 = topLeft[blkY >> 2] + (topLine[mbOffX + blkX + 0] << 1) + topLine[mbOffX + blkX + 1] + 2 >> 2;
		int c6 = topLine[mbOffX + blkX] + (topLine[mbOffX + blkX + 1] << 1) + topLine[mbOffX + blkX + 2] + 2 >> 2;
		int c7 = topLine[mbOffX + blkX + 1] + (topLine[mbOffX + blkX + 2] << 1) + topLine[mbOffX + blkX + 3] + 2 >> 2;
		pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[0] + c0, -128, 127);
		pixOut[off + 1] = (byte)(sbyte)MathUtil.clip(residual[1] + c1, -128, 127);
		pixOut[off + 2] = (byte)(sbyte)MathUtil.clip(residual[2] + c6, -128, 127);
		pixOut[off + 3] = (byte)(sbyte)MathUtil.clip(residual[3] + c7, -128, 127);
		int c8 = topLeft[blkY >> 2] + (leftRow[blkY] << 1) + leftRow[blkY + 1] + 2 >> 2;
		int c9 = topLeft[blkY >> 2] + (topLine[mbOffX + blkX] << 1) + topLine[mbOffX + blkX + 1] + 2 >> 2;
		int c10 = topLine[mbOffX + blkX] + (topLine[mbOffX + blkX + 1] << 1) + topLine[mbOffX + blkX + 2] + 2 >> 2;
		pixOut[off + 16] = (byte)(sbyte)MathUtil.clip(residual[4] + c8, -128, 127);
		pixOut[off + 17] = (byte)(sbyte)MathUtil.clip(residual[5] + c0, -128, 127);
		pixOut[off + 18] = (byte)(sbyte)MathUtil.clip(residual[6] + c9, -128, 127);
		pixOut[off + 19] = (byte)(sbyte)MathUtil.clip(residual[7] + c10, -128, 127);
		int c11 = leftRow[blkY + 0] + (leftRow[blkY + 1] << 1) + leftRow[blkY + 2] + 2 >> 2;
		int c12 = topLeft[blkY >> 2] + (leftRow[blkY] << 1) + leftRow[blkY + 1] + 2 >> 2;
		int c2 = topLeft[blkY >> 2] + (topLine[mbOffX + blkX] << 1) + topLine[mbOffX + blkX + 1] + 2 >> 2;
		pixOut[off + 32] = (byte)(sbyte)MathUtil.clip(residual[8] + c11, -128, 127);
		pixOut[off + 33] = (byte)(sbyte)MathUtil.clip(residual[9] + c12, -128, 127);
		pixOut[off + 34] = (byte)(sbyte)MathUtil.clip(residual[10] + c0, -128, 127);
		pixOut[off + 35] = (byte)(sbyte)MathUtil.clip(residual[11] + c2, -128, 127);
		int c3 = leftRow[blkY + 1] + (leftRow[blkY + 2] << 1) + leftRow[blkY + 3] + 2 >> 2;
		int c4 = leftRow[blkY] + (leftRow[blkY + 1] << 1) + leftRow[blkY + 2] + 2 >> 2;
		int c5 = topLeft[blkY >> 2] + (leftRow[blkY] << 1) + leftRow[blkY + 1] + 2 >> 2;
		pixOut[off + 48] = (byte)(sbyte)MathUtil.clip(residual[12] + c3, -128, 127);
		pixOut[off + 49] = (byte)(sbyte)MathUtil.clip(residual[13] + c4, -128, 127);
		pixOut[off + 50] = (byte)(sbyte)MathUtil.clip(residual[14] + c5, -128, 127);
		pixOut[off + 51] = (byte)(sbyte)MathUtil.clip(residual[15] + c0, -128, 127);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 88, 98, 120, 123, 123, 123, 127, 1, 127,
		7, 127, 10, 127, 10, 124, 155, 106, 117, 119,
		119, 119, 121, 121, 121, 121, 121, 121, 121, 121,
		122, 122, 122, 122
	})]
	public static void predictVerticalRight(int[] residual, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, byte[] topLeft, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		int v1 = topLeft[blkY >> 2] + topLine[mbOffX + blkX + 0] + 1 >> 1;
		int v3 = topLine[mbOffX + blkX + 0] + topLine[mbOffX + blkX + 1] + 1 >> 1;
		int v4 = topLine[mbOffX + blkX + 1] + topLine[mbOffX + blkX + 2] + 1 >> 1;
		int v5 = topLine[mbOffX + blkX + 2] + topLine[mbOffX + blkX + 3] + 1 >> 1;
		int v6 = leftRow[blkY] + 2 * topLeft[blkY >> 2] + topLine[mbOffX + blkX + 0] + 2 >> 2;
		int v7 = topLeft[blkY >> 2] + 2 * topLine[mbOffX + blkX + 0] + topLine[mbOffX + blkX + 1] + 2 >> 2;
		int v8 = topLine[mbOffX + blkX + 0] + 2 * topLine[mbOffX + blkX + 1] + topLine[mbOffX + blkX + 2] + 2 >> 2;
		int v9 = topLine[mbOffX + blkX + 1] + 2 * topLine[mbOffX + blkX + 2] + topLine[mbOffX + blkX + 3] + 2 >> 2;
		int v10 = topLeft[blkY >> 2] + 2 * leftRow[blkY] + leftRow[blkY + 1] + 2 >> 2;
		int v2 = leftRow[blkY] + 2 * leftRow[blkY + 1] + leftRow[blkY + 2] + 2 >> 2;
		int off = (blkY << 4) + blkX;
		pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[0] + v1, -128, 127);
		pixOut[off + 1] = (byte)(sbyte)MathUtil.clip(residual[1] + v3, -128, 127);
		pixOut[off + 2] = (byte)(sbyte)MathUtil.clip(residual[2] + v4, -128, 127);
		pixOut[off + 3] = (byte)(sbyte)MathUtil.clip(residual[3] + v5, -128, 127);
		pixOut[off + 16] = (byte)(sbyte)MathUtil.clip(residual[4] + v6, -128, 127);
		pixOut[off + 17] = (byte)(sbyte)MathUtil.clip(residual[5] + v7, -128, 127);
		pixOut[off + 18] = (byte)(sbyte)MathUtil.clip(residual[6] + v8, -128, 127);
		pixOut[off + 19] = (byte)(sbyte)MathUtil.clip(residual[7] + v9, -128, 127);
		pixOut[off + 32] = (byte)(sbyte)MathUtil.clip(residual[8] + v10, -128, 127);
		pixOut[off + 33] = (byte)(sbyte)MathUtil.clip(residual[9] + v1, -128, 127);
		pixOut[off + 34] = (byte)(sbyte)MathUtil.clip(residual[10] + v3, -128, 127);
		pixOut[off + 35] = (byte)(sbyte)MathUtil.clip(residual[11] + v4, -128, 127);
		pixOut[off + 48] = (byte)(sbyte)MathUtil.clip(residual[12] + v2, -128, 127);
		pixOut[off + 49] = (byte)(sbyte)MathUtil.clip(residual[13] + v6, -128, 127);
		pixOut[off + 50] = (byte)(sbyte)MathUtil.clip(residual[14] + v7, -128, 127);
		pixOut[off + 51] = (byte)(sbyte)MathUtil.clip(residual[15] + v8, -128, 127);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 80, 130, 114, 127, 0, 127, 6, 127, 9,
		114, 124, 116, 123, 116, 157, 106, 117, 119, 119,
		119, 121, 121, 120, 120, 121, 122, 122, 122, 122,
		122, 122, 122
	})]
	public static void predictHorizontalDown(int[] residual, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, byte[] topLeft, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		int c0 = topLeft[blkY >> 2] + leftRow[blkY] + 1 >> 1;
		int c1 = leftRow[blkY] + 2 * topLeft[blkY >> 2] + topLine[mbOffX + blkX + 0] + 2 >> 2;
		int c2 = topLeft[blkY >> 2] + 2 * topLine[mbOffX + blkX + 0] + topLine[mbOffX + blkX + 1] + 2 >> 2;
		int c3 = topLine[mbOffX + blkX + 0] + 2 * topLine[mbOffX + blkX + 1] + topLine[mbOffX + blkX + 2] + 2 >> 2;
		int c4 = leftRow[blkY] + leftRow[blkY + 1] + 1 >> 1;
		int c5 = topLeft[blkY >> 2] + 2 * leftRow[blkY] + leftRow[blkY + 1] + 2 >> 2;
		int c6 = leftRow[blkY + 1] + leftRow[blkY + 2] + 1 >> 1;
		int c7 = leftRow[blkY] + 2 * leftRow[blkY + 1] + leftRow[blkY + 2] + 2 >> 2;
		int c8 = leftRow[blkY + 2] + leftRow[blkY + 3] + 1 >> 1;
		int c9 = leftRow[blkY + 1] + 2 * leftRow[blkY + 2] + leftRow[blkY + 3] + 2 >> 2;
		int off = (blkY << 4) + blkX;
		pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[0] + c0, -128, 127);
		pixOut[off + 1] = (byte)(sbyte)MathUtil.clip(residual[1] + c1, -128, 127);
		pixOut[off + 2] = (byte)(sbyte)MathUtil.clip(residual[2] + c2, -128, 127);
		pixOut[off + 3] = (byte)(sbyte)MathUtil.clip(residual[3] + c3, -128, 127);
		pixOut[off + 16] = (byte)(sbyte)MathUtil.clip(residual[4] + c4, -128, 127);
		pixOut[off + 17] = (byte)(sbyte)MathUtil.clip(residual[5] + c5, -128, 127);
		pixOut[off + 18] = (byte)(sbyte)MathUtil.clip(residual[6] + c0, -128, 127);
		pixOut[off + 19] = (byte)(sbyte)MathUtil.clip(residual[7] + c1, -128, 127);
		pixOut[off + 32] = (byte)(sbyte)MathUtil.clip(residual[8] + c6, -128, 127);
		pixOut[off + 33] = (byte)(sbyte)MathUtil.clip(residual[9] + c7, -128, 127);
		pixOut[off + 34] = (byte)(sbyte)MathUtil.clip(residual[10] + c4, -128, 127);
		pixOut[off + 35] = (byte)(sbyte)MathUtil.clip(residual[11] + c5, -128, 127);
		pixOut[off + 48] = (byte)(sbyte)MathUtil.clip(residual[12] + c8, -128, 127);
		pixOut[off + 49] = (byte)(sbyte)MathUtil.clip(residual[13] + c9, -128, 127);
		pixOut[off + 50] = (byte)(sbyte)MathUtil.clip(residual[14] + c6, -128, 127);
		pixOut[off + 51] = (byte)(sbyte)MathUtil.clip(residual[15] + c7, -128, 127);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 72, 161, 67, 103, 116, 100, 103, 103, 168,
		112, 114, 114, 110, 106, 120, 122, 118, 114, 143,
		106, 118, 120, 120, 152, 121, 121, 121, 153, 121,
		122, 122, 154, 122, 122, 122, 122
	})]
	public static void predictVerticalLeft(int[] residual, bool topAvailable, bool topRightAvailable, byte[] topLine, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		int to = mbOffX + blkX;
		int tr0 = topLine[to + 3];
		int tr1 = topLine[to + 3];
		int tr2 = topLine[to + 3];
		if (topRightAvailable)
		{
			tr0 = topLine[to + 4];
			tr1 = topLine[to + 5];
			tr2 = topLine[to + 6];
		}
		int c0 = topLine[to] + topLine[to + 1] + 1 >> 1;
		int c1 = topLine[to + 1] + topLine[to + 2] + 1 >> 1;
		int c2 = topLine[to + 2] + topLine[to + 3] + 1 >> 1;
		int c3 = topLine[to + 3] + tr0 + 1 >> 1;
		int c4 = tr0 + tr1 + 1 >> 1;
		int c5 = topLine[to] + 2 * topLine[to + 1] + topLine[to + 2] + 2 >> 2;
		int c6 = topLine[to + 1] + 2 * topLine[to + 2] + topLine[to + 3] + 2 >> 2;
		int c7 = topLine[to + 2] + 2 * topLine[to + 3] + tr0 + 2 >> 2;
		int c8 = topLine[to + 3] + 2 * tr0 + tr1 + 2 >> 2;
		int c9 = tr0 + 2 * tr1 + tr2 + 2 >> 2;
		int off = (blkY << 4) + blkX;
		pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[0] + c0, -128, 127);
		pixOut[off + 1] = (byte)(sbyte)MathUtil.clip(residual[1] + c1, -128, 127);
		pixOut[off + 2] = (byte)(sbyte)MathUtil.clip(residual[2] + c2, -128, 127);
		pixOut[off + 3] = (byte)(sbyte)MathUtil.clip(residual[3] + c3, -128, 127);
		pixOut[off + 16] = (byte)(sbyte)MathUtil.clip(residual[4] + c5, -128, 127);
		pixOut[off + 17] = (byte)(sbyte)MathUtil.clip(residual[5] + c6, -128, 127);
		pixOut[off + 18] = (byte)(sbyte)MathUtil.clip(residual[6] + c7, -128, 127);
		pixOut[off + 19] = (byte)(sbyte)MathUtil.clip(residual[7] + c8, -128, 127);
		pixOut[off + 32] = (byte)(sbyte)MathUtil.clip(residual[8] + c1, -128, 127);
		pixOut[off + 33] = (byte)(sbyte)MathUtil.clip(residual[9] + c2, -128, 127);
		pixOut[off + 34] = (byte)(sbyte)MathUtil.clip(residual[10] + c3, -128, 127);
		pixOut[off + 35] = (byte)(sbyte)MathUtil.clip(residual[11] + c4, -128, 127);
		pixOut[off + 48] = (byte)(sbyte)MathUtil.clip(residual[12] + c6, -128, 127);
		pixOut[off + 49] = (byte)(sbyte)MathUtil.clip(residual[13] + c7, -128, 127);
		pixOut[off + 50] = (byte)(sbyte)MathUtil.clip(residual[14] + c8, -128, 127);
		pixOut[off + 51] = (byte)(sbyte)MathUtil.clip(residual[15] + c9, -128, 127);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 61, 162, 113, 122, 115, 124, 116, 125, 137,
		106, 117, 119, 119, 151, 120, 120, 121, 153, 121,
		122, 122, 154, 122, 122, 122, 122
	})]
	public static void predictHorizontalUp(int[] residual, bool leftAvailable, byte[] leftRow, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		int c0 = leftRow[blkY] + leftRow[blkY + 1] + 1 >> 1;
		int c1 = leftRow[blkY] + (leftRow[blkY + 1] << 1) + leftRow[blkY + 2] + 2 >> 2;
		int c2 = leftRow[blkY + 1] + leftRow[blkY + 2] + 1 >> 1;
		int c3 = leftRow[blkY + 1] + (leftRow[blkY + 2] << 1) + leftRow[blkY + 3] + 2 >> 2;
		int c4 = leftRow[blkY + 2] + leftRow[blkY + 3] + 1 >> 1;
		int c5 = leftRow[blkY + 2] + (leftRow[blkY + 3] << 1) + leftRow[blkY + 3] + 2 >> 2;
		int c6 = leftRow[blkY + 3];
		int off = (blkY << 4) + blkX;
		pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[0] + c0, -128, 127);
		pixOut[off + 1] = (byte)(sbyte)MathUtil.clip(residual[1] + c1, -128, 127);
		pixOut[off + 2] = (byte)(sbyte)MathUtil.clip(residual[2] + c2, -128, 127);
		pixOut[off + 3] = (byte)(sbyte)MathUtil.clip(residual[3] + c3, -128, 127);
		pixOut[off + 16] = (byte)(sbyte)MathUtil.clip(residual[4] + c2, -128, 127);
		pixOut[off + 17] = (byte)(sbyte)MathUtil.clip(residual[5] + c3, -128, 127);
		pixOut[off + 18] = (byte)(sbyte)MathUtil.clip(residual[6] + c4, -128, 127);
		pixOut[off + 19] = (byte)(sbyte)MathUtil.clip(residual[7] + c5, -128, 127);
		pixOut[off + 32] = (byte)(sbyte)MathUtil.clip(residual[8] + c4, -128, 127);
		pixOut[off + 33] = (byte)(sbyte)MathUtil.clip(residual[9] + c5, -128, 127);
		pixOut[off + 34] = (byte)(sbyte)MathUtil.clip(residual[10] + c6, -128, 127);
		pixOut[off + 35] = (byte)(sbyte)MathUtil.clip(residual[11] + c6, -128, 127);
		pixOut[off + 48] = (byte)(sbyte)MathUtil.clip(residual[12] + c6, -128, 127);
		pixOut[off + 49] = (byte)(sbyte)MathUtil.clip(residual[13] + c6, -128, 127);
		pixOut[off + 50] = (byte)(sbyte)MathUtil.clip(residual[14] + c6, -128, 127);
		pixOut[off + 51] = (byte)(sbyte)MathUtil.clip(residual[15] + c6, -128, 127);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public Intra4x4PredictionBuilder()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 138, 161, 72, 159, 17, 114, 134, 114, 134,
		117, 134, 115, 134, 151, 131, 151, 131, 151, 131,
		115, 131, 210, 103, 140, 142, 107, 112, 112, 144,
		109, 106, 110, 110, 110
	})]
	public static void predictWithMode(int mode, int[] residual, bool leftAvailable, bool topAvailable, bool topRightAvailable, byte[] leftRow, byte[] topLine, byte[] topLeft, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		switch (mode)
		{
		case 0:
			predictVertical(residual, topAvailable, topLine, mbOffX, blkX, blkY, pixOut);
			break;
		case 1:
			predictHorizontal(residual, leftAvailable, leftRow, mbOffX, blkX, blkY, pixOut);
			break;
		case 2:
			predictDC(residual, leftAvailable, topAvailable, leftRow, topLine, mbOffX, blkX, blkY, pixOut);
			break;
		case 3:
			predictDiagonalDownLeft(residual, topAvailable, topRightAvailable, topLine, mbOffX, blkX, blkY, pixOut);
			break;
		case 4:
			predictDiagonalDownRight(residual, leftAvailable, topAvailable, leftRow, topLine, topLeft, mbOffX, blkX, blkY, pixOut);
			break;
		case 5:
			predictVerticalRight(residual, leftAvailable, topAvailable, leftRow, topLine, topLeft, mbOffX, blkX, blkY, pixOut);
			break;
		case 6:
			predictHorizontalDown(residual, leftAvailable, topAvailable, leftRow, topLine, topLeft, mbOffX, blkX, blkY, pixOut);
			break;
		case 7:
			predictVerticalLeft(residual, topAvailable, topRightAvailable, topLine, mbOffX, blkX, blkY, pixOut);
			break;
		case 8:
			predictHorizontalUp(residual, leftAvailable, leftRow, mbOffX, blkX, blkY, pixOut);
			break;
		}
		int oo1 = mbOffX + blkX;
		int off1 = (blkY << 4) + blkX + 3;
		topLeft[blkY >> 2] = topLine[oo1 + 3];
		leftRow[blkY] = pixOut[off1];
		leftRow[blkY + 1] = pixOut[off1 + 16];
		leftRow[blkY + 2] = pixOut[off1 + 32];
		leftRow[blkY + 3] = pixOut[off1 + 48];
		int off2 = (blkY << 4) + blkX + 48;
		topLine[oo1] = pixOut[off2];
		topLine[oo1 + 1] = pixOut[off2 + 1];
		topLine[oo1 + 2] = pixOut[off2 + 2];
		topLine[oo1 + 3] = pixOut[off2 + 3];
	}
}
