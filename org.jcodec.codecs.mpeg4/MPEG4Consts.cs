using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.mpeg4;

public class MPEG4Consts : Object
{
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static Macroblock.Vector ZERO_MV;

	internal const int BS_VERSION_BUGGY_DC_CLIP = 34;

	internal const int MODE_INTER = 0;

	internal const int MODE_INTER_Q = 1;

	internal const int MODE_INTER4V = 2;

	internal const int MODE_INTRA = 3;

	internal const int MODE_INTRA_Q = 4;

	internal const int MODE_NOT_CODED = 16;

	internal const int MODE_NOT_CODED_GMC = 17;

	internal const int MODE_DIRECT = 0;

	internal const int MODE_INTERPOLATE = 1;

	internal const int MODE_BACKWARD = 2;

	internal const int MODE_FORWARD = 3;

	internal const int MODE_DIRECT_NONE_MV = 4;

	internal const int MODE_DIRECT_NO4V = 5;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[] ROUNDTAB_76;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[] ROUNDTAB_79;

	internal const int ALT_CHROMA_ROUNDING = 1;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[] INTRA_DC_THRESHOLD_TABLE;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static short[] DEFAULT_INTRA_MATRIX;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static short[] DEFAULT_INTER_MATRIX;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[][] MCBPC_INTRA_TABLE;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[][] MCBPC_INTER_TABLE;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[][] CBPY_TABLE;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[][] TMNMV_TAB_0;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[][] TMNMV_TAB_1;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[][] TMNMV_TAB_2;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static byte[][][] MAX_LEVEL;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static byte[][][] MAX_RUN;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[][][] COEFF_TAB;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static short[][] SCAN_TABLES;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static short[] DEFAULT_ACDC_VALUES;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[][] DC_LUM_TAB;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[][] FILTER_TAB;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[][] SPRITE_TRAJECTORY_LEN;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public MPEG4Consts()
	{
	}

	[LineNumberTable(new byte[]
	{
		159,
		139,
		98,
		235,
		80,
		127,
		53,
		220,
		127,
		20,
		byte.MaxValue,
		161,
		36,
		75,
		byte.MaxValue,
		161,
		37,
		75,
		byte.MaxValue,
		164,
		64,
		75,
		byte.MaxValue,
		179,
		172,
		100,
		byte.MaxValue,
		164,
		74,
		75,
		byte.MaxValue,
		160,
		164,
		69,
		byte.MaxValue,
		166,
		171,
		91,
		byte.MaxValue,
		169,
		85,
		98,
		byte.MaxValue,
		164,
		198,
		115,
		byte.MaxValue,
		164,
		201,
		114,
		byte.MaxValue,
		185,
		41,
		160,
		211,
		byte.MaxValue,
		164,
		21,
		94,
		159,
		52,
		byte.MaxValue,
		117,
		69,
		byte.MaxValue,
		160,
		72,
		70
	})]
	static MPEG4Consts()
	{
		ZERO_MV = Macroblock.vec();
		ROUNDTAB_76 = new int[16]
		{
			0, 0, 0, 1, 1, 1, 1, 1, 0, 0,
			0, 0, 0, 0, 1, 1
		};
		ROUNDTAB_79 = new int[4] { 0, 1, 0, 0 };
		INTRA_DC_THRESHOLD_TABLE = new int[8] { 32, 13, 15, 17, 19, 21, 23, 1 };
		DEFAULT_INTRA_MATRIX = new short[64]
		{
			8, 17, 18, 19, 21, 23, 25, 27, 17, 18,
			19, 21, 23, 25, 27, 28, 20, 21, 22, 23,
			24, 26, 28, 30, 21, 22, 23, 24, 26, 28,
			30, 32, 22, 23, 24, 26, 28, 30, 32, 35,
			23, 24, 26, 28, 30, 32, 35, 38, 25, 26,
			28, 30, 32, 35, 38, 41, 27, 28, 30, 32,
			35, 38, 41, 45
		};
		DEFAULT_INTER_MATRIX = new short[64]
		{
			16, 17, 18, 19, 20, 21, 22, 23, 17, 18,
			19, 20, 21, 22, 23, 24, 18, 19, 20, 21,
			22, 23, 24, 25, 19, 20, 21, 22, 23, 24,
			26, 27, 20, 21, 22, 23, 25, 26, 27, 28,
			21, 22, 23, 24, 26, 27, 28, 30, 22, 23,
			24, 26, 27, 28, 30, 31, 23, 24, 25, 27,
			28, 30, 31, 33
		};
		MCBPC_INTRA_TABLE = new int[64][]
		{
			new int[2] { -1, 0 },
			new int[2] { 20, 6 },
			new int[2] { 36, 6 },
			new int[2] { 52, 6 },
			new int[2] { 4, 4 },
			new int[2] { 4, 4 },
			new int[2] { 4, 4 },
			new int[2] { 4, 4 },
			new int[2] { 19, 3 },
			new int[2] { 19, 3 },
			new int[2] { 19, 3 },
			new int[2] { 19, 3 },
			new int[2] { 19, 3 },
			new int[2] { 19, 3 },
			new int[2] { 19, 3 },
			new int[2] { 19, 3 },
			new int[2] { 35, 3 },
			new int[2] { 35, 3 },
			new int[2] { 35, 3 },
			new int[2] { 35, 3 },
			new int[2] { 35, 3 },
			new int[2] { 35, 3 },
			new int[2] { 35, 3 },
			new int[2] { 35, 3 },
			new int[2] { 51, 3 },
			new int[2] { 51, 3 },
			new int[2] { 51, 3 },
			new int[2] { 51, 3 },
			new int[2] { 51, 3 },
			new int[2] { 51, 3 },
			new int[2] { 51, 3 },
			new int[2] { 51, 3 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 },
			new int[2] { 3, 1 }
		};
		MCBPC_INTER_TABLE = new int[257][]
		{
			new int[2] { -1, 0 },
			new int[2] { 255, 9 },
			new int[2] { 52, 9 },
			new int[2] { 36, 9 },
			new int[2] { 20, 9 },
			new int[2] { 49, 9 },
			new int[2] { 35, 8 },
			new int[2] { 35, 8 },
			new int[2] { 19, 8 },
			new int[2] { 19, 8 },
			new int[2] { 50, 8 },
			new int[2] { 50, 8 },
			new int[2] { 51, 7 },
			new int[2] { 51, 7 },
			new int[2] { 51, 7 },
			new int[2] { 51, 7 },
			new int[2] { 34, 7 },
			new int[2] { 34, 7 },
			new int[2] { 34, 7 },
			new int[2] { 34, 7 },
			new int[2] { 18, 7 },
			new int[2] { 18, 7 },
			new int[2] { 18, 7 },
			new int[2] { 18, 7 },
			new int[2] { 33, 7 },
			new int[2] { 33, 7 },
			new int[2] { 33, 7 },
			new int[2] { 33, 7 },
			new int[2] { 17, 7 },
			new int[2] { 17, 7 },
			new int[2] { 17, 7 },
			new int[2] { 17, 7 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 48, 6 },
			new int[2] { 48, 6 },
			new int[2] { 48, 6 },
			new int[2] { 48, 6 },
			new int[2] { 48, 6 },
			new int[2] { 48, 6 },
			new int[2] { 48, 6 },
			new int[2] { 48, 6 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 3, 5 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 32, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 16, 4 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 1, 3 },
			new int[2] { 0, 1 }
		};
		CBPY_TABLE = new int[64][]
		{
			new int[2] { -1, 0 },
			new int[2] { -1, 0 },
			new int[2] { 6, 6 },
			new int[2] { 9, 6 },
			new int[2] { 8, 5 },
			new int[2] { 8, 5 },
			new int[2] { 4, 5 },
			new int[2] { 4, 5 },
			new int[2] { 2, 5 },
			new int[2] { 2, 5 },
			new int[2] { 1, 5 },
			new int[2] { 1, 5 },
			new int[2] { 0, 4 },
			new int[2] { 0, 4 },
			new int[2] { 0, 4 },
			new int[2] { 0, 4 },
			new int[2] { 12, 4 },
			new int[2] { 12, 4 },
			new int[2] { 12, 4 },
			new int[2] { 12, 4 },
			new int[2] { 10, 4 },
			new int[2] { 10, 4 },
			new int[2] { 10, 4 },
			new int[2] { 10, 4 },
			new int[2] { 14, 4 },
			new int[2] { 14, 4 },
			new int[2] { 14, 4 },
			new int[2] { 14, 4 },
			new int[2] { 5, 4 },
			new int[2] { 5, 4 },
			new int[2] { 5, 4 },
			new int[2] { 5, 4 },
			new int[2] { 13, 4 },
			new int[2] { 13, 4 },
			new int[2] { 13, 4 },
			new int[2] { 13, 4 },
			new int[2] { 3, 4 },
			new int[2] { 3, 4 },
			new int[2] { 3, 4 },
			new int[2] { 3, 4 },
			new int[2] { 11, 4 },
			new int[2] { 11, 4 },
			new int[2] { 11, 4 },
			new int[2] { 11, 4 },
			new int[2] { 7, 4 },
			new int[2] { 7, 4 },
			new int[2] { 7, 4 },
			new int[2] { 7, 4 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 },
			new int[2] { 15, 2 }
		};
		TMNMV_TAB_0 = new int[14][]
		{
			new int[2] { 3, 4 },
			new int[2] { -3, 4 },
			new int[2] { 2, 3 },
			new int[2] { 2, 3 },
			new int[2] { -2, 3 },
			new int[2] { -2, 3 },
			new int[2] { 1, 2 },
			new int[2] { 1, 2 },
			new int[2] { 1, 2 },
			new int[2] { 1, 2 },
			new int[2] { -1, 2 },
			new int[2] { -1, 2 },
			new int[2] { -1, 2 },
			new int[2] { -1, 2 }
		};
		TMNMV_TAB_1 = new int[96][]
		{
			new int[2] { 12, 10 },
			new int[2] { -12, 10 },
			new int[2] { 11, 10 },
			new int[2] { -11, 10 },
			new int[2] { 10, 9 },
			new int[2] { 10, 9 },
			new int[2] { -10, 9 },
			new int[2] { -10, 9 },
			new int[2] { 9, 9 },
			new int[2] { 9, 9 },
			new int[2] { -9, 9 },
			new int[2] { -9, 9 },
			new int[2] { 8, 9 },
			new int[2] { 8, 9 },
			new int[2] { -8, 9 },
			new int[2] { -8, 9 },
			new int[2] { 7, 7 },
			new int[2] { 7, 7 },
			new int[2] { 7, 7 },
			new int[2] { 7, 7 },
			new int[2] { 7, 7 },
			new int[2] { 7, 7 },
			new int[2] { 7, 7 },
			new int[2] { 7, 7 },
			new int[2] { -7, 7 },
			new int[2] { -7, 7 },
			new int[2] { -7, 7 },
			new int[2] { -7, 7 },
			new int[2] { -7, 7 },
			new int[2] { -7, 7 },
			new int[2] { -7, 7 },
			new int[2] { -7, 7 },
			new int[2] { 6, 7 },
			new int[2] { 6, 7 },
			new int[2] { 6, 7 },
			new int[2] { 6, 7 },
			new int[2] { 6, 7 },
			new int[2] { 6, 7 },
			new int[2] { 6, 7 },
			new int[2] { 6, 7 },
			new int[2] { -6, 7 },
			new int[2] { -6, 7 },
			new int[2] { -6, 7 },
			new int[2] { -6, 7 },
			new int[2] { -6, 7 },
			new int[2] { -6, 7 },
			new int[2] { -6, 7 },
			new int[2] { -6, 7 },
			new int[2] { 5, 7 },
			new int[2] { 5, 7 },
			new int[2] { 5, 7 },
			new int[2] { 5, 7 },
			new int[2] { 5, 7 },
			new int[2] { 5, 7 },
			new int[2] { 5, 7 },
			new int[2] { 5, 7 },
			new int[2] { -5, 7 },
			new int[2] { -5, 7 },
			new int[2] { -5, 7 },
			new int[2] { -5, 7 },
			new int[2] { -5, 7 },
			new int[2] { -5, 7 },
			new int[2] { -5, 7 },
			new int[2] { -5, 7 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { 4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 },
			new int[2] { -4, 6 }
		};
		TMNMV_TAB_2 = new int[124][]
		{
			new int[2] { 32, 12 },
			new int[2] { -32, 12 },
			new int[2] { 31, 12 },
			new int[2] { -31, 12 },
			new int[2] { 30, 11 },
			new int[2] { 30, 11 },
			new int[2] { -30, 11 },
			new int[2] { -30, 11 },
			new int[2] { 29, 11 },
			new int[2] { 29, 11 },
			new int[2] { -29, 11 },
			new int[2] { -29, 11 },
			new int[2] { 28, 11 },
			new int[2] { 28, 11 },
			new int[2] { -28, 11 },
			new int[2] { -28, 11 },
			new int[2] { 27, 11 },
			new int[2] { 27, 11 },
			new int[2] { -27, 11 },
			new int[2] { -27, 11 },
			new int[2] { 26, 11 },
			new int[2] { 26, 11 },
			new int[2] { -26, 11 },
			new int[2] { -26, 11 },
			new int[2] { 25, 11 },
			new int[2] { 25, 11 },
			new int[2] { -25, 11 },
			new int[2] { -25, 11 },
			new int[2] { 24, 10 },
			new int[2] { 24, 10 },
			new int[2] { 24, 10 },
			new int[2] { 24, 10 },
			new int[2] { -24, 10 },
			new int[2] { -24, 10 },
			new int[2] { -24, 10 },
			new int[2] { -24, 10 },
			new int[2] { 23, 10 },
			new int[2] { 23, 10 },
			new int[2] { 23, 10 },
			new int[2] { 23, 10 },
			new int[2] { -23, 10 },
			new int[2] { -23, 10 },
			new int[2] { -23, 10 },
			new int[2] { -23, 10 },
			new int[2] { 22, 10 },
			new int[2] { 22, 10 },
			new int[2] { 22, 10 },
			new int[2] { 22, 10 },
			new int[2] { -22, 10 },
			new int[2] { -22, 10 },
			new int[2] { -22, 10 },
			new int[2] { -22, 10 },
			new int[2] { 21, 10 },
			new int[2] { 21, 10 },
			new int[2] { 21, 10 },
			new int[2] { 21, 10 },
			new int[2] { -21, 10 },
			new int[2] { -21, 10 },
			new int[2] { -21, 10 },
			new int[2] { -21, 10 },
			new int[2] { 20, 10 },
			new int[2] { 20, 10 },
			new int[2] { 20, 10 },
			new int[2] { 20, 10 },
			new int[2] { -20, 10 },
			new int[2] { -20, 10 },
			new int[2] { -20, 10 },
			new int[2] { -20, 10 },
			new int[2] { 19, 10 },
			new int[2] { 19, 10 },
			new int[2] { 19, 10 },
			new int[2] { 19, 10 },
			new int[2] { -19, 10 },
			new int[2] { -19, 10 },
			new int[2] { -19, 10 },
			new int[2] { -19, 10 },
			new int[2] { 18, 10 },
			new int[2] { 18, 10 },
			new int[2] { 18, 10 },
			new int[2] { 18, 10 },
			new int[2] { -18, 10 },
			new int[2] { -18, 10 },
			new int[2] { -18, 10 },
			new int[2] { -18, 10 },
			new int[2] { 17, 10 },
			new int[2] { 17, 10 },
			new int[2] { 17, 10 },
			new int[2] { 17, 10 },
			new int[2] { -17, 10 },
			new int[2] { -17, 10 },
			new int[2] { -17, 10 },
			new int[2] { -17, 10 },
			new int[2] { 16, 10 },
			new int[2] { 16, 10 },
			new int[2] { 16, 10 },
			new int[2] { 16, 10 },
			new int[2] { -16, 10 },
			new int[2] { -16, 10 },
			new int[2] { -16, 10 },
			new int[2] { -16, 10 },
			new int[2] { 15, 10 },
			new int[2] { 15, 10 },
			new int[2] { 15, 10 },
			new int[2] { 15, 10 },
			new int[2] { -15, 10 },
			new int[2] { -15, 10 },
			new int[2] { -15, 10 },
			new int[2] { -15, 10 },
			new int[2] { 14, 10 },
			new int[2] { 14, 10 },
			new int[2] { 14, 10 },
			new int[2] { 14, 10 },
			new int[2] { -14, 10 },
			new int[2] { -14, 10 },
			new int[2] { -14, 10 },
			new int[2] { -14, 10 },
			new int[2] { 13, 10 },
			new int[2] { 13, 10 },
			new int[2] { 13, 10 },
			new int[2] { 13, 10 },
			new int[2] { -13, 10 },
			new int[2] { -13, 10 },
			new int[2] { -13, 10 },
			new int[2] { -13, 10 }
		};
		MAX_LEVEL = new byte[2][][]
		{
			new byte[2][]
			{
				new byte[64]
				{
					12, 6, 4, 3, 3, 3, 3, 2, 2, 2,
					2, 1, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 1, 1, 1, 1, 1, 1, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0
				},
				new byte[64]
				{
					3, 2, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0
				}
			},
			new byte[2][]
			{
				new byte[64]
				{
					27, 10, 5, 4, 3, 3, 3, 3, 2, 2,
					1, 1, 1, 1, 1, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0
				},
				new byte[64]
				{
					8, 3, 2, 2, 2, 2, 2, 1, 1, 1,
					1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0
				}
			}
		};
		MAX_RUN = new byte[2][][]
		{
			new byte[2][]
			{
				new byte[64]
				{
					0, 26, 10, 6, 2, 1, 1, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0
				},
				new byte[64]
				{
					0, 40, 1, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0
				}
			},
			new byte[2][]
			{
				new byte[64]
				{
					0, 14, 9, 7, 3, 2, 1, 1, 1, 1,
					1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0
				},
				new byte[64]
				{
					0, 20, 6, 1, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0
				}
			}
		};
		COEFF_TAB = new int[2][][]
		{
			new int[102][]
			{
				new int[5] { 2, 2, 0, 0, 1 },
				new int[5] { 15, 4, 0, 0, 2 },
				new int[5] { 21, 6, 0, 0, 3 },
				new int[5] { 23, 7, 0, 0, 4 },
				new int[5] { 31, 8, 0, 0, 5 },
				new int[5] { 37, 9, 0, 0, 6 },
				new int[5] { 36, 9, 0, 0, 7 },
				new int[5] { 33, 10, 0, 0, 8 },
				new int[5] { 32, 10, 0, 0, 9 },
				new int[5] { 7, 11, 0, 0, 10 },
				new int[5] { 6, 11, 0, 0, 11 },
				new int[5] { 32, 11, 0, 0, 12 },
				new int[5] { 6, 3, 0, 1, 1 },
				new int[5] { 20, 6, 0, 1, 2 },
				new int[5] { 30, 8, 0, 1, 3 },
				new int[5] { 15, 10, 0, 1, 4 },
				new int[5] { 33, 11, 0, 1, 5 },
				new int[5] { 80, 12, 0, 1, 6 },
				new int[5] { 14, 4, 0, 2, 1 },
				new int[5] { 29, 8, 0, 2, 2 },
				new int[5] { 14, 10, 0, 2, 3 },
				new int[5] { 81, 12, 0, 2, 4 },
				new int[5] { 13, 5, 0, 3, 1 },
				new int[5] { 35, 9, 0, 3, 2 },
				new int[5] { 13, 10, 0, 3, 3 },
				new int[5] { 12, 5, 0, 4, 1 },
				new int[5] { 34, 9, 0, 4, 2 },
				new int[5] { 82, 12, 0, 4, 3 },
				new int[5] { 11, 5, 0, 5, 1 },
				new int[5] { 12, 10, 0, 5, 2 },
				new int[5] { 83, 12, 0, 5, 3 },
				new int[5] { 19, 6, 0, 6, 1 },
				new int[5] { 11, 10, 0, 6, 2 },
				new int[5] { 84, 12, 0, 6, 3 },
				new int[5] { 18, 6, 0, 7, 1 },
				new int[5] { 10, 10, 0, 7, 2 },
				new int[5] { 17, 6, 0, 8, 1 },
				new int[5] { 9, 10, 0, 8, 2 },
				new int[5] { 16, 6, 0, 9, 1 },
				new int[5] { 8, 10, 0, 9, 2 },
				new int[5] { 22, 7, 0, 10, 1 },
				new int[5] { 85, 12, 0, 10, 2 },
				new int[5] { 21, 7, 0, 11, 1 },
				new int[5] { 20, 7, 0, 12, 1 },
				new int[5] { 28, 8, 0, 13, 1 },
				new int[5] { 27, 8, 0, 14, 1 },
				new int[5] { 33, 9, 0, 15, 1 },
				new int[5] { 32, 9, 0, 16, 1 },
				new int[5] { 31, 9, 0, 17, 1 },
				new int[5] { 30, 9, 0, 18, 1 },
				new int[5] { 29, 9, 0, 19, 1 },
				new int[5] { 28, 9, 0, 20, 1 },
				new int[5] { 27, 9, 0, 21, 1 },
				new int[5] { 26, 9, 0, 22, 1 },
				new int[5] { 34, 11, 0, 23, 1 },
				new int[5] { 35, 11, 0, 24, 1 },
				new int[5] { 86, 12, 0, 25, 1 },
				new int[5] { 87, 12, 0, 26, 1 },
				new int[5] { 7, 4, 1, 0, 1 },
				new int[5] { 25, 9, 1, 0, 2 },
				new int[5] { 5, 11, 1, 0, 3 },
				new int[5] { 15, 6, 1, 1, 1 },
				new int[5] { 4, 11, 1, 1, 2 },
				new int[5] { 14, 6, 1, 2, 1 },
				new int[5] { 13, 6, 1, 3, 1 },
				new int[5] { 12, 6, 1, 4, 1 },
				new int[5] { 19, 7, 1, 5, 1 },
				new int[5] { 18, 7, 1, 6, 1 },
				new int[5] { 17, 7, 1, 7, 1 },
				new int[5] { 16, 7, 1, 8, 1 },
				new int[5] { 26, 8, 1, 9, 1 },
				new int[5] { 25, 8, 1, 10, 1 },
				new int[5] { 24, 8, 1, 11, 1 },
				new int[5] { 23, 8, 1, 12, 1 },
				new int[5] { 22, 8, 1, 13, 1 },
				new int[5] { 21, 8, 1, 14, 1 },
				new int[5] { 20, 8, 1, 15, 1 },
				new int[5] { 19, 8, 1, 16, 1 },
				new int[5] { 24, 9, 1, 17, 1 },
				new int[5] { 23, 9, 1, 18, 1 },
				new int[5] { 22, 9, 1, 19, 1 },
				new int[5] { 21, 9, 1, 20, 1 },
				new int[5] { 20, 9, 1, 21, 1 },
				new int[5] { 19, 9, 1, 22, 1 },
				new int[5] { 18, 9, 1, 23, 1 },
				new int[5] { 17, 9, 1, 24, 1 },
				new int[5] { 7, 10, 1, 25, 1 },
				new int[5] { 6, 10, 1, 26, 1 },
				new int[5] { 5, 10, 1, 27, 1 },
				new int[5] { 4, 10, 1, 28, 1 },
				new int[5] { 36, 11, 1, 29, 1 },
				new int[5] { 37, 11, 1, 30, 1 },
				new int[5] { 38, 11, 1, 31, 1 },
				new int[5] { 39, 11, 1, 32, 1 },
				new int[5] { 88, 12, 1, 33, 1 },
				new int[5] { 89, 12, 1, 34, 1 },
				new int[5] { 90, 12, 1, 35, 1 },
				new int[5] { 91, 12, 1, 36, 1 },
				new int[5] { 92, 12, 1, 37, 1 },
				new int[5] { 93, 12, 1, 38, 1 },
				new int[5] { 94, 12, 1, 39, 1 },
				new int[5] { 95, 12, 1, 40, 1 }
			},
			new int[102][]
			{
				new int[5] { 2, 2, 0, 0, 1 },
				new int[5] { 15, 4, 0, 0, 3 },
				new int[5] { 21, 6, 0, 0, 6 },
				new int[5] { 23, 7, 0, 0, 9 },
				new int[5] { 31, 8, 0, 0, 10 },
				new int[5] { 37, 9, 0, 0, 13 },
				new int[5] { 36, 9, 0, 0, 14 },
				new int[5] { 33, 10, 0, 0, 17 },
				new int[5] { 32, 10, 0, 0, 18 },
				new int[5] { 7, 11, 0, 0, 21 },
				new int[5] { 6, 11, 0, 0, 22 },
				new int[5] { 32, 11, 0, 0, 23 },
				new int[5] { 6, 3, 0, 0, 2 },
				new int[5] { 20, 6, 0, 1, 2 },
				new int[5] { 30, 8, 0, 0, 11 },
				new int[5] { 15, 10, 0, 0, 19 },
				new int[5] { 33, 11, 0, 0, 24 },
				new int[5] { 80, 12, 0, 0, 25 },
				new int[5] { 14, 4, 0, 1, 1 },
				new int[5] { 29, 8, 0, 0, 12 },
				new int[5] { 14, 10, 0, 0, 20 },
				new int[5] { 81, 12, 0, 0, 26 },
				new int[5] { 13, 5, 0, 0, 4 },
				new int[5] { 35, 9, 0, 0, 15 },
				new int[5] { 13, 10, 0, 1, 7 },
				new int[5] { 12, 5, 0, 0, 5 },
				new int[5] { 34, 9, 0, 4, 2 },
				new int[5] { 82, 12, 0, 0, 27 },
				new int[5] { 11, 5, 0, 2, 1 },
				new int[5] { 12, 10, 0, 2, 4 },
				new int[5] { 83, 12, 0, 1, 9 },
				new int[5] { 19, 6, 0, 0, 7 },
				new int[5] { 11, 10, 0, 3, 4 },
				new int[5] { 84, 12, 0, 6, 3 },
				new int[5] { 18, 6, 0, 0, 8 },
				new int[5] { 10, 10, 0, 4, 3 },
				new int[5] { 17, 6, 0, 3, 1 },
				new int[5] { 9, 10, 0, 8, 2 },
				new int[5] { 16, 6, 0, 4, 1 },
				new int[5] { 8, 10, 0, 5, 3 },
				new int[5] { 22, 7, 0, 1, 3 },
				new int[5] { 85, 12, 0, 1, 10 },
				new int[5] { 21, 7, 0, 2, 2 },
				new int[5] { 20, 7, 0, 7, 1 },
				new int[5] { 28, 8, 0, 1, 4 },
				new int[5] { 27, 8, 0, 3, 2 },
				new int[5] { 33, 9, 0, 0, 16 },
				new int[5] { 32, 9, 0, 1, 5 },
				new int[5] { 31, 9, 0, 1, 6 },
				new int[5] { 30, 9, 0, 2, 3 },
				new int[5] { 29, 9, 0, 3, 3 },
				new int[5] { 28, 9, 0, 5, 2 },
				new int[5] { 27, 9, 0, 6, 2 },
				new int[5] { 26, 9, 0, 7, 2 },
				new int[5] { 34, 11, 0, 1, 8 },
				new int[5] { 35, 11, 0, 9, 2 },
				new int[5] { 86, 12, 0, 2, 5 },
				new int[5] { 87, 12, 0, 7, 3 },
				new int[5] { 7, 4, 1, 0, 1 },
				new int[5] { 25, 9, 0, 11, 1 },
				new int[5] { 5, 11, 1, 0, 6 },
				new int[5] { 15, 6, 1, 1, 1 },
				new int[5] { 4, 11, 1, 0, 7 },
				new int[5] { 14, 6, 1, 2, 1 },
				new int[5] { 13, 6, 0, 5, 1 },
				new int[5] { 12, 6, 1, 0, 2 },
				new int[5] { 19, 7, 1, 5, 1 },
				new int[5] { 18, 7, 0, 6, 1 },
				new int[5] { 17, 7, 1, 3, 1 },
				new int[5] { 16, 7, 1, 4, 1 },
				new int[5] { 26, 8, 1, 9, 1 },
				new int[5] { 25, 8, 0, 8, 1 },
				new int[5] { 24, 8, 0, 9, 1 },
				new int[5] { 23, 8, 0, 10, 1 },
				new int[5] { 22, 8, 1, 0, 3 },
				new int[5] { 21, 8, 1, 6, 1 },
				new int[5] { 20, 8, 1, 7, 1 },
				new int[5] { 19, 8, 1, 8, 1 },
				new int[5] { 24, 9, 0, 12, 1 },
				new int[5] { 23, 9, 1, 0, 4 },
				new int[5] { 22, 9, 1, 1, 2 },
				new int[5] { 21, 9, 1, 10, 1 },
				new int[5] { 20, 9, 1, 11, 1 },
				new int[5] { 19, 9, 1, 12, 1 },
				new int[5] { 18, 9, 1, 13, 1 },
				new int[5] { 17, 9, 1, 14, 1 },
				new int[5] { 7, 10, 0, 13, 1 },
				new int[5] { 6, 10, 1, 0, 5 },
				new int[5] { 5, 10, 1, 1, 3 },
				new int[5] { 4, 10, 1, 2, 2 },
				new int[5] { 36, 11, 1, 3, 2 },
				new int[5] { 37, 11, 1, 4, 2 },
				new int[5] { 38, 11, 1, 15, 1 },
				new int[5] { 39, 11, 1, 16, 1 },
				new int[5] { 88, 12, 0, 14, 1 },
				new int[5] { 89, 12, 1, 0, 8 },
				new int[5] { 90, 12, 1, 5, 2 },
				new int[5] { 91, 12, 1, 6, 2 },
				new int[5] { 92, 12, 1, 17, 1 },
				new int[5] { 93, 12, 1, 18, 1 },
				new int[5] { 94, 12, 1, 19, 1 },
				new int[5] { 95, 12, 1, 20, 1 }
			}
		};
		SCAN_TABLES = new short[3][]
		{
			new short[64]
			{
				0, 1, 8, 16, 9, 2, 3, 10, 17, 24,
				32, 25, 18, 11, 4, 5, 12, 19, 26, 33,
				40, 48, 41, 34, 27, 20, 13, 6, 7, 14,
				21, 28, 35, 42, 49, 56, 57, 50, 43, 36,
				29, 22, 15, 23, 30, 37, 44, 51, 58, 59,
				52, 45, 38, 31, 39, 46, 53, 60, 61, 54,
				47, 55, 62, 63
			},
			new short[64]
			{
				0, 1, 2, 3, 8, 9, 16, 17, 10, 11,
				4, 5, 6, 7, 15, 14, 13, 12, 19, 18,
				24, 25, 32, 33, 26, 27, 20, 21, 22, 23,
				28, 29, 30, 31, 34, 35, 40, 41, 48, 49,
				42, 43, 36, 37, 38, 39, 44, 45, 46, 47,
				50, 51, 56, 57, 58, 59, 52, 53, 54, 55,
				60, 61, 62, 63
			},
			new short[64]
			{
				0, 8, 16, 24, 1, 9, 2, 10, 17, 25,
				32, 40, 48, 56, 57, 49, 41, 33, 26, 18,
				3, 11, 4, 12, 19, 27, 34, 42, 50, 58,
				35, 43, 51, 59, 20, 28, 5, 13, 6, 14,
				21, 29, 36, 44, 52, 60, 37, 45, 53, 61,
				22, 30, 7, 15, 23, 31, 38, 46, 54, 62,
				39, 47, 55, 63
			}
		};
		DEFAULT_ACDC_VALUES = new short[15]
		{
			1024, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0
		};
		DC_LUM_TAB = new int[8][]
		{
			new int[2] { 0, 0 },
			new int[2] { 4, 3 },
			new int[2] { 3, 3 },
			new int[2] { 0, 3 },
			new int[2] { 2, 2 },
			new int[2] { 2, 2 },
			new int[2] { 1, 2 },
			new int[2] { 1, 2 }
		};
		FILTER_TAB = new int[4][]
		{
			new int[5] { 14, 23, -7, 3, -1 },
			new int[6] { -3, 19, 20, -6, 3, -1 },
			new int[7] { 2, -6, 20, 20, -6, 3, -1 },
			new int[8] { -1, 3, -6, 20, 20, -6, 3, -1 }
		};
		SPRITE_TRAJECTORY_LEN = new int[15][]
		{
			new int[2] { 0, 2 },
			new int[2] { 2, 3 },
			new int[2] { 3, 3 },
			new int[2] { 4, 3 },
			new int[2] { 5, 3 },
			new int[2] { 6, 3 },
			new int[2] { 14, 4 },
			new int[2] { 30, 5 },
			new int[2] { 62, 6 },
			new int[2] { 126, 7 },
			new int[2] { 254, 8 },
			new int[2] { 510, 9 },
			new int[2] { 1022, 10 },
			new int[2] { 2046, 11 },
			new int[2] { 4094, 12 }
		};
	}
}
