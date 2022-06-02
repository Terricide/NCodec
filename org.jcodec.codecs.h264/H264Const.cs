using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.util;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264;

public class H264Const : java.lang.Object
{
	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/codecs/h264/H264Const$PartPred;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class PartPred : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			L0,
			L1,
			Bi,
			Direct
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static PartPred ___003C_003EL0;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static PartPred ___003C_003EL1;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static PartPred ___003C_003EBi;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static PartPred ___003C_003EDirect;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static PartPred[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static PartPred L0
		{
			[HideFromJava]
			get
			{
				return ___003C_003EL0;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static PartPred L1
		{
			[HideFromJava]
			get
			{
				return ___003C_003EL1;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static PartPred Bi
		{
			[HideFromJava]
			get
			{
				return ___003C_003EBi;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static PartPred Direct
		{
			[HideFromJava]
			get
			{
				return ___003C_003EDirect;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(440)]
		private PartPred(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(440)]
		public static PartPred[] values()
		{
			return (PartPred[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(440)]
		public static PartPred valueOf(string name)
		{
			return (PartPred)java.lang.Enum.valueOf(ClassLiteral<PartPred>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 32, 98, 63, 34 })]
		static PartPred()
		{
			___003C_003EL0 = new PartPred("L0", 0);
			___003C_003EL1 = new PartPred("L1", 1);
			___003C_003EBi = new PartPred("Bi", 2);
			___003C_003EDirect = new PartPred("Direct", 3);
			_0024VALUES = new PartPred[4] { ___003C_003EL0, ___003C_003EL1, ___003C_003EBi, ___003C_003EDirect };
		}
	}

	internal static VLC[] ___003C_003ECoeffToken;

	internal static VLC ___003C_003EcoeffTokenChromaDCY420;

	internal static VLC ___003C_003EcoeffTokenChromaDCY422;

	internal static VLC[] ___003C_003Erun;

	internal static VLC[] ___003C_003EtotalZeros16;

	internal static VLC[] ___003C_003EtotalZeros4;

	internal static VLC[] ___003C_003EtotalZeros8;

	internal static PartPred[][] ___003C_003EbPredModes;

	internal static MBType[] ___003C_003EbMbTypes;

	internal static int[] ___003C_003EbPartW;

	internal static int[] ___003C_003EbPartH;

	internal static int[] ___003C_003EBLK_X;

	internal static int[] ___003C_003EBLK_Y;

	internal static int[] ___003C_003EBLK_8x8_X;

	internal static int[] ___003C_003EBLK_8x8_Y;

	internal static int[] ___003C_003EBLK_INV_MAP;

	internal static int[] ___003C_003EMB_BLK_OFF_LEFT;

	internal static int[] ___003C_003EMB_BLK_OFF_TOP;

	internal static int[] ___003C_003EQP_SCALE_CR;

	internal static Picture ___003C_003ENO_PIC;

	internal static int[] ___003C_003EBLK_8x8_MB_OFF_LUMA;

	internal static int[] ___003C_003EBLK_8x8_MB_OFF_CHROMA;

	internal static int[] ___003C_003EBLK_4x4_MB_OFF_LUMA;

	internal static int[] ___003C_003EBLK_8x8_IND;

	internal static int[][] ___003C_003EBLK8x8_BLOCKS;

	internal static int[][] ___003C_003EARRAY;

	internal static int[] ___003C_003ECODED_BLOCK_PATTERN_INTRA_COLOR;

	internal static int[] ___003C_003Ecoded_block_pattern_intra_monochrome;

	internal static int[] ___003C_003ECODED_BLOCK_PATTERN_INTER_COLOR;

	internal static int[] ___003C_003ECODED_BLOCK_PATTERN_INTER_COLOR_INV;

	internal static int[] ___003C_003Ecoded_block_pattern_inter_monochrome;

	internal static int[] ___003C_003Esig_coeff_map_8x8;

	internal static int[] ___003C_003Esig_coeff_map_8x8_mbaff;

	internal static int[] ___003C_003Elast_sig_coeff_map_8x8;

	internal static int[] ___003C_003EidentityMapping16;

	internal static int[] ___003C_003EidentityMapping4;

	internal static PartPred[] ___003C_003EbPartPredModes;

	internal static int[] ___003C_003EbSubMbTypes;

	internal static int[] ___003C_003ELUMA_4x4_BLOCK_LUT;

	internal static int[] ___003C_003ELUMA_4x4_POS_LUT;

	internal static int[] ___003C_003ELUMA_8x8_BLOCK_LUT;

	internal static int[] ___003C_003ELUMA_8x8_POS_LUT;

	internal static int[] ___003C_003ECHROMA_BLOCK_LUT;

	internal static int[] ___003C_003ECHROMA_POS_LUT;

	internal static int[][] ___003C_003ECOMP_BLOCK_4x4_LUT;

	internal static int[][] ___003C_003ECOMP_POS_4x4_LUT;

	internal static int[][] ___003C_003ECOMP_BLOCK_8x8_LUT;

	internal static int[][] ___003C_003ECOMP_POS_8x8_LUT;

	internal static int[][] ___003C_003EPIX_MAP_SPLIT_4x4;

	internal static int[][] ___003C_003EPIX_MAP_SPLIT_2x2;

	public const int PROFILE_CAVLC_INTRA = 44;

	public const int PROFILE_BASELINE = 66;

	public const int PROFILE_MAIN = 77;

	public const int PROFILE_EXTENDED = 88;

	public const int PROFILE_HIGH = 100;

	public const int PROFILE_HIGH_10 = 110;

	public const int PROFILE_HIGH_422 = 122;

	public const int PROFILE_HIGH_444 = 244;

	internal static int[] ___003C_003EdefaultScalingList4x4Intra;

	internal static int[] ___003C_003EdefaultScalingList4x4Inter;

	internal static int[] ___003C_003EdefaultScalingList8x8Intra;

	internal static int[] ___003C_003EdefaultScalingList8x8Inter;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC[] CoeffToken
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECoeffToken;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC coeffTokenChromaDCY420
	{
		[HideFromJava]
		get
		{
			return ___003C_003EcoeffTokenChromaDCY420;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC coeffTokenChromaDCY422
	{
		[HideFromJava]
		get
		{
			return ___003C_003EcoeffTokenChromaDCY422;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC[] run
	{
		[HideFromJava]
		get
		{
			return ___003C_003Erun;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC[] totalZeros16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EtotalZeros16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC[] totalZeros4
	{
		[HideFromJava]
		get
		{
			return ___003C_003EtotalZeros4;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static VLC[] totalZeros8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EtotalZeros8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static PartPred[][] bPredModes
	{
		[HideFromJava]
		get
		{
			return ___003C_003EbPredModes;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType[] bMbTypes
	{
		[HideFromJava]
		get
		{
			return ___003C_003EbMbTypes;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] bPartW
	{
		[HideFromJava]
		get
		{
			return ___003C_003EbPartW;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] bPartH
	{
		[HideFromJava]
		get
		{
			return ___003C_003EbPartH;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLK_X
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLK_X;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLK_Y
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLK_Y;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLK_8x8_X
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLK_8x8_X;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLK_8x8_Y
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLK_8x8_Y;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLK_INV_MAP
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLK_INV_MAP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] MB_BLK_OFF_LEFT
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMB_BLK_OFF_LEFT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] MB_BLK_OFF_TOP
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMB_BLK_OFF_TOP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] QP_SCALE_CR
	{
		[HideFromJava]
		get
		{
			return ___003C_003EQP_SCALE_CR;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Picture NO_PIC
	{
		[HideFromJava]
		get
		{
			return ___003C_003ENO_PIC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLK_8x8_MB_OFF_LUMA
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLK_8x8_MB_OFF_LUMA;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLK_8x8_MB_OFF_CHROMA
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLK_8x8_MB_OFF_CHROMA;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLK_4x4_MB_OFF_LUMA
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLK_4x4_MB_OFF_LUMA;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] BLK_8x8_IND
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLK_8x8_IND;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] BLK8x8_BLOCKS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLK8x8_BLOCKS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] ARRAY
	{
		[HideFromJava]
		get
		{
			return ___003C_003EARRAY;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] CODED_BLOCK_PATTERN_INTRA_COLOR
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECODED_BLOCK_PATTERN_INTRA_COLOR;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] coded_block_pattern_intra_monochrome
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ecoded_block_pattern_intra_monochrome;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] CODED_BLOCK_PATTERN_INTER_COLOR
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECODED_BLOCK_PATTERN_INTER_COLOR;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] CODED_BLOCK_PATTERN_INTER_COLOR_INV
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECODED_BLOCK_PATTERN_INTER_COLOR_INV;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] coded_block_pattern_inter_monochrome
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ecoded_block_pattern_inter_monochrome;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] sig_coeff_map_8x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003Esig_coeff_map_8x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] sig_coeff_map_8x8_mbaff
	{
		[HideFromJava]
		get
		{
			return ___003C_003Esig_coeff_map_8x8_mbaff;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] last_sig_coeff_map_8x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003Elast_sig_coeff_map_8x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] identityMapping16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EidentityMapping16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] identityMapping4
	{
		[HideFromJava]
		get
		{
			return ___003C_003EidentityMapping4;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static PartPred[] bPartPredModes
	{
		[HideFromJava]
		get
		{
			return ___003C_003EbPartPredModes;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] bSubMbTypes
	{
		[HideFromJava]
		get
		{
			return ___003C_003EbSubMbTypes;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] LUMA_4x4_BLOCK_LUT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELUMA_4x4_BLOCK_LUT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] LUMA_4x4_POS_LUT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELUMA_4x4_POS_LUT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] LUMA_8x8_BLOCK_LUT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELUMA_8x8_BLOCK_LUT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] LUMA_8x8_POS_LUT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELUMA_8x8_POS_LUT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] CHROMA_BLOCK_LUT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHROMA_BLOCK_LUT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] CHROMA_POS_LUT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHROMA_POS_LUT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] COMP_BLOCK_4x4_LUT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECOMP_BLOCK_4x4_LUT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] COMP_POS_4x4_LUT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECOMP_POS_4x4_LUT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] COMP_BLOCK_8x8_LUT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECOMP_BLOCK_8x8_LUT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] COMP_POS_8x8_LUT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECOMP_POS_8x8_LUT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] PIX_MAP_SPLIT_4x4
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPIX_MAP_SPLIT_4x4;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] PIX_MAP_SPLIT_2x2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPIX_MAP_SPLIT_2x2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultScalingList4x4Intra
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultScalingList4x4Intra;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultScalingList4x4Inter
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultScalingList4x4Inter;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultScalingList8x8Intra
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultScalingList8x8Intra;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultScalingList8x8Inter
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultScalingList8x8Inter;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(609)]
	public static bool usesList(PartPred pred, int l)
	{
		return pred == PartPred.___003C_003EBi || (((pred == PartPred.___003C_003EL0 && l == 0) || (pred == PartPred.___003C_003EL1 && l == 1)) ? true : false);
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(613)]
	public static int coeffToken(int totalCoeff, int trailingOnes)
	{
		return (totalCoeff << 4) | trailingOnes;
	}

	[LineNumberTable(new byte[] { 159, 17, 98, 105, 104, 39, 167 })]
	private static int[] inverse(int[] arr)
	{
		int[] inv = new int[(nint)arr.LongLength];
		for (int i = 0; i < (nint)inv.LongLength; i++)
		{
			inv[arr[i]] = i;
		}
		return inv;
	}

	[LineNumberTable(new byte[]
	{
		158,
		byte.MaxValue,
		98,
		113,
		103,
		44,
		135,
		101,
		230,
		60,
		231,
		70
	})]
	private static void putBlk(int[] _in, int blkX, int blkY, int blkW, int blkH, int stride, int[] @out)
	{
		int line = 0;
		int srcOff = 0;
		int dstOff = blkY * stride + blkX;
		for (; line < blkH; line++)
		{
			for (int i = 0; i < blkW; i++)
			{
				@out[dstOff + i] = _in[srcOff + i];
			}
			srcOff += blkW;
			dstOff += stride;
		}
	}

	[LineNumberTable(new byte[]
	{
		158, 253, 130, 191, 160, 164, 110, 105, 106, 47,
		41, 209, 230, 59, 234, 71
	})]
	private static int[][] buildPixSplitMap4x4()
	{
		int[][] result = new int[16][]
		{
			new int[16]
			{
				0, 1, 2, 3, 16, 17, 18, 19, 32, 33,
				34, 35, 48, 49, 50, 51
			},
			new int[16],
			new int[16],
			new int[16],
			new int[16],
			new int[16],
			new int[16],
			new int[16],
			new int[16],
			new int[16],
			new int[16],
			new int[16],
			new int[16],
			new int[16],
			new int[16],
			new int[16]
		};
		int blkY = 0;
		int blk = 0;
		int off = 0;
		for (; blkY < 4; blkY++)
		{
			int blkX = 0;
			while (blkX < 4)
			{
				for (int i = 0; i < 16; i++)
				{
					result[blk][i] = result[0][i] + off;
				}
				blkX++;
				blk++;
				off += 4;
			}
			off += 48;
		}
		return result;
	}

	[LineNumberTable(new byte[]
	{
		158, 249, 66, 159, 99, 110, 105, 106, 47, 41,
		209, 230, 59, 234, 71
	})]
	private static int[][] buildPixSplitMap2x2()
	{
		int[][] result = new int[4][]
		{
			new int[16]
			{
				0, 1, 2, 3, 8, 9, 10, 11, 16, 17,
				18, 19, 24, 25, 26, 27
			},
			new int[16],
			new int[16],
			new int[16]
		};
		int blkY = 0;
		int blk = 0;
		int off = 0;
		for (; blkY < 2; blkY++)
		{
			int blkX = 0;
			while (blkX < 2)
			{
				for (int i = 0; i < 16; i++)
				{
					result[blk][i] = result[0][i] + off;
				}
				blkX++;
				blk++;
				off += 4;
			}
			off += 24;
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(21)]
	public H264Const()
	{
	}

	[LineNumberTable(new byte[]
	{
		159,
		137,
		162,
		237,
		70,
		135,
		142,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		116,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		220,
		104,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		221,
		136,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		223,
		26,
		104,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		118,
		207,
		104,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		205,
		104,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		117,
		205,
		116,
		127,
		5,
		127,
		16,
		127,
		27,
		127,
		38,
		127,
		30,
		116,
		127,
		36,
		121,
		127,
		36,
		127,
		28,
		191,
		22,
		149,
		127,
		36,
		127,
		28,
		127,
		18,
		153,
		127,
		36,
		127,
		40,
		159,
		18,
		127,
		36,
		127,
		40,
		159,
		6,
		127,
		36,
		127,
		40,
		153,
		127,
		36,
		127,
		33,
		148,
		127,
		36,
		159,
		40,
		127,
		36,
		159,
		28,
		127,
		36,
		159,
		16,
		127,
		36,
		159,
		6,
		127,
		36,
		154,
		127,
		30,
		149,
		159,
		39,
		159,
		28,
		159,
		17,
		156,
		159,
		46,
		159,
		16,
		156,
		116,
		127,
		36,
		159,
		5,
		127,
		36,
		153,
		159,
		49,
		159,
		38,
		159,
		27,
		159,
		16,
		252,
		70,
		byte.MaxValue,
		161,
		180,
		72,
		byte.MaxValue,
		160,
		116,
		70,
		127,
		95,
		159,
		95,
		127,
		57,
		159,
		57,
		124,
		156,
		159,
		60,
		127,
		53,
		159,
		53,
		223,
		160,
		212,
		116,
		127,
		5,
		126,
		127,
		90,
		127,
		53,
		byte.MaxValue,
		88,
		70,
		159,
		33,
		223,
		160,
		188,
		191,
		60,
		byte.MaxValue,
		160,
		188,
		76,
		144,
		191,
		60,
		223,
		160,
		250,
		223,
		161,
		9,
		223,
		160,
		224,
		127,
		60,
		124,
		127,
		90,
		159,
		38,
		112,
		112,
		112,
		112,
		109,
		141,
		127,
		5,
		159,
		5,
		127,
		5,
		159,
		5,
		138,
		109,
		106,
		40,
		169,
		127,
		2,
		106,
		byte.MaxValue,
		2,
		58,
		236,
		72,
		108,
		106,
		40,
		169,
		127,
		1,
		106,
		byte.MaxValue,
		1,
		58,
		236,
		72,
		106,
		108,
		106,
		40,
		169,
		127,
		2,
		106,
		byte.MaxValue,
		2,
		58,
		236,
		118,
		107,
		235,
		75,
		159,
		68,
		159,
		69,
		191,
		161,
		36
	})]
	static H264Const()
	{
		___003C_003ECoeffToken = new VLC[10];
		VLCBuilder vbl6 = new VLCBuilder();
		vbl6.set(0, "1");
		vbl6.set(coeffToken(1, 0), "000101");
		vbl6.set(coeffToken(1, 1), "01");
		vbl6.set(coeffToken(2, 0), "00000111");
		vbl6.set(coeffToken(2, 1), "000100");
		vbl6.set(coeffToken(2, 2), "001");
		vbl6.set(coeffToken(3, 0), "000000111");
		vbl6.set(coeffToken(3, 1), "00000110");
		vbl6.set(coeffToken(3, 2), "0000101");
		vbl6.set(coeffToken(3, 3), "00011");
		vbl6.set(coeffToken(4, 0), "0000000111");
		vbl6.set(coeffToken(4, 1), "000000110");
		vbl6.set(coeffToken(4, 2), "00000101");
		vbl6.set(coeffToken(4, 3), "000011");
		vbl6.set(coeffToken(5, 0), "00000000111");
		vbl6.set(coeffToken(5, 1), "0000000110");
		vbl6.set(coeffToken(5, 2), "000000101");
		vbl6.set(coeffToken(5, 3), "0000100");
		vbl6.set(coeffToken(6, 0), "0000000001111");
		vbl6.set(coeffToken(6, 1), "00000000110");
		vbl6.set(coeffToken(6, 2), "0000000101");
		vbl6.set(coeffToken(6, 3), "00000100");
		vbl6.set(coeffToken(7, 0), "0000000001011");
		vbl6.set(coeffToken(7, 1), "0000000001110");
		vbl6.set(coeffToken(7, 2), "00000000101");
		vbl6.set(coeffToken(7, 3), "000000100");
		vbl6.set(coeffToken(8, 0), "0000000001000");
		vbl6.set(coeffToken(8, 1), "0000000001010");
		vbl6.set(coeffToken(8, 2), "0000000001101");
		vbl6.set(coeffToken(8, 3), "0000000100");
		vbl6.set(coeffToken(9, 0), "00000000001111");
		vbl6.set(coeffToken(9, 1), "00000000001110");
		vbl6.set(coeffToken(9, 2), "0000000001001");
		vbl6.set(coeffToken(9, 3), "00000000100");
		vbl6.set(coeffToken(10, 0), "00000000001011");
		vbl6.set(coeffToken(10, 1), "00000000001010");
		vbl6.set(coeffToken(10, 2), "00000000001101");
		vbl6.set(coeffToken(10, 3), "0000000001100");
		vbl6.set(coeffToken(11, 0), "000000000001111");
		vbl6.set(coeffToken(11, 1), "000000000001110");
		vbl6.set(coeffToken(11, 2), "00000000001001");
		vbl6.set(coeffToken(11, 3), "00000000001100");
		vbl6.set(coeffToken(12, 0), "000000000001011");
		vbl6.set(coeffToken(12, 1), "000000000001010");
		vbl6.set(coeffToken(12, 2), "000000000001101");
		vbl6.set(coeffToken(12, 3), "00000000001000");
		vbl6.set(coeffToken(13, 0), "0000000000001111");
		vbl6.set(coeffToken(13, 1), "000000000000001");
		vbl6.set(coeffToken(13, 2), "000000000001001");
		vbl6.set(coeffToken(13, 3), "000000000001100");
		vbl6.set(coeffToken(14, 0), "0000000000001011");
		vbl6.set(coeffToken(14, 1), "0000000000001110");
		vbl6.set(coeffToken(14, 2), "0000000000001101");
		vbl6.set(coeffToken(14, 3), "000000000001000");
		vbl6.set(coeffToken(15, 0), "0000000000000111");
		vbl6.set(coeffToken(15, 1), "0000000000001010");
		vbl6.set(coeffToken(15, 2), "0000000000001001");
		vbl6.set(coeffToken(15, 3), "0000000000001100");
		vbl6.set(coeffToken(16, 0), "0000000000000100");
		vbl6.set(coeffToken(16, 1), "0000000000000110");
		vbl6.set(coeffToken(16, 2), "0000000000000101");
		vbl6.set(coeffToken(16, 3), "0000000000001000");
		VLC[] __003C_003ECoeffToken = ___003C_003ECoeffToken;
		VLC[] __003C_003ECoeffToken2 = ___003C_003ECoeffToken;
		VLC vLC = vbl6.getVLC();
		int num = 1;
		VLC[] array = __003C_003ECoeffToken2;
		VLC vLC2 = vLC;
		array[num] = vLC;
		__003C_003ECoeffToken[0] = vLC2;
		VLCBuilder vbl5 = new VLCBuilder();
		vbl5.set(coeffToken(0, 0), "11");
		vbl5.set(coeffToken(1, 0), "001011");
		vbl5.set(coeffToken(1, 1), "10");
		vbl5.set(coeffToken(2, 0), "000111");
		vbl5.set(coeffToken(2, 1), "00111");
		vbl5.set(coeffToken(2, 2), "011");
		vbl5.set(coeffToken(3, 0), "0000111");
		vbl5.set(coeffToken(3, 1), "001010");
		vbl5.set(coeffToken(3, 2), "001001");
		vbl5.set(coeffToken(3, 3), "0101");
		vbl5.set(coeffToken(4, 0), "00000111");
		vbl5.set(coeffToken(4, 1), "000110");
		vbl5.set(coeffToken(4, 2), "000101");
		vbl5.set(coeffToken(4, 3), "0100");
		vbl5.set(coeffToken(5, 0), "00000100");
		vbl5.set(coeffToken(5, 1), "0000110");
		vbl5.set(coeffToken(5, 2), "0000101");
		vbl5.set(coeffToken(5, 3), "00110");
		vbl5.set(coeffToken(6, 0), "000000111");
		vbl5.set(coeffToken(6, 1), "00000110");
		vbl5.set(coeffToken(6, 2), "00000101");
		vbl5.set(coeffToken(6, 3), "001000");
		vbl5.set(coeffToken(7, 0), "00000001111");
		vbl5.set(coeffToken(7, 1), "000000110");
		vbl5.set(coeffToken(7, 2), "000000101");
		vbl5.set(coeffToken(7, 3), "000100");
		vbl5.set(coeffToken(8, 0), "00000001011");
		vbl5.set(coeffToken(8, 1), "00000001110");
		vbl5.set(coeffToken(8, 2), "00000001101");
		vbl5.set(coeffToken(8, 3), "0000100");
		vbl5.set(coeffToken(9, 0), "000000001111");
		vbl5.set(coeffToken(9, 1), "00000001010");
		vbl5.set(coeffToken(9, 2), "00000001001");
		vbl5.set(coeffToken(9, 3), "000000100");
		vbl5.set(coeffToken(10, 0), "000000001011");
		vbl5.set(coeffToken(10, 1), "000000001110");
		vbl5.set(coeffToken(10, 2), "000000001101");
		vbl5.set(coeffToken(10, 3), "00000001100");
		vbl5.set(coeffToken(11, 0), "000000001000");
		vbl5.set(coeffToken(11, 1), "000000001010");
		vbl5.set(coeffToken(11, 2), "000000001001");
		vbl5.set(coeffToken(11, 3), "00000001000");
		vbl5.set(coeffToken(12, 0), "0000000001111");
		vbl5.set(coeffToken(12, 1), "0000000001110");
		vbl5.set(coeffToken(12, 2), "0000000001101");
		vbl5.set(coeffToken(12, 3), "000000001100");
		vbl5.set(coeffToken(13, 0), "0000000001011");
		vbl5.set(coeffToken(13, 1), "0000000001010");
		vbl5.set(coeffToken(13, 2), "0000000001001");
		vbl5.set(coeffToken(13, 3), "0000000001100");
		vbl5.set(coeffToken(14, 0), "0000000000111");
		vbl5.set(coeffToken(14, 1), "00000000001011");
		vbl5.set(coeffToken(14, 2), "0000000000110");
		vbl5.set(coeffToken(14, 3), "0000000001000");
		vbl5.set(coeffToken(15, 0), "00000000001001");
		vbl5.set(coeffToken(15, 1), "00000000001000");
		vbl5.set(coeffToken(15, 2), "00000000001010");
		vbl5.set(coeffToken(15, 3), "0000000000001");
		vbl5.set(coeffToken(16, 0), "00000000000111");
		vbl5.set(coeffToken(16, 1), "00000000000110");
		vbl5.set(coeffToken(16, 2), "00000000000101");
		vbl5.set(coeffToken(16, 3), "00000000000100");
		VLC[] __003C_003ECoeffToken3 = ___003C_003ECoeffToken;
		VLC[] __003C_003ECoeffToken4 = ___003C_003ECoeffToken;
		vLC = vbl5.getVLC();
		num = 3;
		array = __003C_003ECoeffToken4;
		VLC vLC3 = vLC;
		array[num] = vLC;
		__003C_003ECoeffToken3[2] = vLC3;
		VLCBuilder vbl4 = new VLCBuilder();
		vbl4.set(coeffToken(0, 0), "1111");
		vbl4.set(coeffToken(1, 0), "001111");
		vbl4.set(coeffToken(1, 1), "1110");
		vbl4.set(coeffToken(2, 0), "001011");
		vbl4.set(coeffToken(2, 1), "01111");
		vbl4.set(coeffToken(2, 2), "1101");
		vbl4.set(coeffToken(3, 0), "001000");
		vbl4.set(coeffToken(3, 1), "01100");
		vbl4.set(coeffToken(3, 2), "01110");
		vbl4.set(coeffToken(3, 3), "1100");
		vbl4.set(coeffToken(4, 0), "0001111");
		vbl4.set(coeffToken(4, 1), "01010");
		vbl4.set(coeffToken(4, 2), "01011");
		vbl4.set(coeffToken(4, 3), "1011");
		vbl4.set(coeffToken(5, 0), "0001011");
		vbl4.set(coeffToken(5, 1), "01000");
		vbl4.set(coeffToken(5, 2), "01001");
		vbl4.set(coeffToken(5, 3), "1010");
		vbl4.set(coeffToken(6, 0), "0001001");
		vbl4.set(coeffToken(6, 1), "001110");
		vbl4.set(coeffToken(6, 2), "001101");
		vbl4.set(coeffToken(6, 3), "1001");
		vbl4.set(coeffToken(7, 0), "0001000");
		vbl4.set(coeffToken(7, 1), "001010");
		vbl4.set(coeffToken(7, 2), "001001");
		vbl4.set(coeffToken(7, 3), "1000");
		vbl4.set(coeffToken(8, 0), "00001111");
		vbl4.set(coeffToken(8, 1), "0001110");
		vbl4.set(coeffToken(8, 2), "0001101");
		vbl4.set(coeffToken(8, 3), "01101");
		vbl4.set(coeffToken(9, 0), "00001011");
		vbl4.set(coeffToken(9, 1), "00001110");
		vbl4.set(coeffToken(9, 2), "0001010");
		vbl4.set(coeffToken(9, 3), "001100");
		vbl4.set(coeffToken(10, 0), "000001111");
		vbl4.set(coeffToken(10, 1), "00001010");
		vbl4.set(coeffToken(10, 2), "00001101");
		vbl4.set(coeffToken(10, 3), "0001100");
		vbl4.set(coeffToken(11, 0), "000001011");
		vbl4.set(coeffToken(11, 1), "000001110");
		vbl4.set(coeffToken(11, 2), "00001001");
		vbl4.set(coeffToken(11, 3), "00001100");
		vbl4.set(coeffToken(12, 0), "000001000");
		vbl4.set(coeffToken(12, 1), "000001010");
		vbl4.set(coeffToken(12, 2), "000001101");
		vbl4.set(coeffToken(12, 3), "00001000");
		vbl4.set(coeffToken(13, 0), "0000001101");
		vbl4.set(coeffToken(13, 1), "000000111");
		vbl4.set(coeffToken(13, 2), "000001001");
		vbl4.set(coeffToken(13, 3), "000001100");
		vbl4.set(coeffToken(14, 0), "0000001001");
		vbl4.set(coeffToken(14, 1), "0000001100");
		vbl4.set(coeffToken(14, 2), "0000001011");
		vbl4.set(coeffToken(14, 3), "0000001010");
		vbl4.set(coeffToken(15, 0), "0000000101");
		vbl4.set(coeffToken(15, 1), "0000001000");
		vbl4.set(coeffToken(15, 2), "0000000111");
		vbl4.set(coeffToken(15, 3), "0000000110");
		vbl4.set(coeffToken(16, 0), "0000000001");
		vbl4.set(coeffToken(16, 1), "0000000100");
		vbl4.set(coeffToken(16, 2), "0000000011");
		vbl4.set(coeffToken(16, 3), "0000000010");
		VLC[] __003C_003ECoeffToken5 = ___003C_003ECoeffToken;
		VLC[] __003C_003ECoeffToken6 = ___003C_003ECoeffToken;
		VLC[] __003C_003ECoeffToken7 = ___003C_003ECoeffToken;
		VLC[] __003C_003ECoeffToken8 = ___003C_003ECoeffToken;
		vLC = vbl4.getVLC();
		num = 7;
		array = __003C_003ECoeffToken8;
		VLC vLC4 = vLC;
		array[num] = vLC;
		vLC = vLC4;
		num = 6;
		array = __003C_003ECoeffToken7;
		VLC vLC5 = vLC;
		array[num] = vLC;
		vLC = vLC5;
		num = 5;
		array = __003C_003ECoeffToken6;
		VLC vLC6 = vLC;
		array[num] = vLC;
		__003C_003ECoeffToken5[4] = vLC6;
		VLCBuilder vbl3 = new VLCBuilder();
		vbl3.set(coeffToken(0, 0), "000011");
		vbl3.set(coeffToken(1, 0), "000000");
		vbl3.set(coeffToken(1, 1), "000001");
		vbl3.set(coeffToken(2, 0), "000100");
		vbl3.set(coeffToken(2, 1), "000101");
		vbl3.set(coeffToken(2, 2), "000110");
		vbl3.set(coeffToken(3, 0), "001000");
		vbl3.set(coeffToken(3, 1), "001001");
		vbl3.set(coeffToken(3, 2), "001010");
		vbl3.set(coeffToken(3, 3), "001011");
		vbl3.set(coeffToken(4, 0), "001100");
		vbl3.set(coeffToken(4, 1), "001101");
		vbl3.set(coeffToken(4, 2), "001110");
		vbl3.set(coeffToken(4, 3), "001111");
		vbl3.set(coeffToken(5, 0), "010000");
		vbl3.set(coeffToken(5, 1), "010001");
		vbl3.set(coeffToken(5, 2), "010010");
		vbl3.set(coeffToken(5, 3), "010011");
		vbl3.set(coeffToken(6, 0), "010100");
		vbl3.set(coeffToken(6, 1), "010101");
		vbl3.set(coeffToken(6, 2), "010110");
		vbl3.set(coeffToken(6, 3), "010111");
		vbl3.set(coeffToken(7, 0), "011000");
		vbl3.set(coeffToken(7, 1), "011001");
		vbl3.set(coeffToken(7, 2), "011010");
		vbl3.set(coeffToken(7, 3), "011011");
		vbl3.set(coeffToken(8, 0), "011100");
		vbl3.set(coeffToken(8, 1), "011101");
		vbl3.set(coeffToken(8, 2), "011110");
		vbl3.set(coeffToken(8, 3), "011111");
		vbl3.set(coeffToken(9, 0), "100000");
		vbl3.set(coeffToken(9, 1), "100001");
		vbl3.set(coeffToken(9, 2), "100010");
		vbl3.set(coeffToken(9, 3), "100011");
		vbl3.set(coeffToken(10, 0), "100100");
		vbl3.set(coeffToken(10, 1), "100101");
		vbl3.set(coeffToken(10, 2), "100110");
		vbl3.set(coeffToken(10, 3), "100111");
		vbl3.set(coeffToken(11, 0), "101000");
		vbl3.set(coeffToken(11, 1), "101001");
		vbl3.set(coeffToken(11, 2), "101010");
		vbl3.set(coeffToken(11, 3), "101011");
		vbl3.set(coeffToken(12, 0), "101100");
		vbl3.set(coeffToken(12, 1), "101101");
		vbl3.set(coeffToken(12, 2), "101110");
		vbl3.set(coeffToken(12, 3), "101111");
		vbl3.set(coeffToken(13, 0), "110000");
		vbl3.set(coeffToken(13, 1), "110001");
		vbl3.set(coeffToken(13, 2), "110010");
		vbl3.set(coeffToken(13, 3), "110011");
		vbl3.set(coeffToken(14, 0), "110100");
		vbl3.set(coeffToken(14, 1), "110101");
		vbl3.set(coeffToken(14, 2), "110110");
		vbl3.set(coeffToken(14, 3), "110111");
		vbl3.set(coeffToken(15, 0), "111000");
		vbl3.set(coeffToken(15, 1), "111001");
		vbl3.set(coeffToken(15, 2), "111010");
		vbl3.set(coeffToken(15, 3), "111011");
		vbl3.set(coeffToken(16, 0), "111100");
		vbl3.set(coeffToken(16, 1), "111101");
		vbl3.set(coeffToken(16, 2), "111110");
		vbl3.set(coeffToken(16, 3), "111111");
		___003C_003ECoeffToken[8] = vbl3.getVLC();
		VLCBuilder vbl2 = new VLCBuilder();
		vbl2.set(coeffToken(0, 0), "01");
		vbl2.set(coeffToken(1, 0), "000111");
		vbl2.set(coeffToken(1, 1), "1");
		vbl2.set(coeffToken(2, 0), "000100");
		vbl2.set(coeffToken(2, 1), "000110");
		vbl2.set(coeffToken(2, 2), "001");
		vbl2.set(coeffToken(3, 0), "000011");
		vbl2.set(coeffToken(3, 1), "0000011");
		vbl2.set(coeffToken(3, 2), "0000010");
		vbl2.set(coeffToken(3, 3), "000101");
		vbl2.set(coeffToken(4, 0), "000010");
		vbl2.set(coeffToken(4, 1), "00000011");
		vbl2.set(coeffToken(4, 2), "00000010");
		vbl2.set(coeffToken(4, 3), "0000000");
		___003C_003EcoeffTokenChromaDCY420 = vbl2.getVLC();
		VLCBuilder vbl = new VLCBuilder();
		vbl.set(coeffToken(0, 0), "1");
		vbl.set(coeffToken(1, 0), "0001111");
		vbl.set(coeffToken(1, 1), "01");
		vbl.set(coeffToken(2, 0), "0001110");
		vbl.set(coeffToken(2, 1), "0001101");
		vbl.set(coeffToken(2, 2), "001");
		vbl.set(coeffToken(3, 0), "000000111");
		vbl.set(coeffToken(3, 1), "0001100");
		vbl.set(coeffToken(3, 2), "0001011");
		vbl.set(coeffToken(3, 3), "00001");
		vbl.set(coeffToken(4, 0), "000000110");
		vbl.set(coeffToken(4, 1), "000000101");
		vbl.set(coeffToken(4, 2), "0001010");
		vbl.set(coeffToken(4, 3), "000001");
		vbl.set(coeffToken(5, 0), "0000000111");
		vbl.set(coeffToken(5, 1), "0000000110");
		vbl.set(coeffToken(5, 2), "000000100");
		vbl.set(coeffToken(5, 3), "0001001");
		vbl.set(coeffToken(6, 0), "00000000111");
		vbl.set(coeffToken(6, 1), "00000000110");
		vbl.set(coeffToken(6, 2), "0000000101");
		vbl.set(coeffToken(6, 3), "0001000");
		vbl.set(coeffToken(7, 0), "000000000111");
		vbl.set(coeffToken(7, 1), "000000000110");
		vbl.set(coeffToken(7, 2), "00000000101");
		vbl.set(coeffToken(7, 3), "0000000100");
		vbl.set(coeffToken(8, 0), "0000000000111");
		vbl.set(coeffToken(8, 1), "000000000101");
		vbl.set(coeffToken(8, 2), "000000000100");
		vbl.set(coeffToken(8, 3), "00000000100");
		___003C_003EcoeffTokenChromaDCY422 = vbl.getVLC();
		___003C_003Erun = new VLC[7]
		{
			new VLCBuilder().set(0, "1").set(1, "0").getVLC(),
			new VLCBuilder().set(0, "1").set(1, "01").set(2, "00")
				.getVLC(),
			new VLCBuilder().set(0, "11").set(1, "10").set(2, "01")
				.set(3, "00")
				.getVLC(),
			new VLCBuilder().set(0, "11").set(1, "10").set(2, "01")
				.set(3, "001")
				.set(4, "000")
				.getVLC(),
			new VLCBuilder().set(0, "11").set(1, "10").set(2, "011")
				.set(3, "010")
				.set(4, "001")
				.set(5, "000")
				.getVLC(),
			new VLCBuilder().set(0, "11").set(1, "000").set(2, "001")
				.set(3, "011")
				.set(4, "010")
				.set(5, "101")
				.set(6, "100")
				.getVLC(),
			new VLCBuilder().set(0, "111").set(1, "110").set(2, "101")
				.set(3, "100")
				.set(4, "011")
				.set(5, "010")
				.set(6, "001")
				.set(7, "0001")
				.set(8, "00001")
				.set(9, "000001")
				.set(10, "0000001")
				.set(11, "00000001")
				.set(12, "000000001")
				.set(13, "0000000001")
				.set(14, "00000000001")
				.getVLC()
		};
		___003C_003EtotalZeros16 = new VLC[15]
		{
			new VLCBuilder().set(0, "1").set(1, "011").set(2, "010")
				.set(3, "0011")
				.set(4, "0010")
				.set(5, "00011")
				.set(6, "00010")
				.set(7, "000011")
				.set(8, "000010")
				.set(9, "0000011")
				.set(10, "0000010")
				.set(11, "00000011")
				.set(12, "00000010")
				.set(13, "000000011")
				.set(14, "000000010")
				.set(15, "000000001")
				.getVLC(),
			new VLCBuilder().set(0, "111").set(1, "110").set(2, "101")
				.set(3, "100")
				.set(4, "011")
				.set(5, "0101")
				.set(6, "0100")
				.set(7, "0011")
				.set(8, "0010")
				.set(9, "00011")
				.set(10, "00010")
				.set(11, "000011")
				.set(12, "000010")
				.set(13, "000001")
				.set(14, "000000")
				.getVLC(),
			new VLCBuilder().set(0, "0101").set(1, "111").set(2, "110")
				.set(3, "101")
				.set(4, "0100")
				.set(5, "0011")
				.set(6, "100")
				.set(7, "011")
				.set(8, "0010")
				.set(9, "00011")
				.set(10, "00010")
				.set(11, "000001")
				.set(12, "00001")
				.set(13, "000000")
				.getVLC(),
			new VLCBuilder().set(0, "00011").set(1, "111").set(2, "0101")
				.set(3, "0100")
				.set(4, "110")
				.set(5, "101")
				.set(6, "100")
				.set(7, "0011")
				.set(8, "011")
				.set(9, "0010")
				.set(10, "00010")
				.set(11, "00001")
				.set(12, "00000")
				.getVLC(),
			new VLCBuilder().set(0, "0101").set(1, "0100").set(2, "0011")
				.set(3, "111")
				.set(4, "110")
				.set(5, "101")
				.set(6, "100")
				.set(7, "011")
				.set(8, "0010")
				.set(9, "00001")
				.set(10, "0001")
				.set(11, "00000")
				.getVLC(),
			new VLCBuilder().set(0, "000001").set(1, "00001").set(2, "111")
				.set(3, "110")
				.set(4, "101")
				.set(5, "100")
				.set(6, "011")
				.set(7, "010")
				.set(8, "0001")
				.set(9, "001")
				.set(10, "000000")
				.getVLC(),
			new VLCBuilder().set(0, "000001").set(1, "00001").set(2, "101")
				.set(3, "100")
				.set(4, "011")
				.set(5, "11")
				.set(6, "010")
				.set(7, "0001")
				.set(8, "001")
				.set(9, "000000")
				.getVLC(),
			new VLCBuilder().set(0, "000001").set(1, "0001").set(2, "00001")
				.set(3, "011")
				.set(4, "11")
				.set(5, "10")
				.set(6, "010")
				.set(7, "001")
				.set(8, "000000")
				.getVLC(),
			new VLCBuilder().set(0, "000001").set(1, "000000").set(2, "0001")
				.set(3, "11")
				.set(4, "10")
				.set(5, "001")
				.set(6, "01")
				.set(7, "00001")
				.getVLC(),
			new VLCBuilder().set(0, "00001").set(1, "00000").set(2, "001")
				.set(3, "11")
				.set(4, "10")
				.set(5, "01")
				.set(6, "0001")
				.getVLC(),
			new VLCBuilder().set(0, "0000").set(1, "0001").set(2, "001")
				.set(3, "010")
				.set(4, "1")
				.set(5, "011")
				.getVLC(),
			new VLCBuilder().set(0, "0000").set(1, "0001").set(2, "01")
				.set(3, "1")
				.set(4, "001")
				.getVLC(),
			new VLCBuilder().set(0, "000").set(1, "001").set(2, "1")
				.set(3, "01")
				.getVLC(),
			new VLCBuilder().set(0, "00").set(1, "01").set(2, "1")
				.getVLC(),
			new VLCBuilder().set(0, "0").set(1, "1").getVLC()
		};
		___003C_003EtotalZeros4 = new VLC[3]
		{
			new VLCBuilder().set(0, "1").set(1, "01").set(2, "001")
				.set(3, "000")
				.getVLC(),
			new VLCBuilder().set(0, "1").set(1, "01").set(2, "00")
				.getVLC(),
			new VLCBuilder().set(0, "1").set(1, "0").getVLC()
		};
		___003C_003EtotalZeros8 = new VLC[7]
		{
			new VLCBuilder().set(0, "1").set(1, "010").set(2, "011")
				.set(3, "0010")
				.set(4, "0011")
				.set(5, "0001")
				.set(6, "00001")
				.set(7, "00000")
				.getVLC(),
			new VLCBuilder().set(0, "000").set(1, "01").set(2, "001")
				.set(3, "100")
				.set(4, "101")
				.set(5, "110")
				.set(6, "111")
				.getVLC(),
			new VLCBuilder().set(0, "000").set(1, "001").set(2, "01")
				.set(3, "10")
				.set(4, "110")
				.set(5, "111")
				.getVLC(),
			new VLCBuilder().set(0, "110").set(1, "00").set(2, "01")
				.set(3, "10")
				.set(4, "111")
				.getVLC(),
			new VLCBuilder().set(0, "00").set(1, "01").set(2, "10")
				.set(3, "11")
				.getVLC(),
			new VLCBuilder().set(0, "00").set(1, "01").set(2, "1")
				.getVLC(),
			new VLCBuilder().set(0, "0").set(1, "1").getVLC()
		};
		___003C_003EbPredModes = new PartPred[22][]
		{
			null,
			new PartPred[1] { PartPred.___003C_003EL0 },
			new PartPred[1] { PartPred.___003C_003EL1 },
			new PartPred[1] { PartPred.___003C_003EBi },
			new PartPred[2]
			{
				PartPred.___003C_003EL0,
				PartPred.___003C_003EL0
			},
			new PartPred[2]
			{
				PartPred.___003C_003EL0,
				PartPred.___003C_003EL0
			},
			new PartPred[2]
			{
				PartPred.___003C_003EL1,
				PartPred.___003C_003EL1
			},
			new PartPred[2]
			{
				PartPred.___003C_003EL1,
				PartPred.___003C_003EL1
			},
			new PartPred[2]
			{
				PartPred.___003C_003EL0,
				PartPred.___003C_003EL1
			},
			new PartPred[2]
			{
				PartPred.___003C_003EL0,
				PartPred.___003C_003EL1
			},
			new PartPred[2]
			{
				PartPred.___003C_003EL1,
				PartPred.___003C_003EL0
			},
			new PartPred[2]
			{
				PartPred.___003C_003EL1,
				PartPred.___003C_003EL0
			},
			new PartPred[2]
			{
				PartPred.___003C_003EL0,
				PartPred.___003C_003EBi
			},
			new PartPred[2]
			{
				PartPred.___003C_003EL0,
				PartPred.___003C_003EBi
			},
			new PartPred[2]
			{
				PartPred.___003C_003EL1,
				PartPred.___003C_003EBi
			},
			new PartPred[2]
			{
				PartPred.___003C_003EL1,
				PartPred.___003C_003EBi
			},
			new PartPred[2]
			{
				PartPred.___003C_003EBi,
				PartPred.___003C_003EL0
			},
			new PartPred[2]
			{
				PartPred.___003C_003EBi,
				PartPred.___003C_003EL0
			},
			new PartPred[2]
			{
				PartPred.___003C_003EBi,
				PartPred.___003C_003EL1
			},
			new PartPred[2]
			{
				PartPred.___003C_003EBi,
				PartPred.___003C_003EL1
			},
			new PartPred[2]
			{
				PartPred.___003C_003EBi,
				PartPred.___003C_003EBi
			},
			new PartPred[2]
			{
				PartPred.___003C_003EBi,
				PartPred.___003C_003EBi
			}
		};
		___003C_003EbMbTypes = new MBType[23]
		{
			MBType.___003C_003EB_Direct_16x16,
			MBType.___003C_003EB_L0_16x16,
			MBType.___003C_003EB_L1_16x16,
			MBType.___003C_003EB_Bi_16x16,
			MBType.___003C_003EB_L0_L0_16x8,
			MBType.___003C_003EB_L0_L0_8x16,
			MBType.___003C_003EB_L1_L1_16x8,
			MBType.___003C_003EB_L1_L1_8x16,
			MBType.___003C_003EB_L0_L1_16x8,
			MBType.___003C_003EB_L0_L1_8x16,
			MBType.___003C_003EB_L1_L0_16x8,
			MBType.___003C_003EB_L1_L0_8x16,
			MBType.___003C_003EB_L0_Bi_16x8,
			MBType.___003C_003EB_L0_Bi_8x16,
			MBType.___003C_003EB_L1_Bi_16x8,
			MBType.___003C_003EB_L1_Bi_8x16,
			MBType.___003C_003EB_Bi_L0_16x8,
			MBType.___003C_003EB_Bi_L0_8x16,
			MBType.___003C_003EB_Bi_L1_16x8,
			MBType.___003C_003EB_Bi_L1_8x16,
			MBType.___003C_003EB_Bi_Bi_16x8,
			MBType.___003C_003EB_Bi_Bi_8x16,
			MBType.___003C_003EB_8x8
		};
		___003C_003EbPartW = new int[22]
		{
			0, 16, 16, 16, 16, 8, 16, 8, 16, 8,
			16, 8, 16, 8, 16, 8, 16, 8, 16, 8,
			16, 8
		};
		___003C_003EbPartH = new int[22]
		{
			0, 16, 16, 16, 8, 16, 8, 16, 8, 16,
			8, 16, 8, 16, 8, 16, 8, 16, 8, 16,
			8, 16
		};
		___003C_003EBLK_X = new int[16]
		{
			0, 4, 0, 4, 8, 12, 8, 12, 0, 4,
			0, 4, 8, 12, 8, 12
		};
		___003C_003EBLK_Y = new int[16]
		{
			0, 0, 4, 4, 0, 0, 4, 4, 8, 8,
			12, 12, 8, 8, 12, 12
		};
		___003C_003EBLK_8x8_X = new int[4] { 0, 8, 0, 8 };
		___003C_003EBLK_8x8_Y = new int[4] { 0, 0, 8, 8 };
		___003C_003EBLK_INV_MAP = new int[16]
		{
			0, 1, 4, 5, 2, 3, 6, 7, 8, 9,
			12, 13, 10, 11, 14, 15
		};
		___003C_003EMB_BLK_OFF_LEFT = new int[16]
		{
			0, 1, 0, 1, 2, 3, 2, 3, 0, 1,
			0, 1, 2, 3, 2, 3
		};
		___003C_003EMB_BLK_OFF_TOP = new int[16]
		{
			0, 0, 1, 1, 0, 0, 1, 1, 2, 2,
			3, 3, 2, 2, 3, 3
		};
		___003C_003EQP_SCALE_CR = new int[52]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
			20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
			29, 30, 31, 32, 32, 33, 34, 34, 35, 35,
			36, 36, 37, 37, 37, 38, 38, 38, 39, 39,
			39, 39
		};
		___003C_003ENO_PIC = Picture.createPicture(0, 0, null, null);
		___003C_003EBLK_8x8_MB_OFF_LUMA = new int[4] { 0, 8, 128, 136 };
		___003C_003EBLK_8x8_MB_OFF_CHROMA = new int[4] { 0, 4, 32, 36 };
		___003C_003EBLK_4x4_MB_OFF_LUMA = new int[16]
		{
			0, 4, 8, 12, 64, 68, 72, 76, 128, 132,
			136, 140, 192, 196, 200, 204
		};
		___003C_003EBLK_8x8_IND = new int[16]
		{
			0, 0, 1, 1, 0, 0, 1, 1, 2, 2,
			3, 3, 2, 2, 3, 3
		};
		___003C_003EBLK8x8_BLOCKS = new int[4][]
		{
			new int[4] { 0, 1, 4, 5 },
			new int[4] { 2, 3, 6, 7 },
			new int[4] { 8, 9, 12, 13 },
			new int[4] { 10, 11, 14, 15 }
		};
		___003C_003EARRAY = new int[4][]
		{
			new int[1] { 0 },
			new int[1] { 1 },
			new int[1] { 2 },
			new int[1] { 3 }
		};
		___003C_003ECODED_BLOCK_PATTERN_INTRA_COLOR = new int[48]
		{
			47, 31, 15, 0, 23, 27, 29, 30, 7, 11,
			13, 14, 39, 43, 45, 46, 16, 3, 5, 10,
			12, 19, 21, 26, 28, 35, 37, 42, 44, 1,
			2, 4, 8, 17, 18, 20, 24, 6, 9, 22,
			25, 32, 33, 34, 36, 40, 38, 41
		};
		___003C_003Ecoded_block_pattern_intra_monochrome = new int[16]
		{
			15, 0, 7, 11, 13, 14, 3, 5, 10, 12,
			1, 2, 4, 8, 6, 9
		};
		___003C_003ECODED_BLOCK_PATTERN_INTER_COLOR = new int[48]
		{
			0, 16, 1, 2, 4, 8, 32, 3, 5, 10,
			12, 15, 47, 7, 11, 13, 14, 6, 9, 31,
			35, 37, 42, 44, 33, 34, 36, 40, 39, 43,
			45, 46, 17, 18, 20, 24, 19, 21, 26, 28,
			23, 27, 29, 30, 22, 25, 38, 41
		};
		___003C_003ECODED_BLOCK_PATTERN_INTER_COLOR_INV = inverse(___003C_003ECODED_BLOCK_PATTERN_INTER_COLOR);
		___003C_003Ecoded_block_pattern_inter_monochrome = new int[16]
		{
			0, 1, 2, 4, 8, 3, 5, 10, 12, 15,
			7, 11, 13, 14, 6, 9
		};
		___003C_003Esig_coeff_map_8x8 = new int[63]
		{
			0, 1, 2, 3, 4, 5, 5, 4, 4, 3,
			3, 4, 4, 4, 5, 5, 4, 4, 4, 4,
			3, 3, 6, 7, 7, 7, 8, 9, 10, 9,
			8, 7, 7, 6, 11, 12, 13, 11, 6, 7,
			8, 9, 14, 10, 9, 8, 6, 11, 12, 13,
			11, 6, 9, 14, 10, 9, 11, 12, 13, 11,
			14, 10, 12
		};
		___003C_003Esig_coeff_map_8x8_mbaff = new int[63]
		{
			0, 1, 1, 2, 2, 3, 3, 4, 5, 6,
			7, 7, 7, 8, 4, 5, 6, 9, 10, 10,
			8, 11, 12, 11, 9, 9, 10, 10, 8, 11,
			12, 11, 9, 9, 10, 10, 8, 11, 12, 11,
			9, 9, 10, 10, 8, 13, 13, 9, 9, 10,
			10, 8, 13, 13, 9, 9, 10, 10, 14, 14,
			14, 14, 14
		};
		___003C_003Elast_sig_coeff_map_8x8 = new int[63]
		{
			0, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 3, 3, 3, 3, 3, 3, 3, 3,
			4, 4, 4, 4, 4, 4, 4, 4, 5, 5,
			5, 5, 6, 6, 6, 6, 7, 7, 7, 7,
			8, 8, 8
		};
		___003C_003EidentityMapping16 = new int[16]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15
		};
		___003C_003EidentityMapping4 = new int[4] { 0, 1, 2, 3 };
		___003C_003EbPartPredModes = new PartPred[13]
		{
			PartPred.___003C_003EDirect,
			PartPred.___003C_003EL0,
			PartPred.___003C_003EL1,
			PartPred.___003C_003EBi,
			PartPred.___003C_003EL0,
			PartPred.___003C_003EL0,
			PartPred.___003C_003EL1,
			PartPred.___003C_003EL1,
			PartPred.___003C_003EBi,
			PartPred.___003C_003EBi,
			PartPred.___003C_003EL0,
			PartPred.___003C_003EL1,
			PartPred.___003C_003EBi
		};
		___003C_003EbSubMbTypes = new int[13]
		{
			0, 0, 0, 0, 1, 2, 1, 2, 1, 2,
			3, 3, 3
		};
		___003C_003ELUMA_4x4_BLOCK_LUT = new int[256];
		___003C_003ELUMA_4x4_POS_LUT = new int[256];
		___003C_003ELUMA_8x8_BLOCK_LUT = new int[256];
		___003C_003ELUMA_8x8_POS_LUT = new int[256];
		___003C_003ECHROMA_BLOCK_LUT = new int[64];
		___003C_003ECHROMA_POS_LUT = new int[64];
		___003C_003ECOMP_BLOCK_4x4_LUT = new int[3][] { ___003C_003ELUMA_4x4_BLOCK_LUT, ___003C_003ECHROMA_BLOCK_LUT, ___003C_003ECHROMA_BLOCK_LUT };
		___003C_003ECOMP_POS_4x4_LUT = new int[3][] { ___003C_003ELUMA_4x4_POS_LUT, ___003C_003ECHROMA_POS_LUT, ___003C_003ECHROMA_POS_LUT };
		___003C_003ECOMP_BLOCK_8x8_LUT = new int[3][] { ___003C_003ELUMA_8x8_BLOCK_LUT, ___003C_003ECHROMA_BLOCK_LUT, ___003C_003ECHROMA_BLOCK_LUT };
		___003C_003ECOMP_POS_8x8_LUT = new int[3][] { ___003C_003ELUMA_8x8_POS_LUT, ___003C_003ECHROMA_POS_LUT, ___003C_003ECHROMA_POS_LUT };
		int[] tmp = new int[16];
		for (int blk3 = 0; blk3 < 16; blk3++)
		{
			for (int k = 0; k < 16; k++)
			{
				tmp[k] = k;
			}
			putBlk(tmp, ___003C_003EBLK_X[blk3], ___003C_003EBLK_Y[blk3], 4, 4, 16, ___003C_003ELUMA_4x4_POS_LUT);
			Arrays.fill(tmp, blk3);
			putBlk(tmp, ___003C_003EBLK_X[blk3], ___003C_003EBLK_Y[blk3], 4, 4, 16, ___003C_003ELUMA_4x4_BLOCK_LUT);
		}
		for (int blk2 = 0; blk2 < 4; blk2++)
		{
			for (int j = 0; j < 16; j++)
			{
				tmp[j] = j;
			}
			putBlk(tmp, ___003C_003EBLK_X[blk2], ___003C_003EBLK_Y[blk2], 4, 4, 8, ___003C_003ECHROMA_POS_LUT);
			Arrays.fill(tmp, blk2);
			putBlk(tmp, ___003C_003EBLK_X[blk2], ___003C_003EBLK_Y[blk2], 4, 4, 8, ___003C_003ECHROMA_BLOCK_LUT);
		}
		tmp = new int[64];
		for (int blk = 0; blk < 4; blk++)
		{
			for (int i = 0; i < 64; i++)
			{
				tmp[i] = i;
			}
			putBlk(tmp, ___003C_003EBLK_8x8_X[blk], ___003C_003EBLK_8x8_Y[blk], 8, 8, 16, ___003C_003ELUMA_8x8_POS_LUT);
			Arrays.fill(tmp, blk);
			putBlk(tmp, ___003C_003EBLK_8x8_X[blk], ___003C_003EBLK_8x8_Y[blk], 8, 8, 16, ___003C_003ELUMA_8x8_BLOCK_LUT);
		}
		___003C_003EPIX_MAP_SPLIT_4x4 = buildPixSplitMap4x4();
		___003C_003EPIX_MAP_SPLIT_2x2 = buildPixSplitMap2x2();
		___003C_003EdefaultScalingList4x4Intra = new int[16]
		{
			6, 13, 13, 20, 20, 20, 28, 28, 28, 28,
			32, 32, 32, 37, 37, 42
		};
		___003C_003EdefaultScalingList4x4Inter = new int[16]
		{
			10, 14, 14, 20, 20, 20, 24, 24, 24, 24,
			27, 27, 27, 30, 30, 34
		};
		___003C_003EdefaultScalingList8x8Intra = new int[64]
		{
			6, 10, 10, 13, 11, 13, 16, 16, 16, 16,
			18, 18, 18, 18, 18, 23, 23, 23, 23, 23,
			23, 25, 25, 25, 25, 25, 25, 25, 27, 27,
			27, 27, 27, 27, 27, 27, 29, 29, 29, 29,
			29, 29, 29, 31, 31, 31, 31, 31, 31, 33,
			33, 33, 33, 33, 36, 36, 36, 36, 38, 38,
			38, 40, 40, 42
		};
		___003C_003EdefaultScalingList8x8Inter = new int[64]
		{
			9, 13, 13, 15, 13, 15, 17, 17, 17, 17,
			19, 19, 19, 19, 19, 21, 21, 21, 21, 21,
			21, 22, 22, 22, 22, 22, 22, 22, 24, 24,
			24, 24, 24, 24, 24, 24, 25, 25, 25, 25,
			25, 25, 25, 27, 27, 27, 27, 27, 27, 28,
			28, 28, 28, 28, 30, 30, 30, 30, 32, 32,
			32, 33, 33, 35
		};
	}
}
