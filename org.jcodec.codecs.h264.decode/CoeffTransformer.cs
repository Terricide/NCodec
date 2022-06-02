using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;
using org.jcodec.common;

namespace org.jcodec.codecs.h264.decode;

public class CoeffTransformer : Object
{
	public static int[] zigzag4x4;

	public static int[] invZigzag4x4;

	internal static int[][] dequantCoef;

	internal static int[][] dequantCoef8x8;

	internal static int[][] initDequantCoeff8x8;

	public static int[] zigzag8x8;

	public static int[] invZigzag8x8;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[][] quantCoeff;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 119, 130, 107, 107, 107, 111, 144, 104, 105,
		105, 234, 55, 234, 77, 108, 110, 110, 115, 115,
		106, 108, 108, 237, 56, 236, 76, 106, 46, 169
	})]
	public static void _idct4x4(int[] block, int[] @out)
	{
		for (int k = 0; k < 16; k += 4)
		{
			int e0 = block[k] + block[k + 2];
			int e1 = block[k] - block[k + 2];
			int e2 = (block[k + 1] >> 1) - block[k + 3];
			int e3 = block[k + 1] + (block[k + 3] >> 1);
			@out[k] = e0 + e3;
			@out[k + 1] = e1 + e2;
			@out[k + 2] = e1 - e2;
			@out[k + 3] = e0 - e3;
		}
		for (int j = 0; j < 4; j++)
		{
			int g0 = @out[j] + @out[j + 8];
			int g1 = @out[j] - @out[j + 8];
			int g2 = (@out[j + 4] >> 1) - @out[j + 12];
			int g3 = @out[j + 4] + (@out[j + 12] >> 1);
			@out[j] = g0 + g3;
			@out[j + 4] = g1 + g2;
			@out[j + 8] = g1 - g2;
			@out[j + 12] = g0 - g3;
		}
		for (int i = 0; i < 16; i++)
		{
			@out[i] = @out[i] + 32 >> 6;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 64, 130, 105, 105, 105, 137, 103, 103, 103,
		103
	})]
	public static void invDC2x2(int[] block)
	{
		int t0 = block[0] + block[1];
		int t1 = block[0] - block[1];
		int t2 = block[2] + block[3];
		int t3 = block[2] - block[3];
		block[0] = t0 + t2;
		block[1] = t1 + t3;
		block[2] = t0 - t2;
		block[3] = t1 - t3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(15)]
	public CoeffTransformer()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 120, 98, 106 })]
	public static void idct4x4(int[] block)
	{
		_idct4x4(block, block);
	}

	[LineNumberTable(new byte[]
	{
		159, 111, 130, 107, 107, 109, 109, 140, 103, 108,
		105, 236, 55, 234, 77, 108, 111, 112, 112, 143,
		106, 110, 108, 239, 55, 236, 75
	})]
	public static void fdct4x4(int[] block)
	{
		for (int j = 0; j < 16; j += 4)
		{
			int t = block[j] + block[j + 3];
			int t3 = block[j + 1] + block[j + 2];
			int t5 = block[j + 1] - block[j + 2];
			int t7 = block[j] - block[j + 3];
			block[j] = t + t3;
			block[j + 1] = (t7 << 1) + t5;
			block[j + 2] = t - t3;
			block[j + 3] = t7 - (t5 << 1);
		}
		for (int i = 0; i < 4; i++)
		{
			int t0 = block[i] + block[i + 12];
			int t2 = block[i + 4] + block[i + 8];
			int t4 = block[i + 4] - block[i + 8];
			int t6 = block[i] - block[i + 12];
			block[i] = t0 + t2;
			block[i + 4] = t4 + (t6 << 1);
			block[i + 8] = t0 - t2;
			block[i + 12] = t6 - (t4 << 1);
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 103, 162, 107, 107, 107, 109, 142, 104, 105,
		105, 234, 55, 234, 77, 108, 110, 110, 113, 113,
		106, 108, 108, 237, 56, 236, 74
	})]
	public static void invDC4x4(int[] scaled)
	{
		for (int j = 0; j < 16; j += 4)
		{
			int e0 = scaled[j] + scaled[j + 2];
			int e1 = scaled[j] - scaled[j + 2];
			int e2 = scaled[j + 1] - scaled[j + 3];
			int e3 = scaled[j + 1] + scaled[j + 3];
			scaled[j] = e0 + e3;
			scaled[j + 1] = e1 + e2;
			scaled[j + 2] = e1 - e2;
			scaled[j + 3] = e0 - e3;
		}
		for (int i = 0; i < 4; i++)
		{
			int g0 = scaled[i] + scaled[i + 8];
			int g1 = scaled[i] - scaled[i + 8];
			int g2 = scaled[i + 4] - scaled[i + 12];
			int g3 = scaled[i + 4] + scaled[i + 12];
			scaled[i] = g0 + g3;
			scaled[i + 4] = g1 + g2;
			scaled[i + 8] = g1 - g2;
			scaled[i + 12] = g0 - g3;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 95, 162, 107, 107, 109, 109, 140, 103, 106,
		105, 234, 55, 234, 77, 108, 111, 112, 112, 143,
		108, 110, 110, 239, 55, 236, 75
	})]
	public static void fvdDC4x4(int[] scaled)
	{
		for (int j = 0; j < 16; j += 4)
		{
			int t = scaled[j] + scaled[j + 3];
			int t3 = scaled[j + 1] + scaled[j + 2];
			int t5 = scaled[j + 1] - scaled[j + 2];
			int t7 = scaled[j] - scaled[j + 3];
			scaled[j] = t + t3;
			scaled[j + 1] = t7 + t5;
			scaled[j + 2] = t - t3;
			scaled[j + 3] = t7 - t5;
		}
		for (int i = 0; i < 4; i++)
		{
			int t0 = scaled[i] + scaled[i + 12];
			int t2 = scaled[i + 4] + scaled[i + 8];
			int t4 = scaled[i + 4] - scaled[i + 8];
			int t6 = scaled[i] - scaled[i + 12];
			scaled[i] = t0 + t2 >> 1;
			scaled[i + 4] = t4 + t6 >> 1;
			scaled[i + 8] = t0 - t2 >> 1;
			scaled[i + 12] = t6 - t4 >> 1;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 88, 130, 142, 100, 101, 104, 54, 135, 102,
		102, 103, 106, 63, 5, 137, 99, 104, 109, 106,
		63, 9, 201
	})]
	public static void dequantizeAC(int[] coeffs, int qp, int[] scalingList)
	{
		int group = ((6 != -1) ? (qp % 6) : 0);
		if (scalingList == null)
		{
			int qbits3 = qp / 6;
			for (int k = 0; k < 16; k++)
			{
				coeffs[k] = coeffs[k] * dequantCoef[group][k] << qbits3;
			}
			return;
		}
		if (qp >= 24)
		{
			int qbits2 = qp / 6 - 4;
			for (int j = 0; j < 16; j++)
			{
				coeffs[j] = coeffs[j] * dequantCoef[group][j] * scalingList[invZigzag4x4[j]] << qbits2;
			}
			return;
		}
		int qbits = 4 - qp / 6;
		int addition = 1 << 3 - qp / 6;
		for (int i = 0; i < 16; i++)
		{
			coeffs[i] = coeffs[i] * scalingList[invZigzag4x4[i]] * dequantCoef[group][i] + addition >> qbits;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 83, 162, 101, 142, 112, 134, 102, 106, 106,
		31, 18, 235, 70, 106, 106, 31, 8, 233, 69
	})]
	public static void quantizeAC(int[] coeffs, int qp)
	{
		int level = qp / 6;
		int offset = ((6 != -1) ? (qp % 6) : 0);
		int addition = 682 << qp / 6 + 4;
		int qbits = 15 + level;
		if (qp < 10)
		{
			for (int j = 0; j < 16; j++)
			{
				int sign2 = coeffs[j] >> 31;
				coeffs[j] = (Math.min(((coeffs[j] ^ sign2) - sign2) * quantCoeff[offset][j] + addition >> qbits, 2063) ^ sign2) - sign2;
			}
		}
		else
		{
			for (int i = 0; i < 16; i++)
			{
				int sign = coeffs[i] >> 31;
				coeffs[i] = ((((coeffs[i] ^ sign) - sign) * quantCoeff[offset][i] + addition >> qbits) ^ sign) - sign;
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 77, 66, 142, 105, 103, 100, 104, 56, 169,
		104, 58, 167, 102, 104, 109, 100, 106, 62, 171,
		106, 63, 1, 201
	})]
	public static void dequantizeDC4x4(int[] coeffs, int qp, int[] scalingList)
	{
		int group = ((6 != -1) ? (qp % 6) : 0);
		if (qp >= 36)
		{
			int qbits2 = qp / 6 - 6;
			if (scalingList == null)
			{
				for (int l = 0; l < 16; l++)
				{
					coeffs[l] = coeffs[l] * (dequantCoef[group][0] << 4) << qbits2;
				}
			}
			else
			{
				for (int k = 0; k < 16; k++)
				{
					coeffs[k] = coeffs[k] * dequantCoef[group][0] * scalingList[0] << qbits2;
				}
			}
			return;
		}
		int qbits = 6 - qp / 6;
		int addition = 1 << 5 - qp / 6;
		if (scalingList == null)
		{
			for (int j = 0; j < 16; j++)
			{
				coeffs[j] = coeffs[j] * (dequantCoef[group][0] << 4) + addition >> qbits;
			}
		}
		else
		{
			for (int i = 0; i < 16; i++)
			{
				coeffs[i] = coeffs[i] * dequantCoef[group][0] * scalingList[0] + addition >> qbits;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 71, 130, 101, 142, 112, 134, 102, 106, 106,
		31, 17, 235, 70, 106, 106, 31, 7, 233, 69
	})]
	public static void quantizeDC4x4(int[] coeffs, int qp)
	{
		int level = qp / 6;
		int offset = ((6 != -1) ? (qp % 6) : 0);
		int addition = 682 << qp / 6 + 5;
		int qbits = 16 + level;
		if (qp < 10)
		{
			for (int j = 0; j < 16; j++)
			{
				int sign2 = coeffs[j] >> 31;
				coeffs[j] = (Math.min(((coeffs[j] ^ sign2) - sign2) * quantCoeff[offset][0] + addition >> qbits, 2063) ^ sign2) - sign2;
			}
		}
		else
		{
			for (int i = 0; i < 16; i++)
			{
				int sign = coeffs[i] >> 31;
				coeffs[i] = ((((coeffs[i] ^ sign) - sign) * quantCoeff[offset][0] + addition >> qbits) ^ sign) - sign;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 60, 162, 105 })]
	public static void fvdDC2x2(int[] block)
	{
		invDC2x2(block);
	}

	[LineNumberTable(new byte[]
	{
		159, 58, 66, 142, 100, 133, 103, 56, 167, 102,
		102, 103, 105, 62, 169, 99, 104, 109, 105, 63,
		3, 233, 69
	})]
	public static void dequantizeDC2x2(int[] transformed, int qp, int[] scalingList)
	{
		int group = ((6 != -1) ? (qp % 6) : 0);
		if (scalingList == null)
		{
			int shift = qp / 6;
			for (int k = 0; k < 4; k++)
			{
				transformed[k] = transformed[k] * dequantCoef[group][0] << shift >> 1;
			}
			return;
		}
		if (qp >= 24)
		{
			int qbits2 = qp / 6 - 4;
			for (int j = 0; j < 4; j++)
			{
				transformed[j] = transformed[j] * dequantCoef[group][0] * scalingList[0] << qbits2 >> 1;
			}
			return;
		}
		int qbits = 4 - qp / 6;
		int addition = 1 << 3 - qp / 6;
		for (int i = 0; i < 4; i++)
		{
			transformed[i] = transformed[i] * dequantCoef[group][0] * scalingList[0] + addition >> qbits >> 1;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 52, 130, 101, 142, 112, 134, 101, 105, 106,
		31, 17, 235, 70, 105, 106, 31, 7, 233, 69
	})]
	public static void quantizeDC2x2(int[] coeffs, int qp)
	{
		int level = qp / 6;
		int offset = ((6 != -1) ? (qp % 6) : 0);
		int addition = 682 << qp / 6 + 5;
		int qbits = 16 + level;
		if (qp < 4)
		{
			for (int j = 0; j < 4; j++)
			{
				int sign2 = coeffs[j] >> 31;
				coeffs[j] = (Math.min(((coeffs[j] ^ sign2) - sign2) * quantCoeff[offset][0] + addition >> qbits, 2063) ^ sign2) - sign2;
			}
		}
		else
		{
			for (int i = 0; i < 4; i++)
			{
				int sign = coeffs[i] >> 31;
				coeffs[i] = ((((coeffs[i] ^ sign) - sign) * quantCoeff[offset][0] + addition >> qbits) ^ sign) - sign;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 47, 162, 105, 105, 107, 109 })]
	public static void reorderDC4x4(int[] dc)
	{
		ArrayUtil.swap(dc, 2, 4);
		ArrayUtil.swap(dc, 3, 5);
		ArrayUtil.swap(dc, 10, 12);
		ArrayUtil.swap(dc, 11, 13);
	}

	[LineNumberTable(391)]
	public static void fvdDC4x2(int[] dc)
	{
	}

	[LineNumberTable(395)]
	public static void quantizeDC4x2(int[] dc, int qp)
	{
	}

	[LineNumberTable(400)]
	public static void invDC4x2(int[] dc)
	{
	}

	[LineNumberTable(405)]
	public static void dequantizeDC4x2(int[] dc, int qp)
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 39, 130, 142, 105, 103, 100, 104, 56, 169,
		104, 63, 1, 167, 102, 104, 109, 100, 106, 63,
		0, 171, 106, 63, 9, 201
	})]
	public static void dequantizeAC8x8(int[] coeffs, int qp, int[] scalingList)
	{
		int group = ((6 != -1) ? (qp % 6) : 0);
		if (qp >= 36)
		{
			int qbits2 = qp / 6 - 6;
			if (scalingList == null)
			{
				for (int l = 0; l < 64; l++)
				{
					coeffs[l] = coeffs[l] * dequantCoef8x8[group][l] << 4 << qbits2;
				}
			}
			else
			{
				for (int k = 0; k < 64; k++)
				{
					coeffs[k] = coeffs[k] * dequantCoef8x8[group][k] * scalingList[invZigzag8x8[k]] << qbits2;
				}
			}
			return;
		}
		int qbits = 6 - qp / 6;
		int addition = 1 << 5 - qp / 6;
		if (scalingList == null)
		{
			for (int j = 0; j < 64; j++)
			{
				coeffs[j] = coeffs[j] * (dequantCoef8x8[group][j] << 4) + addition >> qbits;
			}
		}
		else
		{
			for (int i = 0; i < 64; i++)
			{
				coeffs[i] = coeffs[i] * dequantCoef8x8[group][i] * scalingList[invZigzag8x8[i]] + addition >> qbits;
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 33, 162, 163, 106, 107, 124, 108, 124, 112,
		125, 112, 156, 103, 105, 104, 106, 104, 106, 103,
		137, 105, 107, 107, 107, 107, 107, 107, 139, 229,
		36, 234, 96, 108, 111, 127, 6, 111, 127, 4,
		116, 127, 5, 116, 159, 3, 104, 106, 104, 106,
		104, 106, 104, 138, 106, 108, 109, 109, 109, 109,
		109, 237, 38, 236, 94, 106, 46, 169
	})]
	public static void idct8x8(int[] ac)
	{
		int off = 0;
		for (int row = 0; row < 8; row++)
		{
			int e = ac[off] + ac[off + 4];
			int e3 = -ac[off + 3] + ac[off + 5] - ac[off + 7] - (ac[off + 7] >> 1);
			int e5 = ac[off] - ac[off + 4];
			int e7 = ac[off + 1] + ac[off + 7] - ac[off + 3] - (ac[off + 3] >> 1);
			int e9 = (ac[off + 2] >> 1) - ac[off + 6];
			int e11 = -ac[off + 1] + ac[off + 7] + ac[off + 5] + (ac[off + 5] >> 1);
			int e13 = ac[off + 2] + (ac[off + 6] >> 1);
			int e15 = ac[off + 3] + ac[off + 5] + ac[off + 1] + (ac[off + 1] >> 1);
			int f = e + e13;
			int f3 = e3 + (e15 >> 2);
			int f5 = e5 + e9;
			int f7 = e7 + (e11 >> 2);
			int f9 = e5 - e9;
			int f11 = (e7 >> 2) - e11;
			int f13 = e - e13;
			int f15 = e15 - (e3 >> 2);
			ac[off] = f + f15;
			ac[off + 1] = f5 + f11;
			ac[off + 2] = f9 + f7;
			ac[off + 3] = f13 + f3;
			ac[off + 4] = f13 - f3;
			ac[off + 5] = f9 - f7;
			ac[off + 6] = f5 - f11;
			ac[off + 7] = f - f15;
			off += 8;
		}
		for (int col = 0; col < 8; col++)
		{
			int e0 = ac[col] + ac[col + 32];
			int e2 = -ac[col + 24] + ac[col + 40] - ac[col + 56] - (ac[col + 56] >> 1);
			int e4 = ac[col] - ac[col + 32];
			int e6 = ac[col + 8] + ac[col + 56] - ac[col + 24] - (ac[col + 24] >> 1);
			int e8 = (ac[col + 16] >> 1) - ac[col + 48];
			int e10 = -ac[col + 8] + ac[col + 56] + ac[col + 40] + (ac[col + 40] >> 1);
			int e12 = ac[col + 16] + (ac[col + 48] >> 1);
			int e14 = ac[col + 24] + ac[col + 40] + ac[col + 8] + (ac[col + 8] >> 1);
			int f0 = e0 + e12;
			int f2 = e2 + (e14 >> 2);
			int f4 = e4 + e8;
			int f6 = e6 + (e10 >> 2);
			int f8 = e4 - e8;
			int f10 = (e6 >> 2) - e10;
			int f12 = e0 - e12;
			int f14 = e14 - (e2 >> 2);
			ac[col] = f0 + f14;
			ac[col + 8] = f4 + f10;
			ac[col + 16] = f8 + f6;
			ac[col + 24] = f12 + f2;
			ac[col + 32] = f12 - f2;
			ac[col + 40] = f8 - f6;
			ac[col + 48] = f4 - f10;
			ac[col + 56] = f0 - f14;
		}
		for (int i = 0; i < 64; i++)
		{
			ac[i] = ac[i] + 32 >> 6;
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		138,
		98,
		127,
		60,
		141,
		byte.MaxValue,
		161,
		243,
		73,
		159,
		11,
		223,
		160,
		151,
		191,
		161,
		28,
		173,
		104,
		47,
		135,
		104,
		47,
		199,
		byte.MaxValue,
		163,
		19,
		73,
		108,
		120,
		105,
		105,
		59,
		41,
		169,
		105,
		105,
		59,
		41,
		169,
		105,
		105,
		59,
		41,
		169,
		105,
		105,
		59,
		41,
		169,
		105,
		105,
		59,
		41,
		169,
		105,
		105,
		59,
		41,
		169,
		105,
		105,
		59,
		41,
		233,
		44,
		236,
		88
	})]
	static CoeffTransformer()
	{
		zigzag4x4 = new int[16]
		{
			0, 1, 4, 8, 5, 2, 3, 6, 9, 12,
			13, 10, 7, 11, 14, 15
		};
		invZigzag4x4 = new int[16];
		dequantCoef = new int[6][]
		{
			new int[16]
			{
				10, 13, 10, 13, 13, 16, 13, 16, 10, 13,
				10, 13, 13, 16, 13, 16
			},
			new int[16]
			{
				11, 14, 11, 14, 14, 18, 14, 18, 11, 14,
				11, 14, 14, 18, 14, 18
			},
			new int[16]
			{
				13, 16, 13, 16, 16, 20, 16, 20, 13, 16,
				13, 16, 16, 20, 16, 20
			},
			new int[16]
			{
				14, 18, 14, 18, 18, 23, 18, 23, 14, 18,
				14, 18, 18, 23, 18, 23
			},
			new int[16]
			{
				16, 20, 16, 20, 20, 25, 20, 25, 16, 20,
				16, 20, 20, 25, 20, 25
			},
			new int[16]
			{
				18, 23, 18, 23, 23, 29, 23, 29, 18, 23,
				18, 23, 23, 29, 23, 29
			}
		};
		int[] array = new int[2];
		int num = (array[1] = 64);
		num = (array[0] = 6);
		dequantCoef8x8 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		initDequantCoeff8x8 = new int[6][]
		{
			new int[6] { 20, 18, 32, 19, 25, 24 },
			new int[6] { 22, 19, 35, 21, 28, 26 },
			new int[6] { 26, 23, 42, 24, 33, 31 },
			new int[6] { 28, 25, 45, 26, 35, 33 },
			new int[6] { 32, 28, 51, 30, 40, 38 },
			new int[6] { 36, 32, 58, 34, 46, 43 }
		};
		zigzag8x8 = new int[64]
		{
			0, 1, 8, 16, 9, 2, 3, 10, 17, 24,
			32, 25, 18, 11, 4, 5, 12, 19, 26, 33,
			40, 48, 41, 34, 27, 20, 13, 6, 7, 14,
			21, 28, 35, 42, 49, 56, 57, 50, 43, 36,
			29, 22, 15, 23, 30, 37, 44, 51, 58, 59,
			52, 45, 38, 31, 39, 46, 53, 60, 61, 54,
			47, 55, 62, 63
		};
		invZigzag8x8 = new int[64];
		for (int i4 = 0; i4 < 16; i4++)
		{
			invZigzag4x4[zigzag4x4[i4]] = i4;
		}
		for (int i3 = 0; i3 < 64; i3++)
		{
			invZigzag8x8[zigzag8x8[i3]] = i3;
		}
		quantCoeff = new int[6][]
		{
			new int[16]
			{
				13107, 8066, 13107, 8066, 8066, 5243, 8066, 5243, 13107, 8066,
				13107, 8066, 8066, 5243, 8066, 5243
			},
			new int[16]
			{
				11916, 7490, 11916, 7490, 7490, 4660, 7490, 4660, 11916, 7490,
				11916, 7490, 7490, 4660, 7490, 4660
			},
			new int[16]
			{
				10082, 6554, 10082, 6554, 6554, 4194, 6554, 4194, 10082, 6554,
				10082, 6554, 6554, 4194, 6554, 4194
			},
			new int[16]
			{
				9362, 5825, 9362, 5825, 5825, 3647, 5825, 3647, 9362, 5825,
				9362, 5825, 5825, 3647, 5825, 3647
			},
			new int[16]
			{
				8192, 5243, 8192, 5243, 5243, 3355, 5243, 3355, 8192, 5243,
				8192, 5243, 5243, 3355, 5243, 3355
			},
			new int[16]
			{
				7282, 4559, 7282, 4559, 4559, 2893, 4559, 2893, 7282, 4559,
				7282, 4559, 4559, 2893, 4559, 2893
			}
		};
		for (int g = 0; g < 6; g++)
		{
			Arrays.fill(dequantCoef8x8[g], initDequantCoeff8x8[g][5]);
			for (int i2 = 0; i2 < 8; i2 += 4)
			{
				for (int j8 = 0; j8 < 8; j8 += 4)
				{
					dequantCoef8x8[g][(i2 << 3) + j8] = initDequantCoeff8x8[g][0];
				}
			}
			for (int n = 1; n < 8; n += 2)
			{
				for (int j7 = 1; j7 < 8; j7 += 2)
				{
					dequantCoef8x8[g][(n << 3) + j7] = initDequantCoeff8x8[g][1];
				}
			}
			for (int m = 2; m < 8; m += 4)
			{
				for (int j6 = 2; j6 < 8; j6 += 4)
				{
					dequantCoef8x8[g][(m << 3) + j6] = initDequantCoeff8x8[g][2];
				}
			}
			for (int l = 0; l < 8; l += 4)
			{
				for (int j5 = 1; j5 < 8; j5 += 2)
				{
					dequantCoef8x8[g][(l << 3) + j5] = initDequantCoeff8x8[g][3];
				}
			}
			for (int k = 1; k < 8; k += 2)
			{
				for (int j4 = 0; j4 < 8; j4 += 4)
				{
					dequantCoef8x8[g][(k << 3) + j4] = initDequantCoeff8x8[g][3];
				}
			}
			for (int j = 0; j < 8; j += 4)
			{
				for (int j3 = 2; j3 < 8; j3 += 4)
				{
					dequantCoef8x8[g][(j << 3) + j3] = initDequantCoeff8x8[g][4];
				}
			}
			for (int i = 2; i < 8; i += 4)
			{
				for (int j2 = 0; j2 < 8; j2 += 4)
				{
					dequantCoef8x8[g][(i << 3) + j2] = initDequantCoeff8x8[g][4];
				}
			}
		}
	}
}
