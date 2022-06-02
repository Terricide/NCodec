using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.ps;

[Implements(new string[] { "net.sourceforge.jaad.aac.ps.PSConstants", "net.sourceforge.jaad.aac.ps.PSTables", "net.sourceforge.jaad.aac.ps.PSHuffmanTables" })]
public class PS : Object, PSConstants, PSTables, PSHuffmanTables
{
	internal bool enable_iid;

	internal bool enable_icc;

	internal bool enable_ext;

	internal int iid_mode;

	internal int icc_mode;

	internal int nr_iid_par;

	internal int nr_ipdopd_par;

	internal int nr_icc_par;

	internal int frame_class;

	internal int num_env;

	internal int[] border_position;

	internal bool[] iid_dt;

	internal bool[] icc_dt;

	internal bool enable_ipdopd;

	internal int ipd_mode;

	internal bool[] ipd_dt;

	internal bool[] opd_dt;

	internal int[] iid_index_prev;

	internal int[] icc_index_prev;

	internal int[] ipd_index_prev;

	internal int[] opd_index_prev;

	internal int[][] iid_index;

	internal int[][] icc_index;

	internal int[][] ipd_index;

	internal int[][] opd_index;

	internal int[] ipd_index_1;

	internal int[] opd_index_1;

	internal int[] ipd_index_2;

	internal int[] opd_index_2;

	internal int ps_data_available;

	public bool header_read;

	internal PSFilterbank hyb;

	internal bool use34hybrid_bands;

	internal int numTimeSlotsRate;

	internal int num_groups;

	internal int num_hybrid_groups;

	internal int nr_par_bands;

	internal int nr_allpass_bands;

	internal int decay_cutoff;

	internal int[] group_border;

	internal int[] map_group2bk;

	internal int saved_delay;

	internal int[] delay_buf_index_ser;

	internal int[] num_sample_delay_ser;

	internal int[] delay_D;

	internal int[] delay_buf_index_delay;

	internal float[][][] delay_Qmf;

	internal float[][][] delay_SubQmf;

	internal float[][][][] delay_Qmf_ser;

	internal float[][][][] delay_SubQmf_ser;

	internal float alpha_decay;

	internal float alpha_smooth;

	internal float[] P_PeakDecayNrg;

	internal float[] P_prev;

	internal float[] P_SmoothPeakDecayDiffNrg_prev;

	internal float[][] h11_prev;

	internal float[][] h12_prev;

	internal float[][] h21_prev;

	internal float[][] h22_prev;

	internal int phase_hist;

	internal float[][][] ipd_prev;

	internal float[][][] opd_prev;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int MAX_PS_ENVELOPES = 5;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int NO_ALLPASS_LINKS = 3;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int NEGATE_IPD_MASK = 4096;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const float DECAY_SLOPE = 0.05f;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const float COEF_SQRT2 = 1.41421354f;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] nr_iid_par_tab
	{
		[HideFromJava]
		get
		{
			return PSTables.nr_iid_par_tab;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] nr_icc_par_tab
	{
		[HideFromJava]
		get
		{
			return PSTables.nr_icc_par_tab;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] nr_ipdopd_par_tab
	{
		[HideFromJava]
		get
		{
			return PSTables.nr_ipdopd_par_tab;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] num_env_tab
	{
		[HideFromJava]
		get
		{
			return PSTables.num_env_tab;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] filter_a
	{
		[HideFromJava]
		get
		{
			return PSTables.filter_a;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] group_border20
	{
		[HideFromJava]
		get
		{
			return PSTables.group_border20;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] group_border34
	{
		[HideFromJava]
		get
		{
			return PSTables.group_border34;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] map_group2bk20
	{
		[HideFromJava]
		get
		{
			return PSTables.map_group2bk20;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] map_group2bk34
	{
		[HideFromJava]
		get
		{
			return PSTables.map_group2bk34;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] delay_length_d
	{
		[HideFromJava]
		get
		{
			return PSTables.delay_length_d;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] p8_13_20
	{
		[HideFromJava]
		get
		{
			return PSTables.p8_13_20;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] p2_13_20
	{
		[HideFromJava]
		get
		{
			return PSTables.p2_13_20;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] p12_13_34
	{
		[HideFromJava]
		get
		{
			return PSTables.p12_13_34;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] p8_13_34
	{
		[HideFromJava]
		get
		{
			return PSTables.p8_13_34;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] p4_13_34
	{
		[HideFromJava]
		get
		{
			return PSTables.p4_13_34;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] Phi_Fract_Qmf
	{
		[HideFromJava]
		get
		{
			return PSTables.Phi_Fract_Qmf;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] Phi_Fract_SubQmf20
	{
		[HideFromJava]
		get
		{
			return PSTables.Phi_Fract_SubQmf20;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] Phi_Fract_SubQmf34
	{
		[HideFromJava]
		get
		{
			return PSTables.Phi_Fract_SubQmf34;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][][] Q_Fract_allpass_Qmf
	{
		[HideFromJava]
		get
		{
			return PSTables.Q_Fract_allpass_Qmf;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][][] Q_Fract_allpass_SubQmf20
	{
		[HideFromJava]
		get
		{
			return PSTables.Q_Fract_allpass_SubQmf20;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][][] Q_Fract_allpass_SubQmf34
	{
		[HideFromJava]
		get
		{
			return PSTables.Q_Fract_allpass_SubQmf34;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] cos_alphas
	{
		[HideFromJava]
		get
		{
			return PSTables.cos_alphas;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] sin_alphas
	{
		[HideFromJava]
		get
		{
			return PSTables.sin_alphas;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] cos_betas_normal
	{
		[HideFromJava]
		get
		{
			return PSTables.cos_betas_normal;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] sin_betas_normal
	{
		[HideFromJava]
		get
		{
			return PSTables.sin_betas_normal;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] cos_betas_fine
	{
		[HideFromJava]
		get
		{
			return PSTables.cos_betas_fine;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] sin_betas_fine
	{
		[HideFromJava]
		get
		{
			return PSTables.sin_betas_fine;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] sincos_alphas_B_normal
	{
		[HideFromJava]
		get
		{
			return PSTables.sincos_alphas_B_normal;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] sincos_alphas_B_fine
	{
		[HideFromJava]
		get
		{
			return PSTables.sincos_alphas_B_fine;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] cos_gammas_normal
	{
		[HideFromJava]
		get
		{
			return PSTables.cos_gammas_normal;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] cos_gammas_fine
	{
		[HideFromJava]
		get
		{
			return PSTables.cos_gammas_fine;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] sin_gammas_normal
	{
		[HideFromJava]
		get
		{
			return PSTables.sin_gammas_normal;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] sin_gammas_fine
	{
		[HideFromJava]
		get
		{
			return PSTables.sin_gammas_fine;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] sf_iid_normal
	{
		[HideFromJava]
		get
		{
			return PSTables.sf_iid_normal;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] sf_iid_fine
	{
		[HideFromJava]
		get
		{
			return PSTables.sf_iid_fine;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] ipdopd_cos_tab
	{
		[HideFromJava]
		get
		{
			return PSTables.ipdopd_cos_tab;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] ipdopd_sin_tab
	{
		[HideFromJava]
		get
		{
			return PSTables.ipdopd_sin_tab;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] f_huff_iid_def
	{
		[HideFromJava]
		get
		{
			return PSHuffmanTables.f_huff_iid_def;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] t_huff_iid_def
	{
		[HideFromJava]
		get
		{
			return PSHuffmanTables.t_huff_iid_def;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] f_huff_iid_fine
	{
		[HideFromJava]
		get
		{
			return PSHuffmanTables.f_huff_iid_fine;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] t_huff_iid_fine
	{
		[HideFromJava]
		get
		{
			return PSHuffmanTables.t_huff_iid_fine;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] f_huff_icc
	{
		[HideFromJava]
		get
		{
			return PSHuffmanTables.f_huff_icc;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] t_huff_icc
	{
		[HideFromJava]
		get
		{
			return PSHuffmanTables.t_huff_icc;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] f_huff_ipd
	{
		[HideFromJava]
		get
		{
			return PSHuffmanTables.f_huff_ipd;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] t_huff_ipd
	{
		[HideFromJava]
		get
		{
			return PSHuffmanTables.t_huff_ipd;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] f_huff_opd
	{
		[HideFromJava]
		get
		{
			return PSHuffmanTables.f_huff_opd;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] t_huff_opd
	{
		[HideFromJava]
		get
		{
			return PSHuffmanTables.t_huff_opd;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 59, 65, 67, 132, 103, 46, 231, 70, 142,
		103, 46, 199
	})]
	private void huff_data(IBitStream ld, bool dt, int nr_par, int[][] t_huff, int[][] f_huff, int[] par)
	{
		if (dt)
		{
			for (int j = 0; j < nr_par; j++)
			{
				par[j] = ps_huff_dec(ld, t_huff);
			}
			return;
		}
		par[0] = ps_huff_dec(ld, f_huff);
		for (int i = 1; i < nr_par; i++)
		{
			par[i] = ps_huff_dec(ld, f_huff);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159,
		68,
		130,
		137,
		103,
		141,
		108,
		111,
		175,
		191,
		9,
		175,
		byte.MaxValue,
		9,
		54,
		234,
		78,
		200,
		140
	})]
	private int ps_extension(IBitStream ld, int ps_extension_id, int num_bits_left)
	{
		long bits = ld.getPosition();
		if (ps_extension_id == 0)
		{
			enable_ipdopd = ld.readBool();
			if (enable_ipdopd)
			{
				for (int i = 0; i < num_env; i++)
				{
					ipd_dt[i] = ld.readBool();
					huff_data(ld, ipd_dt[i], nr_ipdopd_par, PSHuffmanTables.t_huff_ipd, PSHuffmanTables.f_huff_ipd, ipd_index[i]);
					opd_dt[i] = ld.readBool();
					huff_data(ld, opd_dt[i], nr_ipdopd_par, PSHuffmanTables.t_huff_opd, PSHuffmanTables.f_huff_opd, opd_index[i]);
				}
			}
			ld.readBit();
		}
		return (int)(ld.getPosition() - bits);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 55, 162, 131, 101, 104, 169 })]
	private int ps_huff_dec(IBitStream ld, int[][] t_huff)
	{
		int index = 0;
		while (index >= 0)
		{
			int bit = ld.readBit();
			index = t_huff[index][bit];
		}
		return index + 31;
	}

	[LineNumberTable(new byte[] { 159, 52, 162, 103, 103 })]
	private int delta_clip(int i, int min, int max)
	{
		if (i < min)
		{
			return min;
		}
		if (i > max)
		{
			return max;
		}
		return i;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 49, 161, 70, 103, 132, 105, 145, 107, 109,
		17, 231, 71, 232, 70, 142, 241, 56, 231, 86,
		106, 38, 233, 70, 102, 110, 43, 201
	})]
	private void delta_decode(bool enable, int[] index, int[] index_prev, bool dt_flag, int nr_par, int stride, int min_index, int max_index)
	{
		if (enable)
		{
			if (!dt_flag)
			{
				index[0] = 0 + index[0];
				index[0] = delta_clip(index[0], min_index, max_index);
				for (int k = 1; k < nr_par; k++)
				{
					index[k] = index[k - 1] + index[k];
					index[k] = delta_clip(index[k], min_index, max_index);
				}
			}
			else
			{
				for (int j = 0; j < nr_par; j++)
				{
					index[j] = index_prev[j * stride] + index[j];
					index[j] = delta_clip(index[j], min_index, max_index);
				}
			}
		}
		else
		{
			for (int i = 0; i < nr_par; i++)
			{
				index[i] = 0;
			}
		}
		if (stride == 2)
		{
			for (int i = (nr_par << 1) - 1; i > 0; i += -1)
			{
				index[i] = index[i >> 1];
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 35, 129, 70, 103, 135, 105, 142, 109, 112,
		15, 233, 71, 106, 113, 15, 233, 72, 106, 38,
		233, 70, 102, 101, 110, 43, 201
	})]
	private void delta_modulo_decode(bool enable, int[] index, int[] index_prev, bool dt_flag, int nr_par, int stride, int and_modulo)
	{
		if (enable)
		{
			if (!dt_flag)
			{
				index[0] = 0 + index[0];
				int num = 0;
				int[] array = index;
				array[num] &= and_modulo;
				for (int k = 1; k < nr_par; k++)
				{
					index[k] = index[k - 1] + index[k];
					num = k;
					array = index;
					array[num] &= and_modulo;
				}
			}
			else
			{
				for (int j = 0; j < nr_par; j++)
				{
					index[j] = index_prev[j * stride] + index[j];
					int num = j;
					int[] array = index;
					array[num] &= and_modulo;
				}
			}
		}
		else
		{
			for (int i = 0; i < nr_par; i++)
			{
				index[i] = 0;
			}
		}
		if (stride == 2)
		{
			index[0] = 0;
			for (int i = (nr_par << 1) - 1; i > 0; i += -1)
			{
				index[i] = index[i >> 1];
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 26, 162, 109, 103, 103, 109, 103, 103, 103,
		103, 104, 104, 104, 104, 104, 105, 105, 137, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 137
	})]
	private void map20indexto34(int[] index, int bins)
	{
		index[1] = (index[0] + index[1]) / 2;
		index[2] = index[1];
		index[3] = index[2];
		index[4] = (index[2] + index[3]) / 2;
		index[5] = index[3];
		index[6] = index[4];
		index[7] = index[4];
		index[8] = index[5];
		index[9] = index[5];
		index[10] = index[6];
		index[11] = index[7];
		index[12] = index[8];
		index[13] = index[8];
		index[14] = index[9];
		index[15] = index[9];
		index[16] = index[10];
		if (bins == 34)
		{
			index[17] = index[11];
			index[18] = index[12];
			index[19] = index[13];
			index[20] = index[14];
			index[21] = index[14];
			index[22] = index[15];
			index[23] = index[15];
			index[24] = index[16];
			index[25] = index[16];
			index[26] = index[17];
			index[27] = index[17];
			index[28] = index[18];
			index[29] = index[18];
			index[30] = index[18];
			index[31] = index[18];
			index[32] = index[19];
			index[33] = index[19];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(958)]
	private float magnitude_c(float[] c)
	{
		return (float)Math.sqrt(c[0] * c[0] + c[1] * c[1]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		15,
		130,
		105,
		168,
		239,
		70,
		144,
		132,
		104,
		104,
		105,
		203,
		108,
		108,
		109,
		237,
		69,
		byte.MaxValue,
		29,
		71,
		byte.MaxValue,
		28,
		70,
		223,
		8,
		byte.MaxValue,
		8,
		22,
		234,
		111,
		140,
		136,
		105,
		106,
		53,
		233,
		69,
		106,
		45,
		233,
		69,
		105,
		106,
		53,
		233,
		69,
		106,
		45,
		233,
		69,
		105,
		109,
		117,
		21,
		233,
		70,
		106,
		109,
		13,
		233,
		72,
		106,
		60,
		169,
		106,
		60,
		169,
		106,
		124,
		28,
		233,
		69,
		136,
		105,
		106,
		108,
		63,
		1,
		167,
		185,
		138,
		121,
		106,
		127,
		4,
		31,
		4,
		201,
		106,
		127,
		4,
		31,
		4,
		201,
		111,
		180,
		111,
		146,
		109,
		173,
		111,
		109,
		235,
		55,
		234,
		82,
		108,
		111,
		115,
		113,
		115,
		113,
		115,
		113,
		241,
		57,
		234,
		75
	})]
	private void ps_data_decode()
	{
		if (ps_data_available == 0)
		{
			num_env = 0;
		}
		for (int env = 0; env < num_env; env++)
		{
			int num_iid_steps = ((iid_mode >= 3) ? 15 : 7);
			int[] iid_index_prev;
			int[] icc_index_prev;
			int[] ipd_index_prev;
			int[] opd_index_prev;
			if (env == 0)
			{
				iid_index_prev = this.iid_index_prev;
				icc_index_prev = this.icc_index_prev;
				ipd_index_prev = this.ipd_index_prev;
				opd_index_prev = this.opd_index_prev;
			}
			else
			{
				iid_index_prev = iid_index[env - 1];
				icc_index_prev = icc_index[env - 1];
				ipd_index_prev = ipd_index[env - 1];
				opd_index_prev = opd_index[env - 1];
			}
			delta_decode(enable_iid, iid_index[env], iid_index_prev, iid_dt[env], nr_iid_par, (iid_mode != 0 && iid_mode != 3) ? 1 : 2, -num_iid_steps, num_iid_steps);
			delta_decode(enable_icc, icc_index[env], icc_index_prev, icc_dt[env], nr_icc_par, (icc_mode != 0 && icc_mode != 3) ? 1 : 2, 0, 7);
			delta_modulo_decode(enable_ipdopd, ipd_index[env], ipd_index_prev, ipd_dt[env], nr_ipdopd_par, 1, 7);
			delta_modulo_decode(enable_ipdopd, opd_index[env], opd_index_prev, opd_dt[env], nr_ipdopd_par, 1, 7);
		}
		if (num_env == 0)
		{
			num_env = 1;
			if (enable_iid)
			{
				for (int bin3 = 0; bin3 < 34; bin3++)
				{
					iid_index[0][bin3] = this.iid_index_prev[bin3];
				}
			}
			else
			{
				for (int bin2 = 0; bin2 < 34; bin2++)
				{
					iid_index[0][bin2] = 0;
				}
			}
			if (enable_icc)
			{
				for (int bin2 = 0; bin2 < 34; bin2++)
				{
					icc_index[0][bin2] = this.icc_index_prev[bin2];
				}
			}
			else
			{
				for (int bin2 = 0; bin2 < 34; bin2++)
				{
					icc_index[0][bin2] = 0;
				}
			}
			if (enable_ipdopd)
			{
				for (int bin2 = 0; bin2 < 17; bin2++)
				{
					ipd_index[0][bin2] = this.ipd_index_prev[bin2];
					opd_index[0][bin2] = this.opd_index_prev[bin2];
				}
			}
			else
			{
				for (int bin2 = 0; bin2 < 17; bin2++)
				{
					ipd_index[0][bin2] = 0;
					opd_index[0][bin2] = 0;
				}
			}
		}
		for (int bin = 0; bin < 34; bin++)
		{
			this.iid_index_prev[bin] = iid_index[num_env - 1][bin];
		}
		for (int bin = 0; bin < 34; bin++)
		{
			this.icc_index_prev[bin] = icc_index[num_env - 1][bin];
		}
		for (int bin = 0; bin < 17; bin++)
		{
			this.ipd_index_prev[bin] = ipd_index[num_env - 1][bin];
			this.opd_index_prev[bin] = opd_index[num_env - 1][bin];
		}
		ps_data_available = 0;
		if (frame_class == 0)
		{
			border_position[0] = 0;
			for (int env = 1; env < num_env; env++)
			{
				int[] array = border_position;
				int num = env;
				int num2 = env * numTimeSlotsRate;
				int num3 = num_env;
				array[num] = ((num3 != -1) ? (num2 / num3) : (-num2));
			}
			border_position[num_env] = numTimeSlotsRate;
		}
		else
		{
			border_position[0] = 0;
			if (border_position[num_env] < numTimeSlotsRate)
			{
				for (int bin = 0; bin < 34; bin++)
				{
					iid_index[num_env][bin] = iid_index[num_env - 1][bin];
					icc_index[num_env][bin] = icc_index[num_env - 1][bin];
				}
				for (int bin = 0; bin < 17; bin++)
				{
					ipd_index[num_env][bin] = ipd_index[num_env - 1][bin];
					opd_index[num_env][bin] = opd_index[num_env - 1][bin];
				}
				num_env++;
				border_position[num_env] = numTimeSlotsRate;
			}
			for (int env = 1; env < num_env; env++)
			{
				int thr = numTimeSlotsRate - (num_env - env);
				if (border_position[env] > thr)
				{
					border_position[env] = thr;
					continue;
				}
				thr = border_position[env - 1] + 1;
				if (border_position[env] < thr)
				{
					border_position[env] = thr;
				}
			}
		}
		if (!use34hybrid_bands)
		{
			return;
		}
		for (int env = 0; env < num_env; env++)
		{
			if (iid_mode != 2 && iid_mode != 5)
			{
				map20indexto34(iid_index[env], 34);
			}
			if (icc_mode != 2 && icc_mode != 5)
			{
				map20indexto34(icc_index[env], 34);
			}
			if (ipd_mode != 2 && ipd_mode != 5)
			{
				map20indexto34(ipd_index[env], 17);
				map20indexto34(opd_index[env], 17);
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		158,
		230,
		130,
		131,
		136,
		127,
		9,
		127,
		9,
		233,
		69,
		105,
		170,
		200,
		106,
		106,
		46,
		41,
		233,
		71,
		145,
		178,
		159,
		6,
		117,
		191,
		0,
		107,
		110,
		176,
		110,
		206,
		byte.MaxValue,
		12,
		51,
		44,
		236,
		57,
		236,
		91,
		113,
		127,
		0,
		136,
		123,
		116,
		178,
		108,
		127,
		14,
		172,
		108,
		127,
		3,
		172,
		107,
		176,
		243,
		42,
		44,
		236,
		93,
		113,
		107,
		144,
		174,
		149,
		169,
		117,
		170,
		108,
		103,
		202,
		244,
		69,
		105,
		50,
		233,
		70,
		104,
		105,
		46,
		201,
		127,
		0,
		153,
		139,
		110,
		208,
		110,
		174,
		219,
		122,
		122,
		105,
		105,
		122,
		byte.MaxValue,
		0,
		69,
		169,
		142,
		114,
		146,
		114,
		146,
		108,
		209,
		114,
		146,
		114,
		146,
		111,
		207,
		123,
		155,
		105,
		105,
		108,
		177,
		142,
		120,
		152,
		105,
		114,
		183,
		114,
		244,
		69,
		120,
		152,
		114,
		242,
		69,
		123,
		187,
		125,
		189,
		118,
		182,
		107,
		120,
		186,
		120,
		216,
		105,
		233,
		12,
		236,
		121,
		178,
		115,
		147,
		139,
		111,
		209,
		110,
		206,
		105,
		195,
		149,
		127,
		8,
		203,
		105,
		127,
		3,
		6,
		233,
		159,
		109,
		236,
		33,
		236,
		57,
		236,
		160,
		195,
		104,
		105,
		46,
		169
	})]
	private void ps_decorrelate(float[][][] X_left, float[][][] X_right, float[][][] X_hybrid_left, float[][][] X_hybrid_right)
	{
		int temp_delay = 0;
		int[] temp_delay_ser = new int[3];
		int[] array = new int[2];
		int num = (array[1] = 34);
		num = (array[0] = 32);
		float[][] P = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 34);
		num = (array[0] = 32);
		float[][] G_TransientRatio = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		float[] inputLeft = new float[2];
		float[][] Phi_Fract_SubQmf = ((!use34hybrid_bands) ? PSTables.Phi_Fract_SubQmf20 : PSTables.Phi_Fract_SubQmf34);
		for (int k = 0; k < 32; k++)
		{
			for (int bk3 = 0; bk3 < 34; bk3++)
			{
				P[k][bk3] = 0f;
			}
		}
		for (int gr = 0; gr < num_groups; gr++)
		{
			int bk2 = -4097 & map_group2bk[gr];
			int maxsb2 = ((gr >= num_hybrid_groups) ? group_border[gr + 1] : (group_border[gr] + 1));
			for (int sb2 = group_border[gr]; sb2 < maxsb2; sb2++)
			{
				for (int k = border_position[0]; k < border_position[num_env]; k++)
				{
					if (gr < num_hybrid_groups)
					{
						inputLeft[0] = X_hybrid_left[k][sb2][0];
						inputLeft[1] = X_hybrid_left[k][sb2][1];
					}
					else
					{
						inputLeft[0] = X_left[k][sb2][0];
						inputLeft[1] = X_left[k][sb2][1];
					}
					float[] obj = P[k];
					num = bk2;
					float[] array2 = obj;
					array2[num] += inputLeft[0] * inputLeft[0] + inputLeft[1] * inputLeft[1];
				}
			}
		}
		for (int bk = 0; bk < nr_par_bands; bk++)
		{
			for (int k = border_position[0]; k < border_position[num_env]; k++)
			{
				float gamma = 1.5f;
				P_PeakDecayNrg[bk] *= alpha_decay;
				if (P_PeakDecayNrg[bk] < P[k][bk])
				{
					P_PeakDecayNrg[bk] = P[k][bk];
				}
				float P_SmoothPeakDecayDiffNrg = P_SmoothPeakDecayDiffNrg_prev[bk];
				P_SmoothPeakDecayDiffNrg += (P_PeakDecayNrg[bk] - P[k][bk] - P_SmoothPeakDecayDiffNrg_prev[bk]) * alpha_smooth;
				P_SmoothPeakDecayDiffNrg_prev[bk] = P_SmoothPeakDecayDiffNrg;
				float nrg = P_prev[bk];
				nrg += (P[k][bk] - P_prev[bk]) * alpha_smooth;
				P_prev[bk] = nrg;
				if (P_SmoothPeakDecayDiffNrg * gamma <= nrg)
				{
					G_TransientRatio[k][bk] = 1f;
				}
				else
				{
					G_TransientRatio[k][bk] = nrg / (P_SmoothPeakDecayDiffNrg * gamma);
				}
			}
		}
		for (int gr = 0; gr < num_groups; gr++)
		{
			int maxsb = ((gr >= num_hybrid_groups) ? group_border[gr + 1] : (group_border[gr] + 1));
			for (int sb = group_border[gr]; sb < maxsb; sb++)
			{
				float[] g_DecaySlope_filt = new float[3];
				float g_DecaySlope;
				if (gr < num_hybrid_groups || sb <= decay_cutoff)
				{
					g_DecaySlope = 1f;
				}
				else
				{
					int decay = decay_cutoff - sb;
					g_DecaySlope = ((decay > -20) ? (1f + 0.05f * (float)decay) : 0f);
				}
				for (int j = 0; j < 3; j++)
				{
					g_DecaySlope_filt[j] = g_DecaySlope * PSTables.filter_a[j];
				}
				temp_delay = saved_delay;
				for (int k = 0; k < 3; k++)
				{
					temp_delay_ser[k] = delay_buf_index_ser[k];
				}
				for (int k = border_position[0]; k < border_position[num_env]; k++)
				{
					float[] tmp = new float[2];
					float[] tmp2 = new float[2];
					float[] R0 = new float[2];
					if (gr < num_hybrid_groups)
					{
						inputLeft[0] = X_hybrid_left[k][sb][0];
						inputLeft[1] = X_hybrid_left[k][sb][1];
					}
					else
					{
						inputLeft[0] = X_left[k][sb][0];
						inputLeft[1] = X_left[k][sb][1];
					}
					if (sb > nr_allpass_bands && gr >= num_hybrid_groups)
					{
						tmp[0] = delay_Qmf[delay_buf_index_delay[sb]][sb][0];
						tmp[1] = delay_Qmf[delay_buf_index_delay[sb]][sb][1];
						R0[0] = tmp[0];
						R0[1] = tmp[1];
						delay_Qmf[delay_buf_index_delay[sb]][sb][0] = inputLeft[0];
						delay_Qmf[delay_buf_index_delay[sb]][sb][1] = inputLeft[1];
					}
					else
					{
						float[] Phi_Fract = new float[2];
						if (gr < num_hybrid_groups)
						{
							tmp2[0] = delay_SubQmf[temp_delay][sb][0];
							tmp2[1] = delay_SubQmf[temp_delay][sb][1];
							delay_SubQmf[temp_delay][sb][0] = inputLeft[0];
							delay_SubQmf[temp_delay][sb][1] = inputLeft[1];
							Phi_Fract[0] = Phi_Fract_SubQmf[sb][0];
							Phi_Fract[1] = Phi_Fract_SubQmf[sb][1];
						}
						else
						{
							tmp2[0] = delay_Qmf[temp_delay][sb][0];
							tmp2[1] = delay_Qmf[temp_delay][sb][1];
							delay_Qmf[temp_delay][sb][0] = inputLeft[0];
							delay_Qmf[temp_delay][sb][1] = inputLeft[1];
							Phi_Fract[0] = PSTables.Phi_Fract_Qmf[sb][0];
							Phi_Fract[1] = PSTables.Phi_Fract_Qmf[sb][1];
						}
						tmp[0] = tmp[0] * Phi_Fract[0] + tmp2[1] * Phi_Fract[1];
						tmp[1] = tmp2[1] * Phi_Fract[0] - tmp2[0] * Phi_Fract[1];
						R0[0] = tmp[0];
						R0[1] = tmp[1];
						for (int j = 0; j < 3; j++)
						{
							float[] Q_Fract_allpass = new float[2];
							float[] tmp3 = new float[2];
							if (gr < num_hybrid_groups)
							{
								tmp2[0] = delay_SubQmf_ser[j][temp_delay_ser[j]][sb][0];
								tmp2[1] = delay_SubQmf_ser[j][temp_delay_ser[j]][sb][1];
								if (use34hybrid_bands)
								{
									Q_Fract_allpass[0] = PSTables.Q_Fract_allpass_SubQmf34[sb][j][0];
									Q_Fract_allpass[1] = PSTables.Q_Fract_allpass_SubQmf34[sb][j][1];
								}
								else
								{
									Q_Fract_allpass[0] = PSTables.Q_Fract_allpass_SubQmf20[sb][j][0];
									Q_Fract_allpass[1] = PSTables.Q_Fract_allpass_SubQmf20[sb][j][1];
								}
							}
							else
							{
								tmp2[0] = delay_Qmf_ser[j][temp_delay_ser[j]][sb][0];
								tmp2[1] = delay_Qmf_ser[j][temp_delay_ser[j]][sb][1];
								Q_Fract_allpass[0] = PSTables.Q_Fract_allpass_Qmf[sb][j][0];
								Q_Fract_allpass[1] = PSTables.Q_Fract_allpass_Qmf[sb][j][1];
							}
							tmp[0] = tmp2[0] * Q_Fract_allpass[0] + tmp2[1] * Q_Fract_allpass[1];
							tmp[1] = tmp2[1] * Q_Fract_allpass[0] - tmp2[0] * Q_Fract_allpass[1];
							num = 0;
							float[] array2 = tmp;
							array2[num] += 0f - g_DecaySlope_filt[j] * R0[0];
							num = 1;
							array2 = tmp;
							array2[num] += 0f - g_DecaySlope_filt[j] * R0[1];
							tmp3[0] = R0[0] + g_DecaySlope_filt[j] * tmp[0];
							tmp3[1] = R0[1] + g_DecaySlope_filt[j] * tmp[1];
							if (gr < num_hybrid_groups)
							{
								delay_SubQmf_ser[j][temp_delay_ser[j]][sb][0] = tmp3[0];
								delay_SubQmf_ser[j][temp_delay_ser[j]][sb][1] = tmp3[1];
							}
							else
							{
								delay_Qmf_ser[j][temp_delay_ser[j]][sb][0] = tmp3[0];
								delay_Qmf_ser[j][temp_delay_ser[j]][sb][1] = tmp3[1];
							}
							R0[0] = tmp[0];
							R0[1] = tmp[1];
						}
					}
					int bk = -4097 & map_group2bk[gr];
					R0[0] = G_TransientRatio[k][bk] * R0[0];
					R0[1] = G_TransientRatio[k][bk] * R0[1];
					if (gr < num_hybrid_groups)
					{
						X_hybrid_right[k][sb][0] = R0[0];
						X_hybrid_right[k][sb][1] = R0[1];
					}
					else
					{
						X_right[k][sb][0] = R0[0];
						X_right[k][sb][1] = R0[1];
					}
					temp_delay++;
					if (temp_delay >= 2)
					{
						temp_delay = 0;
					}
					if (sb > nr_allpass_bands && gr >= num_hybrid_groups)
					{
						int[] array3 = delay_buf_index_delay;
						num = sb;
						array = array3;
						int[] array4 = array;
						int num2 = num;
						num = array[num] + 1;
						int num3 = num2;
						array = array4;
						int num4 = num;
						array[num3] = num;
						if (num4 >= delay_D[sb])
						{
							delay_buf_index_delay[sb] = 0;
						}
					}
					for (int j = 0; j < 3; j++)
					{
						num = j;
						array = temp_delay_ser;
						int[] array5 = array;
						int num5 = num;
						num = array[num] + 1;
						int num3 = num5;
						array = array5;
						int num6 = num;
						array[num3] = num;
						if (num6 >= num_sample_delay_ser[j])
						{
							temp_delay_ser[j] = 0;
						}
					}
				}
			}
		}
		saved_delay = temp_delay;
		for (int i = 0; i < 3; i++)
		{
			delay_buf_index_ser[i] = temp_delay_ser[i];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 157, 98, 195, 126, 127, 2, 127, 2, 105,
		105, 105, 233, 69, 106, 101, 170, 100, 168, 114,
		199, 169, 113, 177, 159, 6, 113, 237, 81, 116,
		180, 116, 148, 109, 111, 127, 2, 191, 8, 127,
		1, 223, 6, 111, 127, 2, 191, 5, 127, 1,
		223, 1, 105, 105, 105, 169, 110, 110, 110, 111,
		230, 70, 109, 147, 127, 4, 127, 7, 119, 119,
		134, 147, 127, 4, 127, 7, 119, 183, 113, 113,
		114, 242, 71, 212, 169, 121, 121, 121, 185, 127,
		5, 127, 5, 127, 5, 191, 5, 127, 2, 127,
		2, 127, 2, 191, 2, 101, 132, 167, 127, 9,
		127, 9, 127, 9, 159, 9, 107, 139, 106, 109,
		175, 106, 170, 137, 106, 121, 153, 107, 107, 131,
		106, 202, 109, 109, 109, 143, 109, 109, 109, 239,
		69, 185, 121, 121, 121, 154, 112, 112, 112, 144,
		111, 111, 111, 176, 148, 121, 121, 121, 154, 112,
		112, 112, 144, 114, 106, 106, 106, 138, 106, 106,
		106, 170, 111, 111, 111, 208, 158, 120, 120, 120,
		120, 116, 120, 120, 120, 216, 117, 177, 107, 110,
		110, 111, 177, 110, 110, 110, 206, 123, 123, 123,
		187, 148, 127, 11, 127, 11, 127, 11, 223, 11,
		107, 110, 110, 111, 177, 110, 110, 110, 238, 21,
		236, 50, 236, 127, 111, 106, 232, 158, 232, 236,
		58, 236, 161, 34
	})]
	private void ps_mix_phase(float[][][] X_left, float[][][] X_right, float[][][] X_hybrid_left, float[][][] X_hybrid_right)
	{
		int bk = 0;
		float[] h11 = new float[2];
		float[] h12 = new float[2];
		float[] h13 = new float[2];
		float[] h14 = new float[2];
		float[] H11 = new float[2];
		float[] H12 = new float[2];
		float[] H13 = new float[2];
		float[] H14 = new float[2];
		float[] deltaH11 = new float[2];
		float[] deltaH12 = new float[2];
		float[] deltaH13 = new float[2];
		float[] deltaH14 = new float[2];
		float[] tempLeft = new float[2];
		float[] tempRight = new float[2];
		float[] phaseLeft = new float[2];
		float[] phaseRight = new float[2];
		int no_iid_steps;
		float[] sf_iid;
		if (iid_mode >= 3)
		{
			no_iid_steps = 15;
			sf_iid = PSTables.sf_iid_fine;
		}
		else
		{
			no_iid_steps = 7;
			sf_iid = PSTables.sf_iid_normal;
		}
		int nr_ipdopd_par = ((ipd_mode != 0 && ipd_mode != 3) ? this.nr_ipdopd_par : 11);
		for (int gr = 0; gr < num_groups; gr++)
		{
			bk = -4097 & map_group2bk[gr];
			int maxsb = ((gr >= num_hybrid_groups) ? group_border[gr + 1] : (group_border[gr] + 1));
			for (int env = 0; env < num_env; env++)
			{
				if (icc_mode < 3)
				{
					float c_1 = sf_iid[no_iid_steps + iid_index[env][bk]];
					float c_2 = sf_iid[no_iid_steps - iid_index[env][bk]];
					float cosa2 = PSTables.cos_alphas[icc_index[env][bk]];
					float sina2 = PSTables.sin_alphas[icc_index[env][bk]];
					float cosb;
					float sinb;
					if (iid_mode >= 3)
					{
						if (iid_index[env][bk] < 0)
						{
							cosb = PSTables.cos_betas_fine[-iid_index[env][bk]][icc_index[env][bk]];
							sinb = 0f - PSTables.sin_betas_fine[-iid_index[env][bk]][icc_index[env][bk]];
						}
						else
						{
							cosb = PSTables.cos_betas_fine[iid_index[env][bk]][icc_index[env][bk]];
							sinb = PSTables.sin_betas_fine[iid_index[env][bk]][icc_index[env][bk]];
						}
					}
					else if (iid_index[env][bk] < 0)
					{
						cosb = PSTables.cos_betas_normal[-iid_index[env][bk]][icc_index[env][bk]];
						sinb = 0f - PSTables.sin_betas_normal[-iid_index[env][bk]][icc_index[env][bk]];
					}
					else
					{
						cosb = PSTables.cos_betas_normal[iid_index[env][bk]][icc_index[env][bk]];
						sinb = PSTables.sin_betas_normal[iid_index[env][bk]][icc_index[env][bk]];
					}
					float ab1 = cosb * cosa2;
					float ab2 = sinb * sina2;
					float ab3 = sinb * cosa2;
					float ab4 = cosb * sina2;
					h11[0] = c_2 * (ab1 - ab2);
					h12[0] = c_1 * (ab1 + ab2);
					h13[0] = c_2 * (ab3 + ab4);
					h14[0] = c_1 * (ab3 - ab4);
				}
				else
				{
					float cosa;
					float sina;
					float cosg;
					float sing;
					if (iid_mode >= 3)
					{
						int abs_iid2 = Math.abs(iid_index[env][bk]);
						cosa = PSTables.sincos_alphas_B_fine[no_iid_steps + iid_index[env][bk]][icc_index[env][bk]];
						sina = PSTables.sincos_alphas_B_fine[30 - (no_iid_steps + iid_index[env][bk])][icc_index[env][bk]];
						cosg = PSTables.cos_gammas_fine[abs_iid2][icc_index[env][bk]];
						sing = PSTables.sin_gammas_fine[abs_iid2][icc_index[env][bk]];
					}
					else
					{
						int abs_iid = Math.abs(iid_index[env][bk]);
						cosa = PSTables.sincos_alphas_B_normal[no_iid_steps + iid_index[env][bk]][icc_index[env][bk]];
						sina = PSTables.sincos_alphas_B_normal[14 - (no_iid_steps + iid_index[env][bk])][icc_index[env][bk]];
						cosg = PSTables.cos_gammas_normal[abs_iid][icc_index[env][bk]];
						sing = PSTables.sin_gammas_normal[abs_iid][icc_index[env][bk]];
					}
					h11[0] = 1.41421354f * (cosa * cosg);
					h12[0] = 1.41421354f * (sina * cosg);
					h13[0] = 1.41421354f * ((0f - cosa) * sing);
					h14[0] = 1.41421354f * (sina * sing);
				}
				if (enable_ipdopd && bk < nr_ipdopd_par)
				{
					int i = phase_hist;
					tempLeft[0] = ipd_prev[bk][i][0] * 0.25f;
					tempLeft[1] = ipd_prev[bk][i][1] * 0.25f;
					tempRight[0] = opd_prev[bk][i][0] * 0.25f;
					tempRight[1] = opd_prev[bk][i][1] * 0.25f;
					ipd_prev[bk][i][0] = PSTables.ipdopd_cos_tab[Math.abs(ipd_index[env][bk])];
					ipd_prev[bk][i][1] = PSTables.ipdopd_sin_tab[Math.abs(ipd_index[env][bk])];
					opd_prev[bk][i][0] = PSTables.ipdopd_cos_tab[Math.abs(opd_index[env][bk])];
					opd_prev[bk][i][1] = PSTables.ipdopd_sin_tab[Math.abs(opd_index[env][bk])];
					int num = 0;
					float[] array = tempLeft;
					array[num] += ipd_prev[bk][i][0];
					num = 1;
					array = tempLeft;
					array[num] += ipd_prev[bk][i][1];
					num = 0;
					array = tempRight;
					array[num] += opd_prev[bk][i][0];
					num = 1;
					array = tempRight;
					array[num] += opd_prev[bk][i][1];
					if (i == 0)
					{
						i = 2;
					}
					i += -1;
					num = 0;
					array = tempLeft;
					array[num] += ipd_prev[bk][i][0] * 0.5f;
					num = 1;
					array = tempLeft;
					array[num] += ipd_prev[bk][i][1] * 0.5f;
					num = 0;
					array = tempRight;
					array[num] += opd_prev[bk][i][0] * 0.5f;
					num = 1;
					array = tempRight;
					array[num] += opd_prev[bk][i][1] * 0.5f;
					float xy = magnitude_c(tempRight);
					float pq = magnitude_c(tempLeft);
					if (xy != 0f)
					{
						phaseLeft[0] = tempRight[0] / xy;
						phaseLeft[1] = tempRight[1] / xy;
					}
					else
					{
						phaseLeft[0] = 0f;
						phaseLeft[1] = 0f;
					}
					float xypq = xy * pq;
					if (xypq != 0f)
					{
						float tmp1 = tempRight[0] * tempLeft[0] + tempRight[1] * tempLeft[1];
						float tmp2 = tempRight[1] * tempLeft[0] - tempRight[0] * tempLeft[1];
						phaseRight[0] = tmp1 / xypq;
						phaseRight[1] = tmp2 / xypq;
					}
					else
					{
						phaseRight[0] = 0f;
						phaseRight[1] = 0f;
					}
					h11[1] = h11[0] * phaseLeft[1];
					h12[1] = h12[0] * phaseRight[1];
					h13[1] = h13[0] * phaseLeft[1];
					h14[1] = h14[0] * phaseRight[1];
					h11[0] = h11[0] * phaseLeft[0];
					h12[0] = h12[0] * phaseRight[0];
					h13[0] = h13[0] * phaseLeft[0];
					h14[0] = h14[0] * phaseRight[0];
				}
				float L = border_position[env + 1] - border_position[env];
				deltaH11[0] = (h11[0] - h11_prev[gr][0]) / L;
				deltaH12[0] = (h12[0] - h12_prev[gr][0]) / L;
				deltaH13[0] = (h13[0] - h21_prev[gr][0]) / L;
				deltaH14[0] = (h14[0] - h22_prev[gr][0]) / L;
				H11[0] = h11_prev[gr][0];
				H12[0] = h12_prev[gr][0];
				H13[0] = h21_prev[gr][0];
				H14[0] = h22_prev[gr][0];
				h11_prev[gr][0] = h11[0];
				h12_prev[gr][0] = h12[0];
				h21_prev[gr][0] = h13[0];
				h22_prev[gr][0] = h14[0];
				if (enable_ipdopd && bk < nr_ipdopd_par)
				{
					deltaH11[1] = (h11[1] - h11_prev[gr][1]) / L;
					deltaH12[1] = (h12[1] - h12_prev[gr][1]) / L;
					deltaH13[1] = (h13[1] - h21_prev[gr][1]) / L;
					deltaH14[1] = (h14[1] - h22_prev[gr][1]) / L;
					H11[1] = h11_prev[gr][1];
					H12[1] = h12_prev[gr][1];
					H13[1] = h21_prev[gr][1];
					H14[1] = h22_prev[gr][1];
					if ((0x1000u & (uint)map_group2bk[gr]) != 0)
					{
						deltaH11[1] = 0f - deltaH11[1];
						deltaH12[1] = 0f - deltaH12[1];
						deltaH13[1] = 0f - deltaH13[1];
						deltaH14[1] = 0f - deltaH14[1];
						H11[1] = 0f - H11[1];
						H12[1] = 0f - H12[1];
						H13[1] = 0f - H13[1];
						H14[1] = 0f - H14[1];
					}
					h11_prev[gr][1] = h11[1];
					h12_prev[gr][1] = h12[1];
					h21_prev[gr][1] = h13[1];
					h22_prev[gr][1] = h14[1];
				}
				for (int j = border_position[env]; j < border_position[env + 1]; j++)
				{
					int num = 0;
					float[] array = H11;
					array[num] += deltaH11[0];
					num = 0;
					array = H12;
					array[num] += deltaH12[0];
					num = 0;
					array = H13;
					array[num] += deltaH13[0];
					num = 0;
					array = H14;
					array[num] += deltaH14[0];
					if (enable_ipdopd && bk < nr_ipdopd_par)
					{
						num = 1;
						array = H11;
						array[num] += deltaH11[1];
						num = 1;
						array = H12;
						array[num] += deltaH12[1];
						num = 1;
						array = H13;
						array[num] += deltaH13[1];
						num = 1;
						array = H14;
						array[num] += deltaH14[1];
					}
					for (int sb = group_border[gr]; sb < maxsb; sb++)
					{
						float[] inLeft = new float[2];
						float[] inRight = new float[2];
						if (gr < num_hybrid_groups)
						{
							inLeft[0] = X_hybrid_left[j][sb][0];
							inLeft[1] = X_hybrid_left[j][sb][1];
							inRight[0] = X_hybrid_right[j][sb][0];
							inRight[1] = X_hybrid_right[j][sb][1];
						}
						else
						{
							inLeft[0] = X_left[j][sb][0];
							inLeft[1] = X_left[j][sb][1];
							inRight[0] = X_right[j][sb][0];
							inRight[1] = X_right[j][sb][1];
						}
						tempLeft[0] = H11[0] * inLeft[0] + H13[0] * inRight[0];
						tempLeft[1] = H11[0] * inLeft[1] + H13[0] * inRight[1];
						tempRight[0] = H12[0] * inLeft[0] + H14[0] * inRight[0];
						tempRight[1] = H12[0] * inLeft[1] + H14[0] * inRight[1];
						if (enable_ipdopd && bk < nr_ipdopd_par)
						{
							num = 0;
							array = tempLeft;
							array[num] -= H11[1] * inLeft[1] + H13[1] * inRight[1];
							num = 1;
							array = tempLeft;
							array[num] += H11[1] * inLeft[0] + H13[1] * inRight[0];
							num = 0;
							array = tempRight;
							array[num] -= H12[1] * inLeft[1] + H14[1] * inRight[1];
							num = 1;
							array = tempRight;
							array[num] += H12[1] * inLeft[0] + H14[1] * inRight[0];
						}
						if (gr < num_hybrid_groups)
						{
							X_hybrid_left[j][sb][0] = tempLeft[0];
							X_hybrid_left[j][sb][1] = tempLeft[1];
							X_hybrid_right[j][sb][0] = tempRight[0];
							X_hybrid_right[j][sb][1] = tempRight[1];
						}
						else
						{
							X_left[j][sb][0] = tempLeft[0];
							X_left[j][sb][1] = tempLeft[1];
							X_right[j][sb][0] = tempRight[0];
							X_right[j][sb][1] = tempRight[1];
						}
					}
				}
				phase_hist++;
				if (phase_hist == 2)
				{
					phase_hist = 0;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		120,
		162,
		105,
		109,
		109,
		109,
		109,
		109,
		110,
		110,
		110,
		110,
		127,
		12,
		127,
		12,
		127,
		12,
		127,
		12,
		110,
		110,
		110,
		110,
		109,
		109,
		110,
		110,
		127,
		19,
		127,
		18,
		127,
		24,
		127,
		24,
		110,
		110,
		110,
		127,
		12,
		127,
		12,
		127,
		12,
		127,
		12,
		127,
		18,
		byte.MaxValue,
		18,
		69,
		109,
		136,
		168,
		136,
		104,
		42,
		199,
		103,
		138,
		240,
		61,
		231,
		71,
		101,
		105,
		108,
		172,
		104,
		43,
		167,
		105,
		42,
		231,
		69,
		104,
		112,
		112,
		112,
		240,
		60,
		231,
		71,
		136,
		107,
		114,
		114,
		114,
		114,
		114,
		114,
		114,
		242,
		56,
		234,
		74
	})]
	public PS(SampleFrequency sr, int numTimeSlotsRate)
	{
		border_position = new int[6];
		iid_dt = new bool[5];
		icc_dt = new bool[5];
		ipd_dt = new bool[5];
		opd_dt = new bool[5];
		iid_index_prev = new int[34];
		icc_index_prev = new int[34];
		ipd_index_prev = new int[17];
		opd_index_prev = new int[17];
		int[] array = new int[2];
		int num = (array[1] = 34);
		num = (array[0] = 5);
		iid_index = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 34);
		num = (array[0] = 5);
		icc_index = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 17);
		num = (array[0] = 5);
		ipd_index = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 17);
		num = (array[0] = 5);
		opd_index = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		ipd_index_1 = new int[17];
		opd_index_1 = new int[17];
		ipd_index_2 = new int[17];
		opd_index_2 = new int[17];
		delay_buf_index_ser = new int[3];
		num_sample_delay_ser = new int[3];
		delay_D = new int[64];
		delay_buf_index_delay = new int[64];
		array = new int[3];
		num = (array[2] = 2);
		num = (array[1] = 64);
		num = (array[0] = 14);
		delay_Qmf = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 2);
		num = (array[1] = 32);
		num = (array[0] = 2);
		delay_SubQmf = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		array = new int[4];
		num = (array[3] = 2);
		num = (array[2] = 64);
		num = (array[1] = 5);
		num = (array[0] = 3);
		delay_Qmf_ser = (float[][][][])ByteCodeHelper.multianewarray(typeof(float[][][][]).TypeHandle, array);
		array = new int[4];
		num = (array[3] = 2);
		num = (array[2] = 32);
		num = (array[1] = 5);
		num = (array[0] = 3);
		delay_SubQmf_ser = (float[][][][])ByteCodeHelper.multianewarray(typeof(float[][][][]).TypeHandle, array);
		P_PeakDecayNrg = new float[34];
		P_prev = new float[34];
		P_SmoothPeakDecayDiffNrg_prev = new float[34];
		array = new int[2];
		num = (array[1] = 2);
		num = (array[0] = 50);
		h11_prev = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 2);
		num = (array[0] = 50);
		h12_prev = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 2);
		num = (array[0] = 50);
		h21_prev = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 2);
		num = (array[0] = 50);
		h22_prev = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 2);
		num = (array[1] = 2);
		num = (array[0] = 20);
		ipd_prev = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 2);
		num = (array[1] = 2);
		num = (array[0] = 20);
		opd_prev = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		hyb = new PSFilterbank(numTimeSlotsRate);
		this.numTimeSlotsRate = numTimeSlotsRate;
		ps_data_available = 0;
		saved_delay = 0;
		for (int i = 0; i < 64; i++)
		{
			delay_buf_index_delay[i] = 0;
		}
		for (int i = 0; i < 3; i++)
		{
			delay_buf_index_ser[i] = 0;
			num_sample_delay_ser[i] = PSTables.delay_length_d[i];
		}
		int short_delay_band = 35;
		nr_allpass_bands = 22;
		alpha_decay = 0.7659283f;
		alpha_smooth = 0.25f;
		for (int i = 0; i < short_delay_band; i++)
		{
			delay_D[i] = 14;
		}
		for (int i = short_delay_band; i < 64; i++)
		{
			delay_D[i] = 1;
		}
		for (int i = 0; i < 50; i++)
		{
			h11_prev[i][0] = 1f;
			h12_prev[i][1] = 1f;
			h11_prev[i][0] = 1f;
			h12_prev[i][1] = 1f;
		}
		phase_hist = 0;
		for (int i = 0; i < 20; i++)
		{
			ipd_prev[i][0][0] = 0f;
			ipd_prev[i][0][1] = 0f;
			ipd_prev[i][1][0] = 0f;
			ipd_prev[i][1][1] = 0f;
			opd_prev[i][0][0] = 0f;
			opd_prev[i][0][1] = 0f;
			opd_prev[i][1][0] = 0f;
			opd_prev[i][1][1] = 0f;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159,
		96,
		130,
		169,
		108,
		136,
		168,
		141,
		105,
		142,
		115,
		147,
		115,
		168,
		205,
		141,
		105,
		142,
		147,
		115,
		200,
		205,
		105,
		104,
		163,
		109,
		137,
		149,
		105,
		110,
		50,
		231,
		69,
		108,
		111,
		175,
		106,
		223,
		11,
		byte.MaxValue,
		9,
		55,
		234,
		79,
		105,
		110,
		176,
		byte.MaxValue,
		11,
		60,
		233,
		73,
		140,
		106,
		103,
		173,
		103,
		102,
		138,
		104,
		113,
		131,
		169,
		141,
		136
	})]
	public virtual int decode(IBitStream ld)
	{
		long bits = ld.getPosition();
		if (ld.readBool())
		{
			header_read = true;
			use34hybrid_bands = false;
			enable_iid = ld.readBool();
			if (enable_iid)
			{
				iid_mode = ld.readBits(3);
				nr_iid_par = PSTables.nr_iid_par_tab[iid_mode];
				nr_ipdopd_par = PSTables.nr_ipdopd_par_tab[iid_mode];
				if (iid_mode == 2 || iid_mode == 5)
				{
					use34hybrid_bands = true;
				}
				ipd_mode = iid_mode;
			}
			enable_icc = ld.readBool();
			if (enable_icc)
			{
				icc_mode = ld.readBits(3);
				nr_icc_par = PSTables.nr_icc_par_tab[icc_mode];
				if (icc_mode == 2 || icc_mode == 5)
				{
					use34hybrid_bands = true;
				}
			}
			enable_ext = ld.readBool();
		}
		if (!header_read)
		{
			ps_data_available = 0;
			return 1;
		}
		frame_class = ld.readBit();
		int tmp = ld.readBits(2);
		num_env = PSTables.num_env_tab[frame_class][tmp];
		if (frame_class != 0)
		{
			for (int k = 1; k < num_env + 1; k++)
			{
				border_position[k] = ld.readBits(5) + 1;
			}
		}
		if (enable_iid)
		{
			for (int j = 0; j < num_env; j++)
			{
				iid_dt[j] = ld.readBool();
				if (iid_mode < 3)
				{
					huff_data(ld, iid_dt[j], nr_iid_par, PSHuffmanTables.t_huff_iid_def, PSHuffmanTables.f_huff_iid_def, iid_index[j]);
				}
				else
				{
					huff_data(ld, iid_dt[j], nr_iid_par, PSHuffmanTables.t_huff_iid_fine, PSHuffmanTables.f_huff_iid_fine, iid_index[j]);
				}
			}
		}
		if (enable_icc)
		{
			for (int i = 0; i < num_env; i++)
			{
				icc_dt[i] = ld.readBool();
				huff_data(ld, icc_dt[i], nr_icc_par, PSHuffmanTables.t_huff_icc, PSHuffmanTables.f_huff_icc, icc_index[i]);
			}
		}
		if (enable_ext)
		{
			int cnt = ld.readBits(4);
			if (cnt == 15)
			{
				cnt += ld.readBits(8);
			}
			int num_bits_left = 8 * cnt;
			while (num_bits_left > 7)
			{
				int ps_extension_id = ld.readBits(2);
				num_bits_left += -2;
				num_bits_left -= ps_extension(ld, ps_extension_id, num_bits_left);
			}
			ld.skipBits(num_bits_left);
		}
		int bits2 = (int)(ld.getPosition() - bits);
		ps_data_available = 1;
		return bits2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 76, 162, 127, 14, 191, 14, 167, 105, 108,
		108, 105, 105, 105, 170, 108, 108, 105, 105, 105,
		232, 70, 218, 171, 171, 186, 186
	})]
	public virtual int process(float[][][] X_left, float[][][] X_right)
	{
		int[] array = new int[3];
		int num = (array[2] = 2);
		num = (array[1] = 32);
		num = (array[0] = 32);
		float[][][] X_hybrid_left = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 2);
		num = (array[1] = 32);
		num = (array[0] = 32);
		float[][][] X_hybrid_right = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		ps_data_decode();
		if (use34hybrid_bands)
		{
			group_border = PSTables.group_border34;
			map_group2bk = PSTables.map_group2bk34;
			num_groups = 50;
			num_hybrid_groups = 32;
			nr_par_bands = 34;
			decay_cutoff = 5;
		}
		else
		{
			group_border = PSTables.group_border20;
			map_group2bk = PSTables.map_group2bk20;
			num_groups = 22;
			num_hybrid_groups = 10;
			nr_par_bands = 20;
			decay_cutoff = 3;
		}
		hyb.hybrid_analysis(X_left, X_hybrid_left, use34hybrid_bands, numTimeSlotsRate);
		ps_decorrelate(X_left, X_right, X_hybrid_left, X_hybrid_right);
		ps_mix_phase(X_left, X_right, X_hybrid_left, X_hybrid_right);
		hyb.hybrid_synthesis(X_left, X_hybrid_left, use34hybrid_bands, numTimeSlotsRate);
		hyb.hybrid_synthesis(X_right, X_hybrid_right, use34hybrid_bands, numTimeSlotsRate);
		return 0;
	}
}
