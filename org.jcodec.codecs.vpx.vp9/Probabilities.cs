using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.vpx.vp9;

public class Probabilities : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[][] default_partition_probs;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] default_skip_prob;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[][][] default_tx_probs;

	internal static int[] ___003C_003Edefault_is_inter_probs;

	internal static int[][][] ___003C_003Ekf_y_mode_probs;

	internal static int[][] ___003C_003Ekf_uv_mode_probs;

	internal static int[][] ___003C_003Edefault_y_mode_probs;

	internal static int[][] ___003C_003Edefault_uv_mode_probs;

	internal static int[][] ___003C_003Edefault_single_ref_prob;

	internal static int[] ___003C_003Edefault_comp_ref_prob;

	internal static int[][] ___003C_003Edefault_interp_filter_probs;

	internal static int[][] ___003C_003Edefault_inter_mode_probs;

	internal static int[] ___003C_003Edefault_mv_joint_probs;

	internal static int[][] ___003C_003Edefault_mv_bits_prob;

	internal static int[] ___003C_003Edefault_mv_class0_bit_prob;

	internal static int[] ___003C_003Edefault_mv_class0_hp_prob;

	internal static int[][][][][][] ___003C_003Ecoef_probs;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] default_is_inter_probs
	{
		[HideFromJava]
		get
		{
			return ___003C_003Edefault_is_inter_probs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][][] kf_y_mode_probs
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ekf_y_mode_probs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] kf_uv_mode_probs
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ekf_uv_mode_probs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] default_y_mode_probs
	{
		[HideFromJava]
		get
		{
			return ___003C_003Edefault_y_mode_probs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] default_uv_mode_probs
	{
		[HideFromJava]
		get
		{
			return ___003C_003Edefault_uv_mode_probs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] default_single_ref_prob
	{
		[HideFromJava]
		get
		{
			return ___003C_003Edefault_single_ref_prob;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] default_comp_ref_prob
	{
		[HideFromJava]
		get
		{
			return ___003C_003Edefault_comp_ref_prob;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] default_interp_filter_probs
	{
		[HideFromJava]
		get
		{
			return ___003C_003Edefault_interp_filter_probs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] default_inter_mode_probs
	{
		[HideFromJava]
		get
		{
			return ___003C_003Edefault_inter_mode_probs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] default_mv_joint_probs
	{
		[HideFromJava]
		get
		{
			return ___003C_003Edefault_mv_joint_probs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] default_mv_bits_prob
	{
		[HideFromJava]
		get
		{
			return ___003C_003Edefault_mv_bits_prob;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] default_mv_class0_bit_prob
	{
		[HideFromJava]
		get
		{
			return ___003C_003Edefault_mv_class0_bit_prob;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] default_mv_class0_hp_prob
	{
		[HideFromJava]
		get
		{
			return ___003C_003Edefault_mv_class0_hp_prob;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][][][][][] coef_probs
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ecoef_probs;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public Probabilities()
	{
	}

	[LineNumberTable(544)]
	public virtual int[] getSegmentationPredProb()
	{
		return null;
	}

	[LineNumberTable(548)]
	public virtual int[] getSegmentationTreeProbs()
	{
		return null;
	}

	[LineNumberTable(552)]
	public virtual int[] getSkipProbs()
	{
		return null;
	}

	[LineNumberTable(556)]
	public virtual int[][][] getTxProbs()
	{
		return null;
	}

	[LineNumberTable(560)]
	public virtual int[] getIsInterProbs()
	{
		return null;
	}

	[LineNumberTable(564)]
	public virtual int[][][] getKfYModeProbs()
	{
		return null;
	}

	[LineNumberTable(568)]
	public virtual int[][] getKfUVModeProbs()
	{
		return null;
	}

	[LineNumberTable(572)]
	public virtual int[][] getYModeProbs()
	{
		return null;
	}

	[LineNumberTable(576)]
	public virtual int[][] getUVModeProbs()
	{
		return null;
	}

	[LineNumberTable(580)]
	public virtual int[] getCompModeProbs()
	{
		return null;
	}

	[LineNumberTable(584)]
	public virtual int[] getCompRefProbs()
	{
		return null;
	}

	[LineNumberTable(588)]
	public virtual int[][] getSingleRefProbs()
	{
		return null;
	}

	[LineNumberTable(592)]
	public virtual int[][] getInterpFilterProbs()
	{
		return null;
	}

	[LineNumberTable(596)]
	public virtual int[][] getInterModeProbs()
	{
		return null;
	}

	[LineNumberTable(600)]
	public virtual int[] getMvJointProbs()
	{
		return null;
	}

	[LineNumberTable(604)]
	public virtual int[][] getMvBitsProb()
	{
		return null;
	}

	[LineNumberTable(608)]
	public virtual int[] getMvClass0bitProbs()
	{
		return null;
	}

	[LineNumberTable(612)]
	public virtual int[][] getMvClassProbs()
	{
		return null;
	}

	[LineNumberTable(616)]
	public virtual int[][][] getMvClassFrProbs()
	{
		return null;
	}

	[LineNumberTable(620)]
	public virtual int[][] getMvFrProbs()
	{
		return null;
	}

	[LineNumberTable(624)]
	public virtual int[] getMvClass0HpProbs()
	{
		return null;
	}

	[LineNumberTable(628)]
	public virtual int[] getMvHpProbs()
	{
		return null;
	}

	[LineNumberTable(632)]
	public virtual int[][][][][][] getCoefProbs()
	{
		return null;
	}

	[LineNumberTable(637)]
	public virtual int[] getPartitionProbs(int ctx)
	{
		return null;
	}

	[LineNumberTable(new byte[]
	{
		159,
		139,
		66,
		byte.MaxValue,
		161,
		78,
		87,
		159,
		2,
		191,
		160,
		137,
		159,
		7,
		byte.MaxValue,
		184,
		102,
		160,
		114,
		byte.MaxValue,
		162,
		41,
		77,
		byte.MaxValue,
		160,
		182,
		72,
		byte.MaxValue,
		162,
		27,
		78,
		191,
		94,
		159,
		12,
		191,
		71,
		159,
		160,
		89,
		155,
		223,
		160,
		99,
		156,
		156
	})]
	static Probabilities()
	{
		default_partition_probs = new int[16][]
		{
			new int[3] { 199, 122, 141 },
			new int[3] { 147, 63, 159 },
			new int[3] { 148, 133, 118 },
			new int[3] { 121, 104, 114 },
			new int[3] { 174, 73, 87 },
			new int[3] { 92, 41, 83 },
			new int[3] { 82, 99, 50 },
			new int[3] { 53, 39, 39 },
			new int[3] { 177, 58, 59 },
			new int[3] { 68, 26, 63 },
			new int[3] { 52, 79, 25 },
			new int[3] { 17, 14, 12 },
			new int[3] { 222, 34, 30 },
			new int[3] { 72, 16, 44 },
			new int[3] { 58, 32, 12 },
			new int[3] { 10, 7, 6 }
		};
		default_skip_prob = new int[3] { 192, 128, 64 };
		default_tx_probs = new int[4][][]
		{
			new int[2][]
			{
				new int[3] { 0, 0, 0 },
				new int[3] { 0, 0, 0 }
			},
			new int[2][]
			{
				new int[3] { 100, 0, 0 },
				new int[3] { 66, 0, 0 }
			},
			new int[2][]
			{
				new int[3] { 20, 152, 0 },
				new int[3] { 15, 101, 0 }
			},
			new int[2][]
			{
				new int[3] { 3, 136, 37 },
				new int[3] { 5, 52, 13 }
			}
		};
		___003C_003Edefault_is_inter_probs = new int[4] { 9, 102, 187, 225 };
		___003C_003Ekf_y_mode_probs = new int[10][][]
		{
			new int[10][]
			{
				new int[9] { 137, 30, 42, 148, 151, 207, 70, 52, 91 },
				new int[9] { 92, 45, 102, 136, 116, 180, 74, 90, 100 },
				new int[9] { 73, 32, 19, 187, 222, 215, 46, 34, 100 },
				new int[9] { 91, 30, 32, 116, 121, 186, 93, 86, 94 },
				new int[9] { 72, 35, 36, 149, 68, 206, 68, 63, 105 },
				new int[9] { 73, 31, 28, 138, 57, 124, 55, 122, 151 },
				new int[9] { 67, 23, 21, 140, 126, 197, 40, 37, 171 },
				new int[9] { 86, 27, 28, 128, 154, 212, 45, 43, 53 },
				new int[9] { 74, 32, 27, 107, 86, 160, 63, 134, 102 },
				new int[9] { 59, 67, 44, 140, 161, 202, 78, 67, 119 }
			},
			new int[10][]
			{
				new int[9] { 63, 36, 126, 146, 123, 158, 60, 90, 96 },
				new int[9] { 43, 46, 168, 134, 107, 128, 69, 142, 92 },
				new int[9] { 44, 29, 68, 159, 201, 177, 50, 57, 77 },
				new int[9] { 58, 38, 76, 114, 97, 172, 78, 133, 92 },
				new int[9] { 46, 41, 76, 140, 63, 184, 69, 112, 57 },
				new int[9] { 38, 32, 85, 140, 46, 112, 54, 151, 133 },
				new int[9] { 39, 27, 61, 131, 110, 175, 44, 75, 136 },
				new int[9] { 52, 30, 74, 113, 130, 175, 51, 64, 58 },
				new int[9] { 47, 35, 80, 100, 74, 143, 64, 163, 74 },
				new int[9] { 36, 61, 116, 114, 128, 162, 80, 125, 82 }
			},
			new int[10][]
			{
				new int[9] { 82, 26, 26, 171, 208, 204, 44, 32, 105 },
				new int[9] { 55, 44, 68, 166, 179, 192, 57, 57, 108 },
				new int[9] { 42, 26, 11, 199, 241, 228, 23, 15, 85 },
				new int[9] { 68, 42, 19, 131, 160, 199, 55, 52, 83 },
				new int[9] { 58, 50, 25, 139, 115, 232, 39, 52, 118 },
				new int[9] { 50, 35, 33, 153, 104, 162, 64, 59, 131 },
				new int[9] { 44, 24, 16, 150, 177, 202, 33, 19, 156 },
				new int[9] { 55, 27, 12, 153, 203, 218, 26, 27, 49 },
				new int[9] { 53, 49, 21, 110, 116, 168, 59, 80, 76 },
				new int[9] { 38, 72, 19, 168, 203, 212, 50, 50, 107 }
			},
			new int[10][]
			{
				new int[9] { 103, 26, 36, 129, 132, 201, 83, 80, 93 },
				new int[9] { 59, 38, 83, 112, 103, 162, 98, 136, 90 },
				new int[9] { 62, 30, 23, 158, 200, 207, 59, 57, 50 },
				new int[9] { 67, 30, 29, 84, 86, 191, 102, 91, 59 },
				new int[9] { 60, 32, 33, 112, 71, 220, 64, 89, 104 },
				new int[9] { 53, 26, 34, 130, 56, 149, 84, 120, 103 },
				new int[9] { 53, 21, 23, 133, 109, 210, 56, 77, 172 },
				new int[9] { 77, 19, 29, 112, 142, 228, 55, 66, 36 },
				new int[9] { 61, 29, 29, 93, 97, 165, 83, 175, 162 },
				new int[9] { 47, 47, 43, 114, 137, 181, 100, 99, 95 }
			},
			new int[10][]
			{
				new int[9] { 69, 23, 29, 128, 83, 199, 46, 44, 101 },
				new int[9] { 53, 40, 55, 139, 69, 183, 61, 80, 110 },
				new int[9] { 40, 29, 19, 161, 180, 207, 43, 24, 91 },
				new int[9] { 60, 34, 19, 105, 61, 198, 53, 64, 89 },
				new int[9] { 52, 31, 22, 158, 40, 209, 58, 62, 89 },
				new int[9] { 44, 31, 29, 147, 46, 158, 56, 102, 198 },
				new int[9] { 35, 19, 12, 135, 87, 209, 41, 45, 167 },
				new int[9] { 55, 25, 21, 118, 95, 215, 38, 39, 66 },
				new int[9] { 51, 38, 25, 113, 58, 164, 70, 93, 97 },
				new int[9] { 47, 54, 34, 146, 108, 203, 72, 103, 151 }
			},
			new int[10][]
			{
				new int[9] { 64, 19, 37, 156, 66, 138, 49, 95, 133 },
				new int[9] { 46, 27, 80, 150, 55, 124, 55, 121, 135 },
				new int[9] { 36, 23, 27, 165, 149, 166, 54, 64, 118 },
				new int[9] { 53, 21, 36, 131, 63, 163, 60, 109, 81 },
				new int[9] { 40, 26, 35, 154, 40, 185, 51, 97, 123 },
				new int[9] { 35, 19, 34, 179, 19, 97, 48, 129, 124 },
				new int[9] { 36, 20, 26, 136, 62, 164, 33, 77, 154 },
				new int[9] { 45, 18, 32, 130, 90, 157, 40, 79, 91 },
				new int[9] { 45, 26, 28, 129, 45, 129, 49, 147, 123 },
				new int[9] { 38, 44, 51, 136, 74, 162, 57, 97, 121 }
			},
			new int[10][]
			{
				new int[9] { 75, 17, 22, 136, 138, 185, 32, 34, 166 },
				new int[9] { 56, 39, 58, 133, 117, 173, 48, 53, 187 },
				new int[9] { 35, 21, 12, 161, 212, 207, 20, 23, 145 },
				new int[9] { 56, 29, 19, 117, 109, 181, 55, 68, 112 },
				new int[9] { 47, 29, 17, 153, 64, 220, 59, 51, 114 },
				new int[9] { 46, 16, 24, 136, 76, 147, 41, 64, 172 },
				new int[9] { 34, 17, 11, 108, 152, 187, 13, 15, 209 },
				new int[9] { 51, 24, 14, 115, 133, 209, 32, 26, 104 },
				new int[9] { 55, 30, 18, 122, 79, 179, 44, 88, 116 },
				new int[9] { 37, 49, 25, 129, 168, 164, 41, 54, 148 }
			},
			new int[10][]
			{
				new int[9] { 82, 22, 32, 127, 143, 213, 39, 41, 70 },
				new int[9] { 62, 44, 61, 123, 105, 189, 48, 57, 64 },
				new int[9] { 47, 25, 17, 175, 222, 220, 24, 30, 86 },
				new int[9] { 68, 36, 17, 106, 102, 206, 59, 74, 74 },
				new int[9] { 57, 39, 23, 151, 68, 216, 55, 63, 58 },
				new int[9] { 49, 30, 35, 141, 70, 168, 82, 40, 115 },
				new int[9] { 51, 25, 15, 136, 129, 202, 38, 35, 139 },
				new int[9] { 68, 26, 16, 111, 141, 215, 29, 28, 28 },
				new int[9] { 59, 39, 19, 114, 75, 180, 77, 104, 42 },
				new int[9] { 40, 61, 26, 126, 152, 206, 61, 59, 93 }
			},
			new int[10][]
			{
				new int[9] { 78, 23, 39, 111, 117, 170, 74, 124, 94 },
				new int[9] { 48, 34, 86, 101, 92, 146, 78, 179, 134 },
				new int[9] { 47, 22, 24, 138, 187, 178, 68, 69, 59 },
				new int[9] { 56, 25, 33, 105, 112, 187, 95, 177, 129 },
				new int[9] { 48, 31, 27, 114, 63, 183, 82, 116, 56 },
				new int[9] { 43, 28, 37, 121, 63, 123, 61, 192, 169 },
				new int[9] { 42, 17, 24, 109, 97, 177, 56, 76, 122 },
				new int[9] { 58, 18, 28, 105, 139, 182, 70, 92, 63 },
				new int[9] { 46, 23, 32, 74, 86, 150, 67, 183, 88 },
				new int[9] { 36, 38, 48, 92, 122, 165, 88, 137, 91 }
			},
			new int[10][]
			{
				new int[9] { 65, 70, 60, 155, 159, 199, 61, 60, 81 },
				new int[9] { 44, 78, 115, 132, 119, 173, 71, 112, 93 },
				new int[9] { 39, 38, 21, 184, 227, 206, 42, 32, 64 },
				new int[9] { 58, 47, 36, 124, 137, 193, 80, 82, 78 },
				new int[9] { 49, 50, 35, 144, 95, 205, 63, 78, 59 },
				new int[9] { 41, 53, 52, 148, 71, 142, 65, 128, 51 },
				new int[9] { 40, 36, 28, 143, 143, 202, 40, 55, 137 },
				new int[9] { 52, 34, 29, 129, 183, 227, 42, 35, 43 },
				new int[9] { 42, 44, 44, 104, 105, 164, 64, 130, 80 },
				new int[9] { 43, 81, 53, 140, 169, 204, 68, 84, 72 }
			}
		};
		___003C_003Ekf_uv_mode_probs = new int[10][]
		{
			new int[9] { 144, 11, 54, 157, 195, 130, 46, 58, 108 },
			new int[9] { 118, 15, 123, 148, 131, 101, 44, 93, 131 },
			new int[9] { 113, 12, 23, 188, 226, 142, 26, 32, 125 },
			new int[9] { 120, 11, 50, 123, 163, 135, 64, 77, 103 },
			new int[9] { 113, 9, 36, 155, 111, 157, 32, 44, 161 },
			new int[9] { 116, 9, 55, 176, 76, 96, 37, 61, 149 },
			new int[9] { 115, 9, 28, 141, 161, 167, 21, 25, 193 },
			new int[9] { 120, 12, 32, 145, 195, 142, 32, 38, 86 },
			new int[9] { 116, 12, 64, 120, 140, 125, 49, 115, 121 },
			new int[9] { 102, 19, 66, 162, 182, 122, 35, 59, 128 }
		};
		___003C_003Edefault_y_mode_probs = new int[4][]
		{
			new int[9] { 65, 32, 18, 144, 162, 194, 41, 51, 98 },
			new int[9] { 132, 68, 18, 165, 217, 196, 45, 40, 78 },
			new int[9] { 173, 80, 19, 176, 240, 193, 64, 35, 46 },
			new int[9] { 221, 135, 38, 194, 248, 121, 96, 85, 29 }
		};
		___003C_003Edefault_uv_mode_probs = new int[10][]
		{
			new int[9] { 120, 7, 76, 176, 208, 126, 28, 54, 103 },
			new int[9] { 48, 12, 154, 155, 139, 90, 34, 117, 119 },
			new int[9] { 67, 6, 25, 204, 243, 158, 13, 21, 96 },
			new int[9] { 97, 5, 44, 131, 176, 139, 48, 68, 97 },
			new int[9] { 83, 5, 42, 156, 111, 152, 26, 49, 152 },
			new int[9] { 80, 5, 58, 178, 74, 83, 33, 62, 145 },
			new int[9] { 86, 5, 32, 154, 192, 168, 14, 22, 163 },
			new int[9] { 85, 5, 32, 156, 216, 148, 19, 29, 73 },
			new int[9] { 77, 7, 64, 116, 132, 122, 37, 126, 120 },
			new int[9] { 101, 21, 107, 181, 192, 103, 19, 67, 125 }
		};
		___003C_003Edefault_single_ref_prob = new int[5][]
		{
			new int[2] { 33, 16 },
			new int[2] { 77, 74 },
			new int[2] { 142, 142 },
			new int[2] { 172, 170 },
			new int[2] { 238, 247 }
		};
		___003C_003Edefault_comp_ref_prob = new int[5] { 50, 126, 123, 221, 226 };
		___003C_003Edefault_interp_filter_probs = new int[4][]
		{
			new int[2] { 235, 162 },
			new int[2] { 36, 255 },
			new int[2] { 34, 3 },
			new int[2] { 149, 144 }
		};
		___003C_003Edefault_inter_mode_probs = new int[7][]
		{
			new int[3] { 2, 173, 34 },
			new int[3] { 7, 145, 85 },
			new int[3] { 7, 166, 63 },
			new int[3] { 7, 94, 66 },
			new int[3] { 8, 64, 46 },
			new int[3] { 17, 81, 31 },
			new int[3] { 25, 29, 30 }
		};
		___003C_003Edefault_mv_joint_probs = new int[3] { 32, 64, 96 };
		___003C_003Edefault_mv_bits_prob = new int[2][]
		{
			new int[10] { 136, 140, 148, 160, 176, 192, 224, 234, 234, 240 },
			new int[10] { 136, 140, 148, 160, 176, 192, 224, 234, 234, 240 }
		};
		___003C_003Edefault_mv_class0_bit_prob = new int[2] { 216, 208 };
		___003C_003Edefault_mv_class0_hp_prob = new int[2] { 160, 160 };
		___003C_003Ecoef_probs = new int[4][][][][][]
		{
			new int[2][][][][]
			{
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 195, 29, 183 },
							new int[3] { 84, 49, 136 },
							new int[3] { 8, 42, 71 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 31, 107, 169 },
							new int[3] { 35, 99, 159 },
							new int[3] { 17, 82, 140 },
							new int[3] { 8, 66, 114 },
							new int[3] { 2, 44, 76 },
							new int[3] { 1, 19, 32 }
						},
						new int[6][]
						{
							new int[3] { 40, 132, 201 },
							new int[3] { 29, 114, 187 },
							new int[3] { 13, 91, 157 },
							new int[3] { 7, 75, 127 },
							new int[3] { 3, 58, 95 },
							new int[3] { 1, 28, 47 }
						},
						new int[6][]
						{
							new int[3] { 69, 142, 221 },
							new int[3] { 42, 122, 201 },
							new int[3] { 15, 91, 159 },
							new int[3] { 6, 67, 121 },
							new int[3] { 1, 42, 77 },
							new int[3] { 1, 17, 31 }
						},
						new int[6][]
						{
							new int[3] { 102, 148, 228 },
							new int[3] { 67, 117, 204 },
							new int[3] { 17, 82, 154 },
							new int[3] { 6, 59, 114 },
							new int[3] { 2, 39, 75 },
							new int[3] { 1, 15, 29 }
						},
						new int[6][]
						{
							new int[3] { 156, 57, 233 },
							new int[3] { 119, 57, 212 },
							new int[3] { 58, 48, 163 },
							new int[3] { 29, 40, 124 },
							new int[3] { 12, 30, 81 },
							new int[3] { 3, 12, 31 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 191, 107, 226 },
							new int[3] { 124, 117, 204 },
							new int[3] { 25, 99, 155 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 29, 148, 210 },
							new int[3] { 37, 126, 194 },
							new int[3] { 8, 93, 157 },
							new int[3] { 2, 68, 118 },
							new int[3] { 1, 39, 69 },
							new int[3] { 1, 17, 33 }
						},
						new int[6][]
						{
							new int[3] { 41, 151, 213 },
							new int[3] { 27, 123, 193 },
							new int[3] { 3, 82, 144 },
							new int[3] { 1, 58, 105 },
							new int[3] { 1, 32, 60 },
							new int[3] { 1, 13, 26 }
						},
						new int[6][]
						{
							new int[3] { 59, 159, 220 },
							new int[3] { 23, 126, 198 },
							new int[3] { 4, 88, 151 },
							new int[3] { 1, 66, 114 },
							new int[3] { 1, 38, 71 },
							new int[3] { 1, 18, 34 }
						},
						new int[6][]
						{
							new int[3] { 114, 136, 232 },
							new int[3] { 51, 114, 207 },
							new int[3] { 11, 83, 155 },
							new int[3] { 3, 56, 105 },
							new int[3] { 1, 33, 65 },
							new int[3] { 1, 17, 34 }
						},
						new int[6][]
						{
							new int[3] { 149, 65, 234 },
							new int[3] { 121, 57, 215 },
							new int[3] { 61, 49, 166 },
							new int[3] { 28, 36, 114 },
							new int[3] { 12, 25, 76 },
							new int[3] { 3, 16, 42 }
						}
					}
				},
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 214, 49, 220 },
							new int[3] { 132, 63, 188 },
							new int[3] { 42, 65, 137 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 85, 137, 221 },
							new int[3] { 104, 131, 216 },
							new int[3] { 49, 111, 192 },
							new int[3] { 21, 87, 155 },
							new int[3] { 2, 49, 87 },
							new int[3] { 1, 16, 28 }
						},
						new int[6][]
						{
							new int[3] { 89, 163, 230 },
							new int[3] { 90, 137, 220 },
							new int[3] { 29, 100, 183 },
							new int[3] { 10, 70, 135 },
							new int[3] { 2, 42, 81 },
							new int[3] { 1, 17, 33 }
						},
						new int[6][]
						{
							new int[3] { 108, 167, 237 },
							new int[3] { 55, 133, 222 },
							new int[3] { 15, 97, 179 },
							new int[3] { 4, 72, 135 },
							new int[3] { 1, 45, 85 },
							new int[3] { 1, 19, 38 }
						},
						new int[6][]
						{
							new int[3] { 124, 146, 240 },
							new int[3] { 66, 124, 224 },
							new int[3] { 17, 88, 175 },
							new int[3] { 4, 58, 122 },
							new int[3] { 1, 36, 75 },
							new int[3] { 1, 18, 37 }
						},
						new int[6][]
						{
							new int[3] { 141, 79, 241 },
							new int[3] { 126, 70, 227 },
							new int[3] { 66, 58, 182 },
							new int[3] { 30, 44, 136 },
							new int[3] { 12, 34, 96 },
							new int[3] { 2, 20, 47 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 229, 99, 249 },
							new int[3] { 143, 111, 235 },
							new int[3] { 46, 109, 192 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 82, 158, 236 },
							new int[3] { 94, 146, 224 },
							new int[3] { 25, 117, 191 },
							new int[3] { 9, 87, 149 },
							new int[3] { 3, 56, 99 },
							new int[3] { 1, 33, 57 }
						},
						new int[6][]
						{
							new int[3] { 83, 167, 237 },
							new int[3] { 68, 145, 222 },
							new int[3] { 10, 103, 177 },
							new int[3] { 2, 72, 131 },
							new int[3] { 1, 41, 79 },
							new int[3] { 1, 20, 39 }
						},
						new int[6][]
						{
							new int[3] { 99, 167, 239 },
							new int[3] { 47, 141, 224 },
							new int[3] { 10, 104, 178 },
							new int[3] { 2, 73, 133 },
							new int[3] { 1, 44, 85 },
							new int[3] { 1, 22, 47 }
						},
						new int[6][]
						{
							new int[3] { 127, 145, 243 },
							new int[3] { 71, 129, 228 },
							new int[3] { 17, 93, 177 },
							new int[3] { 3, 61, 124 },
							new int[3] { 1, 41, 84 },
							new int[3] { 1, 21, 52 }
						},
						new int[6][]
						{
							new int[3] { 157, 78, 244 },
							new int[3] { 140, 72, 231 },
							new int[3] { 69, 58, 184 },
							new int[3] { 31, 44, 137 },
							new int[3] { 14, 38, 105 },
							new int[3] { 8, 23, 61 }
						}
					}
				}
			},
			new int[2][][][][]
			{
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 125, 34, 187 },
							new int[3] { 52, 41, 133 },
							new int[3] { 6, 31, 56 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 37, 109, 153 },
							new int[3] { 51, 102, 147 },
							new int[3] { 23, 87, 128 },
							new int[3] { 8, 67, 101 },
							new int[3] { 1, 41, 63 },
							new int[3] { 1, 19, 29 }
						},
						new int[6][]
						{
							new int[3] { 31, 154, 185 },
							new int[3] { 17, 127, 175 },
							new int[3] { 6, 96, 145 },
							new int[3] { 2, 73, 114 },
							new int[3] { 1, 51, 82 },
							new int[3] { 1, 28, 45 }
						},
						new int[6][]
						{
							new int[3] { 23, 163, 200 },
							new int[3] { 10, 131, 185 },
							new int[3] { 2, 93, 148 },
							new int[3] { 1, 67, 111 },
							new int[3] { 1, 41, 69 },
							new int[3] { 1, 14, 24 }
						},
						new int[6][]
						{
							new int[3] { 29, 176, 217 },
							new int[3] { 12, 145, 201 },
							new int[3] { 3, 101, 156 },
							new int[3] { 1, 69, 111 },
							new int[3] { 1, 39, 63 },
							new int[3] { 1, 14, 23 }
						},
						new int[6][]
						{
							new int[3] { 57, 192, 233 },
							new int[3] { 25, 154, 215 },
							new int[3] { 6, 109, 167 },
							new int[3] { 3, 78, 118 },
							new int[3] { 1, 48, 69 },
							new int[3] { 1, 21, 29 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 202, 105, 245 },
							new int[3] { 108, 106, 216 },
							new int[3] { 18, 90, 144 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 33, 172, 219 },
							new int[3] { 64, 149, 206 },
							new int[3] { 14, 117, 177 },
							new int[3] { 5, 90, 141 },
							new int[3] { 2, 61, 95 },
							new int[3] { 1, 37, 57 }
						},
						new int[6][]
						{
							new int[3] { 33, 179, 220 },
							new int[3] { 11, 140, 198 },
							new int[3] { 1, 89, 148 },
							new int[3] { 1, 60, 104 },
							new int[3] { 1, 33, 57 },
							new int[3] { 1, 12, 21 }
						},
						new int[6][]
						{
							new int[3] { 30, 181, 221 },
							new int[3] { 8, 141, 198 },
							new int[3] { 1, 87, 145 },
							new int[3] { 1, 58, 100 },
							new int[3] { 1, 31, 55 },
							new int[3] { 1, 12, 20 }
						},
						new int[6][]
						{
							new int[3] { 32, 186, 224 },
							new int[3] { 7, 142, 198 },
							new int[3] { 1, 86, 143 },
							new int[3] { 1, 58, 100 },
							new int[3] { 1, 31, 55 },
							new int[3] { 1, 12, 22 }
						},
						new int[6][]
						{
							new int[3] { 57, 192, 227 },
							new int[3] { 20, 143, 204 },
							new int[3] { 3, 96, 154 },
							new int[3] { 1, 68, 112 },
							new int[3] { 1, 42, 69 },
							new int[3] { 1, 19, 32 }
						}
					}
				},
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 212, 35, 215 },
							new int[3] { 113, 47, 169 },
							new int[3] { 29, 48, 105 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 74, 129, 203 },
							new int[3] { 106, 120, 203 },
							new int[3] { 49, 107, 178 },
							new int[3] { 19, 84, 144 },
							new int[3] { 4, 50, 84 },
							new int[3] { 1, 15, 25 }
						},
						new int[6][]
						{
							new int[3] { 71, 172, 217 },
							new int[3] { 44, 141, 209 },
							new int[3] { 15, 102, 173 },
							new int[3] { 6, 76, 133 },
							new int[3] { 2, 51, 89 },
							new int[3] { 1, 24, 42 }
						},
						new int[6][]
						{
							new int[3] { 64, 185, 231 },
							new int[3] { 31, 148, 216 },
							new int[3] { 8, 103, 175 },
							new int[3] { 3, 74, 131 },
							new int[3] { 1, 46, 81 },
							new int[3] { 1, 18, 30 }
						},
						new int[6][]
						{
							new int[3] { 65, 196, 235 },
							new int[3] { 25, 157, 221 },
							new int[3] { 5, 105, 174 },
							new int[3] { 1, 67, 120 },
							new int[3] { 1, 38, 69 },
							new int[3] { 1, 15, 30 }
						},
						new int[6][]
						{
							new int[3] { 65, 204, 238 },
							new int[3] { 30, 156, 224 },
							new int[3] { 7, 107, 177 },
							new int[3] { 2, 70, 124 },
							new int[3] { 1, 42, 73 },
							new int[3] { 1, 18, 34 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 225, 86, 251 },
							new int[3] { 144, 104, 235 },
							new int[3] { 42, 99, 181 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 85, 175, 239 },
							new int[3] { 112, 165, 229 },
							new int[3] { 29, 136, 200 },
							new int[3] { 12, 103, 162 },
							new int[3] { 6, 77, 123 },
							new int[3] { 2, 53, 84 }
						},
						new int[6][]
						{
							new int[3] { 75, 183, 239 },
							new int[3] { 30, 155, 221 },
							new int[3] { 3, 106, 171 },
							new int[3] { 1, 74, 128 },
							new int[3] { 1, 44, 76 },
							new int[3] { 1, 17, 28 }
						},
						new int[6][]
						{
							new int[3] { 73, 185, 240 },
							new int[3] { 27, 159, 222 },
							new int[3] { 2, 107, 172 },
							new int[3] { 1, 75, 127 },
							new int[3] { 1, 42, 73 },
							new int[3] { 1, 17, 29 }
						},
						new int[6][]
						{
							new int[3] { 62, 190, 238 },
							new int[3] { 21, 159, 222 },
							new int[3] { 2, 107, 172 },
							new int[3] { 1, 72, 122 },
							new int[3] { 1, 40, 71 },
							new int[3] { 1, 18, 32 }
						},
						new int[6][]
						{
							new int[3] { 61, 199, 240 },
							new int[3] { 27, 161, 226 },
							new int[3] { 4, 113, 180 },
							new int[3] { 1, 76, 129 },
							new int[3] { 1, 46, 80 },
							new int[3] { 1, 23, 41 }
						}
					}
				}
			},
			new int[2][][][][]
			{
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 7, 27, 153 },
							new int[3] { 5, 30, 95 },
							new int[3] { 1, 16, 30 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 50, 75, 127 },
							new int[3] { 57, 75, 124 },
							new int[3] { 27, 67, 108 },
							new int[3] { 10, 54, 86 },
							new int[3] { 1, 33, 52 },
							new int[3] { 1, 12, 18 }
						},
						new int[6][]
						{
							new int[3] { 43, 125, 151 },
							new int[3] { 26, 108, 148 },
							new int[3] { 7, 83, 122 },
							new int[3] { 2, 59, 89 },
							new int[3] { 1, 38, 60 },
							new int[3] { 1, 17, 27 }
						},
						new int[6][]
						{
							new int[3] { 23, 144, 163 },
							new int[3] { 13, 112, 154 },
							new int[3] { 2, 75, 117 },
							new int[3] { 1, 50, 81 },
							new int[3] { 1, 31, 51 },
							new int[3] { 1, 14, 23 }
						},
						new int[6][]
						{
							new int[3] { 18, 162, 185 },
							new int[3] { 6, 123, 171 },
							new int[3] { 1, 78, 125 },
							new int[3] { 1, 51, 86 },
							new int[3] { 1, 31, 54 },
							new int[3] { 1, 14, 23 }
						},
						new int[6][]
						{
							new int[3] { 15, 199, 227 },
							new int[3] { 3, 150, 204 },
							new int[3] { 1, 91, 146 },
							new int[3] { 1, 55, 95 },
							new int[3] { 1, 30, 53 },
							new int[3] { 1, 11, 20 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 19, 55, 240 },
							new int[3] { 19, 59, 196 },
							new int[3] { 3, 52, 105 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 41, 166, 207 },
							new int[3] { 104, 153, 199 },
							new int[3] { 31, 123, 181 },
							new int[3] { 14, 101, 152 },
							new int[3] { 5, 72, 106 },
							new int[3] { 1, 36, 52 }
						},
						new int[6][]
						{
							new int[3] { 35, 176, 211 },
							new int[3] { 12, 131, 190 },
							new int[3] { 2, 88, 144 },
							new int[3] { 1, 60, 101 },
							new int[3] { 1, 36, 60 },
							new int[3] { 1, 16, 28 }
						},
						new int[6][]
						{
							new int[3] { 28, 183, 213 },
							new int[3] { 8, 134, 191 },
							new int[3] { 1, 86, 142 },
							new int[3] { 1, 56, 96 },
							new int[3] { 1, 30, 53 },
							new int[3] { 1, 12, 20 }
						},
						new int[6][]
						{
							new int[3] { 20, 190, 215 },
							new int[3] { 4, 135, 192 },
							new int[3] { 1, 84, 139 },
							new int[3] { 1, 53, 91 },
							new int[3] { 1, 28, 49 },
							new int[3] { 1, 11, 20 }
						},
						new int[6][]
						{
							new int[3] { 13, 196, 216 },
							new int[3] { 2, 137, 192 },
							new int[3] { 1, 86, 143 },
							new int[3] { 1, 57, 99 },
							new int[3] { 1, 32, 56 },
							new int[3] { 1, 13, 24 }
						}
					}
				},
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 211, 29, 217 },
							new int[3] { 96, 47, 156 },
							new int[3] { 22, 43, 87 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 78, 120, 193 },
							new int[3] { 111, 116, 186 },
							new int[3] { 46, 102, 164 },
							new int[3] { 15, 80, 128 },
							new int[3] { 2, 49, 76 },
							new int[3] { 1, 18, 28 }
						},
						new int[6][]
						{
							new int[3] { 71, 161, 203 },
							new int[3] { 42, 132, 192 },
							new int[3] { 10, 98, 150 },
							new int[3] { 3, 69, 109 },
							new int[3] { 1, 44, 70 },
							new int[3] { 1, 18, 29 }
						},
						new int[6][]
						{
							new int[3] { 57, 186, 211 },
							new int[3] { 30, 140, 196 },
							new int[3] { 4, 93, 146 },
							new int[3] { 1, 62, 102 },
							new int[3] { 1, 38, 65 },
							new int[3] { 1, 16, 27 }
						},
						new int[6][]
						{
							new int[3] { 47, 199, 217 },
							new int[3] { 14, 145, 196 },
							new int[3] { 1, 88, 142 },
							new int[3] { 1, 57, 98 },
							new int[3] { 1, 36, 62 },
							new int[3] { 1, 15, 26 }
						},
						new int[6][]
						{
							new int[3] { 26, 219, 229 },
							new int[3] { 5, 155, 207 },
							new int[3] { 1, 94, 151 },
							new int[3] { 1, 60, 104 },
							new int[3] { 1, 36, 62 },
							new int[3] { 1, 16, 28 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 233, 29, 248 },
							new int[3] { 146, 47, 220 },
							new int[3] { 43, 52, 140 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 100, 163, 232 },
							new int[3] { 179, 161, 222 },
							new int[3] { 63, 142, 204 },
							new int[3] { 37, 113, 174 },
							new int[3] { 26, 89, 137 },
							new int[3] { 18, 68, 97 }
						},
						new int[6][]
						{
							new int[3] { 85, 181, 230 },
							new int[3] { 32, 146, 209 },
							new int[3] { 7, 100, 164 },
							new int[3] { 3, 71, 121 },
							new int[3] { 1, 45, 77 },
							new int[3] { 1, 18, 30 }
						},
						new int[6][]
						{
							new int[3] { 65, 187, 230 },
							new int[3] { 20, 148, 207 },
							new int[3] { 2, 97, 159 },
							new int[3] { 1, 68, 116 },
							new int[3] { 1, 40, 70 },
							new int[3] { 1, 14, 29 }
						},
						new int[6][]
						{
							new int[3] { 40, 194, 227 },
							new int[3] { 8, 147, 204 },
							new int[3] { 1, 94, 155 },
							new int[3] { 1, 65, 112 },
							new int[3] { 1, 39, 66 },
							new int[3] { 1, 14, 26 }
						},
						new int[6][]
						{
							new int[3] { 16, 208, 228 },
							new int[3] { 3, 151, 207 },
							new int[3] { 1, 98, 160 },
							new int[3] { 1, 67, 117 },
							new int[3] { 1, 41, 74 },
							new int[3] { 1, 17, 31 }
						}
					}
				}
			},
			new int[2][][][][]
			{
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 17, 38, 140 },
							new int[3] { 7, 34, 80 },
							new int[3] { 1, 17, 29 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 37, 75, 128 },
							new int[3] { 41, 76, 128 },
							new int[3] { 26, 66, 116 },
							new int[3] { 12, 52, 94 },
							new int[3] { 2, 32, 55 },
							new int[3] { 1, 10, 16 }
						},
						new int[6][]
						{
							new int[3] { 50, 127, 154 },
							new int[3] { 37, 109, 152 },
							new int[3] { 16, 82, 121 },
							new int[3] { 5, 59, 85 },
							new int[3] { 1, 35, 54 },
							new int[3] { 1, 13, 20 }
						},
						new int[6][]
						{
							new int[3] { 40, 142, 167 },
							new int[3] { 17, 110, 157 },
							new int[3] { 2, 71, 112 },
							new int[3] { 1, 44, 72 },
							new int[3] { 1, 27, 45 },
							new int[3] { 1, 11, 17 }
						},
						new int[6][]
						{
							new int[3] { 30, 175, 188 },
							new int[3] { 9, 124, 169 },
							new int[3] { 1, 74, 116 },
							new int[3] { 1, 48, 78 },
							new int[3] { 1, 30, 49 },
							new int[3] { 1, 11, 18 }
						},
						new int[6][]
						{
							new int[3] { 10, 222, 223 },
							new int[3] { 2, 150, 194 },
							new int[3] { 1, 83, 128 },
							new int[3] { 1, 48, 79 },
							new int[3] { 1, 27, 45 },
							new int[3] { 1, 11, 17 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 36, 41, 235 },
							new int[3] { 29, 36, 193 },
							new int[3] { 10, 27, 111 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 85, 165, 222 },
							new int[3] { 177, 162, 215 },
							new int[3] { 110, 135, 195 },
							new int[3] { 57, 113, 168 },
							new int[3] { 23, 83, 120 },
							new int[3] { 10, 49, 61 }
						},
						new int[6][]
						{
							new int[3] { 85, 190, 223 },
							new int[3] { 36, 139, 200 },
							new int[3] { 5, 90, 146 },
							new int[3] { 1, 60, 103 },
							new int[3] { 1, 38, 65 },
							new int[3] { 1, 18, 30 }
						},
						new int[6][]
						{
							new int[3] { 72, 202, 223 },
							new int[3] { 23, 141, 199 },
							new int[3] { 2, 86, 140 },
							new int[3] { 1, 56, 97 },
							new int[3] { 1, 36, 61 },
							new int[3] { 1, 16, 27 }
						},
						new int[6][]
						{
							new int[3] { 55, 218, 225 },
							new int[3] { 13, 145, 200 },
							new int[3] { 1, 86, 141 },
							new int[3] { 1, 57, 99 },
							new int[3] { 1, 35, 61 },
							new int[3] { 1, 13, 22 }
						},
						new int[6][]
						{
							new int[3] { 15, 235, 212 },
							new int[3] { 1, 132, 184 },
							new int[3] { 1, 84, 139 },
							new int[3] { 1, 57, 97 },
							new int[3] { 1, 34, 56 },
							new int[3] { 1, 14, 23 }
						}
					}
				},
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 181, 21, 201 },
							new int[3] { 61, 37, 123 },
							new int[3] { 10, 38, 71 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 47, 106, 172 },
							new int[3] { 95, 104, 173 },
							new int[3] { 42, 93, 159 },
							new int[3] { 18, 77, 131 },
							new int[3] { 4, 50, 81 },
							new int[3] { 1, 17, 23 }
						},
						new int[6][]
						{
							new int[3] { 62, 147, 199 },
							new int[3] { 44, 130, 189 },
							new int[3] { 28, 102, 154 },
							new int[3] { 18, 75, 115 },
							new int[3] { 2, 44, 65 },
							new int[3] { 1, 12, 19 }
						},
						new int[6][]
						{
							new int[3] { 55, 153, 210 },
							new int[3] { 24, 130, 194 },
							new int[3] { 3, 93, 146 },
							new int[3] { 1, 61, 97 },
							new int[3] { 1, 31, 50 },
							new int[3] { 1, 10, 16 }
						},
						new int[6][]
						{
							new int[3] { 49, 186, 223 },
							new int[3] { 17, 148, 204 },
							new int[3] { 1, 96, 142 },
							new int[3] { 1, 53, 83 },
							new int[3] { 1, 26, 44 },
							new int[3] { 1, 11, 17 }
						},
						new int[6][]
						{
							new int[3] { 13, 217, 212 },
							new int[3] { 2, 136, 180 },
							new int[3] { 1, 78, 124 },
							new int[3] { 1, 50, 83 },
							new int[3] { 1, 29, 49 },
							new int[3] { 1, 14, 23 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 197, 13, 247 },
							new int[3] { 82, 17, 222 },
							new int[3] { 25, 17, 162 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 126, 186, 247 },
							new int[3] { 234, 191, 243 },
							new int[3] { 176, 177, 234 },
							new int[3] { 104, 158, 220 },
							new int[3] { 66, 128, 186 },
							new int[3] { 55, 90, 137 }
						},
						new int[6][]
						{
							new int[3] { 111, 197, 242 },
							new int[3] { 46, 158, 219 },
							new int[3] { 9, 104, 171 },
							new int[3] { 2, 65, 125 },
							new int[3] { 1, 44, 80 },
							new int[3] { 1, 17, 91 }
						},
						new int[6][]
						{
							new int[3] { 104, 208, 245 },
							new int[3] { 39, 168, 224 },
							new int[3] { 3, 109, 162 },
							new int[3] { 1, 79, 124 },
							new int[3] { 1, 50, 102 },
							new int[3] { 1, 43, 102 }
						},
						new int[6][]
						{
							new int[3] { 84, 220, 246 },
							new int[3] { 31, 177, 231 },
							new int[3] { 2, 115, 180 },
							new int[3] { 1, 79, 134 },
							new int[3] { 1, 55, 77 },
							new int[3] { 1, 60, 79 }
						},
						new int[6][]
						{
							new int[3] { 43, 243, 240 },
							new int[3] { 8, 180, 217 },
							new int[3] { 1, 115, 166 },
							new int[3] { 1, 84, 121 },
							new int[3] { 1, 51, 67 },
							new int[3] { 1, 16, 6 }
						}
					}
				}
			}
		};
	}
}
