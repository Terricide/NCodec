using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace net.sourceforge.jaad.aac.gain;

internal class FFT : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[][] FFT_TABLE_128;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[][] FFT_TABLE_16;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	internal FFT()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 120, 162, 126, 182, 127, 7, 100, 108, 110,
		110, 102, 108, 104, 137, 232, 56, 236, 74, 105,
		110, 14, 233, 70, 102, 132, 105, 108, 103, 100,
		101, 109, 100, 109, 127, 4, 127, 4, 115, 115,
		119, 119, 104, 103, 231, 55, 236, 75, 104, 232,
		50, 236, 80, 103, 231, 43, 236, 87
	})]
	internal static void process(float[][] _in, int n)
	{
		int ln = (int)Math.round(Math.log(n) / Math.log(2.0));
		float[][] table = ((n != 128) ? FFT_TABLE_16 : FFT_TABLE_128);
		int[] array = new int[2];
		int num = (array[1] = 2);
		num = (array[0] = n);
		float[][] rev = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		int ii = 0;
		for (int i = 0; i < n; i++)
		{
			rev[i][0] = _in[ii][0];
			rev[i][1] = _in[ii][1];
			int l = n >> 1;
			while (ii >= l && l > 0)
			{
				ii -= l;
				l >>= 1;
			}
			ii += l;
		}
		for (int i = 0; i < n; i++)
		{
			_in[i][0] = rev[i][0];
			_in[i][1] = rev[i][1];
		}
		int blocks = n / 2;
		int size = 2;
		float[] a = new float[2];
		for (int i = 0; i < ln; i++)
		{
			int size2 = size / 2;
			int k2 = 0;
			int k3 = size2;
			for (int j = 0; j < blocks; j++)
			{
				int m = 0;
				for (int k = 0; k < size2; k++)
				{
					a[0] = _in[k3][0] * table[m][0] - _in[k3][1] * table[m][1];
					a[1] = _in[k3][0] * table[m][1] + _in[k3][1] * table[m][0];
					_in[k3][0] = _in[k2][0] - a[0];
					_in[k3][1] = _in[k2][1] - a[1];
					float[] obj = _in[k2];
					num = 0;
					float[] array2 = obj;
					array2[num] += a[0];
					float[] obj2 = _in[k2];
					num = 1;
					array2 = obj2;
					array2[num] += a[1];
					m += blocks;
					k2++;
					k3++;
				}
				k2 += size2;
				k3 += size2;
			}
			blocks /= 2;
			size *= 2;
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		139,
		98,
		byte.MaxValue,
		166,
		37,
		160,
		66
	})]
	static FFT()
	{
		FFT_TABLE_128 = new float[64][]
		{
			new float[2] { 1f, -0f },
			new float[2] { 0.99879545f, -0.0490676761f },
			new float[2] { 0.9951847f, -0.09801714f },
			new float[2] { 0.9891765f, -0.146730468f },
			new float[2] { 0.980785251f, -0.195090324f },
			new float[2] { 0.970031261f, -0.242980182f },
			new float[2] { 0.956940353f, -0.290284663f },
			new float[2] { 0.941544056f, -0.336889863f },
			new float[2] { 0.9238795f, -0.382683426f },
			new float[2] { 0.9039893f, -0.427555084f },
			new float[2] { 0.8819213f, -0.471396744f },
			new float[2] { 0.8577286f, -0.514102757f },
			new float[2] { 0.8314696f, -0.555570245f },
			new float[2] { 0.8032075f, -0.5956993f },
			new float[2] { 0.773010433f, -0.6343933f },
			new float[2] { 0.7409511f, -0.671559f },
			new float[2] { 0.707106769f, -0.707106769f },
			new float[2] { 0.671559f, -0.7409511f },
			new float[2] { 0.6343933f, -0.773010433f },
			new float[2] { 0.5956993f, -0.8032075f },
			new float[2] { 0.555570245f, -0.8314696f },
			new float[2] { 0.514102757f, -0.8577286f },
			new float[2] { 0.471396744f, -0.8819213f },
			new float[2] { 0.427555084f, -0.9039893f },
			new float[2] { 0.382683426f, -0.9238795f },
			new float[2] { 0.336889863f, -0.941544056f },
			new float[2] { 0.290284663f, -0.956940353f },
			new float[2] { 0.242980182f, -0.970031261f },
			new float[2] { 0.195090324f, -0.980785251f },
			new float[2] { 0.146730468f, -0.9891765f },
			new float[2] { 0.09801714f, -0.9951847f },
			new float[2] { 0.0490676761f, -0.99879545f },
			new float[2] { 6.123234E-17f, -1f },
			new float[2] { -0.0490676761f, -0.99879545f },
			new float[2] { -0.09801714f, -0.9951847f },
			new float[2] { -0.146730468f, -0.9891765f },
			new float[2] { -0.195090324f, -0.980785251f },
			new float[2] { -0.242980182f, -0.970031261f },
			new float[2] { -0.290284663f, -0.956940353f },
			new float[2] { -0.336889863f, -0.941544056f },
			new float[2] { -0.382683426f, -0.9238795f },
			new float[2] { -0.427555084f, -0.9039893f },
			new float[2] { -0.471396744f, -0.8819213f },
			new float[2] { -0.514102757f, -0.8577286f },
			new float[2] { -0.555570245f, -0.8314696f },
			new float[2] { -0.5956993f, -0.8032075f },
			new float[2] { -0.6343933f, -0.773010433f },
			new float[2] { -0.671559f, -0.7409511f },
			new float[2] { -0.707106769f, -0.707106769f },
			new float[2] { -0.7409511f, -0.671559f },
			new float[2] { -0.773010433f, -0.6343933f },
			new float[2] { -0.8032075f, -0.5956993f },
			new float[2] { -0.8314696f, -0.555570245f },
			new float[2] { -0.8577286f, -0.514102757f },
			new float[2] { -0.8819213f, -0.471396744f },
			new float[2] { -0.9039893f, -0.427555084f },
			new float[2] { -0.9238795f, -0.382683426f },
			new float[2] { -0.941544056f, -0.336889863f },
			new float[2] { -0.956940353f, -0.290284663f },
			new float[2] { -0.970031261f, -0.242980182f },
			new float[2] { -0.980785251f, -0.195090324f },
			new float[2] { -0.9891765f, -0.146730468f },
			new float[2] { -0.9951847f, -0.09801714f },
			new float[2] { -0.99879545f, -0.0490676761f }
		};
		FFT_TABLE_16 = new float[8][]
		{
			new float[2] { 1f, -0f },
			new float[2] { 0.9238795f, -0.382683426f },
			new float[2] { 0.707106769f, -0.707106769f },
			new float[2] { 0.382683426f, -0.9238795f },
			new float[2] { 6.123234E-17f, -1f },
			new float[2] { -0.382683426f, -0.9238795f },
			new float[2] { -0.707106769f, -0.707106769f },
			new float[2] { -0.9238795f, -0.382683426f }
		};
	}
}
