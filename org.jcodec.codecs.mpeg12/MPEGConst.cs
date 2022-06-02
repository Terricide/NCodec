using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12;

public class MPEGConst : Object
{
	public class MBType : Object
	{
		public int macroblock_quant;

		public int macroblock_motion_forward;

		public int macroblock_motion_backward;

		public int macroblock_pattern;

		public int macroblock_intra;

		public int spatial_temporal_weight_code_flag;

		public int permitted_spatial_temporal_weight_classes;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 127, 66, 105, 104, 104, 104, 105, 105, 105,
			105
		})]
		internal MBType(int macroblock_quant, int macroblock_motion_forward, int macroblock_motion_backward, int macroblock_pattern, int macroblock_intra, int spatial_temporal_weight_code_flag, int permitted_spatial_temporal_weight_classes)
		{
			this.macroblock_quant = macroblock_quant;
			this.macroblock_motion_forward = macroblock_motion_forward;
			this.macroblock_motion_backward = macroblock_motion_backward;
			this.macroblock_pattern = macroblock_pattern;
			this.macroblock_intra = macroblock_intra;
			this.spatial_temporal_weight_code_flag = spatial_temporal_weight_code_flag;
			this.permitted_spatial_temporal_weight_classes = permitted_spatial_temporal_weight_classes;
		}
	}

	public const int PICTURE_START_CODE = 0;

	public const int SLICE_START_CODE_FIRST = 1;

	public const int SLICE_START_CODE_LAST = 175;

	public const int USER_DATA_START_CODE = 178;

	public const int SEQUENCE_HEADER_CODE = 179;

	public const int SEQUENCE_ERROR_CODE = 180;

	public const int EXTENSION_START_CODE = 181;

	public const int SEQUENCE_END_CODE = 183;

	public const int GROUP_START_CODE = 184;

	internal static VLC ___003C_003EvlcAddressIncrement;

	internal static VLC ___003C_003EvlcMBTypeI;

	internal static MBType[] ___003C_003EmbTypeValI;

	internal static VLC ___003C_003EvlcMBTypeP;

	internal static MBType[] ___003C_003EmbTypeValP;

	internal static VLC ___003C_003EvlcMBTypeB;

	internal static MBType[] ___003C_003EmbTypeValB;

	internal static VLC ___003C_003EvlcMBTypeISpat;

	internal static MBType[] ___003C_003EmbTypeValISpat;

	internal static VLC ___003C_003EvlcMBTypePSpat;

	internal static MBType[] ___003C_003EmbTypeValPSpat;

	internal static VLC ___003C_003EvlcMBTypeBSpat;

	internal static MBType[] ___003C_003EmbTypeValBSpat;

	internal static VLC ___003C_003EvlcMBTypeSNR;

	internal static MBType[] ___003C_003EmbTypeValSNR;

	internal static VLC ___003C_003EvlcCBP;

	internal static VLC ___003C_003EvlcMotionCode;

	internal static VLC ___003C_003EvlcDualPrime;

	internal static VLC ___003C_003EvlcDCSizeLuma;

	internal static VLC ___003C_003EvlcDCSizeChroma;

	internal static VLC ___003C_003EvlcCoeff0;

	internal static VLC ___003C_003EvlcCoeff1;

	public const int CODE_ESCAPE = 2049;

	public const int CODE_END = 2048;

	internal static int[] ___003C_003EqScaleTab1;

	internal static int[] ___003C_003EqScaleTab2;

	internal static int[] ___003C_003EdefaultQMatIntra;

	internal static int[] ___003C_003EdefaultQMatInter;

	internal static int[][] ___003C_003Escan;

	internal static int[] ___003C_003EBLOCK_TO_CC;

	internal static int[] ___003C_003EBLOCK_POS_X;

	internal static int[] ___003C_003EBLOCK_POS_Y;

	internal static int[] ___003C_003ESTEP_Y;

	internal static int[] ___003C_003ESQUEEZE_X;

	internal static int[] ___003C_003ESQUEEZE_Y;

	public const int IntraCoded = 1;

	public const int PredictiveCoded = 2;

	public const int BiPredictiveCoded = 3;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcAddressIncrement
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcAddressIncrement;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcMBTypeI
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcMBTypeI;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType[] mbTypeValI
	{
		[HideFromJava]
		get
		{
			return ___003C_003EmbTypeValI;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcMBTypeP
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcMBTypeP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType[] mbTypeValP
	{
		[HideFromJava]
		get
		{
			return ___003C_003EmbTypeValP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcMBTypeB
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcMBTypeB;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType[] mbTypeValB
	{
		[HideFromJava]
		get
		{
			return ___003C_003EmbTypeValB;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcMBTypeISpat
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcMBTypeISpat;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType[] mbTypeValISpat
	{
		[HideFromJava]
		get
		{
			return ___003C_003EmbTypeValISpat;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcMBTypePSpat
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcMBTypePSpat;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType[] mbTypeValPSpat
	{
		[HideFromJava]
		get
		{
			return ___003C_003EmbTypeValPSpat;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcMBTypeBSpat
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcMBTypeBSpat;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType[] mbTypeValBSpat
	{
		[HideFromJava]
		get
		{
			return ___003C_003EmbTypeValBSpat;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcMBTypeSNR
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcMBTypeSNR;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType[] mbTypeValSNR
	{
		[HideFromJava]
		get
		{
			return ___003C_003EmbTypeValSNR;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcCBP
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcCBP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcMotionCode
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcMotionCode;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcDualPrime
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcDualPrime;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcDCSizeLuma
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcDCSizeLuma;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcDCSizeChroma
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcDCSizeChroma;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcCoeff0
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcCoeff0;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC vlcCoeff1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvlcCoeff1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] qScaleTab1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EqScaleTab1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] qScaleTab2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EqScaleTab2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultQMatIntra
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultQMatIntra;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultQMatInter
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultQMatInter;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] scan
	{
		[HideFromJava]
		get
		{
			return ___003C_003Escan;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLOCK_TO_CC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLOCK_TO_CC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLOCK_POS_X
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLOCK_POS_X;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLOCK_POS_Y
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLOCK_POS_Y;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] STEP_Y
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESTEP_Y;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] SQUEEZE_X
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESQUEEZE_X;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] SQUEEZE_Y
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESQUEEZE_Y;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public MPEGConst()
	{
	}

	[LineNumberTable(new byte[]
	{
		159,
		124,
		162,
		127,
		160,
		201,
		139,
		127,
		2,
		159,
		11,
		127,
		42,
		223,
		86,
		127,
		77,
		byte.MaxValue,
		160,
		85,
		69,
		127,
		26,
		191,
		56,
		127,
		122,
		byte.MaxValue,
		160,
		165,
		71,
		127,
		160,
		94,
		byte.MaxValue,
		160,
		229,
		72,
		127,
		10,
		191,
		26,
		127,
		161,
		224,
		139,
		159,
		160,
		67,
		159,
		10,
		127,
		86,
		159,
		86,
		103,
		114,
		114,
		110,
		111,
		110,
		114,
		110,
		114,
		114,
		111,
		114,
		114,
		114,
		110,
		114,
		114,
		114,
		110,
		110,
		111,
		114,
		114,
		114,
		114,
		114,
		110,
		111,
		114,
		114,
		114,
		114,
		114,
		114,
		110,
		111,
		111,
		111,
		111,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		111,
		111,
		111,
		111,
		111,
		111,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		140,
		103,
		114,
		114,
		110,
		111,
		110,
		114,
		110,
		114,
		114,
		111,
		114,
		114,
		114,
		110,
		114,
		114,
		114,
		110,
		110,
		111,
		114,
		114,
		114,
		114,
		114,
		110,
		111,
		114,
		114,
		114,
		114,
		114,
		114,
		110,
		111,
		111,
		111,
		111,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		111,
		111,
		111,
		111,
		111,
		111,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		111,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		172,
		159,
		160,
		96,
		191,
		160,
		92,
		223,
		161,
		36,
		223,
		161,
		37,
		byte.MaxValue,
		162,
		157,
		73,
		127,
		33,
		159,
		113,
		159,
		113,
		159,
		113,
		124
	})]
	static MPEGConst()
	{
		___003C_003EvlcAddressIncrement = VLC.createVLC(new string[33]
		{
			"1", "011", "010", "0011", "0010", "00011", "00010", "0000111", "0000110", "00001011",
			"00001010", "00001001", "00001000", "00000111", "00000110", "0000010111", "0000010110", "0000010101", "0000010100", "0000010011",
			"0000010010", "00000100011", "00000100010", "00000100001", "00000100000", "00000011111", "00000011110", "00000011101", "00000011100", "00000011011",
			"00000011010", "00000011001", "00000011000"
		});
		___003C_003EvlcMBTypeI = VLC.createVLC(new string[2] { "1", "01" });
		___003C_003EmbTypeValI = new MBType[2]
		{
			new MBType(0, 0, 0, 0, 1, 0, 0),
			new MBType(1, 0, 0, 0, 1, 0, 0)
		};
		___003C_003EvlcMBTypeP = VLC.createVLC(new string[7] { "1", "01", "001", "00011", "00010", "00001", "000001" });
		___003C_003EmbTypeValP = new MBType[7]
		{
			new MBType(0, 1, 0, 1, 0, 0, 0),
			new MBType(0, 0, 0, 1, 0, 0, 0),
			new MBType(0, 1, 0, 0, 0, 0, 0),
			new MBType(0, 0, 0, 0, 1, 0, 0),
			new MBType(1, 1, 0, 1, 0, 0, 0),
			new MBType(1, 0, 0, 1, 0, 0, 0),
			new MBType(1, 0, 0, 0, 1, 0, 0)
		};
		___003C_003EvlcMBTypeB = VLC.createVLC(new string[11]
		{
			"10", "11", "010", "011", "0010", "0011", "00011", "00010", "000011", "000010",
			"000001"
		});
		___003C_003EmbTypeValB = new MBType[11]
		{
			new MBType(0, 1, 1, 0, 0, 0, 0),
			new MBType(0, 1, 1, 1, 0, 0, 0),
			new MBType(0, 0, 1, 0, 0, 0, 0),
			new MBType(0, 0, 1, 1, 0, 0, 0),
			new MBType(0, 1, 0, 0, 0, 0, 0),
			new MBType(0, 1, 0, 1, 0, 0, 0),
			new MBType(0, 0, 0, 0, 1, 0, 0),
			new MBType(1, 1, 1, 1, 0, 0, 0),
			new MBType(1, 1, 0, 1, 0, 0, 0),
			new MBType(1, 0, 1, 1, 0, 0, 0),
			new MBType(1, 0, 0, 0, 1, 0, 0)
		};
		___003C_003EvlcMBTypeISpat = VLC.createVLC(new string[5] { "1", "01", "0011", "0010", "0001" });
		___003C_003EmbTypeValISpat = new MBType[5]
		{
			new MBType(0, 0, 0, 0, 1, 0, 0),
			new MBType(1, 0, 0, 0, 1, 0, 0),
			new MBType(0, 0, 0, 0, 1, 0, 0),
			new MBType(1, 0, 0, 0, 1, 0, 0),
			new MBType(0, 0, 0, 0, 0, 0, 0)
		};
		___003C_003EvlcMBTypePSpat = VLC.createVLC(new string[16]
		{
			"10", "011", "0000100", "000111", "0010", "0000111", "0011", "010", "000100", "0000110",
			"11", "000101", "000110", "0000101", "0000010", "0000011"
		});
		___003C_003EmbTypeValPSpat = new MBType[16]
		{
			new MBType(0, 1, 0, 1, 0, 0, 0),
			new MBType(0, 1, 0, 1, 0, 1, 0),
			new MBType(0, 0, 0, 1, 0, 0, 0),
			new MBType(0, 0, 0, 1, 0, 1, 0),
			new MBType(0, 1, 0, 0, 0, 0, 0),
			new MBType(0, 0, 0, 0, 1, 0, 0),
			new MBType(0, 1, 0, 0, 0, 1, 0),
			new MBType(1, 1, 0, 1, 0, 0, 0),
			new MBType(1, 0, 0, 1, 0, 0, 0),
			new MBType(1, 0, 0, 0, 1, 0, 0),
			new MBType(1, 1, 0, 1, 0, 1, 0),
			new MBType(1, 0, 0, 1, 0, 1, 0),
			new MBType(0, 0, 0, 0, 0, 1, 0),
			new MBType(0, 0, 0, 1, 0, 0, 0),
			new MBType(1, 0, 0, 1, 0, 0, 0),
			new MBType(0, 0, 0, 0, 0, 0, 0)
		};
		___003C_003EvlcMBTypeBSpat = VLC.createVLC(new string[20]
		{
			"10", "11", "010", "011", "0010", "0011", "000110", "000111", "000100", "000101",
			"0000110", "0000111", "0000100", "0000101", "00000100", "00000101", "000001100", "000001110", "000001101", "000001111"
		});
		___003C_003EmbTypeValBSpat = new MBType[20]
		{
			new MBType(0, 1, 1, 0, 0, 0, 0),
			new MBType(0, 1, 1, 1, 0, 0, 0),
			new MBType(0, 0, 1, 0, 0, 0, 0),
			new MBType(0, 0, 1, 1, 0, 0, 0),
			new MBType(0, 1, 0, 0, 0, 0, 0),
			new MBType(0, 1, 0, 1, 0, 0, 0),
			new MBType(0, 0, 1, 0, 0, 1, 0),
			new MBType(0, 0, 1, 1, 0, 1, 0),
			new MBType(0, 1, 0, 0, 0, 1, 0),
			new MBType(0, 1, 0, 1, 0, 1, 0),
			new MBType(0, 0, 0, 0, 1, 0, 0),
			new MBType(1, 1, 1, 1, 0, 0, 0),
			new MBType(1, 1, 0, 1, 0, 0, 0),
			new MBType(1, 0, 1, 1, 0, 0, 0),
			new MBType(1, 0, 0, 0, 1, 0, 0),
			new MBType(1, 1, 0, 1, 0, 1, 0),
			new MBType(1, 0, 1, 1, 0, 1, 0),
			new MBType(0, 0, 0, 0, 0, 0, 0),
			new MBType(1, 0, 0, 1, 0, 0, 0),
			new MBType(0, 0, 0, 1, 0, 0, 0)
		};
		___003C_003EvlcMBTypeSNR = VLC.createVLC(new string[3] { "1", "01", "001" });
		___003C_003EmbTypeValSNR = new MBType[3]
		{
			new MBType(0, 0, 0, 1, 0, 0, 0),
			new MBType(1, 0, 0, 1, 0, 0, 0),
			new MBType(0, 0, 0, 0, 0, 0, 0)
		};
		___003C_003EvlcCBP = VLC.createVLC(new string[64]
		{
			"000000001", "01011", "01001", "001101", "1101", "0010111", "0010011", "00011111", "1100", "0010110",
			"0010010", "00011110", "10011", "00011011", "00010111", "00010011", "1011", "0010101", "0010001", "00011101",
			"10001", "00011001", "00010101", "00010001", "001111", "00001111", "00001101", "000000011", "01111", "00001011",
			"00000111", "000000111", "1010", "0010100", "0010000", "00011100", "001110", "00001110", "00001100", "000000010",
			"10000", "00011000", "00010100", "00010000", "01110", "00001010", "00000110", "000000110", "10010", "00011010",
			"00010110", "00010010", "01101", "00001001", "00000101", "000000101", "01100", "00001000", "00000100", "000000100",
			"111", "01010", "01000", "001100"
		});
		___003C_003EvlcMotionCode = VLC.createVLC(new string[17]
		{
			"1", "01", "001", "0001", "000011", "0000101", "0000100", "0000011", "000001011", "000001010",
			"000001001", "0000010001", "0000010000", "0000001111", "0000001110", "0000001101", "0000001100"
		});
		___003C_003EvlcDualPrime = VLC.createVLC(new string[3] { "11", "0", "10" });
		___003C_003EvlcDCSizeLuma = VLC.createVLC(new string[12]
		{
			"100", "00", "01", "101", "110", "1110", "11110", "111110", "1111110", "11111110",
			"111111110", "111111111"
		});
		___003C_003EvlcDCSizeChroma = VLC.createVLC(new string[12]
		{
			"00", "01", "10", "110", "1110", "11110", "111110", "1111110", "11111110", "111111110",
			"1111111110", "1111111111"
		});
		VLCBuilder vlcCoeffBldr = new VLCBuilder();
		vlcCoeffBldr.set(2049, "000001");
		vlcCoeffBldr.set(2048, "10");
		vlcCoeffBldr.set(1, "11");
		vlcCoeffBldr.set(65, "011");
		vlcCoeffBldr.set(2, "0100");
		vlcCoeffBldr.set(129, "0101");
		vlcCoeffBldr.set(3, "00101");
		vlcCoeffBldr.set(193, "00111");
		vlcCoeffBldr.set(257, "00110");
		vlcCoeffBldr.set(66, "000110");
		vlcCoeffBldr.set(321, "000111");
		vlcCoeffBldr.set(385, "000101");
		vlcCoeffBldr.set(449, "000100");
		vlcCoeffBldr.set(4, "0000110");
		vlcCoeffBldr.set(130, "0000100");
		vlcCoeffBldr.set(513, "0000111");
		vlcCoeffBldr.set(577, "0000101");
		vlcCoeffBldr.set(5, "00100110");
		vlcCoeffBldr.set(6, "00100001");
		vlcCoeffBldr.set(67, "00100101");
		vlcCoeffBldr.set(194, "00100100");
		vlcCoeffBldr.set(641, "00100111");
		vlcCoeffBldr.set(705, "00100011");
		vlcCoeffBldr.set(769, "00100010");
		vlcCoeffBldr.set(833, "00100000");
		vlcCoeffBldr.set(7, "0000001010");
		vlcCoeffBldr.set(68, "0000001100");
		vlcCoeffBldr.set(131, "0000001011");
		vlcCoeffBldr.set(258, "0000001111");
		vlcCoeffBldr.set(322, "0000001001");
		vlcCoeffBldr.set(897, "0000001110");
		vlcCoeffBldr.set(961, "0000001101");
		vlcCoeffBldr.set(1025, "0000001000");
		vlcCoeffBldr.set(8, "000000011101");
		vlcCoeffBldr.set(9, "000000011000");
		vlcCoeffBldr.set(10, "000000010011");
		vlcCoeffBldr.set(11, "000000010000");
		vlcCoeffBldr.set(69, "000000011011");
		vlcCoeffBldr.set(132, "000000010100");
		vlcCoeffBldr.set(195, "000000011100");
		vlcCoeffBldr.set(259, "000000010010");
		vlcCoeffBldr.set(386, "000000011110");
		vlcCoeffBldr.set(450, "000000010101");
		vlcCoeffBldr.set(514, "000000010001");
		vlcCoeffBldr.set(1089, "000000011111");
		vlcCoeffBldr.set(1153, "000000011010");
		vlcCoeffBldr.set(1217, "000000011001");
		vlcCoeffBldr.set(1281, "000000010111");
		vlcCoeffBldr.set(1345, "000000010110");
		vlcCoeffBldr.set(12, "0000000011010");
		vlcCoeffBldr.set(13, "0000000011001");
		vlcCoeffBldr.set(14, "0000000011000");
		vlcCoeffBldr.set(15, "0000000010111");
		vlcCoeffBldr.set(70, "0000000010110");
		vlcCoeffBldr.set(71, "0000000010101");
		vlcCoeffBldr.set(133, "0000000010100");
		vlcCoeffBldr.set(196, "0000000010011");
		vlcCoeffBldr.set(323, "0000000010010");
		vlcCoeffBldr.set(578, "0000000010001");
		vlcCoeffBldr.set(642, "0000000010000");
		vlcCoeffBldr.set(1409, "0000000011111");
		vlcCoeffBldr.set(1473, "0000000011110");
		vlcCoeffBldr.set(1537, "0000000011101");
		vlcCoeffBldr.set(1601, "0000000011100");
		vlcCoeffBldr.set(1665, "0000000011011");
		vlcCoeffBldr.set(16, "00000000011111");
		vlcCoeffBldr.set(17, "00000000011110");
		vlcCoeffBldr.set(18, "00000000011101");
		vlcCoeffBldr.set(19, "00000000011100");
		vlcCoeffBldr.set(20, "00000000011011");
		vlcCoeffBldr.set(21, "00000000011010");
		vlcCoeffBldr.set(22, "00000000011001");
		vlcCoeffBldr.set(23, "00000000011000");
		vlcCoeffBldr.set(24, "00000000010111");
		vlcCoeffBldr.set(25, "00000000010110");
		vlcCoeffBldr.set(26, "00000000010101");
		vlcCoeffBldr.set(27, "00000000010100");
		vlcCoeffBldr.set(28, "00000000010011");
		vlcCoeffBldr.set(29, "00000000010010");
		vlcCoeffBldr.set(30, "00000000010001");
		vlcCoeffBldr.set(31, "00000000010000");
		vlcCoeffBldr.set(32, "000000000011000");
		vlcCoeffBldr.set(33, "000000000010111");
		vlcCoeffBldr.set(34, "000000000010110");
		vlcCoeffBldr.set(35, "000000000010101");
		vlcCoeffBldr.set(36, "000000000010100");
		vlcCoeffBldr.set(37, "000000000010011");
		vlcCoeffBldr.set(38, "000000000010010");
		vlcCoeffBldr.set(39, "000000000010001");
		vlcCoeffBldr.set(40, "000000000010000");
		vlcCoeffBldr.set(72, "000000000011111");
		vlcCoeffBldr.set(73, "000000000011110");
		vlcCoeffBldr.set(74, "000000000011101");
		vlcCoeffBldr.set(75, "000000000011100");
		vlcCoeffBldr.set(76, "000000000011011");
		vlcCoeffBldr.set(77, "000000000011010");
		vlcCoeffBldr.set(78, "000000000011001");
		vlcCoeffBldr.set(79, "0000000000010011");
		vlcCoeffBldr.set(80, "0000000000010010");
		vlcCoeffBldr.set(81, "0000000000010001");
		vlcCoeffBldr.set(82, "0000000000010000");
		vlcCoeffBldr.set(387, "0000000000010100");
		vlcCoeffBldr.set(706, "0000000000011010");
		vlcCoeffBldr.set(770, "0000000000011001");
		vlcCoeffBldr.set(834, "0000000000011000");
		vlcCoeffBldr.set(898, "0000000000010111");
		vlcCoeffBldr.set(962, "0000000000010110");
		vlcCoeffBldr.set(1026, "0000000000010101");
		vlcCoeffBldr.set(1729, "0000000000011111");
		vlcCoeffBldr.set(1793, "0000000000011110");
		vlcCoeffBldr.set(1857, "0000000000011101");
		vlcCoeffBldr.set(1921, "0000000000011100");
		vlcCoeffBldr.set(1985, "0000000000011011");
		___003C_003EvlcCoeff0 = vlcCoeffBldr.getVLC();
		vlcCoeffBldr = new VLCBuilder();
		vlcCoeffBldr.set(2049, "000001");
		vlcCoeffBldr.set(2048, "0110");
		vlcCoeffBldr.set(1, "10");
		vlcCoeffBldr.set(65, "010");
		vlcCoeffBldr.set(2, "110");
		vlcCoeffBldr.set(129, "00101");
		vlcCoeffBldr.set(3, "0111");
		vlcCoeffBldr.set(193, "00111");
		vlcCoeffBldr.set(257, "000110");
		vlcCoeffBldr.set(66, "00110");
		vlcCoeffBldr.set(321, "000111");
		vlcCoeffBldr.set(385, "0000110");
		vlcCoeffBldr.set(449, "0000100");
		vlcCoeffBldr.set(4, "11100");
		vlcCoeffBldr.set(130, "0000111");
		vlcCoeffBldr.set(513, "0000101");
		vlcCoeffBldr.set(577, "1111000");
		vlcCoeffBldr.set(5, "11101");
		vlcCoeffBldr.set(6, "000101");
		vlcCoeffBldr.set(67, "1111001");
		vlcCoeffBldr.set(194, "00100110");
		vlcCoeffBldr.set(641, "1111010");
		vlcCoeffBldr.set(705, "00100001");
		vlcCoeffBldr.set(769, "00100101");
		vlcCoeffBldr.set(833, "00100100");
		vlcCoeffBldr.set(7, "000100");
		vlcCoeffBldr.set(68, "00100111");
		vlcCoeffBldr.set(131, "11111100");
		vlcCoeffBldr.set(258, "11111101");
		vlcCoeffBldr.set(322, "000000100");
		vlcCoeffBldr.set(897, "000000101");
		vlcCoeffBldr.set(961, "000000111");
		vlcCoeffBldr.set(1025, "0000001101");
		vlcCoeffBldr.set(8, "1111011");
		vlcCoeffBldr.set(9, "1111100");
		vlcCoeffBldr.set(10, "00100011");
		vlcCoeffBldr.set(11, "00100010");
		vlcCoeffBldr.set(69, "00100000");
		vlcCoeffBldr.set(132, "0000001100");
		vlcCoeffBldr.set(195, "000000011100");
		vlcCoeffBldr.set(259, "000000010010");
		vlcCoeffBldr.set(386, "000000011110");
		vlcCoeffBldr.set(450, "000000010101");
		vlcCoeffBldr.set(514, "000000010001");
		vlcCoeffBldr.set(1089, "000000011111");
		vlcCoeffBldr.set(1153, "000000011010");
		vlcCoeffBldr.set(1217, "000000011001");
		vlcCoeffBldr.set(1281, "000000010111");
		vlcCoeffBldr.set(1345, "000000010110");
		vlcCoeffBldr.set(12, "11111010");
		vlcCoeffBldr.set(13, "11111011");
		vlcCoeffBldr.set(14, "11111110");
		vlcCoeffBldr.set(15, "11111111");
		vlcCoeffBldr.set(70, "0000000010110");
		vlcCoeffBldr.set(71, "0000000010101");
		vlcCoeffBldr.set(133, "0000000010100");
		vlcCoeffBldr.set(196, "0000000010011");
		vlcCoeffBldr.set(323, "0000000010010");
		vlcCoeffBldr.set(578, "0000000010001");
		vlcCoeffBldr.set(642, "0000000010000");
		vlcCoeffBldr.set(1409, "0000000011111");
		vlcCoeffBldr.set(1473, "0000000011110");
		vlcCoeffBldr.set(1537, "0000000011101");
		vlcCoeffBldr.set(1601, "0000000011100");
		vlcCoeffBldr.set(1665, "0000000011011");
		vlcCoeffBldr.set(16, "00000000011111");
		vlcCoeffBldr.set(17, "00000000011110");
		vlcCoeffBldr.set(18, "00000000011101");
		vlcCoeffBldr.set(19, "00000000011100");
		vlcCoeffBldr.set(20, "00000000011011");
		vlcCoeffBldr.set(21, "00000000011010");
		vlcCoeffBldr.set(22, "00000000011001");
		vlcCoeffBldr.set(23, "00000000011000");
		vlcCoeffBldr.set(24, "00000000010111");
		vlcCoeffBldr.set(25, "00000000010110");
		vlcCoeffBldr.set(26, "00000000010101");
		vlcCoeffBldr.set(27, "00000000010100");
		vlcCoeffBldr.set(28, "00000000010011");
		vlcCoeffBldr.set(29, "00000000010010");
		vlcCoeffBldr.set(30, "00000000010001");
		vlcCoeffBldr.set(31, "00000000010000");
		vlcCoeffBldr.set(32, "000000000011000");
		vlcCoeffBldr.set(33, "000000000010111");
		vlcCoeffBldr.set(34, "000000000010110");
		vlcCoeffBldr.set(35, "000000000010101");
		vlcCoeffBldr.set(36, "000000000010100");
		vlcCoeffBldr.set(37, "000000000010011");
		vlcCoeffBldr.set(38, "000000000010010");
		vlcCoeffBldr.set(39, "000000000010001");
		vlcCoeffBldr.set(40, "000000000010000");
		vlcCoeffBldr.set(72, "000000000011111");
		vlcCoeffBldr.set(73, "000000000011110");
		vlcCoeffBldr.set(74, "000000000011101");
		vlcCoeffBldr.set(75, "000000000011100");
		vlcCoeffBldr.set(76, "000000000011011");
		vlcCoeffBldr.set(77, "000000000011010");
		vlcCoeffBldr.set(78, "000000000011001");
		vlcCoeffBldr.set(79, "0000000000010011");
		vlcCoeffBldr.set(80, "0000000000010010");
		vlcCoeffBldr.set(81, "0000000000010001");
		vlcCoeffBldr.set(82, "0000000000010000");
		vlcCoeffBldr.set(387, "0000000000010100");
		vlcCoeffBldr.set(706, "0000000000011010");
		vlcCoeffBldr.set(770, "0000000000011001");
		vlcCoeffBldr.set(834, "0000000000011000");
		vlcCoeffBldr.set(898, "0000000000010111");
		vlcCoeffBldr.set(962, "0000000000010110");
		vlcCoeffBldr.set(1026, "0000000000010101");
		vlcCoeffBldr.set(1729, "0000000000011111");
		vlcCoeffBldr.set(1793, "0000000000011110");
		vlcCoeffBldr.set(1857, "0000000000011101");
		vlcCoeffBldr.set(1921, "0000000000011100");
		vlcCoeffBldr.set(1985, "0000000000011011");
		___003C_003EvlcCoeff1 = vlcCoeffBldr.getVLC();
		___003C_003EqScaleTab1 = new int[32]
		{
			0, 2, 4, 6, 8, 10, 12, 14, 16, 18,
			20, 22, 24, 26, 28, 30, 32, 34, 36, 38,
			40, 42, 44, 46, 48, 50, 52, 54, 56, 58,
			60, 62
		};
		___003C_003EqScaleTab2 = new int[32]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 10,
			12, 14, 16, 18, 20, 22, 24, 28, 32, 36,
			40, 44, 48, 52, 56, 64, 72, 80, 88, 96,
			104, 112
		};
		___003C_003EdefaultQMatIntra = new int[64]
		{
			8, 16, 19, 22, 26, 27, 29, 34, 16, 16,
			22, 24, 27, 29, 34, 37, 19, 22, 26, 27,
			29, 34, 34, 38, 22, 22, 26, 27, 29, 34,
			37, 40, 22, 26, 27, 29, 32, 35, 40, 48,
			26, 27, 29, 32, 35, 40, 48, 58, 26, 27,
			29, 34, 38, 46, 56, 69, 27, 29, 35, 38,
			46, 56, 69, 83
		};
		___003C_003EdefaultQMatInter = new int[64]
		{
			16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
			16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
			16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
			16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
			16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
			16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
			16, 16, 16, 16
		};
		___003C_003Escan = new int[2][]
		{
			new int[64]
			{
				0, 1, 8, 16, 9, 2, 3, 10, 17, 24,
				32, 25, 18, 11, 4, 5, 12, 19, 26, 33,
				40, 48, 41, 34, 27, 20, 13, 6, 7, 14,
				21, 28, 35, 42, 49, 56, 57, 50, 43, 36,
				29, 22, 15, 23, 30, 37, 44, 51, 58, 59,
				52, 45, 38, 31, 39, 46, 53, 60, 61, 54,
				47, 55, 62, 63
			},
			new int[64]
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
		___003C_003EBLOCK_TO_CC = new int[12]
		{
			0, 0, 0, 0, 1, 2, 1, 2, 1, 2,
			1, 2
		};
		___003C_003EBLOCK_POS_X = new int[28]
		{
			0, 8, 0, 8, 0, 0, 0, 0, 8, 8,
			8, 8, 0, 0, 0, 0, 0, 8, 0, 8,
			0, 0, 0, 0, 8, 8, 8, 8
		};
		___003C_003EBLOCK_POS_Y = new int[28]
		{
			0, 0, 8, 8, 0, 0, 8, 8, 0, 0,
			8, 8, 0, 0, 0, 0, 0, 0, 1, 1,
			0, 0, 1, 1, 0, 0, 1, 1
		};
		___003C_003ESTEP_Y = new int[28]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1
		};
		___003C_003ESQUEEZE_X = new int[4] { 0, 1, 1, 0 };
		___003C_003ESQUEEZE_Y = new int[4] { 0, 1, 0, 0 };
	}
}
