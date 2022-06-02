using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.scale;

namespace org.jcodec.common.dct;

public class SlowDCT : DCT
{
	internal static SlowDCT ___003C_003EINSTANCE;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static double rSqrt2;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SlowDCT INSTANCE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EINSTANCE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public SlowDCT()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 130, 105, 106, 106, 103, 108, 105, 127,
		9, 127, 2, 234, 61, 41, 236, 71, 240, 55,
		42, 234, 78, 118, 114, 108, 126, 159, 1, 105,
		60, 233, 60, 236, 73
	})]
	public override short[] encode(byte[] orig)
	{
		short[] result = new short[64];
		for (int u = 0; u < 8; u++)
		{
			for (int v = 0; v < 8; v++)
			{
				float sum = 0f;
				for (int j = 0; j < 8; j++)
				{
					for (int l = 0; l < 8; l++)
					{
						sum = (float)((double)sum + (double)(float)(int)orig[j * 8 + l] * java.lang.Math.cos(System.Math.PI / 8.0 * ((double)j + 0.5) * (double)u) * java.lang.Math.cos(System.Math.PI / 8.0 * ((double)l + 0.5) * (double)v));
					}
				}
				result[u * 8 + v] = (sbyte)ByteCodeHelper.f2i(sum);
			}
		}
		result[0] = (sbyte)ByteCodeHelper.f2i((float)result[0] / 8f);
		double sqrt2 = java.lang.Math.sqrt(2.0);
		for (int i = 1; i < 8; i++)
		{
			result[i] = (sbyte)ByteCodeHelper.d2i((double)(float)result[0] * sqrt2 / 8.0);
			result[i * 8] = (sbyte)ByteCodeHelper.d2i((double)(float)result[0] * sqrt2 / 8.0);
			for (int k = 1; k < 8; k++)
			{
				result[i * 8 + k] = (sbyte)ByteCodeHelper.f2i((float)result[0] / 4f);
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 66, 105, 99, 106, 106, 104, 100, 108,
		115, 108, 115, 104, 127, 2, 127, 2, 127, 0,
		231, 58, 12, 236, 75, 112, 117, 106, 239, 47,
		42, 234, 85
	})]
	public override int[] decode(int[] orig)
	{
		int[] res = new int[64];
		int i = 0;
		for (int y = 0; y < 8; y++)
		{
			for (int x = 0; x < 8; x++)
			{
				double sum = 0.0;
				int pixOffset = 0;
				for (int u = 0; u < 8; u++)
				{
					double cu = ((u != 0) ? 1.0 : rSqrt2);
					for (int v = 0; v < 8; v++)
					{
						double cv = ((v != 0) ? 1.0 : rSqrt2);
						double svu = orig[pixOffset];
						double c1 = (double)((2 * x + 1) * v) * System.Math.PI / 16.0;
						double c2 = (double)((2 * y + 1) * u) * System.Math.PI / 16.0;
						sum += cu * cv * svu * java.lang.Math.cos(c1) * java.lang.Math.cos(c2);
						pixOffset++;
					}
				}
				sum *= 0.25;
				sum = java.lang.Math.round(sum + 128.0);
				int isum = ByteCodeHelper.d2i(sum);
				int num = i;
				i++;
				res[num] = ImageConvert.icrop(isum);
			}
		}
		return res;
	}

	[LineNumberTable(new byte[] { 159, 138, 98, 139 })]
	static SlowDCT()
	{
		___003C_003EINSTANCE = new SlowDCT();
		rSqrt2 = 1.0 / java.lang.Math.sqrt(2.0);
	}
}
