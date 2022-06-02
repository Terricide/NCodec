using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.vpx.vp9;

public class Consts : Object
{
	public const int KEY_FRAME = 0;

	public const int INTER_FRAME = 1;

	public const int ONLY_4X4 = 0;

	public const int ALLOW_8X8 = 1;

	public const int ALLOW_16X16 = 2;

	public const int ALLOW_32X32 = 3;

	public const int TX_MODE_SELECT = 4;

	public const int PARTITION_NONE = 0;

	public const int PARTITION_HORZ = 1;

	public const int PARTITION_VERT = 2;

	public const int PARTITION_SPLIT = 3;

	public const int BLOCK_INVALID = -1;

	public const int BLOCK_4X4 = 0;

	public const int BLOCK_4X8 = 1;

	public const int BLOCK_8X4 = 2;

	public const int BLOCK_8X8 = 3;

	public const int BLOCK_8X16 = 4;

	public const int BLOCK_16X8 = 5;

	public const int BLOCK_16X16 = 6;

	public const int BLOCK_16X32 = 7;

	public const int BLOCK_32X16 = 8;

	public const int BLOCK_32X32 = 9;

	public const int BLOCK_32X64 = 10;

	public const int BLOCK_64X32 = 11;

	public const int BLOCK_64X64 = 12;

	public const int TX_4X4 = 0;

	public const int TX_8X8 = 1;

	public const int TX_16X16 = 2;

	public const int TX_32X32 = 3;

	public const int INTRA_FRAME = 0;

	public const int LAST_FRAME = 1;

	public const int ALTREF_FRAME = 2;

	public const int GOLDEN_FRAME = 3;

	public const int DC_PRED = 0;

	public const int V_PRED = 1;

	public const int H_PRED = 2;

	public const int D45_PRED = 3;

	public const int D135_PRED = 4;

	public const int D117_PRED = 5;

	public const int D153_PRED = 6;

	public const int D207_PRED = 7;

	public const int D63_PRED = 8;

	public const int TM_PRED = 9;

	public const int NEARESTMV = 10;

	public const int NEARMV = 11;

	public const int ZEROMV = 12;

	public const int NEWMV = 13;

	public const int SINGLE_REF = 0;

	public const int COMPOUND_REF = 1;

	public const int REFERENCE_MODE_SELECT = 2;

	public const int NORMAL = 0;

	public const int SMOOTH = 1;

	public const int SHARP = 2;

	public const int SWITCHABLE = 3;

	public const int MV_JOINT_ZERO = 0;

	public const int MV_JOINT_HNZVZ = 1;

	public const int MV_JOINT_HZVNZ = 2;

	public const int MV_JOINT_HNZVNZ = 3;

	public const int ZERO_TOKEN = 0;

	public const int ONE_TOKEN = 1;

	public const int TWO_TOKEN = 2;

	public const int THREE_TOKEN = 3;

	public const int FOUR_TOKEN = 4;

	public const int DCT_VAL_CAT1 = 5;

	public const int DCT_VAL_CAT2 = 6;

	public const int DCT_VAL_CAT3 = 7;

	public const int DCT_VAL_CAT4 = 8;

	public const int DCT_VAL_CAT5 = 9;

	public const int DCT_VAL_CAT6 = 10;

	internal static int[] ___003C_003ETREE_SEGMENT_ID;

	internal static int[][] ___003C_003ETREE_TX_SIZE;

	internal static int[] ___003C_003EmaxTxLookup;

	internal static int[] ___003C_003EblW;

	internal static int[] ___003C_003EblH;

	internal static int[] ___003C_003ETREE_INTRA_MODE;

	internal static int[] ___003C_003Esize_group_lookup;

	internal static int[] ___003C_003ETREE_INTERP_FILTER;

	internal static int[] ___003C_003ETREE_INTER_MODE;

	internal static int[][] ___003C_003Emv_ref_blocks_sm;

	internal static int[][] ___003C_003Emv_ref_blocks;

	internal static int[] ___003C_003ETREE_MV_JOINT;

	internal static int[] ___003C_003EMV_CLASS_TREE;

	internal static int[] ___003C_003EMV_FR_TREE;

	internal static int[] ___003C_003ELITERAL_TO_FILTER_TYPE;

	internal static int[][] ___003C_003EPARETO_TABLE;

	internal static int[][] ___003C_003Eextra_bits;

	internal static int[][] ___003C_003Ecat_probs;

	internal static int[] ___003C_003ETOKEN_TREE;

	internal static int[] ___003C_003Ecoefband_4x4;

	internal static int[] ___003C_003Ecoefband_8x8plus;

	public const int SZ_8x8 = 0;

	public const int SZ_16x16 = 1;

	public const int SZ_32x32 = 2;

	public const int SZ_64x64 = 3;

	internal static int[][] ___003C_003EblSizeLookup_;

	internal static int[][] ___003C_003EblSizeLookup;

	internal static int[] ___003C_003Esub8x8PartitiontoBlockType;

	internal static int[] ___003C_003ETREE_PARTITION;

	internal static int[] ___003C_003ETREE_PARTITION_RIGHT_E;

	internal static int[] ___003C_003ETREE_PARTITION_BOTTOM_E;

	internal static int[] ___003C_003EINV_REMAP_TABLE;

	public const int REFS_PER_FRAME = 3;

	public const int MV_FR_SIZE = 4;

	public const int MVREF_NEIGHBOURS = 8;

	public const int BLOCK_SIZE_GROUPS = 4;

	public const int BLOCK_SIZES = 13;

	public const int PARTITION_CONTEXTS = 16;

	public const int MI_SIZE = 8;

	public const int MIN_TILE_WIDTH_B64 = 4;

	public const int MAX_TILE_WIDTH_B64 = 64;

	public const int MAX_MV_REF_CANDIDATES = 2;

	public const int NUM_REF_FRAMES = 8;

	public const int MAX_REF_FRAMES = 4;

	public const int IS_INTER_CONTEXTS = 4;

	public const int COMP_MODE_CONTEXTS = 5;

	public const int REF_CONTEXTS = 5;

	public const int MAX_SEGMENTS = 8;

	public const int SEG_LVL_ALT_Q = 0;

	public const int SEG_LVL_ALT_L = 1;

	public const int SEG_LVL_REF_FRAME = 2;

	public const int SEG_LVL_SKIP = 3;

	public const int SEG_LVL_MAX = 4;

	public const int BLOCK_TYPES = 2;

	public const int REF_TYPES = 2;

	public const int COEF_BANDS = 6;

	public const int PREV_COEF_CONTEXTS = 6;

	public const int UNCONSTRAINED_NODES = 3;

	public const int TX_SIZE_CONTEXTS = 2;

	public const int SWITCHABLE_FILTERS = 3;

	public const int INTERP_FILTER_CONTEXTS = 4;

	public const int SKIP_CONTEXTS = 3;

	public const int PARTITION_TYPES = 4;

	public const int TX_SIZES = 4;

	public const int TX_MODES = 5;

	public const int DCT_DCT = 0;

	public const int ADST_DCT = 1;

	public const int DCT_ADST = 2;

	public const int ADST_ADST = 3;

	public const int MB_MODE_COUNT = 14;

	public const int INTRA_MODES = 10;

	public const int INTER_MODES = 4;

	public const int INTER_MODE_CONTEXTS = 7;

	public const int MV_JOINTS = 4;

	public const int MV_CLASSES = 11;

	public const int CLASS0_SIZE = 2;

	public const int MV_OFFSET_BITS = 10;

	public const int MAX_PROB = 255;

	public const int MAX_MODE_LF_DELTAS = 2;

	public const int COMPANDED_MVREF_THRESH = 8;

	public const int MAX_LOOP_FILTER = 63;

	public const int REF_SCALE_SHIFT = 14;

	public const int SUBPEL_BITS = 4;

	public const int SUBPEL_SHIFTS = 16;

	public const int SUBPEL_MASK = 15;

	public const int MV_BORDER = 128;

	public const int INTERP_EXTEND = 4;

	public const int BORDERINPIXELS = 160;

	public const int MAX_UPDATE_FACTOR = 128;

	public const int COUNT_SAT = 20;

	public const int BOTH_ZERO = 0;

	public const int ZERO_PLUS_PREDICTED = 1;

	public const int BOTH_PREDICTED = 2;

	public const int NEW_PLUS_NON_INTRA = 3;

	public const int BOTH_NEW = 4;

	public const int INTRA_PLUS_NON_INTRA = 5;

	public const int BOTH_INTRA = 6;

	public const int INVALID_CASE = 9;

	internal int CS_UNKNOWN;

	public const int CS_BT_601 = 1;

	public const int CS_BT_709 = 2;

	public const int CS_SMPTE_170 = 3;

	public const int CS_SMPTE_240 = 4;

	public const int CS_BT_2020 = 5;

	public const int CS_RESERVED = 6;

	public const int CS_RGB = 7;

	internal static int[] ___003C_003ESEGMENTATION_FEATURE_BITS;

	internal static int[] ___003C_003ESEGMENTATION_FEATURE_SIGNED;

	internal static int[] ___003C_003Etx_mode_to_biggest_tx_size;

	internal static int[] ___003C_003Eintra_mode_to_tx_type_lookup;

	public const int CAT1_MIN_VAL = 5;

	public const int CAT2_MIN_VAL = 7;

	public const int CAT3_MIN_VAL = 11;

	public const int CAT4_MIN_VAL = 19;

	public const int CAT5_MIN_VAL = 35;

	public const int CAT6_MIN_VAL = 67;

	internal static int[] ___003C_003EcatMinVal;

	internal static int[][][][] ___003C_003Euv_txsize_lookup;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] TREE_SEGMENT_ID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETREE_SEGMENT_ID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] TREE_TX_SIZE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETREE_TX_SIZE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] maxTxLookup
	{
		[HideFromJava]
		get
		{
			return ___003C_003EmaxTxLookup;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] blW
	{
		[HideFromJava]
		get
		{
			return ___003C_003EblW;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] blH
	{
		[HideFromJava]
		get
		{
			return ___003C_003EblH;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] TREE_INTRA_MODE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETREE_INTRA_MODE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] size_group_lookup
	{
		[HideFromJava]
		get
		{
			return ___003C_003Esize_group_lookup;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] TREE_INTERP_FILTER
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETREE_INTERP_FILTER;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] TREE_INTER_MODE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETREE_INTER_MODE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] mv_ref_blocks_sm
	{
		[HideFromJava]
		get
		{
			return ___003C_003Emv_ref_blocks_sm;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] mv_ref_blocks
	{
		[HideFromJava]
		get
		{
			return ___003C_003Emv_ref_blocks;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] TREE_MV_JOINT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETREE_MV_JOINT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] MV_CLASS_TREE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMV_CLASS_TREE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] MV_FR_TREE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMV_FR_TREE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] LITERAL_TO_FILTER_TYPE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELITERAL_TO_FILTER_TYPE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] PARETO_TABLE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPARETO_TABLE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] extra_bits
	{
		[HideFromJava]
		get
		{
			return ___003C_003Eextra_bits;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] cat_probs
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ecat_probs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] TOKEN_TREE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETOKEN_TREE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] coefband_4x4
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ecoefband_4x4;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] coefband_8x8plus
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ecoefband_8x8plus;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] blSizeLookup_
	{
		[HideFromJava]
		get
		{
			return ___003C_003EblSizeLookup_;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] blSizeLookup
	{
		[HideFromJava]
		get
		{
			return ___003C_003EblSizeLookup;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] sub8x8PartitiontoBlockType
	{
		[HideFromJava]
		get
		{
			return ___003C_003Esub8x8PartitiontoBlockType;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] TREE_PARTITION
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETREE_PARTITION;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] TREE_PARTITION_RIGHT_E
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETREE_PARTITION_RIGHT_E;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] TREE_PARTITION_BOTTOM_E
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETREE_PARTITION_BOTTOM_E;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] INV_REMAP_TABLE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EINV_REMAP_TABLE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] SEGMENTATION_FEATURE_BITS
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESEGMENTATION_FEATURE_BITS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] SEGMENTATION_FEATURE_SIGNED
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESEGMENTATION_FEATURE_SIGNED;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] tx_mode_to_biggest_tx_size
	{
		[HideFromJava]
		get
		{
			return ___003C_003Etx_mode_to_biggest_tx_size;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] intra_mode_to_tx_type_lookup
	{
		[HideFromJava]
		get
		{
			return ___003C_003Eintra_mode_to_tx_type_lookup;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] catMinVal
	{
		[HideFromJava]
		get
		{
			return ___003C_003EcatMinVal;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][][][] uv_txsize_lookup
	{
		[HideFromJava]
		get
		{
			return ___003C_003Euv_txsize_lookup;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 140, 130, 233, 162, 96 })]
	public Consts()
	{
		CS_UNKNOWN = 0;
	}

	[LineNumberTable(new byte[]
	{
		159,
		118,
		130,
		127,
		51,
		191,
		63,
		127,
		38,
		127,
		38,
		159,
		38,
		191,
		75,
		159,
		38,
		157,
		byte.MaxValue,
		7,
		69,
		byte.MaxValue,
		160,
		143,
		71,
		byte.MaxValue,
		162,
		3,
		70,
		191,
		7,
		191,
		87,
		159,
		7,
		156,
		byte.MaxValue,
		208,
		71,
		189,
		161,
		2,
		191,
		160,
		156,
		223,
		160,
		222,
		223,
		65,
		159,
		53,
		byte.MaxValue,
		190,
		40,
		99,
		byte.MaxValue,
		160,
		188,
		72,
		byte.MaxValue,
		70,
		70,
		156,
		159,
		7,
		117,
		148,
		byte.MaxValue,
		168,
		151,
		160,
		166,
		124,
		156,
		191,
		1,
		byte.MaxValue,
		23,
		84,
		191,
		9
	})]
	static Consts()
	{
		___003C_003ETREE_SEGMENT_ID = new int[14]
		{
			2, 4, 6, 8, 10, 12, 0, -1, -2, -3,
			-4, -5, -6, -7
		};
		___003C_003ETREE_TX_SIZE = new int[4][]
		{
			null,
			new int[2] { 0, -1 },
			new int[4] { 0, 2, -1, -2 },
			new int[6] { 0, 2, -1, 4, -2, -3 }
		};
		___003C_003EmaxTxLookup = new int[13]
		{
			0, 0, 0, 1, 1, 1, 2, 2, 2, 3,
			3, 3, 3
		};
		___003C_003EblW = new int[13]
		{
			1, 1, 1, 1, 1, 2, 2, 2, 4, 4,
			4, 8, 8
		};
		___003C_003EblH = new int[13]
		{
			1, 1, 1, 1, 2, 1, 2, 4, 2, 4,
			8, 4, 8
		};
		___003C_003ETREE_INTRA_MODE = new int[18]
		{
			0, 2, -9, 4, -1, 6, 8, 12, -2, 10,
			-4, -5, -3, 14, -8, 16, -6, -7
		};
		___003C_003Esize_group_lookup = new int[13]
		{
			0, 0, 0, 1, 1, 1, 2, 2, 2, 3,
			3, 3, 3
		};
		___003C_003ETREE_INTERP_FILTER = new int[4] { 0, 2, -1, -2 };
		___003C_003ETREE_INTER_MODE = new int[6] { -2, 2, 0, 4, -1, -3 };
		___003C_003Emv_ref_blocks_sm = new int[13][]
		{
			new int[2] { 0, 1 },
			new int[2] { 0, 1 },
			new int[2] { 0, 1 },
			new int[2] { 0, 1 },
			new int[2] { 1, 0 },
			new int[2] { 0, 1 },
			new int[2] { 0, 1 },
			new int[2] { 1, 0 },
			new int[2] { 0, 1 },
			new int[2] { 2, 3 },
			new int[2] { 1, 0 },
			new int[2] { 0, 1 },
			new int[2] { 4, 5 }
		};
		___003C_003Emv_ref_blocks = new int[13][]
		{
			new int[8] { 0, 1, 11, 12, 13, 16, 17, 18 },
			new int[8] { 0, 1, 11, 12, 13, 16, 17, 18 },
			new int[8] { 0, 1, 11, 12, 13, 16, 17, 18 },
			new int[8] { 0, 1, 11, 12, 13, 16, 17, 18 },
			new int[8] { 1, 0, 3, 11, 13, 12, 16, 17 },
			new int[8] { 0, 1, 2, 11, 12, 13, 17, 16 },
			new int[8] { 0, 1, 2, 3, 11, 14, 15, 19 },
			new int[8] { 1, 0, 7, 11, 2, 15, 14, 19 },
			new int[8] { 0, 1, 6, 11, 3, 14, 15, 19 },
			new int[8] { 2, 3, 6, 7, 11, 14, 15, 19 },
			new int[8] { 1, 0, 9, 6, 11, 15, 14, 7 },
			new int[8] { 0, 1, 8, 7, 11, 14, 15, 6 },
			new int[8] { 4, 5, 8, 9, 11, 0, 1, 10 }
		};
		___003C_003ETREE_MV_JOINT = new int[6] { 0, 2, -1, 4, -2, -3 };
		___003C_003EMV_CLASS_TREE = new int[20]
		{
			0, 2, -1, 4, 6, 8, -2, -3, 10, 12,
			-4, -5, -6, 14, 16, 18, -7, -8, -9, -10
		};
		___003C_003EMV_FR_TREE = new int[6] { 0, 2, -1, 4, -2, -3 };
		___003C_003ELITERAL_TO_FILTER_TYPE = new int[4] { 1, 0, 2, 3 };
		___003C_003EPARETO_TABLE = new int[255][]
		{
			new int[8] { 3, 86, 128, 6, 86, 23, 88, 29 },
			new int[8] { 6, 86, 128, 11, 87, 42, 91, 52 },
			new int[8] { 9, 86, 129, 17, 88, 61, 94, 76 },
			new int[8] { 12, 86, 129, 22, 88, 77, 97, 93 },
			new int[8] { 15, 87, 129, 28, 89, 93, 100, 110 },
			new int[8] { 17, 87, 129, 33, 90, 105, 103, 123 },
			new int[8] { 20, 88, 130, 38, 91, 118, 106, 136 },
			new int[8] { 23, 88, 130, 43, 91, 128, 108, 146 },
			new int[8] { 26, 89, 131, 48, 92, 139, 111, 156 },
			new int[8] { 28, 89, 131, 53, 93, 147, 114, 163 },
			new int[8] { 31, 90, 131, 58, 94, 156, 117, 171 },
			new int[8] { 34, 90, 131, 62, 94, 163, 119, 177 },
			new int[8] { 37, 90, 132, 66, 95, 171, 122, 184 },
			new int[8] { 39, 90, 132, 70, 96, 177, 124, 189 },
			new int[8] { 42, 91, 132, 75, 97, 183, 127, 194 },
			new int[8] { 44, 91, 132, 79, 97, 188, 129, 198 },
			new int[8] { 47, 92, 133, 83, 98, 193, 132, 202 },
			new int[8] { 49, 92, 133, 86, 99, 197, 134, 205 },
			new int[8] { 52, 93, 133, 90, 100, 201, 137, 208 },
			new int[8] { 54, 93, 133, 94, 100, 204, 139, 211 },
			new int[8] { 57, 94, 134, 98, 101, 208, 142, 214 },
			new int[8] { 59, 94, 134, 101, 102, 211, 144, 216 },
			new int[8] { 62, 94, 135, 105, 103, 214, 146, 218 },
			new int[8] { 64, 94, 135, 108, 103, 216, 148, 220 },
			new int[8] { 66, 95, 135, 111, 104, 219, 151, 222 },
			new int[8] { 68, 95, 135, 114, 105, 221, 153, 223 },
			new int[8] { 71, 96, 136, 117, 106, 224, 155, 225 },
			new int[8] { 73, 96, 136, 120, 106, 225, 157, 226 },
			new int[8] { 76, 97, 136, 123, 107, 227, 159, 228 },
			new int[8] { 78, 97, 136, 126, 108, 229, 160, 229 },
			new int[8] { 80, 98, 137, 129, 109, 231, 162, 231 },
			new int[8] { 82, 98, 137, 131, 109, 232, 164, 232 },
			new int[8] { 84, 98, 138, 134, 110, 234, 166, 233 },
			new int[8] { 86, 98, 138, 137, 111, 235, 168, 234 },
			new int[8] { 89, 99, 138, 140, 112, 236, 170, 235 },
			new int[8] { 91, 99, 138, 142, 112, 237, 171, 235 },
			new int[8] { 93, 100, 139, 145, 113, 238, 173, 236 },
			new int[8] { 95, 100, 139, 147, 114, 239, 174, 237 },
			new int[8] { 97, 101, 140, 149, 115, 240, 176, 238 },
			new int[8] { 99, 101, 140, 151, 115, 241, 177, 238 },
			new int[8] { 101, 102, 140, 154, 116, 242, 179, 239 },
			new int[8] { 103, 102, 140, 156, 117, 242, 180, 239 },
			new int[8] { 105, 103, 141, 158, 118, 243, 182, 240 },
			new int[8] { 107, 103, 141, 160, 118, 243, 183, 240 },
			new int[8] { 109, 104, 141, 162, 119, 244, 185, 241 },
			new int[8] { 111, 104, 141, 164, 119, 244, 186, 241 },
			new int[8] { 113, 104, 142, 166, 120, 245, 187, 242 },
			new int[8] { 114, 104, 142, 168, 121, 245, 188, 242 },
			new int[8] { 116, 105, 143, 170, 122, 246, 190, 243 },
			new int[8] { 118, 105, 143, 171, 122, 246, 191, 243 },
			new int[8] { 120, 106, 143, 173, 123, 247, 192, 244 },
			new int[8] { 121, 106, 143, 175, 124, 247, 193, 244 },
			new int[8] { 123, 107, 144, 177, 125, 248, 195, 244 },
			new int[8] { 125, 107, 144, 178, 125, 248, 196, 244 },
			new int[8] { 127, 108, 145, 180, 126, 249, 197, 245 },
			new int[8] { 128, 108, 145, 181, 127, 249, 198, 245 },
			new int[8] { 130, 109, 145, 183, 128, 249, 199, 245 },
			new int[8] { 132, 109, 145, 184, 128, 249, 200, 245 },
			new int[8] { 134, 110, 146, 186, 129, 250, 201, 246 },
			new int[8] { 135, 110, 146, 187, 130, 250, 202, 246 },
			new int[8] { 137, 111, 147, 189, 131, 251, 203, 246 },
			new int[8] { 138, 111, 147, 190, 131, 251, 204, 246 },
			new int[8] { 140, 112, 147, 192, 132, 251, 205, 247 },
			new int[8] { 141, 112, 147, 193, 132, 251, 206, 247 },
			new int[8] { 143, 113, 148, 194, 133, 251, 207, 247 },
			new int[8] { 144, 113, 148, 195, 134, 251, 207, 247 },
			new int[8] { 146, 114, 149, 197, 135, 252, 208, 248 },
			new int[8] { 147, 114, 149, 198, 135, 252, 209, 248 },
			new int[8] { 149, 115, 149, 199, 136, 252, 210, 248 },
			new int[8] { 150, 115, 149, 200, 137, 252, 210, 248 },
			new int[8] { 152, 115, 150, 201, 138, 252, 211, 248 },
			new int[8] { 153, 115, 150, 202, 138, 252, 212, 248 },
			new int[8] { 155, 116, 151, 204, 139, 253, 213, 249 },
			new int[8] { 156, 116, 151, 205, 139, 253, 213, 249 },
			new int[8] { 158, 117, 151, 206, 140, 253, 214, 249 },
			new int[8] { 159, 117, 151, 207, 141, 253, 215, 249 },
			new int[8] { 161, 118, 152, 208, 142, 253, 216, 249 },
			new int[8] { 162, 118, 152, 209, 142, 253, 216, 249 },
			new int[8] { 163, 119, 153, 210, 143, 253, 217, 249 },
			new int[8] { 164, 119, 153, 211, 143, 253, 217, 249 },
			new int[8] { 166, 120, 153, 212, 144, 254, 218, 250 },
			new int[8] { 167, 120, 153, 212, 145, 254, 219, 250 },
			new int[8] { 168, 121, 154, 213, 146, 254, 220, 250 },
			new int[8] { 169, 121, 154, 214, 146, 254, 220, 250 },
			new int[8] { 171, 122, 155, 215, 147, 254, 221, 250 },
			new int[8] { 172, 122, 155, 216, 147, 254, 221, 250 },
			new int[8] { 173, 123, 155, 217, 148, 254, 222, 250 },
			new int[8] { 174, 123, 155, 217, 149, 254, 222, 250 },
			new int[8] { 176, 124, 156, 218, 150, 254, 223, 250 },
			new int[8] { 177, 124, 156, 219, 150, 254, 223, 250 },
			new int[8] { 178, 125, 157, 220, 151, 254, 224, 251 },
			new int[8] { 179, 125, 157, 220, 151, 254, 224, 251 },
			new int[8] { 180, 126, 157, 221, 152, 254, 225, 251 },
			new int[8] { 181, 126, 157, 221, 152, 254, 225, 251 },
			new int[8] { 183, 127, 158, 222, 153, 254, 226, 251 },
			new int[8] { 184, 127, 158, 223, 154, 254, 226, 251 },
			new int[8] { 185, 128, 159, 224, 155, 255, 227, 251 },
			new int[8] { 186, 128, 159, 224, 155, 255, 227, 251 },
			new int[8] { 187, 129, 160, 225, 156, 255, 228, 251 },
			new int[8] { 188, 130, 160, 225, 156, 255, 228, 251 },
			new int[8] { 189, 131, 160, 226, 157, 255, 228, 251 },
			new int[8] { 190, 131, 160, 226, 158, 255, 228, 251 },
			new int[8] { 191, 132, 161, 227, 159, 255, 229, 251 },
			new int[8] { 192, 132, 161, 227, 159, 255, 229, 251 },
			new int[8] { 193, 133, 162, 228, 160, 255, 230, 252 },
			new int[8] { 194, 133, 162, 229, 160, 255, 230, 252 },
			new int[8] { 195, 134, 163, 230, 161, 255, 231, 252 },
			new int[8] { 196, 134, 163, 230, 161, 255, 231, 252 },
			new int[8] { 197, 135, 163, 231, 162, 255, 231, 252 },
			new int[8] { 198, 135, 163, 231, 162, 255, 231, 252 },
			new int[8] { 199, 136, 164, 232, 163, 255, 232, 252 },
			new int[8] { 200, 136, 164, 232, 164, 255, 232, 252 },
			new int[8] { 201, 137, 165, 233, 165, 255, 233, 252 },
			new int[8] { 201, 137, 165, 233, 165, 255, 233, 252 },
			new int[8] { 202, 138, 166, 233, 166, 255, 233, 252 },
			new int[8] { 203, 138, 166, 233, 166, 255, 233, 252 },
			new int[8] { 204, 139, 166, 234, 167, 255, 234, 252 },
			new int[8] { 205, 139, 166, 234, 167, 255, 234, 252 },
			new int[8] { 206, 140, 167, 235, 168, 255, 235, 252 },
			new int[8] { 206, 140, 167, 235, 168, 255, 235, 252 },
			new int[8] { 207, 141, 168, 236, 169, 255, 235, 252 },
			new int[8] { 208, 141, 168, 236, 170, 255, 235, 252 },
			new int[8] { 209, 142, 169, 237, 171, 255, 236, 252 },
			new int[8] { 209, 143, 169, 237, 171, 255, 236, 252 },
			new int[8] { 210, 144, 169, 237, 172, 255, 236, 252 },
			new int[8] { 211, 144, 169, 237, 172, 255, 236, 252 },
			new int[8] { 212, 145, 170, 238, 173, 255, 237, 252 },
			new int[8] { 213, 145, 170, 238, 173, 255, 237, 252 },
			new int[8] { 214, 146, 171, 239, 174, 255, 237, 253 },
			new int[8] { 214, 146, 171, 239, 174, 255, 237, 253 },
			new int[8] { 215, 147, 172, 240, 175, 255, 238, 253 },
			new int[8] { 215, 147, 172, 240, 175, 255, 238, 253 },
			new int[8] { 216, 148, 173, 240, 176, 255, 238, 253 },
			new int[8] { 217, 148, 173, 240, 176, 255, 238, 253 },
			new int[8] { 218, 149, 173, 241, 177, 255, 239, 253 },
			new int[8] { 218, 149, 173, 241, 178, 255, 239, 253 },
			new int[8] { 219, 150, 174, 241, 179, 255, 239, 253 },
			new int[8] { 219, 151, 174, 241, 179, 255, 239, 253 },
			new int[8] { 220, 152, 175, 242, 180, 255, 240, 253 },
			new int[8] { 221, 152, 175, 242, 180, 255, 240, 253 },
			new int[8] { 222, 153, 176, 242, 181, 255, 240, 253 },
			new int[8] { 222, 153, 176, 242, 181, 255, 240, 253 },
			new int[8] { 223, 154, 177, 243, 182, 255, 240, 253 },
			new int[8] { 223, 154, 177, 243, 182, 255, 240, 253 },
			new int[8] { 224, 155, 178, 244, 183, 255, 241, 253 },
			new int[8] { 224, 155, 178, 244, 183, 255, 241, 253 },
			new int[8] { 225, 156, 178, 244, 184, 255, 241, 253 },
			new int[8] { 225, 157, 178, 244, 184, 255, 241, 253 },
			new int[8] { 226, 158, 179, 244, 185, 255, 242, 253 },
			new int[8] { 227, 158, 179, 244, 185, 255, 242, 253 },
			new int[8] { 228, 159, 180, 245, 186, 255, 242, 253 },
			new int[8] { 228, 159, 180, 245, 186, 255, 242, 253 },
			new int[8] { 229, 160, 181, 245, 187, 255, 242, 253 },
			new int[8] { 229, 160, 181, 245, 187, 255, 242, 253 },
			new int[8] { 230, 161, 182, 246, 188, 255, 243, 253 },
			new int[8] { 230, 162, 182, 246, 188, 255, 243, 253 },
			new int[8] { 231, 163, 183, 246, 189, 255, 243, 253 },
			new int[8] { 231, 163, 183, 246, 189, 255, 243, 253 },
			new int[8] { 232, 164, 184, 247, 190, 255, 243, 253 },
			new int[8] { 232, 164, 184, 247, 190, 255, 243, 253 },
			new int[8] { 233, 165, 185, 247, 191, 255, 244, 253 },
			new int[8] { 233, 165, 185, 247, 191, 255, 244, 253 },
			new int[8] { 234, 166, 185, 247, 192, 255, 244, 253 },
			new int[8] { 234, 167, 185, 247, 192, 255, 244, 253 },
			new int[8] { 235, 168, 186, 248, 193, 255, 244, 253 },
			new int[8] { 235, 168, 186, 248, 193, 255, 244, 253 },
			new int[8] { 236, 169, 187, 248, 194, 255, 244, 253 },
			new int[8] { 236, 169, 187, 248, 194, 255, 244, 253 },
			new int[8] { 236, 170, 188, 248, 195, 255, 245, 253 },
			new int[8] { 236, 170, 188, 248, 195, 255, 245, 253 },
			new int[8] { 237, 171, 189, 249, 196, 255, 245, 254 },
			new int[8] { 237, 172, 189, 249, 196, 255, 245, 254 },
			new int[8] { 238, 173, 190, 249, 197, 255, 245, 254 },
			new int[8] { 238, 173, 190, 249, 197, 255, 245, 254 },
			new int[8] { 239, 174, 191, 249, 198, 255, 245, 254 },
			new int[8] { 239, 174, 191, 249, 198, 255, 245, 254 },
			new int[8] { 240, 175, 192, 249, 199, 255, 246, 254 },
			new int[8] { 240, 176, 192, 249, 199, 255, 246, 254 },
			new int[8] { 240, 177, 193, 250, 200, 255, 246, 254 },
			new int[8] { 240, 177, 193, 250, 200, 255, 246, 254 },
			new int[8] { 241, 178, 194, 250, 201, 255, 246, 254 },
			new int[8] { 241, 178, 194, 250, 201, 255, 246, 254 },
			new int[8] { 242, 179, 195, 250, 202, 255, 246, 254 },
			new int[8] { 242, 180, 195, 250, 202, 255, 246, 254 },
			new int[8] { 242, 181, 196, 250, 203, 255, 247, 254 },
			new int[8] { 242, 181, 196, 250, 203, 255, 247, 254 },
			new int[8] { 243, 182, 197, 251, 204, 255, 247, 254 },
			new int[8] { 243, 183, 197, 251, 204, 255, 247, 254 },
			new int[8] { 244, 184, 198, 251, 205, 255, 247, 254 },
			new int[8] { 244, 184, 198, 251, 205, 255, 247, 254 },
			new int[8] { 244, 185, 199, 251, 206, 255, 247, 254 },
			new int[8] { 244, 185, 199, 251, 206, 255, 247, 254 },
			new int[8] { 245, 186, 200, 251, 207, 255, 247, 254 },
			new int[8] { 245, 187, 200, 251, 207, 255, 247, 254 },
			new int[8] { 246, 188, 201, 252, 207, 255, 248, 254 },
			new int[8] { 246, 188, 201, 252, 207, 255, 248, 254 },
			new int[8] { 246, 189, 202, 252, 208, 255, 248, 254 },
			new int[8] { 246, 190, 202, 252, 208, 255, 248, 254 },
			new int[8] { 247, 191, 203, 252, 209, 255, 248, 254 },
			new int[8] { 247, 191, 203, 252, 209, 255, 248, 254 },
			new int[8] { 247, 192, 204, 252, 210, 255, 248, 254 },
			new int[8] { 247, 193, 204, 252, 210, 255, 248, 254 },
			new int[8] { 248, 194, 205, 252, 211, 255, 248, 254 },
			new int[8] { 248, 194, 205, 252, 211, 255, 248, 254 },
			new int[8] { 248, 195, 206, 252, 212, 255, 249, 254 },
			new int[8] { 248, 196, 206, 252, 212, 255, 249, 254 },
			new int[8] { 249, 197, 207, 253, 213, 255, 249, 254 },
			new int[8] { 249, 197, 207, 253, 213, 255, 249, 254 },
			new int[8] { 249, 198, 208, 253, 214, 255, 249, 254 },
			new int[8] { 249, 199, 209, 253, 214, 255, 249, 254 },
			new int[8] { 250, 200, 210, 253, 215, 255, 249, 254 },
			new int[8] { 250, 200, 210, 253, 215, 255, 249, 254 },
			new int[8] { 250, 201, 211, 253, 215, 255, 249, 254 },
			new int[8] { 250, 202, 211, 253, 215, 255, 249, 254 },
			new int[8] { 250, 203, 212, 253, 216, 255, 249, 254 },
			new int[8] { 250, 203, 212, 253, 216, 255, 249, 254 },
			new int[8] { 251, 204, 213, 253, 217, 255, 250, 254 },
			new int[8] { 251, 205, 213, 253, 217, 255, 250, 254 },
			new int[8] { 251, 206, 214, 254, 218, 255, 250, 254 },
			new int[8] { 251, 206, 215, 254, 218, 255, 250, 254 },
			new int[8] { 252, 207, 216, 254, 219, 255, 250, 254 },
			new int[8] { 252, 208, 216, 254, 219, 255, 250, 254 },
			new int[8] { 252, 209, 217, 254, 220, 255, 250, 254 },
			new int[8] { 252, 210, 217, 254, 220, 255, 250, 254 },
			new int[8] { 252, 211, 218, 254, 221, 255, 250, 254 },
			new int[8] { 252, 212, 218, 254, 221, 255, 250, 254 },
			new int[8] { 253, 213, 219, 254, 222, 255, 250, 254 },
			new int[8] { 253, 213, 220, 254, 222, 255, 250, 254 },
			new int[8] { 253, 214, 221, 254, 223, 255, 250, 254 },
			new int[8] { 253, 215, 221, 254, 223, 255, 250, 254 },
			new int[8] { 253, 216, 222, 254, 224, 255, 251, 254 },
			new int[8] { 253, 217, 223, 254, 224, 255, 251, 254 },
			new int[8] { 253, 218, 224, 254, 225, 255, 251, 254 },
			new int[8] { 253, 219, 224, 254, 225, 255, 251, 254 },
			new int[8] { 254, 220, 225, 254, 225, 255, 251, 254 },
			new int[8] { 254, 221, 226, 254, 225, 255, 251, 254 },
			new int[8] { 254, 222, 227, 255, 226, 255, 251, 254 },
			new int[8] { 254, 223, 227, 255, 226, 255, 251, 254 },
			new int[8] { 254, 224, 228, 255, 227, 255, 251, 254 },
			new int[8] { 254, 225, 229, 255, 227, 255, 251, 254 },
			new int[8] { 254, 226, 230, 255, 228, 255, 251, 254 },
			new int[8] { 254, 227, 230, 255, 229, 255, 251, 254 },
			new int[8] { 255, 228, 231, 255, 230, 255, 251, 254 },
			new int[8] { 255, 229, 232, 255, 230, 255, 251, 254 },
			new int[8] { 255, 230, 233, 255, 231, 255, 252, 254 },
			new int[8] { 255, 231, 234, 255, 231, 255, 252, 254 },
			new int[8] { 255, 232, 235, 255, 232, 255, 252, 254 },
			new int[8] { 255, 233, 236, 255, 232, 255, 252, 254 },
			new int[8] { 255, 235, 237, 255, 233, 255, 252, 254 },
			new int[8] { 255, 236, 238, 255, 234, 255, 252, 254 },
			new int[8] { 255, 238, 240, 255, 235, 255, 252, 255 },
			new int[8] { 255, 239, 241, 255, 235, 255, 252, 254 },
			new int[8] { 255, 241, 243, 255, 236, 255, 252, 254 },
			new int[8] { 255, 243, 245, 255, 237, 255, 252, 254 },
			new int[8] { 255, 246, 247, 255, 239, 255, 253, 255 }
		};
		___003C_003Eextra_bits = new int[11][]
		{
			new int[3] { 0, 0, 0 },
			new int[3] { 0, 0, 1 },
			new int[3] { 0, 0, 2 },
			new int[3] { 0, 0, 3 },
			new int[3] { 0, 0, 4 },
			new int[3] { 1, 1, 5 },
			new int[3] { 2, 2, 7 },
			new int[3] { 3, 3, 11 },
			new int[3] { 4, 4, 19 },
			new int[3] { 5, 5, 35 },
			new int[3] { 6, 14, 67 }
		};
		___003C_003Ecat_probs = new int[7][]
		{
			new int[1] { 0 },
			new int[1] { 159 },
			new int[2] { 165, 145 },
			new int[3] { 173, 148, 140 },
			new int[4] { 176, 155, 140, 135 },
			new int[5] { 180, 157, 141, 134, 130 },
			new int[14]
			{
				254, 254, 254, 252, 249, 243, 230, 196, 177, 153,
				140, 133, 130, 129
			}
		};
		___003C_003ETOKEN_TREE = new int[16]
		{
			2, 6, -2, 4, -3, -4, 8, 10, -5, -6,
			12, 14, -7, -8, -9, -10
		};
		___003C_003Ecoefband_4x4 = new int[16]
		{
			0, 1, 1, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 5, 5, 5
		};
		___003C_003Ecoefband_8x8plus = new int[1024]
		{
			0, 1, 1, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5
		};
		___003C_003EblSizeLookup_ = new int[4][]
		{
			new int[13]
			{
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
				10, 11, 12
			},
			new int[13]
			{
				-1, -1, -1, 2, -1, -1, 5, -1, -1, 8,
				-1, -1, 11
			},
			new int[13]
			{
				-1, -1, -1, 1, -1, 1, 4, -1, -1, 7,
				-1, -1, 10
			},
			new int[13]
			{
				-1, -1, -1, 0, -1, -1, 3, -1, -1, 6,
				-1, -1, 9
			}
		};
		___003C_003EblSizeLookup = new int[4][]
		{
			new int[2] { 0, 1 },
			new int[3] { 2, 3, 4 },
			new int[4] { -1, 5, 6, 7 },
			new int[4] { -1, -1, 8, 9 }
		};
		___003C_003Esub8x8PartitiontoBlockType = new int[4] { 3, 2, 1, 0 };
		___003C_003ETREE_PARTITION = new int[6] { 0, 2, -1, 4, -2, -3 };
		___003C_003ETREE_PARTITION_RIGHT_E = new int[2] { 0, -2 };
		___003C_003ETREE_PARTITION_BOTTOM_E = new int[2] { 0, -1 };
		___003C_003EINV_REMAP_TABLE = new int[255]
		{
			7, 20, 33, 46, 59, 72, 85, 98, 111, 124,
			137, 150, 163, 176, 189, 202, 215, 228, 241, 254,
			1, 2, 3, 4, 5, 6, 8, 9, 10, 11,
			12, 13, 14, 15, 16, 17, 18, 19, 21, 22,
			23, 24, 25, 26, 27, 28, 29, 30, 31, 32,
			34, 35, 36, 37, 38, 39, 40, 41, 42, 43,
			44, 45, 47, 48, 49, 50, 51, 52, 53, 54,
			55, 56, 57, 58, 60, 61, 62, 63, 64, 65,
			66, 67, 68, 69, 70, 71, 73, 74, 75, 76,
			77, 78, 79, 80, 81, 82, 83, 84, 86, 87,
			88, 89, 90, 91, 92, 93, 94, 95, 96, 97,
			99, 100, 101, 102, 103, 104, 105, 106, 107, 108,
			109, 110, 112, 113, 114, 115, 116, 117, 118, 119,
			120, 121, 122, 123, 125, 126, 127, 128, 129, 130,
			131, 132, 133, 134, 135, 136, 138, 139, 140, 141,
			142, 143, 144, 145, 146, 147, 148, 149, 151, 152,
			153, 154, 155, 156, 157, 158, 159, 160, 161, 162,
			164, 165, 166, 167, 168, 169, 170, 171, 172, 173,
			174, 175, 177, 178, 179, 180, 181, 182, 183, 184,
			185, 186, 187, 188, 190, 191, 192, 193, 194, 195,
			196, 197, 198, 199, 200, 201, 203, 204, 205, 206,
			207, 208, 209, 210, 211, 212, 213, 214, 216, 217,
			218, 219, 220, 221, 222, 223, 224, 225, 226, 227,
			229, 230, 231, 232, 233, 234, 235, 236, 237, 238,
			239, 240, 242, 243, 244, 245, 246, 247, 248, 249,
			250, 251, 252, 253, 253
		};
		___003C_003ESEGMENTATION_FEATURE_BITS = new int[4] { 8, 6, 2, 0 };
		___003C_003ESEGMENTATION_FEATURE_SIGNED = new int[4] { 1, 1, 0, 0 };
		___003C_003Etx_mode_to_biggest_tx_size = new int[5] { 0, 1, 2, 3, 3 };
		___003C_003Eintra_mode_to_tx_type_lookup = new int[10] { 0, 1, 2, 0, 3, 1, 2, 2, 1, 3 };
		___003C_003EcatMinVal = new int[6] { 5, 7, 11, 19, 35, 67 };
		___003C_003Euv_txsize_lookup = new int[13][][][]
		{
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 0 },
					new int[2] { 0, 0 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 1 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 1 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 1 },
					new int[2] { 0, 0 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 0 },
					new int[2] { 1, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 0 },
					new int[2] { 1, 1 }
				},
				new int[2][]
				{
					new int[2] { 1, 0 },
					new int[2] { 1, 1 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 1 },
					new int[2] { 1, 1 }
				},
				new int[2][]
				{
					new int[2] { 2, 1 },
					new int[2] { 1, 1 }
				},
				new int[2][]
				{
					new int[2] { 2, 1 },
					new int[2] { 1, 1 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 1 },
					new int[2] { 1, 1 }
				},
				new int[2][]
				{
					new int[2] { 2, 2 },
					new int[2] { 1, 1 }
				},
				new int[2][]
				{
					new int[2] { 2, 2 },
					new int[2] { 1, 1 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 1 },
					new int[2] { 1, 1 }
				},
				new int[2][]
				{
					new int[2] { 2, 1 },
					new int[2] { 2, 1 }
				},
				new int[2][]
				{
					new int[2] { 2, 1 },
					new int[2] { 2, 1 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 1 },
					new int[2] { 1, 1 }
				},
				new int[2][]
				{
					new int[2] { 2, 2 },
					new int[2] { 2, 2 }
				},
				new int[2][]
				{
					new int[2] { 3, 2 },
					new int[2] { 2, 2 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 1 },
					new int[2] { 1, 1 }
				},
				new int[2][]
				{
					new int[2] { 2, 2 },
					new int[2] { 2, 2 }
				},
				new int[2][]
				{
					new int[2] { 3, 3 },
					new int[2] { 2, 2 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 1 },
					new int[2] { 1, 1 }
				},
				new int[2][]
				{
					new int[2] { 2, 2 },
					new int[2] { 2, 2 }
				},
				new int[2][]
				{
					new int[2] { 3, 2 },
					new int[2] { 3, 2 }
				}
			},
			new int[4][][]
			{
				new int[2][]
				{
					new int[2] { 0, 0 },
					new int[2] { 0, 0 }
				},
				new int[2][]
				{
					new int[2] { 1, 1 },
					new int[2] { 1, 1 }
				},
				new int[2][]
				{
					new int[2] { 2, 2 },
					new int[2] { 2, 2 }
				},
				new int[2][]
				{
					new int[2] { 3, 3 },
					new int[2] { 3, 3 }
				}
			}
		};
	}
}
