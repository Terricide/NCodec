using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace net.sourceforge.jaad.aac.gain;

[Implements(new string[] { "net.sourceforge.jaad.aac.gain.GCConstants", "net.sourceforge.jaad.aac.gain.PQFTables" })]
internal class IPQF : Object, GCConstants, PQFTables
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[] buf;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] tmp1;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] tmp2;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 105, 109, 127, 12, 127, 12 })]
	internal IPQF()
	{
		buf = new float[4];
		int[] array = new int[2];
		int num = (array[1] = 24);
		num = (array[0] = 2);
		tmp1 = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 24);
		num = (array[0] = 2);
		tmp2 = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 66, 103, 42, 199, 105, 103, 46, 167,
		242, 60, 231, 70
	})]
	internal virtual void process(float[][] _in, int frameLen, int maxBand, float[] @out)
	{
		for (int i = 0; i < frameLen; i++)
		{
			@out[i] = 0f;
		}
		for (int i = 0; i < frameLen / 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				buf[j] = _in[j][i];
			}
			performSynthesis(buf, @out, i * 4);
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 133, 98, 196, 103, 104, 119, 23, 39, 231,
		71, 106, 103, 105, 53, 169, 141, 103, 105, 53,
		169, 237, 53, 234, 78, 106, 104, 106, 63, 4,
		169, 106, 63, 4, 169, 136, 104, 106, 63, 6,
		169, 106, 63, 6, 169, 236, 47, 234, 83
	})]
	private void performSynthesis(float[] _in, float[] @out, int outOff)
	{
		int kk = 12;
		for (int l = 0; l < 2; l++)
		{
			for (int k = 0; k < 23; k++)
			{
				tmp1[l][k] = tmp1[l][k + 1];
				tmp2[l][k] = tmp2[l][k + 1];
			}
		}
		for (int l = 0; l < 2; l++)
		{
			float acc2 = 0f;
			for (int i = 0; i < 4; i++)
			{
				acc2 += PQFTables.COEFS_Q0[l][i] * _in[i];
			}
			tmp1[l][23] = acc2;
			acc2 = 0f;
			for (int i = 0; i < 4; i++)
			{
				acc2 += PQFTables.COEFS_Q1[l][i] * _in[i];
			}
			tmp2[l][23] = acc2;
		}
		for (int l = 0; l < 2; l++)
		{
			float acc = 0f;
			for (int j = 0; j < 12; j++)
			{
				acc += PQFTables.COEFS_T0[l][j] * tmp1[l][23 - 2 * j];
			}
			for (int j = 0; j < 12; j++)
			{
				acc += PQFTables.COEFS_T1[l][j] * tmp2[l][22 - 2 * j];
			}
			@out[outOff + l] = acc;
			acc = 0f;
			for (int j = 0; j < 12; j++)
			{
				acc += PQFTables.COEFS_T0[3 - l][j] * tmp1[l][23 - 2 * j];
			}
			for (int j = 0; j < 12; j++)
			{
				acc -= PQFTables.COEFS_T1[3 - l][j] * tmp2[l][22 - 2 * j];
			}
			@out[outOff + 4 - 1 - l] = acc;
		}
	}
}
