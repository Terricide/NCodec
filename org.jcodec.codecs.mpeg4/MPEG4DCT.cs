using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mpeg4;

public class MPEG4DCT : Object
{
	private const int W1 = 2841;

	private const int W2 = 2676;

	private const int W3 = 2408;

	private const int W5 = 1609;

	private const int W6 = 1108;

	private const int W7 = 565;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 129, 67, 135, 159, 5, 109, 166, 109,
		166, 100, 144, 145, 163, 100, 144, 145, 163, 108,
		163, 174
	})]
	public static void idctAdd(byte[][] p, short[] block, int index, bool interlacing)
	{
		idctRows(block);
		switch (index)
		{
		case 0:
			idctColumnsAdd(block, p[0], 0, 16);
			break;
		case 1:
			idctColumnsAdd(block, p[0], 8, 16);
			break;
		case 2:
			if (interlacing)
			{
				idctColumnsAdd(block, p[0], 16, 32);
			}
			else
			{
				idctColumnsAdd(block, p[0], 128, 16);
			}
			break;
		case 3:
			if (interlacing)
			{
				idctColumnsAdd(block, p[0], 24, 32);
			}
			else
			{
				idctColumnsAdd(block, p[0], 136, 16);
			}
			break;
		case 4:
			idctColumnsAdd(block, p[1], 0, 8);
			break;
		case 5:
			idctColumnsAdd(block, p[2], 0, 8);
			break;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 90, 98, 106, 133, 191, 38, 159, 112, 166,
		143, 110, 110, 110, 110, 110, 142, 103, 103, 108,
		107, 109, 103, 104, 104, 136, 104, 104, 103, 103,
		117, 150, 107, 109, 110, 110, 110, 110, 109, 237,
		20, 234, 110
	})]
	public static void idctRows(short[] block)
	{
		for (int i = 0; i < 8; i++)
		{
			int offset = i << 3;
			int X1;
			int X6;
			int X5;
			int X7;
			int X3;
			int X2;
			int X4;
			if (((X1 = block[offset + 4] << 11) | (X2 = block[offset + 6]) | (X3 = block[offset + 2]) | (X4 = block[offset + 1]) | (X5 = block[offset + 7]) | (X6 = block[offset + 5]) | (X7 = block[offset + 3])) == 0)
			{
				int num = offset + 1;
				int num2 = offset + 2;
				int num3 = offset + 3;
				int num4 = offset + 4;
				int num5 = offset + 5;
				int num6 = offset + 6;
				int num7 = offset + 7;
				int num8 = (short)(block[offset] << 3);
				int num9 = num7;
				short[] array = block;
				int num10 = num8;
				array[num9] = (short)num8;
				num8 = num10;
				num9 = num6;
				array = block;
				int num11 = num8;
				array[num9] = (short)num8;
				num8 = num11;
				num9 = num5;
				array = block;
				int num12 = num8;
				array[num9] = (short)num8;
				num8 = num12;
				num9 = num4;
				array = block;
				int num13 = num8;
				array[num9] = (short)num8;
				num8 = num13;
				num9 = num3;
				array = block;
				int num14 = num8;
				array[num9] = (short)num8;
				num8 = num14;
				num9 = num2;
				array = block;
				int num15 = num8;
				array[num9] = (short)num8;
				num8 = num15;
				num9 = num;
				array = block;
				int num16 = num8;
				array[num9] = (short)num8;
				block[offset] = (short)num16;
			}
			else
			{
				int X0 = (block[offset] << 11) + 128;
				int X8 = 565 * (X4 + X5);
				X4 = X8 + 2276 * X4;
				X5 = X8 - 3406 * X5;
				X8 = 2408 * (X6 + X7);
				X6 = X8 - 799 * X6;
				X7 = X8 - 4017 * X7;
				X8 = X0 + X1;
				X0 -= X1;
				X1 = 1108 * (X3 + X2);
				X2 = X1 - 3784 * X2;
				X3 = X1 + 1568 * X3;
				X1 = X4 + X6;
				X4 -= X6;
				X6 = X5 + X7;
				X5 -= X7;
				X7 = X8 + X3;
				X8 -= X3;
				X3 = X0 + X2;
				X0 -= X2;
				X2 = 181 * (X4 + X5) + 128 >> 8;
				X4 = 181 * (X4 - X5) + 128 >> 8;
				block[offset] = (short)(X7 + X1 >> 8);
				block[offset + 1] = (short)(X3 + X2 >> 8);
				block[offset + 2] = (short)(X0 + X4 >> 8);
				block[offset + 3] = (short)(X8 + X6 >> 8);
				block[offset + 4] = (short)(X8 - X6 >> 8);
				block[offset + 5] = (short)(X0 - X4 >> 8);
				block[offset + 6] = (short)(X3 - X2 >> 8);
				block[offset + 7] = (short)(X7 - X1 >> 8);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 118, 66, 106, 133, 191, 43, 159, 28, 159,
		82, 166, 144, 112, 112, 112, 112, 112, 144, 103,
		103, 110, 109, 111, 103, 104, 104, 136, 104, 104,
		103, 103, 117, 150, 117, 117, 118, 118, 118, 118,
		117, 245, 18, 234, 112
	})]
	public static void idctColumnsPut(short[] block, byte[] dst, int dstOffset, int stride)
	{
		for (int i = 0; i < 8; i++)
		{
			int offset = dstOffset + i;
			int X1;
			int X6;
			int X5;
			int X7;
			int X3;
			int X2;
			int X4;
			if (((X1 = block[i + 32] << 8) | (X2 = block[i + 48]) | (X3 = block[i + 16]) | (X4 = block[i + 8]) | (X5 = block[i + 56]) | (X6 = block[i + 40]) | (X7 = block[i + 24])) == 0)
			{
				int num = offset + stride * 0;
				int num2 = offset + stride * 1;
				int num3 = offset + stride * 2;
				int num4 = offset + stride * 3;
				int num5 = offset + stride * 4;
				int num6 = offset + stride * 5;
				int num7 = offset + stride * 6;
				int num8 = offset + stride * 7;
				int num9 = (sbyte)clamp255(block[i + 0] + 32 >> 6);
				int num10 = num8;
				byte[] array = dst;
				int num11 = num9;
				array[num10] = (byte)num9;
				num9 = num11;
				num10 = num7;
				array = dst;
				int num12 = num9;
				array[num10] = (byte)num9;
				num9 = num12;
				num10 = num6;
				array = dst;
				int num13 = num9;
				array[num10] = (byte)num9;
				num9 = num13;
				num10 = num5;
				array = dst;
				int num14 = num9;
				array[num10] = (byte)num9;
				num9 = num14;
				num10 = num4;
				array = dst;
				int num15 = num9;
				array[num10] = (byte)num9;
				num9 = num15;
				num10 = num3;
				array = dst;
				int num16 = num9;
				array[num10] = (byte)num9;
				num9 = num16;
				num10 = num2;
				array = dst;
				int num17 = num9;
				array[num10] = (byte)num9;
				dst[num] = (byte)num17;
			}
			else
			{
				int X0 = (block[i + 0] << 8) + 8192;
				int X8 = 565 * (X4 + X5) + 4;
				X4 = X8 + 2276 * X4 >> 3;
				X5 = X8 - 3406 * X5 >> 3;
				X8 = 2408 * (X6 + X7) + 4;
				X6 = X8 - 799 * X6 >> 3;
				X7 = X8 - 4017 * X7 >> 3;
				X8 = X0 + X1;
				X0 -= X1;
				X1 = 1108 * (X3 + X2) + 4;
				X2 = X1 - 3784 * X2 >> 3;
				X3 = X1 + 1568 * X3 >> 3;
				X1 = X4 + X6;
				X4 -= X6;
				X6 = X5 + X7;
				X5 -= X7;
				X7 = X8 + X3;
				X8 -= X3;
				X3 = X0 + X2;
				X0 -= X2;
				X2 = 181 * (X4 + X5) + 128 >> 8;
				X4 = 181 * (X4 - X5) + 128 >> 8;
				dst[offset + stride * 0] = (byte)(sbyte)clamp255(X7 + X1 >> 14);
				dst[offset + stride * 1] = (byte)(sbyte)clamp255(X3 + X2 >> 14);
				dst[offset + stride * 2] = (byte)(sbyte)clamp255(X0 + X4 >> 14);
				dst[offset + stride * 3] = (byte)(sbyte)clamp255(X8 + X6 >> 14);
				dst[offset + stride * 4] = (byte)(sbyte)clamp255(X8 - X6 >> 14);
				dst[offset + stride * 5] = (byte)(sbyte)clamp255(X0 - X4 >> 14);
				dst[offset + stride * 6] = (byte)(sbyte)clamp255(X3 - X2 >> 14);
				dst[offset + stride * 7] = (byte)(sbyte)clamp255(X7 - X1 >> 14);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		105,
		98,
		106,
		133,
		191,
		43,
		141,
		124,
		124,
		124,
		124,
		124,
		124,
		124,
		156,
		166,
		144,
		112,
		112,
		112,
		112,
		112,
		144,
		103,
		103,
		110,
		109,
		111,
		103,
		104,
		104,
		136,
		104,
		104,
		103,
		103,
		117,
		150,
		127,
		2,
		127,
		2,
		127,
		3,
		127,
		3,
		127,
		3,
		127,
		3,
		127,
		2,
		byte.MaxValue,
		2,
		11,
		234,
		119
	})]
	public static void idctColumnsAdd(short[] block, byte[] dst, int dstOffset, int stride)
	{
		for (int i = 0; i < 8; i++)
		{
			int offset = dstOffset + i;
			int X1;
			int X6;
			int X5;
			int X7;
			int X3;
			int X2;
			int X4;
			if (((X1 = block[i + 32] << 8) | (X2 = block[i + 48]) | (X3 = block[i + 16]) | (X4 = block[i + 8]) | (X5 = block[i + 56]) | (X6 = block[i + 40]) | (X7 = block[i + 24])) == 0)
			{
				int pixel = block[i + 0] + 32 >> 6;
				dst[offset + stride * 0] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 0] + pixel, -128, 127);
				dst[offset + stride * 1] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 1] + pixel, -128, 127);
				dst[offset + stride * 2] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 2] + pixel, -128, 127);
				dst[offset + stride * 3] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 3] + pixel, -128, 127);
				dst[offset + stride * 4] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 4] + pixel, -128, 127);
				dst[offset + stride * 5] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 5] + pixel, -128, 127);
				dst[offset + stride * 6] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 6] + pixel, -128, 127);
				dst[offset + stride * 7] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 7] + pixel, -128, 127);
				continue;
			}
			int X0 = (block[i + 0] << 8) + 8192;
			int X8 = 565 * (X4 + X5) + 4;
			X4 = X8 + 2276 * X4 >> 3;
			X5 = X8 - 3406 * X5 >> 3;
			X8 = 2408 * (X6 + X7) + 4;
			X6 = X8 - 799 * X6 >> 3;
			X7 = X8 - 4017 * X7 >> 3;
			X8 = X0 + X1;
			X0 -= X1;
			X1 = 1108 * (X3 + X2) + 4;
			X2 = X1 - 3784 * X2 >> 3;
			X3 = X1 + 1568 * X3 >> 3;
			X1 = X4 + X6;
			X4 -= X6;
			X6 = X5 + X7;
			X5 -= X7;
			X7 = X8 + X3;
			X8 -= X3;
			X3 = X0 + X2;
			X0 -= X2;
			X2 = 181 * (X4 + X5) + 128 >> 8;
			X4 = 181 * (X4 - X5) + 128 >> 8;
			dst[offset + stride * 0] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 0] + (X7 + X1 >> 14), -128, 127);
			dst[offset + stride * 1] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 1] + (X3 + X2 >> 14), -128, 127);
			dst[offset + stride * 2] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 2] + (X0 + X4 >> 14), -128, 127);
			dst[offset + stride * 3] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 3] + (X8 + X6 >> 14), -128, 127);
			dst[offset + stride * 4] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 4] + (X8 - X6 >> 14), -128, 127);
			dst[offset + stride * 5] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 5] + (X0 - X4 >> 14), -128, 127);
			dst[offset + stride * 6] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 6] + (X3 - X2 >> 14), -128, 127);
			dst[offset + stride * 7] = (byte)(sbyte)MathUtil.clip(dst[offset + stride * 7] + (X7 - X1 >> 14), -128, 127);
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 120, 66, 106, 112 })]
	private static byte clamp255(int val)
	{
		val += -255;
		val = -(255 + ((val >> 31) & val));
		return (byte)(sbyte)(-((val >> 31) & val) - 128);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public MPEG4DCT()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 129, 67, 105, 105, 105, 105, 105, 137,
		100, 99, 103, 100, 99, 165, 110, 110, 110, 112,
		110, 112
	})]
	public static void idctPut(byte[][] p, short[][] block, bool interlacing)
	{
		idctRows(block[0]);
		idctRows(block[1]);
		idctRows(block[2]);
		idctRows(block[3]);
		idctRows(block[4]);
		idctRows(block[5]);
		int stride = 16;
		int stride2 = 8;
		int nextBlock = 128;
		if (interlacing)
		{
			nextBlock = stride;
			stride *= 2;
		}
		idctColumnsPut(block[0], p[0], 0, stride);
		idctColumnsPut(block[1], p[0], 8, stride);
		idctColumnsPut(block[2], p[0], nextBlock, stride);
		idctColumnsPut(block[3], p[0], nextBlock + 8, stride);
		idctColumnsPut(block[4], p[1], 0, stride2);
		idctColumnsPut(block[5], p[2], 0, stride2);
	}
}
