using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace net.sourceforge.jaad.aac.filterbank;

internal class MDCT : Object, MDCTTables
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int N;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int N2;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int N4;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int N8;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] sincos;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private FFT fft;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] buf;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[] tmp;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 137, 66, 105, 104, 106, 106, 106, 159, 18,
		108, 131, 108, 131, 108, 131, 140, 159, 7, 114,
		127, 16, 109
	})]
	internal MDCT(int length)
	{
		N = length;
		N2 = length >> 1;
		N4 = length >> 2;
		N8 = length >> 3;
		if (length != 240)
		{
			if (length != 256)
			{
				if (length != 1920)
				{
					if (length != 2048)
					{
						goto IL_009a;
					}
					sincos = MDCTTables.MDCT_TABLE_2048;
				}
				else
				{
					sincos = MDCTTables.MDCT_TABLE_1920;
				}
			}
			else
			{
				sincos = MDCTTables.MDCT_TABLE_128;
			}
			fft = new FFT(N4);
			int n = N4;
			int[] array = new int[2];
			int num = (array[1] = 2);
			num = (array[0] = n);
			buf = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
			tmp = new float[2];
			return;
		}
		sincos = MDCTTables.MDCT_TABLE_240;
		goto IL_009a;
		IL_009a:
		string message = new StringBuilder().append("unsupported MDCT length: ").append(length).toString();
		
		throw new AACException(message);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		130,
		98,
		111,
		127,
		29,
		31,
		29,
		234,
		70,
		179,
		111,
		115,
		115,
		127,
		22,
		byte.MaxValue,
		22,
		60,
		234,
		72,
		111,
		122,
		158,
		127,
		0,
		159,
		0,
		122,
		158,
		127,
		7,
		159,
		7,
		127,
		2,
		159,
		6,
		127,
		7,
		159,
		7,
		127,
		3,
		159,
		7,
		127,
		13,
		byte.MaxValue,
		13,
		41,
		234,
		89
	})]
	internal virtual void process(float[] _in, int inOff, float[] @out, int outOff)
	{
		for (int i = 0; i < N4; i++)
		{
			buf[i][1] = _in[inOff + 2 * i] * sincos[i][0] + _in[inOff + N2 - 1 - 2 * i] * sincos[i][1];
			buf[i][0] = _in[inOff + N2 - 1 - 2 * i] * sincos[i][0] - _in[inOff + 2 * i] * sincos[i][1];
		}
		fft.process(buf, forward: false);
		for (int i = 0; i < N4; i++)
		{
			tmp[0] = buf[i][0];
			tmp[1] = buf[i][1];
			buf[i][1] = tmp[1] * sincos[i][0] + tmp[0] * sincos[i][1];
			buf[i][0] = tmp[0] * sincos[i][0] - tmp[1] * sincos[i][1];
		}
		for (int i = 0; i < N8; i += 2)
		{
			@out[outOff + 2 * i] = buf[N8 + i][1];
			@out[outOff + 2 + 2 * i] = buf[N8 + 1 + i][1];
			@out[outOff + 1 + 2 * i] = 0f - buf[N8 - 1 - i][0];
			@out[outOff + 3 + 2 * i] = 0f - buf[N8 - 2 - i][0];
			@out[outOff + N4 + 2 * i] = buf[i][0];
			@out[outOff + N4 + 2 + 2 * i] = buf[1 + i][0];
			@out[outOff + N4 + 1 + 2 * i] = 0f - buf[N4 - 1 - i][1];
			@out[outOff + N4 + 3 + 2 * i] = 0f - buf[N4 - 2 - i][1];
			@out[outOff + N2 + 2 * i] = buf[N8 + i][0];
			@out[outOff + N2 + 2 + 2 * i] = buf[N8 + 1 + i][0];
			@out[outOff + N2 + 1 + 2 * i] = 0f - buf[N8 - 1 - i][1];
			@out[outOff + N2 + 3 + 2 * i] = 0f - buf[N8 - 2 - i][1];
			@out[outOff + N2 + N4 + 2 * i] = 0f - buf[i][1];
			@out[outOff + N2 + N4 + 2 + 2 * i] = 0f - buf[1 + i][1];
			@out[outOff + N2 + N4 + 1 + 2 * i] = buf[N4 - 1 - i][0];
			@out[outOff + N2 + N4 + 3 + 2 * i] = buf[N4 - 2 - i][0];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		118,
		66,
		111,
		101,
		127,
		16,
		159,
		2,
		127,
		22,
		159,
		22,
		123,
		155,
		122,
		159,
		2,
		127,
		43,
		159,
		43,
		127,
		3,
		byte.MaxValue,
		3,
		46,
		234,
		86,
		179,
		111,
		134,
		127,
		24,
		159,
		24,
		110,
		118,
		117,
		246,
		55,
		234,
		75
	})]
	internal virtual void processForward(float[] _in, float[] @out)
	{
		for (int i = 0; i < N8; i++)
		{
			int k = i << 1;
			tmp[0] = _in[N - N4 - 1 - k] + _in[N - N4 + k];
			tmp[1] = _in[N4 + k] - _in[N4 - 1 - k];
			buf[i][0] = tmp[0] * sincos[i][0] + tmp[1] * sincos[i][1];
			buf[i][1] = tmp[1] * sincos[i][0] - tmp[0] * sincos[i][1];
			float[] obj = buf[i];
			int num = 0;
			float[] array = obj;
			array[num] *= (float)N;
			float[] obj2 = buf[i];
			num = 1;
			array = obj2;
			array[num] *= (float)N;
			tmp[0] = _in[N2 - 1 - k] - _in[k];
			tmp[1] = _in[N2 + k] + _in[N - 1 - k];
			buf[i + N8][0] = tmp[0] * sincos[i + N8][0] + tmp[1] * sincos[i + N8][1];
			buf[i + N8][1] = tmp[1] * sincos[i + N8][0] - tmp[0] * sincos[i + N8][1];
			float[] obj3 = buf[i + N8];
			num = 0;
			array = obj3;
			array[num] *= (float)N;
			float[] obj4 = buf[i + N8];
			num = 1;
			array = obj4;
			array[num] *= (float)N;
		}
		fft.process(buf, forward: true);
		for (int i = 0; i < N4; i++)
		{
			int j = i << 1;
			tmp[0] = buf[i][0] * sincos[i][0] + buf[i][1] * sincos[i][1];
			tmp[1] = buf[i][1] * sincos[i][0] - buf[i][0] * sincos[i][1];
			@out[j] = 0f - tmp[0];
			@out[N2 - 1 - j] = tmp[1];
			@out[N2 + j] = 0f - tmp[1];
			@out[N - 1 - j] = tmp[0];
		}
	}
}
