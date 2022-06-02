using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace org.jcodec.common;

public class IntIntHistogram : IntIntMap
{
	private int maxBin;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 142, 162, 105 })]
	public IntIntHistogram()
	{
		maxBin = -1;
	}

	[LineNumberTable(7)]
	public virtual int max()
	{
		return maxBin;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 140, 162, 105, 112, 137, 106, 104, 110, 101,
		104, 131
	})]
	public virtual void increment(int bin)
	{
		int count = get(bin);
		count = ((count == int.MinValue) ? 1 : (1 + count));
		put(bin, count);
		if (maxBin == -1)
		{
			maxBin = bin;
		}
		int maxCount = get(maxBin);
		if (count > maxCount)
		{
			maxBin = bin;
			maxCount = count;
		}
	}
}
