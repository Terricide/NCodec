using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace net.sourceforge.jaad.aac.sbr;

internal class HFGeneration : Object
{
	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class acorr_coef : Object
	{
		internal float[] r01;

		internal float[] r02;

		internal float[] r11;

		internal float[] r12;

		internal float[] r22;

		internal float det;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 137, 162, 105, 109, 109, 109, 109, 109 })]
		public acorr_coef()
		{
			r01 = new float[2];
			r02 = new float[2];
			r11 = new float[2];
			r12 = new float[2];
			r22 = new float[2];
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] goalSbTab;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 80, 98, 111, 159, 5, 119, 159, 18, 159,
		16, 114, 144, 114, 144, 117, 245, 49, 234, 81
	})]
	private static void calc_chirp_factors(SBR sbr, int ch)
	{
		for (int i = 0; i < sbr.N_Q; i++)
		{
			sbr.bwArray[ch][i] = mapNewBw(sbr.bs_invf_mode[ch][i], sbr.bs_invf_mode_prev[ch][i]);
			if (sbr.bwArray[ch][i] < sbr.bwArray_prev[ch][i])
			{
				sbr.bwArray[ch][i] = sbr.bwArray[ch][i] * 0.75f + sbr.bwArray_prev[ch][i] * 0.25f;
			}
			else
			{
				sbr.bwArray[ch][i] = sbr.bwArray[ch][i] * (29f / 32f) + sbr.bwArray_prev[ch][i] * (3f / 32f);
			}
			if (sbr.bwArray[ch][i] < 1f / 64f)
			{
				sbr.bwArray[ch][i] = 0f;
			}
			if (sbr.bwArray[ch][i] >= 0.99609375f)
			{
				sbr.bwArray[ch][i] = 0.99609375f;
			}
			sbr.bwArray_prev[ch][i] = sbr.bwArray[ch][i];
			sbr.bs_invf_mode_prev[ch][i] = sbr.bs_invf_mode[ch][i];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 75, 162, 104, 136, 147, 136, 113, 113, 38,
		231, 69, 169, 105, 104, 106, 138, 194, 167, 135,
		108, 153, 146, 120, 191, 6, 113, 100, 100, 177,
		168, 112, 137, 149, 124, 175, 115
	})]
	private static void patch_construction(SBR sbr)
	{
		int msb = sbr.k0;
		int usb = sbr.kx;
		int goalSb = goalSbTab[sbr.sample_rate.getIndex()];
		sbr.noPatches = 0;
		int k;
		if (goalSb < sbr.kx + sbr.M)
		{
			int i = 0;
			k = 0;
			for (; sbr.f_master[i] < goalSb; i++)
			{
				k = i + 1;
			}
		}
		else
		{
			k = sbr.N_master;
		}
		if (sbr.N_master == 0)
		{
			sbr.noPatches = 0;
			sbr.patchNoSubbands[0] = 0;
			sbr.patchStartSubband[0] = 0;
			return;
		}
		int sb;
		do
		{
			int j = k + 1;
			int odd;
			do
			{
				j += -1;
				sb = sbr.f_master[j];
				int num = sb - 2 + sbr.k0;
				odd = ((2 != -1) ? (num % 2) : 0);
			}
			while (sb > sbr.k0 - 1 + msb - odd);
			sbr.patchNoSubbands[sbr.noPatches] = Math.max(sb - usb, 0);
			sbr.patchStartSubband[sbr.noPatches] = sbr.k0 - odd - sbr.patchNoSubbands[sbr.noPatches];
			if (sbr.patchNoSubbands[sbr.noPatches] > 0)
			{
				usb = sb;
				msb = sb;
				sbr.noPatches++;
			}
			else
			{
				msb = sbr.kx;
			}
			if (sbr.f_master[k] - sb < 3)
			{
				k = sbr.N_master;
			}
		}
		while (sb != sbr.kx + sbr.M);
		if (sbr.patchNoSubbands[sbr.noPatches - 1] < 3 && sbr.noPatches > 1)
		{
			sbr.noPatches--;
		}
		sbr.noPatches = Math.min(sbr.noPatches, 5);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 96, 98, 135, 147, 110, 108, 177, 111, 127,
		37, 191, 37, 112, 108, 177, 113, 127, 24, 191,
		24, 159, 44, 108, 108, 108, 140
	})]
	private static void calc_prediction_coef(SBR sbr, float[][][] Xlow, float[][] alpha_0, float[][] alpha_1, int k)
	{
		acorr_coef ac = new acorr_coef();
		auto_correlation(sbr, ac, Xlow, k, sbr.numTimeSlotsRate + 6);
		if (ac.det == 0f)
		{
			alpha_1[k][0] = 0f;
			alpha_1[k][1] = 0f;
		}
		else
		{
			float tmp2 = 1f / ac.det;
			alpha_1[k][0] = (ac.r01[0] * ac.r12[0] - ac.r01[1] * ac.r12[1] - ac.r02[0] * ac.r11[0]) * tmp2;
			alpha_1[k][1] = (ac.r01[1] * ac.r12[0] + ac.r01[0] * ac.r12[1] - ac.r02[1] * ac.r11[0]) * tmp2;
		}
		if (ac.r11[0] == 0f)
		{
			alpha_0[k][0] = 0f;
			alpha_0[k][1] = 0f;
		}
		else
		{
			float tmp = 1f / ac.r11[0];
			alpha_0[k][0] = (0f - (ac.r01[0] + alpha_1[k][0] * ac.r12[0] + alpha_1[k][1] * ac.r12[1])) * tmp;
			alpha_0[k][1] = (0f - (ac.r01[1] + alpha_1[k][1] * ac.r12[0] - alpha_1[k][0] * ac.r12[1])) * tmp;
		}
		if (alpha_0[k][0] * alpha_0[k][0] + alpha_0[k][1] * alpha_0[k][1] >= 16f || alpha_1[k][0] * alpha_1[k][0] + alpha_1[k][1] * alpha_1[k][1] >= 16f)
		{
			alpha_0[k][0] = 0f;
			alpha_0[k][1] = 0f;
			alpha_1[k][0] = 0f;
			alpha_1[k][1] = 0f;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 112, 98, 159, 1, 136, 137, 109, 109, 109,
		141, 101, 101, 101, 133, 113, 101, 101, 101, 101,
		107, 107, 115, 115, 115, 115, 245, 53, 236, 89,
		191, 11, 191, 11, 223, 12, 106, 106, 106, 106,
		139, 127, 38
	})]
	private static void auto_correlation(SBR sbr, acorr_coef ac, float[][][] buffer, int bd, int len)
	{
		float r01r = 0f;
		float r01i = 0f;
		float r02r = 0f;
		float r02i = 0f;
		float r11r = 0f;
		float rel = 0.999999046f;
		int offset = sbr.tHFAdj;
		float temp2_r = buffer[offset - 2][bd][0];
		float temp2_i = buffer[offset - 2][bd][1];
		float temp3_r = buffer[offset - 1][bd][0];
		float temp3_i = buffer[offset - 1][bd][1];
		float temp4_r = temp2_r;
		float temp4_i = temp2_i;
		float temp5_r = temp3_r;
		float temp5_i = temp3_i;
		for (int i = offset; i < len + offset; i++)
		{
			float temp1_r = temp2_r;
			float temp1_i = temp2_i;
			temp2_r = temp3_r;
			temp2_i = temp3_i;
			temp3_r = buffer[i][bd][0];
			temp3_i = buffer[i][bd][1];
			r01r += temp3_r * temp2_r + temp3_i * temp2_i;
			r01i += temp3_i * temp2_r - temp3_r * temp2_i;
			r02r += temp3_r * temp1_r + temp3_i * temp1_i;
			r02i += temp3_i * temp1_r - temp3_r * temp1_i;
			r11r += temp2_r * temp2_r + temp2_i * temp2_i;
		}
		ac.r12[0] = r01r - (temp3_r * temp2_r + temp3_i * temp2_i) + (temp5_r * temp4_r + temp5_i * temp4_i);
		ac.r12[1] = r01i - (temp3_i * temp2_r - temp3_r * temp2_i) + (temp5_i * temp4_r - temp5_r * temp4_i);
		ac.r22[0] = r11r - (temp2_r * temp2_r + temp2_i * temp2_i) + (temp4_r * temp4_r + temp4_i * temp4_i);
		ac.r01[0] = r01r;
		ac.r01[1] = r01i;
		ac.r02[0] = r02r;
		ac.r02[1] = r02i;
		ac.r11[0] = r11r;
		ac.det = ac.r11[0] * ac.r22[0] - rel * (ac.r12[0] * ac.r12[0] + ac.r12[1] * ac.r12[1]);
	}

	[LineNumberTable(new byte[]
	{
		159, 87, 66, 183, 100, 135, 199, 199, 199, 101,
		135
	})]
	private static float mapNewBw(int invf_mode, int invf_mode_prev)
	{
		switch (invf_mode)
		{
		case 1:
			if (invf_mode_prev == 0)
			{
				return 0.6f;
			}
			return 0.75f;
		case 2:
			return 0.9f;
		case 3:
			return 0.98f;
		default:
			if (invf_mode_prev == 1)
			{
				return 0.6f;
			}
			return 0f;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	internal HFGeneration()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		134,
		162,
		159,
		44,
		105,
		109,
		148,
		136,
		108,
		231,
		69,
		113,
		244,
		70,
		108,
		106,
		47,
		169,
		143,
		140,
		110,
		201,
		173,
		140,
		109,
		109,
		109,
		141,
		113,
		113,
		113,
		113,
		110,
		101,
		101,
		111,
		101,
		101,
		143,
		byte.MaxValue,
		16,
		70,
		byte.MaxValue,
		16,
		50,
		236,
		85,
		131,
		107,
		121,
		25,
		233,
		8,
		44,
		236,
		160,
		65,
		105,
		137
	})]
	public static void hf_generation(SBR sbr, float[][][] Xlow, float[][][] Xhigh, int ch)
	{
		int[] array = new int[2];
		int num = (array[1] = 2);
		num = (array[0] = 64);
		float[][] alpha_0 = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 2);
		num = (array[0] = 64);
		float[][] alpha_1 = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		int offset = sbr.tHFAdj;
		int first = sbr.t_E[ch][0];
		int last = sbr.t_E[ch][sbr.L_E[ch]];
		calc_chirp_factors(sbr, ch);
		if (ch == 0 && sbr.Reset)
		{
			patch_construction(sbr);
		}
		for (int i = 0; i < sbr.noPatches; i++)
		{
			for (int x = 0; x < sbr.patchNoSubbands[i]; x++)
			{
				int j = sbr.kx + x;
				for (int q = 0; q < i; q++)
				{
					j += sbr.patchNoSubbands[q];
				}
				int p = sbr.patchStartSubband[i] + x;
				int g = sbr.table_map_k_to_g[j];
				float bw = sbr.bwArray[ch][g];
				float bw2 = bw * bw;
				if (bw2 > 0f)
				{
					calc_prediction_coef(sbr, Xlow, alpha_0, alpha_1, p);
					float a0_r = alpha_0[p][0] * bw;
					float a1_r = alpha_1[p][0] * bw2;
					float a0_i = alpha_0[p][1] * bw;
					float a1_i = alpha_1[p][1] * bw2;
					float temp2_r = Xlow[first - 2 + offset][p][0];
					float temp3_r = Xlow[first - 1 + offset][p][0];
					float temp2_i = Xlow[first - 2 + offset][p][1];
					float temp3_i = Xlow[first - 1 + offset][p][1];
					for (int l = first; l < last; l++)
					{
						float temp1_r = temp2_r;
						temp2_r = temp3_r;
						temp3_r = Xlow[l + offset][p][0];
						float temp1_i = temp2_i;
						temp2_i = temp3_i;
						temp3_i = Xlow[l + offset][p][1];
						Xhigh[l + offset][j][0] = temp3_r + (a0_r * temp2_r - a0_i * temp2_i + a1_r * temp1_r - a1_i * temp1_i);
						Xhigh[l + offset][j][1] = temp3_i + (a0_i * temp2_r + a0_r * temp2_i + a1_i * temp1_r + a1_r * temp1_i);
					}
				}
				else
				{
					for (int k = first; k < last; k++)
					{
						Xhigh[k + offset][j][0] = Xlow[k + offset][p][0];
						Xhigh[k + offset][j][1] = Xlow[k + offset][p][1];
					}
				}
			}
		}
		if (sbr.Reset)
		{
			FBT.limiter_frequency_table(sbr);
		}
	}

	[LineNumberTable(12)]
	static HFGeneration()
	{
		goalSbTab = new int[12]
		{
			21, 23, 32, 43, 46, 64, 85, 93, 128, 0,
			0, 0
		};
	}
}
