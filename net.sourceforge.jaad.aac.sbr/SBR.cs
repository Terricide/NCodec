using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;
using net.sourceforge.jaad.aac.ps;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.sbr;

[Implements(new string[] { "net.sourceforge.jaad.aac.sbr.SBRConstants", "net.sourceforge.jaad.aac.syntax.SyntaxConstants", "net.sourceforge.jaad.aac.sbr.HuffmanTables" })]
public class SBR : Object, SBRConstants, SyntaxConstants, HuffmanTables
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private bool downSampledSBR;

	[Modifiers(Modifiers.Final)]
	internal SampleFrequency sample_rate;

	internal int maxAACLine;

	internal int rate;

	internal bool just_seeked;

	internal int ret;

	internal bool[] amp_res;

	internal int k0;

	internal int kx;

	internal int M;

	internal int N_master;

	internal int N_high;

	internal int N_low;

	internal int N_Q;

	internal int[] N_L;

	internal int[] n;

	internal int[] f_master;

	internal int[][] f_table_res;

	internal int[] f_table_noise;

	internal int[][] f_table_lim;

	internal int[] table_map_k_to_g;

	internal int[] abs_bord_lead;

	internal int[] abs_bord_trail;

	internal int[] n_rel_lead;

	internal int[] n_rel_trail;

	internal int[] L_E;

	internal int[] L_E_prev;

	internal int[] L_Q;

	internal int[][] t_E;

	internal int[][] t_Q;

	internal int[][] f;

	internal int[] f_prev;

	internal float[][][] G_temp_prev;

	internal float[][][] Q_temp_prev;

	internal int[] GQ_ringbuf_index;

	internal int[][][] E;

	internal int[][] E_prev;

	internal float[][][] E_orig;

	internal float[][][] E_curr;

	internal int[][][] Q;

	internal float[][][] Q_div;

	internal float[][][] Q_div2;

	internal int[][] Q_prev;

	internal int[] l_A;

	internal int[] l_A_prev;

	internal int[][] bs_invf_mode;

	internal int[][] bs_invf_mode_prev;

	internal float[][] bwArray;

	internal float[][] bwArray_prev;

	internal int noPatches;

	internal int[] patchNoSubbands;

	internal int[] patchStartSubband;

	internal int[][] bs_add_harmonic;

	internal int[][] bs_add_harmonic_prev;

	internal int[] index_noise_prev;

	internal int[] psi_is_prev;

	internal int bs_start_freq_prev;

	internal int bs_stop_freq_prev;

	internal int bs_xover_band_prev;

	internal int bs_freq_scale_prev;

	internal bool bs_alter_scale_prev;

	internal int bs_noise_bands_prev;

	internal int[] prevEnvIsShort;

	internal int kx_prev;

	internal int bsco;

	internal int bsco_prev;

	internal int M_prev;

	internal bool Reset;

	internal int frame;

	internal int header_count;

	internal bool stereo;

	internal AnalysisFilterbank[] qmfa;

	internal SynthesisFilterbank[] qmfs;

	internal float[][][][] Xsbr;

	internal int numTimeSlotsRate;

	internal int numTimeSlots;

	internal int tHFGen;

	internal int tHFAdj;

	internal PS ps;

	internal bool ps_used;

	internal bool psResetFlag;

	internal bool bs_header_flag;

	internal int bs_crc_flag;

	internal int bs_sbr_crc_bits;

	internal int bs_protocol_version;

	internal bool bs_amp_res;

	internal int bs_start_freq;

	internal int bs_stop_freq;

	internal int bs_xover_band;

	internal int bs_freq_scale;

	internal bool bs_alter_scale;

	internal int bs_noise_bands;

	internal int bs_limiter_bands;

	internal int bs_limiter_gains;

	internal bool bs_interpol_freq;

	internal bool bs_smoothing_mode;

	internal int bs_samplerate_mode;

	internal bool[] bs_add_harmonic_flag;

	internal bool[] bs_add_harmonic_flag_prev;

	internal bool bs_extended_data;

	internal int bs_extension_id;

	internal int bs_extension_data;

	internal bool bs_coupling;

	internal int[] bs_frame_class;

	internal int[][] bs_rel_bord;

	internal int[][] bs_rel_bord_0;

	internal int[][] bs_rel_bord_1;

	internal int[] bs_pointer;

	internal int[] bs_abs_bord_0;

	internal int[] bs_abs_bord_1;

	internal int[] bs_num_rel_0;

	internal int[] bs_num_rel_1;

	internal int[][] bs_df_env;

	internal int[][] bs_df_noise;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int EXTENSION_ID_PS = 2;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int MAX_NTSRHFG = 40;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int MAX_NTSR = 32;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int MAX_M = 49;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int MAX_L_E = 5;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int EXT_SBR_DATA = 13;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int EXT_SBR_DATA_CRC = 14;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int FIXFIX = 0;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int FIXVAR = 1;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int VARFIX = 2;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int VARVAR = 3;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int LO_RES = 0;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int HI_RES = 1;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int NO_TIME_SLOTS_960 = 15;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int NO_TIME_SLOTS = 16;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int RATE = 2;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int NOISE_FLOOR_OFFSET = 6;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int T_HFGEN = 8;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int T_HFADJ = 2;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] startMinTable
	{
		[HideFromJava]
		get
		{
			return SBRConstants.startMinTable;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] offsetIndexTable
	{
		[HideFromJava]
		get
		{
			return SBRConstants.offsetIndexTable;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] OFFSET
	{
		[HideFromJava]
		get
		{
			return SBRConstants.OFFSET;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] T_HUFFMAN_ENV_1_5DB
	{
		[HideFromJava]
		get
		{
			return HuffmanTables.T_HUFFMAN_ENV_1_5DB;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] F_HUFFMAN_ENV_1_5DB
	{
		[HideFromJava]
		get
		{
			return HuffmanTables.F_HUFFMAN_ENV_1_5DB;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] T_HUFFMAN_ENV_BAL_1_5DB
	{
		[HideFromJava]
		get
		{
			return HuffmanTables.T_HUFFMAN_ENV_BAL_1_5DB;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] F_HUFFMAN_ENV_BAL_1_5DB
	{
		[HideFromJava]
		get
		{
			return HuffmanTables.F_HUFFMAN_ENV_BAL_1_5DB;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] T_HUFFMAN_ENV_3_0DB
	{
		[HideFromJava]
		get
		{
			return HuffmanTables.T_HUFFMAN_ENV_3_0DB;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] F_HUFFMAN_ENV_3_0DB
	{
		[HideFromJava]
		get
		{
			return HuffmanTables.F_HUFFMAN_ENV_3_0DB;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] T_HUFFMAN_ENV_BAL_3_0DB
	{
		[HideFromJava]
		get
		{
			return HuffmanTables.T_HUFFMAN_ENV_BAL_3_0DB;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] F_HUFFMAN_ENV_BAL_3_0DB
	{
		[HideFromJava]
		get
		{
			return HuffmanTables.F_HUFFMAN_ENV_BAL_3_0DB;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] T_HUFFMAN_NOISE_3_0DB
	{
		[HideFromJava]
		get
		{
			return HuffmanTables.T_HUFFMAN_NOISE_3_0DB;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] T_HUFFMAN_NOISE_BAL_3_0DB
	{
		[HideFromJava]
		get
		{
			return HuffmanTables.T_HUFFMAN_NOISE_BAL_3_0DB;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 15, 162, 143, 205, 110, 110, 110, 105, 104,
		136, 100, 110, 109, 208, 104, 104, 168, 100, 110,
		110, 109, 207, 104, 104, 104, 168
	})]
	private void sbr_header(IBitStream ld)
	{
		header_count++;
		bs_amp_res = ld.readBool();
		bs_start_freq = ld.readBits(4);
		bs_stop_freq = ld.readBits(4);
		bs_xover_band = ld.readBits(3);
		ld.readBits(2);
		int bs_header_extra_1 = (ld.readBool() ? 1 : 0);
		int bs_header_extra_2 = (ld.readBool() ? 1 : 0);
		if (bs_header_extra_1 != 0)
		{
			bs_freq_scale = ld.readBits(2);
			bs_alter_scale = ld.readBool();
			bs_noise_bands = ld.readBits(2);
		}
		else
		{
			bs_freq_scale = 2;
			bs_alter_scale = true;
			bs_noise_bands = 2;
		}
		if (bs_header_extra_2 != 0)
		{
			bs_limiter_bands = ld.readBits(2);
			bs_limiter_gains = ld.readBits(2);
			bs_interpol_freq = ld.readBool();
			bs_smoothing_mode = ld.readBool();
		}
		else
		{
			bs_limiter_bands = 2;
			bs_limiter_gains = 2;
			bs_interpol_freq = true;
			bs_smoothing_mode = true;
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		56,
		98,
		byte.MaxValue,
		54,
		70,
		170,
		168,
		109,
		109,
		109,
		109,
		109,
		109
	})]
	internal virtual void sbr_reset()
	{
		if (bs_start_freq != bs_start_freq_prev || bs_stop_freq != bs_stop_freq_prev || bs_freq_scale != bs_freq_scale_prev || bs_alter_scale != bs_alter_scale_prev || bs_xover_band != bs_xover_band_prev || bs_noise_bands != bs_noise_bands_prev)
		{
			Reset = true;
		}
		else
		{
			Reset = false;
		}
		bs_start_freq_prev = bs_start_freq;
		bs_stop_freq_prev = bs_stop_freq;
		bs_freq_scale_prev = bs_freq_scale;
		bs_alter_scale_prev = bs_alter_scale;
		bs_xover_band_prev = bs_xover_band;
		bs_noise_bands_prev = bs_noise_bands;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 50, 65, 68, 195, 116, 180, 115, 109, 135,
		115, 109, 199, 109, 165, 101, 180, 148, 141, 134
	})]
	internal virtual int calc_sbr_tables(int start_freq, int stop_freq, int samplerate_mode, int freq_scale, bool alter_scale, int xover_band)
	{
		int result = 0;
		k0 = FBT.qmf_start_channel(start_freq, samplerate_mode, sample_rate);
		int k2 = FBT.qmf_stop_channel(stop_freq, sample_rate, k0);
		if (sample_rate.getFrequency() >= 48000)
		{
			if (k2 - k0 > 32)
			{
				result++;
			}
		}
		else if (sample_rate.getFrequency() <= 32000)
		{
			if (k2 - k0 > 48)
			{
				result++;
			}
		}
		else if (k2 - k0 > 45)
		{
			result++;
		}
		result = ((freq_scale != 0) ? (result + FBT.master_frequency_table(this, k0, k2, freq_scale, alter_scale)) : (result + FBT.master_frequency_table_fs0(this, k0, k2, alter_scale)));
		result += FBT.derived_frequency_table(this, xover_band, k2);
		return (result > 0) ? 1 : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 3, 66, 147, 105, 109, 163, 109, 163 })]
	private int sbr_data(IBitStream ld)
	{
		rate = ((bs_samplerate_mode == 0) ? 1 : 2);
		int result;
		if (stereo)
		{
			int result2;
			if ((result2 = sbr_channel_pair_element(ld)) > 0)
			{
				return result2;
			}
		}
		else if ((result = sbr_single_channel_element(ld)) > 0)
		{
			return result;
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		158, 237, 130, 137, 105, 169, 141, 108, 110, 163,
		113, 113, 113, 145, 110, 117, 21, 199, 110, 53,
		199, 105, 105, 169, 108, 53, 199, 105, 105, 105,
		137, 114, 146, 111, 107, 137, 111, 110, 174, 111,
		107, 107, 139, 106, 48, 169, 106, 48, 201, 111,
		100, 146, 107, 107, 107, 105, 48, 169, 105, 48,
		201, 132, 105, 105, 105, 105, 105, 105, 105, 137,
		114, 146, 111, 107, 137, 111, 107, 137, 104, 136,
		105, 135, 109, 140, 106, 103, 173, 103, 102, 132,
		110, 103, 181, 103, 131, 104, 163, 102, 202
	})]
	private int sbr_channel_pair_element(IBitStream ld)
	{
		if (ld.readBool())
		{
			ld.readBits(4);
			ld.readBits(4);
		}
		bs_coupling = ld.readBool();
		if (bs_coupling)
		{
			int result2;
			if ((result2 = sbr_grid(ld, 0)) > 0)
			{
				return result2;
			}
			bs_frame_class[1] = bs_frame_class[0];
			L_E[1] = L_E[0];
			L_Q[1] = L_Q[0];
			bs_pointer[1] = bs_pointer[0];
			for (int j = 0; j <= L_E[0]; j++)
			{
				t_E[1][j] = t_E[0][j];
				f[1][j] = f[0][j];
			}
			for (int j = 0; j <= L_Q[0]; j++)
			{
				t_Q[1][j] = t_Q[0][j];
			}
			sbr_dtdf(ld, 0);
			sbr_dtdf(ld, 1);
			invf_mode(ld, 0);
			for (int j = 0; j < N_Q; j++)
			{
				bs_invf_mode[1][j] = bs_invf_mode[0][j];
			}
			sbr_envelope(ld, 0);
			sbr_noise(ld, 0);
			sbr_envelope(ld, 1);
			sbr_noise(ld, 1);
			Arrays.fill(bs_add_harmonic[0], 0, 64, 0);
			Arrays.fill(bs_add_harmonic[1], 0, 64, 0);
			bs_add_harmonic_flag[0] = ld.readBool();
			if (bs_add_harmonic_flag[0])
			{
				sinusoidal_coding(ld, 0);
			}
			bs_add_harmonic_flag[1] = ld.readBool();
			if (bs_add_harmonic_flag[1])
			{
				sinusoidal_coding(ld, 1);
			}
		}
		else
		{
			int[] saved_t_E = new int[6];
			int[] saved_t_Q = new int[3];
			int saved_L_E = L_E[0];
			int saved_L_Q = L_Q[0];
			int saved_frame_class = bs_frame_class[0];
			for (int i = 0; i < saved_L_E; i++)
			{
				saved_t_E[i] = t_E[0][i];
			}
			for (int i = 0; i < saved_L_Q; i++)
			{
				saved_t_Q[i] = t_Q[0][i];
			}
			int result;
			if ((result = sbr_grid(ld, 0)) > 0)
			{
				return result;
			}
			if ((result = sbr_grid(ld, 1)) > 0)
			{
				bs_frame_class[0] = saved_frame_class;
				L_E[0] = saved_L_E;
				L_Q[0] = saved_L_Q;
				for (int i = 0; i < 6; i++)
				{
					t_E[0][i] = saved_t_E[i];
				}
				for (int i = 0; i < 3; i++)
				{
					t_Q[0][i] = saved_t_Q[i];
				}
				return result;
			}
			sbr_dtdf(ld, 0);
			sbr_dtdf(ld, 1);
			invf_mode(ld, 0);
			invf_mode(ld, 1);
			sbr_envelope(ld, 0);
			sbr_envelope(ld, 1);
			sbr_noise(ld, 0);
			sbr_noise(ld, 1);
			Arrays.fill(bs_add_harmonic[0], 0, 64, 0);
			Arrays.fill(bs_add_harmonic[1], 0, 64, 0);
			bs_add_harmonic_flag[0] = ld.readBool();
			if (bs_add_harmonic_flag[0])
			{
				sinusoidal_coding(ld, 0);
			}
			bs_add_harmonic_flag[1] = ld.readBool();
			if (bs_add_harmonic_flag[1])
			{
				sinusoidal_coding(ld, 1);
			}
		}
		NoiseEnvelope.dequantChannel(this, 0);
		NoiseEnvelope.dequantChannel(this, 1);
		if (bs_coupling)
		{
			NoiseEnvelope.unmap(this);
		}
		bs_extended_data = ld.readBool();
		if (bs_extended_data)
		{
			int cnt = ld.readBits(4);
			if (cnt == 15)
			{
				cnt += ld.readBits(8);
			}
			int nr_bits_left = 8 * cnt;
			while (nr_bits_left > 7)
			{
				int tmp_nr_bits = 0;
				bs_extension_id = ld.readBits(2);
				tmp_nr_bits += 2;
				tmp_nr_bits += sbr_extension(ld, bs_extension_id, nr_bits_left);
				if (tmp_nr_bits > nr_bits_left)
				{
					return 1;
				}
				nr_bits_left -= tmp_nr_bits;
			}
			if (nr_bits_left > 0)
			{
				ld.readBits(nr_bits_left);
			}
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		158,
		byte.MaxValue,
		130,
		105,
		169,
		110,
		131,
		105,
		105,
		105,
		137,
		136,
		114,
		146,
		111,
		107,
		137,
		141,
		140,
		99,
		105,
		102,
		171,
		101,
		104,
		132,
		110,
		167,
		106,
		100,
		229,
		69,
		200,
		180,
		102,
		131,
		102,
		166,
		101,
		201
	})]
	private int sbr_single_channel_element(IBitStream ld)
	{
		if (ld.readBool())
		{
			ld.readBits(4);
		}
		int result;
		if ((result = sbr_grid(ld, 0)) > 0)
		{
			return result;
		}
		sbr_dtdf(ld, 0);
		invf_mode(ld, 0);
		sbr_envelope(ld, 0);
		sbr_noise(ld, 0);
		NoiseEnvelope.dequantChannel(this, 0);
		Arrays.fill(bs_add_harmonic[0], 0, 64, 0);
		Arrays.fill(bs_add_harmonic[1], 0, 64, 0);
		bs_add_harmonic_flag[0] = ld.readBool();
		if (bs_add_harmonic_flag[0])
		{
			sinusoidal_coding(ld, 0);
		}
		bs_extended_data = ld.readBool();
		if (bs_extended_data)
		{
			int ps_ext_read = 0;
			int cnt = ld.readBits(4);
			if (cnt == 15)
			{
				cnt += ld.readBits(8);
			}
			int nr_bits_left = 8 * cnt;
			while (nr_bits_left > 7)
			{
				int tmp_nr_bits = 0;
				bs_extension_id = ld.readBits(2);
				tmp_nr_bits += 2;
				if (bs_extension_id == 2)
				{
					if (ps_ext_read == 0)
					{
						ps_ext_read = 1;
					}
					else
					{
						bs_extension_id = 3;
					}
				}
				tmp_nr_bits += sbr_extension(ld, bs_extension_id, nr_bits_left);
				if (tmp_nr_bits > nr_bits_left)
				{
					return 1;
				}
				nr_bits_left -= tmp_nr_bits;
			}
			if (nr_bits_left > 0)
			{
				ld.readBits(nr_bits_left);
			}
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		158, 199, 130, 99, 106, 106, 138, 144, 159, 4,
		138, 143, 105, 105, 46, 201, 106, 111, 108, 106,
		166, 113, 139, 107, 55, 169, 108, 145, 105, 54,
		201, 106, 107, 106, 108, 166, 106, 139, 107, 55,
		169, 108, 145, 105, 50, 201, 107, 111, 108, 106,
		166, 106, 113, 112, 144, 155, 112, 55, 169, 112,
		55, 169, 124, 145, 105, 50, 201, 107, 107, 113,
		209, 108, 146, 144, 108, 131, 108, 140, 170, 110,
		106, 106, 106, 132, 136
	})]
	private int sbr_grid(IBitStream ld, int ch)
	{
		int bs_num_env = 0;
		int saved_L_E = L_E[ch];
		int saved_L_Q = L_Q[ch];
		int saved_frame_class = bs_frame_class[ch];
		bs_frame_class[ch] = ld.readBits(2);
		switch (bs_frame_class[ch])
		{
		case 0:
		{
			int i = ld.readBits(2);
			bs_num_env = Math.min(1 << i, 5);
			i = ld.readBit();
			for (int env = 0; env < bs_num_env; env++)
			{
				f[ch][env] = i;
			}
			abs_bord_lead[ch] = 0;
			abs_bord_trail[ch] = numTimeSlots;
			n_rel_lead[ch] = bs_num_env - 1;
			n_rel_trail[ch] = 0;
			break;
		}
		case 1:
		{
			int bs_abs_bord = ld.readBits(2) + numTimeSlots;
			bs_num_env = ld.readBits(2) + 1;
			for (int rel = 0; rel < bs_num_env - 1; rel++)
			{
				bs_rel_bord[ch][rel] = 2 * ld.readBits(2) + 2;
			}
			int j = sbr_log2(bs_num_env + 1);
			bs_pointer[ch] = ld.readBits(j);
			for (int env2 = 0; env2 < bs_num_env; env2++)
			{
				f[ch][bs_num_env - env2 - 1] = ld.readBit();
			}
			abs_bord_lead[ch] = 0;
			abs_bord_trail[ch] = bs_abs_bord;
			n_rel_lead[ch] = 0;
			n_rel_trail[ch] = bs_num_env - 1;
			break;
		}
		case 2:
		{
			int bs_abs_bord2 = ld.readBits(2);
			bs_num_env = ld.readBits(2) + 1;
			for (int rel2 = 0; rel2 < bs_num_env - 1; rel2++)
			{
				bs_rel_bord[ch][rel2] = 2 * ld.readBits(2) + 2;
			}
			int k = sbr_log2(bs_num_env + 1);
			bs_pointer[ch] = ld.readBits(k);
			for (int env3 = 0; env3 < bs_num_env; env3++)
			{
				f[ch][env3] = ld.readBit();
			}
			abs_bord_lead[ch] = bs_abs_bord2;
			abs_bord_trail[ch] = numTimeSlots;
			n_rel_lead[ch] = bs_num_env - 1;
			n_rel_trail[ch] = 0;
			break;
		}
		case 3:
		{
			int bs_abs_bord3 = ld.readBits(2);
			int bs_abs_bord_1 = ld.readBits(2) + numTimeSlots;
			bs_num_rel_0[ch] = ld.readBits(2);
			bs_num_rel_1[ch] = ld.readBits(2);
			bs_num_env = Math.min(5, bs_num_rel_0[ch] + bs_num_rel_1[ch] + 1);
			for (int rel3 = 0; rel3 < bs_num_rel_0[ch]; rel3++)
			{
				bs_rel_bord_0[ch][rel3] = 2 * ld.readBits(2) + 2;
			}
			for (int rel3 = 0; rel3 < bs_num_rel_1[ch]; rel3++)
			{
				bs_rel_bord_1[ch][rel3] = 2 * ld.readBits(2) + 2;
			}
			int l = sbr_log2(bs_num_rel_0[ch] + bs_num_rel_1[ch] + 2);
			bs_pointer[ch] = ld.readBits(l);
			for (int env4 = 0; env4 < bs_num_env; env4++)
			{
				f[ch][env4] = ld.readBit();
			}
			abs_bord_lead[ch] = bs_abs_bord3;
			abs_bord_trail[ch] = bs_abs_bord_1;
			n_rel_lead[ch] = bs_num_rel_0[ch];
			n_rel_trail[ch] = bs_num_rel_1[ch];
			break;
		}
		}
		if (bs_frame_class[ch] == 3)
		{
			L_E[ch] = Math.min(bs_num_env, 5);
		}
		else
		{
			L_E[ch] = Math.min(bs_num_env, 4);
		}
		if (L_E[ch] <= 0)
		{
			return 1;
		}
		if (L_E[ch] > 1)
		{
			L_Q[ch] = 2;
		}
		else
		{
			L_Q[ch] = 1;
		}
		int result;
		if ((result = TFGrid.envelope_time_border_vector(this, ch)) > 0)
		{
			bs_frame_class[ch] = saved_frame_class;
			L_E[ch] = saved_L_E;
			L_Q[ch] = saved_L_Q;
			return result;
		}
		TFGrid.noise_floor_time_border_vector(this, ch);
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 158, 169, 162, 110, 49, 199, 110, 49, 167 })]
	private void sbr_dtdf(IBitStream ld, int ch)
	{
		for (int i = 0; i < L_E[ch]; i++)
		{
			bs_df_env[ch][i] = ld.readBit();
		}
		for (int i = 0; i < L_Q[ch]; i++)
		{
			bs_df_noise[ch][i] = ld.readBit();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 158, 165, 66, 108, 50, 167 })]
	private void invf_mode(IBitStream ld, int ch)
	{
		for (int i = 0; i < N_Q; i++)
		{
			bs_invf_mode[ch][i] = ld.readBits(2);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		158, 154, 130, 163, 118, 140, 143, 109, 99, 107,
		103, 169, 103, 201, 99, 107, 103, 169, 103, 199,
		113, 112, 109, 107, 187, 219, 107, 187, 217, 124,
		59, 233, 70, 121, 59, 233, 39, 234, 95, 106
	})]
	private void sbr_envelope(IBitStream ld, int ch)
	{
		int delta = 0;
		if (L_E[ch] == 1 && bs_frame_class[ch] == 0)
		{
			amp_res[ch] = false;
		}
		else
		{
			amp_res[ch] = bs_amp_res;
		}
		int[][] t_huff;
		int[][] f_huff;
		if (bs_coupling && ch == 1)
		{
			delta = 1;
			if (amp_res[ch])
			{
				t_huff = HuffmanTables.T_HUFFMAN_ENV_BAL_3_0DB;
				f_huff = HuffmanTables.F_HUFFMAN_ENV_BAL_3_0DB;
			}
			else
			{
				t_huff = HuffmanTables.T_HUFFMAN_ENV_BAL_1_5DB;
				f_huff = HuffmanTables.F_HUFFMAN_ENV_BAL_1_5DB;
			}
		}
		else
		{
			delta = 0;
			if (amp_res[ch])
			{
				t_huff = HuffmanTables.T_HUFFMAN_ENV_3_0DB;
				f_huff = HuffmanTables.F_HUFFMAN_ENV_3_0DB;
			}
			else
			{
				t_huff = HuffmanTables.T_HUFFMAN_ENV_1_5DB;
				f_huff = HuffmanTables.F_HUFFMAN_ENV_1_5DB;
			}
		}
		for (int env = 0; env < L_E[ch]; env++)
		{
			if (bs_df_env[ch][env] == 0)
			{
				if (bs_coupling && ch == 1)
				{
					if (amp_res[ch])
					{
						E[ch][0][env] = ld.readBits(5) << delta;
					}
					else
					{
						E[ch][0][env] = ld.readBits(6) << delta;
					}
				}
				else if (amp_res[ch])
				{
					E[ch][0][env] = ld.readBits(6) << delta;
				}
				else
				{
					E[ch][0][env] = ld.readBits(7) << delta;
				}
				for (int band2 = 1; band2 < n[f[ch][env]]; band2++)
				{
					E[ch][band2][env] = decodeHuffman(ld, f_huff) << delta;
				}
			}
			else
			{
				for (int band = 0; band < n[f[ch][env]]; band++)
				{
					E[ch][band][env] = decodeHuffman(ld, t_huff) << delta;
				}
			}
		}
		NoiseEnvelope.extract_envelope_data(this, ch);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		158, 137, 130, 163, 109, 99, 103, 169, 99, 103,
		167, 113, 112, 109, 187, 153, 113, 59, 233, 69,
		110, 59, 233, 51, 234, 83, 106
	})]
	private void sbr_noise(IBitStream ld, int ch)
	{
		int delta = 0;
		int[][] t_huff;
		int[][] f_huff;
		if (bs_coupling && ch == 1)
		{
			delta = 1;
			t_huff = HuffmanTables.T_HUFFMAN_NOISE_BAL_3_0DB;
			f_huff = HuffmanTables.F_HUFFMAN_ENV_BAL_3_0DB;
		}
		else
		{
			delta = 0;
			t_huff = HuffmanTables.T_HUFFMAN_NOISE_3_0DB;
			f_huff = HuffmanTables.F_HUFFMAN_ENV_3_0DB;
		}
		for (int noise = 0; noise < L_Q[ch]; noise++)
		{
			if (bs_df_noise[ch][noise] == 0)
			{
				if (bs_coupling && ch == 1)
				{
					Q[ch][0][noise] = ld.readBits(5) << delta;
				}
				else
				{
					Q[ch][0][noise] = ld.readBits(5) << delta;
				}
				for (int band2 = 1; band2 < N_Q; band2++)
				{
					Q[ch][band2][noise] = decodeHuffman(ld, f_huff) << delta;
				}
			}
			else
			{
				for (int band = 0; band < N_Q; band++)
				{
					Q[ch][band][noise] = decodeHuffman(ld, t_huff) << delta;
				}
			}
		}
		NoiseEnvelope.extract_noise_floor_data(this, ch);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 158, 156, 130, 108, 49, 167 })]
	private void sinusoidal_coding(IBitStream ld, int ch)
	{
		for (int i = 0; i < N_high; i++)
		{
			bs_add_harmonic[ch][i] = ld.readBit();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		158, 163, 66, 143, 105, 152, 105, 141, 174, 118,
		168, 110, 168, 131, 110
	})]
	private int sbr_extension(IBitStream ld, int bs_extension_id, int num_bits_left)
	{
		if (bs_extension_id == 2)
		{
			if (ps == null)
			{
				ps = new PS(sample_rate, numTimeSlotsRate);
			}
			if (psResetFlag)
			{
				ps.header_read = false;
			}
			int ret = ps.decode(ld);
			if (!ps_used && ps.header_read)
			{
				ps_used = true;
			}
			if (ps.header_read)
			{
				psResetFlag = false;
			}
			return ret;
		}
		bs_extension_data = ld.readBits(6);
		return 6;
	}

	[LineNumberTable(new byte[] { 158, 202, 130, 127, 19, 106, 133 })]
	private int sbr_log2(int val)
	{
		int[] log2tab = new int[10] { 0, 0, 1, 2, 2, 3, 3, 3, 3, 4 };
		if (val < 10 && val >= 0)
		{
			return log2tab[val];
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 158, 127, 66, 131, 101, 104, 169 })]
	private int decodeHuffman(IBitStream ld, int[][] t_huff)
	{
		int index = 0;
		while (index >= 0)
		{
			int bit = ld.readBit();
			index = t_huff[index][bit];
		}
		return index + 64;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 112, 161, 68, 131, 168, 100, 159, 3, 159,
		5, 164, 216, 113, 101, 195, 111, 111, 104, 125,
		29, 199, 105, 109, 13, 231, 59, 234, 76, 177,
		111, 105, 105, 171, 105, 105, 169, 109, 127, 2,
		31, 2, 201, 113, 127, 2, 31, 2, 201, 120,
		111, 15, 233, 42, 236, 93
	})]
	private int sbr_process_channel(float[] channel_buf, float[][][] X, int ch, bool dont_process)
	{
		int dont_process2 = (dont_process ? 1 : 0);
		int ret = 0;
		bsco = 0;
		if (dont_process2 != 0)
		{
			qmfa[ch].sbr_qmf_analysis_32(this, channel_buf, Xsbr[ch], tHFGen, 32);
		}
		else
		{
			qmfa[ch].sbr_qmf_analysis_32(this, channel_buf, Xsbr[ch], tHFGen, kx);
		}
		if (dont_process2 == 0)
		{
			HFGeneration.hf_generation(this, Xsbr[ch], Xsbr[ch], ch);
			ret = HFAdjustment.hf_adjustment(this, Xsbr[ch], ch);
			if (ret > 0)
			{
				dont_process2 = 1;
			}
		}
		if (just_seeked || dont_process2 != 0)
		{
			for (int k = 0; k < numTimeSlotsRate; k++)
			{
				for (int i = 0; i < 32; i++)
				{
					X[k][i][0] = Xsbr[ch][k + tHFAdj][i][0];
					X[k][i][1] = Xsbr[ch][k + tHFAdj][i][1];
				}
				for (int i = 32; i < 64; i++)
				{
					X[k][i][0] = 0f;
					X[k][i][1] = 0f;
				}
			}
		}
		else
		{
			for (int l = 0; l < numTimeSlotsRate; l++)
			{
				int kx_band;
				int M_band;
				int bsco_band;
				if (l < t_E[ch][0])
				{
					kx_band = kx_prev;
					M_band = M_prev;
					bsco_band = bsco_prev;
				}
				else
				{
					kx_band = kx;
					M_band = M;
					bsco_band = bsco;
				}
				for (int j = 0; j < kx_band + bsco_band; j++)
				{
					X[l][j][0] = Xsbr[ch][l + tHFAdj][j][0];
					X[l][j][1] = Xsbr[ch][l + tHFAdj][j][1];
				}
				for (int j = kx_band + bsco_band; j < kx_band + M_band; j++)
				{
					X[l][j][0] = Xsbr[ch][l + tHFAdj][j][0];
					X[l][j][1] = Xsbr[ch][l + tHFAdj][j][1];
				}
				for (int j = Math.max(kx_band + bsco_band, kx_band + M_band); j < 64; j++)
				{
					X[l][j][0] = 0f;
					X[l][j][1] = 0f;
				}
			}
		}
		return ret;
	}

	[LineNumberTable(new byte[]
	{
		158, 124, 130, 109, 109, 141, 177, 108, 132, 124,
		104, 127, 1, 31, 1, 231, 69, 104, 53, 167,
		145, 115, 140, 138
	})]
	private int sbr_save_prev_data(int ch)
	{
		kx_prev = kx;
		M_prev = M;
		bsco_prev = bsco;
		L_E_prev[ch] = L_E[ch];
		if (L_E[ch] <= 0)
		{
			return 19;
		}
		f_prev[ch] = f[ch][L_E[ch] - 1];
		for (int i = 0; i < 49; i++)
		{
			E_prev[ch][i] = E[ch][i][L_E[ch] - 1];
			Q_prev[ch][i] = Q[ch][i][L_Q[ch] - 1];
		}
		for (int i = 0; i < 49; i++)
		{
			bs_add_harmonic_prev[ch][i] = bs_add_harmonic[ch][i];
		}
		bs_add_harmonic_flag_prev[ch] = bs_add_harmonic_flag[ch];
		if (l_A[ch] == L_E[ch])
		{
			prevEnvIsShort[ch] = 0;
		}
		else
		{
			prevEnvIsShort[ch] = -1;
		}
		return 0;
	}

	[LineNumberTable(new byte[]
	{
		158, 116, 130, 111, 104, 127, 5, 31, 5, 39,
		234, 70, 109, 104, 116, 20, 39, 231, 70
	})]
	private void sbr_save_matrix(int ch)
	{
		for (int i = 0; i < tHFGen; i++)
		{
			for (int k = 0; k < 64; k++)
			{
				Xsbr[ch][i][k][0] = Xsbr[ch][i + numTimeSlotsRate][k][0];
				Xsbr[ch][i][k][1] = Xsbr[ch][i + numTimeSlotsRate][k][1];
			}
		}
		for (int i = tHFGen; i < 40; i++)
		{
			for (int j = 0; j < 64; j++)
			{
				Xsbr[ch][i][j][0] = 0f;
				Xsbr[ch][i][j][1] = 0f;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 129, 72, 105, 109, 109, 109, 110, 127,
		20, 110, 127, 20, 110, 109, 109, 109, 109, 109,
		109, 109, 127, 19, 127, 19, 127, 19, 109, 127,
		29, 127, 29, 109, 127, 29, 127, 20, 127, 29,
		127, 29, 127, 29, 127, 29, 127, 29, 127, 20,
		109, 109, 127, 19, 127, 19, 127, 20, 127, 20,
		110, 110, 127, 20, 127, 20, 109, 109, 109, 109,
		109, 127, 39, 109, 109, 109, 127, 20, 127, 20,
		127, 20, 109, 109, 109, 109, 109, 127, 20, 191,
		19, 104, 104, 136, 104, 104, 104, 104, 104, 104,
		104, 104, 104, 104, 106, 106, 104, 136, 104, 136,
		104, 104, 168, 136, 100, 105, 171, 105, 169, 106,
		138, 164, 112, 112, 124, 222, 112, 124, 138
	})]
	public SBR(bool smallFrames, bool stereo, SampleFrequency sample_rate, bool downSampledSBR)
	{
		amp_res = new bool[2];
		N_L = new int[4];
		n = new int[2];
		f_master = new int[64];
		int[] array = new int[2];
		int num = (array[1] = 64);
		num = (array[0] = 2);
		f_table_res = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		f_table_noise = new int[64];
		array = new int[2];
		num = (array[1] = 64);
		num = (array[0] = 4);
		f_table_lim = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		table_map_k_to_g = new int[64];
		abs_bord_lead = new int[2];
		abs_bord_trail = new int[2];
		n_rel_lead = new int[2];
		n_rel_trail = new int[2];
		L_E = new int[2];
		L_E_prev = new int[2];
		L_Q = new int[2];
		array = new int[2];
		num = (array[1] = 6);
		num = (array[0] = 2);
		t_E = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 3);
		num = (array[0] = 2);
		t_Q = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 6);
		num = (array[0] = 2);
		f = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		f_prev = new int[2];
		array = new int[3];
		num = (array[2] = 64);
		num = (array[1] = 5);
		num = (array[0] = 2);
		G_temp_prev = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 64);
		num = (array[1] = 5);
		num = (array[0] = 2);
		Q_temp_prev = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		GQ_ringbuf_index = new int[2];
		array = new int[3];
		num = (array[2] = 5);
		num = (array[1] = 64);
		num = (array[0] = 2);
		E = (int[][][])ByteCodeHelper.multianewarray(typeof(int[][][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 64);
		num = (array[0] = 2);
		E_prev = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 5);
		num = (array[1] = 64);
		num = (array[0] = 2);
		E_orig = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 5);
		num = (array[1] = 64);
		num = (array[0] = 2);
		E_curr = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 2);
		num = (array[1] = 64);
		num = (array[0] = 2);
		Q = (int[][][])ByteCodeHelper.multianewarray(typeof(int[][][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 2);
		num = (array[1] = 64);
		num = (array[0] = 2);
		Q_div = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 2);
		num = (array[1] = 64);
		num = (array[0] = 2);
		Q_div2 = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 64);
		num = (array[0] = 2);
		Q_prev = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		l_A = new int[2];
		l_A_prev = new int[2];
		array = new int[2];
		num = (array[1] = 5);
		num = (array[0] = 2);
		bs_invf_mode = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 5);
		num = (array[0] = 2);
		bs_invf_mode_prev = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 64);
		num = (array[0] = 2);
		bwArray = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 64);
		num = (array[0] = 2);
		bwArray_prev = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		patchNoSubbands = new int[64];
		patchStartSubband = new int[64];
		array = new int[2];
		num = (array[1] = 64);
		num = (array[0] = 2);
		bs_add_harmonic = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 64);
		num = (array[0] = 2);
		bs_add_harmonic_prev = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		index_noise_prev = new int[2];
		psi_is_prev = new int[2];
		prevEnvIsShort = new int[2];
		qmfa = new AnalysisFilterbank[2];
		qmfs = new SynthesisFilterbank[2];
		array = new int[4];
		num = (array[3] = 2);
		num = (array[2] = 64);
		num = (array[1] = 40);
		num = (array[0] = 2);
		Xsbr = (float[][][][])ByteCodeHelper.multianewarray(typeof(float[][][][]).TypeHandle, array);
		bs_add_harmonic_flag = new bool[2];
		bs_add_harmonic_flag_prev = new bool[2];
		bs_frame_class = new int[2];
		array = new int[2];
		num = (array[1] = 9);
		num = (array[0] = 2);
		bs_rel_bord = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 9);
		num = (array[0] = 2);
		bs_rel_bord_0 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 9);
		num = (array[0] = 2);
		bs_rel_bord_1 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		bs_pointer = new int[2];
		bs_abs_bord_0 = new int[2];
		bs_abs_bord_1 = new int[2];
		bs_num_rel_0 = new int[2];
		bs_num_rel_1 = new int[2];
		array = new int[2];
		num = (array[1] = 9);
		num = (array[0] = 2);
		bs_df_env = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 3);
		num = (array[0] = 2);
		bs_df_noise = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		this.downSampledSBR = downSampledSBR;
		this.stereo = stereo;
		this.sample_rate = sample_rate;
		bs_freq_scale = 2;
		bs_alter_scale = true;
		bs_noise_bands = 2;
		bs_limiter_bands = 2;
		bs_limiter_gains = 2;
		bs_interpol_freq = true;
		bs_smoothing_mode = true;
		bs_start_freq = 5;
		bs_amp_res = true;
		bs_samplerate_mode = 1;
		prevEnvIsShort[0] = -1;
		prevEnvIsShort[1] = -1;
		header_count = 0;
		Reset = true;
		tHFGen = 8;
		tHFAdj = 2;
		bsco = 0;
		bsco_prev = 0;
		M_prev = 0;
		bs_start_freq_prev = -1;
		if (smallFrames)
		{
			numTimeSlotsRate = 30;
			numTimeSlots = 15;
		}
		else
		{
			numTimeSlotsRate = 32;
			numTimeSlots = 16;
		}
		GQ_ringbuf_index[0] = 0;
		GQ_ringbuf_index[1] = 0;
		if (stereo)
		{
			qmfa[0] = new AnalysisFilterbank(32);
			qmfa[1] = new AnalysisFilterbank(32);
			SynthesisFilterbank[] array2 = qmfs;
			SynthesisFilterbank.___003Cclinit_003E();
			array2[0] = new SynthesisFilterbank((!downSampledSBR) ? 64 : 32);
			SynthesisFilterbank[] array3 = qmfs;
			SynthesisFilterbank.___003Cclinit_003E();
			array3[1] = new SynthesisFilterbank((!downSampledSBR) ? 64 : 32);
		}
		else
		{
			qmfa[0] = new AnalysisFilterbank(32);
			SynthesisFilterbank[] array4 = qmfs;
			SynthesisFilterbank.___003Cclinit_003E();
			array4[0] = new SynthesisFilterbank((!downSampledSBR) ? 64 : 32);
			qmfs[1] = null;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		72,
		162,
		120,
		120,
		120,
		152,
		106,
		127,
		2,
		127,
		2,
		127,
		2,
		byte.MaxValue,
		2,
		60,
		234,
		71,
		107,
		104,
		116,
		116,
		116,
		244,
		60,
		39,
		234,
		73,
		106,
		106,
		104,
		136,
		106,
		106,
		104,
		104,
		104,
		104,
		104,
		104,
		104,
		104,
		104,
		104,
		106,
		106,
		104,
		104,
		104,
		136,
		106,
		106,
		104,
		108,
		108,
		108,
		108,
		108,
		236,
		58,
		231,
		72,
		106,
		106
	})]
	internal virtual void sbrReset()
	{
		if (qmfa[0] != null)
		{
			qmfa[0].reset();
		}
		if (qmfa[1] != null)
		{
			qmfa[1].reset();
		}
		if (qmfs[0] != null)
		{
			qmfs[0].reset();
		}
		if (qmfs[1] != null)
		{
			qmfs[1].reset();
		}
		int j;
		for (j = 0; j < 5; j++)
		{
			if (G_temp_prev[0][j] != null)
			{
				Arrays.fill(G_temp_prev[0][j], 0f);
			}
			if (G_temp_prev[1][j] != null)
			{
				Arrays.fill(G_temp_prev[1][j], 0f);
			}
			if (Q_temp_prev[0][j] != null)
			{
				Arrays.fill(Q_temp_prev[0][j], 0f);
			}
			if (Q_temp_prev[1][j] != null)
			{
				Arrays.fill(Q_temp_prev[1][j], 0f);
			}
		}
		for (int i = 0; i < 40; i++)
		{
			for (int k = 0; k < 64; k++)
			{
				Xsbr[0][i][j][0] = 0f;
				Xsbr[0][i][j][1] = 0f;
				Xsbr[1][i][j][0] = 0f;
				Xsbr[1][i][j][1] = 0f;
			}
		}
		GQ_ringbuf_index[0] = 0;
		GQ_ringbuf_index[1] = 0;
		header_count = 0;
		Reset = true;
		L_E_prev[0] = 0;
		L_E_prev[1] = 0;
		bs_freq_scale = 2;
		bs_alter_scale = true;
		bs_noise_bands = 2;
		bs_limiter_bands = 2;
		bs_limiter_gains = 2;
		bs_interpol_freq = true;
		bs_smoothing_mode = true;
		bs_start_freq = 5;
		bs_amp_res = true;
		bs_samplerate_mode = 1;
		prevEnvIsShort[0] = -1;
		prevEnvIsShort[1] = -1;
		bsco = 0;
		bsco_prev = 0;
		M_prev = 0;
		bs_start_freq_prev = -1;
		f_prev[0] = 0;
		f_prev[1] = 0;
		for (j = 0; j < 49; j++)
		{
			E_prev[0][j] = 0;
			Q_prev[0][j] = 0;
			E_prev[1][j] = 0;
			Q_prev[1][j] = 0;
			bs_add_harmonic_prev[0][j] = 0;
			bs_add_harmonic_prev[1][j] = 0;
		}
		bs_add_harmonic_flag_prev[0] = false;
		bs_add_harmonic_flag_prev[1] = false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159,
		41,
		98,
		99,
		99,
		233,
		72,
		137,
		102,
		207,
		105,
		105,
		105,
		105,
		105,
		137,
		141,
		105,
		168,
		199,
		108,
		121,
		byte.MaxValue,
		14,
		69,
		102,
		244,
		70,
		100,
		233,
		72,
		157,
		246,
		74,
		163,
		173,
		104,
		241,
		78,
		136,
		101,
		105,
		136,
		169
	})]
	public virtual int decode(IBitStream ld, int cnt)
	{
		int result = 0;
		int num_align_bits = 0;
		long num_sbr_bits1 = ld.getPosition();
		int bs_extension_type = ld.readBits(4);
		if (bs_extension_type == 14)
		{
			bs_sbr_crc_bits = ld.readBits(10);
		}
		int saved_start_freq = bs_start_freq;
		int saved_samplerate_mode = bs_samplerate_mode;
		int saved_stop_freq = bs_stop_freq;
		int saved_freq_scale = bs_freq_scale;
		int saved_alter_scale = (bs_alter_scale ? 1 : 0);
		int saved_xover_band = bs_xover_band;
		bs_header_flag = ld.readBool();
		if (bs_header_flag)
		{
			sbr_header(ld);
		}
		sbr_reset();
		if (header_count != 0)
		{
			if (Reset || (bs_header_flag && just_seeked))
			{
				int rt = calc_sbr_tables(bs_start_freq, bs_stop_freq, bs_samplerate_mode, bs_freq_scale, bs_alter_scale, bs_xover_band);
				if (rt > 0)
				{
					calc_sbr_tables(saved_start_freq, saved_stop_freq, saved_samplerate_mode, saved_freq_scale, (byte)saved_alter_scale != 0, saved_xover_band);
				}
			}
			if (result == 0)
			{
				result = sbr_data(ld);
				if (result > 0 && (Reset || (bs_header_flag && just_seeked)))
				{
					calc_sbr_tables(saved_start_freq, saved_stop_freq, saved_samplerate_mode, saved_freq_scale, (byte)saved_alter_scale != 0, saved_xover_band);
				}
			}
		}
		else
		{
			result = 1;
		}
		int num_sbr_bits2 = (int)(ld.getPosition() - num_sbr_bits1);
		if (8 * cnt < num_sbr_bits2)
		{
			
			throw new AACException("frame overread");
		}
		for (num_align_bits = 8 * cnt - num_sbr_bits2; num_align_bits > 7; num_align_bits += -8)
		{
			ld.readBits(8);
		}
		ld.readBits(num_align_bits);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 94, 97, 67, 99, 99, 191, 21, 140, 145,
		163, 113, 168, 100, 170, 168, 153, 105, 180, 178,
		153, 105, 180, 178, 105, 136, 113, 105, 102, 105,
		166, 104, 136, 143
	})]
	public virtual int _process(float[] left_chan, float[] right_chan, bool just_seeked)
	{
		int dont_process = 0;
		int ret = 0;
		int[] array = new int[3];
		int num = (array[2] = 2);
		num = (array[1] = 64);
		num = (array[0] = 32);
		float[][][] X = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		if (!stereo)
		{
			return 21;
		}
		if (this.ret != 0 || header_count == 0)
		{
			dont_process = 1;
			if (this.ret != 0 && Reset)
			{
				bs_start_freq_prev = -1;
			}
		}
		if (just_seeked)
		{
			this.just_seeked = true;
		}
		else
		{
			this.just_seeked = false;
		}
		this.ret += sbr_process_channel(left_chan, X, 0, (byte)dont_process != 0);
		if (downSampledSBR)
		{
			qmfs[0].sbr_qmf_synthesis_32(this, X, left_chan);
		}
		else
		{
			qmfs[0].sbr_qmf_synthesis_64(this, X, left_chan);
		}
		this.ret += sbr_process_channel(right_chan, X, 1, (byte)dont_process != 0);
		if (downSampledSBR)
		{
			qmfs[1].sbr_qmf_synthesis_32(this, X, right_chan);
		}
		else
		{
			qmfs[1].sbr_qmf_synthesis_64(this, X, right_chan);
		}
		if (bs_header_flag)
		{
			this.just_seeked = false;
		}
		if (header_count != 0 && this.ret == 0)
		{
			ret = sbr_save_prev_data(0);
			if (ret != 0)
			{
				return ret;
			}
			ret = sbr_save_prev_data(1);
			if (ret != 0)
			{
				return ret;
			}
		}
		sbr_save_matrix(0);
		sbr_save_matrix(1);
		frame++;
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 79, 129, 67, 99, 99, 191, 21, 140, 145,
		163, 113, 168, 100, 170, 168, 153, 105, 180, 178,
		105, 136, 113, 105, 166, 136, 143
	})]
	public virtual int process(float[] channel, bool just_seeked)
	{
		int dont_process = 0;
		int ret = 0;
		int[] array = new int[3];
		int num = (array[2] = 2);
		num = (array[1] = 64);
		num = (array[0] = 32);
		float[][][] X = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		if (stereo)
		{
			return 21;
		}
		if (this.ret != 0 || header_count == 0)
		{
			dont_process = 1;
			if (this.ret != 0 && Reset)
			{
				bs_start_freq_prev = -1;
			}
		}
		if (just_seeked)
		{
			this.just_seeked = true;
		}
		else
		{
			this.just_seeked = false;
		}
		this.ret += sbr_process_channel(channel, X, 0, (byte)dont_process != 0);
		if (downSampledSBR)
		{
			qmfs[0].sbr_qmf_synthesis_32(this, X, channel);
		}
		else
		{
			qmfs[0].sbr_qmf_synthesis_64(this, X, channel);
		}
		if (bs_header_flag)
		{
			this.just_seeked = false;
		}
		if (header_count != 0 && this.ret == 0)
		{
			ret = sbr_save_prev_data(0);
			if (ret != 0)
			{
				return ret;
			}
		}
		sbr_save_matrix(0);
		frame++;
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 66, 65, 67, 99, 99, 127, 21, 191, 21,
		140, 145, 163, 113, 168, 100, 170, 168, 107, 191,
		2, 185, 120, 105, 127, 3, 31, 3, 41, 236,
		72, 177, 105, 114, 180, 114, 178, 105, 136, 113,
		105, 166, 136, 143
	})]
	public virtual int processPS(float[] left_channel, float[] right_channel, bool just_seeked)
	{
		int dont_process = 0;
		int ret = 0;
		int[] array = new int[3];
		int num = (array[2] = 2);
		num = (array[1] = 64);
		num = (array[0] = 38);
		float[][][] X_left = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 2);
		num = (array[1] = 64);
		num = (array[0] = 38);
		float[][][] X_right = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		if (stereo)
		{
			return 21;
		}
		if (this.ret != 0 || header_count == 0)
		{
			dont_process = 1;
			if (this.ret != 0 && Reset)
			{
				bs_start_freq_prev = -1;
			}
		}
		if (just_seeked)
		{
			this.just_seeked = true;
		}
		else
		{
			this.just_seeked = false;
		}
		if (qmfs[1] == null)
		{
			SynthesisFilterbank[] array2 = qmfs;
			SynthesisFilterbank.___003Cclinit_003E();
			array2[1] = new SynthesisFilterbank((!downSampledSBR) ? 64 : 32);
		}
		this.ret += sbr_process_channel(left_channel, X_left, 0, (byte)dont_process != 0);
		for (int j = numTimeSlotsRate; j < numTimeSlotsRate + 6; j++)
		{
			for (int i = 0; i < 5; i++)
			{
				X_left[j][i][0] = Xsbr[0][tHFAdj + j][i][0];
				X_left[j][i][1] = Xsbr[0][tHFAdj + j][i][1];
			}
		}
		ps.process(X_left, X_right);
		if (downSampledSBR)
		{
			qmfs[0].sbr_qmf_synthesis_32(this, X_left, left_channel);
			qmfs[1].sbr_qmf_synthesis_32(this, X_right, right_channel);
		}
		else
		{
			qmfs[0].sbr_qmf_synthesis_64(this, X_left, left_channel);
			qmfs[1].sbr_qmf_synthesis_64(this, X_right, right_channel);
		}
		if (bs_header_flag)
		{
			this.just_seeked = false;
		}
		if (header_count != 0 && this.ret == 0)
		{
			ret = sbr_save_prev_data(0);
			if (ret != 0)
			{
				return ret;
			}
		}
		sbr_save_matrix(0);
		frame++;
		return 0;
	}

	[LineNumberTable(1395)]
	public virtual bool isPSUsed()
	{
		return ps_used;
	}
}
