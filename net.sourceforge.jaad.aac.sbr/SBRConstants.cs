using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.sbr;

internal interface SBRConstants
{
	static readonly int[] startMinTable;

	static readonly int[] offsetIndexTable;

	static readonly int[][] OFFSET;

	const int EXTENSION_ID_PS = 2;

	const int MAX_NTSRHFG = 40;

	const int MAX_NTSR = 32;

	const int MAX_M = 49;

	const int MAX_L_E = 5;

	const int EXT_SBR_DATA = 13;

	const int EXT_SBR_DATA_CRC = 14;

	const int FIXFIX = 0;

	const int FIXVAR = 1;

	const int VARFIX = 2;

	const int VARVAR = 3;

	const int LO_RES = 0;

	const int HI_RES = 1;

	const int NO_TIME_SLOTS_960 = 15;

	const int NO_TIME_SLOTS = 16;

	const int RATE = 2;

	const int NOISE_FLOOR_OFFSET = 6;

	const int T_HFGEN = 8;

	const int T_HFADJ = 2;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[] { 159, 139, 66, 159, 43, 159, 33 })]
	static SBRConstants()
	{
		startMinTable = new int[12]
		{
			7, 7, 10, 11, 12, 16, 16, 17, 24, 32,
			35, 48
		};
		offsetIndexTable = new int[12]
		{
			5, 5, 4, 4, 4, 3, 2, 1, 0, 6,
			6, 6
		};
		OFFSET = new int[7][]
		{
			new int[16]
			{
				-8, -7, -6, -5, -4, -3, -2, -1, 0, 1,
				2, 3, 4, 5, 6, 7
			},
			new int[16]
			{
				-5, -4, -3, -2, -1, 0, 1, 2, 3, 4,
				5, 6, 7, 9, 11, 13
			},
			new int[16]
			{
				-5, -3, -2, -1, 0, 1, 2, 3, 4, 5,
				6, 7, 9, 11, 13, 16
			},
			new int[16]
			{
				-6, -4, -2, -1, 0, 1, 2, 3, 4, 5,
				6, 7, 9, 11, 13, 16
			},
			new int[16]
			{
				-4, -2, -1, 0, 1, 2, 3, 4, 5, 6,
				7, 9, 11, 13, 16, 20
			},
			new int[16]
			{
				-2, -1, 0, 1, 2, 3, 4, 5, 6, 7,
				9, 11, 13, 16, 20, 24
			},
			new int[16]
			{
				0, 1, 2, 3, 4, 5, 6, 7, 9, 11,
				13, 16, 20, 24, 28, 33
			}
		};
	}
}
