using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.containers.mp4;

public class Chunk : Object
{
	private long offset;

	private long startTv;

	private int sampleCount;

	private int sampleSize;

	private int[] sampleSizes;

	private int sampleDur;

	private int[] sampleDurs;

	private int entry;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 98, 105, 104, 104, 104, 105, 105, 105,
		105, 105
	})]
	public Chunk(long offset, long startTv, int sampleCount, int sampleSize, int[] sampleSizes, int sampleDur, int[] sampleDurs, int entry)
	{
		this.offset = offset;
		this.startTv = startTv;
		this.sampleCount = sampleCount;
		this.sampleSize = sampleSize;
		this.sampleSizes = sampleSizes;
		this.sampleDur = sampleDur;
		this.sampleDurs = sampleDurs;
		this.entry = entry;
	}

	[LineNumberTable(33)]
	public virtual long getOffset()
	{
		return offset;
	}

	[LineNumberTable(37)]
	public virtual long getStartTv()
	{
		return startTv;
	}

	[LineNumberTable(41)]
	public virtual int getSampleCount()
	{
		return sampleCount;
	}

	[LineNumberTable(45)]
	public virtual int getSampleSize()
	{
		return sampleSize;
	}

	[LineNumberTable(49)]
	public virtual int[] getSampleSizes()
	{
		return sampleSizes;
	}

	[LineNumberTable(53)]
	public virtual int getSampleDur()
	{
		return sampleDur;
	}

	[LineNumberTable(57)]
	public virtual int[] getSampleDurs()
	{
		return sampleDurs;
	}

	[LineNumberTable(61)]
	public virtual int getEntry()
	{
		return entry;
	}

	[LineNumberTable(new byte[] { 159, 126, 98, 106, 111, 99, 109, 106, 5, 199 })]
	public virtual int getDuration()
	{
		if (sampleDur > 0)
		{
			return sampleDur * sampleCount;
		}
		int sum = 0;
		for (int j = 0; j < (nint)sampleDurs.LongLength; j++)
		{
			int i = sampleDurs[j];
			sum += i;
		}
		return sum;
	}

	[LineNumberTable(new byte[] { 159, 123, 66, 106, 112, 100, 109, 106, 6, 199 })]
	public virtual long getSize()
	{
		if (sampleSize > 0)
		{
			return sampleSize * sampleCount;
		}
		long sum = 0L;
		for (int j = 0; j < (nint)sampleSizes.LongLength; j++)
		{
			int i = sampleSizes[j];
			sum += i;
		}
		return sum;
	}
}
