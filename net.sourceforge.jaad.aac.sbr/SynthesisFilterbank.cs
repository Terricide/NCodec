using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace net.sourceforge.jaad.aac.sbr;

internal class SynthesisFilterbank : Object, FilterbankTable
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[][] qmf32_pre_twiddle;

	private float[] v;

	private int v_index;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int channels;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 105, 104, 114, 104 })]
	public SynthesisFilterbank(int channels)
	{
		this.channels = channels;
		v = new float[2 * channels * 20];
		v_index = 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 162, 115 })]
	public virtual void reset()
	{
		Arrays.fill(v, 0f);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 66, 113, 103, 227, 69, 241, 71, 109,
		127, 18, 159, 18, 117, 245, 59, 236, 73, 105,
		137, 109, 127, 33, 31, 38, 236, 70, 109, 63,
		161, 57, 236, 78, 112, 106, 236, 23, 236, 107
	})]
	internal virtual void sbr_qmf_synthesis_32(SBR sbr, float[][][] X, float[] output)
	{
		float[] x1 = new float[32];
		float[] x2 = new float[32];
		float scale = 1f / 64f;
		int @out = 0;
		for (int j = 0; j < sbr.numTimeSlotsRate; j++)
		{
			for (int i = 0; i < 32; i++)
			{
				x1[i] = X[j][i][0] * qmf32_pre_twiddle[i][0] - X[j][i][1] * qmf32_pre_twiddle[i][1];
				x2[i] = X[j][i][1] * qmf32_pre_twiddle[i][0] + X[j][i][0] * qmf32_pre_twiddle[i][1];
				int num = i;
				float[] array = x1;
				array[num] *= scale;
				num = i;
				array = x2;
				array[num] *= scale;
			}
			DCT4_32(x1, x1);
			DST4_32(x2, x2);
			for (int k = 0; k < 32; k++)
			{
				float[] array2 = v;
				int num2 = v_index + k;
				float[] array3 = v;
				int num3 = v_index + 640 + k;
				float num4 = 0f - x1[k] + x2[k];
				int num = num3;
				float[] array = array3;
				float num5 = num4;
				array[num] = num4;
				array2[num2] = num5;
				float[] array4 = v;
				int num6 = v_index + 63 - k;
				float[] array5 = v;
				int num7 = v_index + 640 + 63 - k;
				num4 = x1[k] + x2[k];
				num = num7;
				array = array5;
				float num8 = num4;
				array[num] = num4;
				array4[num6] = num8;
			}
			for (int i = 0; i < 32; i++)
			{
				int num9 = @out;
				@out++;
				output[num9] = v[v_index + i] * FilterbankTable.qmf_c[2 * i] + v[v_index + 96 + i] * FilterbankTable.qmf_c[64 + 2 * i] + v[v_index + 128 + i] * FilterbankTable.qmf_c[128 + 2 * i] + v[v_index + 224 + i] * FilterbankTable.qmf_c[192 + 2 * i] + v[v_index + 256 + i] * FilterbankTable.qmf_c[256 + 2 * i] + v[v_index + 352 + i] * FilterbankTable.qmf_c[320 + 2 * i] + v[v_index + 384 + i] * FilterbankTable.qmf_c[384 + 2 * i] + v[v_index + 480 + i] * FilterbankTable.qmf_c[448 + 2 * i] + v[v_index + 512 + i] * FilterbankTable.qmf_c[512 + 2 * i] + v[v_index + 608 + i] * FilterbankTable.qmf_c[576 + 2 * i];
			}
			v_index -= 64;
			if (v_index < 0)
			{
				v_index = 576;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		113,
		130,
		127,
		2,
		159,
		6,
		104,
		228,
		69,
		241,
		70,
		135,
		111,
		110,
		113,
		112,
		109,
		119,
		114,
		123,
		246,
		60,
		236,
		70,
		111,
		112,
		111,
		176,
		106,
		142,
		105,
		203,
		141,
		127,
		23,
		127,
		29,
		127,
		33,
		byte.MaxValue,
		39,
		59,
		236,
		72,
		169,
		109,
		63,
		161,
		7,
		236,
		79,
		115,
		106,
		236,
		4,
		236,
		126
	})]
	internal virtual void sbr_qmf_synthesis_64(SBR sbr, float[][][] X, float[] output)
	{
		float[] in_real1 = new float[32];
		float[] in_imag1 = new float[32];
		float[] out_real1 = new float[32];
		float[] out_imag1 = new float[32];
		float[] in_real2 = new float[32];
		float[] in_imag2 = new float[32];
		float[] out_real2 = new float[32];
		float[] out_imag2 = new float[32];
		float scale = 1f / 64f;
		int @out = 0;
		for (int j = 0; j < sbr.numTimeSlotsRate; j++)
		{
			float[][] pX = X[j];
			in_imag1[31] = scale * pX[1][0];
			in_real1[0] = scale * pX[0][0];
			in_imag2[31] = scale * pX[62][1];
			in_real2[0] = scale * pX[63][1];
			for (int i = 1; i < 31; i++)
			{
				in_imag1[31 - i] = scale * pX[2 * i + 1][0];
				in_real1[i] = scale * pX[2 * i][0];
				in_imag2[31 - i] = scale * pX[63 - (2 * i + 1)][1];
				in_real2[i] = scale * pX[63 - 2 * i][1];
			}
			in_imag1[0] = scale * pX[63][0];
			in_real1[31] = scale * pX[62][0];
			in_imag2[0] = scale * pX[0][1];
			in_real2[31] = scale * pX[1][1];
			DCT.dct4_kernel(in_real1, in_imag1, out_real1, out_imag1);
			DCT.dct4_kernel(in_real2, in_imag2, out_real2, out_imag2);
			int pring_buffer_1 = v_index;
			int pring_buffer_2 = pring_buffer_1 + 1280;
			for (int k = 0; k < 32; k++)
			{
				float[] array = v;
				int num = pring_buffer_1 + 2 * k;
				float[] array2 = v;
				int num2 = pring_buffer_2 + 2 * k;
				float num3 = out_real2[k] - out_real1[k];
				int num4 = num2;
				float[] array3 = array2;
				float num5 = num3;
				array3[num4] = num3;
				array[num] = num5;
				float[] array4 = v;
				int num6 = pring_buffer_1 + 127 - 2 * k;
				float[] array5 = v;
				int num7 = pring_buffer_2 + 127 - 2 * k;
				num3 = out_real2[k] + out_real1[k];
				num4 = num7;
				array3 = array5;
				float num8 = num3;
				array3[num4] = num3;
				array4[num6] = num8;
				float[] array6 = v;
				int num9 = pring_buffer_1 + 2 * k + 1;
				float[] array7 = v;
				int num10 = pring_buffer_2 + 2 * k + 1;
				num3 = out_imag2[31 - k] + out_imag1[31 - k];
				num4 = num10;
				array3 = array7;
				float num11 = num3;
				array3[num4] = num3;
				array6[num9] = num11;
				float[] array8 = v;
				int num12 = pring_buffer_1 + 127 - (2 * k + 1);
				float[] array9 = v;
				int num13 = pring_buffer_2 + 127 - (2 * k + 1);
				num3 = out_imag2[31 - k] - out_imag1[31 - k];
				num4 = num13;
				array3 = array9;
				float num14 = num3;
				array3[num4] = num3;
				array8[num12] = num14;
			}
			pring_buffer_1 = v_index;
			for (int i = 0; i < 64; i++)
			{
				int num15 = @out;
				@out++;
				output[num15] = v[pring_buffer_1 + i + 0] * FilterbankTable.qmf_c[i + 0] + v[pring_buffer_1 + i + 192] * FilterbankTable.qmf_c[i + 64] + v[pring_buffer_1 + i + 256] * FilterbankTable.qmf_c[i + 128] + v[pring_buffer_1 + i + 448] * FilterbankTable.qmf_c[i + 192] + v[pring_buffer_1 + i + 512] * FilterbankTable.qmf_c[i + 256] + v[pring_buffer_1 + i + 704] * FilterbankTable.qmf_c[i + 320] + v[pring_buffer_1 + i + 768] * FilterbankTable.qmf_c[i + 384] + v[pring_buffer_1 + i + 960] * FilterbankTable.qmf_c[i + 448] + v[pring_buffer_1 + i + 1024] * FilterbankTable.qmf_c[i + 512] + v[pring_buffer_1 + i + 1216] * FilterbankTable.qmf_c[i + 576];
			}
			v_index -= 128;
			if (v_index < 0)
			{
				v_index = 1152;
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 85, 130, 108, 108, 106, 106, 108, 108, 108,
		108, 109, 109, 108, 108, 109, 109, 108, 108, 109,
		109, 108, 108, 109, 109, 108, 108, 109, 109, 108,
		108, 109, 109, 108, 108, 105, 105, 106, 106, 106,
		106, 107, 107, 106, 106, 107, 107, 106, 106, 107,
		107, 106, 106, 107, 107, 106, 106, 107, 107, 106,
		106, 107, 107, 106, 106, 107, 107, 105, 108, 108,
		108, 105, 105, 105, 108, 108, 108, 105, 105, 105,
		108, 108, 108, 105, 105, 105, 108, 108, 108, 105,
		105, 105, 108, 108, 108, 105, 105, 105, 108, 108,
		108, 105, 105, 105, 108, 108, 108, 105, 105, 105,
		108, 108, 108, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 108, 108,
		108, 105, 105, 105, 108, 108, 108, 105, 105, 105,
		108, 108, 108, 105, 105, 105, 108, 108, 108, 105,
		105, 105, 108, 108, 108, 105, 105, 105, 108, 108,
		108, 105, 105, 105, 108, 108, 108, 105, 105, 105,
		108, 108, 108, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 108, 108,
		108, 105, 105, 105, 108, 108, 108, 105, 105, 105,
		108, 108, 108, 105, 105, 105, 108, 108, 108, 105,
		105, 105, 108, 108, 108, 105, 105, 105, 108, 110,
		110, 109, 111, 107, 110, 112, 110, 111, 111, 107,
		110, 112, 110, 111, 111, 107, 107, 107, 107, 107,
		107, 107, 107, 107, 107, 107, 107, 107, 107, 107,
		107, 107, 107, 107, 107, 109, 109, 109, 109, 109,
		109, 109, 109, 109, 109, 109, 109, 111, 112, 112,
		112, 110, 111, 111, 112, 112, 112, 110, 111, 111,
		112, 112, 112, 110, 111, 111, 112, 112, 112, 110,
		111, 111, 112, 112, 112, 110, 111, 111, 112, 112,
		112, 111, 111, 111, 112, 112, 112, 111, 111, 111,
		112, 112, 112, 111, 111, 111, 112, 112, 112, 111,
		111, 111, 112, 112, 112, 111, 111, 111, 112, 112,
		112, 111, 111, 111, 112, 112, 112, 111, 111, 111,
		112, 112, 112, 111, 110, 111, 112, 112, 112, 111,
		110, 111, 112, 112, 112, 111, 110, 111, 112, 112,
		112, 111, 110
	})]
	private void DCT4_32(float[] y, float[] x)
	{
		float f0 = x[15] - x[16];
		float f1 = x[15] + x[16];
		float f112 = 0.707106769f * f1;
		float f223 = 0.707106769f * f0;
		float f302 = x[8] - x[23];
		float f313 = x[8] + x[23];
		float f324 = 0.707106769f * f313;
		float f335 = 0.707106769f * f302;
		float f346 = x[12] - x[19];
		float f357 = x[12] + x[19];
		float f2 = 0.707106769f * f357;
		float f13 = 0.707106769f * f346;
		float f24 = x[11] - x[20];
		float f35 = x[11] + x[20];
		float f46 = 0.707106769f * f35;
		float f57 = 0.707106769f * f24;
		float f68 = x[14] - x[17];
		float f79 = x[14] + x[17];
		float f90 = 0.707106769f * f79;
		float f101 = 0.707106769f * f68;
		float f113 = x[9] - x[22];
		float f124 = x[9] + x[22];
		float f135 = 0.707106769f * f124;
		float f146 = 0.707106769f * f113;
		float f157 = x[13] - x[18];
		float f168 = x[13] + x[18];
		float f179 = 0.707106769f * f168;
		float f190 = 0.707106769f * f157;
		float f201 = x[10] - x[21];
		float f212 = x[10] + x[21];
		float f224 = 0.707106769f * f212;
		float f233 = 0.707106769f * f201;
		float f242 = x[0] - f112;
		float f249 = x[0] + f112;
		float f256 = x[31] - f223;
		float f265 = x[31] + f223;
		float f272 = x[7] - f324;
		float f279 = x[7] + f324;
		float f288 = x[24] - f335;
		float f295 = x[24] + f335;
		float f303 = x[3] - f2;
		float f304 = x[3] + f2;
		float f305 = x[28] - f13;
		float f306 = x[28] + f13;
		float f307 = x[4] - f46;
		float f308 = x[4] + f46;
		float f309 = x[27] - f57;
		float f310 = x[27] + f57;
		float f311 = x[1] - f90;
		float f312 = x[1] + f90;
		float f314 = x[30] - f101;
		float f315 = x[30] + f101;
		float f316 = x[6] - f135;
		float f317 = x[6] + f135;
		float f318 = x[25] - f146;
		float f319 = x[25] + f146;
		float f320 = x[2] - f179;
		float f321 = x[2] + f179;
		float f322 = x[29] - f190;
		float f323 = x[29] + f190;
		float f325 = x[5] - f224;
		float f326 = x[5] + f224;
		float f327 = x[26] - f233;
		float f328 = x[26] + f233;
		float f329 = f295 + f279;
		float f330 = -0.5411961f * f295;
		float f331 = 0.9238795f * f329;
		float f332 = 1.306563f * f279;
		float f333 = f330 + f331;
		float f334 = f332 - f331;
		float f336 = f288 + f272;
		float f337 = 1.306563f * f288;
		float f338 = -0.382683426f * f336;
		float f339 = 0.5411961f * f272;
		float f340 = f337 + f338;
		float f341 = f339 - f338;
		float f342 = f310 + f308;
		float f343 = -0.5411961f * f310;
		float f344 = 0.9238795f * f342;
		float f345 = 1.306563f * f308;
		float f347 = f343 + f344;
		float f348 = f345 - f344;
		float f349 = f309 + f307;
		float f350 = 1.306563f * f309;
		float f351 = -0.382683426f * f349;
		float f352 = 0.5411961f * f307;
		float f353 = f350 + f351;
		float f354 = f352 - f351;
		float f355 = f319 + f317;
		float f356 = -0.5411961f * f319;
		float f358 = 0.9238795f * f355;
		float f359 = 1.306563f * f317;
		float f360 = f356 + f358;
		float f361 = f359 - f358;
		float f362 = f318 + f316;
		float f363 = 1.306563f * f318;
		float f364 = -0.382683426f * f362;
		float f365 = 0.5411961f * f316;
		float f366 = f363 + f364;
		float f367 = f365 - f364;
		float f3 = f328 + f326;
		float f4 = -0.5411961f * f328;
		float f5 = 0.9238795f * f3;
		float f6 = 1.306563f * f326;
		float f7 = f4 + f5;
		float f8 = f6 - f5;
		float f9 = f327 + f325;
		float f10 = 1.306563f * f327;
		float f11 = -0.382683426f * f9;
		float f12 = 0.5411961f * f325;
		float f14 = f10 + f11;
		float f15 = f12 - f11;
		float f16 = f249 - f333;
		float f17 = f249 + f333;
		float f18 = f265 - f334;
		float f19 = f265 + f334;
		float f20 = f242 - f340;
		float f21 = f242 + f340;
		float f22 = f256 - f341;
		float f23 = f256 + f341;
		float f25 = f304 - f347;
		float f26 = f304 + f347;
		float f27 = f306 - f348;
		float f28 = f306 + f348;
		float f29 = f303 - f353;
		float f30 = f303 + f353;
		float f31 = f305 - f354;
		float f32 = f305 + f354;
		float f33 = f312 - f360;
		float f34 = f312 + f360;
		float f36 = f315 - f361;
		float f37 = f315 + f361;
		float f38 = f311 - f366;
		float f39 = f311 + f366;
		float f40 = f314 - f367;
		float f41 = f314 + f367;
		float f42 = f321 - f7;
		float f43 = f321 + f7;
		float f44 = f323 - f8;
		float f45 = f323 + f8;
		float f47 = f320 - f14;
		float f48 = f320 + f14;
		float f49 = f322 - f15;
		float f50 = f322 + f15;
		float f51 = f28 + f26;
		float f52 = -0.785694957f * f28;
		float f53 = 0.980785251f * f51;
		float f54 = 1.17587554f * f26;
		float f55 = f52 + f53;
		float f56 = f54 - f53;
		float f58 = f32 + f30;
		float f59 = 0.27589938f * f32;
		float f60 = 0.555570245f * f58;
		float f61 = 1.3870399f * f30;
		float f62 = f59 + f60;
		float f63 = f61 - f60;
		float f64 = f27 + f25;
		float f65 = 1.17587554f * f27;
		float f66 = -0.195090324f * f64;
		float f67 = 0.785694957f * f25;
		float f69 = f65 + f66;
		float f70 = f67 - f66;
		float f71 = f31 + f29;
		float f72 = 1.3870399f * f31;
		float f73 = -0.8314696f * f71;
		float f74 = -0.27589938f * f29;
		float f75 = f72 + f73;
		float f76 = f74 - f73;
		float f77 = f45 + f43;
		float f78 = -0.785694957f * f45;
		float f80 = 0.980785251f * f77;
		float f81 = 1.17587554f * f43;
		float f82 = f78 + f80;
		float f83 = f81 - f80;
		float f84 = f50 + f48;
		float f85 = 0.27589938f * f50;
		float f86 = 0.555570245f * f84;
		float f87 = 1.3870399f * f48;
		float f88 = f85 + f86;
		float f89 = f87 - f86;
		float f91 = f44 + f42;
		float f92 = 1.17587554f * f44;
		float f93 = -0.195090324f * f91;
		float f94 = 0.785694957f * f42;
		float f95 = f92 + f93;
		float f96 = f94 - f93;
		float f97 = f49 + f47;
		float f98 = 1.3870399f * f49;
		float f99 = -0.8314696f * f97;
		float f100 = -0.27589938f * f47;
		float f102 = f98 + f99;
		float f103 = f100 - f99;
		float f104 = f17 - f55;
		float f105 = f17 + f55;
		float f106 = f19 - f56;
		float f107 = f19 + f56;
		float f108 = f21 - f62;
		float f109 = f21 + f62;
		float f110 = f23 - f63;
		float f111 = f23 + f63;
		float f114 = f16 - f69;
		float f115 = f16 + f69;
		float f116 = f18 - f70;
		float f117 = f18 + f70;
		float f118 = f20 - f75;
		float f119 = f20 + f75;
		float f120 = f22 - f76;
		float f121 = f22 + f76;
		float f122 = f34 - f82;
		float f123 = f34 + f82;
		float f125 = f37 - f83;
		float f126 = f37 + f83;
		float f127 = f39 - f88;
		float f128 = f39 + f88;
		float f129 = f41 - f89;
		float f130 = f41 + f89;
		float f131 = f33 - f95;
		float f132 = f33 + f95;
		float f133 = f36 - f96;
		float f134 = f36 + f96;
		float f136 = f38 - f102;
		float f137 = f38 + f102;
		float f138 = f40 - f103;
		float f139 = f40 + f103;
		float f140 = f126 + f123;
		float f141 = -0.897167563f * f126;
		float f142 = 0.9951847f * f140;
		float f143 = 1.09320188f * f123;
		float f144 = f141 + f142;
		float f145 = f143 - f142;
		float f147 = f130 + f128;
		float f148 = -0.410524517f * f130;
		float f149 = 0.8819213f * f147;
		float f150 = 1.353318f * f128;
		float f151 = f148 + f149;
		float f152 = f150 - f149;
		float f153 = f134 + f132;
		float f154 = 0.138617173f * f134;
		float f155 = 0.6343933f * f153;
		float f156 = 1.40740371f * f132;
		float f158 = f154 + f155;
		float f159 = f156 - f155;
		float f160 = f139 + f137;
		float f161 = 0.66665566f * f139;
		float f162 = 0.290284663f * f160;
		float f163 = 1.247225f * f137;
		float f164 = f161 + f162;
		float f165 = f163 - f162;
		float f166 = f125 + f122;
		float f167 = 1.09320188f * f125;
		float f169 = -0.09801714f * f166;
		float f170 = 0.897167563f * f122;
		float f171 = f167 + f169;
		float f172 = f170 - f169;
		float f173 = f129 + f127;
		float f174 = 1.353318f * f129;
		float f175 = -0.471396744f * f173;
		float f176 = 0.410524517f * f127;
		float f177 = f174 + f175;
		float f178 = f176 - f175;
		float f180 = f133 + f131;
		float f181 = 1.40740371f * f133;
		float f182 = -0.773010433f * f180;
		float f183 = -0.138617173f * f131;
		float f184 = f181 + f182;
		float f185 = f183 - f182;
		float f186 = f138 + f136;
		float f187 = 1.247225f * f138;
		float f188 = -0.956940353f * f186;
		float f189 = -0.66665566f * f136;
		float f191 = f187 + f188;
		float f192 = f189 - f188;
		float f193 = f105 - f144;
		float f194 = f105 + f144;
		float f195 = f107 - f145;
		float f196 = f107 + f145;
		float f197 = f109 - f151;
		float f198 = f109 + f151;
		float f199 = f111 - f152;
		float f200 = f111 + f152;
		float f202 = f115 - f158;
		float f203 = f115 + f158;
		float f204 = f117 - f159;
		float f205 = f117 + f159;
		float f206 = f119 - f164;
		float f207 = f119 + f164;
		float f208 = f121 - f165;
		float f209 = f121 + f165;
		float f210 = f104 - f171;
		float f211 = f104 + f171;
		float f213 = f106 - f172;
		float f214 = f106 + f172;
		float f215 = f108 - f177;
		float f216 = f108 + f177;
		float f217 = f110 - f178;
		float f218 = f110 + f178;
		float f219 = f114 - f184;
		float f220 = f114 + f184;
		float f221 = f116 - f185;
		float f222 = f116 + f185;
		float f225 = f118 - f191;
		float f226 = f118 + f191;
		float f227 = f120 - f192;
		float f228 = f120 + f192;
		float f229 = f196 + f194;
		float f230 = -0.9751576f * f196;
		float f231 = 0.9996988f * f229;
		float f232 = 1.02424f * f194;
		y[0] = f230 + f231;
		y[31] = f232 - f231;
		float f234 = f200 + f198;
		float f235 = -0.870068848f * f200;
		float f236 = 0.992479563f * f234;
		float f237 = 1.11489022f * f198;
		y[2] = f235 + f236;
		y[29] = f237 - f236;
		float f238 = f205 + f203;
		float f239 = -0.7566009f * f205;
		float f240 = 0.9757021f * f238;
		float f241 = 1.19480336f * f203;
		y[4] = f239 + f240;
		y[27] = f241 - f240;
		float f243 = f209 + f207;
		float f244 = -0.635846436f * f209;
		float f245 = 0.949528158f * f243;
		float f246 = 1.26320994f * f207;
		y[6] = f244 + f245;
		y[25] = f246 - f245;
		float f247 = f214 + f211;
		float f248 = -0.5089684f * f214;
		float f250 = 0.9142098f * f247;
		float f251 = 1.31945109f * f211;
		y[8] = f248 + f250;
		y[23] = f251 - f250;
		float f252 = f218 + f216;
		float f253 = -0.3771888f * f218;
		float f254 = 0.870086968f * f252;
		float f255 = 1.36298513f * f216;
		y[10] = f253 + f254;
		y[21] = f255 - f254;
		float f257 = f222 + f220;
		float f258 = -0.241776615f * f222;
		float f259 = 0.8175848f * f257;
		float f260 = 1.393393f * f220;
		y[12] = f258 + f259;
		y[19] = f260 - f259;
		float f261 = f228 + f226;
		float f262 = -0.104036f * f228;
		float f263 = 0.7572088f * f261;
		float f264 = 1.41038167f * f226;
		y[14] = f262 + f263;
		y[17] = f264 - f263;
		float f266 = f195 + f193;
		float f267 = 0.0347065367f * f195;
		float f268 = 0.689540565f * f266;
		float f269 = 1.4137876f * f193;
		y[16] = f267 + f268;
		y[15] = f269 - f268;
		float f270 = f199 + f197;
		float f271 = 0.173114836f * f199;
		float f273 = 0.6152316f * f270;
		float f274 = 1.403578f * f197;
		y[18] = f271 + f273;
		y[13] = f274 - f273;
		float f275 = f204 + f202;
		float f276 = 0.309855938f * f204;
		float f277 = 0.534997642f * f275;
		float f278 = 1.37985122f * f202;
		y[20] = f276 + f277;
		y[11] = f278 - f277;
		float f280 = f208 + f206;
		float f281 = 0.443612963f * f208;
		float f282 = 0.449611336f * f280;
		float f283 = 1.34283566f * f206;
		y[22] = f281 + f282;
		y[9] = f283 - f282;
		float f284 = f213 + f210;
		float f285 = 0.573097765f * f213;
		float f286 = 0.359895051f * f284;
		float f287 = 1.29288781f * f210;
		y[24] = f285 + f286;
		y[7] = f287 - f286;
		float f289 = f217 + f215;
		float f290 = 0.6970633f * f217;
		float f291 = 0.266712755f * f289;
		float f292 = 1.23048878f * f215;
		y[26] = f290 + f291;
		y[5] = f292 - f291;
		float f293 = f221 + f219;
		float f294 = 0.814315736f * f221;
		float f296 = 0.170961887f * f293;
		float f297 = 1.15623951f * f219;
		y[28] = f294 + f296;
		y[3] = f297 - f296;
		float f298 = f227 + f225;
		float f299 = 0.9237259f * f227;
		float f300 = 0.07356457f * f298;
		float f301 = 1.070855f * f225;
		y[30] = f299 + f300;
		y[1] = f301 - f300;
	}

	[LineNumberTable(new byte[]
	{
		158, 231, 66, 106, 106, 106, 106, 107, 107, 107,
		107, 108, 109, 109, 109, 109, 109, 109, 109, 109,
		109, 109, 109, 109, 109, 109, 109, 109, 109, 109,
		109, 109, 109, 109, 108, 106, 106, 105, 108, 108,
		108, 105, 105, 105, 105, 105, 105, 105, 105, 108,
		104, 104, 108, 105, 105, 105, 108, 108, 108, 105,
		105, 105, 108, 108, 108, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 108, 104, 104, 105, 108, 108, 108, 105, 105,
		105, 105, 105, 105, 108, 105, 105, 105, 108, 108,
		108, 105, 105, 105, 105, 105, 105, 105, 108, 108,
		108, 105, 105, 105, 108, 108, 108, 105, 105, 105,
		108, 108, 108, 105, 105, 105, 108, 108, 108, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 104, 107, 108,
		108, 105, 105, 105, 108, 108, 108, 105, 105, 105,
		108, 108, 108, 105, 105, 105, 108, 108, 108, 105,
		105, 105, 108, 108, 108, 105, 105, 105, 108, 108,
		108, 105, 105, 105, 108, 108, 108, 105, 105, 104,
		108, 108, 107, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 108, 108, 108, 105, 105, 105, 108, 108,
		108, 105, 105, 105, 108, 108, 108, 105, 105, 105,
		108, 108, 108, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 108, 108, 108, 105, 105, 105, 108, 110,
		110, 109, 111, 107, 110, 112, 110, 111, 111, 107,
		110, 112, 110, 111, 111, 107, 107, 107, 107, 109,
		109, 109, 109, 107, 107, 107, 107, 111, 111, 111,
		111, 111, 111, 112, 112, 111, 111, 112, 112, 111,
		111, 112, 112, 111, 111, 112, 112, 109, 109, 109,
		109, 109, 109, 109, 109, 109, 109, 109, 109, 109,
		109, 109, 109, 109, 109, 109, 109, 109, 109, 109,
		109, 109, 109, 109, 109, 109, 109, 109, 109, 112,
		112, 112, 112, 112, 112, 112, 112, 112, 112, 112,
		112, 112, 112, 112, 112, 112, 112, 112, 112, 112,
		112, 112, 111, 111, 111, 111, 111, 111, 111, 111,
		111
	})]
	private void DST4_32(float[] y, float[] x)
	{
		float f0 = x[0] - x[1];
		float f1 = x[2] - x[1];
		float f112 = x[2] - x[3];
		float f223 = x[4] - x[3];
		float f270 = x[4] - x[5];
		float f281 = x[6] - x[5];
		float f292 = x[6] - x[7];
		float f303 = x[8] - x[7];
		float f314 = x[8] - x[9];
		float f325 = x[10] - x[9];
		float f2 = x[10] - x[11];
		float f13 = x[12] - x[11];
		float f24 = x[12] - x[13];
		float f35 = x[14] - x[13];
		float f46 = x[14] - x[15];
		float f57 = x[16] - x[15];
		float f68 = x[16] - x[17];
		float f79 = x[18] - x[17];
		float f90 = x[18] - x[19];
		float f101 = x[20] - x[19];
		float f113 = x[20] - x[21];
		float f124 = x[22] - x[21];
		float f135 = x[22] - x[23];
		float f146 = x[24] - x[23];
		float f157 = x[24] - x[25];
		float f168 = x[26] - x[25];
		float f179 = x[26] - x[27];
		float f190 = x[28] - x[27];
		float f201 = x[28] - x[29];
		float f212 = x[30] - x[29];
		float f224 = x[30] - x[31];
		float f235 = 0.707106769f * f57;
		float f246 = x[0] - f235;
		float f257 = x[0] + f235;
		float f264 = f303 + f146;
		float f265 = 1.306563f * f303;
		float f266 = -0.9238795f * f264;
		float f267 = -0.5411961f * f146;
		float f268 = f265 + f266;
		float f269 = f267 - f266;
		float f271 = f257 - f269;
		float f272 = f257 + f269;
		float f273 = f246 - f268;
		float f274 = f246 + f268;
		float f275 = f13 - f101;
		float f276 = f13 + f101;
		float f277 = 0.707106769f * f276;
		float f278 = f223 - f277;
		float f279 = f223 + f277;
		float f280 = 0.707106769f * f275;
		float f282 = f280 - f190;
		float f283 = f280 + f190;
		float f284 = f283 + f279;
		float f285 = -0.785694957f * f283;
		float f286 = 0.980785251f * f284;
		float f287 = 1.17587554f * f279;
		float f288 = f285 + f286;
		float f289 = f287 - f286;
		float f290 = f282 + f278;
		float f291 = -0.27589938f * f282;
		float f293 = 0.8314696f * f290;
		float f294 = 1.3870399f * f278;
		float f295 = f291 + f293;
		float f296 = f294 - f293;
		float f297 = f272 - f288;
		float f298 = f272 + f288;
		float f299 = f274 - f295;
		float f300 = f274 + f295;
		float f301 = f273 - f296;
		float f302 = f273 + f296;
		float f304 = f271 - f289;
		float f305 = f271 + f289;
		float f306 = f281 - f325;
		float f307 = f281 + f325;
		float f308 = f35 - f79;
		float f309 = f35 + f79;
		float f310 = f124 - f168;
		float f311 = f124 + f168;
		float f312 = 0.707106769f * f309;
		float f313 = f1 - f312;
		float f315 = f1 + f312;
		float f316 = f307 + f311;
		float f317 = 1.306563f * f307;
		float f318 = -0.9238795f * f316;
		float f319 = -0.5411961f * f311;
		float f320 = f317 + f318;
		float f321 = f319 - f318;
		float f322 = f315 - f321;
		float f323 = f315 + f321;
		float f324 = f313 - f320;
		float f326 = f313 + f320;
		float f327 = 0.707106769f * f308;
		float f328 = f212 - f327;
		float f329 = f212 + f327;
		float f330 = f310 + f306;
		float f331 = 1.306563f * f310;
		float f332 = -0.9238795f * f330;
		float f333 = -0.5411961f * f306;
		float f334 = f331 + f332;
		float f335 = f333 - f332;
		float f3 = f329 - f335;
		float f4 = f329 + f335;
		float f5 = f328 - f334;
		float f6 = f328 + f334;
		float f7 = f4 + f323;
		float f8 = -0.897167563f * f4;
		float f9 = 0.9951847f * f7;
		float f10 = 1.09320188f * f323;
		float f11 = f8 + f9;
		float f12 = f10 - f9;
		float f14 = f326 - f6;
		float f15 = -0.66665566f * f6;
		float f16 = 0.956940353f * f14;
		float f17 = 1.247225f * f326;
		float f18 = f16 - f15;
		float f19 = f17 - f16;
		float f20 = f5 + f324;
		float f21 = -0.410524517f * f5;
		float f22 = 0.8819213f * f20;
		float f23 = 1.353318f * f324;
		float f25 = f21 + f22;
		float f26 = f23 - f22;
		float f27 = f322 - f3;
		float f28 = -0.138617173f * f3;
		float f29 = 0.773010433f * f27;
		float f30 = 1.40740371f * f322;
		float f31 = f29 - f28;
		float f32 = f30 - f29;
		float f33 = f298 - f11;
		float f34 = f298 + f11;
		float f36 = f300 - f18;
		float f37 = f300 + f18;
		float f38 = f302 - f25;
		float f39 = f302 + f25;
		float f40 = f305 - f31;
		float f41 = f305 + f31;
		float f42 = f304 - f32;
		float f43 = f304 + f32;
		float f44 = f301 - f26;
		float f45 = f301 + f26;
		float f47 = f299 - f19;
		float f48 = f299 + f19;
		float f49 = f297 - f12;
		float f50 = f297 + f12;
		float f51 = f0 + f224;
		float f52 = 1.04786313f * f0;
		float f53 = -0.99879545f * f51;
		float f54 = -0.9497278f * f224;
		float f55 = f52 + f53;
		float f56 = f54 - f53;
		float f58 = f270 + f179;
		float f59 = 1.21301138f * f270;
		float f60 = -0.970031261f * f58;
		float f61 = -0.7270511f * f179;
		float f62 = f59 + f60;
		float f63 = f61 - f60;
		float f64 = f314 + f135;
		float f65 = 1.3315444f * f314;
		float f66 = -0.9039893f * f64;
		float f67 = -0.4764342f * f135;
		float f69 = f65 + f66;
		float f70 = f67 - f66;
		float f71 = f24 + f90;
		float f72 = 1.39890683f * f24;
		float f73 = -0.8032075f * f71;
		float f74 = -0.207508221f * f90;
		float f75 = f72 + f73;
		float f76 = f74 - f73;
		float f77 = f68 + f46;
		float f78 = 1.41251f * f68;
		float f80 = -0.671559f * f77;
		float f81 = 0.06939217f * f46;
		float f82 = f78 + f80;
		float f83 = f81 - f80;
		float f84 = f113 + f2;
		float f85 = 1.3718313f * f113;
		float f86 = -0.514102757f * f84;
		float f87 = 0.343625873f * f2;
		float f88 = f85 + f86;
		float f89 = f87 - f86;
		float f91 = f157 + f292;
		float f92 = 1.27843392f * f157;
		float f93 = -0.336889863f * f91;
		float f94 = 0.6046542f * f292;
		float f95 = f92 + f93;
		float f96 = f94 - f93;
		float f97 = f201 + f112;
		float f98 = 1.13590693f * f201;
		float f99 = -0.146730468f * f97;
		float f100 = 0.842446f * f112;
		float f102 = f98 + f99;
		float f103 = f100 - f99;
		float f104 = f56 - f83;
		float f105 = f56 + f83;
		float f106 = f55 - f82;
		float f107 = f55 + f82;
		float f108 = f63 - f89;
		float f109 = f63 + f89;
		float f110 = f62 - f88;
		float f111 = f62 + f88;
		float f114 = f70 - f96;
		float f115 = f70 + f96;
		float f116 = f69 - f95;
		float f117 = f69 + f95;
		float f118 = f76 - f103;
		float f119 = f76 + f103;
		float f120 = f75 - f102;
		float f121 = f75 + f102;
		float f122 = f104 + f106;
		float f123 = 1.17587554f * f104;
		float f125 = -0.980785251f * f122;
		float f126 = -0.785694957f * f106;
		float f127 = f123 + f125;
		float f128 = f126 - f125;
		float f129 = f108 + f110;
		float f130 = 1.3870399f * f108;
		float f131 = -0.555570245f * f129;
		float f132 = 0.27589938f * f110;
		float f133 = f130 + f131;
		float f134 = f132 - f131;
		float f136 = f114 + f116;
		float f137 = 0.785694957f * f114;
		float f138 = 0.195090324f * f136;
		float f139 = 1.17587554f * f116;
		float f140 = f137 + f138;
		float f141 = f139 - f138;
		float f142 = f118 + f120;
		float f143 = -0.27589938f * f118;
		float f144 = 0.8314696f * f142;
		float f145 = 1.3870399f * f120;
		float f147 = f143 + f144;
		float f148 = f145 - f144;
		float f149 = f105 - f115;
		float f150 = f105 + f115;
		float f151 = f107 - f117;
		float f152 = f107 + f117;
		float f153 = f109 - f119;
		float f154 = f109 + f119;
		float f155 = f111 - f121;
		float f156 = f111 + f121;
		float f158 = f128 - f141;
		float f159 = f128 + f141;
		float f160 = f127 - f140;
		float f161 = f127 + f140;
		float f162 = f134 - f148;
		float f163 = f134 + f148;
		float f164 = f133 - f147;
		float f165 = f133 + f147;
		float f166 = f149 + f151;
		float f167 = 1.306563f * f149;
		float f169 = -0.9238795f * f166;
		float f170 = -0.5411961f * f151;
		float f171 = f167 + f169;
		float f172 = f170 - f169;
		float f173 = f153 + f155;
		float f174 = 0.5411961f * f153;
		float f175 = 0.382683426f * f173;
		float f176 = 1.306563f * f155;
		float f177 = f174 + f175;
		float f178 = f176 - f175;
		float f180 = f158 + f160;
		float f181 = 1.306563f * f158;
		float f182 = -0.9238795f * f180;
		float f183 = -0.5411961f * f160;
		float f184 = f181 + f182;
		float f185 = f183 - f182;
		float f186 = f162 + f164;
		float f187 = 0.5411961f * f162;
		float f188 = 0.382683426f * f186;
		float f189 = 1.306563f * f164;
		float f191 = f187 + f188;
		float f192 = f189 - f188;
		float f193 = f150 - f154;
		float f194 = f150 + f154;
		float f195 = f152 - f156;
		float f196 = f152 + f156;
		float f197 = f172 - f178;
		float f198 = f172 + f178;
		float f199 = f171 - f177;
		float f200 = f171 + f177;
		float f202 = f159 - f163;
		float f203 = f159 + f163;
		float f204 = f161 - f165;
		float f205 = f161 + f165;
		float f206 = f185 - f192;
		float f207 = f185 + f192;
		float f208 = f184 - f191;
		float f209 = f184 + f191;
		float f210 = f193 - f195;
		float f211 = f193 + f195;
		float f213 = 0.707106769f * f210;
		float f214 = 0.707106769f * f211;
		float f215 = f197 - f199;
		float f216 = f197 + f199;
		float f217 = 0.707106769f * f215;
		float f218 = 0.707106769f * f216;
		float f219 = f202 - f204;
		float f220 = f202 + f204;
		float f221 = 0.707106769f * f219;
		float f222 = 0.707106769f * f220;
		float f225 = f206 - f208;
		float f226 = f206 + f208;
		float f227 = 0.707106769f * f225;
		float f228 = 0.707106769f * f226;
		float f229 = f34 - f194;
		float f230 = f34 + f194;
		float f231 = f37 - f203;
		float f232 = f37 + f203;
		float f233 = f39 - f207;
		float f234 = f39 + f207;
		float f236 = f41 - f198;
		float f237 = f41 + f198;
		float f238 = f43 - f218;
		float f239 = f43 + f218;
		float f240 = f45 - f228;
		float f241 = f45 + f228;
		float f242 = f48 - f222;
		float f243 = f48 + f222;
		float f244 = f50 - f214;
		float f245 = f50 + f214;
		float f247 = f49 - f213;
		float f248 = f49 + f213;
		float f249 = f47 - f221;
		float f250 = f47 + f221;
		float f251 = f44 - f227;
		float f252 = f44 + f227;
		float f253 = f42 - f217;
		float f254 = f42 + f217;
		float f255 = f40 - f200;
		float f256 = f40 + f200;
		float f258 = f38 - f209;
		float f259 = f38 + f209;
		float f260 = f36 - f205;
		float f261 = f36 + f205;
		float f262 = f33 - f196;
		float f263 = f33 + f196;
		y[31] = 0.5001506f * f230;
		y[30] = 0.501358449f * f232;
		y[29] = 0.5037887f * f234;
		y[28] = 0.507471144f * f237;
		y[27] = 0.51245147f * f239;
		y[26] = 0.5187927f * f241;
		y[25] = 0.5265773f * f243;
		y[24] = 0.535909832f * f245;
		y[23] = 0.5469204f * f248;
		y[22] = 0.5597698f * f250;
		y[21] = 0.5746552f * f252;
		y[20] = 0.5918185f * f254;
		y[19] = 0.611557364f * f256;
		y[18] = 0.634238958f * f259;
		y[17] = 0.6603198f * f261;
		y[16] = 0.6903721f * f263;
		y[15] = 0.725120544f * f262;
		y[14] = 0.765494168f * f260;
		y[13] = 0.8127021f * f258;
		y[12] = 0.8683447f * f255;
		y[11] = 0.9345836f * f253;
		y[10] = 1.01440823f * f251;
		y[9] = 1.11207163f * f249;
		y[8] = 1.23383272f * f247;
		y[7] = 1.38929391f * f244;
		y[6] = 1.59397233f * f242;
		y[5] = 1.874676f * f240;
		y[4] = 2.28205013f * f238;
		y[3] = 2.9246285f * f236;
		y[2] = 4.084611f * f233;
		y[1] = 6.79675055f * f231;
		y[0] = 20.3738785f * f229;
	}

	[LineNumberTable(13)]
	static SynthesisFilterbank()
	{
		qmf32_pre_twiddle = new float[32][]
		{
			new float[2] { 0.9999247f, -0.0122715384f },
			new float[2] { 0.999322355f, -0.0368072242f },
			new float[2] { 0.9981181f, -0.061320737f },
			new float[2] { 0.9963126f, -0.08579731f },
			new float[2] { 0.993907f, -0.110222206f },
			new float[2] { 0.990902662f, -0.1345807f },
			new float[2] { 0.9873014f, -0.15885815f },
			new float[2] { 0.9831055f, -0.183039889f },
			new float[2] { 0.9783174f, -0.207111374f },
			new float[2] { 0.972939968f, -0.2310581f },
			new float[2] { 0.966976464f, -0.254865646f },
			new float[2] { 0.9604305f, -0.2785197f },
			new float[2] { 0.953306f, -0.302005947f },
			new float[2] { 0.9456073f, -0.3253103f },
			new float[2] { 0.937339f, -0.348418683f },
			new float[2] { 0.9285061f, -0.3713172f },
			new float[2] { 0.9191139f, -0.393992037f },
			new float[2] { 0.909168f, -0.416429549f },
			new float[2] { 0.8986745f, -0.438616246f },
			new float[2] { 0.887639642f, -0.460538715f },
			new float[2] { 0.8760701f, -0.482183784f },
			new float[2] { 0.863972843f, -0.50353837f },
			new float[2] { 0.8513552f, -0.524589658f },
			new float[2] { 0.8382247f, -0.545325f },
			new float[2] { 0.8245893f, -0.5657318f },
			new float[2] { 0.81045717f, -0.585797846f },
			new float[2] { 0.7958369f, -0.605511069f },
			new float[2] { 0.7807372f, -0.6248595f },
			new float[2] { 0.765167236f, -0.643831551f },
			new float[2] { 0.7491364f, -0.6624158f },
			new float[2] { 0.7326543f, -0.680601f },
			new float[2] { 0.715730846f, -0.698376238f }
		};
	}
}
