using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common.dct;

public class SimpleIDCT10Bit : Object
{
	private const int ROUND_COL = 8192;

	private const int ROUND_ROW = 32768;

	private const int SHIFT_COL = 14;

	private const int SHIFT_ROW = 16;

	public const int C0 = 23170;

	public const int C1 = 32138;

	public const int C2 = 27246;

	public const int C3 = 18205;

	public const int C4 = 6393;

	public const int C5 = 30274;

	public const int C6 = 12540;

	public static int W1;

	public static int W2;

	public static int W3;

	public static int W4;

	public static int W5;

	public static int W6;

	public static int W7;

	public static int ROW_SHIFT;

	public static int COL_SHIFT;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 134, 162, 103, 44, 135, 103, 42, 135 })]
	public static void idct10(int[] buf, int off)
	{
		for (int j = 0; j < 8; j++)
		{
			idctRow(buf, off + (j << 3));
		}
		for (int i = 0; i < 8; i++)
		{
			idctCol(buf, off + i);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 162, 103, 42, 199, 104, 42, 167 })]
	public static void fdctProres10(int[] block, int off)
	{
		for (int j = 0; j < 8; j++)
		{
			fdctCol(block, off + j);
		}
		for (int i = 0; i < 64; i += 8)
		{
			fdctRow(block, off + i);
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 116, 98, 120, 99, 99, 131, 111, 111, 111,
		143, 110, 113, 110, 114, 110, 114, 110, 146, 127,
		1, 123, 124, 124, 155, 113, 145, 114, 146, 113,
		145, 113, 178, 115, 115, 115, 115, 115, 115, 115,
		115
	})]
	private static void idctRow(int[] buf, int off)
	{
		int a0 = W4 * buf[off] + (1 << ROW_SHIFT - 1);
		int a1 = a0;
		int a2 = a0;
		int a3 = a0;
		a0 += W2 * buf[off + 2];
		a1 += W6 * buf[off + 2];
		a2 -= W6 * buf[off + 2];
		a3 -= W2 * buf[off + 2];
		int b0 = W1 * buf[off + 1];
		b0 += W3 * buf[off + 3];
		int b1 = W3 * buf[off + 1];
		b1 += -W7 * buf[off + 3];
		int b2 = W5 * buf[off + 1];
		b2 += -W1 * buf[off + 3];
		int b3 = W7 * buf[off + 1];
		b3 += -W5 * buf[off + 3];
		if (buf[off + 4] != 0 || buf[off + 5] != 0 || buf[off + 6] != 0 || buf[off + 7] != 0)
		{
			a0 += W4 * buf[off + 4] + W6 * buf[off + 6];
			a1 += -W4 * buf[off + 4] - W2 * buf[off + 6];
			a2 += -W4 * buf[off + 4] + W2 * buf[off + 6];
			a3 += W4 * buf[off + 4] - W6 * buf[off + 6];
			b0 += W5 * buf[off + 5];
			b0 += W7 * buf[off + 7];
			b1 += -W1 * buf[off + 5];
			b1 += -W5 * buf[off + 7];
			b2 += W7 * buf[off + 5];
			b2 += W3 * buf[off + 7];
			b3 += W3 * buf[off + 5];
			b3 += -W1 * buf[off + 7];
		}
		buf[off + 0] = a0 + b0 >> ROW_SHIFT;
		buf[off + 7] = a0 - b0 >> ROW_SHIFT;
		buf[off + 1] = a1 + b1 >> ROW_SHIFT;
		buf[off + 6] = a1 - b1 >> ROW_SHIFT;
		buf[off + 2] = a2 + b2 >> ROW_SHIFT;
		buf[off + 5] = a2 - b2 >> ROW_SHIFT;
		buf[off + 3] = a3 + b3 >> ROW_SHIFT;
		buf[off + 4] = a3 - b3 >> ROW_SHIFT;
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 131, 66, 127, 9, 99, 99, 131, 112, 112,
		113, 145, 110, 110, 110, 142, 114, 115, 115, 147,
		105, 112, 113, 113, 176, 105, 114, 115, 114, 178,
		105, 112, 113, 112, 177, 105, 114, 115, 114, 179,
		113, 115, 116, 116, 116, 116, 116, 116
	})]
	private static void idctCol(int[] buf, int off)
	{
		int w = W4;
		int num = buf[off + 0];
		int num2 = 1 << COL_SHIFT - 1;
		int w2 = W4;
		int a0 = w * (num + ((w2 != -1) ? (num2 / w2) : (-num2)));
		int a1 = a0;
		int a2 = a0;
		int a3 = a0;
		a0 += W2 * buf[off + 16];
		a1 += W6 * buf[off + 16];
		a2 += -W6 * buf[off + 16];
		a3 += -W2 * buf[off + 16];
		int b0 = W1 * buf[off + 8];
		int b1 = W3 * buf[off + 8];
		int b2 = W5 * buf[off + 8];
		int b3 = W7 * buf[off + 8];
		b0 += W3 * buf[off + 24];
		b1 += -W7 * buf[off + 24];
		b2 += -W1 * buf[off + 24];
		b3 += -W5 * buf[off + 24];
		if (buf[off + 32] != 0)
		{
			a0 += W4 * buf[off + 32];
			a1 += -W4 * buf[off + 32];
			a2 += -W4 * buf[off + 32];
			a3 += W4 * buf[off + 32];
		}
		if (buf[off + 40] != 0)
		{
			b0 += W5 * buf[off + 40];
			b1 += -W1 * buf[off + 40];
			b2 += W7 * buf[off + 40];
			b3 += W3 * buf[off + 40];
		}
		if (buf[off + 48] != 0)
		{
			a0 += W6 * buf[off + 48];
			a1 += -W2 * buf[off + 48];
			a2 += W2 * buf[off + 48];
			a3 += -W6 * buf[off + 48];
		}
		if (buf[off + 56] != 0)
		{
			b0 += W7 * buf[off + 56];
			b1 += -W5 * buf[off + 56];
			b2 += W3 * buf[off + 56];
			b3 += -W1 * buf[off + 56];
		}
		buf[off] = a0 + b0 >> COL_SHIFT;
		buf[off + 8] = a1 + b1 >> COL_SHIFT;
		buf[off + 16] = a2 + b2 >> COL_SHIFT;
		buf[off + 24] = a3 + b3 >> COL_SHIFT;
		buf[off + 32] = a3 - b3 >> COL_SHIFT;
		buf[off + 40] = a2 - b2 >> COL_SHIFT;
		buf[off + 48] = a1 - b1 >> COL_SHIFT;
		buf[off + 56] = a0 - b0 >> COL_SHIFT;
	}

	[LineNumberTable(new byte[]
	{
		159, 94, 162, 110, 110, 111, 111, 111, 112, 111,
		144, 104, 104, 110, 142, 107, 107, 107, 139, 127,
		15, 127, 16, 127, 16, 127, 16, 116, 117, 117,
		117
	})]
	private static void fdctCol(int[] block, int off)
	{
		int z0 = block[off + 0] - block[off + 56];
		int z1 = block[off + 8] - block[off + 48];
		int z2 = block[off + 16] - block[off + 40];
		int z3 = block[off + 24] - block[off + 32];
		int z4 = block[off + 0] + block[off + 56];
		int z5 = block[off + 24] + block[off + 32];
		int z6 = block[off + 8] + block[off + 48];
		int z7 = block[off + 16] + block[off + 40];
		int u0 = z4 - z5;
		int u1 = z6 - z7;
		int c0 = (z4 + z5) * 23170;
		int c1 = (z6 + z7) * 23170;
		int c2 = u0 * 30274;
		int c3 = u1 * 12540;
		int c4 = u0 * 12540;
		int c5 = u1 * 30274;
		block[8 + off] = z0 * 32138 + z1 * 27246 + z2 * 18205 + z3 * 6393 + 8192 >> 14;
		block[24 + off] = z0 * 27246 - z1 * 6393 - z2 * 32138 - z3 * 18205 + 8192 >> 14;
		block[40 + off] = z0 * 18205 - z1 * 32138 + z2 * 6393 + z3 * 27246 + 8192 >> 14;
		block[56 + off] = z0 * 6393 - z1 * 18205 + z2 * 27246 - z3 * 32138 + 8192 >> 14;
		block[0 + off] = c0 + c1 + 8192 >> 14;
		block[16 + off] = c2 + c3 + 8192 >> 14;
		block[32 + off] = c0 - c1 + 8192 >> 14;
		block[48 + off] = c4 - c5 + 8192 >> 14;
	}

	[LineNumberTable(new byte[]
	{
		159, 101, 98, 109, 109, 109, 109, 110, 110, 110,
		142, 104, 104, 110, 142, 107, 107, 107, 139, 127,
		15, 127, 15, 127, 15, 127, 15, 116, 116, 116,
		116
	})]
	private static void fdctRow(int[] block, int off)
	{
		int z0 = block[off + 0] - block[off + 7];
		int z1 = block[off + 1] - block[off + 6];
		int z2 = block[off + 2] - block[off + 5];
		int z3 = block[off + 3] - block[off + 4];
		int z4 = block[off + 0] + block[off + 7];
		int z5 = block[off + 3] + block[off + 4];
		int z6 = block[off + 1] + block[off + 6];
		int z7 = block[off + 2] + block[off + 5];
		int u0 = z4 - z5;
		int u1 = z6 - z7;
		int c0 = (z4 + z5) * 23170;
		int c1 = (z6 + z7) * 23170;
		int c2 = u0 * 30274;
		int c3 = u1 * 12540;
		int c4 = u0 * 12540;
		int c5 = u1 * 30274;
		block[1 + off] = z0 * 32138 + z1 * 27246 + z2 * 18205 + z3 * 6393 + 32768 >> 16;
		block[3 + off] = z0 * 27246 - z1 * 6393 - z2 * 32138 - z3 * 18205 + 32768 >> 16;
		block[5 + off] = z0 * 18205 - z1 * 32138 + z2 * 6393 + z3 * 27246 + 32768 >> 16;
		block[7 + off] = z0 * 6393 - z1 * 18205 + z2 * 27246 - z3 * 32138 + 32768 >> 16;
		block[0 + off] = c0 + c1 + 32768 >> 16;
		block[2 + off] = c2 + c3 + 32768 >> 16;
		block[4 + off] = c0 - c1 + 32768 >> 16;
		block[6 + off] = c4 - c5 + 32768 >> 16;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public SimpleIDCT10Bit()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 136, 66, 107, 107, 107, 107, 107, 107, 107,
		104
	})]
	static SimpleIDCT10Bit()
	{
		W1 = 90901;
		W2 = 85627;
		W3 = 77062;
		W4 = 65535;
		W5 = 51491;
		W6 = 35468;
		W7 = 18081;
		ROW_SHIFT = 15;
		COL_SHIFT = 20;
	}
}
