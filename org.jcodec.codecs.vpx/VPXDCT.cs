using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.vpx;

public class VPXDCT : Object
{
	public static int cospi8sqrt2minus1;

	public static int sinpi8sqrt2;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 132, 98, 107, 109, 111, 111, 142, 111, 106,
		106, 233, 55, 234, 76, 108, 110, 113, 113, 142,
		104, 104, 104, 136, 107, 107, 107, 139, 107, 109,
		109, 238, 45, 236, 85
	})]
	public static void walsh4x4(int[] coef)
	{
		for (int j = 0; j < 16; j += 4)
		{
			int a2 = coef[j] + coef[j + 2] << 2;
			int d2 = coef[j + 1] + coef[j + 3] << 2;
			int c2 = coef[j + 1] - coef[j + 3] << 2;
			int b2 = coef[j] - coef[j + 2] << 2;
			coef[j] = a2 + d2 + ((a2 != 0) ? 1 : 0);
			coef[j + 1] = b2 + c2;
			coef[j + 2] = b2 - c2;
			coef[j + 3] = a2 - d2;
		}
		for (int i = 0; i < 4; i++)
		{
			int a1 = coef[i] + coef[i + 8];
			int d1 = coef[i + 4] + coef[i + 12];
			int c1 = coef[i + 4] - coef[i + 12];
			int b1 = coef[i] - coef[i + 8];
			int a3 = a1 + d1;
			int b3 = b1 + c1;
			int c3 = b1 - c1;
			int d3 = a1 - d1;
			a3 += ((a3 < 0) ? 1 : 0);
			b3 += ((b3 < 0) ? 1 : 0);
			c3 += ((c3 < 0) ? 1 : 0);
			d3 += ((d3 < 0) ? 1 : 0);
			coef[i] = a3 + 3 >> 3;
			coef[i + 4] = b3 + 3 >> 3;
			coef[i + 8] = c3 + 3 >> 3;
			coef[i + 12] = d3 + 3 >> 3;
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		139,
		98,
		107,
		109,
		111,
		111,
		142,
		103,
		137,
		127,
		0,
		byte.MaxValue,
		0,
		54,
		234,
		77,
		108,
		111,
		112,
		112,
		143,
		110,
		144,
		127,
		11,
		byte.MaxValue,
		3,
		54,
		236,
		76
	})]
	public static void fdct4x4(int[] coef)
	{
		for (int j = 0; j < 16; j += 4)
		{
			int a2 = coef[j] + coef[j + 3] << 3;
			int b2 = coef[j + 1] + coef[j + 2] << 3;
			int c2 = coef[j + 1] - coef[j + 2] << 3;
			int d2 = coef[j] - coef[j + 3] << 3;
			coef[j] = a2 + b2;
			coef[j + 2] = a2 - b2;
			coef[j + 1] = c2 * 2217 + d2 * 5352 + 14500 >> 12;
			coef[j + 3] = d2 * 2217 - c2 * 5352 + 7500 >> 12;
		}
		for (int i = 0; i < 4; i++)
		{
			int a1 = coef[i] + coef[i + 12];
			int b1 = coef[i + 4] + coef[i + 8];
			int c1 = coef[i + 4] - coef[i + 8];
			int d1 = coef[i] - coef[i + 12];
			coef[i] = a1 + b1 + 7 >> 4;
			coef[i + 8] = a1 - b1 + 7 >> 4;
			coef[i + 4] = (c1 * 2217 + d1 * 5352 + 12000 >> 16) + ((d1 != 0) ? 1 : 0);
			coef[i + 12] = d1 * 2217 - c1 * 5352 + 51000 >> 16;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 122, 98, 106, 107, 139, 112, 121, 135, 118,
		114, 135, 104, 139, 106, 234, 48, 234, 83, 109,
		110, 142, 114, 121, 136, 121, 114, 136, 110, 144,
		112, 240, 48, 236, 83
	})]
	public static void idct4x4(int[] coef)
	{
		for (int j = 0; j < 4; j++)
		{
			int a2 = coef[j] + coef[j + 8];
			int b2 = coef[j] - coef[j + 8];
			int temp2 = coef[j + 4] * sinpi8sqrt2 >> 16;
			int temp4 = coef[j + 12] + (coef[j + 12] * cospi8sqrt2minus1 >> 16);
			int c2 = temp2 - temp4;
			temp2 = coef[j + 4] + (coef[j + 4] * cospi8sqrt2minus1 >> 16);
			temp4 = coef[j + 12] * sinpi8sqrt2 >> 16;
			int d2 = temp2 + temp4;
			coef[j] = a2 + d2;
			coef[j + 12] = a2 - d2;
			coef[j + 4] = b2 + c2;
			coef[j + 8] = b2 - c2;
		}
		for (int i = 0; i < 16; i += 4)
		{
			int a1 = coef[i] + coef[i + 2];
			int b1 = coef[i] - coef[i + 2];
			int temp1 = coef[i + 1] * sinpi8sqrt2 >> 16;
			int temp3 = coef[i + 3] + (coef[i + 3] * cospi8sqrt2minus1 >> 16);
			int c1 = temp1 - temp3;
			temp1 = coef[i + 1] + (coef[i + 1] * cospi8sqrt2minus1 >> 16);
			temp3 = coef[i + 3] * sinpi8sqrt2 >> 16;
			int d1 = temp1 + temp3;
			coef[i] = a1 + d1 + 4 >> 3;
			coef[i + 3] = a1 - d1 + 4 >> 3;
			coef[i + 1] = b1 + c1 + 4 >> 3;
			coef[i + 2] = b1 - c1 + 4 >> 3;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 112, 162, 106, 108, 109, 109, 141, 103, 106,
		105, 235, 55, 234, 76, 109, 110, 112, 112, 142,
		104, 104, 104, 136, 107, 109, 109, 237, 50, 236,
		81
	})]
	public static void iwalsh4x4(int[] coef)
	{
		for (int j = 0; j < 4; j++)
		{
			int a2 = coef[j] + coef[j + 12];
			int b2 = coef[j + 4] + coef[j + 8];
			int c2 = coef[j + 4] - coef[j + 8];
			int d2 = coef[j] - coef[j + 12];
			coef[j] = a2 + b2;
			coef[j + 4] = c2 + d2;
			coef[j + 8] = a2 - b2;
			coef[j + 12] = d2 - c2;
		}
		for (int i = 0; i < 16; i += 4)
		{
			int a1 = coef[i] + coef[i + 3];
			int b1 = coef[i + 1] + coef[i + 2];
			int c1 = coef[i + 1] - coef[i + 2];
			int d1 = coef[i] - coef[i + 3];
			int a3 = a1 + b1;
			int b3 = c1 + d1;
			int c3 = a1 - b1;
			int d3 = d1 - c1;
			coef[i] = a3 + 3 >> 3;
			coef[i + 1] = b3 + 3 >> 3;
			coef[i + 2] = c3 + 3 >> 3;
			coef[i + 3] = d3 + 3 >> 3;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public VPXDCT()
	{
	}

	[LineNumberTable(new byte[] { 159, 123, 66, 107 })]
	static VPXDCT()
	{
		cospi8sqrt2minus1 = 20091;
		sinpi8sqrt2 = 35468;
	}
}
