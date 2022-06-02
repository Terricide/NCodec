using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mpa;

public class MpaPqmf : java.lang.Object
{
	private const double MY_PI = System.Math.PI;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos1_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos3_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos5_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos7_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos9_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos11_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos13_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos15_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos17_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos19_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos21_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos23_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos25_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos27_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos29_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos31_64;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos1_32;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos3_32;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos5_32;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos7_32;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos9_32;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos11_32;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos13_32;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos15_32;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos1_16;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos3_16;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos5_16;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos7_16;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos1_8;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos3_8;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float cos1_4;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] bf32;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] bf16;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] bf8;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 98, 135, 103, 135, 104, 104, 105, 137,
		104, 104, 8, 231, 69, 104, 104, 8, 231, 69,
		121, 114, 106, 112, 109, 115, 107, 109, 107, 115,
		122, 122, 115, 141, 102, 102, 102, 102, 102, 102,
		102, 102, 103, 103, 103, 103, 103, 103, 103, 103,
		103, 103, 103, 103, 103, 135, 102, 102, 102, 102,
		102, 106, 106, 106, 102, 103, 103, 103, 103, 103,
		103, 103, 108, 108, 112, 108, 119, 106, 115, 108,
		115, 106, 111, 112, 107, 116, 107, 104
	})]
	internal static void computeButterfly(int pos, float[] s)
	{
		butterfly32(s);
		butterfly16L(s);
		butterfly16H(s);
		butterfly8L(s, 0);
		butterfly8H(s, 0);
		butterfly8L(s, 16);
		butterfly8H(s, 16);
		for (int j = 0; j < 32; j += 8)
		{
			butterfly4L(s, j);
			butterfly4H(s, j);
		}
		for (int i = 0; i < 32; i += 4)
		{
			butterfly2L(s, i);
			butterfly2H(s, i);
		}
		float k0 = 0f - s[14] - s[15] - s[10] - s[11];
		float k1 = s[29] + s[31] + s[25];
		float k6 = k1 + s[17];
		float k7 = k1 + s[21] + s[23];
		float k8 = s[15] + s[11];
		float k9 = s[15] + s[13] + s[9];
		float k10 = s[7] + s[5];
		float k11 = s[31] + s[23];
		float k12 = k11 + s[27];
		float k13 = s[31] + s[27] + s[19];
		float k2 = 0f - s[26] - s[27] - s[30] - s[31];
		float k3 = 0f - s[24] - s[28] - s[30] - s[31];
		float k4 = s[20] + s[22] + s[23];
		float k5 = s[21] + s[29];
		float s2 = s[0];
		float s3 = s[1];
		float s11 = s[2];
		float s17 = s[3];
		float s20 = s[4];
		float s21 = s[6];
		float s22 = s[7];
		float s23 = s[8];
		float s4 = s[12];
		float s5 = s[13];
		float s6 = s[14];
		float s7 = s[15];
		float s8 = s[16];
		float s9 = s[18];
		float s10 = s[19];
		float s12 = s[21];
		float s13 = s[22];
		float s14 = s[23];
		float s15 = s[28];
		float s16 = s[29];
		float s18 = s[30];
		float s19 = s[31];
		s[0] = s3;
		s[1] = k6;
		s[2] = k9;
		s[3] = k7;
		s[4] = k10;
		s[5] = k12 + k5;
		s[6] = k8 + s5;
		s[7] = k13 + s16;
		s[8] = s17;
		s[9] = k13;
		s[10] = k8;
		s[11] = k12;
		s[12] = s22;
		s[13] = k11;
		s[14] = s7;
		s[15] = s19;
		s[16] = 0f - k6 - s18;
		s[17] = 0f - k9 - s6;
		s[18] = 0f - k7 - s13 - s18;
		s[19] = 0f - k10 - s21;
		s[20] = k2 - s16 - s12 - s13 - s14;
		s[21] = k0 - s5;
		s[22] = k2 - s16 - s9 - s10;
		s[23] = 0f - s17 - s11;
		s[24] = k2 - s15 - s9 - s10;
		s[25] = k0 - s4;
		s[26] = k2 - s15 - k4;
		s[27] = 0f - s21 - s22 - s20;
		s[28] = k3 - k4;
		s[29] = 0f - s6 - s7 - s4 - s23;
		s[30] = k3 - s8;
		s[31] = 0f - s2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		128,
		66,
		99,
		139,
		101,
		byte.MaxValue,
		161,
		37,
		83,
		124,
		230,
		41,
		234,
		89
	})]
	internal static void computeFilter(int sampleOff, float[] samples, short[] @out, int outOff, float scalefactor)
	{
		int dvp = 0;
		for (int i = 0; i < 32; i++)
		{
			int b = i << 4;
			float pcm_sample = (samples[((16 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 0] + samples[((15 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 1] + samples[((14 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 2] + samples[((13 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 3] + samples[((12 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 4] + samples[((11 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 5] + samples[((10 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 6] + samples[((9 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 7] + samples[((8 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 8] + samples[((7 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 9] + samples[((6 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 10] + samples[((5 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 11] + samples[((4 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 12] + samples[((3 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 13] + samples[((2 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 14] + samples[((1 + sampleOff) & 0xF) + dvp] * MpaConst.dp[b + 15]) * scalefactor;
			@out[outOff + i] = (short)MathUtil.clip(ByteCodeHelper.f2i(pcm_sample), -32768, 32767);
			dvp += 16;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 79, 130, 104, 101, 104, 104, 244, 60, 231,
		70
	})]
	private static void butterfly32(float[] s)
	{
		for (int i = 0; i < 16; i++)
		{
			float tmp0 = s[i];
			float tmp1 = s[31 - i];
			s[i] = tmp0 + tmp1;
			s[31 - i] = (tmp0 - tmp1) * bf32[i];
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 96, 162, 103, 101, 104, 104, 244, 60, 231,
		70
	})]
	private static void butterfly16L(float[] s)
	{
		for (int i = 0; i < 8; i++)
		{
			float tmp0 = s[i];
			float tmp1 = s[15 - i];
			s[i] = tmp0 + tmp1;
			s[15 - i] = (tmp0 - tmp1) * bf16[i];
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 98, 130, 103, 104, 104, 107, 245, 60, 231,
		70
	})]
	private static void butterfly16H(float[] s)
	{
		for (int i = 0; i < 8; i++)
		{
			float tmp0 = s[16 + i];
			float tmp1 = s[31 - i];
			s[16 + i] = tmp0 + tmp1;
			s[31 - i] = (0f - (tmp0 - tmp1)) * bf16[i];
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 91, 98, 103, 103, 105, 106, 245, 60, 231,
		70
	})]
	private static void butterfly8L(float[] s, int o)
	{
		for (int i = 0; i < 4; i++)
		{
			float tmp0 = s[o + i];
			float tmp1 = s[o + 7 - i];
			s[o + i] = tmp0 + tmp1;
			s[o + 7 - i] = (tmp0 - tmp1) * bf8[i];
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 93, 66, 103, 105, 106, 108, 247, 60, 231,
		70
	})]
	private static void butterfly8H(float[] s, int o)
	{
		for (int i = 0; i < 4; i++)
		{
			float tmp0 = s[o + 8 + i];
			float tmp1 = s[o + 15 - i];
			s[o + 8 + i] = tmp0 + tmp1;
			s[o + 15 - i] = (0f - (tmp0 - tmp1)) * bf8[i];
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 86, 130, 101, 103, 106, 145, 103, 103, 106,
		113
	})]
	private static void butterfly4L(float[] s, int o)
	{
		float tmp0 = s[o];
		float tmp1 = s[o + 3];
		s[o + 0] = tmp0 + tmp1;
		s[o + 3] = (tmp0 - tmp1) * cos1_8;
		float tmp2 = s[o + 1];
		float tmp3 = s[o + 2];
		s[o + 1] = tmp2 + tmp3;
		s[o + 2] = (tmp2 - tmp3) * cos3_8;
	}

	[LineNumberTable(new byte[]
	{
		159, 89, 130, 103, 103, 106, 146, 103, 103, 106,
		114
	})]
	private static void butterfly4H(float[] s, int o)
	{
		float tmp0 = s[o + 4];
		float tmp1 = s[o + 7];
		s[o + 4] = tmp0 + tmp1;
		s[o + 7] = (0f - (tmp0 - tmp1)) * cos1_8;
		float tmp2 = s[o + 5];
		float tmp3 = s[o + 6];
		s[o + 5] = tmp2 + tmp3;
		s[o + 6] = (0f - (tmp2 - tmp3)) * cos3_8;
	}

	[LineNumberTable(new byte[] { 159, 81, 98, 101, 103, 106, 113 })]
	private static void butterfly2L(float[] s, int o)
	{
		float tmp0 = s[o];
		float tmp1 = s[o + 1];
		s[o + 0] = tmp0 + tmp1;
		s[o + 1] = (tmp0 - tmp1) * cos1_4;
	}

	[LineNumberTable(new byte[] { 159, 83, 130, 103, 103, 106, 114 })]
	private static void butterfly2H(float[] s, int o)
	{
		float tmp0 = s[o + 2];
		float tmp1 = s[o + 3];
		s[o + 2] = tmp0 + tmp1;
		s[o + 3] = (0f - (tmp0 - tmp1)) * cos1_4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(13)]
	public MpaPqmf()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 138, 66, 127, 6, 127, 6, 127, 6, 127,
		6, 127, 6, 127, 6, 127, 6, 127, 6, 127,
		6, 127, 6, 127, 6, 127, 6, 127, 6, 127,
		6, 127, 6, 127, 6, 127, 6, 127, 6, 127,
		6, 127, 6, 127, 6, 127, 6, 127, 6, 127,
		6, 127, 6, 127, 6, 127, 6, 127, 6, 127,
		6, 127, 6, 159, 6, 191, 117, 159, 45
	})]
	static MpaPqmf()
	{
		cos1_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI / 64.0)));
		cos3_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 3.0 / 64.0)));
		cos5_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 5.0 / 64.0)));
		cos7_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 7.0 / 64.0)));
		cos9_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 9.0 / 64.0)));
		cos11_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 11.0 / 64.0)));
		cos13_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 13.0 / 64.0)));
		cos15_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 15.0 / 64.0)));
		cos17_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 17.0 / 64.0)));
		cos19_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 19.0 / 64.0)));
		cos21_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 21.0 / 64.0)));
		cos23_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 23.0 / 64.0)));
		cos25_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 25.0 / 64.0)));
		cos27_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 27.0 / 64.0)));
		cos29_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 29.0 / 64.0)));
		cos31_64 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 31.0 / 64.0)));
		cos1_32 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI / 32.0)));
		cos3_32 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 3.0 / 32.0)));
		cos5_32 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 5.0 / 32.0)));
		cos7_32 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 7.0 / 32.0)));
		cos9_32 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 9.0 / 32.0)));
		cos11_32 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 11.0 / 32.0)));
		cos13_32 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 13.0 / 32.0)));
		cos15_32 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 15.0 / 32.0)));
		cos1_16 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI / 16.0)));
		cos3_16 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 3.0 / 16.0)));
		cos5_16 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 5.0 / 16.0)));
		cos7_16 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 7.0 / 16.0)));
		cos1_8 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI / 8.0)));
		cos3_8 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI * 3.0 / 8.0)));
		cos1_4 = (float)(1.0 / (2.0 * java.lang.Math.cos(System.Math.PI / 4.0)));
		bf32 = new float[16]
		{
			cos1_64, cos3_64, cos5_64, cos7_64, cos9_64, cos11_64, cos13_64, cos15_64, cos17_64, cos19_64,
			cos21_64, cos23_64, cos25_64, cos27_64, cos29_64, cos31_64
		};
		bf16 = new float[8] { cos1_32, cos3_32, cos5_32, cos7_32, cos9_32, cos11_32, cos13_32, cos15_32 };
		bf8 = new float[4] { cos1_16, cos3_16, cos5_16, cos7_16 };
	}
}
