using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace net.sourceforge.jaad.aac.ps;

[Implements(new string[] { "net.sourceforge.jaad.aac.ps.PSTables" })]
internal class PSFilterbank : Object, PSTables, PSConstants
{
	private int frame_len;

	private int[] resolution20;

	private int[] resolution34;

	private float[][] work;

	private float[][][] buffer;

	private float[][][] temp;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 66, 105, 109, 205, 107, 106, 106, 106,
		138, 106, 106, 138, 136, 159, 19, 159, 17, 127,
		23
	})]
	internal PSFilterbank(int numTimeSlotsRate)
	{
		resolution20 = new int[3];
		resolution34 = new int[5];
		resolution34[0] = 12;
		resolution34[1] = 8;
		resolution34[2] = 4;
		resolution34[3] = 4;
		resolution34[4] = 4;
		resolution20[0] = 8;
		resolution20[1] = 2;
		resolution20[2] = 2;
		frame_len = numTimeSlotsRate;
		int num = frame_len + 12;
		int[] array = new int[2];
		int num2 = (array[1] = 2);
		num2 = (array[0] = num);
		work = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[3];
		num2 = (array[2] = 2);
		num2 = (array[1] = 2);
		num2 = (array[0] = 5);
		buffer = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		int num3 = frame_len;
		array = new int[3];
		num2 = (array[2] = 2);
		num2 = (array[1] = 12);
		num2 = (array[0] = num3);
		temp = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 161, 67, 99, 105, 147, 172, 106, 122,
		26, 233, 70, 110, 122, 26, 233, 71, 106, 127,
		2, 31, 2, 233, 69, 191, 12, 125, 163, 125,
		163, 159, 9, 163, 222, 113, 108, 122, 26, 41,
		236, 70, 232, 17, 236, 115, 103, 109, 127, 1,
		127, 1, 110, 142, 127, 1, 127, 1, 110, 238,
		55, 236, 76
	})]
	internal virtual void hybrid_analysis(float[][][] X, float[][][] X_hybrid, bool use34, int numTimeSlotsRate)
	{
		int offset = 0;
		int qmf_bands = ((!use34) ? 3 : 5);
		int[] resolution = ((!use34) ? resolution20 : resolution34);
		for (int band = 0; band < qmf_bands; band++)
		{
			for (int j = 0; j < 12; j++)
			{
				work[j][0] = buffer[band][j][0];
				work[j][1] = buffer[band][j][1];
			}
			for (int m = 0; m < frame_len; m++)
			{
				work[12 + m][0] = X[m + 6][band][0];
				work[12 + m][0] = X[m + 6][band][0];
			}
			for (int i = 0; i < 12; i++)
			{
				buffer[band][i][0] = work[frame_len + i][0];
				buffer[band][i][1] = work[frame_len + i][1];
			}
			switch (resolution[band])
			{
			case 2:
				channel_filter2(frame_len, PSTables.p2_13_20, work, temp);
				break;
			case 4:
				channel_filter4(frame_len, PSTables.p4_13_34, work, temp);
				break;
			case 8:
				channel_filter8(frame_len, (!use34) ? PSTables.p8_13_20 : PSTables.p8_13_34, work, temp);
				break;
			case 12:
				channel_filter12(frame_len, PSTables.p12_13_34, work, temp);
				break;
			}
			for (int m = 0; m < frame_len; m++)
			{
				for (int k = 0; k < resolution[band]; k++)
				{
					X_hybrid[m][offset + k][0] = temp[m][k][0];
					X_hybrid[m][offset + k][1] = temp[m][k][1];
				}
			}
			offset += resolution[band];
		}
		if (!use34)
		{
			for (int l = 0; l < numTimeSlotsRate; l++)
			{
				float[] obj = X_hybrid[l][3];
				int num = 0;
				float[] array = obj;
				array[num] += X_hybrid[l][4][0];
				float[] obj2 = X_hybrid[l][3];
				num = 1;
				array = obj2;
				array[num] += X_hybrid[l][4][1];
				X_hybrid[l][4][0] = 0f;
				X_hybrid[l][4][1] = 0f;
				float[] obj3 = X_hybrid[l][2];
				num = 0;
				array = obj3;
				array[num] += X_hybrid[l][5][0];
				float[] obj4 = X_hybrid[l][2];
				num = 1;
				array = obj4;
				array[num] += X_hybrid[l][5][1];
				X_hybrid[l][5][0] = 0f;
				X_hybrid[l][5][1] = 0f;
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 54, 65, 67, 99, 105, 147, 108, 113, 111,
		143, 111, 127, 5, 31, 5, 236, 60, 236, 73,
		232, 54, 236, 76
	})]
	internal virtual void hybrid_synthesis(float[][][] X, float[][][] X_hybrid, bool use34, int numTimeSlotsRate)
	{
		int offset = 0;
		int qmf_bands = ((!use34) ? 3 : 5);
		int[] resolution = ((!use34) ? resolution20 : resolution34);
		for (int band = 0; band < qmf_bands; band++)
		{
			for (int j = 0; j < frame_len; j++)
			{
				X[j][band][0] = 0f;
				X[j][band][1] = 0f;
				for (int i = 0; i < resolution[band]; i++)
				{
					float[] obj = X[j][band];
					int num = 0;
					float[] array = obj;
					array[num] += X_hybrid[j][offset + i][0];
					float[] obj2 = X[j][band];
					num = 1;
					array = obj2;
					array[num] += X_hybrid[j][offset + i][1];
				}
			}
			offset += resolution[band];
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		112,
		130,
		106,
		120,
		120,
		120,
		121,
		120,
		120,
		111,
		121,
		121,
		121,
		121,
		120,
		120,
		175,
		127,
		0,
		191,
		3,
		127,
		0,
		byte.MaxValue,
		3,
		42,
		234,
		88
	})]
	internal static void channel_filter2(int frame_len, float[] filter, float[][] buffer, float[][][] X_hybrid)
	{
		for (int i = 0; i < frame_len; i++)
		{
			float r0 = filter[0] * (buffer[0 + i][0] + buffer[12 + i][0]);
			float r1 = filter[1] * (buffer[1 + i][0] + buffer[11 + i][0]);
			float r2 = filter[2] * (buffer[2 + i][0] + buffer[10 + i][0]);
			float r3 = filter[3] * (buffer[3 + i][0] + buffer[9 + i][0]);
			float r4 = filter[4] * (buffer[4 + i][0] + buffer[8 + i][0]);
			float r5 = filter[5] * (buffer[5 + i][0] + buffer[7 + i][0]);
			float r6 = filter[6] * buffer[6 + i][0];
			float i2 = filter[0] * (buffer[0 + i][1] + buffer[12 + i][1]);
			float i3 = filter[1] * (buffer[1 + i][1] + buffer[11 + i][1]);
			float i4 = filter[2] * (buffer[2 + i][1] + buffer[10 + i][1]);
			float i5 = filter[3] * (buffer[3 + i][1] + buffer[9 + i][1]);
			float i6 = filter[4] * (buffer[4 + i][1] + buffer[8 + i][1]);
			float i7 = filter[5] * (buffer[5 + i][1] + buffer[7 + i][1]);
			float i8 = filter[6] * buffer[6 + i][1];
			X_hybrid[i][0][0] = r0 + r1 + r2 + r3 + r4 + r5 + r6;
			X_hybrid[i][0][1] = i2 + i3 + i4 + i5 + i6 + i7 + i8;
			X_hybrid[i][1][0] = r0 - r1 + r2 - r3 + r4 - r5 + r6;
			X_hybrid[i][1][1] = i2 - i3 + i4 - i5 + i6 - i7 + i8;
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		104,
		66,
		111,
		143,
		108,
		159,
		13,
		byte.MaxValue,
		55,
		69,
		159,
		22,
		byte.MaxValue,
		55,
		69,
		159,
		22,
		byte.MaxValue,
		55,
		69,
		159,
		13,
		byte.MaxValue,
		55,
		70,
		123,
		188,
		123,
		187,
		123,
		188,
		123,
		251,
		21,
		236,
		109
	})]
	internal static void channel_filter4(int frame_len, float[] filter, float[][] buffer, float[][][] X_hybrid)
	{
		float[] input_re1 = new float[2];
		float[] input_re2 = new float[2];
		float[] input_im1 = new float[2];
		float[] input_im2 = new float[2];
		for (int i = 0; i < frame_len; i++)
		{
			input_re1[0] = 0f - filter[2] * (buffer[i + 2][0] + buffer[i + 10][0]) + filter[6] * buffer[i + 6][0];
			input_re1[1] = -0.707106769f * (filter[1] * (buffer[i + 1][0] + buffer[i + 11][0]) + filter[3] * (buffer[i + 3][0] + buffer[i + 9][0]) - filter[5] * (buffer[i + 5][0] + buffer[i + 7][0]));
			input_im1[0] = filter[0] * (buffer[i + 0][1] - buffer[i + 12][1]) - filter[4] * (buffer[i + 4][1] - buffer[i + 8][1]);
			input_im1[1] = 0.707106769f * (filter[1] * (buffer[i + 1][1] - buffer[i + 11][1]) - filter[3] * (buffer[i + 3][1] - buffer[i + 9][1]) - filter[5] * (buffer[i + 5][1] - buffer[i + 7][1]));
			input_re2[0] = filter[0] * (buffer[i + 0][0] - buffer[i + 12][0]) - filter[4] * (buffer[i + 4][0] - buffer[i + 8][0]);
			input_re2[1] = 0.707106769f * (filter[1] * (buffer[i + 1][0] - buffer[i + 11][0]) - filter[3] * (buffer[i + 3][0] - buffer[i + 9][0]) - filter[5] * (buffer[i + 5][0] - buffer[i + 7][0]));
			input_im2[0] = 0f - filter[2] * (buffer[i + 2][1] + buffer[i + 10][1]) + filter[6] * buffer[i + 6][1];
			input_im2[1] = -0.707106769f * (filter[1] * (buffer[i + 1][1] + buffer[i + 11][1]) + filter[3] * (buffer[i + 3][1] + buffer[i + 9][1]) - filter[5] * (buffer[i + 5][1] + buffer[i + 7][1]));
			X_hybrid[i][0][0] = input_re1[0] + input_re1[1] + input_im1[0] + input_im1[1];
			X_hybrid[i][0][1] = 0f - input_re2[0] - input_re2[1] + input_im2[0] + input_im2[1];
			X_hybrid[i][1][0] = input_re1[0] - input_re1[1] - input_im1[0] + input_im1[1];
			X_hybrid[i][1][1] = input_re2[0] - input_re2[1] + input_im2[0] - input_im2[1];
			X_hybrid[i][2][0] = input_re1[0] - input_re1[1] + input_im1[0] - input_im1[1];
			X_hybrid[i][2][1] = 0f - input_re2[0] + input_re2[1] + input_im2[0] - input_im2[1];
			X_hybrid[i][3][0] = input_re1[0] + input_re1[1] - input_im1[0] - input_im1[1];
			X_hybrid[i][3][1] = input_re2[0] + input_re2[1] + input_im2[0] + input_im2[1];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 86, 66, 111, 111, 137, 108, 113, 123, 127,
		23, 159, 24, 123, 127, 22, 127, 23, 156, 105,
		50, 169, 106, 110, 110, 110, 142, 105, 50, 169,
		106, 110, 110, 110, 142, 113, 123, 127, 23, 159,
		24, 123, 127, 22, 127, 23, 156, 105, 50, 169,
		106, 110, 110, 110, 142, 105, 50, 169, 106, 110,
		110, 110, 238, 9, 236, 121
	})]
	internal virtual void channel_filter8(int frame_len, float[] filter, float[][] buffer, float[][][] X_hybrid)
	{
		float[] input_re1 = new float[4];
		float[] input_re2 = new float[4];
		float[] input_im1 = new float[4];
		float[] input_im2 = new float[4];
		float[] x = new float[4];
		for (int i = 0; i < frame_len; i++)
		{
			input_re1[0] = filter[6] * buffer[6 + i][0];
			input_re1[1] = filter[5] * (buffer[5 + i][0] + buffer[7 + i][0]);
			input_re1[2] = 0f - filter[0] * (buffer[0 + i][0] + buffer[12 + i][0]) + filter[4] * (buffer[4 + i][0] + buffer[8 + i][0]);
			input_re1[3] = 0f - filter[1] * (buffer[1 + i][0] + buffer[11 + i][0]) + filter[3] * (buffer[3 + i][0] + buffer[9 + i][0]);
			input_im1[0] = filter[5] * (buffer[7 + i][1] - buffer[5 + i][1]);
			input_im1[1] = filter[0] * (buffer[12 + i][1] - buffer[0 + i][1]) + filter[4] * (buffer[8 + i][1] - buffer[4 + i][1]);
			input_im1[2] = filter[1] * (buffer[11 + i][1] - buffer[1 + i][1]) + filter[3] * (buffer[9 + i][1] - buffer[3 + i][1]);
			input_im1[3] = filter[2] * (buffer[10 + i][1] - buffer[2 + i][1]);
			for (int j = 0; j < 4; j++)
			{
				x[j] = input_re1[j] - input_im1[3 - j];
			}
			DCT3_4_unscaled(x, x);
			X_hybrid[i][7][0] = x[0];
			X_hybrid[i][5][0] = x[2];
			X_hybrid[i][3][0] = x[3];
			X_hybrid[i][1][0] = x[1];
			for (int j = 0; j < 4; j++)
			{
				x[j] = input_re1[j] + input_im1[3 - j];
			}
			DCT3_4_unscaled(x, x);
			X_hybrid[i][6][0] = x[1];
			X_hybrid[i][4][0] = x[3];
			X_hybrid[i][2][0] = x[2];
			X_hybrid[i][0][0] = x[0];
			input_im2[0] = filter[6] * buffer[6 + i][1];
			input_im2[1] = filter[5] * (buffer[5 + i][1] + buffer[7 + i][1]);
			input_im2[2] = 0f - filter[0] * (buffer[0 + i][1] + buffer[12 + i][1]) + filter[4] * (buffer[4 + i][1] + buffer[8 + i][1]);
			input_im2[3] = 0f - filter[1] * (buffer[1 + i][1] + buffer[11 + i][1]) + filter[3] * (buffer[3 + i][1] + buffer[9 + i][1]);
			input_re2[0] = filter[5] * (buffer[7 + i][0] - buffer[5 + i][0]);
			input_re2[1] = filter[0] * (buffer[12 + i][0] - buffer[0 + i][0]) + filter[4] * (buffer[8 + i][0] - buffer[4 + i][0]);
			input_re2[2] = filter[1] * (buffer[11 + i][0] - buffer[1 + i][0]) + filter[3] * (buffer[9 + i][0] - buffer[3 + i][0]);
			input_re2[3] = filter[2] * (buffer[10 + i][0] - buffer[2 + i][0]);
			for (int j = 0; j < 4; j++)
			{
				x[j] = input_im2[j] + input_re2[3 - j];
			}
			DCT3_4_unscaled(x, x);
			X_hybrid[i][7][1] = x[0];
			X_hybrid[i][5][1] = x[2];
			X_hybrid[i][3][1] = x[3];
			X_hybrid[i][1][1] = x[1];
			for (int j = 0; j < 4; j++)
			{
				x[j] = input_im2[j] - input_re2[3 - j];
			}
			DCT3_4_unscaled(x, x);
			X_hybrid[i][6][1] = x[1];
			X_hybrid[i][4][1] = x[3];
			X_hybrid[i][2][1] = x[2];
			X_hybrid[i][0][1] = x[0];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		65,
		130,
		111,
		111,
		113,
		145,
		108,
		108,
		101,
		113,
		179,
		127,
		5,
		159,
		5,
		127,
		3,
		byte.MaxValue,
		3,
		54,
		236,
		77,
		106,
		138,
		106,
		138,
		108,
		119,
		119,
		125,
		157,
		126,
		126,
		122,
		250,
		55,
		236,
		44,
		236,
		96
	})]
	internal virtual void channel_filter12(int frame_len, float[] filter, float[][] buffer, float[][][] X_hybrid)
	{
		float[] input_re1 = new float[6];
		float[] input_re2 = new float[6];
		float[] input_im1 = new float[6];
		float[] input_im2 = new float[6];
		float[] out_re1 = new float[6];
		float[] out_re2 = new float[6];
		float[] out_im1 = new float[6];
		float[] out_im2 = new float[6];
		for (int i = 0; i < frame_len; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 0)
				{
					input_re1[0] = buffer[6 + i][0] * filter[6];
					input_re2[0] = buffer[6 + i][1] * filter[6];
				}
				else
				{
					input_re1[6 - j] = (buffer[j + i][0] + buffer[12 - j + i][0]) * filter[j];
					input_re2[6 - j] = (buffer[j + i][1] + buffer[12 - j + i][1]) * filter[j];
				}
				input_im2[j] = (buffer[j + i][0] - buffer[12 - j + i][0]) * filter[j];
				input_im1[j] = (buffer[j + i][1] - buffer[12 - j + i][1]) * filter[j];
			}
			DCT3_6_unscaled(out_re1, input_re1);
			DCT3_6_unscaled(out_re2, input_re2);
			DCT3_6_unscaled(out_im1, input_im1);
			DCT3_6_unscaled(out_im2, input_im2);
			for (int j = 0; j < 6; j += 2)
			{
				X_hybrid[i][j][0] = out_re1[j] - out_im1[j];
				X_hybrid[i][j][1] = out_re2[j] + out_im2[j];
				X_hybrid[i][j + 1][0] = out_re1[j + 1] + out_im1[j + 1];
				X_hybrid[i][j + 1][1] = out_re2[j + 1] - out_im2[j + 1];
				X_hybrid[i][10 - j][0] = out_re1[j + 1] - out_im1[j + 1];
				X_hybrid[i][10 - j][1] = out_re2[j + 1] + out_im2[j + 1];
				X_hybrid[i][11 - j][0] = out_re1[j] + out_im1[j];
				X_hybrid[i][11 - j][1] = out_re2[j] - out_im2[j];
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 91, 98, 108, 104, 104, 106, 109, 107, 109,
		105, 105, 105, 105, 105, 105
	})]
	internal static void DCT3_4_unscaled(float[] y, float[] x)
	{
		float f0 = x[2] * 0.707106769f;
		float f1 = x[0] - f0;
		float f2 = x[0] + f0;
		float f3 = x[1] + x[3];
		float f4 = x[1] * 1.306563f;
		float f5 = f3 * -0.9238795f;
		float f6 = x[3] * -0.5411961f;
		float f7 = f4 + f5;
		float f8 = f6 - f5;
		y[3] = f2 - f8;
		y[0] = f2 + f8;
		y[2] = f1 - f7;
		y[1] = f1 + f7;
	}

	[LineNumberTable(new byte[]
	{
		159, 70, 130, 108, 104, 104, 113, 121, 106, 121,
		104, 109, 109, 109, 109, 109, 109
	})]
	internal virtual void DCT3_6_unscaled(float[] y, float[] x)
	{
		float f0 = x[3] * 0.707106769f;
		float f1 = x[0] + f0;
		float f2 = x[0] - f0;
		float f3 = (x[1] - x[5]) * 0.707106769f;
		float f4 = x[2] * 0.8660254f + x[4] * 0.5f;
		float f5 = f4 - x[4];
		float f6 = x[1] * 0.9659258f + x[5] * 0.258819044f;
		float f7 = f6 - f3;
		y[0] = f1 + f6 + f4;
		y[1] = f2 + f3 - x[4];
		y[2] = f7 + f2 - f5;
		y[3] = f1 - f7 - f5;
		y[4] = f1 - f3 - x[4];
		y[5] = f2 - f6 + f4;
	}
}
