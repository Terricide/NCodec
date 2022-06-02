using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace org.jcodec.common.dct;

public class DCTRef : java.lang.Object
{
	internal static double[] coefficients;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(13)]
	public DCTRef()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 135, 66, 137, 107, 106, 103, 105, 59, 169,
		242, 59, 42, 234, 74, 108, 107, 104, 105, 59,
		169, 250, 59, 42, 236, 73
	})]
	public static void fdct(int[] block, int off)
	{
		double[] @out = new double[64];
		for (int i = 0; i < 64; i += 8)
		{
			for (int k = 0; k < 8; k++)
			{
				double tmp2 = 0.0;
				for (int m = 0; m < 8; m++)
				{
					tmp2 += coefficients[i + m] * (double)block[m * 8 + k + off];
				}
				@out[i + k] = tmp2 * 4.0;
			}
		}
		for (int j = 0; j < 8; j++)
		{
			for (int i = 0; i < 64; i += 8)
			{
				double tmp = 0.0;
				for (int l = 0; l < 8; l++)
				{
					tmp += @out[i + l] * coefficients[j * 8 + l];
				}
				block[i + j + off] = ByteCodeHelper.d2i(tmp + 0.499999999999);
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 129, 130, 169, 107, 103, 103, 105, 57, 169,
		231, 59, 39, 234, 75, 106, 108, 104, 106, 57,
		169, 250, 59, 44, 234, 73
	})]
	public static void idct(int[] block, int off)
	{
		double[] @out = new double[64];
		for (int i = 0; i < 64; i += 8)
		{
			for (int k = 0; k < 8; k++)
			{
				double tmp2 = 0.0;
				for (int m = 0; m < 8; m++)
				{
					tmp2 += (double)block[i + m] * coefficients[m * 8 + k];
				}
				@out[i + k] = tmp2;
			}
		}
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				double tmp = 0.0;
				for (int l = 0; l < 64; l += 8)
				{
					tmp += coefficients[l + i] * @out[l + j];
				}
				block[i * 8 + j] = ByteCodeHelper.d2i(tmp + 0.5);
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 139, 162, 173, 106, 119, 104, 63, 30, 7,
		234, 70
	})]
	static DCTRef()
	{
		coefficients = new double[64];
		for (int j = 0; j < 8; j++)
		{
			coefficients[j] = java.lang.Math.sqrt(0.125);
			for (int i = 8; i < 64; i += 8)
			{
				coefficients[i + j] = 0.5 * java.lang.Math.cos((double)i * ((double)j + 0.5) * System.Math.PI / 64.0);
			}
		}
	}
}
