using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace net.sourceforge.jaad.aac.sbr;

[Implements(new string[] { "net.sourceforge.jaad.aac.sbr.SBRConstants", "net.sourceforge.jaad.aac.sbr.NoiseTable" })]
internal class HFAdjustment : Object, SBRConstants, NoiseTable
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] h_smooth;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] phi_re;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] phi_im;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] limGain;

	private const float EPS = 1E-12f;

	private float[][] G_lim_boost;

	private float[][] Q_M_lim_boost;

	private float[][] S_M_boost;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 130, 105, 127, 12, 127, 12, 127, 12 })]
	public HFAdjustment()
	{
		int[] array = new int[2];
		int num = (array[1] = 49);
		num = (array[0] = 5);
		G_lim_boost = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 49);
		num = (array[0] = 5);
		Q_M_lim_boost = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 49);
		num = (array[0] = 5);
		S_M_boost = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
	}

	[LineNumberTable(new byte[]
	{
		159, 116, 66, 108, 177, 108, 142, 134, 105, 135,
		113, 136, 119, 63, 46, 236, 69, 243, 56, 236,
		53, 234, 88, 115, 125, 120, 154, 142, 136, 110,
		144, 143, 106, 136, 121, 107, 63, 18, 41, 236,
		71, 252, 45, 236, 60, 44, 236, 94
	})]
	private static int estimate_current_envelope(SBR sbr, HFAdjustment adj, float[][][] Xsbr, int ch)
	{
		if (sbr.bs_interpol_freq)
		{
			for (int n = 0; n < sbr.L_E[ch]; n++)
			{
				int l_i2 = sbr.t_E[ch][n];
				int u_i2 = sbr.t_E[ch][n + 1];
				float div2 = u_i2 - l_i2;
				if (div2 == 0f)
				{
					div2 = 1f;
				}
				for (int m2 = 0; m2 < sbr.M; m2++)
				{
					float nrg2 = 0f;
					for (int j = l_i2 + sbr.tHFAdj; j < u_i2 + sbr.tHFAdj; j++)
					{
						nrg2 += Xsbr[j][m2 + sbr.kx][0] * Xsbr[j][m2 + sbr.kx][0] + Xsbr[j][m2 + sbr.kx][1] * Xsbr[j][m2 + sbr.kx][1];
					}
					sbr.E_curr[ch][m2][n] = nrg2 / div2;
				}
			}
		}
		else
		{
			for (int m = 0; m < sbr.L_E[ch]; m++)
			{
				for (int p = 0; p < sbr.n[sbr.f[ch][m]]; p++)
				{
					int k_l = sbr.f_table_res[sbr.f[ch][m]][p];
					int k_h = sbr.f_table_res[sbr.f[ch][m]][p + 1];
					for (int l = k_l; l < k_h; l++)
					{
						float nrg = 0f;
						int l_i = sbr.t_E[ch][m];
						int u_i = sbr.t_E[ch][m + 1];
						float div = (u_i - l_i) * (k_h - k_l);
						if (div == 0f)
						{
							div = 1f;
						}
						for (int i = l_i + sbr.tHFAdj; i < u_i + sbr.tHFAdj; i++)
						{
							for (int k = k_l; k < k_h; k++)
							{
								nrg += Xsbr[i][k][0] * Xsbr[i][k][0] + Xsbr[i][k][1] * Xsbr[i][k][1];
							}
						}
						sbr.E_curr[ch][l - sbr.kx][m] = nrg / div;
					}
				}
			}
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 76, 130, 163, 105, 137, 137, 115, 100, 100,
		100, 132, 159, 8, 142, 124, 165, 152, 104, 104,
		104, 196, 115, 213, 110, 127, 4, 135, 117, 245,
		59, 236, 77, 127, 6, 143, 238, 71, 151, 231,
		69, 159, 4, 231, 69, 238, 69, 153, 231, 72,
		100, 191, 5, 127, 8, 238, 69, 208, 240, 71,
		245, 70, 101, 172, 183, 235, 72, 127, 9, 110,
		107, 102, 233, 69, 103, 103, 169, 111, 231, 69,
		123, 113, 235, 159, 156, 236, 160, 104, 119, 143,
		142, 124, 156, 108, 190, 242, 55, 236, 159, 119,
		236, 50, 236, 160, 165
	})]
	private static void calculate_gain(SBR sbr, HFAdjustment adj, int ch)
	{
		int current_t_noise_band = 0;
		float[] Q_M_lim = new float[49];
		float[] G_lim = new float[49];
		float[] S_M = new float[49];
		for (int j = 0; j < sbr.L_E[ch]; j++)
		{
			int current_f_noise_band = 0;
			int current_res_band = 0;
			int current_res_band2 = 0;
			int current_hi_res_band = 0;
			float delta = ((j != sbr.l_A[ch] && j != sbr.prevEnvIsShort[ch]) ? 1f : 0f);
			int S_mapped = get_S_mapped(sbr, ch, j, current_res_band2);
			if (sbr.t_E[ch][j + 1] > sbr.t_Q[ch][current_t_noise_band + 1])
			{
				current_t_noise_band++;
			}
			for (int i = 0; i < sbr.N_L[sbr.bs_limiter_bands]; i++)
			{
				float den = 0f;
				float acc1 = 0f;
				float acc2 = 0f;
				int current_res_band_size = 0;
				int ml1 = sbr.f_table_lim[sbr.bs_limiter_bands][i];
				int ml2 = sbr.f_table_lim[sbr.bs_limiter_bands][i + 1];
				for (int k = ml1; k < ml2; k++)
				{
					if (k + sbr.kx == sbr.f_table_res[sbr.f[ch][j]][current_res_band + 1])
					{
						current_res_band++;
					}
					acc1 += sbr.E_orig[ch][current_res_band][j];
					acc2 += sbr.E_curr[ch][k][j];
				}
				float G_max = (1E-12f + acc1) / (1E-12f + acc2) * limGain[sbr.bs_limiter_gains];
				G_max = Math.min(G_max, 1E+10f);
				for (int k = ml1; k < ml2; k++)
				{
					if (k + sbr.kx == sbr.f_table_noise[current_f_noise_band + 1])
					{
						current_f_noise_band++;
					}
					if (k + sbr.kx == sbr.f_table_res[sbr.f[ch][j]][current_res_band2 + 1])
					{
						current_res_band2++;
						S_mapped = get_S_mapped(sbr, ch, j, current_res_band2);
					}
					if (k + sbr.kx == sbr.f_table_res[1][current_hi_res_band + 1])
					{
						current_hi_res_band++;
					}
					int S_index_mapped = 0;
					if ((j >= sbr.l_A[ch] || (sbr.bs_add_harmonic_prev[ch][current_hi_res_band] != 0 && sbr.bs_add_harmonic_flag_prev[ch])) && k + sbr.kx == sbr.f_table_res[1][current_hi_res_band + 1] + sbr.f_table_res[1][current_hi_res_band] >> 1)
					{
						S_index_mapped = sbr.bs_add_harmonic[ch][current_hi_res_band];
					}
					float Q_div = sbr.Q_div[ch][current_f_noise_band][current_t_noise_band];
					float Q_div2 = sbr.Q_div2[ch][current_f_noise_band][current_t_noise_band];
					float Q_M = sbr.E_orig[ch][current_res_band2][j] * Q_div2;
					if (S_index_mapped == 0)
					{
						S_M[k] = 0f;
					}
					else
					{
						S_M[k] = sbr.E_orig[ch][current_res_band2][j] * Q_div;
						den += S_M[k];
					}
					float G = sbr.E_orig[ch][current_res_band2][j] / (1f + sbr.E_curr[ch][k][j]);
					if (S_mapped == 0 && delta == 1f)
					{
						G *= Q_div;
					}
					else if (S_mapped == 1)
					{
						G *= Q_div2;
					}
					if (G_max > G)
					{
						Q_M_lim[k] = Q_M;
						G_lim[k] = G;
					}
					else
					{
						Q_M_lim[k] = Q_M * G_max / G;
						G_lim[k] = G_max;
					}
					den += sbr.E_curr[ch][k][j] * G_lim[k];
					if (S_index_mapped == 0 && j != sbr.l_A[ch])
					{
						den += Q_M_lim[k];
					}
				}
				float G_boost = (acc1 + 1E-12f) / (den + 1E-12f);
				G_boost = Math.min(G_boost, 2.51188636f);
				for (int k = ml1; k < ml2; k++)
				{
					adj.G_lim_boost[j][k] = (float)Math.sqrt(G_lim[k] * G_boost);
					adj.Q_M_lim_boost[j][k] = (float)Math.sqrt(Q_M_lim[k] * G_boost);
					if (S_M[k] != 0f)
					{
						adj.S_M_boost[j][k] = (float)Math.sqrt(S_M[k] * G_boost);
					}
					else
					{
						adj.S_M_boost[j][k] = 0f;
					}
				}
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		101,
		130,
		99,
		99,
		227,
		70,
		105,
		99,
		165,
		138,
		138,
		113,
		157,
		111,
		140,
		100,
		105,
		127,
		2,
		31,
		2,
		233,
		69,
		106,
		163,
		159,
		1,
		127,
		8,
		159,
		8,
		113,
		137,
		104,
		136,
		104,
		107,
		108,
		107,
		103,
		102,
		104,
		121,
		249,
		58,
		236,
		72,
		131,
		119,
		183,
		191,
		3,
		203,
		159,
		35,
		116,
		125,
		223,
		35,
		116,
		122,
		159,
		12,
		127,
		0,
		byte.MaxValue,
		12,
		22,
		236,
		110,
		167,
		120,
		108,
		234,
		8,
		236,
		48,
		234,
		160,
		76,
		106,
		106
	})]
	private static void hf_assembly(SBR sbr, HFAdjustment adj, float[][][] Xsbr, int ch)
	{
		int fIndexNoise = 0;
		int fIndexSine = 0;
		int assembly_reset = 0;
		if (sbr.Reset)
		{
			assembly_reset = 1;
			fIndexNoise = 0;
		}
		else
		{
			fIndexNoise = sbr.index_noise_prev[ch];
		}
		fIndexSine = sbr.psi_is_prev[ch];
		for (int j = 0; j < sbr.L_E[ch]; j++)
		{
			int no_noise = ((j == sbr.l_A[ch] || j == sbr.prevEnvIsShort[ch]) ? 1 : 0);
			int h_SL = ((!sbr.bs_smoothing_mode) ? 4 : 0);
			h_SL = ((no_noise == 0) ? h_SL : 0);
			if (assembly_reset != 0)
			{
				for (int m = 0; m < 4; m++)
				{
					ByteCodeHelper.arraycopy_primitive_4(adj.G_lim_boost[j], 0, sbr.G_temp_prev[ch][m], 0, sbr.M);
					ByteCodeHelper.arraycopy_primitive_4(adj.Q_M_lim_boost[j], 0, sbr.Q_temp_prev[ch][m], 0, sbr.M);
				}
				sbr.GQ_ringbuf_index[ch] = 4;
				assembly_reset = 0;
			}
			for (int i = sbr.t_E[ch][j]; i < sbr.t_E[ch][j + 1]; i++)
			{
				ByteCodeHelper.arraycopy_primitive_4(adj.G_lim_boost[j], 0, sbr.G_temp_prev[ch][sbr.GQ_ringbuf_index[ch]], 0, sbr.M);
				ByteCodeHelper.arraycopy_primitive_4(adj.Q_M_lim_boost[j], 0, sbr.Q_temp_prev[ch][sbr.GQ_ringbuf_index[ch]], 0, sbr.M);
				int num;
				for (int k = 0; k < sbr.M; k++)
				{
					float[] psi = new float[2];
					float G_filt = 0f;
					float Q_filt = 0f;
					if (h_SL != 0)
					{
						int ri = sbr.GQ_ringbuf_index[ch];
						for (int l = 0; l <= 4; l++)
						{
							float curr_h_smooth = h_smooth[l];
							ri++;
							if (ri >= 5)
							{
								ri += -5;
							}
							G_filt += sbr.G_temp_prev[ch][ri][k] * curr_h_smooth;
							Q_filt += sbr.Q_temp_prev[ch][ri][k] * curr_h_smooth;
						}
					}
					else
					{
						G_filt = sbr.G_temp_prev[ch][sbr.GQ_ringbuf_index[ch]][k];
						Q_filt = sbr.Q_temp_prev[ch][sbr.GQ_ringbuf_index[ch]][k];
					}
					Q_filt = ((adj.S_M_boost[j][k] == 0f && no_noise == 0) ? Q_filt : 0f);
					fIndexNoise = (fIndexNoise + 1) & 0x1FF;
					Xsbr[i + sbr.tHFAdj][k + sbr.kx][0] = G_filt * Xsbr[i + sbr.tHFAdj][k + sbr.kx][0] + Q_filt * NoiseTable.NOISE_TABLE[fIndexNoise][0];
					if (sbr.bs_extension_id == 3 && sbr.bs_extension_data == 42)
					{
						Xsbr[i + sbr.tHFAdj][k + sbr.kx][0] = 1.642832E+07f;
					}
					Xsbr[i + sbr.tHFAdj][k + sbr.kx][1] = G_filt * Xsbr[i + sbr.tHFAdj][k + sbr.kx][1] + Q_filt * NoiseTable.NOISE_TABLE[fIndexNoise][1];
					int rev = ((((k + sbr.kx) & 1) == 0) ? 1 : (-1));
					psi[0] = adj.S_M_boost[j][k] * (float)phi_re[fIndexSine];
					float[] obj = Xsbr[i + sbr.tHFAdj][k + sbr.kx];
					num = 0;
					float[] array = obj;
					array[num] += psi[0];
					psi[1] = (float)rev * adj.S_M_boost[j][k] * (float)phi_im[fIndexSine];
					float[] obj2 = Xsbr[i + sbr.tHFAdj][k + sbr.kx];
					num = 1;
					array = obj2;
					array[num] += psi[1];
				}
				fIndexSine = (fIndexSine + 1) & 3;
				int[] gQ_ringbuf_index = sbr.GQ_ringbuf_index;
				num = ch;
				int[] array2 = gQ_ringbuf_index;
				array2[num]++;
				if (sbr.GQ_ringbuf_index[ch] >= 5)
				{
					sbr.GQ_ringbuf_index[ch] = 0;
				}
			}
		}
		sbr.index_noise_prev[ch] = fIndexNoise;
		sbr.psi_is_prev[ch] = fIndexSine;
	}

	[LineNumberTable(new byte[]
	{
		159, 127, 162, 206, 159, 9, 236, 77, 148, 182,
		103, 159, 3, 110, 227, 60, 231, 73
	})]
	private static int get_S_mapped(SBR sbr, int ch, int l, int current_band)
	{
		if (sbr.f[ch][l] == 1)
		{
			if (l >= sbr.l_A[ch] || (sbr.bs_add_harmonic_prev[ch][current_band] != 0 && sbr.bs_add_harmonic_flag_prev[ch]))
			{
				return sbr.bs_add_harmonic[ch][current_band];
			}
		}
		else
		{
			int lb = 2 * current_band - ((((uint)sbr.N_high & (true ? 1u : 0u)) != 0) ? 1 : 0);
			int ub = 2 * (current_band + 1) - ((((uint)sbr.N_high & (true ? 1u : 0u)) != 0) ? 1 : 0);
			for (int b = lb; b < ub; b++)
			{
				if ((l >= sbr.l_A[ch] || (sbr.bs_add_harmonic_prev[ch][b] != 0 && sbr.bs_add_harmonic_flag_prev[ch])) && sbr.bs_add_harmonic[ch][b] == 1)
				{
					return 1;
				}
			}
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 98, 103, 131, 107, 143, 108, 108, 149,
		172, 107, 140, 188, 107, 135, 137, 138
	})]
	public static int hf_adjustment(SBR sbr, float[][][] Xsbr, int ch)
	{
		HFAdjustment adj = new HFAdjustment();
		int ret = 0;
		if (sbr.bs_frame_class[ch] == 0)
		{
			sbr.l_A[ch] = -1;
		}
		else if (sbr.bs_frame_class[ch] == 2)
		{
			if (sbr.bs_pointer[ch] > 1)
			{
				sbr.l_A[ch] = sbr.bs_pointer[ch] - 1;
			}
			else
			{
				sbr.l_A[ch] = -1;
			}
		}
		else if (sbr.bs_pointer[ch] == 0)
		{
			sbr.l_A[ch] = -1;
		}
		else
		{
			sbr.l_A[ch] = sbr.L_E[ch] + 1 - sbr.bs_pointer[ch];
		}
		ret = estimate_current_envelope(sbr, adj, Xsbr, ch);
		if (ret > 0)
		{
			return 1;
		}
		calculate_gain(sbr, adj, ch);
		hf_assembly(sbr, adj, Xsbr, ch);
		return 0;
	}

	[LineNumberTable(new byte[]
	{
		159,
		139,
		98,
		byte.MaxValue,
		21,
		69,
		124,
		124
	})]
	static HFAdjustment()
	{
		h_smooth = new float[5]
		{
			0.0318305f,
			0.115163833f,
			0.2181695f,
			0.301502824f,
			1f / 3f
		};
		phi_re = new int[4] { 1, 0, -1, 0 };
		phi_im = new int[4] { 0, 1, 0, -1 };
		limGain = new float[4] { 0.5f, 1f, 2f, 1E+10f };
	}
}
