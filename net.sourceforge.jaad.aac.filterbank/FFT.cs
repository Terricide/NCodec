using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace net.sourceforge.jaad.aac.filterbank;

internal class FFT : Object, FFTTables
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int length;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] roots;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] rev;

	private float[] a;

	private float[] b;

	private float[] c;

	private float[] d;

	private float[] e1;

	private float[] e2;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 138, 162, 105, 136, 159, 12, 108, 131, 108,
		131, 108, 131, 108, 131, 223, 7, 127, 11, 109,
		109, 109, 109, 109, 109
	})]
	internal FFT(int length)
	{
		this.length = length;
		switch (length)
		{
		case 64:
			roots = FFTTables.FFT_TABLE_64;
			break;
		case 512:
			roots = FFTTables.FFT_TABLE_512;
			break;
		case 60:
			roots = FFTTables.FFT_TABLE_60;
			break;
		case 480:
			roots = FFTTables.FFT_TABLE_480;
			break;
		default:
		{
			string message = new StringBuilder().append("unexpected FFT length: ").append(length).toString();
			
			throw new AACException(message);
		}
		}
		int[] array = new int[2];
		int num = (array[1] = 2);
		num = (array[0] = length);
		rev = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		a = new float[2];
		b = new float[2];
		c = new float[2];
		d = new float[2];
		e1 = new float[2];
		e2 = new float[2];
	}

	[LineNumberTable(new byte[]
	{
		159, 130, 129, 67, 105, 142, 99, 113, 113, 113,
		107, 107, 102, 137, 230, 56, 236, 74, 110, 114,
		18, 233, 70, 113, 121, 121, 123, 123, 121, 121,
		123, 123, 121, 121, 123, 155, 123, 123, 123, 123,
		100, 113, 113, 113, 179, 113, 113, 113, 241, 36,
		236, 99, 113, 103, 116, 113, 109, 104, 110, 110,
		127, 6, 159, 6, 126, 126, 123, 251, 54, 44,
		237, 61, 236, 82
	})]
	internal virtual void process(float[][] _in, bool forward)
	{
		int imOff = ((!forward) ? 1 : 2);
		int scale = ((!forward) ? 1 : length);
		int ii = 0;
		for (int l = 0; l < length; l++)
		{
			rev[l][0] = _in[ii][0];
			rev[l][1] = _in[ii][1];
			int k2 = length >> 1;
			while (ii >= k2 && k2 > 0)
			{
				ii -= k2;
				k2 >>= 1;
			}
			ii += k2;
		}
		for (int k = 0; k < length; k++)
		{
			_in[k][0] = rev[k][0];
			_in[k][1] = rev[k][1];
		}
		for (int j = 0; j < length; j += 4)
		{
			a[0] = _in[j][0] + _in[j + 1][0];
			a[1] = _in[j][1] + _in[j + 1][1];
			b[0] = _in[j + 2][0] + _in[j + 3][0];
			b[1] = _in[j + 2][1] + _in[j + 3][1];
			c[0] = _in[j][0] - _in[j + 1][0];
			c[1] = _in[j][1] - _in[j + 1][1];
			d[0] = _in[j + 2][0] - _in[j + 3][0];
			d[1] = _in[j + 2][1] - _in[j + 3][1];
			_in[j][0] = a[0] + b[0];
			_in[j][1] = a[1] + b[1];
			_in[j + 2][0] = a[0] - b[0];
			_in[j + 2][1] = a[1] - b[1];
			e1[0] = c[0] - d[1];
			e1[1] = c[1] + d[0];
			e2[0] = c[0] + d[1];
			e2[1] = c[1] - d[0];
			if (forward)
			{
				_in[j + 1][0] = e2[0];
				_in[j + 1][1] = e2[1];
				_in[j + 3][0] = e1[0];
				_in[j + 3][1] = e1[1];
			}
			else
			{
				_in[j + 1][0] = e1[0];
				_in[j + 1][1] = e1[1];
				_in[j + 3][0] = e2[0];
				_in[j + 3][1] = e2[1];
			}
		}
		for (int i = 4; i < length; i <<= 1)
		{
			int shift = i << 1;
			int num = length;
			int m2 = ((shift != -1) ? (num / shift) : (-num));
			for (int m = 0; m < length; m += shift)
			{
				for (int n = 0; n < i; n++)
				{
					int km = n * m2;
					float rootRe = roots[km][0];
					float rootIm = roots[km][imOff];
					float zRe = _in[i + m + n][0] * rootRe - _in[i + m + n][1] * rootIm;
					float zIm = _in[i + m + n][0] * rootIm + _in[i + m + n][1] * rootRe;
					_in[i + m + n][0] = (_in[m + n][0] - zRe) * (float)scale;
					_in[i + m + n][1] = (_in[m + n][1] - zIm) * (float)scale;
					_in[m + n][0] = (_in[m + n][0] + zRe) * (float)scale;
					_in[m + n][1] = (_in[m + n][1] + zIm) * (float)scale;
				}
			}
		}
	}
}
