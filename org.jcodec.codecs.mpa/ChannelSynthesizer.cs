using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace org.jcodec.codecs.mpa;

internal class ChannelSynthesizer : Object
{
	private float[][] v;

	private int pos;

	private float scalefactor;

	private int current;

	[LineNumberTable(new byte[]
	{
		159, 136, 98, 104, 43, 167, 104, 46, 167, 111,
		138, 104, 53, 167, 104, 52, 167
	})]
	private static void distributeSamples(int pos, float[] dest, float[] next, float[] s)
	{
		for (int l = 0; l < 16; l++)
		{
			dest[(l << 4) + pos] = s[l];
		}
		for (int k = 1; k < 17; k++)
		{
			next[(k << 4) + pos] = s[15 + k];
		}
		dest[256 + pos] = 0f;
		next[0 + pos] = 0f - s[0];
		for (int j = 0; j < 15; j++)
		{
			dest[272 + (j << 4) + pos] = 0f - s[15 - j];
		}
		for (int i = 0; i < 15; i++)
		{
			next[272 + (i << 4) + pos] = s[30 - i];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 105, 127, 15, 105, 105 })]
	public ChannelSynthesizer(int channelnumber, float factor)
	{
		int[] array = new int[2];
		int num = (array[1] = 512);
		num = (array[0] = 2);
		v = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		scalefactor = factor;
		pos = 15;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 162, 109, 108, 127, 3, 159, 2, 114,
		104
	})]
	public virtual void synthesize(float[] coeffs, short[] @out, int off)
	{
		MpaPqmf.computeButterfly(pos, coeffs);
		int next = (current ^ -1) & 1;
		distributeSamples(pos, v[current], v[next], coeffs);
		MpaPqmf.computeFilter(pos, v[current], @out, off, scalefactor);
		pos = (pos + 1) & 0xF;
		current = next;
	}
}
