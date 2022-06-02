using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;

namespace org.jcodec.audio;

public class LanczosInterpolator : java.lang.Object, AudioFilter
{
	private double rateStep;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(17)]
	public static double lanczos(double x, int a)
	{
		float num;
		double result;
		if (x < (double)(-a))
		{
			num = 0f;
			result = num;
		}
		else if (x > (double)a)
		{
			num = 0f;
			result = num;
		}
		else
		{
			result = (double)a * java.lang.Math.sin(System.Math.PI * x) * java.lang.Math.sin(System.Math.PI * x / (double)a) / (9.869604401089358 * x * x);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 105, 109 })]
	public LanczosInterpolator(int fromRate, int toRate)
	{
		rateStep = (double)fromRate / (double)toRate;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 102, 159, 17, 102, 191, 17, 101,
		133, 122, 113, 106, 145, 99, 159, 20, 110, 110,
		109, 108, 166, 105, 113, 137, 108, 140, 114, 146,
		118, 150, 155, 127, 31, 62, 135, 99, 240, 36,
		234, 95
	})]
	public virtual void filter(FloatBuffer[] _in, long[] pos, FloatBuffer[] @out)
	{
		if ((nint)_in.LongLength != 1)
		{
			string s = new StringBuilder().append(java.lang.Object.instancehelper_getClass(this).getName()).append(" filter is designed to work only on one input").toString();
			
			throw new IllegalArgumentException(s);
		}
		if ((nint)@out.LongLength != 1)
		{
			string s2 = new StringBuilder().append(java.lang.Object.instancehelper_getClass(this).getName()).append(" filter is designed to work only on one output").toString();
			
			throw new IllegalArgumentException(s2);
		}
		FloatBuffer in0 = _in[0];
		FloatBuffer out2 = @out[0];
		if ((double)out2.remaining() < (double)(in0.remaining() - 6) / rateStep)
		{
			
			throw new IllegalArgumentException("Output buffer is too small");
		}
		if (in0.remaining() <= 6)
		{
			
			throw new IllegalArgumentException("Input buffer should contain > 6 samples.");
		}
		int outSample = 0;
		int p0i;
		while (true)
		{
			double inSample = 3.0 + (double)outSample * rateStep + java.lang.Math.ceil((double)pos[0] / rateStep) * rateStep - (double)pos[0];
			p0i = ByteCodeHelper.d2i(java.lang.Math.floor(inSample));
			int q0i = ByteCodeHelper.d2i(java.lang.Math.ceil(inSample));
			if (p0i >= in0.limit() - 3)
			{
				break;
			}
			double p0d = (double)p0i - inSample;
			if (p0d < -0.001)
			{
				double q0d = (double)q0i - inSample;
				double p0c = lanczos(p0d, 3);
				double q0c = lanczos(q0d, 3);
				double p1c = lanczos(p0d - 1.0, 3);
				double q1c = lanczos(q0d + 1.0, 3);
				double p2c = lanczos(p0d - 2.0, 3);
				double q2c = lanczos(q0d + 2.0, 3);
				double factor = 1.0 / (p0c + p1c + p2c + q0c + q1c + q2c);
				out2.put((float)(((double)in0.get(q0i) * q0c + (double)in0.get(q0i + 1) * q1c + (double)in0.get(q0i + 2) * q2c + (double)in0.get(p0i) * p0c + (double)in0.get(p0i - 1) * p1c + (double)in0.get(p0i - 2) * p2c) * factor));
			}
			else
			{
				out2.put(in0.get(p0i));
			}
			outSample++;
		}
		in0.position(p0i - 3);
	}

	[LineNumberTable(78)]
	public virtual int getDelay()
	{
		return 3;
	}

	[LineNumberTable(83)]
	public virtual int getNInputs()
	{
		return 1;
	}

	[LineNumberTable(88)]
	public virtual int getNOutputs()
	{
		return 1;
	}
}
