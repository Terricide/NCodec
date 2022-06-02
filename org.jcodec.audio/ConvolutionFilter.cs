using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.audio;

public abstract class ConvolutionFilter : Object, AudioFilter
{
	private double[] kernel;

	protected internal abstract double[] buildKernel();

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public ConvolutionFilter()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 130, 102, 159, 17, 102, 191, 17, 101,
		133, 105, 173, 119, 113, 112, 191, 23, 171, 120,
		104, 111, 62, 169, 235, 59, 234, 71, 107
	})]
	public virtual void filter(FloatBuffer[] _in, long[] pos, FloatBuffer[] @out)
	{
		if ((nint)_in.LongLength != 1)
		{
			string s = new StringBuilder().append(Object.instancehelper_getClass(this).getName()).append(" filter is designed to work only on one input").toString();
			
			throw new IllegalArgumentException(s);
		}
		if ((nint)@out.LongLength != 1)
		{
			string s2 = new StringBuilder().append(Object.instancehelper_getClass(this).getName()).append(" filter is designed to work only on one output").toString();
			
			throw new IllegalArgumentException(s2);
		}
		FloatBuffer in0 = _in[0];
		FloatBuffer out2 = @out[0];
		if (kernel == null)
		{
			kernel = buildKernel();
		}
		if (out2.remaining() < in0.remaining() - (nint)kernel.LongLength)
		{
			
			throw new IllegalArgumentException("Output buffer is too small");
		}
		if (in0.remaining() <= (nint)kernel.LongLength)
		{
			string s3 = new StringBuilder().append("Input buffer should contain > kernel lenght (").append(kernel.Length).append(") samples.")
				.toString();
			
			throw new IllegalArgumentException(s3);
		}
		int halfKernel = (int)((nint)kernel.LongLength / 2);
		int i;
		for (i = in0.position() + halfKernel; i < in0.limit() - halfKernel; i++)
		{
			double result = 0.0;
			for (int j = 0; j < (nint)kernel.LongLength; j++)
			{
				result += kernel[j] * (double)in0.get(i + j - halfKernel);
			}
			out2.put((float)result);
		}
		in0.position(i - halfKernel);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 98, 105, 141 })]
	public virtual int getDelay()
	{
		if (kernel == null)
		{
			kernel = buildKernel();
		}
		return (int)((nint)kernel.LongLength / 2);
	}

	[LineNumberTable(65)]
	public virtual int getNInputs()
	{
		return 1;
	}

	[LineNumberTable(70)]
	public virtual int getNOutputs()
	{
		return 1;
	}
}
