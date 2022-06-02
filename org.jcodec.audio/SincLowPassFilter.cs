using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.audio;

public class SincLowPassFilter : ConvolutionFilter
{
	private int kernelSize;

	private double cutoffFreq;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 105, 104, 106 })]
	public SincLowPassFilter(int kernelSize, double cutoffFreq)
	{
		this.kernelSize = kernelSize;
		this.cutoffFreq = cutoffFreq;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(26)]
	public static SincLowPassFilter createSincLowPassFilter(double cutoffFreq)
	{
		SincLowPassFilter result = new SincLowPassFilter(40, cutoffFreq);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(30)]
	public static SincLowPassFilter createSincLowPassFilter2(int cutoffFreq, int samplingRate)
	{
		SincLowPassFilter result = new SincLowPassFilter(40, (double)cutoffFreq / (double)samplingRate);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 66, 109, 103, 111, 108, 228, 69, 127,
		55, 173, 117, 232, 52, 234, 78, 110, 53, 169
	})]
	protected internal override double[] buildKernel()
	{
		double[] kernel = new double[kernelSize];
		double sum = 0.0;
		for (int j = 0; j < kernelSize; j++)
		{
			if (j - kernelSize / 2 != 0)
			{
				kernel[j] = java.lang.Math.sin(System.Math.PI * 2.0 * cutoffFreq * (double)(j - kernelSize / 2)) / (double)(j - kernelSize / 2) * (0.54 - 0.46 * java.lang.Math.cos(System.Math.PI * 2.0 * (double)j / (double)kernelSize));
			}
			else
			{
				kernel[j] = System.Math.PI * 2.0 * cutoffFreq;
			}
			sum += kernel[j];
		}
		for (int i = 0; i < kernelSize; i++)
		{
			int num = i;
			double[] array = kernel;
			array[num] /= sum;
		}
		return kernel;
	}
}
