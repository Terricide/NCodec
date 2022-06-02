using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.decode;

public class Intra8x8PredictionBuilder : Object
{
	internal byte[] topBuf;

	internal byte[] leftBuf;

	internal byte[] genBuf;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 104, 161, 69, 122, 105, 99, 108, 123, 127,
		0, 127, 0, 127, 0, 127, 0, 127, 0, 127,
		0, 127, 0, 102, 229, 54, 236, 76
	})]
	public virtual void predictVertical(int[] residual, bool topLeftAvailable, bool topRightAvailable, byte[] topLeft, byte[] topLine, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		interpolateTop(topLeftAvailable, topRightAvailable, topLeft, topLine, mbOffX + blkX, blkY, topBuf);
		int pixOff = (blkY << 4) + blkX;
		int rOff = 0;
		for (int i = 0; i < 8; i++)
		{
			pixOut[pixOff] = (byte)(sbyte)MathUtil.clip(residual[rOff] + topBuf[0], -128, 127);
			pixOut[pixOff + 1] = (byte)(sbyte)MathUtil.clip(residual[rOff + 1] + topBuf[1], -128, 127);
			pixOut[pixOff + 2] = (byte)(sbyte)MathUtil.clip(residual[rOff + 2] + topBuf[2], -128, 127);
			pixOut[pixOff + 3] = (byte)(sbyte)MathUtil.clip(residual[rOff + 3] + topBuf[3], -128, 127);
			pixOut[pixOff + 4] = (byte)(sbyte)MathUtil.clip(residual[rOff + 4] + topBuf[4], -128, 127);
			pixOut[pixOff + 5] = (byte)(sbyte)MathUtil.clip(residual[rOff + 5] + topBuf[5], -128, 127);
			pixOut[pixOff + 6] = (byte)(sbyte)MathUtil.clip(residual[rOff + 6] + topBuf[6], -128, 127);
			pixOut[pixOff + 7] = (byte)(sbyte)MathUtil.clip(residual[rOff + 7] + topBuf[7], -128, 127);
			pixOff += 16;
			rOff += 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 99, 129, 67, 115, 105, 99, 106, 123, 127,
		0, 127, 0, 127, 0, 127, 0, 127, 0, 127,
		0, 127, 0, 102, 229, 54, 234, 76
	})]
	public virtual void predictHorizontal(int[] residual, bool topLeftAvailable, byte[] topLeft, byte[] leftRow, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		interpolateLeft(topLeftAvailable, topLeft, leftRow, blkY, leftBuf);
		int pixOff = (blkY << 4) + blkX;
		int rOff = 0;
		for (int i = 0; i < 8; i++)
		{
			pixOut[pixOff] = (byte)(sbyte)MathUtil.clip(residual[rOff] + leftBuf[i], -128, 127);
			pixOut[pixOff + 1] = (byte)(sbyte)MathUtil.clip(residual[rOff + 1] + leftBuf[i], -128, 127);
			pixOut[pixOff + 2] = (byte)(sbyte)MathUtil.clip(residual[rOff + 2] + leftBuf[i], -128, 127);
			pixOut[pixOff + 3] = (byte)(sbyte)MathUtil.clip(residual[rOff + 3] + leftBuf[i], -128, 127);
			pixOut[pixOff + 4] = (byte)(sbyte)MathUtil.clip(residual[rOff + 4] + leftBuf[i], -128, 127);
			pixOut[pixOff + 5] = (byte)(sbyte)MathUtil.clip(residual[rOff + 5] + leftBuf[i], -128, 127);
			pixOut[pixOff + 6] = (byte)(sbyte)MathUtil.clip(residual[rOff + 6] + leftBuf[i], -128, 127);
			pixOut[pixOff + 7] = (byte)(sbyte)MathUtil.clip(residual[rOff + 7] + leftBuf[i], -128, 127);
			pixOff += 16;
			rOff += 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 94, 129, 75, 109, 122, 116, 127, 7, 127,
		7, 127, 7, 127, 7, 127, 1, 108, 116, 127,
		7, 127, 7, 122, 108, 122, 127, 7, 127, 7,
		122, 99, 148
	})]
	public virtual void predictDC(int[] residual, bool topLeftAvailable, bool topRightAvailable, bool leftAvailable, bool topAvailable, byte[] topLeft, byte[] leftRow, byte[] topLine, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		if (topAvailable && leftAvailable)
		{
			interpolateTop(topLeftAvailable, topRightAvailable, topLeft, topLine, mbOffX + blkX, blkY, topBuf);
			interpolateLeft(topLeftAvailable, topLeft, leftRow, blkY, leftBuf);
			int sum2 = topBuf[0] + topBuf[1] + topBuf[2] + topBuf[3];
			int sum4 = topBuf[4] + topBuf[5] + topBuf[6] + topBuf[7];
			int sum6 = leftBuf[0] + leftBuf[1] + leftBuf[2] + leftBuf[3];
			int sum8 = leftBuf[4] + leftBuf[5] + leftBuf[6] + leftBuf[7];
			fillAdd(residual, (blkY << 4) + blkX, sum2 + sum4 + sum6 + sum8 + 8 >> 4, pixOut);
		}
		else if (leftAvailable)
		{
			interpolateLeft(topLeftAvailable, topLeft, leftRow, blkY, leftBuf);
			int sum5 = leftBuf[0] + leftBuf[1] + leftBuf[2] + leftBuf[3];
			int sum7 = leftBuf[4] + leftBuf[5] + leftBuf[6] + leftBuf[7];
			fillAdd(residual, (blkY << 4) + blkX, sum5 + sum7 + 4 >> 3, pixOut);
		}
		else if (topAvailable)
		{
			interpolateTop(topLeftAvailable, topRightAvailable, topLeft, topLine, mbOffX + blkX, blkY, topBuf);
			int sum1 = topBuf[0] + topBuf[1] + topBuf[2] + topBuf[3];
			int sum3 = topBuf[4] + topBuf[5] + topBuf[6] + topBuf[7];
			fillAdd(residual, (blkY << 4) + blkX, sum1 + sum3 + 4 >> 3, pixOut);
		}
		else
		{
			fillAdd(residual, (blkY << 4) + blkX, 0, pixOut);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 88, 161, 70, 154, 127, 11, 127, 11, 127,
		11, 127, 11, 127, 11, 127, 11, 127, 11, 127,
		12, 127, 13, 127, 15, 127, 15, 127, 15, 127,
		15, 127, 15, 159, 15, 105, 115, 118, 119, 119,
		119, 119, 119, 121
	})]
	public virtual void predictDiagonalDownLeft(int[] residual, bool topLeftAvailable, bool topAvailable, bool topRightAvailable, byte[] topLeft, byte[] topLine, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		interpolateTop(topLeftAvailable, topRightAvailable, topLeft, topLine, mbOffX + blkX, blkY, topBuf);
		genBuf[0] = (byte)(sbyte)(topBuf[0] + topBuf[2] + (topBuf[1] << 1) + 2 >> 2);
		genBuf[1] = (byte)(sbyte)(topBuf[1] + topBuf[3] + (topBuf[2] << 1) + 2 >> 2);
		genBuf[2] = (byte)(sbyte)(topBuf[2] + topBuf[4] + (topBuf[3] << 1) + 2 >> 2);
		genBuf[3] = (byte)(sbyte)(topBuf[3] + topBuf[5] + (topBuf[4] << 1) + 2 >> 2);
		genBuf[4] = (byte)(sbyte)(topBuf[4] + topBuf[6] + (topBuf[5] << 1) + 2 >> 2);
		genBuf[5] = (byte)(sbyte)(topBuf[5] + topBuf[7] + (topBuf[6] << 1) + 2 >> 2);
		genBuf[6] = (byte)(sbyte)(topBuf[6] + topBuf[8] + (topBuf[7] << 1) + 2 >> 2);
		genBuf[7] = (byte)(sbyte)(topBuf[7] + topBuf[9] + (topBuf[8] << 1) + 2 >> 2);
		genBuf[8] = (byte)(sbyte)(topBuf[8] + topBuf[10] + (topBuf[9] << 1) + 2 >> 2);
		genBuf[9] = (byte)(sbyte)(topBuf[9] + topBuf[11] + (topBuf[10] << 1) + 2 >> 2);
		genBuf[10] = (byte)(sbyte)(topBuf[10] + topBuf[12] + (topBuf[11] << 1) + 2 >> 2);
		genBuf[11] = (byte)(sbyte)(topBuf[11] + topBuf[13] + (topBuf[12] << 1) + 2 >> 2);
		genBuf[12] = (byte)(sbyte)(topBuf[12] + topBuf[14] + (topBuf[13] << 1) + 2 >> 2);
		genBuf[13] = (byte)(sbyte)(topBuf[13] + topBuf[15] + (topBuf[14] << 1) + 2 >> 2);
		genBuf[14] = (byte)(sbyte)(topBuf[14] + topBuf[15] + (topBuf[15] << 1) + 2 >> 2);
		int off = (blkY << 4) + blkX;
		copyAdd(genBuf, 0, residual, off, 0, pixOut);
		copyAdd(genBuf, 1, residual, off + 16, 8, pixOut);
		copyAdd(genBuf, 2, residual, off + 32, 16, pixOut);
		copyAdd(genBuf, 3, residual, off + 48, 24, pixOut);
		copyAdd(genBuf, 4, residual, off + 64, 32, pixOut);
		copyAdd(genBuf, 5, residual, off + 80, 40, pixOut);
		copyAdd(genBuf, 6, residual, off + 96, 48, pixOut);
		copyAdd(genBuf, 7, residual, off + 112, 56, pixOut);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 80, 129, 67, 121, 115, 149, 127, 11, 127,
		11, 127, 11, 127, 11, 127, 11, 127, 11, 127,
		4, 127, 4, 127, 4, 127, 12, 127, 12, 127,
		12, 127, 12, 127, 12, 159, 12, 105, 115, 118,
		119, 119, 119, 119, 119, 121
	})]
	public virtual void predictDiagonalDownRight(int[] residual, bool topRightAvailable, byte[] topLeft, byte[] leftRow, byte[] topLine, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		interpolateTop(topLeftAvailable: true, topRightAvailable, topLeft, topLine, mbOffX + blkX, blkY, topBuf);
		interpolateLeft(topLeftAvailable: true, topLeft, leftRow, blkY, leftBuf);
		int tl = interpolateTopLeft(topAvailable: true, leftAvailable: true, topLeft, topLine, leftRow, mbOffX, blkX, blkY);
		genBuf[0] = (byte)(sbyte)(leftBuf[7] + leftBuf[5] + (leftBuf[6] << 1) + 2 >> 2);
		genBuf[1] = (byte)(sbyte)(leftBuf[6] + leftBuf[4] + (leftBuf[5] << 1) + 2 >> 2);
		genBuf[2] = (byte)(sbyte)(leftBuf[5] + leftBuf[3] + (leftBuf[4] << 1) + 2 >> 2);
		genBuf[3] = (byte)(sbyte)(leftBuf[4] + leftBuf[2] + (leftBuf[3] << 1) + 2 >> 2);
		genBuf[4] = (byte)(sbyte)(leftBuf[3] + leftBuf[1] + (leftBuf[2] << 1) + 2 >> 2);
		genBuf[5] = (byte)(sbyte)(leftBuf[2] + leftBuf[0] + (leftBuf[1] << 1) + 2 >> 2);
		genBuf[6] = (byte)(sbyte)(leftBuf[1] + tl + (leftBuf[0] << 1) + 2 >> 2);
		genBuf[7] = (byte)(sbyte)(leftBuf[0] + topBuf[0] + (tl << 1) + 2 >> 2);
		genBuf[8] = (byte)(sbyte)(tl + topBuf[1] + (topBuf[0] << 1) + 2 >> 2);
		genBuf[9] = (byte)(sbyte)(topBuf[0] + topBuf[2] + (topBuf[1] << 1) + 2 >> 2);
		genBuf[10] = (byte)(sbyte)(topBuf[1] + topBuf[3] + (topBuf[2] << 1) + 2 >> 2);
		genBuf[11] = (byte)(sbyte)(topBuf[2] + topBuf[4] + (topBuf[3] << 1) + 2 >> 2);
		genBuf[12] = (byte)(sbyte)(topBuf[3] + topBuf[5] + (topBuf[4] << 1) + 2 >> 2);
		genBuf[13] = (byte)(sbyte)(topBuf[4] + topBuf[6] + (topBuf[5] << 1) + 2 >> 2);
		genBuf[14] = (byte)(sbyte)(topBuf[5] + topBuf[7] + (topBuf[6] << 1) + 2 >> 2);
		int off = (blkY << 4) + blkX;
		copyAdd(genBuf, 7, residual, off, 0, pixOut);
		copyAdd(genBuf, 6, residual, off + 16, 8, pixOut);
		copyAdd(genBuf, 5, residual, off + 32, 16, pixOut);
		copyAdd(genBuf, 4, residual, off + 48, 24, pixOut);
		copyAdd(genBuf, 3, residual, off + 64, 32, pixOut);
		copyAdd(genBuf, 2, residual, off + 80, 40, pixOut);
		copyAdd(genBuf, 1, residual, off + 96, 48, pixOut);
		copyAdd(genBuf, 0, residual, off + 112, 56, pixOut);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 72, 161, 67, 121, 115, 149, 127, 11, 127,
		11, 127, 4, 120, 127, 0, 127, 0, 127, 0,
		127, 0, 127, 0, 127, 1, 127, 1, 127, 12,
		127, 12, 127, 12, 127, 5, 127, 5, 127, 12,
		127, 12, 127, 12, 127, 12, 127, 12, 159, 12,
		105, 115, 119, 119, 120, 119, 120, 119, 122
	})]
	public virtual void predictVerticalRight(int[] residual, bool topRightAvailable, byte[] topLeft, byte[] leftRow, byte[] topLine, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		interpolateTop(topLeftAvailable: true, topRightAvailable, topLeft, topLine, mbOffX + blkX, blkY, topBuf);
		interpolateLeft(topLeftAvailable: true, topLeft, leftRow, blkY, leftBuf);
		int tl = interpolateTopLeft(topAvailable: true, leftAvailable: true, topLeft, topLine, leftRow, mbOffX, blkX, blkY);
		genBuf[0] = (byte)(sbyte)(leftBuf[5] + leftBuf[3] + (leftBuf[4] << 1) + 2 >> 2);
		genBuf[1] = (byte)(sbyte)(leftBuf[3] + leftBuf[1] + (leftBuf[2] << 1) + 2 >> 2);
		genBuf[2] = (byte)(sbyte)(leftBuf[1] + tl + (leftBuf[0] << 1) + 2 >> 2);
		genBuf[3] = (byte)(sbyte)(tl + topBuf[0] + 1 >> 1);
		genBuf[4] = (byte)(sbyte)(topBuf[0] + topBuf[1] + 1 >> 1);
		genBuf[5] = (byte)(sbyte)(topBuf[1] + topBuf[2] + 1 >> 1);
		genBuf[6] = (byte)(sbyte)(topBuf[2] + topBuf[3] + 1 >> 1);
		genBuf[7] = (byte)(sbyte)(topBuf[3] + topBuf[4] + 1 >> 1);
		genBuf[8] = (byte)(sbyte)(topBuf[4] + topBuf[5] + 1 >> 1);
		genBuf[9] = (byte)(sbyte)(topBuf[5] + topBuf[6] + 1 >> 1);
		genBuf[10] = (byte)(sbyte)(topBuf[6] + topBuf[7] + 1 >> 1);
		genBuf[11] = (byte)(sbyte)(leftBuf[6] + leftBuf[4] + (leftBuf[5] << 1) + 2 >> 2);
		genBuf[12] = (byte)(sbyte)(leftBuf[4] + leftBuf[2] + (leftBuf[3] << 1) + 2 >> 2);
		genBuf[13] = (byte)(sbyte)(leftBuf[2] + leftBuf[0] + (leftBuf[1] << 1) + 2 >> 2);
		genBuf[14] = (byte)(sbyte)(leftBuf[0] + topBuf[0] + (tl << 1) + 2 >> 2);
		genBuf[15] = (byte)(sbyte)(tl + topBuf[1] + (topBuf[0] << 1) + 2 >> 2);
		genBuf[16] = (byte)(sbyte)(topBuf[0] + topBuf[2] + (topBuf[1] << 1) + 2 >> 2);
		genBuf[17] = (byte)(sbyte)(topBuf[1] + topBuf[3] + (topBuf[2] << 1) + 2 >> 2);
		genBuf[18] = (byte)(sbyte)(topBuf[2] + topBuf[4] + (topBuf[3] << 1) + 2 >> 2);
		genBuf[19] = (byte)(sbyte)(topBuf[3] + topBuf[5] + (topBuf[4] << 1) + 2 >> 2);
		genBuf[20] = (byte)(sbyte)(topBuf[4] + topBuf[6] + (topBuf[5] << 1) + 2 >> 2);
		genBuf[21] = (byte)(sbyte)(topBuf[5] + topBuf[7] + (topBuf[6] << 1) + 2 >> 2);
		int off = (blkY << 4) + blkX;
		copyAdd(genBuf, 3, residual, off, 0, pixOut);
		copyAdd(genBuf, 14, residual, off + 16, 8, pixOut);
		copyAdd(genBuf, 2, residual, off + 32, 16, pixOut);
		copyAdd(genBuf, 13, residual, off + 48, 24, pixOut);
		copyAdd(genBuf, 1, residual, off + 64, 32, pixOut);
		copyAdd(genBuf, 12, residual, off + 80, 40, pixOut);
		copyAdd(genBuf, 0, residual, off + 96, 48, pixOut);
		copyAdd(genBuf, 11, residual, off + 112, 56, pixOut);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 62, 161, 67, 121, 115, 149, 127, 0, 127,
		11, 127, 0, 127, 11, 127, 0, 127, 11, 127,
		0, 127, 11, 127, 0, 127, 12, 127, 1, 127,
		12, 127, 1, 127, 5, 121, 127, 5, 127, 5,
		127, 12, 127, 12, 127, 12, 127, 12, 159, 12,
		105, 116, 119, 120, 119, 119, 119, 119, 121
	})]
	public virtual void predictHorizontalDown(int[] residual, bool topRightAvailable, byte[] topLeft, byte[] leftRow, byte[] topLine, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		interpolateTop(topLeftAvailable: true, topRightAvailable, topLeft, topLine, mbOffX + blkX, blkY, topBuf);
		interpolateLeft(topLeftAvailable: true, topLeft, leftRow, blkY, leftBuf);
		int tl = interpolateTopLeft(topAvailable: true, leftAvailable: true, topLeft, topLine, leftRow, mbOffX, blkX, blkY);
		genBuf[0] = (byte)(sbyte)(leftBuf[7] + leftBuf[6] + 1 >> 1);
		genBuf[1] = (byte)(sbyte)(leftBuf[5] + leftBuf[7] + (leftBuf[6] << 1) + 2 >> 2);
		genBuf[2] = (byte)(sbyte)(leftBuf[6] + leftBuf[5] + 1 >> 1);
		genBuf[3] = (byte)(sbyte)(leftBuf[4] + leftBuf[6] + (leftBuf[5] << 1) + 2 >> 2);
		genBuf[4] = (byte)(sbyte)(leftBuf[5] + leftBuf[4] + 1 >> 1);
		genBuf[5] = (byte)(sbyte)(leftBuf[3] + leftBuf[5] + (leftBuf[4] << 1) + 2 >> 2);
		genBuf[6] = (byte)(sbyte)(leftBuf[4] + leftBuf[3] + 1 >> 1);
		genBuf[7] = (byte)(sbyte)(leftBuf[2] + leftBuf[4] + (leftBuf[3] << 1) + 2 >> 2);
		genBuf[8] = (byte)(sbyte)(leftBuf[3] + leftBuf[2] + 1 >> 1);
		genBuf[9] = (byte)(sbyte)(leftBuf[1] + leftBuf[3] + (leftBuf[2] << 1) + 2 >> 2);
		genBuf[10] = (byte)(sbyte)(leftBuf[2] + leftBuf[1] + 1 >> 1);
		genBuf[11] = (byte)(sbyte)(leftBuf[0] + leftBuf[2] + (leftBuf[1] << 1) + 2 >> 2);
		genBuf[12] = (byte)(sbyte)(leftBuf[1] + leftBuf[0] + 1 >> 1);
		genBuf[13] = (byte)(sbyte)(tl + leftBuf[1] + (leftBuf[0] << 1) + 2 >> 2);
		genBuf[14] = (byte)(sbyte)(leftBuf[0] + tl + 1 >> 1);
		genBuf[15] = (byte)(sbyte)(leftBuf[0] + topBuf[0] + (tl << 1) + 2 >> 2);
		genBuf[16] = (byte)(sbyte)(tl + topBuf[1] + (topBuf[0] << 1) + 2 >> 2);
		genBuf[17] = (byte)(sbyte)(topBuf[0] + topBuf[2] + (topBuf[1] << 1) + 2 >> 2);
		genBuf[18] = (byte)(sbyte)(topBuf[1] + topBuf[3] + (topBuf[2] << 1) + 2 >> 2);
		genBuf[19] = (byte)(sbyte)(topBuf[2] + topBuf[4] + (topBuf[3] << 1) + 2 >> 2);
		genBuf[20] = (byte)(sbyte)(topBuf[3] + topBuf[5] + (topBuf[4] << 1) + 2 >> 2);
		genBuf[21] = (byte)(sbyte)(topBuf[4] + topBuf[6] + (topBuf[5] << 1) + 2 >> 2);
		int off = (blkY << 4) + blkX;
		copyAdd(genBuf, 14, residual, off, 0, pixOut);
		copyAdd(genBuf, 12, residual, off + 16, 8, pixOut);
		copyAdd(genBuf, 10, residual, off + 32, 16, pixOut);
		copyAdd(genBuf, 8, residual, off + 48, 24, pixOut);
		copyAdd(genBuf, 6, residual, off + 64, 32, pixOut);
		copyAdd(genBuf, 4, residual, off + 80, 40, pixOut);
		copyAdd(genBuf, 2, residual, off + 96, 48, pixOut);
		copyAdd(genBuf, 0, residual, off + 112, 56, pixOut);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 52, 161, 69, 154, 127, 0, 127, 0, 127,
		0, 127, 0, 127, 0, 127, 0, 127, 0, 127,
		0, 127, 1, 127, 3, 127, 3, 127, 12, 127,
		12, 127, 12, 127, 12, 127, 12, 127, 12, 127,
		12, 127, 13, 127, 14, 127, 15, 159, 15, 105,
		115, 119, 119, 120, 119, 120, 119, 122
	})]
	public virtual void predictVerticalLeft(int[] residual, bool topLeftAvailable, bool topRightAvailable, byte[] topLeft, byte[] topLine, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		interpolateTop(topLeftAvailable, topRightAvailable, topLeft, topLine, mbOffX + blkX, blkY, topBuf);
		genBuf[0] = (byte)(sbyte)(topBuf[0] + topBuf[1] + 1 >> 1);
		genBuf[1] = (byte)(sbyte)(topBuf[1] + topBuf[2] + 1 >> 1);
		genBuf[2] = (byte)(sbyte)(topBuf[2] + topBuf[3] + 1 >> 1);
		genBuf[3] = (byte)(sbyte)(topBuf[3] + topBuf[4] + 1 >> 1);
		genBuf[4] = (byte)(sbyte)(topBuf[4] + topBuf[5] + 1 >> 1);
		genBuf[5] = (byte)(sbyte)(topBuf[5] + topBuf[6] + 1 >> 1);
		genBuf[6] = (byte)(sbyte)(topBuf[6] + topBuf[7] + 1 >> 1);
		genBuf[7] = (byte)(sbyte)(topBuf[7] + topBuf[8] + 1 >> 1);
		genBuf[8] = (byte)(sbyte)(topBuf[8] + topBuf[9] + 1 >> 1);
		genBuf[9] = (byte)(sbyte)(topBuf[9] + topBuf[10] + 1 >> 1);
		genBuf[10] = (byte)(sbyte)(topBuf[10] + topBuf[11] + 1 >> 1);
		genBuf[11] = (byte)(sbyte)(topBuf[0] + topBuf[2] + (topBuf[1] << 1) + 2 >> 2);
		genBuf[12] = (byte)(sbyte)(topBuf[1] + topBuf[3] + (topBuf[2] << 1) + 2 >> 2);
		genBuf[13] = (byte)(sbyte)(topBuf[2] + topBuf[4] + (topBuf[3] << 1) + 2 >> 2);
		genBuf[14] = (byte)(sbyte)(topBuf[3] + topBuf[5] + (topBuf[4] << 1) + 2 >> 2);
		genBuf[15] = (byte)(sbyte)(topBuf[4] + topBuf[6] + (topBuf[5] << 1) + 2 >> 2);
		genBuf[16] = (byte)(sbyte)(topBuf[5] + topBuf[7] + (topBuf[6] << 1) + 2 >> 2);
		genBuf[17] = (byte)(sbyte)(topBuf[6] + topBuf[8] + (topBuf[7] << 1) + 2 >> 2);
		genBuf[18] = (byte)(sbyte)(topBuf[7] + topBuf[9] + (topBuf[8] << 1) + 2 >> 2);
		genBuf[19] = (byte)(sbyte)(topBuf[8] + topBuf[10] + (topBuf[9] << 1) + 2 >> 2);
		genBuf[20] = (byte)(sbyte)(topBuf[9] + topBuf[11] + (topBuf[10] << 1) + 2 >> 2);
		genBuf[21] = (byte)(sbyte)(topBuf[10] + topBuf[12] + (topBuf[11] << 1) + 2 >> 2);
		int off = (blkY << 4) + blkX;
		copyAdd(genBuf, 0, residual, off, 0, pixOut);
		copyAdd(genBuf, 11, residual, off + 16, 8, pixOut);
		copyAdd(genBuf, 1, residual, off + 32, 16, pixOut);
		copyAdd(genBuf, 12, residual, off + 48, 24, pixOut);
		copyAdd(genBuf, 2, residual, off + 64, 32, pixOut);
		copyAdd(genBuf, 13, residual, off + 80, 40, pixOut);
		copyAdd(genBuf, 3, residual, off + 96, 48, pixOut);
		copyAdd(genBuf, 14, residual, off + 112, 56, pixOut);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 42, 97, 67, 147, 127, 0, 127, 11, 127,
		0, 127, 11, 127, 0, 127, 11, 127, 0, 127,
		11, 127, 0, 127, 12, 127, 1, 127, 12, 127,
		1, 127, 12, 159, 99, 106, 116, 119, 120, 120,
		120, 121, 121, 123
	})]
	public virtual void predictHorizontalUp(int[] residual, bool topLeftAvailable, byte[] topLeft, byte[] leftRow, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		interpolateLeft(topLeftAvailable, topLeft, leftRow, blkY, leftBuf);
		genBuf[0] = (byte)(sbyte)(leftBuf[0] + leftBuf[1] + 1 >> 1);
		genBuf[1] = (byte)(sbyte)(leftBuf[2] + leftBuf[0] + (leftBuf[1] << 1) + 2 >> 2);
		genBuf[2] = (byte)(sbyte)(leftBuf[1] + leftBuf[2] + 1 >> 1);
		genBuf[3] = (byte)(sbyte)(leftBuf[3] + leftBuf[1] + (leftBuf[2] << 1) + 2 >> 2);
		genBuf[4] = (byte)(sbyte)(leftBuf[2] + leftBuf[3] + 1 >> 1);
		genBuf[5] = (byte)(sbyte)(leftBuf[4] + leftBuf[2] + (leftBuf[3] << 1) + 2 >> 2);
		genBuf[6] = (byte)(sbyte)(leftBuf[3] + leftBuf[4] + 1 >> 1);
		genBuf[7] = (byte)(sbyte)(leftBuf[5] + leftBuf[3] + (leftBuf[4] << 1) + 2 >> 2);
		genBuf[8] = (byte)(sbyte)(leftBuf[4] + leftBuf[5] + 1 >> 1);
		genBuf[9] = (byte)(sbyte)(leftBuf[6] + leftBuf[4] + (leftBuf[5] << 1) + 2 >> 2);
		genBuf[10] = (byte)(sbyte)(leftBuf[5] + leftBuf[6] + 1 >> 1);
		genBuf[11] = (byte)(sbyte)(leftBuf[7] + leftBuf[5] + (leftBuf[6] << 1) + 2 >> 2);
		genBuf[12] = (byte)(sbyte)(leftBuf[6] + leftBuf[7] + 1 >> 1);
		genBuf[13] = (byte)(sbyte)(leftBuf[6] + leftBuf[7] + (leftBuf[7] << 1) + 2 >> 2);
		byte[] array = genBuf;
		byte[] array2 = genBuf;
		byte[] array3 = genBuf;
		byte[] array4 = genBuf;
		byte[] array5 = genBuf;
		byte[] array6 = genBuf;
		byte[] array7 = genBuf;
		byte[] array8 = genBuf;
		int num = leftBuf[7];
		int num2 = 21;
		byte[] array9 = array8;
		int num3 = num;
		array9[num2] = (byte)num;
		num = num3;
		num2 = 20;
		array9 = array7;
		int num4 = num;
		array9[num2] = (byte)num;
		num = num4;
		num2 = 19;
		array9 = array6;
		int num5 = num;
		array9[num2] = (byte)num;
		num = num5;
		num2 = 18;
		array9 = array5;
		int num6 = num;
		array9[num2] = (byte)num;
		num = num6;
		num2 = 17;
		array9 = array4;
		int num7 = num;
		array9[num2] = (byte)num;
		num = num7;
		num2 = 16;
		array9 = array3;
		int num8 = num;
		array9[num2] = (byte)num;
		num = num8;
		num2 = 15;
		array9 = array2;
		int num9 = num;
		array9[num2] = (byte)num;
		array[14] = (byte)num9;
		int off = (blkY << 4) + blkX;
		copyAdd(genBuf, 0, residual, off, 0, pixOut);
		copyAdd(genBuf, 2, residual, off + 16, 8, pixOut);
		copyAdd(genBuf, 4, residual, off + 32, 16, pixOut);
		copyAdd(genBuf, 6, residual, off + 48, 24, pixOut);
		copyAdd(genBuf, 8, residual, off + 64, 32, pixOut);
		copyAdd(genBuf, 10, residual, off + 80, 40, pixOut);
		copyAdd(genBuf, 12, residual, off + 96, 48, pixOut);
		copyAdd(genBuf, 14, residual, off + 112, 56, pixOut);
	}

	[LineNumberTable(new byte[]
	{
		159, 120, 65, 69, 146, 155, 103, 63, 8, 167,
		100, 102, 63, 8, 135, 159, 10, 127, 4, 104,
		44, 167
	})]
	private void interpolateTop(bool topLeftAvailable, bool topRightAvailable, byte[] topLeft, byte[] topLine, int blkX, int blkY, byte[] @out)
	{
		int a = ((!topLeftAvailable) ? topLine[blkX] : topLeft[blkY >> 2]);
		@out[0] = (byte)(sbyte)(a + (topLine[blkX] << 1) + topLine[blkX + 1] + 2 >> 2);
		int i;
		for (i = 1; i < 7; i++)
		{
			@out[i] = (byte)(sbyte)(topLine[blkX + i - 1] + (topLine[blkX + i] << 1) + topLine[blkX + i + 1] + 2 >> 2);
		}
		if (topRightAvailable)
		{
			for (; i < 15; i++)
			{
				@out[i] = (byte)(sbyte)(topLine[blkX + i - 1] + (topLine[blkX + i] << 1) + topLine[blkX + i + 1] + 2 >> 2);
			}
			@out[15] = (byte)(sbyte)(topLine[blkX + 14] + (topLine[blkX + 15] << 1) + topLine[blkX + 15] + 2 >> 2);
			return;
		}
		@out[7] = (byte)(sbyte)(topLine[blkX + 6] + (topLine[blkX + 7] << 1) + topLine[blkX + 7] + 2 >> 2);
		for (i = 8; i < 16; i++)
		{
			@out[i] = topLine[blkX + 7];
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 116, 161, 67, 144, 121, 103, 63, 5, 167,
		127, 1
	})]
	private void interpolateLeft(bool topLeftAvailable, byte[] topLeft, byte[] leftRow, int blkY, byte[] @out)
	{
		int a = ((!topLeftAvailable) ? leftRow[0] : topLeft[blkY >> 2]);
		@out[0] = (byte)(sbyte)(a + (leftRow[blkY] << 1) + leftRow[blkY + 1] + 2 >> 2);
		for (int i = 1; i < 7; i++)
		{
			@out[i] = (byte)(sbyte)(leftRow[blkY + i - 1] + (leftRow[blkY + i] << 1) + leftRow[blkY + i + 1] + 2 >> 2);
		}
		@out[7] = (byte)(sbyte)(leftRow[blkY + 6] + (leftRow[blkY + 7] << 1) + leftRow[blkY + 7] + 2 >> 2);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 108, 130, 99, 106, 116, 120, 120, 120, 120,
		120, 120, 120, 103, 229, 54, 234, 76
	})]
	public virtual void fillAdd(int[] residual, int pixOff, int val, byte[] pixOut)
	{
		int rOff = 0;
		for (int i = 0; i < 8; i++)
		{
			pixOut[pixOff] = (byte)(sbyte)MathUtil.clip(residual[rOff] + val, -128, 127);
			pixOut[pixOff + 1] = (byte)(sbyte)MathUtil.clip(residual[rOff + 1] + val, -128, 127);
			pixOut[pixOff + 2] = (byte)(sbyte)MathUtil.clip(residual[rOff + 2] + val, -128, 127);
			pixOut[pixOff + 3] = (byte)(sbyte)MathUtil.clip(residual[rOff + 3] + val, -128, 127);
			pixOut[pixOff + 4] = (byte)(sbyte)MathUtil.clip(residual[rOff + 4] + val, -128, 127);
			pixOut[pixOff + 5] = (byte)(sbyte)MathUtil.clip(residual[rOff + 5] + val, -128, 127);
			pixOut[pixOff + 6] = (byte)(sbyte)MathUtil.clip(residual[rOff + 6] + val, -128, 127);
			pixOut[pixOff + 7] = (byte)(sbyte)MathUtil.clip(residual[rOff + 7] + val, -128, 127);
			pixOff += 16;
			rOff += 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 162, 120, 126, 126, 126, 126, 126, 126,
		126
	})]
	public virtual void copyAdd(byte[] pred, int srcOff, int[] residual, int pixOff, int rOff, byte[] @out)
	{
		@out[pixOff] = (byte)(sbyte)MathUtil.clip(residual[rOff] + pred[srcOff], -128, 127);
		@out[pixOff + 1] = (byte)(sbyte)MathUtil.clip(residual[rOff + 1] + pred[srcOff + 1], -128, 127);
		@out[pixOff + 2] = (byte)(sbyte)MathUtil.clip(residual[rOff + 2] + pred[srcOff + 2], -128, 127);
		@out[pixOff + 3] = (byte)(sbyte)MathUtil.clip(residual[rOff + 3] + pred[srcOff + 3], -128, 127);
		@out[pixOff + 4] = (byte)(sbyte)MathUtil.clip(residual[rOff + 4] + pred[srcOff + 4], -128, 127);
		@out[pixOff + 5] = (byte)(sbyte)MathUtil.clip(residual[rOff + 5] + pred[srcOff + 5], -128, 127);
		@out[pixOff + 6] = (byte)(sbyte)MathUtil.clip(residual[rOff + 6] + pred[srcOff + 6], -128, 127);
		@out[pixOff + 7] = (byte)(sbyte)MathUtil.clip(residual[rOff + 7] + pred[srcOff + 7], -128, 127);
	}

	[LineNumberTable(new byte[] { 159, 113, 129, 69, 104, 112, 110, 134 })]
	private int interpolateTopLeft(bool topAvailable, bool leftAvailable, byte[] topLeft, byte[] topLine, byte[] leftRow, int mbOffX, int blkX, int blkY)
	{
		int a = topLeft[blkY >> 2];
		int b = ((!topAvailable) ? a : topLine[mbOffX + blkX]);
		int c = ((!leftAvailable) ? a : leftRow[blkY]);
		int aa = a << 1;
		return aa + b + c + 2 >> 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105, 110, 109, 110 })]
	public Intra8x8PredictionBuilder()
	{
		topBuf = new byte[16];
		leftBuf = new byte[8];
		genBuf = new byte[24];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 129, 76, 159, 17, 108, 118, 134, 108,
		117, 134, 154, 134, 108, 151, 134, 120, 119, 134,
		120, 119, 134, 120, 119, 131, 108, 150, 131, 108,
		213, 104, 140, 143, 105, 51, 169, 109, 105, 49,
		169, 113
	})]
	public virtual void predictWithMode(int mode, int[] residual, bool leftAvailable, bool topAvailable, bool topLeftAvailable, bool topRightAvailable, byte[] leftRow, byte[] topLine, byte[] topLeft, int mbOffX, int blkX, int blkY, byte[] pixOut)
	{
		switch (mode)
		{
		case 0:
			Preconditions.checkState(topAvailable, (object)"");
			predictVertical(residual, topLeftAvailable, topRightAvailable, topLeft, topLine, mbOffX, blkX, blkY, pixOut);
			break;
		case 1:
			Preconditions.checkState(leftAvailable, (object)"");
			predictHorizontal(residual, topLeftAvailable, topLeft, leftRow, mbOffX, blkX, blkY, pixOut);
			break;
		case 2:
			predictDC(residual, topLeftAvailable, topRightAvailable, leftAvailable, topAvailable, topLeft, leftRow, topLine, mbOffX, blkX, blkY, pixOut);
			break;
		case 3:
			Preconditions.checkState(topAvailable, (object)"");
			predictDiagonalDownLeft(residual, topLeftAvailable, topAvailable, topRightAvailable, topLeft, topLine, mbOffX, blkX, blkY, pixOut);
			break;
		case 4:
			Preconditions.checkState((topAvailable && leftAvailable && topLeftAvailable) ? true : false, (object)"");
			predictDiagonalDownRight(residual, topRightAvailable, topLeft, leftRow, topLine, mbOffX, blkX, blkY, pixOut);
			break;
		case 5:
			Preconditions.checkState((topAvailable && leftAvailable && topLeftAvailable) ? true : false, (object)"");
			predictVerticalRight(residual, topRightAvailable, topLeft, leftRow, topLine, mbOffX, blkX, blkY, pixOut);
			break;
		case 6:
			Preconditions.checkState((topAvailable && leftAvailable && topLeftAvailable) ? true : false, (object)"");
			predictHorizontalDown(residual, topRightAvailable, topLeft, leftRow, topLine, mbOffX, blkX, blkY, pixOut);
			break;
		case 7:
			Preconditions.checkState(topAvailable, (object)"");
			predictVerticalLeft(residual, topLeftAvailable, topRightAvailable, topLeft, topLine, mbOffX, blkX, blkY, pixOut);
			break;
		case 8:
			Preconditions.checkState(leftAvailable, (object)"");
			predictHorizontalUp(residual, topLeftAvailable, topLeft, leftRow, mbOffX, blkX, blkY, pixOut);
			break;
		}
		int oo1 = mbOffX + blkX;
		int off1 = (blkY << 4) + blkX + 7;
		topLeft[blkY >> 2] = topLine[oo1 + 7];
		for (int j = 0; j < 8; j++)
		{
			leftRow[blkY + j] = pixOut[off1 + (j << 4)];
		}
		int off2 = (blkY << 4) + blkX + 112;
		for (int i = 0; i < 8; i++)
		{
			topLine[oo1 + i] = pixOut[off2 + i];
		}
		topLeft[(blkY >> 2) + 1] = leftRow[blkY + 3];
	}
}
