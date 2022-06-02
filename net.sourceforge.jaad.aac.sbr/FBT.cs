using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;

namespace net.sourceforge.jaad.aac.sbr;

internal class FBT : Object, SBRConstants
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] stopMinTable;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[][] STOP_OFFSET_TABLE;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] limiterBandsCompare;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 66, 113, 141 })]
	public static int find_bands(int warp, int bands, int a0, int a1)
	{
		float div = (float)Math.log(2.0);
		if (warp != 0)
		{
			div *= 1.3f;
		}
		return ByteCodeHelper.d2i((double)bands * Math.log((float)a1 / (float)a0) / (double)div + 0.5);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(139)]
	public static float find_initial_power(int bands, int a0, int a1)
	{
		return (float)Math.pow((float)a1 / (float)a0, 1f / (float)bands);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(13)]
	internal FBT()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 110, 142, 100, 205 })]
	public static int qmf_start_channel(int bs_start_freq, int bs_samplerate_mode, SampleFrequency sample_rate)
	{
		int startMin = SBRConstants.startMinTable[sample_rate.getIndex()];
		int offsetIndex = SBRConstants.offsetIndexTable[sample_rate.getIndex()];
		if (bs_samplerate_mode != 0)
		{
			return startMin + SBRConstants.OFFSET[offsetIndex][bs_start_freq];
		}
		return startMin + SBRConstants.OFFSET[6][bs_start_freq];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 102, 142, 102, 206, 174 })]
	public static int qmf_stop_channel(int bs_stop_freq, SampleFrequency sample_rate, int k0)
	{
		switch (bs_stop_freq)
		{
		case 15:
		{
			int result3 = Math.min(64, k0 * 3);
			
			return result3;
		}
		case 14:
		{
			int result2 = Math.min(64, k0 * 2);
			
			return result2;
		}
		default:
		{
			int stopMin = stopMinTable[sample_rate.getIndex()];
			int result = Math.min(64, stopMin + STOP_OFFSET_TABLE[sample_rate.getIndex()][Math.min(bs_stop_freq, 13)]);
			
			return result;
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 161, 67, 169, 101, 104, 163, 137, 100,
		173, 137, 106, 101, 131, 104, 103, 105, 38, 201,
		104, 108, 142, 101, 117, 104, 202, 106, 105, 60,
		201, 104, 148
	})]
	public static int master_frequency_table_fs0(SBR sbr, int k0, int k2, bool bs_alter_scale)
	{
		int[] vDk = new int[64];
		if (k2 <= k0)
		{
			sbr.N_master = 0;
			return 1;
		}
		int dk = ((!bs_alter_scale) ? 1 : 2);
		int nrBands = ((!bs_alter_scale) ? (k2 - k0 >> 1 << 1) : (k2 - k0 + 2 >> 2 << 1));
		nrBands = Math.min(nrBands, 63);
		if (nrBands <= 0)
		{
			return 1;
		}
		int k2Achieved = k0 + nrBands * dk;
		int k2Diff = k2 - k2Achieved;
		for (int i = 0; i < nrBands; i++)
		{
			vDk[i] = dk;
		}
		if (k2Diff != 0)
		{
			int incr = ((k2Diff <= 0) ? 1 : (-1));
			int i = ((k2Diff > 0) ? (nrBands - 1) : 0);
			for (; k2Diff != 0; k2Diff += incr)
			{
				int num = i;
				int[] array = vDk;
				array[num] -= incr;
				i += incr;
			}
		}
		sbr.f_master[0] = k0;
		for (int i = 1; i <= nrBands; i++)
		{
			sbr.f_master[i] = sbr.f_master[i - 1] + vDk[i - 1];
		}
		sbr.N_master = nrBands;
		sbr.N_master = Math.min(sbr.N_master, 64);
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 162, 113, 113, 245, 69, 101, 104, 163,
		137, 115, 100, 168, 100, 164, 112, 108, 102, 131,
		109, 101, 113, 106, 101, 105, 113, 234, 60, 233,
		73, 138, 101, 106, 114, 105, 227, 61, 233, 70,
		101, 106, 46, 201, 105, 116, 163, 112, 140, 109,
		102, 113, 108, 101, 105, 113, 234, 60, 233, 71,
		236, 69, 108, 109, 106, 240, 69, 106, 102, 106,
		114, 105, 227, 61, 233, 70, 108, 116, 106, 46,
		169, 113, 49, 201
	})]
	public static int master_frequency_table(SBR sbr, int k0, int k2, int bs_freq_scale, bool bs_alter_scale)
	{
		int[] vDk0 = new int[64];
		int[] vDk1 = new int[64];
		int[] vk0 = new int[64];
		int[] vk1 = new int[64];
		int[] temp1 = new int[3] { 6, 5, 4 };
		if (k2 <= k0)
		{
			sbr.N_master = 0;
			return 1;
		}
		int bands = temp1[bs_freq_scale - 1];
		int twoRegions;
		int k3;
		if ((double)((float)k2 / (float)k0) > 2.2449)
		{
			twoRegions = 1;
			k3 = k0 << 1;
		}
		else
		{
			twoRegions = 0;
			k3 = k2;
		}
		int nrBand0 = 2 * find_bands(0, bands, k0, k3);
		nrBand0 = Math.min(nrBand0, 63);
		if (nrBand0 <= 0)
		{
			return 1;
		}
		float q = find_initial_power(nrBand0, k0, k3);
		float qk = k0;
		int A_2 = ByteCodeHelper.f2i(qk + 0.5f);
		for (int i = 0; i <= nrBand0; i++)
		{
			int A_ = A_2;
			qk *= q;
			A_2 = ByteCodeHelper.f2i(qk + 0.5f);
			vDk0[i] = A_2 - A_;
		}
		Arrays.sort(vDk0, 0, nrBand0);
		vk0[0] = k0;
		for (int i = 1; i <= nrBand0; i++)
		{
			vk0[i] = vk0[i - 1] + vDk0[i - 1];
			if (vDk0[i - 1] == 0)
			{
				return 1;
			}
		}
		if (twoRegions == 0)
		{
			for (int i = 0; i <= nrBand0; i++)
			{
				sbr.f_master[i] = vk0[i];
			}
			sbr.N_master = nrBand0;
			sbr.N_master = Math.min(sbr.N_master, 64);
			return 0;
		}
		int nrBand1 = 2 * find_bands(1, bands, k3, k2);
		nrBand1 = Math.min(nrBand1, 63);
		q = find_initial_power(nrBand1, k3, k2);
		qk = k3;
		A_2 = ByteCodeHelper.f2i(qk + 0.5f);
		for (int i = 0; i <= nrBand1 - 1; i++)
		{
			int A_0 = A_2;
			qk *= q;
			A_2 = ByteCodeHelper.f2i(qk + 0.5f);
			vDk1[i] = A_2 - A_0;
		}
		if (vDk1[0] < vDk0[nrBand0 - 1])
		{
			Arrays.sort(vDk1, 0, nrBand1 + 1);
			int change = vDk0[nrBand0 - 1] - vDk1[0];
			vDk1[0] = vDk0[nrBand0 - 1];
			vDk1[nrBand1 - 1] -= change;
		}
		Arrays.sort(vDk1, 0, nrBand1);
		vk1[0] = k3;
		for (int i = 1; i <= nrBand1; i++)
		{
			vk1[i] = vk1[i - 1] + vDk1[i - 1];
			if (vDk1[i - 1] == 0)
			{
				return 1;
			}
		}
		sbr.N_master = nrBand0 + nrBand1;
		sbr.N_master = Math.min(sbr.N_master, 64);
		for (int i = 0; i <= nrBand0; i++)
		{
			sbr.f_master[i] = vk0[i];
		}
		for (int i = nrBand0 + 1; i <= sbr.N_master; i++)
		{
			sbr.f_master[i] = vk1[i - nrBand0];
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 78, 162, 195, 106, 131, 111, 159, 2, 111,
		143, 108, 53, 199, 127, 2, 113, 107, 99, 114,
		131, 144, 108, 100, 133, 103, 245, 59, 231, 72,
		104, 105, 170, 127, 1, 179, 108, 100, 197, 159,
		0, 243, 56, 231, 76, 136, 108, 153, 106, 227,
		60, 7, 231, 74
	})]
	public static int derived_frequency_table(SBR sbr, int bs_xover_band, int k2)
	{
		int i = 0;
		if (sbr.N_master <= bs_xover_band)
		{
			return 1;
		}
		sbr.N_high = sbr.N_master - bs_xover_band;
		sbr.N_low = (sbr.N_high >> 1) + (sbr.N_high - (sbr.N_high >> 1 << 1));
		sbr.n[0] = sbr.N_low;
		sbr.n[1] = sbr.N_high;
		for (int j = 0; j <= sbr.N_high; j++)
		{
			sbr.f_table_res[1][j] = sbr.f_master[j + bs_xover_band];
		}
		sbr.M = sbr.f_table_res[1][sbr.N_high] - sbr.f_table_res[1][0];
		sbr.kx = sbr.f_table_res[1][0];
		if (sbr.kx > 32)
		{
			return 1;
		}
		if (sbr.kx + sbr.M > 64)
		{
			return 1;
		}
		int minus = ((((uint)sbr.N_high & (true ? 1u : 0u)) != 0) ? 1 : 0);
		for (int j = 0; j <= sbr.N_low; j++)
		{
			i = ((j != 0) ? (2 * j - minus) : 0);
			sbr.f_table_res[0][j] = sbr.f_table_res[1][i];
		}
		sbr.N_Q = 0;
		if (sbr.bs_noise_bands == 0)
		{
			sbr.N_Q = 1;
		}
		else
		{
			sbr.N_Q = Math.max(1, find_bands(0, sbr.bs_noise_bands, sbr.kx, k2));
			sbr.N_Q = Math.min(5, sbr.N_Q);
		}
		for (int j = 0; j <= sbr.N_Q; j++)
		{
			if (j == 0)
			{
				i = 0;
			}
			else
			{
				int num = i;
				int num2 = sbr.N_low - i;
				int num3 = sbr.N_Q + 1 - j;
				i = num + ((num3 != -1) ? (num2 / num3) : (-num2));
			}
			sbr.f_table_noise[j] = sbr.f_table_res[0][i];
		}
		for (int j = 0; j < 64; j++)
		{
			for (int g = 0; g < sbr.N_Q; g++)
			{
				if (sbr.f_table_noise[g] <= j && j < sbr.f_table_noise[g + 1])
				{
					sbr.table_map_k_to_g[j] = g;
					break;
				}
			}
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 57, 66, 124, 127, 2, 138, 106, 105, 137,
		106, 108, 52, 199, 108, 46, 167, 108, 46, 231,
		70, 117, 99, 146, 102, 162, 169, 104, 145, 136,
		145, 110, 103, 110, 106, 4, 201, 104, 100, 110,
		108, 4, 201, 101, 101, 198, 149, 117, 103, 230,
		69, 147, 106, 201, 165, 134, 107, 104, 53, 231,
		159, 180, 234, 160, 81
	})]
	public static void limiter_frequency_table(SBR sbr)
	{
		sbr.f_table_lim[0][0] = sbr.f_table_res[0][0] - sbr.kx;
		sbr.f_table_lim[0][1] = sbr.f_table_res[0][sbr.N_low] - sbr.kx;
		sbr.N_L[0] = 1;
		for (int s = 1; s < 4; s++)
		{
			int[] limTable = new int[100];
			int[] patchBorders = new int[64];
			patchBorders[0] = sbr.kx;
			int j;
			for (j = 1; j <= sbr.noPatches; j++)
			{
				patchBorders[j] = patchBorders[j - 1] + sbr.patchNoSubbands[j - 1];
			}
			for (j = 0; j <= sbr.N_low; j++)
			{
				limTable[j] = sbr.f_table_res[0][j];
			}
			for (j = 1; j < sbr.noPatches; j++)
			{
				limTable[j + sbr.N_low] = patchBorders[j];
			}
			Arrays.sort(limTable, 0, sbr.noPatches + sbr.N_low);
			j = 1;
			int nrLim = sbr.noPatches + sbr.N_low - 1;
			if (nrLim < 0)
			{
				break;
			}
			while (j <= nrLim)
			{
				float nOctaves = ((limTable[j - 1] == 0) ? 0f : ((float)limTable[j] / (float)limTable[j - 1]));
				if (nOctaves < limiterBandsCompare[s - 1])
				{
					if (limTable[j] != limTable[j - 1])
					{
						int found = 0;
						int found2 = 0;
						for (int i = 0; i <= sbr.noPatches; i++)
						{
							if (limTable[j] == patchBorders[i])
							{
								found = 1;
							}
						}
						if (found != 0)
						{
							found2 = 0;
							for (int i = 0; i <= sbr.noPatches; i++)
							{
								if (limTable[j - 1] == patchBorders[i])
								{
									found2 = 1;
								}
							}
							if (found2 != 0)
							{
								j++;
								continue;
							}
							limTable[j - 1] = sbr.f_table_res[0][sbr.N_low];
							Arrays.sort(limTable, 0, sbr.noPatches + sbr.N_low);
							nrLim += -1;
							continue;
						}
					}
					limTable[j] = sbr.f_table_res[0][sbr.N_low];
					Arrays.sort(limTable, 0, nrLim);
					nrLim += -1;
				}
				else
				{
					j++;
				}
			}
			sbr.N_L[s] = nrLim;
			for (j = 0; j <= nrLim; j++)
			{
				sbr.f_table_lim[s][j] = limTable[j] - sbr.kx;
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		135,
		162,
		159,
		45,
		byte.MaxValue,
		163,
		107,
		161,
		43
	})]
	static FBT()
	{
		stopMinTable = new int[12]
		{
			13, 15, 20, 21, 23, 32, 32, 35, 48, 64,
			70, 96
		};
		STOP_OFFSET_TABLE = new int[12][]
		{
			new int[14]
			{
				0, 2, 4, 6, 8, 11, 14, 18, 22, 26,
				31, 37, 44, 51
			},
			new int[14]
			{
				0, 2, 4, 6, 8, 11, 14, 18, 22, 26,
				31, 36, 42, 49
			},
			new int[14]
			{
				0, 2, 4, 6, 8, 11, 14, 17, 21, 25,
				29, 34, 39, 44
			},
			new int[14]
			{
				0, 2, 4, 6, 8, 11, 14, 17, 20, 24,
				28, 33, 38, 43
			},
			new int[14]
			{
				0, 2, 4, 6, 8, 11, 14, 17, 20, 24,
				28, 32, 36, 41
			},
			new int[14]
			{
				0, 2, 4, 6, 8, 10, 12, 14, 17, 20,
				23, 26, 29, 32
			},
			new int[14]
			{
				0, 2, 4, 6, 8, 10, 12, 14, 17, 20,
				23, 26, 29, 32
			},
			new int[14]
			{
				0, 1, 3, 5, 7, 9, 11, 13, 15, 17,
				20, 23, 26, 29
			},
			new int[14]
			{
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
				10, 12, 14, 16
			},
			new int[14]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0
			},
			new int[14]
			{
				0, -1, -2, -3, -4, -5, -6, -6, -6, -6,
				-6, -6, -6, -6
			},
			new int[14]
			{
				0, -3, -6, -9, -12, -15, -18, -20, -22, -24,
				-26, -28, -30, -32
			}
		};
		limiterBandsCompare = new float[3] { 1.327152f, 1.185093f, 1.119872f };
	}
}
