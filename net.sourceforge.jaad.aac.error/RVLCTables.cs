using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.error;

internal interface RVLCTables
{
	static readonly int[][] RVLC_BOOK;

	static readonly int[][] ESCAPE_BOOK;

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
		245,
		90
	})]
	static RVLCTables()
	{
		RVLC_BOOK = new int[24][]
		{
			new int[3] { 0, 1, 0 },
			new int[3] { -1, 3, 5 },
			new int[3] { 1, 3, 7 },
			new int[3] { -2, 4, 9 },
			new int[3] { -3, 5, 17 },
			new int[3] { 2, 5, 27 },
			new int[3] { -4, 6, 33 },
			new int[3] { 99, 6, 50 },
			new int[3] { 3, 6, 51 },
			new int[3] { 99, 6, 52 },
			new int[3] { -7, 7, 65 },
			new int[3] { 99, 7, 96 },
			new int[3] { 99, 7, 98 },
			new int[3] { 7, 7, 99 },
			new int[3] { 4, 7, 107 },
			new int[3] { -5, 8, 129 },
			new int[3] { 99, 8, 194 },
			new int[3] { 5, 8, 195 },
			new int[3] { 99, 8, 212 },
			new int[3] { 99, 9, 256 },
			new int[3] { -6, 9, 257 },
			new int[3] { 99, 9, 426 },
			new int[3] { 6, 9, 427 },
			new int[3] { 99, 10, 0 }
		};
		ESCAPE_BOOK = new int[55][]
		{
			new int[3] { 1, 2, 0 },
			new int[3] { 0, 2, 2 },
			new int[3] { 3, 3, 2 },
			new int[3] { 2, 3, 6 },
			new int[3] { 4, 4, 14 },
			new int[3] { 7, 5, 13 },
			new int[3] { 6, 5, 15 },
			new int[3] { 5, 5, 31 },
			new int[3] { 11, 6, 24 },
			new int[3] { 10, 6, 25 },
			new int[3] { 9, 6, 29 },
			new int[3] { 8, 6, 61 },
			new int[3] { 13, 7, 56 },
			new int[3] { 12, 7, 120 },
			new int[3] { 15, 8, 114 },
			new int[3] { 14, 8, 242 },
			new int[3] { 17, 9, 230 },
			new int[3] { 16, 9, 486 },
			new int[3] { 19, 10, 463 },
			new int[3] { 18, 10, 974 },
			new int[3] { 22, 11, 925 },
			new int[3] { 20, 11, 1950 },
			new int[3] { 21, 11, 1951 },
			new int[3] { 23, 12, 1848 },
			new int[3] { 25, 13, 3698 },
			new int[3] { 24, 14, 7399 },
			new int[3] { 26, 15, 14797 },
			new int[3] { 49, 19, 236736 },
			new int[3] { 50, 19, 236737 },
			new int[3] { 51, 19, 236738 },
			new int[3] { 52, 19, 236739 },
			new int[3] { 53, 19, 236740 },
			new int[3] { 27, 20, 473482 },
			new int[3] { 28, 20, 473483 },
			new int[3] { 29, 20, 473484 },
			new int[3] { 30, 20, 473485 },
			new int[3] { 31, 20, 473486 },
			new int[3] { 32, 20, 473487 },
			new int[3] { 33, 20, 473488 },
			new int[3] { 34, 20, 473489 },
			new int[3] { 35, 20, 473490 },
			new int[3] { 36, 20, 473491 },
			new int[3] { 37, 20, 473492 },
			new int[3] { 38, 20, 473493 },
			new int[3] { 39, 20, 473494 },
			new int[3] { 40, 20, 473495 },
			new int[3] { 41, 20, 473496 },
			new int[3] { 42, 20, 473497 },
			new int[3] { 43, 20, 473498 },
			new int[3] { 44, 20, 473499 },
			new int[3] { 45, 20, 473500 },
			new int[3] { 46, 20, 473501 },
			new int[3] { 47, 20, 473502 },
			new int[3] { 48, 20, 473503 },
			new int[3] { 99, 21, 0 }
		};
	}
}
