using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.ps;

internal interface PSHuffmanTables
{
	static readonly int[][] f_huff_iid_def;

	static readonly int[][] t_huff_iid_def;

	static readonly int[][] f_huff_iid_fine;

	static readonly int[][] t_huff_iid_fine;

	static readonly int[][] f_huff_icc;

	static readonly int[][] t_huff_icc;

	static readonly int[][] f_huff_ipd;

	static readonly int[][] t_huff_ipd;

	static readonly int[][] f_huff_opd;

	static readonly int[][] t_huff_opd;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[]
	{
		159,
		139,
		98,
		byte.MaxValue,
		161,
		205,
		94,
		byte.MaxValue,
		161,
		205,
		94,
		byte.MaxValue,
		164,
		76,
		126,
		byte.MaxValue,
		164,
		76,
		126,
		byte.MaxValue,
		160,
		181,
		80,
		byte.MaxValue,
		160,
		181,
		80,
		byte.MaxValue,
		108,
		73,
		byte.MaxValue,
		108,
		73,
		byte.MaxValue,
		108,
		73
	})]
	static PSHuffmanTables()
	{
		f_huff_iid_def = new int[28][]
		{
			new int[2] { -31, 1 },
			new int[2] { 2, 3 },
			new int[2] { -30, -32 },
			new int[2] { 4, 5 },
			new int[2] { -29, -33 },
			new int[2] { 6, 7 },
			new int[2] { -28, -34 },
			new int[2] { 8, 9 },
			new int[2] { -35, -27 },
			new int[2] { -26, 10 },
			new int[2] { -36, 11 },
			new int[2] { -25, 12 },
			new int[2] { -37, 13 },
			new int[2] { -38, 14 },
			new int[2] { -24, 15 },
			new int[2] { 16, 17 },
			new int[2] { -23, -39 },
			new int[2] { 18, 19 },
			new int[2] { -22, -21 },
			new int[2] { 20, 21 },
			new int[2] { -40, -20 },
			new int[2] { 22, 23 },
			new int[2] { -41, 24 },
			new int[2] { 25, 26 },
			new int[2] { -42, -45 },
			new int[2] { -44, -43 },
			new int[2] { -19, 27 },
			new int[2] { -18, -17 }
		};
		t_huff_iid_def = new int[28][]
		{
			new int[2] { -31, 1 },
			new int[2] { -32, 2 },
			new int[2] { -30, 3 },
			new int[2] { -33, 4 },
			new int[2] { -29, 5 },
			new int[2] { -34, 6 },
			new int[2] { -28, 7 },
			new int[2] { -35, 8 },
			new int[2] { -27, 9 },
			new int[2] { -36, 10 },
			new int[2] { -26, 11 },
			new int[2] { -37, 12 },
			new int[2] { -25, 13 },
			new int[2] { -24, 14 },
			new int[2] { -38, 15 },
			new int[2] { 16, 17 },
			new int[2] { -23, -39 },
			new int[2] { 18, 19 },
			new int[2] { 20, 21 },
			new int[2] { 22, 23 },
			new int[2] { -22, -45 },
			new int[2] { -44, -43 },
			new int[2] { 24, 25 },
			new int[2] { 26, 27 },
			new int[2] { -42, -41 },
			new int[2] { -40, -21 },
			new int[2] { -20, -19 },
			new int[2] { -18, -17 }
		};
		f_huff_iid_fine = new int[60][]
		{
			new int[2] { 1, -31 },
			new int[2] { 2, 3 },
			new int[2] { 4, -32 },
			new int[2] { -30, 5 },
			new int[2] { -33, -29 },
			new int[2] { 6, 7 },
			new int[2] { -34, -28 },
			new int[2] { 8, 9 },
			new int[2] { -35, -27 },
			new int[2] { 10, 11 },
			new int[2] { -36, -26 },
			new int[2] { 12, 13 },
			new int[2] { -37, -25 },
			new int[2] { 14, 15 },
			new int[2] { -24, 16 },
			new int[2] { 17, 18 },
			new int[2] { 19, -39 },
			new int[2] { -23, 20 },
			new int[2] { 21, -38 },
			new int[2] { -21, 22 },
			new int[2] { 23, -40 },
			new int[2] { -22, 24 },
			new int[2] { -42, -20 },
			new int[2] { 25, 26 },
			new int[2] { 27, -41 },
			new int[2] { 28, -43 },
			new int[2] { -19, 29 },
			new int[2] { 30, 31 },
			new int[2] { 32, -45 },
			new int[2] { -17, 33 },
			new int[2] { 34, -44 },
			new int[2] { -18, 35 },
			new int[2] { 36, 37 },
			new int[2] { 38, -46 },
			new int[2] { -16, 39 },
			new int[2] { 40, 41 },
			new int[2] { 42, 43 },
			new int[2] { -48, -14 },
			new int[2] { 44, 45 },
			new int[2] { 46, 47 },
			new int[2] { 48, 49 },
			new int[2] { -47, -15 },
			new int[2] { -52, -10 },
			new int[2] { -50, -12 },
			new int[2] { -49, -13 },
			new int[2] { 50, 51 },
			new int[2] { 52, 53 },
			new int[2] { 54, 55 },
			new int[2] { 56, 57 },
			new int[2] { 58, 59 },
			new int[2] { -57, -56 },
			new int[2] { -59, -58 },
			new int[2] { -53, -9 },
			new int[2] { -55, -54 },
			new int[2] { -6, -5 },
			new int[2] { -8, -7 },
			new int[2] { -2, -1 },
			new int[2] { -4, -3 },
			new int[2] { -61, -60 },
			new int[2] { -51, -11 }
		};
		t_huff_iid_fine = new int[60][]
		{
			new int[2] { 1, -31 },
			new int[2] { -30, 2 },
			new int[2] { 3, -32 },
			new int[2] { 4, 5 },
			new int[2] { 6, 7 },
			new int[2] { -33, -29 },
			new int[2] { 8, -34 },
			new int[2] { -28, 9 },
			new int[2] { -35, -27 },
			new int[2] { 10, 11 },
			new int[2] { -26, 12 },
			new int[2] { 13, 14 },
			new int[2] { -37, -25 },
			new int[2] { 15, 16 },
			new int[2] { 17, -36 },
			new int[2] { 18, -38 },
			new int[2] { -24, 19 },
			new int[2] { 20, 21 },
			new int[2] { -22, 22 },
			new int[2] { 23, 24 },
			new int[2] { -39, -23 },
			new int[2] { 25, 26 },
			new int[2] { -20, 27 },
			new int[2] { 28, 29 },
			new int[2] { -41, -21 },
			new int[2] { 30, 31 },
			new int[2] { 32, -40 },
			new int[2] { 33, -44 },
			new int[2] { -18, 34 },
			new int[2] { 35, 36 },
			new int[2] { 37, -43 },
			new int[2] { -19, 38 },
			new int[2] { 39, -42 },
			new int[2] { 40, 41 },
			new int[2] { 42, 43 },
			new int[2] { 44, 45 },
			new int[2] { 46, -46 },
			new int[2] { -16, 47 },
			new int[2] { -45, -17 },
			new int[2] { 48, 49 },
			new int[2] { -52, -51 },
			new int[2] { -13, -12 },
			new int[2] { -50, -49 },
			new int[2] { 50, 51 },
			new int[2] { 52, 53 },
			new int[2] { 54, 55 },
			new int[2] { 56, -48 },
			new int[2] { -14, 57 },
			new int[2] { 58, -47 },
			new int[2] { -15, 59 },
			new int[2] { -57, -5 },
			new int[2] { -59, -58 },
			new int[2] { -2, -1 },
			new int[2] { -4, -3 },
			new int[2] { -61, -60 },
			new int[2] { -56, -6 },
			new int[2] { -55, -7 },
			new int[2] { -54, -8 },
			new int[2] { -53, -9 },
			new int[2] { -11, -10 }
		};
		f_huff_icc = new int[14][]
		{
			new int[2] { -31, 1 },
			new int[2] { -30, 2 },
			new int[2] { -32, 3 },
			new int[2] { -29, 4 },
			new int[2] { -33, 5 },
			new int[2] { -28, 6 },
			new int[2] { -34, 7 },
			new int[2] { -27, 8 },
			new int[2] { -26, 9 },
			new int[2] { -35, 10 },
			new int[2] { -25, 11 },
			new int[2] { -36, 12 },
			new int[2] { -24, 13 },
			new int[2] { -37, -38 }
		};
		t_huff_icc = new int[14][]
		{
			new int[2] { -31, 1 },
			new int[2] { -30, 2 },
			new int[2] { -32, 3 },
			new int[2] { -29, 4 },
			new int[2] { -33, 5 },
			new int[2] { -28, 6 },
			new int[2] { -34, 7 },
			new int[2] { -27, 8 },
			new int[2] { -35, 9 },
			new int[2] { -26, 10 },
			new int[2] { -36, 11 },
			new int[2] { -25, 12 },
			new int[2] { -37, 13 },
			new int[2] { -38, -24 }
		};
		f_huff_ipd = new int[7][]
		{
			new int[2] { 1, -31 },
			new int[2] { 2, 3 },
			new int[2] { -30, 4 },
			new int[2] { 5, 6 },
			new int[2] { -27, -26 },
			new int[2] { -28, -25 },
			new int[2] { -29, -24 }
		};
		t_huff_ipd = new int[7][]
		{
			new int[2] { 1, -31 },
			new int[2] { 2, 3 },
			new int[2] { 4, 5 },
			new int[2] { -30, -24 },
			new int[2] { -26, 6 },
			new int[2] { -29, -25 },
			new int[2] { -27, -28 }
		};
		f_huff_opd = new int[7][]
		{
			new int[2] { 1, -31 },
			new int[2] { 2, 3 },
			new int[2] { -24, -30 },
			new int[2] { 4, 5 },
			new int[2] { -28, -25 },
			new int[2] { -29, 6 },
			new int[2] { -26, -27 }
		};
		t_huff_opd = new int[7][]
		{
			new int[2] { 1, -31 },
			new int[2] { 2, 3 },
			new int[2] { 4, 5 },
			new int[2] { -30, -24 },
			new int[2] { -26, -29 },
			new int[2] { -25, 6 },
			new int[2] { -27, -28 }
		};
	}
}
