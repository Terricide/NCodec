using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace net.sourceforge.jaad.aac.sbr;

internal class AnalysisFilterbank : Object, FilterbankTable
{
	private float[] x;

	private int x_index;

	private int channels;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 105, 104, 114, 104 })]
	internal AnalysisFilterbank(int channels)
	{
		this.channels = channels;
		x = new float[2 * channels * 10];
		x_index = 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 115 })]
	public virtual void reset()
	{
		Arrays.fill(x, 0f);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 130, 105, 113, 114, 196, 241, 72, 106,
		63, 32, 233, 69, 109, 63, 160, 100, 236, 73,
		112, 106, 204, 104, 103, 106, 110, 13, 201, 104,
		170, 171, 109, 110, 122, 123, 127, 1, 191, 5,
		105, 122, 189, 116, 148, 118, 246, 47, 236, 23,
		236, 126
	})]
	internal virtual void sbr_qmf_analysis_32(SBR sbr, float[] input, float[][][] X, int offset, int kx)
	{
		float[] u = new float[64];
		float[] in_real = new float[32];
		float[] in_imag = new float[32];
		float[] out_real = new float[32];
		float[] out_imag = new float[32];
		int _in = 0;
		for (int i = 0; i < sbr.numTimeSlotsRate; i++)
		{
			for (int j = 31; j >= 0; j += -1)
			{
				float[] array = x;
				int num = x_index + j;
				float[] array2 = x;
				int num2 = x_index + j + 320;
				int num3 = _in;
				_in++;
				float num4 = input[num3];
				int num5 = num2;
				float[] array3 = array2;
				array3[num5] = num4;
				array[num] = num4;
			}
			for (int j = 0; j < 64; j++)
			{
				u[j] = x[x_index + j] * FilterbankTable.qmf_c[2 * j] + x[x_index + j + 64] * FilterbankTable.qmf_c[2 * (j + 64)] + x[x_index + j + 128] * FilterbankTable.qmf_c[2 * (j + 128)] + x[x_index + j + 192] * FilterbankTable.qmf_c[2 * (j + 192)] + x[x_index + j + 256] * FilterbankTable.qmf_c[2 * (j + 256)];
			}
			x_index -= 32;
			if (x_index < 0)
			{
				x_index = 288;
			}
			in_imag[31] = u[1];
			in_real[0] = u[0];
			for (int j = 1; j < 31; j++)
			{
				in_imag[31 - j] = u[j + 1];
				in_real[j] = 0f - u[64 - j];
			}
			in_imag[0] = u[32];
			in_real[31] = 0f - u[33];
			DCT.dct4_kernel(in_real, in_imag, out_real, out_imag);
			for (int j = 0; j < 16; j++)
			{
				if (2 * j + 1 < kx)
				{
					X[i + offset][2 * j][0] = 2f * out_real[j];
					X[i + offset][2 * j][1] = 2f * out_imag[j];
					X[i + offset][2 * j + 1][0] = -2f * out_imag[31 - j];
					X[i + offset][2 * j + 1][1] = -2f * out_real[31 - j];
					continue;
				}
				if (2 * j < kx)
				{
					X[i + offset][2 * j][0] = 2f * out_real[j];
					X[i + offset][2 * j][1] = 2f * out_imag[j];
				}
				else
				{
					X[i + offset][2 * j][0] = 0f;
					X[i + offset][2 * j][1] = 0f;
				}
				X[i + offset][2 * j + 1][0] = 0f;
				X[i + offset][2 * j + 1][1] = 0f;
			}
		}
	}
}
